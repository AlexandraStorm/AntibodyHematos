Imports HematosPresenter
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports DevExpress.XtraGrid.Views.Grid

Public Class LuminexDataManagementForm
    Implements ILuminexDataManagement

    Private _lumID As Integer
    Private _returnLumID As String
    Private WithEvents _presenter As LuminexDataManagementPresenter
    Public Event RowSelection(ByVal sender As Object, ByVal e As System.EventArgs) Implements ILuminexDataManagement.gridRowSelection
    'Private XMLPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\Lifecodes\XMLLumServerList.XML"
#Region "Properties"
    Public WriteOnly Property FormTitle() As String Implements ILuminexDataManagement.FormTitle
        Set(ByVal value As String)
            Me.Text = value
        End Set
    End Property

    Public WriteOnly Property btnClear() As String Implements ILuminexDataManagement.btnClear
        Set(ByVal value As String)
            uxBtnClear.Text = value
        End Set
    End Property

    Public WriteOnly Property btnSave() As String Implements ILuminexDataManagement.btnSave
        Set(ByVal value As String)
            uxBtnSave.Text = value
        End Set
    End Property
    Public WriteOnly Property btnSelect() As String Implements ILuminexDataManagement.btnSelect
        Set(ByVal value As String)
            uxBtnSelect.Text = value
        End Set
    End Property

    Public Property gridLuminexData() As DevExpress.XtraGrid.GridControl Implements ILuminexDataManagement.gridLuminexData
        Get
            Return uxGridLuminexData
        End Get
        Set(ByVal value As DevExpress.XtraGrid.GridControl)
            uxGridLuminexData = value
        End Set
    End Property

    Public WriteOnly Property lblLuminexID() As String Implements ILuminexDataManagement.lblLuminexID
        Set(ByVal value As String)
            uxLblLuminexID.Text = value
        End Set
    End Property

    Public WriteOnly Property lblLuminexServer() As String Implements ILuminexDataManagement.lblLuminexServer
        Set(ByVal value As String)
            uxLblLuminexServer.Text = value
        End Set
    End Property

    Public WriteOnly Property lblSerialNum() As String Implements ILuminexDataManagement.lblSerialNum
        Set(ByVal value As String)
            uxLblSerialNum.Text = value
        End Set
    End Property

    Public Property txtLuminexID() As DevExpress.XtraEditors.TextEdit Implements ILuminexDataManagement.txtLuminexID
        Get
            Return uxTxtLuminexID
        End Get
        Set(ByVal value As DevExpress.XtraEditors.TextEdit)
            uxTxtLuminexID = value
        End Set
    End Property

    Public Property txtLuminexServer() As DevExpress.XtraEditors.TextEdit Implements ILuminexDataManagement.txtLuminexServer
        Get
            Return uxTxtLuminexServer
        End Get
        Set(ByVal value As DevExpress.XtraEditors.TextEdit)
            uxTxtLuminexID = value
        End Set
    End Property

    Public Property txtSerialNumber() As DevExpress.XtraEditors.TextEdit Implements ILuminexDataManagement.txtSerialNumber
        Get
            Return uxTxtSerialNum
        End Get
        Set(ByVal value As DevExpress.XtraEditors.TextEdit)
            uxTxtSerialNum = value
        End Set
    End Property
    'Public Property txtLumID() As DevExpress.XtraEditors.TextEdit Implements ILuminexDataManagement.txtLumID
    '    Get
    '        Return uxTxtLumID
    '    End Get
    '    Set(ByVal value As DevExpress.XtraEditors.TextEdit)
    '        uxTxtLumID = value
    '    End Set
    'End Property
    Public Property ReturnLumID() As String
        Get
            Return _returnLumID
        End Get
        Set(ByVal value As String)
            _returnLumID = value
        End Set
    End Property
#End Region

    Private Sub LuminexDataManagementForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        _presenter = New LuminexDataManagementPresenter(Me)
        'uxBtnClear.Enabled = False
        'uxBtnSave.Enabled = False
        'uxBtnSelect.Enabled = False
        '_presenter.CheckLumXMLFile(XMLPath)
    End Sub

    Private Sub uxBtnClear_Click(sender As Object, e As EventArgs) Handles uxBtnClear.Click
        _presenter.ClearLuminexDataForm()
    End Sub

    Private Sub uxBtnSave_Click(sender As Object, e As EventArgs) Handles uxBtnSave.Click
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
            MessageBox.Show("Luminex Data Saved Successfully", "Attention", MessageBoxButtons.OK)
            _presenter.ClearLuminexDataForm()
        End If
    End Sub

    Private Sub uxGridLuminexData_DoubleClick(sender As Object, e As EventArgs) Handles uxGridLuminexData.DoubleClick
        RaiseEvent RowSelection(sender, e)
    End Sub

    Private Sub uxBtnSelect_Click(sender As Object, e As EventArgs) Handles uxBtnSelect.Click
        Dim hello As String = "hello"
        Dim selRows As Integer() = DirectCast(gridLuminexData.MainView, GridView).GetSelectedRows()
        Dim selRow As DataRowView = DirectCast(DirectCast(gridLuminexData.MainView, GridView).GetRow(selRows(0)), DataRowView)
        _returnLumID = selRow("LuminexID")
    End Sub
End Class