 // Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Mappers
{
    using AmHaulage.DomainObjects;
    using AmHaulage.Persistence.Contracts.Entities;
    using AmHaulage.Services.Contracts.Mappers;

    /// <summary>
    /// Mapper responsible for converting calendar event objects.
    /// </summary>
    public class CalendarEventMapper : ICalendarEventMapper
    {
        /// <summary>
        /// Map a calendar event entity to the equivalent domain object.
        /// </summary>
        /// <param name="entity">The source entity.</param>
        /// <returns>The mapped domain object.</returns>
        public CalendarEventDO Map(CalendarEvent entity)
        {
                return new CalendarEventDO
                {
                    Id = entity.Id,
                    CreateRequestId = entity.CreateRequestId,
                    Summary = entity.Summary,
                    Location = entity.Location,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    IsDeleted = entity.IsDeleted,
                };
        }
    }
}