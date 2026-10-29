using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Data.Sqlite;

namespace DynamicJsonToSql
{
    public partial class MainForm : Form
    {
        private string json_ylu = null;
        private const string veritabani_dsyasi = "NormalizedDatabase.sqlite";
        private const string esleme_dsyasi = "SchemaMapping.json";

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// JSON dosyasını seçer ve TreeView'a yükler.
        /// </summary>
        private void btnSelectJson_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dosya_scici = new OpenFileDialog())
            {
                dosya_scici.Filter = "JSON Dosyaları (*.json)|*.json|Tüm Dosyalar (*.*)|*.*";
                dosya_scici.Title = "Analiz Edilecek JSON Dosyasını Seçin";

                if (dosya_scici.ShowDialog() == DialogResult.OK)
                {
                    json_ylu = dosya_scici.FileName;
                    try
                    {
                        string json_icrk = File.ReadAllText(json_ylu);
                        
                        // TreeView'a yükleme işlemini başlat
                        LoadJsonToTreeView(json_icrk);

                        btnConvert.Enabled = true;
                        lblStatus.Text = $"'{Path.GetFileName(json_ylu)}' dosyası yüklendi. Dönüştürmeye hazır.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dosya okunurken hata oluştu: " + ex.Message, "Dosya Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// JSON verisini TreeView üzerinde hiyerarşik ve renklendirilmiş bir şekilde gösterir.
        /// </summary>
        private void LoadJsonToTreeView(string json_icrk)
        {
            tvJsonStructure.Nodes.Clear();
            try
            {
                JToken json_tkni = JToken.Parse(json_icrk);
                tvJsonStructure.BeginUpdate();
                AddJsonNode(json_tkni, tvJsonStructure.Nodes, "JSON Root");
                tvJsonStructure.EndUpdate();
                tvJsonStructure.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON ağaç yapısı oluşturulurken hata: " + ex.Message, "Ağaç Yapısı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Rekürsif olarak JSON elemanlarını TreeView'a ekler ve renklendirir.
        /// </summary>
        private void AddJsonNode(JToken json_tkni, TreeNodeCollection ust_dgmle, string isaretci_smi)
        {
            if (json_tkni == null) return;

            if (json_tkni is JObject json_nsnsi)
            {
                // Obje düğümleri (Turuncu renkte)
                TreeNode dugum_nsnsi = new TreeNode($"{isaretci_smi} {{ }}");
                dugum_nsnsi.ForeColor = Color.FromArgb(230, 126, 34); 
                ust_dgmle.Add(dugum_nsnsi);
                foreach (var ozellik_nsnsi in json_nsnsi.Properties())
                {
                    AddJsonNode(ozellik_nsnsi.Value, dugum_nsnsi.Nodes, ozellik_nsnsi.Name);
                }
            }
            else if (json_tkni is JArray json_dzsi)
            {
                // Dizi düğümleri (Mavi renkte)
                TreeNode dugum_nsnsi = new TreeNode($"{isaretci_smi} [ {json_dzsi.Count} ]");
                dugum_nsnsi.ForeColor = Color.FromArgb(52, 152, 219); 
                ust_dgmle.Add(dugum_nsnsi);
                for (int indeks_dgeri = 0; indeks_dgeri < json_dzsi.Count; indeks_dgeri++)
                {
                    AddJsonNode(json_dzsi[indeks_dgeri], dugum_nsnsi.Nodes, $"[{indeks_dgeri}]");
                }
            }
            else
            {
                // İlkel (Primitive) değerler (Yeşil renkte)
                string deger_mtni = json_tkni.Type == JTokenType.Null ? "null" : json_tkni.ToString();
                TreeNode dugum_nsnsi = new TreeNode($"{isaretci_smi}: {deger_mtni}");
                dugum_nsnsi.ForeColor = Color.FromArgb(46, 204, 113); 
                ust_dgmle.Add(dugum_nsnsi);
            }
        }

        /// <summary>
        /// Arka plandaki dönüştürücü motoru çağırarak NoSQL verilerini SQL'e dönüştürür.
        /// </summary>
        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(json_ylu) || !File.Exists(json_ylu))
            {
                MessageBox.Show("Lütfen önce geçerli bir JSON dosyası seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Arayüz kilitlemesini önlemek için butonları devre dışı bırakıyoruz
            btnConvert.Enabled = false;
            btnSelectJson.Enabled = false;
            btnReset.Enabled = false;
            lblStatus.Text = "Dönüştürme işlemi yapılıyor, lütfen bekleyin...";

            try
            {
                string json_icrk = File.ReadAllText(json_ylu);

                // İşlemi arka planda (Thread havuzunda) çalıştırıyoruz (UI donmasını önleme)
                await Task.Run(() =>
                {
                    JsonToSqlEngine cevirici_motr = new JsonToSqlEngine();
                    DatabaseSchema sema_nsnsi = cevirici_motr.ProcessJson(json_icrk);

                    // Eşleme şemasını JSON olarak kaydet
                    string json_cktsi = JsonConvert.SerializeObject(sema_nsnsi.tablo_stirlr, Formatting.Indented);
                    File.WriteAllText(esleme_dsyasi, json_cktsi);

                    // SQLite işlemlerini çalıştır
                    SqliteExecutor islem_ypici = new SqliteExecutor();
                    islem_ypici.ExecuteSchemaAndInsert(sema_nsnsi, veritabani_dsyasi);
                });

                lblStatus.Text = "Dönüşüm başarılı! Veritabanı ve SchemaMapping.json oluşturuldu.";
                MessageBox.Show("NoSQL verileri başarıyla SQLite ilişkisel şemasına dönüştürüldü!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Veritabanındaki tabloları ComboBox'a yükle
                await LoadSqliteTablesAsync(veritabani_dsyasi);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Dönüştürme sırasında hata oluştu.";
                MessageBox.Show("Dönüşüm Hatası: " + ex.Message, "Dönüşüm Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnConvert.Enabled = true;
                btnSelectJson.Enabled = true;
                btnReset.Enabled = true;
            }
        }

        /// <summary>
        /// SQLite veritabanındaki tabloları listeler ve ComboBox'a aktarır.
        /// </summary>
        private async Task LoadSqliteTablesAsync(string vt_ylu)
        {
            cmbTables.Items.Clear();
            if (!File.Exists(vt_ylu)) return;

            try
            {
                string baglnti_mtn = "Data Source=" + vt_ylu;
                using (var sql_bglnti = new SqliteConnection(baglnti_mtn))
                {
                    await sql_bglnti.OpenAsync();
                    
                    // SQLite sistem tabloları dışındaki kullanıcı tablolarını çekiyoruz
                    string sql_srgsu = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
                    using (var sql_kmtu = new SqliteCommand(sql_srgsu, sql_bglnti))
                    using (var veri_kuycu = await sql_kmtu.ExecuteReaderAsync())
                    {
                        while (await veri_kuycu.ReadAsync())
                        {
                            cmbTables.Items.Add(veri_kuycu.GetString(0));
                        }
                    }
                }

                if (cmbTables.Items.Count > 0)
                {
                    cmbTables.SelectedIndex = 0; // İlk tabloyu seçili hale getir
                }
                else
                {
                    dgvSqlData.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tablo listesi alınırken hata oluştu: " + ex.Message, "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ComboBox'tan tablo seçildiğinde ilgili tablonun verilerini DataGridView'a doldurur.
        /// </summary>
        private async void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secilen_tbl = cmbTables.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(secilen_tbl)) return;

            if (!File.Exists(veritabani_dsyasi)) return;

            try
            {
                lblStatus.Text = $"'{secilen_tbl}' tablosu yükleniyor...";
                DataTable veri_tblsu = new DataTable();
                string baglnti_mtn = "Data Source=" + veritabani_dsyasi;

                // Microsoft.Data.Sqlite'da SqliteDataAdapter her sürümde stabil bulunmayabildiğinden
                // SqliteDataReader kullanarak DataTable'ı dinamik olarak kendimiz dolduruyoruz.
                using (var sql_bglnti = new SqliteConnection(baglnti_mtn))
                {
                    await sql_bglnti.OpenAsync();
                    string sql_srgsu = $"SELECT * FROM [{secilen_tbl}]";
                    using (var sql_kmtu = new SqliteCommand(sql_srgsu, sql_bglnti))
                    using (var veri_kuycu = await sql_kmtu.ExecuteReaderAsync())
                    {
                        // Sütun yapılarını oku
                        for (int i = 0; i < veri_kuycu.FieldCount; i++)
                        {
                            veri_tblsu.Columns.Add(veri_kuycu.GetName(i));
                        }

                        // Satır verilerini oku
                        while (await veri_kuycu.ReadAsync())
                        {
                            object[] satir_dgrlri = new object[veri_kuycu.FieldCount];
                            veri_kuycu.GetValues(satir_dgrlri);
                            veri_tblsu.Rows.Add(satir_dgrlri);
                        }
                    }
                }

                dgvSqlData.DataSource = veri_tblsu;
                dgvSqlData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                lblStatus.Text = $"'{secilen_tbl}' tablosu yüklendi. Toplam satır sayısı: {veri_tblsu.Rows.Count}";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Tablo yüklenirken hata oluştu.";
                MessageBox.Show("Tablo verisi okunurken hata oluştu: " + ex.Message, "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Veritabanındaki tüm tabloları DROP TABLE sorgusuyla siler, arayüzü sıfırlar.
        /// </summary>
        private async void btnReset_Click(object sender, EventArgs e)
        {
            if (!File.Exists(veritabani_dsyasi) && tvJsonStructure.Nodes.Count == 0)
            {
                MessageBox.Show("Sıfırlanacak aktif bir veri veya veritabanı bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var diyalog_sncu = MessageBox.Show(
                "Sistemi sıfırlamak istiyor musunuz?\nBu işlem SQLite veritabanındaki tüm tabloları DROP TABLE ile silecek ve ekranı temizleyecektir.",
                "Sıfırlama Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (diyalog_sncu != DialogResult.Yes) return;

            btnReset.Enabled = false;
            btnConvert.Enabled = false;
            btnSelectJson.Enabled = false;
            lblStatus.Text = "Veritabanı sıfırlanıyor...";

            try
            {
                if (File.Exists(veritabani_dsyasi))
                {
                    string baglnti_mtn = "Data Source=" + veritabani_dsyasi;
                    using (var sql_bglnti = new SqliteConnection(baglnti_mtn))
                    {
                        await sql_bglnti.OpenAsync();
                        using (var pragma_kmtu = new SqliteCommand("PRAGMA foreign_keys = OFF;", sql_bglnti)) { await pragma_kmtu.ExecuteNonQueryAsync(); }

                        // Tablo isimlerini al
                        List<string> kullanici_tbllri = new List<string>();
                        string sql_srgsu = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
                        using (var sql_kmtu = new SqliteCommand(sql_srgsu, sql_bglnti))
                        using (var veri_kuycu = await sql_kmtu.ExecuteReaderAsync())
                        {
                            while (await veri_kuycu.ReadAsync())
                            {
                                kullanici_tbllri.Add(veri_kuycu.GetString(0));
                            }
                        }

                        // Tüm tabloları DROP TABLE ile sil
                        using (var veritabani_islmi = sql_bglnti.BeginTransaction())
                        {
                            foreach (var tablo_dgeri in kullanici_tbllri)
                            {
                                using (var silme_kmtu = new SqliteCommand($"DROP TABLE IF EXISTS [{tablo_dgeri}];", sql_bglnti, veritabani_islmi))
                                {
                                    await silme_kmtu.ExecuteNonQueryAsync();
                                }
                            }
                            veritabani_islmi.Commit();
                        }
                    }

                    // Fiziksel veritabanı dosyasını da temizleyelim
                    try
                    {
                        File.Delete(veritabani_dsyasi);
                    }
                    catch { /* Kilitli ise sessiz geçilebilir */ }
                }

                // Oluşturulmuş olan SchemaMapping.json dosyasını da silelim
                if (File.Exists(esleme_dsyasi))
                {
                    try
                    {
                        File.Delete(esleme_dsyasi);
                    }
                    catch { }
                }

                // UI bileşenlerini temizle
                json_ylu = null;
                tvJsonStructure.Nodes.Clear();
                cmbTables.Items.Clear();
                dgvSqlData.DataSource = null;
                btnConvert.Enabled = false;

                lblStatus.Text = "Sistem başarıyla sıfırlandı. Yeni JSON dosyası yükleyebilirsiniz.";
                MessageBox.Show("Veritabanı tabloları silindi ve ekran başarıyla sıfırlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Sıfırlama hatası.";
                MessageBox.Show("Sıfırlama işlemi sırasında hata oluştu: " + ex.Message, "Sıfırlama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnReset.Enabled = true;
                btnSelectJson.Enabled = true;
            }
        }
    }
}
