using System;

namespace Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public string PhoneNumber { get; set; }
        public bool? NotificationByMail { get; set; }
        public bool? NotificationBySMS { get; set; }
    }
}
