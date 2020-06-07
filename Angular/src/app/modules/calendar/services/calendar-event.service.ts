import { Injectable } from '@angular/core';
import { CalendarApiService } from './calendar-api.service';
import { DateService } from './date.service';
import { MonthPickerService } from './month-picker.service';
import { CalendarWeek } from '../models/calendar-week.model';
import { CalendarEvent } from '../models/calendar-event.model';
import { EventDetailedResponse } from './../models/event-detailed.response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CalendarEventService {

  constructor(
    private calendarApiService: CalendarApiService,
    private dateService: DateService,
    private monthPickerService: MonthPickerService) { }

  /**
   * Gets the calendar events for a given month.
   * @param date A date that falls within the month to be evaluated.
   */
  public getCalendarEventsForMonth(date: Date): CalendarWeek[] {

    // Get the calendar weeks in the month
    const calendarWeeks = this.dateService.getCalendarWeeksForMonth(date);

    this.calendarApiService
      .getCalendarEventsForMonth(date)
      .subscribe(events => {

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
   * Gets the calendar event using the given ID.
   * @param calendarEventId The ID of the calendar event.
   */
  public getCalendarEventForId(calendarEventId: number): Observable<EventDetailedResponse> {

    return this.calendarApiService.getCalendarEventForId(calendarEventId);
  }

  /**
   * Creates a new calendar event.
   * @param requestId A GUID to uniquely identify the request.
   * @param summary The summary.
   * @param location The location.
   * @param startDate The start date.
   * @param endDate The end date.
   */
  public createCalendarEvent(
    requestId: string,
    summary: string,
    location: string,
    startDate: Date,
    endDate: Date): void {

    console.log(requestId);

    const createEventRequest = {
      requestId,
      summary,
      location,
      startDate: this.getJsonDateStr(startDate),
      endDate: this.getJsonDateStr(endDate)
    };

    console.log(createEventRequest);

    this.calendarApiService
      .createCalendarEvent(createEventRequest)
      .subscribe(x => {
        this.monthPickerService.forceRefresh();
      });
  }

  /**
   * Updates an existing calendar event using the given ID.
   * @param calendarEventId The ID of the calendar event.
   * @param summary The summary.
   * @param location The location.
   * @param startDate The start date.
   * @param endDate The end date.
   */
  public updateCalendarEventForId(
    calendarEventId: number,
    summary: string,
    location: string,
    startDate: Date,
    endDate: Date): void {

    const updateEventRequest = {
      summary,
      location,
      startDate: this.getJsonDateStr(startDate),
      endDate: this.getJsonDateStr(endDate)
    };

    this.calendarApiService
      .updateCalendarEventForId(calendarEventId, updateEventRequest)
      .subscribe(x => {
        this.monthPickerService.forceRefresh();
      });
  }

  /**
   * Deletes the calendar event using the given ID.
   * @param calendarEventId The ID of the calendar event.
   */
  public deleteCalendarEventForId(calendarEventId: number): void {

    this.calendarApiService
      .deleteCalendarEventForId(calendarEventId)
      .subscribe(x => {
        this.monthPickerService.forceRefresh();
      });
  }

  private getJsonDateStr(date: Date) {

    const year = date.getFullYear();
    const month = (date.getMonth() < 9 ? '0' : '') + (date.getMonth() + 1);
    const day = (date.getDate() < 10 ? '0' : '') + date.getDate();
    return year + '-' + month + '-' + day;
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
