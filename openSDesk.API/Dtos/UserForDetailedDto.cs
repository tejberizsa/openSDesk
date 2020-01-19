using System;
using System.Collections.Generic;
using openSDesk.API.Dtos;

namespace openSDesk.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Introduction { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<PhotosForDetailedDto> Photos { get; set; }
    }
}