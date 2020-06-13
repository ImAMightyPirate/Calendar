// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a response with the summary of a calendar event.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EventSummaryResponse
    {
        /// <summary>
        /// Gets or sets a unique ID that identifies the record.
        /// </summary>
        public long Id { get; set; }

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