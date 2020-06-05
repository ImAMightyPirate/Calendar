﻿// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services
{
    using System.Linq;
    using AmHaulage.Persistent.Contexts;
    using AmHaulage.Services.Contracts;
    using AmHaulage.Services.Contracts.Exceptions;
    using EnsureThat;
    using Microsoft.Extensions.Logging;

    public class EventDeleterService : IEventDeleterService
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initialises a new instance of the <see cref="EventDeleterService" /> class.
        /// </summary>
        /// <param name="logger">The ASP.NET Core logger.</param>
        public EventDeleterService(ILogger<EventDeleterService> logger)
        {
            this.logger = logger;
        }

        public void DeleteCalendarEvent(long calendarEventId)
        {
            EnsureArg.IsGte(calendarEventId, 1);

            using (var context = new AmHaulageContext())
            {
                var record = context.CalendarEvents
                    .Where(e => e.Id == calendarEventId)
                    .SingleOrDefault();

                if (record == null)
                {
                    this.logger.LogError($"Calendar event with ID '{calendarEventId}' could not be found.");
                    throw new RecordNotFoundException();
                }

                record.IsDeleted = true;

                context.Update(record);
                context.SaveChanges();
            }

            this.logger.LogInformation($"Calendar event with ID '{calendarEventId}' has been marked as deleted.");
        }
    }
}