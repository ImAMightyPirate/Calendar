// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services.Contracts
{
    using System.Collections.Generic;
    using AmCalendar.DomainObjects;
    using AmCalendar.Services.Contracts.Exceptions;

    /// <summary>
    /// Contract for the event reader service.
    /// </summary>
    public interface IEventReaderService
    {
        /// <summary>
        /// Gets a calendar event by its ID.
        /// </summary>
        /// <param name="calendarEventId">The calendar event ID.</param>
        /// <returns>The calendar event.</returns>
        /// <exception cref="RecordNotFoundException">Thrown if no calendar event for the ID is found.</exception>
        CalendarEventDO GetCalendarEvent(long calendarEventId);
    }
}
