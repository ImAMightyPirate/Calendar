import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MonthPickerComponent } from './components/month-picker/month-picker.component';
import { CalendarDateComponent } from './components/calendar-date/calendar-date.component';
import { MonthGridComponent } from './components/month-grid/month-grid.component';
import { WeekGridComponent } from './components/week-grid/week-grid.component';
import { EventDialogComponent } from './components/event-dialog/event-dialog.component';

@NgModule({
  declarations: [MonthPickerComponent, CalendarDateComponent, MonthGridComponent, WeekGridComponent, EventDialogComponent],
  imports: [
    CommonModule,
    MatButtonModule,
    MatCardModule,
  ],
  exports: [
    MonthPickerComponent,
    MonthGridComponent,
  ]
})
export class CalendarModule { }
