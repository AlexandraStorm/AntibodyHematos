Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.UserSkins
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports HematosPresenter
Imports HematosViewModel

Public Class MainForm
    Implements IMainView

    Private presenter As MainPresenter
    Private _isLoading As Boolean = False
    Private _batchlist As List(Of String)
    Private _saveFilePath As String
    Private _lumID As String
    Private _lumSN As String
    Private _siteCode As String
    Private _loginOk As Boolean = False

#Region "Properties"
    Public ReadOnly Property filterSelection As Integer Implements IMainView.filterSelection
        Get
            Return uxrdoFilter.SelectedIndex
        End Get
    End Property
    Public Property SampleList As List(Of SampleVM) Implements IMainView.SampleList
        Get
            Return uxlbSamples.DataSource
        End Get
        Set(value As List(Of SampleVM))
            uxlbSamples.DataSource = value
        End Set
    End Property
    Public Property SelectedSamples As List(Of SampleVM) Implements IMainView.SelectedSamples
        Get
            Return uxlbSelectedSamples.DataSource
        End Get
        Set(value As List(Of SampleVM))
            uxlbSelectedSamples.DataSource = value
        End Set
    End Property
    Public Property BatchSearchList As List(Of String) Implements IMainView.BatchSearchList
        Set(value As List(Of String))
            uxlbBatches.DataSource = value
        End Set
        Get
            Return uxlbBatches.DataSource
        End Get
    End Property
    Public Property selectedBatch As String Implements IMainView.selectedBatch
        Get
            Return uxlbBatches.SelectedItem
        End Get
        Set(value As String)
            uxlbBatches.SelectedItem = value
        End Set
    End Property
    Public Property SaveFilePath As String Implements IMainView.SaveFilePath
        Set(value As String)
            _saveFilePath = value
        End Set
        Get
            Return _saveFilePath
        End Get
    End Property
    Public Property SiteCode As String Implements IMainView.SiteCode
        Set(value As String)
            _siteCode = value
        End Set
        Get
            Return _siteCode
        End Get
    End Property
    Public Property LumID As String Implements IMainView.LumID
        Set(value As String)
            _lumID = value
        End Set
        Get
            Return _lumID
        End Get
    End Property
    Public Property LumSN As String Implements IMainView.LumSN
        Set(value As String)
            _lumSN = value
        End Set
        Get
            Return _lumSN
        End Get
    End Property
    Public Property LumServer As String Implements IMainView.LumServer
#End Region

    Sub New()
        InitSkins()
        InitializeComponent()
    End Sub

    Public Property BatchList As List(Of String) Implements IMainView.BatchList
        Get
            Return _batchlist
        End Get
        Set(value As List(Of String))
            _batchlist = value
        End Set
    End Property

    Private Property addSample As Boolean

    Shared Sub InitSkins()
        SkinManager.EnableFormSkins()
        BonusSkins.Register()
        UserLookAndFeel.Default.SetSkinStyle("DevExpress Style")
    End Sub

    Private Sub MainView_Load(sender As Object, e As EventArgs) Handles Me.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")
        System.Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo("en-US")
        _isLoading = True
        Try
            presenter = New MainPresenter(Me)

        Catch ex As Exception
            ExceptionPolicy.HandleException(ex, "Log Only Policy")
            MessageBox.Show("Error: unable to connect to the database.")
            Application.Exit()
        End Try
        If presenter._dbOK Then
            Using login As New LoginForm()
                Dim canceled As Boolean = False
                While Not (_loginOk OrElse canceled)
                    If login.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        If presenter.VaildateUser(login.UserName(), login.Password()) Then
                            presenter._dbOK = True
                            _loginOk = True
                            LoadComboBoxes()
                            presenter.ReadSavePath()
                            If Not _saveFilePath Is Nothing Then
                                DropFolderPathtxt.Text = _saveFilePath
                            Else
                                _saveFilePath = ""
                            End If
                            _batchlist = New List(Of String)
                            SelectedSamples = New List(Of SampleVM)
                            SampleList = New List(Of SampleVM)
                            uxrdoFilter.SelectedIndex = 0
                            ExportBt.Enabled = False
                            _isLoading = False
                        Else
                            MessageBox.Show("The user credentials you entered are" & vbLf & "not valid for the current database.")
                        End If
                    Else
                        canceled = False
                        _loginOk = False
                    End If
                End While

            End Using
        End If
    End Sub

    Private Sub LoadComboBoxes()
        Try
            LoadSiteCodeComboBx()
            LoadLotIDComboBx()
        Catch ex As Exception
            MessageBox.Show(ex.Message().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub LoadLotIDComboBx()
        LotIDComboBx.Properties.Items.AddRange(presenter.LoadLotIDs())
        LotIDComboBx.SelectedIndex = -1
    End Sub

    Private Sub LoadSiteCodeComboBx()
        Dim SiteCodes As List(Of String) = presenter.LoadSiteCodeComboBx()
        SiteCodeComboBx.Properties.Items.AddRange(SiteCodes)
        Dim Dsite As String = presenter.CheckForDefaultSiteCode()
        If String.IsNullOrEmpty(Dsite) Then
            SiteCodeComboBx.SelectedIndex = -1
        Else
            Dim index = SiteCodes.FindIndex(Function(a) a.Contains(Dsite))
            SiteCodeComboBx.SelectedIndex = index
        End If
    End Sub

    Private Sub LotIDComboBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LotIDComboBx.SelectedIndexChanged
        BatchList = presenter.GetBatches(LotIDComboBx.SelectedItem)
        textBoxLumID.Clear()
        uxlbBatches.Refresh()
        uxlbBatches.DataSource = BatchList
        ExportBt.Enabled = False
    End Sub

    Private Sub uxlbBatches_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxlbBatches.SelectedIndexChanged
        If (SelectedSamples.Count > 0) Then SelectedSamples.Clear()
        textBoxLumID.Clear()
        SampleList.Clear()
        uxlbSamples.Refresh()
        uxlbSelectedSamples.Refresh()
        presenter.GetSamples()
        textBoxLumID.Text = _lumID
        ExportBt.Enabled = False
        If LotIDComboBx.Text.Contains("LMX") Then
            AbClassCode.Enabled = False
        Else
            AbClassCode.Enabled = True
        End If
    End Sub

    Private Sub uxbtnMoveAll_Click(sender As Object, e As EventArgs) Handles uxbtnMoveAll.Click
        Dim removeList As List(Of SampleVM) = New List(Of SampleVM)
        For Each sample As SampleVM In SampleList
            addSample = False
            If sample.CheckSpec = 1 Then
                sample = CheckCommentsForSpec(sample)

                If addSample = True Then
                    SelectedSamples.Add(sample)
                    removeList.Add(sample)
                End If
            Else
                SelectedSamples.Add(sample)
                removeList.Add(sample)
            End If
        Next

        For Each sample As SampleVM In removeList
            SampleList.Remove(sample)
        Next
        uxlbSelectedSamples.Refresh()
        uxlbSamples.Refresh()
        ExportBt.Enabled = True
    End Sub

    Private Sub uxbtnMoveOne_Click(sender As Object, e As EventArgs) Handles uxbtnMoveOne.Click
        If Not (uxlbSamples.SelectedItem Is Nothing) Then
            addSample = False
            Dim sample As SampleVM = CType(uxlbSamples.SelectedItem, SampleVM)
            If sample.CheckSpec = 1 Then
                sample = CheckCommentsForSpec(sample)
                If addSample = True Then
                    SelectedSamples.Add(sample)
                    SampleList.Remove(sample)
                End If
            Else
                SelectedSamples.Add(sample)
                SampleList.Remove(sample)
            End If
           
            uxlbSelectedSamples.Refresh()
            uxlbSamples.Refresh()
            If SelectedSamples.Count > 0 Then
                ExportBt.Enabled = True
            Else
                ExportBt.Enabled = False
            End If
        End If
    End Sub

    Private Sub uxbtnRemoveOne_Click(sender As Object, e As EventArgs) Handles uxbtnRemoveOne.Click
        If Not (uxlbSelectedSamples.SelectedItem Is Nothing) Then
            SampleList.Add(uxlbSelectedSamples.SelectedItem)
            SelectedSamples.Remove(uxlbSelectedSamples.SelectedItem)
            uxlbSelectedSamples.Refresh()
            uxlbSamples.Refresh()
            If SelectedSamples.Count = 0 Then
                ExportBt.Enabled = False
            End If
        End If
    End Sub

    Private Sub uxbtnRemoveAll_Click(sender As Object, e As EventArgs) Handles uxbtnRemoveAll.Click
        For Each item As SampleVM In SelectedSamples
            SampleList.Add(item)
        Next
        SelectedSamples.Clear()
        uxlbSelectedSamples.Refresh()
        uxlbSamples.Refresh()
        ExportBt.Enabled = False
    End Sub

    Private Function CheckCommentsForSpec(ByVal sample As SampleVM) As SampleVM

        sample = presenter.CheckSampleComments(sample)
        If sample.useSpec = False Then
            Using specCommentForm As New SpecificationDecisionForm(sample.comment, sample.specification)
                specCommentForm.Text = String.Format("Sample: {0}", sample.sampleID)
                specCommentForm.ShowDialog()
                If specCommentForm.cancelForm Then
                    addSample = False
                Else
                    addSample = True
                    If specCommentForm.sampleUseSpec = False Then
                        sample.useSpec = False
                    Else
                        sample.specification = presenter.ConvertCode(specCommentForm.sampleSpec)
                        sample.useSpec = True
                    End If
                    sample.comment = specCommentForm.sampleComment
                End If
            End Using
        Else
            addSample = True
        End If

        Return sample
    End Function
    Private Sub BrowseBt_Click(sender As Object, e As EventArgs) Handles BrowseBt.Click
        Dim fDialog As New FolderBrowserDialog() With {.Description = "Please select the folder you would to export files to:"}
        fDialog.ShowDialog()

        If Not (fDialog.SelectedPath = Nothing And fDialog.SelectedPath = "") Then
            DropFolderPathtxt.Text = fDialog.SelectedPath()
            _saveFilePath = fDialog.SelectedPath()
            Try
                presenter.SetSavePath()
            Catch ex As Exception
                ExceptionPolicy.HandleException(ex, "Log Only Policy")
            End Try
        End If
    End Sub

    Private Sub uxrdoFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles uxrdoFilter.SelectedIndexChanged
        If (_isLoading = False) Then
            SelectedSamples.Clear()
            uxlbSelectedSamples.Refresh()
            presenter.GetSamples()
            uxlbSamples.Refresh()
        End If
    End Sub

    Private Sub SiteCodeComboBx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SiteCodeComboBx.SelectedIndexChanged
        _siteCode = SiteCodeComboBx.SelectedItem
        presenter.SaveSiteCode()
    End Sub

    Private Sub ExportBt_Click(sender As Object, e As EventArgs) Handles ExportBt.Click
        If String.IsNullOrEmpty(DropFolderPathtxt.Text) Then
            MessageBox.Show("Please enter a drop folder path!", "Drop Folder Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If (String.IsNullOrEmpty(textBoxLumID.Text) Or LumID = "Luminex ID Not Found" Or LumID = Nothing) Then
            MessageBox.Show(String.Format("Please enter a Luminex ID for the batch: {0}.", selectedBatch), "Luminex ID Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dim AddLumIDFormCanel As Boolean = False

            Using AddLumIDForm As New AddLumIDForm()
                ' fill in server info
                If String.IsNullOrEmpty(_lumSN) Then
                    AddLumIDForm.SetLuminexServer(_LumServer)
                Else
                    AddLumIDForm.SetLuminexSN(_lumSN)
                End If
                AddLumIDForm.ShowDialog()
                _lumID = AddLumIDForm.ReturnLumID
                AddLumIDFormCanel = AddLumIDForm.ReturnCancel
            End Using

            If AddLumIDFormCanel = True Then
                Exit Sub
            End If
        End If

        Dim NewSample As Boolean
        If uxrdoFilter.SelectedIndex = 0 Then
            NewSample = True
        Else
            NewSample = False
        End If

        Try
            If (presenter.ExportData(LotIDComboBx.SelectedItem, AbClassCode.EditValue, _lumID, NewSample)) Then
                MessageBox.Show("Results successfully exported!")
                If (SelectedSamples.Count > 0) Then
                    SelectedSamples.Clear()
                    uxlbSelectedSamples.Refresh()
                    presenter.GetSamples()
                    uxlbSamples.Refresh()
                    textBoxLumID.Text = _lumID
                    AbClassCode.SelectedIndex = 2
                    uxrdoFilter.SelectedIndex = 0
                    ExportBt.Enabled = False
                End If
            Else
                MessageBox.Show("Results failed to export!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            ExceptionPolicy.HandleException(ex, "Log Only Policy")
            MessageBox.Show("Results failed to export!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Dim loci As LociRules
        loci = New LociRules()
        loci.ShowDialog()
    End Sub
End Class