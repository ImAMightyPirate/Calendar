// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services
{
    using System;
    using System.Linq;
    using AmHaulage.Persistent.Contexts;
    using AmHaulage.Services.Contracts;
    using AmHaulage.Services.Contracts.Exceptions;
    using Microsoft.Extensions.Logging;

    public class EventUpdaterService : IEventUpdaterService
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initialises a new instance of the <see cref="EventUpdaterService" /> class.
        /// </summary>
        /// <param name="logger">The ASP.NET Core logger.</param>
        public EventUpdaterService(ILogger<EventUpdaterService> logger)
        {
            this.logger = logger;
        }

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
