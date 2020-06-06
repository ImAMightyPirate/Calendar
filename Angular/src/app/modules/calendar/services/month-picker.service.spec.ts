import { TestBed } from '@angular/core/testing';

import { MonthPickerService } from './month-picker.service';

describe('MonthPickerService', () => {
  let service: MonthPickerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MonthPickerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
