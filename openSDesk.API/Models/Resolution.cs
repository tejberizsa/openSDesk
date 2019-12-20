using System;

namespace openSDesk.API.Models
{
    public class Resolution
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? NotificationSentAt { get; set; }
        public bool Refused { get; set; }
        public int CodeId { get; set; }
        public ResolutionCode Code { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }
}