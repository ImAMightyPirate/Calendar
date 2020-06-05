// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts
{
    using AmHaulage.DomainObjects;

    public interface IEventCreatorService
    {
        void CreateCalendarEvent(CalendarEventDO calendarEvent);
    }
}
