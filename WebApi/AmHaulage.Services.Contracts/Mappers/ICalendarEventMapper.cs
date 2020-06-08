 // Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts.Mappers
{
    using AmHaulage.DomainObjects;
    using AmHaulage.Persistence.Contracts.Entities;

    /// <summary>
    /// Contract for the calendar event mapper.
    /// </summary>
    public interface ICalendarEventMapper
    {
        /// <summary>
        /// Map a calendar event entity to the equivalent domain object.
        /// </summary>
        /// <param name="entity">The source entity.</param>
        /// <returns>The mapped domain object.</returns>
        CalendarEventDO Map(CalendarEvent entity);
    }
}