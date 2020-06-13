import { Component, Input, OnInit } from '@angular/core';
import { CalendarWeek } from '../../models/calendar-week.model';

@Component({
  selector: 'app-week-grid',
  templateUrl: './week-grid.component.html',
  styleUrls: ['./week-grid.component.scss']
})
export class WeekGridComponent implements OnInit {

  @Input() calendarWeek: CalendarWeek;

  constructor() { }

  ngOnInit(): void {
  }

}
