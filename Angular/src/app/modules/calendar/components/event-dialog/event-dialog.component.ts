import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CalendarEventService } from '../../services/calendar-event.service';
import { v1 as uuidv1 } from 'uuid';

export interface EventDialogData {
  calendarEventId?: number;
}

@Component({
  selector: 'app-event-dialog',
  templateUrl: './event-dialog.component.html',
})
export class EventDialogComponent implements OnInit {

  calendarEventId?: number;

  requestId: string;

  dialogTitle: string;
  isUpdateOnSave: boolean;
  isDeleteButtonVisible: boolean;

  summary = new FormControl('', [Validators.required]);
  location = new FormControl('', [Validators.required]);
  startDate = new FormControl('', [Validators.required]);
  endDate = new FormControl('', [Validators.required]);

  eventForm: FormGroup = new FormGroup({
    summary: this.summary,
    location: this.location,
    startDate: this.startDate,
    endDate: this.endDate
  });

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: EventDialogData,
    private calendarEventService: CalendarEventService) {

    this.calendarEventId = data?.calendarEventId;

    // Generate a unique GUID each time the dialog is opened
    // (this is used to prevent certain actions from being executed twice
    // if a user double clicks)
    this.requestId = uuidv1();

    if (this.calendarEventId != null) {

      // Calendar event ID supplied, context is editing an existing event
      this.dialogTitle = 'Edit Appointment';
      this.isUpdateOnSave = true;
      this.isDeleteButtonVisible = true;

      // Retrieve the properties of the existing calendar event
      // and load them in to the form
      this.calendarEventService
        .getCalendarEventForId(this.calendarEventId)
        .subscribe(event =>
          {
            this.summary.setValue(event.summary);
            this.location.setValue(event.location);
            this.startDate.setValue(new Date(event.startDate));
            this.endDate.setValue(new Date(event.endDate));
          });
    }
    else {

      // Calendar event ID not supplied, context is creating a new event
      this.dialogTitle = 'New Appointment';
      this.isUpdateOnSave = false;
      this.isDeleteButtonVisible = false;
    }
  }

  ngOnInit(): void {
  }

  /**
   * Triggered when the user changes the start date. Ensures that
   * the end date is not before the start date.
   */
  onStartDateChanged(): void {

    // Force the end date to be cleared it if occurs before the start date
    if (this.endDate.value == null) {
      return;
    }

    if (this.endDate.value < this.startDate.value) {
      this.endDate.setValue(null);
    }
  }

  /**
   * Triggered when the user clicks the save button. Ensures the correct
   * action is being performed depending on whether a new record is being
   * created or an existing record is being edited.
   */
  onSaveClick(): void {

    if (this.isUpdateOnSave) {
      this.calendarEventService.updateCalendarEventForId(
        this.calendarEventId,
        this.summary.value,
        this.location.value,
        new Date(this.startDate.value),
        new Date(this.endDate.value));
    }
    else {
      this.calendarEventService.createCalendarEvent(
        this.requestId,
        this.summary.value,
        this.location.value,
        new Date(this.startDate.value),
        new Date(this.endDate.value));
    }
  }

  /**
   * Triggered when the user clicks the delete button.
   */
  onDeleteClick(): void {

    this.calendarEventService.deleteCalendarEventForId(this.calendarEventId);
  }
}
