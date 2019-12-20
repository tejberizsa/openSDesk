using System.Collections.Generic;

namespace openSDesk.API.Models
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserGroupAssingment> Users { get; set; }
        public ICollection<Ticket> TicketsAssigned { get; set; }
    }
}