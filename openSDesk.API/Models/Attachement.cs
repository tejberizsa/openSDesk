using System;

namespace openSDesk.API.Models
{
    public class Attachement
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Owner { get; set; }
    }
}