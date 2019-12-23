using System;

namespace openSDesk.API.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string WorkNote { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? NotificationSentAt { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}