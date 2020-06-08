// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts
{
    using AmHaulage.Services.Contracts.Exceptions;

    /// <summary>
    /// Contract for the event deleter service.
    /// </summary>
    public interface IEventDeleterService
    {
        /// <summary>
        /// Deletes an existing calendar event (logical delete, not physical, by applying deletion flag).
        /// </summary>
        /// <param name="calendarEventId">The calendar event ID.</param>
        /// <exception cref="RecordNotFoundException">Exception thrown when calendar event for ID does not exist in the database.</exception>
        void DeleteCalendarEvent(long calendarEventId);
    }
}
