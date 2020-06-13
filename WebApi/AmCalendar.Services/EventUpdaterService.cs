// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services
{
    using System;
    using System.Linq;
    using AmCalendar.Persistence.Contracts;
    using AmCalendar.Services.Contracts;
    using AmCalendar.Services.Contracts.Exceptions;
    using AmCalendar.Services.Contracts.Validators;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Service with responsibility for updating calendar events.
    /// </summary>
    public class EventUpdaterService : IEventUpdaterService
    {
        private readonly ILogger logger;
        private readonly ICalendarEventValidator calendarEventValidator;
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventUpdaterService" /> class.
        /// </summary>
        /// <param name="logger">The ASP.NET Core logger.</param>
        /// <param name="calendarEventValidator">The calendar event validator.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public EventUpdaterService(
            ILogger<EventUpdaterService> logger,
            ICalendarEventValidator calendarEventValidator,
            IRepositoryFactory repositoryFactory)
        {
            this.logger = logger;
            this.calendarEventValidator = calendarEventValidator;
            this.repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Updates an existing calendar event with a new summary, location, start date and/or end date.
        /// </summary>
        /// <param name="calendarEventId">The calendar event ID.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="location">The location.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <exception cref="RecordNotFoundException">Exception thrown when calendar event for ID does not exist in the database.</exception>
        public void UpdateCalendarEvent(
            long calendarEventId,
            string summary,
            string location,
            DateTime startDate,
            DateTime endDate)
        {
            // Guards
            EnsureThat.EnsureArg.IsGte(calendarEventId, 1, nameof(calendarEventId));
            this.calendarEventValidator.ValidateSummary(summary);
            this.calendarEventValidator.ValidateLocation(location);
            this.calendarEventValidator.ValidateDateRange(startDate, endDate);

            using (var repo = this.repositoryFactory.Create())
            {
                var record = repo.CalendarEvents
                    .Where(
                        e =>
                            e.Id == calendarEventId &&
                            e.IsDeleted == false)
                    .SingleOrDefault();

                if (record == null)
                {
                    this.logger.LogError($"Calendar event with ID '{calendarEventId}' could not be found.");
                    throw new RecordNotFoundException();
                }

                record.Summary = summary;
                record.Location = location;
                record.StartDate = startDate.Date;
                record.EndDate = endDate.Date;

                repo.UpdateCalendarEvent(record);
                repo.SaveChanges();
            }

            this.logger.LogInformation($"Calendar event with ID '{calendarEventId}' has been updated.");
        }
    }
}
