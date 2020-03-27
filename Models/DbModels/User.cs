using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.DbModels.TrackingInterfaces;

namespace Models.DbModels
{
    public class User : BaseEntity, IAuditableDelete
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            UserTokens = new HashSet<UserToken>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public byte[] ProfileImage { get; set; }
        /// <summary>
        /// every time the user changes his Password,
        /// or an admin changes his Roles or stat/IsActive,
        /// create a new `SerialNumber` GUID and store it in the DB.
        /// </summary>
        public string SerialNumber { get; set; }
        public string PhoneNumber { get; set; }
        public bool? NotificationByMail { get; set; }
        public bool? NotificationBySMS { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
