namespace openSDesk.API.Helpers
{
    public class ParamsBase
    {
        protected int MaxPageSize = 500;
        protected int pageSize = 40;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value;}
        }
    }
}