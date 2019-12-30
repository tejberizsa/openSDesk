using System.Collections.Generic;

namespace openSDesk.API.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ICollection<SubStatus> SubStatuses { get; set; }
    }
}