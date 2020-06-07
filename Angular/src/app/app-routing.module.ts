import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CalendarMonthComponent } from './modules/calendar/components/calendar-month/calendar-month.component';


const routes: Routes = [
  { path: 'calendar', component: CalendarMonthComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
