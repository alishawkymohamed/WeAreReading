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
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public string ProfilePictureId { get; set; }
        /// <summary>
        /// every time the user changes his Password,
        /// or an admin changes his Roles or stat/IsActive,
        /// create a new `SerialNumber` GUID and store it in the DB.
        /// </summary>
        public string SerialNumber { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey("Government")]
        public int GovernmentId { get; set; }
        public virtual Government Government { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserToken> UserTokens { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Book> Books { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
