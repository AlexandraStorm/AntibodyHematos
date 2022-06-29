<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")> _
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginForm))
        Me.uxPasswordLabel = New DevExpress.XtraEditors.LabelControl()
        Me.uxPassword = New DevExpress.XtraEditors.TextEdit()
        Me.uxLoginLabel = New DevExpress.XtraEditors.LabelControl()
        Me.uxLogin = New DevExpress.XtraEditors.TextEdit()
        Me.uxLoginCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.uxLoginOK = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.uxPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uxLogin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'uxPasswordLabel
        '
        Me.uxPasswordLabel.Location = New System.Drawing.Point(48, 59)
        Me.uxPasswordLabel.Name = "uxPasswordLabel"
        Me.uxPasswordLabel.Size = New System.Drawing.Size(50, 13)
        Me.uxPasswordLabel.TabIndex = 8
        Me.uxPasswordLabel.Text = "&Password:"
        '
        'uxPassword
        '
        Me.uxPassword.Location = New System.Drawing.Point(102, 56)
        Me.uxPassword.Name = "uxPassword"
        Me.uxPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.uxPassword.Size = New System.Drawing.Size(157, 20)
        Me.uxPassword.TabIndex = 9
        '
        'uxLoginLabel
        '
        Me.uxLoginLabel.Location = New System.Drawing.Point(42, 33)
        Me.uxLoginLabel.Name = "uxLoginLabel"
        Me.uxLoginLabel.Size = New System.Drawing.Size(56, 13)
        Me.uxLoginLabel.TabIndex = 6
        Me.uxLoginLabel.Text = "&User Name:"
        '
        'uxLogin
        '
        Me.uxLogin.Location = New System.Drawing.Point(102, 30)
        Me.uxLogin.Name = "uxLogin"
        Me.uxLogin.Size = New System.Drawing.Size(157, 20)
        Me.uxLogin.TabIndex = 7
        '
        'uxLoginCancel
        '
        Me.uxLoginCancel.CausesValidation = False
        Me.uxLoginCancel.Location = New System.Drawing.Point(184, 96)
        Me.uxLoginCancel.Name = "uxLoginCancel"
        Me.uxLoginCancel.Size = New System.Drawing.Size(75, 23)
        Me.uxLoginCancel.TabIndex = 11
        Me.uxLoginCancel.Text = "&Cancel"
        '
        'uxLoginOK
        '
        Me.uxLoginOK.Location = New System.Drawing.Point(102, 96)
        Me.uxLoginOK.Name = "uxLoginOK"
        Me.uxLoginOK.Size = New System.Drawing.Size(75, 23)
        Me.uxLoginOK.TabIndex = 10
        Me.uxLoginOK.Text = "&OK"
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(301, 148)
        Me.Controls.Add(Me.uxPasswordLabel)
        Me.Controls.Add(Me.uxPassword)
        Me.Controls.Add(Me.uxLoginLabel)
        Me.Controls.Add(Me.uxLogin)
        Me.Controls.Add(Me.uxLoginCancel)
        Me.Controls.Add(Me.uxLoginOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "User Login"
        CType(Me.uxPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uxLogin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents uxPasswordLabel As DevExpress.XtraEditors.LabelControl
    Private WithEvents uxPassword As DevExpress.XtraEditors.TextEdit
    Private WithEvents uxLoginLabel As DevExpress.XtraEditors.LabelControl
    Private WithEvents uxLogin As DevExpress.XtraEditors.TextEdit
    Private WithEvents uxLoginCancel As DevExpress.XtraEditors.SimpleButton
    Private WithEvents uxLoginOK As DevExpress.XtraEditors.SimpleButton

End Class
