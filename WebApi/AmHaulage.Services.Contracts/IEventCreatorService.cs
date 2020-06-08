// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts
{
    using AmHaulage.DomainObjects;
    using AmHaulage.Services.Contracts.Exceptions;

    /// <summary>
    /// Contract for the event creator service.
    /// </summary>
    public interface IEventCreatorService
    {
        /// <summary>
        /// Creates a new calendar event.
        /// </summary>
        /// <param name="calendarEvent">The calendar event details.</param>
        /// <exception cref="DuplicateRequestException">Exception thrown when the request has been made multiple times.</exception>
        void CreateCalendarEvent(CalendarEventDO calendarEvent);
    }
}
