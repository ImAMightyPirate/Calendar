// Copyright (c) Adam Mytton. All Rights Reserved.

using System;

namespace AmHaulage.Services.Contracts
{
    public interface IEventUpdaterService
    {
        void UpdateCalendarEvent(long calendarEventId, string summary, string location, DateTime startDate, DateTime endDate);
    }
}
