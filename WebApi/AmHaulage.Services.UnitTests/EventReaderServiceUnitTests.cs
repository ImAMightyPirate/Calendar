// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AmHaulage.DomainObjects;
    using AmHaulage.Persistence.Contracts;
    using AmHaulage.Persistence.Contracts.Entities;
    using AmHaulage.Services.Contracts.Exceptions;
    using AmHaulage.Services.Contracts.Mappers;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behavior of the <see cref="EventReaderService" /> class.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class EventReaderServiceUnitTests
    {
        private EventReaderService sut;

        private Mock<ILogger<EventReaderService>> loggerMock;
        private Mock<IRepositoryFactory> repositoryFactoryMock;
        private Mock<IRepository> repositoryMock;
        private Mock<ICalendarEventMapper> calendarEventMapperMock;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.loggerMock = new Mock<ILogger<EventReaderService>>();
            this.repositoryMock = new Mock<IRepository>();

            this.repositoryFactoryMock = new Mock<IRepositoryFactory>();
            this.repositoryFactoryMock
                .Setup(f => f.Create())
                .Returns(this.repositoryMock.Object);

            this.calendarEventMapperMock = new Mock<ICalendarEventMapper>();

            this.sut = new EventReaderService(
                this.loggerMock.Object,
                this.repositoryFactoryMock.Object,
                this.calendarEventMapperMock.Object);
        }

        /// <summary>
        /// When the calendar event ID is zero then a <see cref="ArgumentException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenCalendarEventIdIsZeroThenArgumentExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentException>(() => this.sut.GetCalendarEvent(0));
        }

        /// <summary>
        /// When the calendar event ID is negative then a <see cref="ArgumentException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenCalendarEventIdIsNegativeThenArgumentExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentException>(() => this.sut.GetCalendarEvent(-1));
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
            Should.Throw<RecordNotFoundException>(() => this.sut.GetCalendarEvent(1));
        }

        /// <summary>
        /// When the calendar event exists then the mapped object should be returned.
        /// </summary>
        [Test]
        public void WhenCalendarEventExistsThenMappedObjectShouldBeReturned()
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

            var mappedEvent = new CalendarEventDO();
            this.calendarEventMapperMock
                .Setup(m => m.Map(It.IsAny<CalendarEvent>()))
                .Returns(mappedEvent);

            // Act
            var result = this.sut.GetCalendarEvent(1);

            // Assert
            result.ShouldBe(mappedEvent);
        }

        /// <summary>
        /// When the calendar event exists and is marked as deleted then the mapped object should be returned.
        /// </summary>
        [Test]
        public void WhenCalendarEventIsDeletedThenMappedObjectShouldBeReturned()
        {
            // Arrange
            var calendarEvent = new CalendarEvent
            {
                Id = 1,
                IsDeleted = true,
            };

            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent> { calendarEvent }.AsQueryable());

            var mappedEvent = new CalendarEventDO();
            this.calendarEventMapperMock
                .Setup(m => m.Map(It.IsAny<CalendarEvent>()))
                .Returns(mappedEvent);

            // Act
            var result = this.sut.GetCalendarEvent(1);

            // Assert
            result.ShouldBe(mappedEvent);
        }
    }
}