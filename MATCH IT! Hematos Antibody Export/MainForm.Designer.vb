Imports DevExpress.XtraEditors
Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.UserSkins


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If presenter IsNot Nothing Then
                    presenter.Dispose()
                    presenter = Nothing
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.ExportBt = New System.Windows.Forms.Button()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.BrowseBt = New DevExpress.XtraEditors.SimpleButton()
        Me.DropFolderPathtxt = New DevExpress.XtraEditors.TextEdit()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.LotIDComboBx = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.SiteCodeComboBx = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.uxrdoFilter = New DevExpress.XtraEditors.RadioGroup()
        Me.uxlblSelSample = New DevExpress.XtraEditors.LabelControl()
        Me.uxlblAvailSamples = New DevExpress.XtraEditors.LabelControl()
        Me.uxlblBatches = New DevExpress.XtraEditors.LabelControl()
        Me.uxlbSelectedSamples = New DevExpress.XtraEditors.ListBoxControl()
        Me.SampleVMBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.uxlbSamples = New DevExpress.XtraEditors.ListBoxControl()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.AbClassCode = New DevExpress.XtraEditors.RadioGroup()
        Me.label6 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.uxbtnMoveAll = New DevExpress.XtraEditors.SimpleButton()
        Me.uxbtnMoveOne = New DevExpress.XtraEditors.SimpleButton()
        Me.uxbtnRemoveOne = New DevExpress.XtraEditors.SimpleButton()
        Me.uxbtnRemoveAll = New DevExpress.XtraEditors.SimpleButton()
        Me.uxlbBatches = New DevExpress.XtraEditors.ListBoxControl()
        Me.textBoxLumID = New System.Windows.Forms.TextBox()
        Me.ApplicationMenu1 = New DevExpress.XtraBars.Ribbon.ApplicationMenu(Me.components)
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.panel1.SuspendLayout()
        CType(Me.DropFolderPathtxt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LotIDComboBx.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SiteCodeComboBx.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxrdoFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxlbSelectedSamples, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SampleVMBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxlbSamples, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox1.SuspendLayout()
        CType(Me.AbClassCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.uxlbBatches, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ApplicationMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExportBt
        '
        Me.ExportBt.Location = New System.Drawing.Point(807, 28)
        Me.ExportBt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ExportBt.Name = "ExportBt"
        Me.ExportBt.Size = New System.Drawing.Size(111, 41)
        Me.ExportBt.TabIndex = 12
        Me.ExportBt.Text = "Export"
        Me.ExportBt.UseVisualStyleBackColor = True
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.BrowseBt)
        Me.panel1.Controls.Add(Me.DropFolderPathtxt)
        Me.panel1.Controls.Add(Me.label3)
        Me.panel1.Controls.Add(Me.label2)
        Me.panel1.Controls.Add(Me.label1)
        Me.panel1.Controls.Add(Me.LotIDComboBx)
        Me.panel1.Controls.Add(Me.SiteCodeComboBx)
        Me.panel1.Controls.Add(Me.ExportBt)
        Me.panel1.Location = New System.Drawing.Point(12, 40)
        Me.panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(1002, 107)
        Me.panel1.TabIndex = 11
        '
        'BrowseBt
        '
        Me.BrowseBt.Location = New System.Drawing.Point(678, 21)
        Me.BrowseBt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BrowseBt.Name = "BrowseBt"
        Me.BrowseBt.Size = New System.Drawing.Size(33, 25)
        Me.BrowseBt.TabIndex = 11
        Me.BrowseBt.Text = "..."
        '
        'DropFolderPathtxt
        '
        Me.DropFolderPathtxt.Location = New System.Drawing.Point(348, 21)
        Me.DropFolderPathtxt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DropFolderPathtxt.Name = "DropFolderPathtxt"
        Me.DropFolderPathtxt.Size = New System.Drawing.Size(323, 22)
        Me.DropFolderPathtxt.TabIndex = 10
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(262, 26)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(85, 17)
        Me.label3.TabIndex = 12
        Me.label3.Text = "Drop Folder:"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(7, 76)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(51, 17)
        Me.label2.TabIndex = 11
        Me.label2.Text = "Lot ID:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(7, 26)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(71, 17)
        Me.label1.TabIndex = 10
        Me.label1.Text = "Site Code:"
        '
        'LotIDComboBx
        '
        Me.LotIDComboBx.Location = New System.Drawing.Point(79, 71)
        Me.LotIDComboBx.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LotIDComboBx.Name = "LotIDComboBx"
        Me.LotIDComboBx.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LotIDComboBx.Size = New System.Drawing.Size(167, 22)
        Me.LotIDComboBx.TabIndex = 0
        '
        'SiteCodeComboBx
        '
        Me.SiteCodeComboBx.Location = New System.Drawing.Point(79, 21)
        Me.SiteCodeComboBx.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SiteCodeComboBx.Name = "SiteCodeComboBx"
        Me.SiteCodeComboBx.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SiteCodeComboBx.Size = New System.Drawing.Size(117, 22)
        Me.SiteCodeComboBx.TabIndex = 7
        '
        'uxrdoFilter
        '
        Me.uxrdoFilter.EditValue = CType(1, Short)
        Me.uxrdoFilter.Location = New System.Drawing.Point(13, 27)
        Me.uxrdoFilter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxrdoFilter.Name = "uxrdoFilter"
        Me.uxrdoFilter.Properties.AllowMouseWheel = False
        Me.uxrdoFilter.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Unprocessed Samples"), New DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Processed Samples")})
        Me.uxrdoFilter.Size = New System.Drawing.Size(162, 64)
        Me.uxrdoFilter.TabIndex = 2
        '
        'uxlblSelSample
        '
        Me.uxlblSelSample.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.uxlblSelSample.Appearance.Options.UseFont = True
        Me.uxlblSelSample.Location = New System.Drawing.Point(582, 166)
        Me.uxlblSelSample.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxlblSelSample.Name = "uxlblSelSample"
        Me.uxlblSelSample.Size = New System.Drawing.Size(119, 17)
        Me.uxlblSelSample.TabIndex = 52
        Me.uxlblSelSample.Text = "Selected Samples"
        '
        'uxlblAvailSamples
        '
        Me.uxlblAvailSamples.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.uxlblAvailSamples.Appearance.Options.UseFont = True
        Me.uxlblAvailSamples.Location = New System.Drawing.Point(275, 166)
        Me.uxlblAvailSamples.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxlblAvailSamples.Name = "uxlblAvailSamples"
        Me.uxlblAvailSamples.Size = New System.Drawing.Size(121, 17)
        Me.uxlblAvailSamples.TabIndex = 51
        Me.uxlblAvailSamples.Text = "Available Samples"
        '
        'uxlblBatches
        '
        Me.uxlblBatches.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.uxlblBatches.Appearance.Options.UseFont = True
        Me.uxlblBatches.Location = New System.Drawing.Point(34, 166)
        Me.uxlblBatches.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxlblBatches.Name = "uxlblBatches"
        Me.uxlblBatches.Size = New System.Drawing.Size(55, 17)
        Me.uxlblBatches.TabIndex = 50
        Me.uxlblBatches.Text = "Batches"
        '
        'uxlbSelectedSamples
        '
        Me.uxlbSelectedSamples.DataSource = Me.SampleVMBindingSource
        Me.uxlbSelectedSamples.DisplayMember = "sampleID"
        Me.uxlbSelectedSamples.Location = New System.Drawing.Point(582, 190)
        Me.uxlbSelectedSamples.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxlbSelectedSamples.Name = "uxlbSelectedSamples"
        Me.uxlbSelectedSamples.Size = New System.Drawing.Size(184, 331)
        Me.uxlbSelectedSamples.TabIndex = 6
        '
        'SampleVMBindingSource
        '
        Me.SampleVMBindingSource.DataSource = GetType(HematosViewModel.SampleVM)
        '
        'uxlbSamples
        '
        Me.uxlbSamples.DataSource = Me.SampleVMBindingSource
        Me.uxlbSamples.DisplayMember = "sampleID"
        Me.uxlbSamples.Location = New System.Drawing.Point(275, 190)
        Me.uxlbSamples.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxlbSamples.Name = "uxlbSamples"
        Me.uxlbSamples.Size = New System.Drawing.Size(196, 331)
        Me.uxlbSamples.TabIndex = 3
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.AbClassCode)
        Me.groupBox1.Location = New System.Drawing.Point(785, 285)
        Me.groupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.groupBox1.Size = New System.Drawing.Size(187, 196)
        Me.groupBox1.TabIndex = 41
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Antibody Class Code"
        '
        'AbClassCode
        '
        Me.AbClassCode.EditValue = "G"
        Me.AbClassCode.Location = New System.Drawing.Point(17, 25)
        Me.AbClassCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.AbClassCode.Name = "AbClassCode"
        Me.AbClassCode.Properties.AllowMouseWheel = False
        Me.AbClassCode.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem("A", "IgA"), New DevExpress.XtraEditors.Controls.RadioGroupItem("AGM", "IgA, IgG && IgM"), New DevExpress.XtraEditors.Controls.RadioGroupItem("G", "IgG"), New DevExpress.XtraEditors.Controls.RadioGroupItem("GM", "IgG && IgM"), New DevExpress.XtraEditors.Controls.RadioGroupItem("M", "IgM")})
        Me.AbClassCode.Size = New System.Drawing.Size(157, 154)
        Me.AbClassCode.TabIndex = 9
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(503, 544)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(83, 17)
        Me.label6.TabIndex = 39
        Me.label6.Text = "Luminex ID:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.uxrdoFilter)
        Me.GroupBox2.Location = New System.Drawing.Point(785, 169)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(187, 105)
        Me.GroupBox2.TabIndex = 54
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Display Options"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.uxbtnMoveAll)
        Me.Panel2.Controls.Add(Me.uxbtnMoveOne)
        Me.Panel2.Controls.Add(Me.uxbtnRemoveOne)
        Me.Panel2.Controls.Add(Me.uxbtnRemoveAll)
        Me.Panel2.Location = New System.Drawing.Point(484, 276)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(84, 158)
        Me.Panel2.TabIndex = 55
        '
        'uxbtnMoveAll
        '
        Me.uxbtnMoveAll.Location = New System.Drawing.Point(9, 11)
        Me.uxbtnMoveAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxbtnMoveAll.Name = "uxbtnMoveAll"
        Me.uxbtnMoveAll.Size = New System.Drawing.Size(65, 28)
        Me.uxbtnMoveAll.TabIndex = 4
        Me.uxbtnMoveAll.Text = ">>"
        '
        'uxbtnMoveOne
        '
        Me.uxbtnMoveOne.Location = New System.Drawing.Point(9, 47)
        Me.uxbtnMoveOne.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxbtnMoveOne.Name = "uxbtnMoveOne"
        Me.uxbtnMoveOne.Size = New System.Drawing.Size(65, 28)
        Me.uxbtnMoveOne.TabIndex = 5
        Me.uxbtnMoveOne.Text = ">"
        '
        'uxbtnRemoveOne
        '
        Me.uxbtnRemoveOne.Location = New System.Drawing.Point(9, 82)
        Me.uxbtnRemoveOne.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxbtnRemoveOne.Name = "uxbtnRemoveOne"
        Me.uxbtnRemoveOne.Size = New System.Drawing.Size(65, 28)
        Me.uxbtnRemoveOne.TabIndex = 7
        Me.uxbtnRemoveOne.Text = "<"
        '
        'uxbtnRemoveAll
        '
        Me.uxbtnRemoveAll.Location = New System.Drawing.Point(9, 118)
        Me.uxbtnRemoveAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxbtnRemoveAll.Name = "uxbtnRemoveAll"
        Me.uxbtnRemoveAll.Size = New System.Drawing.Size(65, 28)
        Me.uxbtnRemoveAll.TabIndex = 8
        Me.uxbtnRemoveAll.Text = "<<"
        '
        'uxlbBatches
        '
        Me.uxlbBatches.Location = New System.Drawing.Point(34, 190)
        Me.uxlbBatches.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.uxlbBatches.Name = "uxlbBatches"
        Me.uxlbBatches.Size = New System.Drawing.Size(215, 331)
        Me.uxlbBatches.TabIndex = 1
        '
        'textBoxLumID
        '
        Me.textBoxLumID.Location = New System.Drawing.Point(582, 541)
        Me.textBoxLumID.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.textBoxLumID.Name = "textBoxLumID"
        Me.textBoxLumID.ReadOnly = True
        Me.textBoxLumID.Size = New System.Drawing.Size(184, 23)
        Me.textBoxLumID.TabIndex = 40
        Me.textBoxLumID.TabStop = False
        '
        'ApplicationMenu1
        '
        Me.ApplicationMenu1.Name = "ApplicationMenu1"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1})
        Me.BarManager1.MaxItemId = 1
        '
        'Bar1
        '
        Me.Bar1.BarName = "Custom 2"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem1)})
        Me.Bar1.Text = "Custom 2"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Show Rules Grid"
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1030, 25)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 619)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1030, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 25)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 594)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1030, 25)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 594)
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1030, 619)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.uxlblSelSample)
        Me.Controls.Add(Me.uxlblAvailSamples)
        Me.Controls.Add(Me.uxlblBatches)
        Me.Controls.Add(Me.uxlbSelectedSamples)
        Me.Controls.Add(Me.uxlbSamples)
        Me.Controls.Add(Me.uxlbBatches)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.textBoxLumID)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.Icon = CType(resources.GetObject("MainForm.IconOptions.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MATCH IT! Antibody Hematos Export v1.1.6"
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        CType(Me.DropFolderPathtxt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LotIDComboBx.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SiteCodeComboBx.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxrdoFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxlbSelectedSamples, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SampleVMBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxlbSamples, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox1.ResumeLayout(False)
        CType(Me.AbClassCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.uxlbBatches, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ApplicationMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents ExportBt As System.Windows.Forms.Button
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents LotIDComboBx As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents SiteCodeComboBx As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents uxrdoFilter As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents uxlblSelSample As DevExpress.XtraEditors.LabelControl
    Friend WithEvents uxlblAvailSamples As DevExpress.XtraEditors.LabelControl
    Friend WithEvents uxlblBatches As DevExpress.XtraEditors.LabelControl
    Friend WithEvents uxlbSelectedSamples As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents uxlbSamples As DevExpress.XtraEditors.ListBoxControl
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents BrowseBt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents DropFolderPathtxt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents AbClassCode As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents uxbtnMoveAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents uxbtnMoveOne As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents uxbtnRemoveOne As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents uxbtnRemoveAll As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents uxlbBatches As DevExpress.XtraEditors.ListBoxControl
    Private WithEvents textBoxLumID As System.Windows.Forms.TextBox
    Friend WithEvents SampleVMBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ApplicationMenu1 As DevExpress.XtraBars.Ribbon.ApplicationMenu
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
End Class
