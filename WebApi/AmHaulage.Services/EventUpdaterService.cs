// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services
{
    using System;
    using System.Linq;
    using AmHaulage.Persistent.Contexts;
    using AmHaulage.Services.Contracts;
    using AmHaulage.Services.Contracts.Exceptions;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Service with responsibility for updating calendar events.
    /// </summary>
    public class EventUpdaterService : IEventUpdaterService
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventUpdaterService" /> class.
        /// </summary>
        /// <param name="logger">The ASP.NET Core logger.</param>
        public EventUpdaterService(ILogger<EventUpdaterService> logger)
        {
            this.logger = logger;
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
            using (var context = new AmHaulageContext())
            {
                var record = context.CalendarEvents
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

                context.Update(record);
                context.SaveChanges();
            }

            this.logger.LogInformation($"Calendar event with ID '{calendarEventId}' has been updated.");
        }
    }
}
