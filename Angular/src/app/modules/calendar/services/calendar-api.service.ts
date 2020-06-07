import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DateService } from './date.service';

const calendarEventsBaseUrl = 'https://localhost:5001/v1.0/calendar/events/';

export interface EventSummaryResponse {
  id: number;
  summary: string;
  location: string;
  startDate: string;
  endDate: string;
}

export interface EventDetailedResponse {
  id: number;
  summary: string;
  location: string;
  startDate: string;
  endDate: string;
  isDeleted: boolean;
}

export interface CreateEventRequest {
  requestId: string;
  summary: string;
  location: string;
  startDate: string;
  endDate: string;
}

export interface UpdateEventRequest {
  summary: string;
  location: string;
  startDate: string;
  endDate: string;
}

@Injectable({
  providedIn: 'root'
})
export class CalendarApiService {

  constructor(
    private http: HttpClient,
    private dateService: DateService) { }

  /**
   * Gets the calendar events for a given month.
   * @param date A date that falls within the month to be evaluated.
   */
  public getCalendarEventsForMonth(date: Date): Observable<EventSummaryResponse[]> {

    // Extract the month and year
    const year = this.dateService.getYear(date);
    const month = this.dateService.getMonth(date);

    return this.http.get<EventSummaryResponse[]>(calendarEventsBaseUrl + year + '/' + month);
  }

  /**
   * Gets the calendar event using the given ID.
   * @param calendarEventId The ID of the calendar event.
   */
  public getCalendarEventForId(calendarEventId: number): Observable<EventDetailedResponse> {

    return this.http.get<EventDetailedResponse>(calendarEventsBaseUrl + calendarEventId);
  }

  /**
   * Creates a new calendar event.
   * @param request The request body.
   */
  public createCalendarEvent(request: CreateEventRequest): Observable<any> {
    return this.http.post(calendarEventsBaseUrl, request);
  }

  /**
   * Updates an existing calendar event using the given ID.
   * @param calendarEventId The ID of the calendar event.
   * @param request The request body.
   */
  public updateCalendarEventForId(calendarEventId: number, request: UpdateEventRequest): Observable<any> {
    return this.http.put(calendarEventsBaseUrl + calendarEventId, request);
  }

  /**
   * Deletes the calendar event using the given ID.
   * @param calendarEventId The ID of the calendar event.
   */
  public deleteCalendarEventForId(calendarEventId: number): Observable<any> {
    return this.http.delete(calendarEventsBaseUrl + calendarEventId);
  }
}
