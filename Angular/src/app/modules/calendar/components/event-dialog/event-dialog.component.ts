import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-event-dialog',
  templateUrl: './event-dialog.component.html',
  styleUrls: ['./event-dialog.component.scss']
})
export class EventDialogComponent implements OnInit {

  summary: string;
  location: string;
  startDate: Date;
  endDate: Date;

  constructor() { }

  ngOnInit(): void {
  }

  getDialogTitle(): string {
    return 'New Appointment';
  }

}
