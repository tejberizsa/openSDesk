using System;

namespace openSDesk.API.Dtos
{
    public class NoteForAddDto
    {
        public int Id { get; set; }
        public string WorkNote { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OwnerId { get; set; }
        public int TicketId { get; set; }
    }
}