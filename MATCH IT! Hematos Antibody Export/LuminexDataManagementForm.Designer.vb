<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LuminexDataManagementForm
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LuminexDataManagementForm))
        Me.uxGridLuminexData = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.uxBtnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.uxBtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.uxTxtLuminexServer = New DevExpress.XtraEditors.TextEdit()
        Me.uxTxtLuminexID = New DevExpress.XtraEditors.TextEdit()
        Me.uxTxtSerialNum = New DevExpress.XtraEditors.TextEdit()
        Me.uxLblLuminexServer = New DevExpress.XtraEditors.LabelControl()
        Me.uxLblLuminexID = New DevExpress.XtraEditors.LabelControl()
        Me.uxLblSerialNum = New DevExpress.XtraEditors.LabelControl()
        Me.uxGroupLDM1 = New DevExpress.XtraEditors.GroupControl()
        Me.uxBtnSelect = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.uxGridLuminexData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxTxtLuminexServer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxTxtLuminexID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxTxtSerialNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxGroupLDM1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.uxGroupLDM1.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxGridLuminexData
        '
        Me.uxGridLuminexData.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.uxGridLuminexData.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(6)
        Me.uxGridLuminexData.Location = New System.Drawing.Point(0, 233)
        Me.uxGridLuminexData.MainView = Me.GridView1
        Me.uxGridLuminexData.Margin = New System.Windows.Forms.Padding(6)
        Me.uxGridLuminexData.Name = "uxGridLuminexData"
        Me.uxGridLuminexData.Size = New System.Drawing.Size(765, 385)
        Me.uxGridLuminexData.TabIndex = 17
        Me.uxGridLuminexData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.uxGridLuminexData
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'uxBtnClear
        '
        Me.uxBtnClear.Location = New System.Drawing.Point(121, 54)
        Me.uxBtnClear.Margin = New System.Windows.Forms.Padding(6)
        Me.uxBtnClear.Name = "uxBtnClear"
        Me.uxBtnClear.Size = New System.Drawing.Size(150, 44)
        Me.uxBtnClear.TabIndex = 0
        Me.uxBtnClear.Text = "uxBtnClear"
        '
        'uxBtnSave
        '
        Me.uxBtnSave.Location = New System.Drawing.Point(297, 54)
        Me.uxBtnSave.Margin = New System.Windows.Forms.Padding(6)
        Me.uxBtnSave.Name = "uxBtnSave"
        Me.uxBtnSave.Size = New System.Drawing.Size(150, 44)
        Me.uxBtnSave.TabIndex = 1
        Me.uxBtnSave.Text = "uxBtnSave"
        '
        'uxTxtLuminexServer
        '
        Me.uxTxtLuminexServer.Location = New System.Drawing.Point(514, 173)
        Me.uxTxtLuminexServer.Margin = New System.Windows.Forms.Padding(6)
        Me.uxTxtLuminexServer.Name = "uxTxtLuminexServer"
        Me.uxTxtLuminexServer.Size = New System.Drawing.Size(200, 32)
        Me.uxTxtLuminexServer.TabIndex = 16
        '
        'uxTxtLuminexID
        '
        Me.uxTxtLuminexID.Location = New System.Drawing.Point(278, 173)
        Me.uxTxtLuminexID.Margin = New System.Windows.Forms.Padding(6)
        Me.uxTxtLuminexID.Name = "uxTxtLuminexID"
        Me.uxTxtLuminexID.Size = New System.Drawing.Size(200, 32)
        Me.uxTxtLuminexID.TabIndex = 15
        '
        'uxTxtSerialNum
        '
        Me.uxTxtSerialNum.Location = New System.Drawing.Point(38, 173)
        Me.uxTxtSerialNum.Margin = New System.Windows.Forms.Padding(6)
        Me.uxTxtSerialNum.Name = "uxTxtSerialNum"
        Me.uxTxtSerialNum.Size = New System.Drawing.Size(200, 32)
        Me.uxTxtSerialNum.TabIndex = 14
        '
        'uxLblLuminexServer
        '
        Me.uxLblLuminexServer.Location = New System.Drawing.Point(514, 137)
        Me.uxLblLuminexServer.Margin = New System.Windows.Forms.Padding(6)
        Me.uxLblLuminexServer.Name = "uxLblLuminexServer"
        Me.uxLblLuminexServer.Size = New System.Drawing.Size(160, 25)
        Me.uxLblLuminexServer.TabIndex = 13
        Me.uxLblLuminexServer.Text = "uxLuminexServer"
        '
        'uxLblLuminexID
        '
        Me.uxLblLuminexID.Location = New System.Drawing.Point(278, 137)
        Me.uxLblLuminexID.Margin = New System.Windows.Forms.Padding(6)
        Me.uxLblLuminexID.Name = "uxLblLuminexID"
        Me.uxLblLuminexID.Size = New System.Drawing.Size(149, 25)
        Me.uxLblLuminexID.TabIndex = 12
        Me.uxLblLuminexID.Text = "uxLblLuminexID"
        '
        'uxLblSerialNum
        '
        Me.uxLblSerialNum.Location = New System.Drawing.Point(38, 137)
        Me.uxLblSerialNum.Margin = New System.Windows.Forms.Padding(6)
        Me.uxLblSerialNum.Name = "uxLblSerialNum"
        Me.uxLblSerialNum.Size = New System.Drawing.Size(145, 25)
        Me.uxLblSerialNum.TabIndex = 11
        Me.uxLblSerialNum.Text = "uxLblSerialNum"
        '
        'uxGroupLDM1
        '
        Me.uxGroupLDM1.Controls.Add(Me.uxBtnSelect)
        Me.uxGroupLDM1.Controls.Add(Me.uxBtnClear)
        Me.uxGroupLDM1.Controls.Add(Me.uxBtnSave)
        Me.uxGroupLDM1.Dock = System.Windows.Forms.DockStyle.Top
        Me.uxGroupLDM1.Location = New System.Drawing.Point(0, 0)
        Me.uxGroupLDM1.Margin = New System.Windows.Forms.Padding(6)
        Me.uxGroupLDM1.Name = "uxGroupLDM1"
        Me.uxGroupLDM1.Size = New System.Drawing.Size(765, 113)
        Me.uxGroupLDM1.TabIndex = 19
        '
        'uxBtnSelect
        '
        Me.uxBtnSelect.Location = New System.Drawing.Point(473, 54)
        Me.uxBtnSelect.Name = "uxBtnSelect"
        Me.uxBtnSelect.Size = New System.Drawing.Size(170, 44)
        Me.uxBtnSelect.TabIndex = 2
        Me.uxBtnSelect.Text = "uxBtnSelect"
        '
        'LuminexDataManagementForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(765, 618)
        Me.Controls.Add(Me.uxGridLuminexData)
        Me.Controls.Add(Me.uxTxtLuminexServer)
        Me.Controls.Add(Me.uxTxtLuminexID)
        Me.Controls.Add(Me.uxTxtSerialNum)
        Me.Controls.Add(Me.uxLblLuminexServer)
        Me.Controls.Add(Me.uxLblLuminexID)
        Me.Controls.Add(Me.uxLblSerialNum)
        Me.Controls.Add(Me.uxGroupLDM1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "LuminexDataManagementForm"
        Me.Text = "Manage Luminex Systems"
        CType(Me.uxGridLuminexData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxTxtLuminexServer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxTxtLuminexID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxTxtSerialNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxGroupLDM1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.uxGroupLDM1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents uxGridLuminexData As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents uxBtnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents uxBtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents uxTxtLuminexServer As DevExpress.XtraEditors.TextEdit
    Friend WithEvents uxTxtLuminexID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents uxTxtSerialNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents uxLblLuminexServer As DevExpress.XtraEditors.LabelControl
    Friend WithEvents uxLblLuminexID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents uxLblSerialNum As DevExpress.XtraEditors.LabelControl
    Friend WithEvents uxGroupLDM1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents uxBtnSelect As DevExpress.XtraEditors.SimpleButton
End Class
