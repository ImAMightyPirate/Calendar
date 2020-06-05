// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts
{
    using System;

    public interface IEventUpdaterService
    {
        void UpdateCalendarEvent(long calendarEventId, string summary, string location, DateTime startDate, DateTime endDate);
    }
}
