﻿using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class RegisterUserDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ProfilePictureId { get; set; }
        public int GovernmentId { get; set; }
        public int RoleId { get; set; }
    }
}
