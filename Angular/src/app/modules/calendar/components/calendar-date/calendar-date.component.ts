import { Component, Input, OnInit } from '@angular/core';
import { CalendarDay } from '../../models/calendar-day.model';

@Component({
  selector: 'app-calendar-date',
  templateUrl: './calendar-date.component.html',
  styleUrls: ['./calendar-date.component.scss']
})
export class CalendarDateComponent implements OnInit {

  @Input() day: CalendarDay;

  constructor() { }

  ngOnInit(): void {
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

}
