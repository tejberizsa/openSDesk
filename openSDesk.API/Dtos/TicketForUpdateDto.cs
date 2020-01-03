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
        public int AssignmentGroupId { get; set; }
        public int AssignedToId { get; set; }
        public ResolutionForDetailedDto Resolution { get; set; }
        public SurveyForDetailedDto Surveys { get; set; }
    }
}