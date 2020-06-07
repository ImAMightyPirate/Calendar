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

  requestId: string;
  dialogTitle: string;
  isUpdateOnSave: boolean;
  isDeleteButtonVisible: boolean;

  calendarEventId?: number;
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

    this.requestId = uuidv1();
    this.calendarEventId = data?.calendarEventId;

    if (this.calendarEventId != null) {
      this.dialogTitle = 'Edit Appointment';
      this.isUpdateOnSave = true;
      this.isDeleteButtonVisible = true;

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
      this.dialogTitle = 'New Appointment';
      this.isUpdateOnSave = false;
      this.isDeleteButtonVisible = false;
    }
  }

  ngOnInit(): void {
  }

  onStartDateChanged(): void {

    // Force the end date to be cleared it if occurs before the start date
    if (this.endDate.value == null) {
      return;
    }

    if (this.endDate.value < this.startDate.value) {
      this.endDate.setValue(null);
    }
  }

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

  onDeleteClick(): void {

    this.calendarEventService.deleteCalendarEventForId(this.calendarEventId);
  }
}
