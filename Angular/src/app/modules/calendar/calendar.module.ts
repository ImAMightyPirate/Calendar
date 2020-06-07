import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule} from '@angular/material/form-field';
import { MatToolbarModule} from '@angular/material/toolbar';
import { MonthPickerComponent } from './components/month-picker/month-picker.component';
import { CalendarDateComponent } from './components/calendar-date/calendar-date.component';
import { MonthGridComponent } from './components/month-grid/month-grid.component';
import { WeekGridComponent } from './components/week-grid/week-grid.component';
import { EventDialogComponent } from './components/event-dialog/event-dialog.component';
import { CalendarMonthComponent } from './components/calendar-month/calendar-month.component';

@NgModule({
  declarations: [
    MonthPickerComponent,
     CalendarDateComponent,
     MonthGridComponent,
     WeekGridComponent,
     EventDialogComponent,
     CalendarMonthComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatCardModule,
    MatDatepickerModule,
    MatDialogModule,
    MatFormFieldModule,
    MatToolbarModule,
  ],
  exports: [
    MonthPickerComponent,
    MonthGridComponent,
  ]
})
export class CalendarModule { }
