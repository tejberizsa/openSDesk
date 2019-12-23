namespace openSDesk.API.Helpers
{
    public class MessageParams : ParamsBase
    {
        public MessageParams()
        {
            MaxPageSize = 50;
            pageSize = 10;
        }
        public int UserId { get; set; }
        public string MessageContainer { get; set; } = "Unread";
    }
}