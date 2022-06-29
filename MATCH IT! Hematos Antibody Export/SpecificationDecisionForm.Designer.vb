<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpecificationDecisionForm
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
        Me.lblAlertMessage = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.RadioGroupOptions = New DevExpress.XtraEditors.RadioGroup()
        Me.txtBoxComment = New DevExpress.XtraEditors.TextEdit()
        Me.lblOptionMessage = New DevExpress.XtraEditors.LabelControl()
        Me.btOK = New System.Windows.Forms.Button()
        Me.btCancel = New System.Windows.Forms.Button()
        Me.ComboBoxSpecification = New System.Windows.Forms.ComboBox()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.RadioGroupOptions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBoxComment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblAlertMessage
        '
        Me.lblAlertMessage.Location = New System.Drawing.Point(17, 12)
        Me.lblAlertMessage.Name = "lblAlertMessage"
        Me.lblAlertMessage.Size = New System.Drawing.Size(421, 39)
        Me.lblAlertMessage.TabIndex = 0
        Me.lblAlertMessage.Text = "The utility was unable to a find specification code in the sample's comments. Thi" & _
    "s sample" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "was found to have positive antigens and did not have any tail antigens" & _
    " assigned. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please correct below."
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(72, 64)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(83, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Select an Option:"
        '
        'RadioGroupOptions
        '
        Me.RadioGroupOptions.Location = New System.Drawing.Point(161, 64)
        Me.RadioGroupOptions.Name = "RadioGroupOptions"
        Me.RadioGroupOptions.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Choose Specification"), New DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Enter Comment"), New DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Both")})
        Me.RadioGroupOptions.Size = New System.Drawing.Size(136, 66)
        Me.RadioGroupOptions.TabIndex = 2
        '
        'txtBoxComment
        '
        Me.txtBoxComment.Location = New System.Drawing.Point(83, 173)
        Me.txtBoxComment.Name = "txtBoxComment"
        Me.txtBoxComment.Size = New System.Drawing.Size(343, 20)
        Me.txtBoxComment.TabIndex = 3
        '
        'lblOptionMessage
        '
        Me.lblOptionMessage.Location = New System.Drawing.Point(12, 176)
        Me.lblOptionMessage.Name = "lblOptionMessage"
        Me.lblOptionMessage.Size = New System.Drawing.Size(49, 13)
        Me.lblOptionMessage.TabIndex = 4
        Me.lblOptionMessage.Text = "Comment:"
        '
        'btOK
        '
        Me.btOK.Location = New System.Drawing.Point(143, 212)
        Me.btOK.Name = "btOK"
        Me.btOK.Size = New System.Drawing.Size(75, 23)
        Me.btOK.TabIndex = 5
        Me.btOK.Text = "OK"
        Me.btOK.UseVisualStyleBackColor = True
        '
        'btCancel
        '
        Me.btCancel.Location = New System.Drawing.Point(237, 212)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(75, 23)
        Me.btCancel.TabIndex = 6
        Me.btCancel.Text = "Cancel"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'ComboBoxSpecification
        '
        Me.ComboBoxSpecification.FormattingEnabled = True
        Me.ComboBoxSpecification.Items.AddRange(New Object() {"", "Invalid", "Multispecific", "Negative", "Not Tested", "Unspecified (P)", "Unspecified (U)"})
        Me.ComboBoxSpecification.Location = New System.Drawing.Point(83, 143)
        Me.ComboBoxSpecification.Name = "ComboBoxSpecification"
        Me.ComboBoxSpecification.Size = New System.Drawing.Size(152, 21)
        Me.ComboBoxSpecification.TabIndex = 7
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 147)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(64, 13)
        Me.LabelControl1.TabIndex = 8
        Me.LabelControl1.Text = "Specification:"
        '
        'SpecificationDecisionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(454, 246)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.ComboBoxSpecification)
        Me.Controls.Add(Me.btCancel)
        Me.Controls.Add(Me.btOK)
        Me.Controls.Add(Me.lblOptionMessage)
        Me.Controls.Add(Me.txtBoxComment)
        Me.Controls.Add(Me.RadioGroupOptions)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.lblAlertMessage)
        Me.Name = "SpecificationDecisionForm"
        Me.Text = "SpecificationDecisionForm"
        CType(Me.RadioGroupOptions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBoxComment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblAlertMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RadioGroupOptions As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents txtBoxComment As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblOptionMessage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btOK As System.Windows.Forms.Button
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Friend WithEvents ComboBoxSpecification As System.Windows.Forms.ComboBox
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
