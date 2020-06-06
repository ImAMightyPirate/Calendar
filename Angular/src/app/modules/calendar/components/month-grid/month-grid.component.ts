import { Component, OnInit } from '@angular/core';
import { MonthPickerService } from './../../services/month-picker.service';
import { CalendarWeek } from '../../models/calendar-week.model';
import { CalendarEventService } from '../../services/calendar-event.service';

@Component({
  selector: 'app-month-grid',
  templateUrl: './month-grid.component.html',
  styleUrls: ['./month-grid.component.scss']
})
export class MonthGridComponent implements OnInit {

  calendarWeeks: CalendarWeek[];

  constructor(
    private calendarEventService: CalendarEventService,
    private monthPickerService: MonthPickerService) {

        // Set the month contents using the current active date
        const initialActiveDate = this.monthPickerService.getActiveDate();
        this.calendarWeeks = this.calendarEventService.getEventsForMonth(initialActiveDate);

        // Subscribe to changes in the active date and update contents of
        // the month
        this.monthPickerService.activeDateChanged$.subscribe(
          activeDate => {
            this.calendarWeeks = this.calendarEventService.getEventsForMonth(activeDate);
          });
   }

  ngOnInit(): void {
  }

}
