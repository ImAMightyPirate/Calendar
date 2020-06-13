import { CalendarDay } from './calendar-day.model';

export class CalendarWeek {

  /** The days in the calendar week. */
  public daysInWeek: CalendarDay[];

  constructor() {
    this.daysInWeek = new Array<CalendarDay>();
  }
}
