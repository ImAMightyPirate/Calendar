// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services
{
    using System.Linq;
    using AmCalendar.Persistence.Contracts;
    using AmCalendar.Services.Contracts;
    using AmCalendar.Services.Contracts.Exceptions;
    using EnsureThat;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Service with responsibility for deleting calendar events.
    /// </summary>
    public class EventDeleterService : IEventDeleterService
    {
        private readonly ILogger logger;
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDeleterService" /> class.
        /// </summary>
        /// <param name="logger">The ASP.NET Core logger.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public EventDeleterService(
            ILogger<EventDeleterService> logger,
            IRepositoryFactory repositoryFactory)
        {
            this.logger = logger;
            this.repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Deletes an existing calendar event (logical delete, not physical, by applying deletion flag).
        /// </summary>
        /// <param name="calendarEventId">The calendar event ID.</param>
        /// <exception cref="RecordNotFoundException">Exception thrown when calendar event for ID does not exist in the database.</exception>
        public void DeleteCalendarEvent(long calendarEventId)
        {
            // Guards
            EnsureArg.IsGte(calendarEventId, 1, nameof(calendarEventId));

            using (var repo = this.repositoryFactory.Create())
            {
                var record = repo.CalendarEvents
                    .Where(e => e.Id == calendarEventId)
                    .SingleOrDefault();

                if (record == null)
                {
                    this.logger.LogError($"Calendar event with ID '{calendarEventId}' could not be found.");
                    throw new RecordNotFoundException();
                }

                record.IsDeleted = true;

                repo.UpdateCalendarEvent(record);
                repo.SaveChanges();
            }

            this.logger.LogInformation($"Calendar event with ID '{calendarEventId}' has been marked as deleted.");
        }
    }
}
