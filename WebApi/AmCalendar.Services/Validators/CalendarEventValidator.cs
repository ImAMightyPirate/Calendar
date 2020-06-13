// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services.Validators
{
    using System;
    using AmCalendar.Services.Contracts.Exceptions;
    using AmCalendar.Services.Contracts.Validators;

    /// <summary>
    /// Service with responsibility for validating calendar events.
    /// </summary>
    public class CalendarEventValidator : ICalendarEventValidator
    {
        /// <summary>
        /// The max length for the summary.
        /// </summary>
        public const int SummaryMaxLength = 255;

        /// <summary>
        /// The max length for the location.
        /// </summary>
        public const int LocationMaxLength = 255;

        /// <summary>
        /// Validates the summary field.
        /// </summary>
        /// <param name="summary">The summary.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if the field has no value.</exception>
        /// <exception cref="ArgumentExceedsMaxLengthException">Exception thrown if the field exceeds the maximum length.</exception>
        public void ValidateSummary(string summary)
        {
            if (string.IsNullOrEmpty(summary))
            {
                throw new ArgumentNullException(nameof(summary));
            }

            if (summary.Length > SummaryMaxLength)
            {
                throw new ArgumentExceedsMaxLengthException(nameof(summary));
            }
        }

        /// <summary>
        /// Validates the location field.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <exception cref="ArgumentNullException">Exception thrown if the field has no value.</exception>
        /// <exception cref="ArgumentExceedsMaxLengthException">Exception thrown if the field exceeds the maximum length.</exception>
        public void ValidateLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            if (location.Length > LocationMaxLength)
            {
                throw new ArgumentExceedsMaxLengthException(nameof(location));
            }
        }

        /// <summary>
        /// Validates the date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <exception cref="DateOutOfRangeException">Exception thrown when the end date occurs before the start date.</exception>
        public void ValidateDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate.Date < startDate.Date)
            {
                throw new DateOutOfRangeException();
            }
        }
    }
}