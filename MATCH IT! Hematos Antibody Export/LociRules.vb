Imports System.Collections
Imports HematosPresenter
Imports HematosPresenter.Interfaces
Imports HematosViewModel

Public Class LociRules
    Implements HematosPresenter.Interfaces.ILociRules
    Private Property myPresenter As LociRulesPresenter
    Private Property myLociData As List(Of LociData)

    Public Property _presenter As HematosPresenter.LociRulesPresenter
        Get
            Return myPresenter
        End Get
        Set(value As HematosPresenter.LociRulesPresenter)
            myPresenter = value
        End Set
    End Property
    Public Property lociData As List(Of LociData) Implements ILociRules.lociData
        Get
            Return myLociData
        End Get
        Set(value As List(Of LociData))
            myLociData = value
        End Set
    End Property

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        myPresenter = New LociRulesPresenter(Me)
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Sub GetData()
        myLociData = myPresenter.LoadData()
    End Sub

    Private Sub LociRules_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetData()
        GridControl1.DataSource = myLociData
    End Sub

    Private Sub btnUpdateRules_Click(sender As Object, e As EventArgs) Handles btnUpdateRules.Click
        Dim dirtyRows = DirectCast(GridControl1.DataSource, List(Of LociData))
        Dim updates As List(Of LociData) = New List(Of LociData)()
        Dim updateAll As Boolean = False

        If (Me.chkUseOne.Checked) Then
            If (Me.UseOne.SelectionLength = 0) Then
                MessageBox.Show("Please select a value to use when One Positive Value exists")
                Return
            Else
                updateAll = True
                _presenter.UpdateAllRule("SelectAllelicOne", UseOne.SelectedText)
            End If
        End If
        If (Me.chkUseMany.Checked) Then
            If (Me.UseMany.SelectionLength = 0) Then
                MessageBox.Show("Please select a value to use when more than 1 Positive Value exists")
                Return
            Else
                updateAll = True
                _presenter.UpdateAllRule("selectedAllelicMany", UseMany.SelectedText)
            End If
        End If
        If (Me.chkUseAll.Checked) Then
            If (Me.AllUse.SelectionLength = 0) Then
                MessageBox.Show("Please select a value to use when All Positive Value exists")
                Return
            Else
                updateAll = True
                _presenter.UpdateAllRule("selectedAllelicAll", AllUse.SelectedText)
            End If
        End If
        If (Me.chkMeanAll.Checked) Then
            If (Me.MeanAll.SelectionLength = 0) Then
                MessageBox.Show("Please select a mean value to use when All Positive Value exists")
                Return
            Else
                updateAll = True
                _presenter.UpdateAllRule("useMedianRawValuesAll", MeanAll.SelectedText)
            End If
        End If
        If (Me.chkMeanMany.Checked) Then
            If (Me.MeanMany.SelectionLength = 0) Then
                MessageBox.Show("Please select a mean value to use when more than 1 Positive Value exists")
                Return
            Else
                updateAll = True
                _presenter.UpdateAllRule("useMedainRawValuesMany", MeanMany.SelectedText)
            End If
        End If
        If (Me.chkMeanOne.Checked) Then
            If (Me.MeanOne.SelectionLength = 0) Then
                MessageBox.Show("Please select a mean value to use when All Positive Value exists")
                Return
            Else
                updateAll = True
                _presenter.UpdateAllRule("useMedianRawValuesOne", MeanOne.SelectedText)
            End If
        End If
        If (Not updateAll) Then
            For Each dr As LociData In dirtyRows
                If (dr.isDirty) Then
                    updates.Add(dr)
                End If
            Next

            Dim complete = _presenter.UpdateRules(updates)
        End If

        GetData()
        GridControl1.DataSource = myLociData
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If (e.Column.Caption.ToUpper() = "LOCI" Or e.Column.Caption.ToUpper() = "ALLELE NAME" Or e.Column.Caption.ToUpper() = "ISDIRTY") Then
            Return
        End If

        Dim gridview As DevExpress.XtraGrid.Views.Grid.GridView = sender
        'gridview.SetRowCellValue(e.RowHandle, e.Column, e.Value)
        gridview.SetRowCellValue(e.RowHandle, gridview.Columns("isDirty"), True)
        Return
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If (_presenter.Restore()) Then
            GetData()
            GridControl1.DataSource = myLociData
            MessageBox.Show("Base Rules Restored")
        End If
    End Sub
End Class