namespace openSDesk.API.Helpers
{
    public class PageParams
    {
        private const int MaxPageSize = 500;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 40;
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value;}
        }
        
        public string QueryString { get; set; }
        public int? TopicId { get; set; }
        public int? UserId { get; set; }
        public bool? isFollowQuery { get; set; }
    }
}