// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts
{
    public interface IEventDeleterService
    {
        void DeleteCalendarEvent(long calendarEventId);
    }
}
