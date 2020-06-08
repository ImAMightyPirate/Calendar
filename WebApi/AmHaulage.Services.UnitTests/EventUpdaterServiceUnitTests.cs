// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AmHaulage.Persistence.Contracts;
    using AmHaulage.Persistence.Contracts.Entities;
    using AmHaulage.Services.Contracts.Exceptions;
    using AmHaulage.Services.Contracts.Validators;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behavior of the <see cref="EventUpdaterService" /> class.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class EventUpdaterServiceUnitTests
    {
        private EventUpdaterService sut;

        private Mock<ILogger<EventUpdaterService>> loggerMock;
        private Mock<ICalendarEventValidator> calendarEventValidatorMock;
        private Mock<IRepositoryFactory> repositoryFactoryMock;
        private Mock<IRepository> repositoryMock;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.loggerMock = new Mock<ILogger<EventUpdaterService>>();
            this.calendarEventValidatorMock = new Mock<ICalendarEventValidator>();

            this.repositoryMock = new Mock<IRepository>();

            this.repositoryFactoryMock = new Mock<IRepositoryFactory>();
            this.repositoryFactoryMock
                .Setup(f => f.Create())
                .Returns(this.repositoryMock.Object);

            this.sut = new EventUpdaterService(
                this.loggerMock.Object,
                this.calendarEventValidatorMock.Object,
                this.repositoryFactoryMock.Object);
        }

        /// <summary>
        /// When the calendar event ID is zero then a <see cref="ArgumentException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenCalendarEventIdIsZeroThenArgumentExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentException>(() => this.sut.UpdateCalendarEvent(
                0,
                "x",
                "x",
                new DateTime(1955, 11, 5),
                new DateTime(1985, 10, 26)));
        }

        /// <summary>
        /// When the calendar event ID is negative then a <see cref="ArgumentException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenCalendarEventIdIsNegativeThenArgumentExceptionShouldBeThrown()
        {
          // Act + Assert
            Should.Throw<ArgumentException>(() => this.sut.UpdateCalendarEvent(
                -1,
                "x",
                "x",
                new DateTime(1955, 11, 5),
                new DateTime(1985, 10, 26)));
        }

        /// <summary>
        /// When the calendar event cannot be found then a <see cref="RecordNotFoundException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenCalendarEventCannotBeFoundThenRecordNotFoundExceptionShouldBeThrown()
        {
            // Arrange
            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent>().AsQueryable());

            // Act + Assert
            Should.Throw<RecordNotFoundException>(() => this.sut.UpdateCalendarEvent(
                1,
                "x",
                "x",
                new DateTime(1955, 11, 5),
                new DateTime(1985, 10, 26)));
        }

        /// <summary>
        /// When the calendar event cannot be found then a <see cref="RecordNotFoundException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenCalendarEventIsDeletedThenRecordNotFoundExceptionShouldBeThrown()
        {
            // Arrange
            var calendarEvent = new CalendarEvent
            {
                Id = 1,
                IsDeleted = true,
            };

            // Arrange
            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent>().AsQueryable());

            // Act + Assert
            Should.Throw<RecordNotFoundException>(() => this.sut.UpdateCalendarEvent(
                1,
                "x",
                "x",
                new DateTime(1955, 11, 5),
                new DateTime(1985, 10, 26)));
        }

        /// <summary>
        /// When the update is performed then the expected properties should be updated.
        /// </summary>
        [Test]
        public void WhenUpdateIsPerformedThenExpectedPropertiesShouldBeUpdated()
        {
            // Arrange
            var calendarEvent = new CalendarEvent
            {
                Id = 1,
                Summary = string.Empty,
                Location = string.Empty,
                StartDate = new DateTime(1980, 1, 1),
                EndDate = new DateTime(1980, 1, 1),
                IsDeleted = false,
            };

            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent> { calendarEvent }.AsQueryable());

            // Act
            this.sut.UpdateCalendarEvent(
                1,
                "x",
                "x",
                new DateTime(1955, 11, 5),
                new DateTime(1985, 10, 26));

            // Assert
            calendarEvent.Summary.ShouldBe("x");
            calendarEvent.Location.ShouldBe("x");
            calendarEvent.StartDate.ShouldBe(new DateTime(1955, 11, 5));
            calendarEvent.EndDate.ShouldBe(new DateTime(1985, 10, 26));
            calendarEvent.IsDeleted.ShouldBeFalse();

            this.repositoryMock.Verify(r => r.UpdateCalendarEvent(calendarEvent), Times.Once);
            this.repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        }
    }
}