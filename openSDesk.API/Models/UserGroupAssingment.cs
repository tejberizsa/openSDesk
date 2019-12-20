namespace openSDesk.API.Models
{
    public class UserGroupAssingment
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }
    }
}