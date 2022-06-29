<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddLumIDForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddLumIDForm))
        Me.btSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.uxTxtLuminexServer = New DevExpress.XtraEditors.TextEdit()
        Me.uxTxtLuminexID = New DevExpress.XtraEditors.TextEdit()
        Me.uxTxtSerialNum = New DevExpress.XtraEditors.TextEdit()
        Me.uxLblLuminexServer = New DevExpress.XtraEditors.LabelControl()
        Me.uxLblLuminexID = New DevExpress.XtraEditors.LabelControl()
        Me.uxLblSerialNum = New DevExpress.XtraEditors.LabelControl()
        CType(Me.uxTxtLuminexServer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxTxtLuminexID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxTxtSerialNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btSave
        '
        Me.btSave.Location = New System.Drawing.Point(82, 105)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(75, 23)
        Me.btSave.TabIndex = 2
        Me.btSave.Text = "btSave"
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(220, 105)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 3
        Me.btCancel.Text = "btCancel"
        '
        'uxTxtLuminexServer
        '
        Me.uxTxtLuminexServer.Location = New System.Drawing.Point(261, 47)
        Me.uxTxtLuminexServer.Name = "uxTxtLuminexServer"
        Me.uxTxtLuminexServer.Size = New System.Drawing.Size(100, 20)
        Me.uxTxtLuminexServer.TabIndex = 1
        '
        'uxTxtLuminexID
        '
        Me.uxTxtLuminexID.Location = New System.Drawing.Point(143, 47)
        Me.uxTxtLuminexID.Name = "uxTxtLuminexID"
        Me.uxTxtLuminexID.Size = New System.Drawing.Size(100, 20)
        Me.uxTxtLuminexID.TabIndex = 0
        '
        'uxTxtSerialNum
        '
        Me.uxTxtSerialNum.Location = New System.Drawing.Point(23, 47)
        Me.uxTxtSerialNum.Name = "uxTxtSerialNum"
        Me.uxTxtSerialNum.Size = New System.Drawing.Size(100, 20)
        Me.uxTxtSerialNum.TabIndex = 4
        '
        'uxLblLuminexServer
        '
        Me.uxLblLuminexServer.Location = New System.Drawing.Point(261, 28)
        Me.uxLblLuminexServer.Name = "uxLblLuminexServer"
        Me.uxLblLuminexServer.Size = New System.Drawing.Size(83, 13)
        Me.uxLblLuminexServer.TabIndex = 19
        Me.uxLblLuminexServer.Text = "uxLuminexServer"
        '
        'uxLblLuminexID
        '
        Me.uxLblLuminexID.Location = New System.Drawing.Point(143, 28)
        Me.uxLblLuminexID.Name = "uxLblLuminexID"
        Me.uxLblLuminexID.Size = New System.Drawing.Size(75, 13)
        Me.uxLblLuminexID.TabIndex = 18
        Me.uxLblLuminexID.Text = "uxLblLuminexID"
        '
        'uxLblSerialNum
        '
        Me.uxLblSerialNum.Location = New System.Drawing.Point(23, 28)
        Me.uxLblSerialNum.Name = "uxLblSerialNum"
        Me.uxLblSerialNum.Size = New System.Drawing.Size(72, 13)
        Me.uxLblSerialNum.TabIndex = 17
        Me.uxLblSerialNum.Text = "uxLblSerialNum"
        '
        'AddLumIDForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 146)
        Me.Controls.Add(Me.uxTxtLuminexServer)
        Me.Controls.Add(Me.uxTxtLuminexID)
        Me.Controls.Add(Me.uxTxtSerialNum)
        Me.Controls.Add(Me.uxLblLuminexServer)
        Me.Controls.Add(Me.uxLblLuminexID)
        Me.Controls.Add(Me.uxLblSerialNum)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.btSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddLumIDForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Missing Luminex ID"
        CType(Me.uxTxtLuminexServer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxTxtLuminexID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxTxtSerialNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents uxTxtLuminexServer As DevExpress.XtraEditors.TextEdit
    Friend WithEvents uxTxtLuminexID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents uxTxtSerialNum As DevExpress.XtraEditors.TextEdit
    Friend WithEvents uxLblLuminexServer As DevExpress.XtraEditors.LabelControl
    Friend WithEvents uxLblLuminexID As DevExpress.XtraEditors.LabelControl
    Friend WithEvents uxLblSerialNum As DevExpress.XtraEditors.LabelControl
End Class
