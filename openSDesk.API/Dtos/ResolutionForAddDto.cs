using System;

namespace openSDesk.API.Dtos
{
    public class ResolutionForAddDto
    {
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Refused { get; set; }
        public int CodeId { get; set; }
        public int OwnerId { get; set; }
    }
}