using System;

namespace openSDesk.API.Models
{
    public class UserPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}