// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts
{
    using System.Collections.Generic;
    using AmHaulage.DomainObjects;
    using AmHaulage.Services.Contracts.Exceptions;

    /// <summary>
    /// Contract for the month reader service.
    /// </summary>
    public interface IMonthReaderService
    {
        /// <summary>
        /// Gets all calendar events within a month that have not been deleted.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month (indexed by 1).</param>
        /// <returns>The calendar events within the time period.</returns>
        IEnumerable<CalendarEventDO> GetCalendarEvents(int year, int month);
    }
}
