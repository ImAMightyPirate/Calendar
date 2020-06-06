import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DateService } from './date.service';
import { CalendarWeek } from '../models/calendar-week.model';
import { CalendarEvent } from '../models/calendar-event.model';

const baseUrl = 'https://localhost:5001/v1.0/calendar/events/';

export interface EventResponse {
  id: number;
  summary: string;
  location: string;
  startDate: string;
  endDate: string;
}

@Injectable({
  providedIn: 'root'
})
export class CalendarEventService {

  constructor(
    private http: HttpClient,
    private dateService: DateService) { }

  public getEventsForMonth(date: Date): CalendarWeek[] {

    // Get the calendar weeks in the month
    const calendarWeeks = this.dateService.getCalendarWeeksForMonth(date);

    // Get the month and year
    const year = this.dateService.getYear(date);
    const month = this.dateService.getMonth(date);

    const fullUrl = baseUrl + year + '/' + month;

    this.http
      .get<EventResponse[]>(fullUrl)
      .subscribe(events =>
        {
          console.log(events);

          for (const calendarWeek of calendarWeeks) {
            for (const day of calendarWeek.daysInWeek) {
              if (day.isPaddingDay) {
                continue;
              }
              for (const event of events) {
                if (this.isDateInRange(day.date, event.startDate, event.endDate)) {
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

  private isDateInRange(calendarDate: Date, eventStartDate: string, eventEndDate: string): boolean {

    const calendarTime = calendarDate.getTime();
    const eventStartTime = new Date(eventStartDate).getTime();
    const eventEndTime = new Date(eventEndDate).getTime();

    return eventStartTime <= calendarTime && eventEndTime >= calendarTime;
  }
}
