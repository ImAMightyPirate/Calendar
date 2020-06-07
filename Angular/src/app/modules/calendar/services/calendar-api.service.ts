import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DateService } from './date.service';

const readCalendarEventsUrl = 'https://localhost:5001/v1.0/calendar/events/';

export interface EventSummaryResponse {
  id: number;
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

    return this.http
      .get<EventSummaryResponse[]>(readCalendarEventsUrl + year + '/' + month);
  }
}
