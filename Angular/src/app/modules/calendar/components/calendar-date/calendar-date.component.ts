import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CalendarDay } from '../../models/calendar-day.model';
import { EventDialogComponent } from '../event-dialog/event-dialog.component';

@Component({
  selector: 'app-calendar-date',
  templateUrl: './calendar-date.component.html',
  styleUrls: ['./calendar-date.component.scss']
})
export class CalendarDateComponent implements OnInit {

  @Input() day: CalendarDay;

  constructor(public matDialog: MatDialog) { }

  ngOnInit(): void {
  }

  /**
   * Determines whether the date should be hidden form the user interface
   * (padding days exist so that real calendar days are correctly aligned based
   * on the start of the week).
   */
  isDayHidden(): boolean {
    return this.day.isPaddingDay;
  }

  /**
   * Gets the day of the month for the date.
   */
  getDayOfMonth(): string {
    if (this.day.date != null) {
      return this.day.date.getDate().toString();
    }

    return '';
  }

  /**
   * Gets the name of the day (e.g. Monday) for the date.
   */
  getDayName(): string {
    if (this.day.date != null) {
      return this.day.date.toLocaleDateString('default', { weekday: 'long' });
    }

    return '';
  }

  /**
   * Triggered when an event on the day is clicked by the user.
   * @param calendarEventId The calender event ID of the clicked event.
   */
  onEventClicked(calendarEventId: number): void {

    // Supply the ID of the calendar event to the dialog.
    this.matDialog.open(
      EventDialogComponent,
      {
        data: { calendarEventId },
        width: '400px',
      });
  }

}
