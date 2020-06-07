import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EventDialogComponent } from '../event-dialog/event-dialog.component';

@Component({
  selector: 'app-calendar-month',
  templateUrl: './calendar-month.component.html',
  styleUrls: ['./calendar-month.component.scss']
})
export class CalendarMonthComponent implements OnInit {

  constructor(public matDialog: MatDialog) { }

  ngOnInit(): void {
  }

  /**
   * Triggered the the user clicks on the new appointment button.
   */
  public onNewAppointmentClick(): void {

    // Supply a null ID for the calendar event to the dialog
    // (as there is none yet in the context of a new event).
    this.matDialog.open(
      EventDialogComponent,
      {
        data: { calendarEventId: null },
        width: '400px',
      });
  }

}
