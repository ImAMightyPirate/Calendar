// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts
{
    using System.Collections.Generic;
    using AmHaulage.DomainObjects;

    public interface IEventReaderService
    {
        CalendarEventDO GetCalendarEvent(long calendarEventId);
        
        IEnumerable<CalendarEventDO> GetCalendarEvents(int year, int month);
    }
}
