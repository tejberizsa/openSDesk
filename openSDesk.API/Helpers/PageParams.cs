namespace openSDesk.API.Helpers
{
    public class PageParams : ParamsBase
    {
        public string QueryString { get; set; }
        public int? TopicId { get; set; }
        public int? UserId { get; set; }
        public bool? isFollowQuery { get; set; }
    }
}