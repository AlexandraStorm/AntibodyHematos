Public Class SpecificationDecisionForm

    Enum SpecOption
        Specification = 0
        Comment = 1
        Both = 2
    End Enum

    Public Property sampleComment As String
    Public Property cancelForm As Boolean
    Public Property sampleSpec As String
    Public Property sampleUseSpec As Boolean
    Private Property _newSample As Boolean

    Public Sub New(ByVal comment As String)
        InitializeComponent()
        sampleComment = comment
    End Sub

    Private Sub SpecificationDecisionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not String.IsNullOrEmpty(sampleComment) Then
            txtBoxComment.Text = sampleComment
            RadioGroupOptions.SelectedIndex = SpecOption.Both
        Else
            RadioGroupOptions.SelectedIndex = SpecOption.Specification
        End If
    End Sub

    Public Sub New(ByVal comment As String, ByVal specification As String)
        InitializeComponent()
        sampleComment = comment
        sampleSpec = specification

        SetSpec()
    End Sub
    Private Sub RadioGroupOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioGroupOptions.SelectedIndexChanged
        If RadioGroupOptions.SelectedIndex = SpecOption.Specification Then
            txtBoxComment.Enabled = False
            txtBoxComment.Text = ""
            ComboBoxSpecification.Enabled = True
        ElseIf RadioGroupOptions.SelectedIndex = SpecOption.Comment Then
            txtBoxComment.Enabled = True
            ComboBoxSpecification.Enabled = False
            ComboBoxSpecification.SelectedIndex = -1
            txtBoxComment.Text = ""
        Else
            txtBoxComment.Enabled = True
            ComboBoxSpecification.Enabled = True
        End If
    End Sub

    Private Sub btOK_Click(sender As Object, e As EventArgs) Handles btOK.Click
        cancelForm = False
        If RadioGroupOptions.SelectedIndex = SpecOption.Comment Then
            sampleUseSpec = False
        Else
            If String.IsNullOrEmpty(ComboBoxSpecification.SelectedItem.ToString()) Then
                MessageBox.Show("Please select a specification for this sample.")
                Exit Sub
            Else
                sampleSpec = ComboBoxSpecification.SelectedItem.ToString()
                sampleUseSpec = True
            End If
            
        End If
        sampleComment = txtBoxComment.Text

        Close()
    End Sub

    Private Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        cancelForm = True
        Close()
    End Sub

    Private Sub SetSpec()

        If String.IsNullOrEmpty(sampleSpec) Then
            ComboBoxSpecification.SelectedIndex = -1
        Else
            Select Case sampleSpec.ToLower()
                Case "i"
                    ComboBoxSpecification.SelectedIndex = 1
                Case "multi"
                    ComboBoxSpecification.SelectedIndex = 2
                Case "n"
                    ComboBoxSpecification.SelectedIndex = 3
                Case "nt"
                    ComboBoxSpecification.SelectedIndex = 4
                Case "u"
                    ComboBoxSpecification.SelectedIndex = 5
            End Select
        End If
    End Sub

End Class