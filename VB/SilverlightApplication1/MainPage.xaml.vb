Imports System
Imports System.Windows.Controls
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler

Partial Public Class MainPage
    Inherits UserControl

    Public Sub New()
        InitializeComponent()

        schedulerControl1.Start = New DateTime(2010, 7, 19, 0, 0, 0, 0)
    End Sub

    ' Commit changes to the database.
    Private Sub SchedulerStorage_AppointmentCollectionModified(sender As Object, e As EventArgs)
        If domainDataSource1.HasChanges Then
            domainDataSource1.SubmitChanges()
        End If
    End Sub

    Private Sub DomainDataSource_LoadedData(sender As Object, _
                                            e As LoadedDataEventArgs)
        If e.HasError Then
            MessageBox.Show("Connection could not be established." _
                            & Environment.NewLine & e.Error.Message, "Connection Error", _
                            MessageBoxButton.OK)
            e.MarkErrorAsHandled()
        End If
    End Sub

    Private Sub DomainDataSource_SubmittedChanges(sender As Object, e As SubmittedChangesEventArgs)
        If e.HasError Then
            MessageBox.Show(e.Error.ToString(), "Submit Changes Error", MessageBoxButton.OK)
            e.MarkErrorAsHandled()
        End If
    End Sub

    Private Sub schedulerControl1_EditAppointmentFormShowing(sender As Object, _
                                                             e As EditAppointmentFormEventArgs)
        e.Form = New CustomAppointmentForm(Me.schedulerControl1, e.Appointment)
        e.AllowResize = False
    End Sub

#Region "#RecurrenceFormShowing"
    Private Sub schedulerControl1_RecurrenceFormShowing(sender As Object, e As RecurrenceFormEventArgs)
        Dim customForm As CustomAppointmentForm = TryCast(e.ParentForm, CustomAppointmentForm)
        e.Controller = customForm.Controller
    End Sub
#End Region
End Class

