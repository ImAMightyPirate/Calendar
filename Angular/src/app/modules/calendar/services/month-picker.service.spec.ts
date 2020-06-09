import { TestBed } from '@angular/core/testing';

import { MonthPickerService } from './month-picker.service';
import { DateService } from './date.service';

/**
 * Mock date service that always returns January 2020.
 */
class MockedDateService {

  constructor() { }

  public getYear(date: Date): number {
    return 2020;
  }

  public getMonth(date: Date): number {
    return 1; // January (indexed by one in services)
  }
}

describe('MonthPickerService', () => {
  let service: MonthPickerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        { provide: DateService, useClass: MockedDateService },
      ]});

    service = TestBed.inject(MonthPickerService);
  });

  it('getActiveDate() should return active date by default (mocked to January 2020)', () => {
    const activeDate = service.getActiveDate();
    expect(activeDate.getFullYear()).toBe(2020);
    expect(activeDate.getMonth()).toBe(0); // January = 0 (indexed by zero in JavaScript)
  });

  it('setPreviousMonthActive() should set active date to December 2019', () => {
    service.setPreviousMonthActive();

    // Retrieve active date to evaluate
    const activeDate = service.getActiveDate();
    expect(activeDate.getFullYear()).toBe(2019);
    expect(activeDate.getMonth()).toBe(11); // December = 11 (indexed by zero in JavaScript)
  });

  it('setNextMonthActive() should set active date to Feburary 2020', () => {
    service.setNextMonthActive();

    // Retrieve active date to evaluate
    const activeDate = service.getActiveDate();
    expect(activeDate.getFullYear()).toBe(2020);
    expect(activeDate.getMonth()).toBe(1); // February = 1 (indexed by zero in JavaScript)
  });
});
