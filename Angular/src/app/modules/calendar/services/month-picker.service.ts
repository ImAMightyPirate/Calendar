import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { DateService } from './date.service';

@Injectable({
  providedIn: 'root'
})
export class MonthPickerService {

  private activeYear: number;
  private activeMonth: number;

  private activeDateChangedSource = new Subject<Date>();
  public activeDateChanged$ = this.activeDateChangedSource.asObservable();

  constructor(private dateService: DateService) {
    const currentDate = new Date();
    this.activeYear = this.dateService.getYear(currentDate);
    this.activeMonth = this.dateService.getMonth(currentDate);
   }

  /**
   * Gets the active date.
   */
  public getActiveDate(): Date {
    return new Date(this.activeYear, this.activeMonth - 1);
  }

  /**
   * Sets the previous month as the active date.
   */
  public setPreviousMonthActive(): void {

    if (this.activeMonth === 1) {
      this.activeMonth = 12;
      this.activeYear--;
    }
    else {
      this.activeMonth--;
    }

    // Notify any subscribers that the active date has changed
    this.activeDateChangedSource.next(this.getActiveDate());
  }

  /**
   * Sets the next month as the active date.
   */
  public setNextMonthActive(): void {

    if (this.activeMonth === 12) {
      this.activeMonth = 1;
      this.activeYear++;
    }
    else {
      this.activeMonth++;
    }

    // Notify any subscribers that the active date has changed
    this.activeDateChangedSource.next(this.getActiveDate());
  }
}
