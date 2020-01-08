using System;

namespace openSDesk.API.Dtos
{
    public class NoteForDetailedDto
    {
        public int Id { get; set; }
        public string WorkNote { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OwnerId { get; set; }
        public UserForTicketDetailDto Owner { get; set; }
    }
}