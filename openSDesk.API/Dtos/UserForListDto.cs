using System;

namespace openSDesk.API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public DateTime Birth { get; set; }
        public int Age { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastActive { get; set; }
    }
}