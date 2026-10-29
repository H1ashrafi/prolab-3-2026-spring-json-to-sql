using System.Drawing;
using System.Windows.Forms;

namespace DynamicJsonToSql
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.pnlLeftHeader = new System.Windows.Forms.Panel();
            this.lblLeftTitle = new System.Windows.Forms.Label();
            this.tvJsonStructure = new System.Windows.Forms.TreeView();
            this.pnlRightControls = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnSelectJson = new System.Windows.Forms.Button();
            this.pnlTableSelector = new System.Windows.Forms.Panel();
            this.cmbTables = new System.Windows.Forms.ComboBox();
            this.lblSelectTable = new System.Windows.Forms.Label();
            this.dgvSqlData = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.pnlLeftHeader.SuspendLayout();
            this.pnlRightControls.SuspendLayout();
            this.pnlTableSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSqlData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1100, 75);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubTitle.Location = new System.Drawing.Point(19, 44);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(359, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Kocaeli Üniversitesi ProLab II - NoSQL verilerin ilişkisel SQL yapısına aktarılması";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(370, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "NoSQL\'den SQL\'e Dönüşüm Sistemi";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlFooter.Controls.Add(this.lblStatus);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 670);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1100, 30);
            this.pnlFooter.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblStatus.Location = new System.Drawing.Point(12, 7);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 15);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Hazır.";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 75);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.splitContainerMain.Panel1.Controls.Add(this.pnlLeftHeader);
            this.splitContainerMain.Panel1.Controls.Add(this.tvJsonStructure);
            this.splitContainerMain.Panel1.Padding = new System.Windows.Forms.Padding(12);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.splitContainerMain.Panel2.Controls.Add(this.dgvSqlData);
            this.splitContainerMain.Panel2.Controls.Add(this.pnlTableSelector);
            this.splitContainerMain.Panel2.Controls.Add(this.pnlRightControls);
            this.splitContainerMain.Panel2.Padding = new System.Windows.Forms.Padding(12);
            this.splitContainerMain.Size = new System.Drawing.Size(1100, 595);
            this.splitContainerMain.SplitterDistance = 400;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 2;
            // 
            // pnlLeftHeader
            // 
            this.pnlLeftHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlLeftHeader.Controls.Add(this.lblLeftTitle);
            this.pnlLeftHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLeftHeader.Location = new System.Drawing.Point(12, 12);
            this.pnlLeftHeader.Name = "pnlLeftHeader";
            this.pnlLeftHeader.Size = new System.Drawing.Size(376, 36);
            this.pnlLeftHeader.TabIndex = 1;
            // 
            // lblLeftTitle
            // 
            this.lblLeftTitle.AutoSize = true;
            this.lblLeftTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeftTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblLeftTitle.Location = new System.Drawing.Point(10, 9);
            this.lblLeftTitle.Name = "lblLeftTitle";
            this.lblLeftTitle.Size = new System.Drawing.Size(141, 17);
            this.lblLeftTitle.TabIndex = 0;
            this.lblLeftTitle.Text = "JSON Hiyerarşik Yapısı";
            // 
            // tvJsonStructure
            // 
            this.tvJsonStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvJsonStructure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.tvJsonStructure.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvJsonStructure.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvJsonStructure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.tvJsonStructure.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.tvJsonStructure.Location = new System.Drawing.Point(12, 54);
            this.tvJsonStructure.Name = "tvJsonStructure";
            this.tvJsonStructure.Size = new System.Drawing.Size(376, 529);
            this.tvJsonStructure.TabIndex = 0;
            // 
            // pnlRightControls
            // 
            this.pnlRightControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlRightControls.Controls.Add(this.btnReset);
            this.pnlRightControls.Controls.Add(this.btnConvert);
            this.pnlRightControls.Controls.Add(this.btnSelectJson);
            this.pnlRightControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRightControls.Location = new System.Drawing.Point(12, 12);
            this.pnlRightControls.Name = "pnlRightControls";
            this.pnlRightControls.Size = new System.Drawing.Size(671, 60);
            this.pnlRightControls.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(521, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(138, 36);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Temizle / Sıfırla";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(172)))), ((int)(((byte)(132)))));
            this.btnConvert.Enabled = false;
            this.btnConvert.FlatAppearance.BorderSize = 0;
            this.btnConvert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(128)))), ((int)(((byte)(98)))));
            this.btnConvert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(209)))), ((int)(((byte)(161)))));
            this.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvert.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvert.ForeColor = System.Drawing.Color.White;
            this.btnConvert.Location = new System.Drawing.Point(167, 12);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(160, 36);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = "Dönüştür / Çalıştır";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnSelectJson
            // 
            this.btnSelectJson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(134)))), ((int)(((byte)(222)))));
            this.btnSelectJson.FlatAppearance.BorderSize = 0;
            this.btnSelectJson.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(97)))), ((int)(((byte)(168)))));
            this.btnSelectJson.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnSelectJson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectJson.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectJson.ForeColor = System.Drawing.Color.White;
            this.btnSelectJson.Location = new System.Drawing.Point(12, 12);
            this.btnSelectJson.Name = "btnSelectJson";
            this.btnSelectJson.Size = new System.Drawing.Size(140, 36);
            this.btnSelectJson.TabIndex = 0;
            this.btnSelectJson.Text = "JSON Dosyası Seç";
            this.btnSelectJson.UseVisualStyleBackColor = false;
            this.btnSelectJson.Click += new System.EventHandler(this.btnSelectJson_Click);
            // 
            // pnlTableSelector
            // 
            this.pnlTableSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTableSelector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlTableSelector.Controls.Add(this.cmbTables);
            this.pnlTableSelector.Controls.Add(this.lblSelectTable);
            this.pnlTableSelector.Location = new System.Drawing.Point(12, 85);
            this.pnlTableSelector.Name = "pnlTableSelector";
            this.pnlTableSelector.Size = new System.Drawing.Size(671, 46);
            this.pnlTableSelector.TabIndex = 1;
            // 
            // cmbTables
            // 
            this.cmbTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTables.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTables.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Location = new System.Drawing.Point(145, 10);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(514, 25);
            this.cmbTables.TabIndex = 1;
            this.cmbTables.SelectedIndexChanged += new System.EventHandler(this.cmbTables_SelectedIndexChanged);
            // 
            // lblSelectTable
            // 
            this.lblSelectTable.AutoSize = true;
            this.lblSelectTable.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblSelectTable.Location = new System.Drawing.Point(12, 13);
            this.lblSelectTable.Name = "lblSelectTable";
            this.lblSelectTable.Size = new System.Drawing.Size(127, 17);
            this.lblSelectTable.TabIndex = 0;
            this.lblSelectTable.Text = "Gösterilecek Tablo:";
            // 
            // dgvSqlData
            // 
            this.dgvSqlData.AllowUserToAddRows = false;
            this.dgvSqlData.AllowUserToDeleteRows = false;
            this.dgvSqlData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSqlData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvSqlData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSqlData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSqlData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSqlData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSqlData.ColumnHeadersHeight = 35;
            this.dgvSqlData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSqlData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSqlData.EnableHeadersVisualStyles = false;
            this.dgvSqlData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.dgvSqlData.Location = new System.Drawing.Point(12, 143);
            this.dgvSqlData.Name = "dgvSqlData";
            this.dgvSqlData.ReadOnly = true;
            this.dgvSqlData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSqlData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSqlData.RowHeadersWidth = 25;
            this.dgvSqlData.RowTemplate.Height = 28;
            this.dgvSqlData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSqlData.Size = new System.Drawing.Size(671, 440);
            this.dgvSqlData.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NoSQL'den SQL'e Dönüşüm Sistemi";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.pnlLeftHeader.ResumeLayout(false);
            this.pnlLeftHeader.PerformLayout();
            this.pnlRightControls.ResumeLayout(false);
            this.pnlTableSelector.ResumeLayout(false);
            this.pnlTableSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSqlData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TreeView tvJsonStructure;
        private System.Windows.Forms.Panel pnlRightControls;
        private System.Windows.Forms.Button btnSelectJson;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnlTableSelector;
        private System.Windows.Forms.ComboBox cmbTables;
        private System.Windows.Forms.Label lblSelectTable;
        private System.Windows.Forms.DataGridView dgvSqlData;
        private System.Windows.Forms.Panel pnlLeftHeader;
        private System.Windows.Forms.Label lblLeftTitle;
    }
}
