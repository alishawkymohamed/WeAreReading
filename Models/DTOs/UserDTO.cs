using System;

namespace Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public string ProfilePictureId { get; set; }
        public string PhoneNumber { get; set; }
        public int GovernmentId { get; set; }
        public string GovernmentName { get; set; }
    }
}
