// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.Persistent.Contexts
{
    using AmHaulage.Persistent.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AmHaulageContext : DbContext
    {
        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Connection string only used locally be developers when running EF Core CLI commands
            options.UseSqlServer("Data Source=(local);Integrated Security=true;");
        }

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