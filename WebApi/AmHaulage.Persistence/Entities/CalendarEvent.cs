// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Persistent.Entities
{
    using System;

    /// <summary>
    /// Entity for the calendar event.
    /// </summary>
    public class CalendarEvent
    {
        /// <summary>
        /// Gets or sets a unique ID that identifies the record.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the SQL Server generated timestamp/row version.
        /// </summary>
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Gets or sets the ID of the request that created the record.
        /// </summary>
        /// <remarks>
        /// This is used to ensure that any HTTP request to create a record is
        /// idempotent (and does not result in multiple calendar events being created).
        /// </remarks>
        public Guid CreateRequestId { get; set; }

        /// <summary>
        /// Gets or sets the summary text for the calendar event.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the location text for the calendar event.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the start date of the event.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the event.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the calendar event has been deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}