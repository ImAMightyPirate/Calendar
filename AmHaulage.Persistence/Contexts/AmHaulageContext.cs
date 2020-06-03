namespace AmHaulage.Persistent.Contexts
{
    using AmHaulage.Persistent.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AmHaulageContext : DbContext
    {
        public DbSet<CalendarEvent> CalendarEvents { get; set; }

         protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=(local)");
    }
}