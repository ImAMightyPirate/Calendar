import { NgModule } from '@angular/core';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { Routes, RouterModule } from '@angular/router';
import { CalendarMonthComponent } from './modules/calendar/components/calendar-month/calendar-month.component';


const routes: Routes = [
  { path: 'calendar', component: CalendarMonthComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    {provide: MAT_DATE_LOCALE, useValue: 'en-GB'},
  ],
})
export class AppRoutingModule { }
