// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Persistence.Contexts
{
    using System.Diagnostics.CodeAnalysis;
    using AmHaulage.Persistence.Contracts.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The EF Core database context.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AmHaulageContext : DbContext
    {
        /// <summary>
        /// Gets or sets the calendar event DB set.
        /// </summary>
        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        /// <summary>
        /// EF Core lifecycle hook to configure the DB context.
        /// </summary>
        /// <param name="options">The options.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Connection string only used locally be developers when running EF Core CLI commands
            options.UseSqlServer("Data Source=(local);Integrated Security=true;");
        }

        /// <summary>
        /// EF Core lifecycle hook to prepare the data model.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("AmHaulage");

            modelBuilder.Entity<CalendarEvent>().Property(e => e.Id)
                .IsRequired()
                .UseIdentityColumn();

            modelBuilder.Entity<CalendarEvent>().Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion();

            modelBuilder.Entity<CalendarEvent>().Property(e => e.Summary)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<CalendarEvent>().Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<CalendarEvent>().Property(e => e.StartDate)
                .IsRequired()
                .HasColumnType("DATE");

            modelBuilder.Entity<CalendarEvent>().Property(e => e.EndDate)
                .IsRequired()
                .HasColumnType("DATE");

            modelBuilder.Entity<CalendarEvent>().HasIndex(e => e.CreateRequestId)
                .IsUnique();
        }
    }
}