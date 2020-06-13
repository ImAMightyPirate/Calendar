// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AmCalendar.DomainObjects;
    using AmCalendar.Persistence.Contracts;
    using AmCalendar.Persistence.Contracts.Entities;
    using AmCalendar.Services.Contracts;
    using AmCalendar.Services.Contracts.Exceptions;
    using AmCalendar.Services.Contracts.Mappers;
    using EnsureThat;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Service with responsibility for reading calendar events.
    /// </summary>
    public class EventReaderService : IEventReaderService
    {
        private readonly ILogger logger;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly ICalendarEventMapper calendarEventMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventReaderService" /> class.
        /// </summary>
        /// <param name="logger">The ASP.NET Core logger.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        /// <param name="calendarEventMapper">The calendar event mapper.</param>
        public EventReaderService(
            ILogger<EventReaderService> logger,
            IRepositoryFactory repositoryFactory,
            ICalendarEventMapper calendarEventMapper)
        {
            this.logger = logger;
            this.repositoryFactory = repositoryFactory;
            this.calendarEventMapper = calendarEventMapper;
        }

        /// <summary>
        /// Gets a calendar event by its ID.
        /// </summary>
        /// <param name="calendarEventId">The calendar event ID.</param>
        /// <returns>The calendar event.</returns>
        /// <exception cref="RecordNotFoundException">Thrown if no calendar event for the ID is found.</exception>
        public CalendarEventDO GetCalendarEvent(long calendarEventId)
        {
            // Guards
            EnsureArg.IsGte(calendarEventId, 1);

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

                return this.calendarEventMapper.Map(record);
            }
        }
    }
}