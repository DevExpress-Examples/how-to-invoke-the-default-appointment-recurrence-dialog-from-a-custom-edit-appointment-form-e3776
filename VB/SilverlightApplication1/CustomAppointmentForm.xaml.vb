Imports Microsoft.VisualBasic
Imports System
Imports System.Globalization
Imports System.Windows.Controls
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.Xpf.Scheduler.UI

Partial Public Class CustomAppointmentForm
    Inherits UserControl
    Private control_Renamed As SchedulerControl
    Private controller_Renamed As CustomAppointmentFormController
    Private apt As Appointment

    Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment)

        If control Is Nothing OrElse apt Is Nothing Then
            Throw New ArgumentNullException("control")
        End If
        If control Is Nothing OrElse apt Is Nothing Then
            Throw New ArgumentNullException("apt")
        End If

        Me.control_Renamed = control
        Me.controller_Renamed = New CustomAppointmentFormController(control, apt)
        Me.apt = apt

        InitializeComponent()
    End Sub

    Public ReadOnly Property Controller() As CustomAppointmentFormController
        Get
            Return controller_Renamed
        End Get
    End Property
    Public ReadOnly Property Control() As SchedulerControl
        Get
            Return control_Renamed
        End Get
    End Property
    Public ReadOnly Property Appointment() As Appointment
        Get
            Return apt
        End Get
    End Property
    Public ReadOnly Property TimeEditMask() As String
        Get
            Return CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern
        End Get
    End Property

    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
        If Controller.IsNewAppointment Then
            SchedulerFormBehavior.SetTitle(Me, "New appointment")
        Else
            SchedulerFormBehavior.SetTitle(Me, "Edit - [" & Appointment.Subject & "]")
        End If
    End Sub

    Private Sub Ok_button_Click(sender As Object, e As RoutedEventArgs)
        ' Save all changes of the editing appointment.            
        Controller.Control.Storage.BeginUpdate()
        Controller.ApplyChanges()
        Controller.Control.Storage.EndUpdate()
        SchedulerFormBehavior.Close(Me, True)
    End Sub

    Private Sub Cancel_button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        SchedulerFormBehavior.Close(Me, False)
    End Sub

#Region "#ShowRecurrenceForm"
    Private Sub Recurrence_button_Click(sender As Object, e As RoutedEventArgs)
        control_Renamed.ShowRecurrenceForm(Me, False)
    End Sub
#End Region
End Class

Public Class CustomAppointmentFormController
    Inherits AppointmentFormController
    Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment)
        MyBase.New(control, apt)
    End Sub
    Public Property Contact() As String
        Get
            Return GetContactValue(EditedAppointmentCopy)
        End Get
        Set(ByVal value As String)
            EditedAppointmentCopy.CustomFields("Contact") = value
        End Set
    End Property
    Private Property SourceContact() As String
        Get
            Return GetContactValue(SourceAppointment)
        End Get
        Set(ByVal value As String)
            SourceAppointment.CustomFields("Contact") = value
        End Set
    End Property
    Public Overrides Function IsAppointmentChanged() As Boolean
        If MyBase.IsAppointmentChanged() Then
            Return True
        End If
        Return SourceContact <> Contact
    End Function

    Protected Function GetContactValue(ByVal apt As Appointment) As String
        Return Convert.ToString(apt.CustomFields("Contact"))
    End Function

    Protected Overrides Sub ApplyCustomFieldsValues()
        SourceContact = Contact
    End Sub
    Protected Overrides Sub ApplyChangesCore()
        MyBase.ApplyChangesCore()
    End Sub
End Class