// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.Contracts.Validators
{
    using System;
    using AmHaulage.Services.Contracts.Exceptions;

    /// <summary>
    /// Contract for the calendar event validator.
    /// </summary>
    public interface ICalendarEventValidator
    {
        /// <summary>
        /// Validates the summary field.
        /// </summary>
        /// <param name="summary">The summary.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if the field has no value.</exception>
        /// <exception cref="ArgumentExceedsMaxLengthException">Exception thrown if the field exceeds the maximum length.</exception>
        void ValidateSummary(string summary);

        /// <summary>
        /// Validates the location field.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if the field has no value.</exception>
        /// <exception cref="ArgumentExceedsMaxLengthException">Exception thrown if the field exceeds the maximum length.</exception>
        void ValidateLocation(string location);

        /// <summary>
        /// Validates the date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <exception cref="DateOutOfRangeException">Exception thrown when the end date occurs before the start date.</exception>
        void ValidateDateRange(DateTime startDate, DateTime endDate);
    }
}
