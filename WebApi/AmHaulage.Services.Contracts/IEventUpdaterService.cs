// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts
{
    using System;
    using AmHaulage.Services.Contracts.Exceptions;

    /// <summary>
    /// Contract for the event updater service.
    /// </summary>
    public interface IEventUpdaterService
    {
        /// <summary>
        /// Updates an existing calendar event with a new summary, location, start date and/or end date.
        /// </summary>
        /// <param name="calendarEventId">The calendar event ID.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="location">The location.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <exception cref="RecordNotFoundException">Exception thrown when calendar event for ID does not exist in the database.</exception>
        void UpdateCalendarEvent(long calendarEventId, string summary, string location, DateTime startDate, DateTime endDate);
    }
}
