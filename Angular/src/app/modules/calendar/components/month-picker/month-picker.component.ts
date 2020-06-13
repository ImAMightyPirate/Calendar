import { Component, OnInit } from '@angular/core';
import { MonthPickerService } from './../../services/month-picker.service';

@Component({
  selector: 'app-month-picker',
  templateUrl: './month-picker.component.html',
  styleUrls: ['./month-picker.component.scss']
})
export class MonthPickerComponent implements OnInit {

  activeMonth: string;

  constructor(private monthPickerService: MonthPickerService) {

    // Set the active month using the current active date
    const initialActiveDate = this.monthPickerService.getActiveDate();
    this.activeMonth = this.getFormattedDisplayDate(initialActiveDate);

    // Subscribe to changes in the active date and update the active
    // month
    this.monthPickerService.activeDateChanged$.subscribe(
      activeDate => {
        this.activeMonth = this.getFormattedDisplayDate(activeDate);
      });

   }

  ngOnInit(): void {
  }

  /**
   * Show the previous month.
   */
  showPreviousMonth(): void {
    this.monthPickerService.setPreviousMonthActive();
  }

  /**
   * Show the next month.
   */
  showNextMonth(): void {
    this.monthPickerService.setNextMonthActive();
  }

  /**
   * Returns the month and year of the date as a formatted string (based on rules for the current locale).
   * @param date The date object to format.
   */
  private getFormattedDisplayDate(date: Date): string {
    return date.toLocaleString('default', { year: 'numeric', month: 'long' });
  }
}
