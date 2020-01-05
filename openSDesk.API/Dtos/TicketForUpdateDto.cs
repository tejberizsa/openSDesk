using System;
using static openSDesk.API.Helpers.Enums;

namespace openSDesk.API.Dtos
{
    public class TicketForUpdateDto
    {
        public PriorityType Priority { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public DateTime? InvoicedAt { get; set; }
        public int StatusId { get; set; }
        public int? SubStatusId { get; set; }
        public int CategoryId { get; set; }
    }
}