// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.UnitTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using AmHaulage.Services.Contracts.Exceptions;
    using AmHaulage.Services.Validators;
    using NUnit.Framework;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behavior of the <see cref="CalendarEventValidator" /> class.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CalendarEventValidatorUnitTests
    {
        private CalendarEventValidator sut;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.sut = new CalendarEventValidator();
        }

        /// <summary>
        /// When the summary is null then a <see cref="ArgumentNullException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenSummaryIsNullThenArgumentNullExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentNullException>(() => this.sut.ValidateSummary(null));
        }

        /// <summary>
        /// When the summary is empty then a <see cref="ArgumentNullException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenSummaryIsEmptyThenArgumentNullExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentNullException>(() => this.sut.ValidateSummary(string.Empty));
        }

        /// <summary>
        /// When the summary exceeds the max length then a <see cref="ArgumentExceedsMaxLengthException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenSummaryExceedsMaxLengthThenArgumentExceedsMaxLengthExceptionShouldBeThrown()
        {
            // Arrange
            var summary = "x".PadRight(CalendarEventValidator.SummaryMaxLength + 1, 'x');

            // Act + Assert
            Should.Throw<ArgumentExceedsMaxLengthException>(() => this.sut.ValidateSummary(summary));
        }

        /// <summary>
        /// When the summary is valid then no exception should be thrown.
        /// </summary>
        [Test]
        public void WhenSummaryIsValidThenNoExceptionShouldBeThrown()
        {
            // Arrange
            var summary = "x";

            // Act + Assert
            Should.NotThrow(() => this.sut.ValidateSummary(summary));
        }

        /// <summary>
        /// When the location is null then a <see cref="ArgumentNullException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenLocationIsNullThenArgumentNullExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentNullException>(() => this.sut.ValidateLocation(null));
        }

        /// <summary>
        /// When the location is empty then a <see cref="ArgumentNullException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenLocationIsEmptyThenArgumentNullExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentNullException>(() => this.sut.ValidateLocation(string.Empty));
        }

        /// <summary>
        /// When the location exceeds the max length then a <see cref="ArgumentExceedsMaxLengthException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenLocationExceedsMaxLengthThenArgumentExceedsMaxLengthExceptionShouldBeThrown()
        {
            // Arrange
            var location = "x".PadRight(CalendarEventValidator.LocationMaxLength + 1, 'x');

            // Act + Assert
            Should.Throw<ArgumentExceedsMaxLengthException>(() => this.sut.ValidateLocation(location));
        }

        /// <summary>
        /// When the location is valid then no exception should be thrown.
        /// </summary>
        [Test]
        public void WhenLocationIsValidThenNoExceptionShouldBeThrown()
        {
            // Arrange
            var location = "x";

            // Act + Assert
            Should.NotThrow(() => this.sut.ValidateLocation(location));
        }

        /// <summary>
        /// When the end date is before the start date then a <see cref="DateOutOfRangeException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenEndDateIsBeforeStartDateThenDateOutOfRangeExceptionShouldBeThrown()
        {
            // Arrange
            var startDate = new DateTime(1985, 10, 26);
            var endDate = new DateTime(1955, 11, 5);

            // Act + Assert
            Should.Throw<DateOutOfRangeException>(() => this.sut.ValidateDateRange(startDate, endDate));
        }

        /// <summary>
        /// When the end date is after the start date then no exception should be thrown.
        /// </summary>
        [Test]
        public void WhenEndDateIsAfterStartDateThenNoExceptionShouldBeThrown()
        {
            // Arrange
            var startDate = new DateTime(1955, 11, 5);
            var endDate = new DateTime(1985, 10, 26);

            // Act + Assert
            Should.NotThrow(() => this.sut.ValidateDateRange(startDate, endDate));
        }

        /// <summary>
        /// When the end date equals the start date then no exception should be thrown.
        /// </summary>
        [Test]
        public void WhenEndDateEqualsStartDateThenNoExceptionShouldBeThrown()
        {
            // Arrange
            var startDate = new DateTime(1985, 10, 26);
            var endDate = new DateTime(1985, 10, 26);

            // Act + Assert
            Should.NotThrow(() => this.sut.ValidateDateRange(startDate, endDate));
        }
    }
}