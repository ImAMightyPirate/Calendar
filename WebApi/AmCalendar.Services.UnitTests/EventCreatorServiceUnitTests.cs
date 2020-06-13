// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AmCalendar.DomainObjects;
    using AmCalendar.Persistence.Contracts;
    using AmCalendar.Persistence.Contracts.Entities;
    using AmCalendar.Services.Contracts.Exceptions;
    using AmCalendar.Services.Contracts.Validators;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NUnit.Framework;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behavior of the <see cref="EventCreatorService" /> class.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class EventCreatorServiceUnitTests
    {
        private EventCreatorService sut;

        private Mock<ILogger<EventCreatorService>> loggerMock;
        private Mock<ICalendarEventValidator> calendarEventValidatorMock;
        private Mock<IRepositoryFactory> repositoryFactoryMock;
        private Mock<IRepository> repositoryMock;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.loggerMock = new Mock<ILogger<EventCreatorService>>();
            this.calendarEventValidatorMock = new Mock<ICalendarEventValidator>();

            this.repositoryMock = new Mock<IRepository>();

            this.repositoryFactoryMock = new Mock<IRepositoryFactory>();
            this.repositoryFactoryMock
                .Setup(f => f.Create())
                .Returns(this.repositoryMock.Object);

            this.sut = new EventCreatorService(
                this.loggerMock.Object,
                this.calendarEventValidatorMock.Object,
                this.repositoryFactoryMock.Object);
        }

        /// <summary>
        /// When the calendar event is null then a <see cref="ArgumentNullException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenCalendarEventIsNullThenArgumentNullExceptionShouldBeThrown()
        {
            // Act + Assert
            Should.Throw<ArgumentNullException>(() => this.sut.CreateCalendarEvent(null));
        }

        /// <summary>
        /// When record with matching create request ID already exists then <see cref="DuplicateRequestException" /> should be thrown.
        /// </summary>
        [Test]
        public void WhenRecordWithMatchingCreateRequestIdAlreadyExistsThenDuplicateRequestExceptionShouldBeThrown()
        {
            // Arrange
            var existingCalendarEvent = new CalendarEvent
            {
                Id = 1,
                CreateRequestId = Guid.NewGuid(),
            };

            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent> { existingCalendarEvent }.AsQueryable());

            var newCalendarEvent = new CalendarEventDO
            {
                CreateRequestId = existingCalendarEvent.CreateRequestId,
            };

            // Act + Assert
            Should.Throw<DuplicateRequestException>(() => this.sut.CreateCalendarEvent(newCalendarEvent));
        }

        /// <summary>
        /// When the creation is performed then the entity should be saved to the repository.
        /// </summary>
        [Test]
        public void WhenCreationIsPerformedThenEntityShouldBeSavedToRepository()
        {
            // Arrange
            this.repositoryMock
                .Setup(r => r.CalendarEvents)
                .Returns(new List<CalendarEvent>().AsQueryable());

            var newCalendarEvent = new CalendarEventDO
            {
                CreateRequestId = Guid.NewGuid(),
            };

            // Act
            this.sut.CreateCalendarEvent(newCalendarEvent);

            // Assert
            this.repositoryMock.Verify(r => r.AddCalendarEvent(It.IsAny<CalendarEvent>()), Times.Once);
            this.repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        }
    }
}