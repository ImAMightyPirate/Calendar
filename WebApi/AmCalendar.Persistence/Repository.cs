// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Persistence
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AmCalendar.Persistence.Contexts;
    using AmCalendar.Persistence.Contracts;
    using AmCalendar.Persistence.Contracts.Entities;

    /// <summary>
    /// The repository class acts as a thin wrapper around the
    /// EF Core database context so that it can be substituted
    /// with a mock in the unit tests.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Repository : IRepository
    {
        private readonly AmCalendarContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository" /> class.
        /// </summary>
        public Repository()
        {
            this.context = new AmCalendarContext();
        }

        /// <summary>
        /// Gets the calendar events from the repository.
        /// </summary>
        public IQueryable<CalendarEvent> CalendarEvents => this.context.CalendarEvents;

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