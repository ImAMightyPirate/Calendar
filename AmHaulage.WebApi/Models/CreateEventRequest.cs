// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateEventRequest
    {
        public Guid RequestId { get; set; }

        public string Summary { get; set; }

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