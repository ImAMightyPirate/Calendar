// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Persistence.Contracts
{
    using System;
    using System.Linq;
    using AmHaulage.Persistence.Contracts.Entities;

    /// <summary>
    /// Contract for the repository.
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Gets the calendar events from the repository.
        /// </summary>
        IQueryable<CalendarEvent> CalendarEvents { get; }

        /// <summary>
        /// Adds a calendar event to the repository.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        void AddCalendarEvent(CalendarEvent entity);

        /// <summary>
        /// Updates a calendar event in the repository.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void UpdateCalendarEvent(CalendarEvent entity);

        /// <summary>
        /// Save all changes to the database.
        /// </summary>
        void SaveChanges();
    }
}