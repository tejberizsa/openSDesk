using System.ComponentModel.DataAnnotations;

namespace openSDesk.API.Dtos
{
    public class UserForConfirmDto
    {
        [Required(ErrorMessage = "Id üres")]
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Nincs aktiváló kulcs")]
        [StringLength(12, ErrorMessage = "Hibás aktiváló kulcs")]
        public string ConfirmKey { get; set; }
    }
}