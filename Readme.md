# How to invoke the default Appointment Recurrence dialog from a custom Edit Appointment form


<p>This example demonstrates how to invoke the default <strong>Appointment Recurrence</strong> dialog from your own custom <strong>Edit Appointment</strong> form.</p>


<h3>Description</h3>

<p>To invoke the default dialog to edit appointment recurrence from a custom appointment editing form, do the following:<br /> 1. On the custom <strong>Edit Appointment</strong> form locate the <strong>Recurrence</strong> button and specify its visibility via the <strong>AppointmentFormController.ShouldShowRecurrence</strong> property.<br /> 2. Call the <strong>SchedulerControl.ShowRecurrenceForm</strong> method in this button's <strong>Click</strong> event handler to invoke the <strong>Appointment Recurrence</strong> dialog when clicking the button.<br /> 3. Handle the <strong>SchedulerControl.RecurrenceFormShowing</strong> event and set the <strong>RecurrenceFormEventArgs.Controller</strong> property to the controller of the custom <strong>Edit Appointment</strong> form instance accessed via the <strong>RecurrenceFormEventArgs.ParentForm</strong> property.</p>

<br/>


