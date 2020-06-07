// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a request to create a new calendar event.
    /// </summary>
    public class CreateEventRequest
    {
        /// <summary>
        /// Gets or sets the ID of the request that created the record.
        /// </summary>
        /// <remarks>
        /// This is used to ensure that any HTTP request to create a record is
        /// idempotent (and does not result in multiple calendar events being created).
        /// </remarks>
        public Guid RequestId { get; set; }

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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the event.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}