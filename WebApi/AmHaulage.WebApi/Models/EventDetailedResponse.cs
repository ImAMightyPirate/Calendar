// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.WebApi.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a response with the details of a calendar event.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EventDetailedResponse : EventSummaryResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether the calendar event has been deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}