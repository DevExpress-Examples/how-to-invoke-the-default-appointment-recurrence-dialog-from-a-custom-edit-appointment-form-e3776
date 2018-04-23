using System;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler;
using DevExpress.Xpf.Scheduler.UI;

namespace SilverlightApplication1 {
    public partial class CustomAppointmentForm : UserControl {
        SchedulerControl control;
        CustomAppointmentFormController controller;
        Appointment apt;

        public CustomAppointmentForm(SchedulerControl control, Appointment apt) {
            if (control == null || apt == null)
                throw new ArgumentNullException("control");
            if (control == null || apt == null)
                throw new ArgumentNullException("apt");

            this.control = control;
            this.controller = new CustomAppointmentFormController(control, apt);
            this.apt = apt;

            InitializeComponent();
        }

        public CustomAppointmentFormController Controller { get { return controller; } }
        public SchedulerControl Control { get { return control; } }
        public Appointment Appointment { get { return apt; } }
        public string TimeEditMask {get { return CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern; }}

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            if (Controller.IsNewAppointment)
                SchedulerFormBehavior.SetTitle(this, "New appointment");
            else
                SchedulerFormBehavior.SetTitle(this, "Edit - [" + Appointment.Subject + "]");
        }

        private void Ok_button_Click(object sender, RoutedEventArgs e) {
            // Save all changes of the editing appointment.            
            Controller.Control.Storage.BeginUpdate();
            Controller.ApplyChanges();
            Controller.Control.Storage.EndUpdate();
            SchedulerFormBehavior.Close(this, true);
        }

        private void Cancel_button_Click(object sender, RoutedEventArgs e) {
            SchedulerFormBehavior.Close(this, false);
        }

        #region #ShowRecurrenceForm
        private void Recurrence_button_Click(object sender, RoutedEventArgs e) {
            control.ShowRecurrenceForm(this, false);
        }
        #endregion #ShowRecurrenceForm
    }

    public class CustomAppointmentFormController : AppointmentFormController {
        public CustomAppointmentFormController(SchedulerControl control, Appointment apt)
            : base(control, apt) {
        }
        public string Contact {
            get { return GetContactValue(EditedAppointmentCopy); }
            set { EditedAppointmentCopy.CustomFields["Contact"] = value; }
        }
        string SourceContact {
            get { return GetContactValue(SourceAppointment); }
            set { SourceAppointment.CustomFields["Contact"] = value; }
        }
        public override bool IsAppointmentChanged() {
            if (base.IsAppointmentChanged())
                return true;
            return SourceContact != Contact;
        }

        protected string GetContactValue(Appointment apt) {
            return Convert.ToString(apt.CustomFields["Contact"]);
        }

        protected override void ApplyCustomFieldsValues() {
            SourceContact = Contact;
        }

        protected override void ApplyChangesCore() {
            base.ApplyChangesCore();
        }
    }
}
