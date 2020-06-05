// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AmHaulage.DomainObjects;
    using AmHaulage.Persistent.Contexts;
    using AmHaulage.Persistent.Entities;
    using AmHaulage.Services.Contracts;
    using AmHaulage.Services.Contracts.Exceptions;
    using EnsureThat;
    using Microsoft.Extensions.Logging;

    public class EventReaderService : IEventReaderService
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initialises a new instance of the <see cref="EventReaderService" /> class.
        /// </summary>
        /// <param name="logger">The ASP.NET Core logger.</param>
        public EventReaderService(ILogger<EventReaderService> logger)
        {
            this.logger = logger;
        }

        public CalendarEventDO GetCalendarEvent(long calendarEventId)
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

                return this.Map(record);
            }
        }

        public IEnumerable<CalendarEventDO> GetCalendarEvents(int year, int month)
        {
            EnsureArg.IsGte(month, 1, nameof(month));
            EnsureArg.IsLte(month, 12, nameof(month));

            var daysInMonth = DateTime.DaysInMonth(year, month);
            var monthStartDate = new DateTime(year, month, 1);
            var monthEndDate = new DateTime(year, month, daysInMonth);

            using (var context = new AmHaulageContext())
            {
                var records = context.CalendarEvents
                    .Where(
                        e =>
                            e.IsDeleted == false &&   
                            (
                                // Event starts within the month
                                (e.StartDate.Date >= monthStartDate.Date && e.StartDate.Date <= monthEndDate.Date) ||

                                // Event ends within the month
                                (e.EndDate.Date >= monthStartDate.Date && e.EndDate.Date <= monthEndDate.Date) ||

                                // Event spans the entire month
                                (e.StartDate.Date < monthStartDate.Date && e.EndDate.Date > monthEndDate.Date)
                            )
                    );

                foreach (var record in records)
                {
                    yield return this.Map(record);
                }
            }
        }

        private CalendarEventDO Map(CalendarEvent record)
        {
                return new CalendarEventDO
                {
                    Id = record.Id,
                    CreateRequestId = record.CreateRequestId,
                    Summary = record.Summary,
                    Location = record.Location,
                    StartDate = record.StartDate,
                    EndDate = record.EndDate,
                    IsDeleted = record.IsDeleted,
                };
        }
    }
}
