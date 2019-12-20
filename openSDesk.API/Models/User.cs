using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace openSDesk.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string EMail { get; set; }
        public string Introduction { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime? ConfirmationEmailSent { get; set; }
        public bool EMailConfirmed { get; set; }
        public string ConfirmKey { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<UserPhoto> Photos { get; set; }
    }
}