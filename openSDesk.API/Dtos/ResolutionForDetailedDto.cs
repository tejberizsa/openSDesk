using System;

namespace openSDesk.API.Dtos
{
    public class ResolutionForDetailedDto
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Refused { get; set; }
        public int CodeId { get; set; }
        public string Code { get; set; }
        public int OwnerId { get; set; }
        public string Owner { get; set; }
    }
}