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

  onEventClicked(calendarEventId: number): void {

    const dialogRef = this.matDialog.open(
      EventDialogComponent,
      {
        data: { calendarEventId },
        width: '400px',
      });
  }

}
