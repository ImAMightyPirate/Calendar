// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AmCalendar.Persistence.Contracts;
    using AmCalendar.Persistence.Contracts.Entities;
    using AmCalendar.Services.Contracts.Exceptions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behavior of the <see cref="EventDeleterService" /> class.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class EventDeleterServiceUnitTests
    {
        private EventDeleterService sut;

        private Mock<ILogger<EventDeleterService>> loggerMock;
        private Mock<IRepositoryFactory> repositoryFactoryMock;
        private Mock<IRepository> repositoryMock;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.loggerMock = new Mock<ILogger<EventDeleterService>>();

            this.repositoryMock = new Mock<IRepository>();

            this.repositoryFactoryMock = new Mock<IRepositoryFactory>();
            this.repositoryFactoryMock
                .Setup(f => f.Create())
                .Returns(this.repositoryMock.Object);

            this.sut = new EventDeleterService(
                this.loggerMock.Object,
                this.repositoryFactoryMock.Object);
        }

        /// <summary>
        /// When the calendar event ID is zero then a <see cref="ArgumentException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenCalendarEventIdIsZeroThenArgumentExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentException>(() => this.sut.DeleteCalendarEvent(0));
        }

        /// <summary>
        /// When the calendar event ID is negative then a <see cref="ArgumentException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenCalendarEventIdIsNegativeThenArgumentExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentException>(() => this.sut.DeleteCalendarEvent(-1));
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
            Should.Throw<RecordNotFoundException>(() => this.sut.DeleteCalendarEvent(1));
        }

        /// <summary>
        /// When the deletion is performed then the <see cref="CalendarEvent.IsDeleted" /> property should be set to true.
        /// </summary>
        [Test]
        public void WhenDeletionIsPerformedThenIsDeletedPropertyShouldBeTrue()
        {
            // Arrange
            var calendarEvent = new CalendarEvent
            {
                Id = 1,
                IsDeleted = false,
            };

            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent> { calendarEvent }.AsQueryable());

            // Act
            this.sut.DeleteCalendarEvent(1);

            // Assert
            calendarEvent.IsDeleted.ShouldBeTrue();

            this.repositoryMock.Verify(r => r.UpdateCalendarEvent(calendarEvent), Times.Once);
            this.repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        }
    }
}