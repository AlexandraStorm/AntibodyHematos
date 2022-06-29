namespace WinFormPrototype
{
    partial class Form1
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcLoci = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSerology = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcallele = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gcMedianall = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gcMany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gcMeanMany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox4 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gcOne = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox6 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gcMeanOne = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox5 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2,
            this.repositoryItemComboBox3,
            this.repositoryItemComboBox4,
            this.repositoryItemComboBox5,
            this.repositoryItemComboBox6});
            this.gridControl1.Size = new System.Drawing.Size(1471, 682);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Load += new System.EventHandler(this.gridControl1_Load);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcLoci,
            this.gcSerology,
            this.gcallele,
            this.gcAll,
            this.gcMedianall,
            this.gcMany,
            this.gcMeanMany,
            this.gcOne,
            this.gcMeanOne});
            this.gridView1.DetailHeight = 431;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // gcLoci
            // 
            this.gcLoci.Caption = "Loci";
            this.gcLoci.FieldName = "locus";
            this.gcLoci.MinWidth = 27;
            this.gcLoci.Name = "gcLoci";
            this.gcLoci.OptionsColumn.AllowEdit = false;
            this.gcLoci.Visible = true;
            this.gcLoci.VisibleIndex = 0;
            this.gcLoci.Width = 100;
            // 
            // gcSerology
            // 
            this.gcSerology.Caption = "Serology";
            this.gcSerology.FieldName = "Serology";
            this.gcSerology.MinWidth = 27;
            this.gcSerology.Name = "gcSerology";
            this.gcSerology.Visible = true;
            this.gcSerology.VisibleIndex = 1;
            this.gcSerology.Width = 100;
            // 
            // gcallele
            // 
            this.gcallele.Caption = "Allele Name";
            this.gcallele.FieldName = "Allelic";
            this.gcallele.MinWidth = 27;
            this.gcallele.Name = "gcallele";
            this.gcallele.OptionsColumn.AllowEdit = false;
            this.gcallele.Visible = true;
            this.gcallele.VisibleIndex = 2;
            this.gcallele.Width = 100;
            // 
            // gcAll
            // 
            this.gcAll.Caption = "For All Positive Use";
            this.gcAll.ColumnEdit = this.repositoryItemComboBox1;
            this.gcAll.FieldName = "selectedAllelicAll";
            this.gcAll.MinWidth = 27;
            this.gcAll.Name = "gcAll";
            this.gcAll.Visible = true;
            this.gcAll.VisibleIndex = 3;
            this.gcAll.Width = 100;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "Serology",
            "Allelic"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // gcMedianall
            // 
            this.gcMedianall.Caption = "Mean Value to Use";
            this.gcMedianall.ColumnEdit = this.repositoryItemComboBox2;
            this.gcMedianall.FieldName = "useMedianRawValuesAll";
            this.gcMedianall.MinWidth = 27;
            this.gcMedianall.Name = "gcMedianall";
            this.gcMedianall.Visible = true;
            this.gcMedianall.VisibleIndex = 4;
            this.gcMedianall.Width = 100;
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Items.AddRange(new object[] {
            "Median Raw Value",
            "Median BCM (MFI/LRA)",
            "Median BCR"});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // gcMany
            // 
            this.gcMany.Caption = "More than 1 Positive";
            this.gcMany.ColumnEdit = this.repositoryItemComboBox3;
            this.gcMany.FieldName = "selectedAllelicMany";
            this.gcMany.MinWidth = 27;
            this.gcMany.Name = "gcMany";
            this.gcMany.Visible = true;
            this.gcMany.VisibleIndex = 5;
            this.gcMany.Width = 100;
            // 
            // repositoryItemComboBox3
            // 
            this.repositoryItemComboBox3.AutoHeight = false;
            this.repositoryItemComboBox3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox3.Items.AddRange(new object[] {
            "Serology",
            "Allelic"});
            this.repositoryItemComboBox3.Name = "repositoryItemComboBox3";
            // 
            // gcMeanMany
            // 
            this.gcMeanMany.Caption = "Median Value to Use";
            this.gcMeanMany.ColumnEdit = this.repositoryItemComboBox4;
            this.gcMeanMany.FieldName = "useMedainRawValuesMany";
            this.gcMeanMany.MinWidth = 27;
            this.gcMeanMany.Name = "gcMeanMany";
            this.gcMeanMany.Visible = true;
            this.gcMeanMany.VisibleIndex = 6;
            this.gcMeanMany.Width = 100;
            // 
            // repositoryItemComboBox4
            // 
            this.repositoryItemComboBox4.AutoHeight = false;
            this.repositoryItemComboBox4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox4.Items.AddRange(new object[] {
            "Median Raw Value",
            "Median BCM (MFI/LRA)",
            "Median BCR"});
            this.repositoryItemComboBox4.Name = "repositoryItemComboBox4";
            // 
            // gcOne
            // 
            this.gcOne.Caption = "Only One Positive";
            this.gcOne.ColumnEdit = this.repositoryItemComboBox6;
            this.gcOne.FieldName = "selectAllelicOne";
            this.gcOne.MinWidth = 27;
            this.gcOne.Name = "gcOne";
            this.gcOne.Visible = true;
            this.gcOne.VisibleIndex = 7;
            this.gcOne.Width = 100;
            // 
            // repositoryItemComboBox6
            // 
            this.repositoryItemComboBox6.AutoHeight = false;
            this.repositoryItemComboBox6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox6.Items.AddRange(new object[] {
            "Serology",
            "Allelic"});
            this.repositoryItemComboBox6.Name = "repositoryItemComboBox6";
            // 
            // gcMeanOne
            // 
            this.gcMeanOne.Caption = "Value to Use";
            this.gcMeanOne.ColumnEdit = this.repositoryItemComboBox5;
            this.gcMeanOne.FieldName = "useMedianRawValuesOne";
            this.gcMeanOne.MinWidth = 27;
            this.gcMeanOne.Name = "gcMeanOne";
            this.gcMeanOne.Visible = true;
            this.gcMeanOne.VisibleIndex = 8;
            this.gcMeanOne.Width = 100;
            // 
            // repositoryItemComboBox5
            // 
            this.repositoryItemComboBox5.AutoHeight = false;
            this.repositoryItemComboBox5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox5.Items.AddRange(new object[] {
            "Median Raw Value",
            "Median BCM (MFI/LRA)",
            "Median BCR"});
            this.repositoryItemComboBox5.Name = "repositoryItemComboBox5";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 682);
            this.Controls.Add(this.gridControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gcLoci;
        private DevExpress.XtraGrid.Columns.GridColumn gcSerology;
        private DevExpress.XtraGrid.Columns.GridColumn gcallele;
        private DevExpress.XtraGrid.Columns.GridColumn gcAll;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gcMedianall;
        private DevExpress.XtraGrid.Columns.GridColumn gcMany;
        private DevExpress.XtraGrid.Columns.GridColumn gcMeanMany;
        private DevExpress.XtraGrid.Columns.GridColumn gcOne;
        private DevExpress.XtraGrid.Columns.GridColumn gcMeanOne;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox3;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox4;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox6;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox5;
    }
}

