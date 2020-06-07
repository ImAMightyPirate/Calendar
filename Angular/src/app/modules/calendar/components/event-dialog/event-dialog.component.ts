import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface EventDialogData {
  calendarEventId?: number;
}

@Component({
  selector: 'app-event-dialog',
  templateUrl: './event-dialog.component.html',
})
export class EventDialogComponent implements OnInit {

  dialogTitle: string;
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

  constructor(@Inject(MAT_DIALOG_DATA) public data: EventDialogData) {

    if (data?.calendarEventId != null) {
      this.dialogTitle = 'Edit Appointment';
      this.isDeleteButtonVisible = true;
    }
    else {
      this.dialogTitle = 'New Appointment';
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

  onSave(): void {
    console.log('Saving...');
  }
}
