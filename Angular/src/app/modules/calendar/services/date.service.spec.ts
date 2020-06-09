import { TestBed } from '@angular/core/testing';

import { DateService } from './date.service';

describe('DateService', () => {
  let service: DateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DateService);
  });

  it('getYear() should return year portion of given date', () => {
    const date = new Date(2020, 0, 1);
    expect(service.getYear(date)).toBe(2020);
  });

  it('getMonth() should return month portion of given date (with month indexed by 1)', () => {
    const date = new Date(2020, 0, 1);
    expect(service.getMonth(date)).toBe(1);
  });

  it('getCalendarWeeksForMonth() should return full 7 day weeks (regardless of actual number of days in month)', () => {
    const date = new Date(2020, 0, 1);
    const calendarWeeks = service.getCalendarWeeksForMonth(date);
    for (const calendarWeek of calendarWeeks) {
      expect(calendarWeek.daysInWeek.length).toBe(7);
    }
  });

  it('getNumberOfDaysInMonth() should return expected number of days for given month (January 2020 = 31 days)', () => {
    const date = new Date(2020, 0, 1);
    expect(service.getNumberOfDaysInMonth(date)).toBe(31);
  });
});
