Imports HematosPresenter
Public Class AddLumIDForm
    Implements ILuminexIDForm
    Private _returnCancel As Boolean
    Private WithEvents _presenter As AddLuminexIDPresenter

#Region "Properties"
    Public WriteOnly Property FormTitle() As String Implements ILuminexIDForm.FormTitle
        Set(ByVal value As String)
            Text = value
        End Set
    End Property
    Public ReadOnly Property BatchID() As String Implements ILuminexIDForm.BatchID
        Get
            Return BatchID
        End Get
    End Property
    Public WriteOnly Property btnCancel() As String Implements ILuminexIDForm.btnCancel
        Set(ByVal value As String)
            btCancel.Text = value
        End Set
    End Property

    Public WriteOnly Property btnSave() As String Implements ILuminexIDForm.btnSave
        Set(ByVal value As String)
            btSave.Text = value
        End Set
    End Property
    Public WriteOnly Property lblLuminexID() As String Implements ILuminexIDForm.lblLuminexID
        Set(ByVal value As String)
            uxLblLuminexID.Text = value
        End Set
    End Property

    Public WriteOnly Property lblLuminexServer() As String Implements ILuminexIDForm.lblLuminexServer
        Set(ByVal value As String)
            uxLblLuminexServer.Text = value
        End Set
    End Property

    Public WriteOnly Property lblSerialNum() As String Implements ILuminexIDForm.lblSerialNum
        Set(ByVal value As String)
            uxLblSerialNum.Text = value
        End Set
    End Property

    Public Property txtLuminexID() As DevExpress.XtraEditors.TextEdit Implements ILuminexIDForm.txtLuminexID
        Get
            Return uxTxtLuminexID
        End Get
        Set(ByVal value As DevExpress.XtraEditors.TextEdit)
            uxTxtLuminexID = value
        End Set
    End Property

    Public Property txtLuminexServer() As DevExpress.XtraEditors.TextEdit Implements ILuminexIDForm.txtLuminexServer
        Get
            Return uxTxtLuminexServer
        End Get
        Set(ByVal value As DevExpress.XtraEditors.TextEdit)
            uxTxtLuminexID = value
        End Set
    End Property

    Public Property txtSerialNumber() As DevExpress.XtraEditors.TextEdit Implements ILuminexIDForm.txtSerialNumber
        Get
            Return uxTxtSerialNum
        End Get
        Set(ByVal value As DevExpress.XtraEditors.TextEdit)
            uxTxtSerialNum = value
        End Set
    End Property
    Public Property ReturnLumID() As String
    Public Property ReturnCancel() As Boolean Implements ILuminexIDForm.ReturnCancel
        Get
            Return _returnCancel
        End Get
        Set(ByVal value As Boolean)
            _returnCancel = value
        End Set
    End Property
#End Region

    Private Sub LuminexDataManagementForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        btSave.Enabled = False
        _returnCancel = False
        _presenter = New AddLuminexIDPresenter(Me)
    End Sub
    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If (uxTxtSerialNum.Text.Trim().Equals("")) Then
            MessageBox.Show("Please enter a Serial Number", _
                            "Warning", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        ElseIf (uxTxtLuminexID.Text.Trim().Equals("")) Then
            MessageBox.Show("Please enter a Luminex ID", _
                            "Warning", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        ElseIf (uxTxtLuminexServer.Text.Trim().Equals("")) Then
            MessageBox.Show("Please enter a Luminex Server", _
                            "Warning", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim result As Integer = _presenter.SaveLuminexData()

        If (result = -1) Then
            MessageBox.Show("Unable to Save Luminex Data" + vbCrLf + "Duplicate Luminex Server", _
                            "Warning", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf (result = -2) Then
            MessageBox.Show("Unable to Save Luminex Data" + vbCrLf + "Duplicate Luminex ID", _
                            "Warning", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf (result = -3) Then
            MessageBox.Show("Unable to Save Luminex Data" + vbCrLf + "Duplicate Serial Number", _
                            "Warning", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf (result = -4) Then
            MessageBox.Show("Unable to update luminex data" + vbCrLf + "Not a valid luminex server", _
                            "Warning", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf (result = 1) Then
            ReturnLumID = uxTxtLuminexID.Text
            MessageBox.Show("Luminex Data Saved Successfully", "Attention", MessageBoxButtons.OK)
            ReturnCancel = False
            Close()
        End If
    End Sub

    Private Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        _returnCancel = True
        Close()
    End Sub

    Private Sub textBox_TextChanged(sender As Object, e As EventArgs) Handles uxTxtLuminexServer.EditValueChanged, uxTxtLuminexID.EditValueChanged
        If String.IsNullOrEmpty(txtLuminexID.Text) And String.IsNullOrEmpty(txtLuminexServer.Text) Then
            btSave.Enabled = False
        Else
            btSave.Enabled = True
        End If
    End Sub

    Sub SetLuminexSN(LuminexSN As String)
        txtSerialNumber.Text = LuminexSN
    End Sub

    Sub SetLuminexServer(LuminexServer As String)
        txtLuminexServer.Text = LuminexServer
    End Sub

End Class