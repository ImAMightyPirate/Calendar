// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Services.UnitTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using AmCalendar.Persistence.Contracts.Entities;
    using AmCalendar.Services.Mappers;
    using NUnit.Framework;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behavior of the <see cref="CalendarEventMapper" /> class.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CalendarEventMapperUnitTests
    {
        private CalendarEventMapper sut;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.sut = new CalendarEventMapper();
        }

        /// <summary>
        /// When the entity is mapped to a domain object then all fields should be populated.
        /// </summary>
        [Test]
        public void WhenEntityMappedToDomainObjectThenAllFieldsShouldBePopulated()
        {
            // Arrange
            var entity = new CalendarEvent
            {
                Id = 1,
                CreateRequestId = Guid.NewGuid(),
                Summary = "Summary",
                Location = "Location",
                StartDate = new DateTime(1955, 11, 5),
                EndDate = new DateTime(1985, 10, 26),
                IsDeleted = true,
            };

            // Act
            var result = this.sut.Map(entity);

            // Assert
            result.Id.ShouldBe(entity.Id);
            result.CreateRequestId.ShouldBe(entity.CreateRequestId);
            result.Summary.ShouldBe(entity.Summary);
            result.Location.ShouldBe(entity.Location);
            result.StartDate.ShouldBe(entity.StartDate);
            result.EndDate.ShouldBe(entity.EndDate);
            result.IsDeleted.ShouldBe(entity.IsDeleted);
        }
    }
}