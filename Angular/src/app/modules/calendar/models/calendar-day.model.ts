import { CalendarEvent } from './calendar-event.model';

export class CalendarDay {

  /** Indicates whether the calendar day is a "padding" day (i.e. not real) used to ensure correct alignment. */
  public isPaddingDay: boolean;

  /** The date this calendar day represents (null if this is a padding day). */
  public date?: Date;

  /** The events that occur on this day of the calendar. */
  public eventsOnDay: CalendarEvent[];

  constructor() {
    this.eventsOnDay = new Array<CalendarEvent>();
  }
}
