namespace openSDesk.API.Helpers
{
    public class TicketParams : ParamsBase
    {
        public string TicketContainer { get; set; } = "Unassigned";
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? StatusId { get; set; }
        public int? UserGroupId { get; set; }
    }
}