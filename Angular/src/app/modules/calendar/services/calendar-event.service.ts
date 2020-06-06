import { Injectable } from '@angular/core';
import { CalendarApiService } from './calendar-api.service';
import { DateService } from './date.service';
import { CalendarWeek } from '../models/calendar-week.model';
import { CalendarEvent } from '../models/calendar-event.model';

@Injectable({
  providedIn: 'root'
})
export class CalendarEventService {

  constructor(
    private calendarApiService: CalendarApiService,
    private dateService: DateService) { }

  /**
   * Gets the calendar events for a given month.
   * @param date A date that falls within the month to be evaluated.
   */
  public getCalendarEventsForMonth(date: Date): CalendarWeek[] {

    // Get the calendar weeks in the month
    const calendarWeeks = this.dateService.getCalendarWeeksForMonth(date);

    this.calendarApiService
      .getCalendarEventsForMonth(date)
      .subscribe(events =>
        {
          for (const calendarWeek of calendarWeeks) {
            for (const day of calendarWeek.daysInWeek) {

              // No matching required if the day in the calendar is only
              // provided for padding
              if (day.isPaddingDay) {
                continue;
              }

              for (const event of events) {
                if (this.isCalendarDateInRange(day.date, event.startDate, event.endDate)) {
                  const calendarEvent = new CalendarEvent();
                  calendarEvent.id = event.id;
                  calendarEvent.summary = event.summary;
                  calendarEvent.location = event.location;
                  day.eventsOnDay.push(calendarEvent);
                }
              }
            }
          }
        });

    return calendarWeeks;
  }

  /**
   * Determines whether the calendar date falls within the range of an event's date range.
   * @param calendarDate The calendar date.
   * @param eventStartDate The event start date.
   * @param eventEndDate The event end date.
   */
  private isCalendarDateInRange(calendarDate: Date, eventStartDate: string, eventEndDate: string): boolean {

    const calendarTime = calendarDate.getTime();
    const eventStartTime = new Date(eventStartDate).getTime();
    const eventEndTime = new Date(eventEndDate).getTime();

    return eventStartTime <= calendarTime && eventEndTime >= calendarTime;
  }
}
