namespace AmHaulage.Persistent.Entities
{
    using System.ComponentModel.DataAnnotations;
    
    public class CalendarEvent
    {
        public long Id { get; set; }

        public byte[] RowVersion { get; set; }

        [Required]
        [MaxLength(255)]
        public string Summary { get; set; }

        [Required]
        [MaxLength(255)]
        public string Location { get; set; }
    }
}