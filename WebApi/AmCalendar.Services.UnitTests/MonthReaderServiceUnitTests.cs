// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AmCalendar.Persistence.Contracts;
    using AmCalendar.Persistence.Contracts.Entities;
    using AmCalendar.Services.Contracts.Mappers;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behavior of the <see cref="MonthReaderService" /> class.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class MonthReaderServiceUnitTests
    {
        private MonthReaderService sut;

        private Mock<ILogger<MonthReaderService>> loggerMock;
        private Mock<IRepositoryFactory> repositoryFactoryMock;
        private Mock<IRepository> repositoryMock;
        private Mock<ICalendarEventMapper> calendarEventMapperMock;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.loggerMock = new Mock<ILogger<MonthReaderService>>();
            this.repositoryMock = new Mock<IRepository>();

            this.repositoryFactoryMock = new Mock<IRepositoryFactory>();
            this.repositoryFactoryMock
                .Setup(f => f.Create())
                .Returns(this.repositoryMock.Object);

            this.calendarEventMapperMock = new Mock<ICalendarEventMapper>();

            this.sut = new MonthReaderService(
                this.loggerMock.Object,
                this.repositoryFactoryMock.Object,
                this.calendarEventMapperMock.Object);
        }

        /// <summary>
        /// When the month is less than one than a <see cref="ArgumentException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenMonthIsLessThanOneThenArgumentExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentException>(() => this.sut.GetCalendarEvents(2020, 0).ToList());
        }

        /// <summary>
        /// When the month is greater than twelve than a <see cref="ArgumentException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenMonthIsGreaterThanTwelvesThenArgumentExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentException>(() => this.sut.GetCalendarEvents(2020, 13).ToList());
        }

        /// <summary>
        /// When the calendar event is entirely within a month it should be returned.
        /// </summary>
        [Test]
        public void WhenCalendarEventIsEntirelyWithinMonthThenEventShouldBeReturned()
        {
            // Arrange
            var calendarEvent = new CalendarEvent
            {
                Id = 1,
                IsDeleted = false,
                StartDate = new DateTime(2020, 6, 1),
                EndDate = new DateTime(2020, 6, 2),
            };

            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent> { calendarEvent }.AsQueryable());

            // Act
            var result = this.sut.GetCalendarEvents(2020, 6).ToList();

            // Assert
            result.Count.ShouldBe(1);
        }

        /// <summary>
        /// When the calendar event is deleted it should not be returned.
        /// </summary>
        [Test]
        public void WhenCalendarEventIsDeletedThenEventShouldNotBeReturned()
        {
            // Arrange
            var calendarEvent = new CalendarEvent
            {
                Id = 1,
                IsDeleted = true,
                StartDate = new DateTime(2020, 6, 1),
                EndDate = new DateTime(2020, 6, 2),
            };

            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent> { calendarEvent }.AsQueryable());

            // Act
            var result = this.sut.GetCalendarEvents(2020, 6).ToList();

            // Assert
            result.Count.ShouldBe(0);
        }

        /// <summary>
        /// When the calendar event starts before the month it should be returned.
        /// </summary>
        [Test]
        public void WhenCalendarEventStartsBeforeMonthThenEventShouldBeReturned()
        {
            // Arrange
            var calendarEvent = new CalendarEvent
            {
                Id = 1,
                IsDeleted = false,
                StartDate = new DateTime(2020, 5, 20),
                EndDate = new DateTime(2020, 6, 2),
            };

            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent> { calendarEvent }.AsQueryable());

            // Act
            var result = this.sut.GetCalendarEvents(2020, 6).ToList();

            // Assert
            result.Count.ShouldBe(1);
        }

        /// <summary>
        /// When the calendar event ends after the month it should be returned.
        /// </summary>
        [Test]
        public void WhenCalendarEventEndsAfterMonthThenEventShouldBeReturned()
        {
            // Arrange
            var calendarEvent = new CalendarEvent
            {
                Id = 1,
                IsDeleted = false,
                StartDate = new DateTime(2020, 6, 20),
                EndDate = new DateTime(2020, 7, 2),
            };

            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent> { calendarEvent }.AsQueryable());

            // Act
            var result = this.sut.GetCalendarEvents(2020, 6).ToList();

            // Assert
            result.Count.ShouldBe(1);
        }

        /// <summary>
        /// When the calendar event extends the entire month it should be returned.
        /// </summary>
        [Test]
        public void WhenCalendarEventExtendsEntireMonthThenEventShouldBeReturned()
        {
            // Arrange
            var calendarEvent = new CalendarEvent
            {
                Id = 1,
                IsDeleted = false,
                StartDate = new DateTime(2020, 5, 20),
                EndDate = new DateTime(2020, 7, 2),
            };

            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent> { calendarEvent }.AsQueryable());

            // Act
            var result = this.sut.GetCalendarEvents(2020, 6).ToList();

            // Assert
            result.Count.ShouldBe(1);
        }

        /// <summary>
        /// When the calendar event is outside of the month it should not be returned.
        /// </summary>
        [Test]
        public void WhenCalendarEventOutsideOfMonthThenEventShouldNotBeReturned()
        {
            // Arrange
            var calendarEvent = new CalendarEvent
            {
                Id = 1,
                IsDeleted = false,
                StartDate = new DateTime(2020, 7, 2),
                EndDate = new DateTime(2020, 7, 20),
            };

            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent> { calendarEvent }.AsQueryable());

            // Act
            var result = this.sut.GetCalendarEvents(2020, 6).ToList();

            // Assert
            result.Count.ShouldBe(0);
        }
    }
}