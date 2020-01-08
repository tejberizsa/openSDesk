using System;
using System.Collections.Generic;
using openSDesk.API.Models;
using static openSDesk.API.Helpers.Enums;

namespace openSDesk.API.Dtos
{
    public class TicketForDetailedDto
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
        public int SourceId { get; set; }
        public string Source { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int SubStatusId { get; set; }
        public SubStatus SubStatus { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int RequesterId { get; set; }
        public UserForTicketDetailDto Requester { get; set; }
        public int AssignmentGroupId { get; set; }
        public UserGroupForTicketDetailDto AssignmentGroup { get; set; }
        public int AssignedToId { get; set; }
        public UserForTicketDetailDto AssignedTo { get; set; }
        
        public ICollection<NoteForDetailedDto> Notes { get; set; }
        public ICollection<ResolutionForDetailedDto> Resolutions { get; set; }
        public ICollection<SurveyForDetailedDto> Surveys { get; set; }
    }
}