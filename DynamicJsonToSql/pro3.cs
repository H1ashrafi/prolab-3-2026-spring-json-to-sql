using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Data.Sqlite;

namespace DynamicJsonToSql
{
    public class ColumnSchema
    {
        public string sutun_di;
        public string sql_tru;
        public object ham_dger;
    }

    public class ReferenceSchema
    {
        public string sutun_di;
        public string hedef_tblo;
        public int hedef_kmlik;
    }

    public class DynamicTable
    {
        public string tablo_ism;
        public int ic_kmlik;
        
        public bool zayif_vrlik = false;
        public int yerel_kmlik;
        public int ust_kmlik;
        public string ust_tblo_ismi;

        public List<ColumnSchema> sutun_lstesi = new List<ColumnSchema>();
        
        public List<ReferenceSchema> dis_anhtarlar = new List<ReferenceSchema>();

        public string GenerateParameterlessInsertForJson()
        {
            List<string> sutun_dizsi = new List<string>();
            List<string> deger_dizsi = new List<string>();

            if (zayif_vrlik)
            {
                sutun_dizsi.Add("Parent_Id");
                deger_dizsi.Add(ust_kmlik.ToString());
                sutun_dizsi.Add("Local_Id");
                deger_dizsi.Add(yerel_kmlik.ToString());
            }
            else
            {
                sutun_dizsi.Add("Id");
                deger_dizsi.Add(ic_kmlik.ToString());
            }

            foreach (var stn_dgeri in sutun_lstesi)
            {
                sutun_dizsi.Add("[" + stn_dgeri.sutun_di + "]");
                if (stn_dgeri.ham_dger == null)
                {
                    deger_dizsi.Add("NULL");
                }
                else
                {
                    deger_dizsi.Add("'" + stn_dgeri.ham_dger.ToString().Replace("'", "''") + "'"); 
                }
            }

            foreach (var dis_anhtar in dis_anhtarlar)
            {
                sutun_dizsi.Add("[" + dis_anhtar.sutun_di + "]");
                deger_dizsi.Add(dis_anhtar.hedef_kmlik.ToString());
            }

            return "INSERT INTO [" + tablo_ism + "] (" + string.Join(", ", sutun_dizsi) + ") VALUES (" + string.Join(", ", deger_dizsi) + ");";
        }
    }

    public class DatabaseSchema
    {
        public List<DynamicTable> tablo_stirlr = new List<DynamicTable>();
        
        public Dictionary<string, int> tablo_syaclari = new Dictionary<string, int>();
        
        public Dictionary<string, int> yerel_syaclar = new Dictionary<string, int>();
        
        [JsonIgnore]
        public Dictionary<string, int> tekrarsizlik_onbllegi = new Dictionary<string, int>();

        public int GetNextIdForTable(string tablo_ism)
        {
            if (!tablo_syaclari.ContainsKey(tablo_ism))
            {
                tablo_syaclari[tablo_ism] = 1;
            }
            else
            {
                tablo_syaclari[tablo_ism] = tablo_syaclari[tablo_ism] + 1;
            }
            return tablo_syaclari[tablo_ism];
        }

        public int GetNextLocalId(string tablo_ism, int ust_kmlik)
        {
            string anahtar_mtni = tablo_ism + "_" + ust_kmlik;
            if (!yerel_syaclar.ContainsKey(anahtar_mtni))
            {
                yerel_syaclar[anahtar_mtni] = 1;
            }
            else
            {
                yerel_syaclar[anahtar_mtni] = yerel_syaclar[anahtar_mtni] + 1;
            }
            return yerel_syaclar[anahtar_mtni];
        }
    }

    public class JsonToSqlEngine
    {
        public DatabaseSchema veri_tbani;
        public int maksimum_drinlik = 2;
        public int maksimum_dngu = 15;

        public DatabaseSchema ProcessJson(string json_mtni)
        {
            veri_tbani = new DatabaseSchema();
            JToken kok_tken = JToken.Parse(json_mtni);
            
            if (kok_tken is JObject)
            {
                JObject kok_nsne = (JObject)kok_tken;
                ProcessObject(kok_nsne, "Main_Table", "", 0, "", 0, false);
            }
            else if (kok_tken is JArray)
            {
                JArray kok_dzi = (JArray)kok_tken;
                foreach (var oge_dgeri in kok_dzi)
                {
                    if (oge_dgeri is JObject)
                    {
                        JObject nesne_dgeri = (JObject)oge_dgeri;
                        ProcessObject(nesne_dgeri, "Main_Table", "", 0, "", 0, false);
                    }
                }
            }
            
            return veri_tbani;
        }

        public int ProcessObject(JObject nesne_dgeri, string tablo_ism, string ust_tblo, int ust_kmlik, string on_k, int derinlik_sviyesi, bool zayif_vrlik)
        {
            if (derinlik_sviyesi > maksimum_dngu)
            {
                return -1;
            }

            bool arama_tblosu = derinlik_sviyesi > maksimum_drinlik;
            string nesne_hsh = null;
            
            if (arama_tblosu)
            {
                nesne_hsh = ComputeHash(nesne_dgeri.ToString(Formatting.None));
            }

            if (arama_tblosu && veri_tbani.tekrarsizlik_onbllegi.ContainsKey(tablo_ism + "_" + nesne_hsh))
            {
                return veri_tbani.tekrarsizlik_onbllegi[tablo_ism + "_" + nesne_hsh];
            }

            DynamicTable yeni_satr = new DynamicTable();
            yeni_satr.tablo_ism = tablo_ism;
            yeni_satr.ic_kmlik = veri_tbani.GetNextIdForTable(tablo_ism);
            
            if (zayif_vrlik)
            {
                yeni_satr.zayif_vrlik = true;
                yeni_satr.ust_kmlik = ust_kmlik;
                yeni_satr.yerel_kmlik = veri_tbani.GetNextLocalId(tablo_ism, ust_kmlik);
                yeni_satr.ust_tblo_ismi = ust_tblo;
            }
            else if (ust_tblo != "" && !arama_tblosu)
            {
                ReferenceSchema referans_sma = new ReferenceSchema();
                referans_sma.sutun_di = ust_tblo + "_Id";
                referans_sma.hedef_tblo = ust_tblo;
                referans_sma.hedef_kmlik = ust_kmlik;
                yeni_satr.dis_anhtarlar.Add(referans_sma);
            }

            ExtractProperties(nesne_dgeri, on_k, yeni_satr, tablo_ism, derinlik_sviyesi);

            if (yeni_satr.sutun_lstesi.Count > 0 || yeni_satr.dis_anhtarlar.Count > 0 || yeni_satr.zayif_vrlik)
            {
                veri_tbani.tablo_stirlr.Add(yeni_satr);
                if (arama_tblosu)
                {
                    veri_tbani.tekrarsizlik_onbllegi[tablo_ism + "_" + nesne_hsh] = yeni_satr.ic_kmlik;
                }
            }

            return yeni_satr.ic_kmlik;
        }

        public void ExtractProperties(JObject nesne_dgeri, string on_k, DynamicTable hedef_stir, string tablo_ism, int derinlik_sviyesi)
        {
            // ISTER: Gelen veri dinamik olarak okunur (Hardcoded yapi yoktur)
            foreach (var ozellik_dgeri in nesne_dgeri.Properties())
            {
                if (ozellik_dgeri.Value is JObject)
                {
                    JObject alt_obj = (JObject)ozellik_dgeri.Value;
                    if (derinlik_sviyesi >= maksimum_drinlik)
                    {
                        string arama_tblo_ismi = on_k + ozellik_dgeri.Name;
                        int arama_kmligi = ProcessObject(alt_obj, arama_tblo_ismi, tablo_ism, hedef_stir.ic_kmlik, "", derinlik_sviyesi + 1, false);
                        if (arama_kmligi != -1)
                        {
                            ReferenceSchema referans_sma = new ReferenceSchema();
                            referans_sma.sutun_di = arama_tblo_ismi + "_Id";
                            referans_sma.hedef_tblo = arama_tblo_ismi;
                            referans_sma.hedef_kmlik = arama_kmligi;
                            hedef_stir.dis_anhtarlar.Add(referans_sma);
                        }
                    }
                    else
                    {
                        // ISTER: Ic ice gecmis objeler tek satirda duzlestirilir (Flattening)
                        string yeni_onk = "";
                        if (string.IsNullOrEmpty(on_k))
                        {
                            yeni_onk = ozellik_dgeri.Name + "_";
                        }
                        else
                        {
                            yeni_onk = on_k;
                        }
                        ExtractProperties(alt_obj, yeni_onk, hedef_stir, tablo_ism, derinlik_sviyesi + 1);
                    }
                }
                // ISTER: 1NF Kurali geregi diziler ayristirilir ve yeni alt tablolar (1:N iliskisi) olusturulur
                else if (ozellik_dgeri.Value is JArray)
                {
                    JArray alt_dzi = (JArray)ozellik_dgeri.Value;
                    string alt_tblo_ismi = tablo_ism + "_" + ozellik_dgeri.Name;

                    foreach (var oge_dgeri in alt_dzi)
                    {
                        if (oge_dgeri is JObject)
                        {
                            JObject dizi_nsnesi = (JObject)oge_dgeri;
                            ProcessObject(dizi_nsnesi, alt_tblo_ismi, tablo_ism, hedef_stir.ic_kmlik, "", derinlik_sviyesi + 1, true);
                        }
                        else if (oge_dgeri is JArray)
                        {
                            JArray ic_dzi = (JArray)oge_dgeri;
                            JObject sarmalayici_nsne = new JObject();
                            sarmalayici_nsne["NestedItem"] = ic_dzi;
                            ProcessObject(sarmalayici_nsne, alt_tblo_ismi + "_Nested", tablo_ism, hedef_stir.ic_kmlik, "", derinlik_sviyesi + 1, true);
                        }
                        else
                        {
                            ProcessPrimitiveArrayItem(oge_dgeri, alt_tblo_ismi, tablo_ism, hedef_stir.ic_kmlik);
                        }
                    }
                }
                else
                {
                    ColumnSchema sutun_nsnesi = new ColumnSchema();

                    string ayristirilmis_sm = on_k + ozellik_dgeri.Name;
                    // ISTER: SQL anahtarlari ile JSON anahtarlari cakismamasi icin dinamik kontrol saglanir
                    if (ayristirilmis_sm.Equals("id", StringComparison.OrdinalIgnoreCase))
                    {
                        sutun_nsnesi.sutun_di = "json_id";
                    }
                    else
                    {
                        sutun_nsnesi.sutun_di = ayristirilmis_sm;
                    }

                    sutun_nsnesi.sql_tru = DetermineSqlDataType(ozellik_dgeri.Value);
                    sutun_nsnesi.ham_dger = ExtractValue(ozellik_dgeri.Value);
                    hedef_stir.sutun_lstesi.Add(sutun_nsnesi);
                }
            }
        }

        public void ProcessPrimitiveArrayItem(JToken oge_dgeri, string tablo_ism, string ust_tblo, int ust_kmlik)
        {
            DynamicTable satir_nsnesi = new DynamicTable();
            satir_nsnesi.tablo_ism = tablo_ism;
            satir_nsnesi.ic_kmlik = veri_tbani.GetNextIdForTable(tablo_ism);
            satir_nsnesi.zayif_vrlik = true;
            satir_nsnesi.ust_kmlik = ust_kmlik;
            satir_nsnesi.yerel_kmlik = veri_tbani.GetNextLocalId(tablo_ism, ust_kmlik);
            satir_nsnesi.ust_tblo_ismi = ust_tblo;

            ColumnSchema sutun_nsnesi = new ColumnSchema();
            sutun_nsnesi.sutun_di = "Value";
            sutun_nsnesi.sql_tru = DetermineSqlDataType(oge_dgeri);
            sutun_nsnesi.ham_dger = ExtractValue(oge_dgeri);
            
            satir_nsnesi.sutun_lstesi.Add(sutun_nsnesi);
            veri_tbani.tablo_stirlr.Add(satir_nsnesi);
        }

        public string DetermineSqlDataType(JToken isaretci_dgeri)
        {
            if (isaretci_dgeri.Type == JTokenType.Integer) return "INTEGER";
            if (isaretci_dgeri.Type == JTokenType.Float) return "REAL";
            if (isaretci_dgeri.Type == JTokenType.Boolean) return "INTEGER";
            
            DateTime tarih_dgeri;
            if (isaretci_dgeri.Type == JTokenType.Date || DateTime.TryParse(isaretci_dgeri.ToString(), out tarih_dgeri)) 
            {
                return "DATETIME";
            }
            return "TEXT";
        }

        public object ExtractValue(JToken isaretci_dgeri)
        {
            if (isaretci_dgeri.Type == JTokenType.Null) return DBNull.Value;
            if (isaretci_dgeri.Type == JTokenType.Boolean)
            {
                bool deger_blgisi = (bool)isaretci_dgeri;
                if (deger_blgisi == true) return 1;
                else return 0;
            }
            return isaretci_dgeri.ToString();
        }

        public string ComputeHash(string ham_vri)
        {
            SHA256 sifreleyici_nsne = SHA256.Create();
            byte[] bayt_dzisi = Encoding.UTF8.GetBytes(ham_vri);
            byte[] hash_dzisi = sifreleyici_nsne.ComputeHash(bayt_dzisi);
            return Convert.ToBase64String(hash_dzisi);
        }
    }

    public class SqliteExecutor
    {
        public void ExecuteSchemaAndInsert(DatabaseSchema sema_nsnesi, string vt_ylu)
        {
            SqliteConnection.ClearAllPools();

            if (File.Exists(vt_ylu))
            {
                File.Delete(vt_ylu);
            }

            string baglanti_mtni = "Data Source=" + vt_ylu;

            using (SqliteConnection veritabani_bglantisi = new SqliteConnection(baglanti_mtni))
            {
                veritabani_bglantisi.Open();

                using (SqliteCommand pragma_kmutu = new SqliteCommand("PRAGMA foreign_keys = OFF;", veritabani_bglantisi))
                {
                    pragma_kmutu.ExecuteNonQuery();
                }

                using (SqliteTransaction veritabani_islmi = veritabani_bglantisi.BeginTransaction())
                {
                    try
                    {
                        var tablo_tnimlari = sema_nsnesi.tablo_stirlr.GroupBy(r => r.tablo_ism);
                        foreach (var tablo_grbu in tablo_tnimlari)
                        {
                            DynamicTable ornek_satr = tablo_grbu.First();
                            
                            var tum_stnlar = tablo_grbu.SelectMany(r => r.sutun_lstesi).GroupBy(c => c.sutun_di).Select(g => g.First()).ToList();
                            
                            var tum_anhtarlar = tablo_grbu.SelectMany(r => r.dis_anhtarlar).GroupBy(fk => fk.sutun_di).Select(g => g.First()).ToList();

                            // ISTER: Sema analizinden sonra CREATE TABLE sorgulari dinamik olarak string bazli uretilir
                            StringBuilder olusturma_srgusu = new StringBuilder();

                            if (ornek_satr.zayif_vrlik)
                            {
                                olusturma_srgusu.Append("CREATE TABLE IF NOT EXISTS [" + tablo_grbu.Key + "] (Parent_Id INTEGER, Local_Id INTEGER");
                                
                                foreach (var sutun_nsnesi in tum_stnlar)
                                {
                                    olusturma_srgusu.Append(", [" + sutun_nsnesi.sutun_di + "] " + sutun_nsnesi.sql_tru);
                                }
                                
                                foreach (var dis_anhtar in tum_anhtarlar)
                                {
                                    olusturma_srgusu.Append(", [" + dis_anhtar.sutun_di + "] INTEGER");
                                    olusturma_srgusu.Append(", FOREIGN KEY ([" + dis_anhtar.sutun_di + "]) REFERENCES [" + dis_anhtar.hedef_tblo + "](Id)");
                                }
                                
                                olusturma_srgusu.Append(", PRIMARY KEY (Parent_Id, Local_Id)");
                                olusturma_srgusu.Append(", FOREIGN KEY (Parent_Id) REFERENCES [" + ornek_satr.ust_tblo_ismi + "](Id)");
                            }
                            else
                            {
                                olusturma_srgusu.Append("CREATE TABLE IF NOT EXISTS [" + tablo_grbu.Key + "] (Id INTEGER PRIMARY KEY");
                                
                                foreach (var sutun_nsnesi in tum_stnlar)
                                {
                                    olusturma_srgusu.Append(", [" + sutun_nsnesi.sutun_di + "] " + sutun_nsnesi.sql_tru);
                                }
                                
                                foreach (var dis_anhtar in tum_anhtarlar)
                                {
                                    olusturma_srgusu.Append(", [" + dis_anhtar.sutun_di + "] INTEGER");
                                    olusturma_srgusu.Append(", FOREIGN KEY ([" + dis_anhtar.sutun_di + "]) REFERENCES [" + dis_anhtar.hedef_tblo + "](Id)");
                                }
                            }
                            
                            olusturma_srgusu.Append(");");

                            using (SqliteCommand sql_kmutu = new SqliteCommand(olusturma_srgusu.ToString(), veritabani_bglantisi, veritabani_islmi))
                            {
                                sql_kmutu.ExecuteNonQuery();
                            }
                        }

                        foreach (var satir_nsnesi in sema_nsnesi.tablo_stirlr)
                        {
                            List<string> sutun_adlr = new List<string>();
                            List<string> parametre_dlari = new List<string>();
                            
                            using (SqliteCommand sql_kmutu = new SqliteCommand("", veritabani_bglantisi, veritabani_islmi))
                            {
                                if (satir_nsnesi.zayif_vrlik)
                                {
                                    sutun_adlr.Add("Parent_Id");
                                    parametre_dlari.Add("@Pid");
                                    sql_kmutu.Parameters.AddWithValue("@Pid", satir_nsnesi.ust_kmlik);

                                    sutun_adlr.Add("Local_Id");
                                    parametre_dlari.Add("@Lid");
                                    sql_kmutu.Parameters.AddWithValue("@Lid", satir_nsnesi.yerel_kmlik);
                                }
                                else
                                {
                                    sutun_adlr.Add("Id");
                                    parametre_dlari.Add("@Id");
                                    sql_kmutu.Parameters.AddWithValue("@Id", satir_nsnesi.ic_kmlik);
                                }

                                int parametre_ndeksi = 0;
                                foreach (var sutun_nsnesi in satir_nsnesi.sutun_lstesi)
                                {
                                    string parametre_smi = "@p" + parametre_ndeksi;
                                    parametre_ndeksi = parametre_ndeksi + 1;
                                    
                                    sutun_adlr.Add("[" + sutun_nsnesi.sutun_di + "]");
                                    parametre_dlari.Add(parametre_smi);
                                    
                                    if (sutun_nsnesi.ham_dger == null)
                                    {
                                        sql_kmutu.Parameters.AddWithValue(parametre_smi, DBNull.Value);
                                    }
                                    else
                                    {
                                        sql_kmutu.Parameters.AddWithValue(parametre_smi, sutun_nsnesi.ham_dger);
                                    }
                                }

                                foreach (var dis_anhtar in satir_nsnesi.dis_anhtarlar)
                                {
                                    string parametre_smi = "@p" + parametre_ndeksi;
                                    parametre_ndeksi = parametre_ndeksi + 1;
                                    
                                    sutun_adlr.Add("[" + dis_anhtar.sutun_di + "]");
                                    parametre_dlari.Add(parametre_smi);
                                    sql_kmutu.Parameters.AddWithValue(parametre_smi, dis_anhtar.hedef_kmlik);
                                }

                                int min_stnlar = 1;
                                if (satir_nsnesi.zayif_vrlik) min_stnlar = 2;
                                if (sutun_adlr.Count <= min_stnlar) continue;

                                sql_kmutu.CommandText = "INSERT INTO [" + satir_nsnesi.tablo_ism + "] (" + string.Join(", ", sutun_adlr) + ") VALUES (" + string.Join(", ", parametre_dlari) + ");";
                                sql_kmutu.ExecuteNonQuery();
                            }
                        }

                        veritabani_islmi.Commit(); 
                    }
                    catch (Exception)
                    {
                        veritabani_islmi.Rollback();
                        throw;
                    }
                }
            }

            SqliteConnection.ClearAllPools();
        }
    }
}
