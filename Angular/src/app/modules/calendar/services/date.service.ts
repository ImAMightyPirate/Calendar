import { Injectable } from '@angular/core';
import { CalendarWeek } from '../models/calendar-week.model';
import { CalendarDay } from '../models/calendar-day.model';

/**
 * Last day of week is Sunday (zero).
 */
const lastDayOfWeek = 0;

@Injectable({
  providedIn: 'root'
})
export class DateService {

  constructor() { }

  /**
   * Gets the year from the date.
   * @param date The date to extract the year from.
   */
  public getYear(date: Date): number {
    return date.getFullYear();
  }

  /**
   * Gets the month from the date (indexed by 1, not 0 as is the JavaScript default).
   * @param date The date to extract the month from.
   */
  public getMonth(date: Date): number {
    return date.getMonth() + 1;
  }

  /**
   * Gets the calendar weeks for a month.
   * @param date A date that falls within the month to be evaluated.
   */
  public getCalendarWeeksForMonth(date: Date): CalendarWeek[] {

    const calendarWeeks = new Array<CalendarWeek>();
    const numberOfDaysInMonth = this.getNumberOfDaysInMonth(date);

    let currentWeek = new CalendarWeek();

    // For every day in the month
    for (let day = 1; day <= numberOfDaysInMonth; day++) {
      const currentDate = new Date(date.getFullYear(), date.getMonth(), day);

      // Create a calendar day and add it to the week
      const calendarDay = new CalendarDay();
      calendarDay.date = currentDate;
      currentWeek.daysInWeek.push(calendarDay);

      // Create a new week when the end of the end the week is reached
      if (this.isLastDayOfWeek(currentDate)) {
        calendarWeeks.push(currentWeek);
        currentWeek = new CalendarWeek();
      }
    }

    // Only add the last week if it contains any days
    if (currentWeek.daysInWeek.length > 0) {
      calendarWeeks.push(currentWeek);
    }

    this.padFirstWeek(calendarWeeks);
    this.padLastWeek(calendarWeeks);

    return calendarWeeks;
  }

  /**
   * Gets the number of days in a month.
   * @param date A date that falls within the month to be evaluated.
   */
  private getNumberOfDaysInMonth(date: Date): number {
    // Use day zero of the next month, this retrieves
    // the last day of the current month.
    // Note month + 1 always work because month 13 is
    // treated as month of 1 the following year by JavaScript.
    const nextMonth = date.getMonth() + 1;
    const lastDayOfCurrentMonth = new Date(date.getFullYear(), nextMonth, 0);
    return lastDayOfCurrentMonth.getDate();
  }

  /**
   * Determines whether the date is the last day of the week.
   * @param date The date to evaluate.
   */
  private isLastDayOfWeek(date: Date): boolean {
    return date.getDay() === lastDayOfWeek;
  }

  /**
   * Ensures that the first week of the month has 7 days.
   * @param calendarWeeks The calendar weeks to pad.
   */
  private padFirstWeek(calendarWeeks: CalendarWeek[]): void {

    const firstWeek = calendarWeeks[0];
    const missingDays = 7 - firstWeek.daysInWeek.length;

    if (missingDays === 0) {
      return;
    }

    const paddingDay = new CalendarDay();
    paddingDay.isPaddingDay = true;

    const paddingDays = new Array<CalendarDay>(missingDays)
      .fill(paddingDay);

    // Pad the start of the week so it is 7 days in length
    const completeDays = paddingDays.concat(firstWeek.daysInWeek);
    firstWeek.daysInWeek = completeDays;
  }

  /**
   * Ensures that the last week of the month has 7 days.
   * @param calendarWeeks The calendar weeks to pad.
   */
  private padLastWeek(calendarWeeks: CalendarWeek[]): void {

    const lastWeek = calendarWeeks[calendarWeeks.length - 1];
    const missingDays = 7 - lastWeek.daysInWeek.length;

    if (missingDays === 0) {
      return;
    }

    const paddingDay = new CalendarDay();
    paddingDay.isPaddingDay = true;

    const paddingDays = new Array<CalendarDay>(missingDays)
      .fill(paddingDay);

    // Pad the start of the week so it is 7 days in length
    const completeDays = lastWeek.daysInWeek.concat(paddingDays);
    lastWeek.daysInWeek = completeDays;
  }
}
