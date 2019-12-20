using System;
using System.Collections.Generic;
using static openSDesk.API.Helpers.Enums;

namespace openSDesk.API.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public PriorityType Priority { get; set; }
        public TicketType Type { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public DateTime? InvoicedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? NotificationSentAt { get; set; }
        public bool Deleted { get; set; }

        public int SourceId { get; set; }
        public Source Source { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Resolution> Resolutions { get; set; }
        public ICollection<Survey> Surveys { get; set; }
        public int RequesterId { get; set; }
        public User Requester { get; set; }
        public int AssignmentGroupId { get; set; }
        public UserGroup AssignmentGroup { get; set; }
        public int AssignedToId { get; set; }
        public User AssignedTo { get; set; }
    }
}