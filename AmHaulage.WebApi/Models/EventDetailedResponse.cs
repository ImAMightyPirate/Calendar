// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EventDetailedResponse
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

        /// <summary>
        /// Gets or sets a value indcating whether the calendar event has been deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}