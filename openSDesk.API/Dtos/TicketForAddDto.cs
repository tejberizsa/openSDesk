using System;
using System.ComponentModel.DataAnnotations;
using static openSDesk.API.Helpers.Enums;

namespace openSDesk.API.Dtos
{
    public class TicketForAddDto
    {
        public TicketForAddDto()
        {
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
            Deleted = false;
        }

        [Required(ErrorMessage = "Summary or title required")]
        [StringLength(250, ErrorMessage = "Summary cannot be that long")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Description required")]
        [StringLength(2000, ErrorMessage = "Description cannot be that long")]
        public string Description { get; set; }
        public string Location { get; set; }
        public PriorityType Priority { get; set; }
        public TicketType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; }
        public string Source { get; set; }
        
        [Required(ErrorMessage = "Category required")]
        public int CategoryId { get; set; }
        public int RequesterId { get; set; }
    }
}