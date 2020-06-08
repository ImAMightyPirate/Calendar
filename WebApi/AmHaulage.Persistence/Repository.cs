// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Persistence
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AmHaulage.Persistence.Contexts;
    using AmHaulage.Persistence.Contracts;
    using AmHaulage.Persistence.Contracts.Entities;

    /// <summary>
    /// Repository that acts as a wrapper around the DB context.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Repository : IRepository
    {
        private readonly AmHaulageContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository" /> class.
        /// </summary>
        public Repository()
        {
            this.context = new AmHaulageContext();
        }

        /// <summary>
        /// Gets the calendar events from the repository.
        /// </summary>
        public IQueryable<CalendarEvent> CalendarEvents => this.CalendarEvents;

        /// <summary>
        /// Adds a calendar event to the repository.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        public void AddCalendarEvent(CalendarEvent entity)
        {
            this.context.CalendarEvents.Add(entity);
        }

        /// <summary>
        /// Updates a calendar event in the repository.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        public void UpdateCalendarEvent(CalendarEvent entity)
        {
            this.context.CalendarEvents.Update(entity);
        }

        /// <summary>
        /// Save all changes to the database.
        /// </summary>
        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        /// <summary>
        /// Disposes of the repository.
        /// </summary>
        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}