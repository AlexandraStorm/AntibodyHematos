Public Class LoginForm

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private _userName As String
    Public Function UserName() As String
        Return _userName
    End Function
    Private _password As String
    Public Function Password() As String
        Return _password
    End Function

    Private Sub uxLoginOK_Click(sender As Object, e As EventArgs) Handles uxLoginOK.Click
        Me.DialogResult = DialogResult.OK
        _userName = uxLogin.Text
        _password = uxPassword.Text
        Me.Close()
    End Sub

    Private Sub uxLoginCancel_Click(sender As Object, e As EventArgs) Handles uxLoginCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Environment.[Exit](0)
    End Sub

    Private Sub uxPassword_KeyPress(sender As Object, ByVal e As KeyPressEventArgs) Handles uxLoginCancel.KeyPress, uxPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            e.Handled = True
            uxLoginOK_Click(Nothing, Nothing)
        ElseIf e.KeyChar = ChrW(Keys.Escape) Then
            uxLoginCancel_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub LoginForm_KeyPress(sender As Object, ByVal e As KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = ChrW(Keys.Escape) Then
            uxLoginCancel_Click(Nothing, Nothing)
        End If
    End Sub
End Class
