using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DbModels
{
    public class UserToken : BaseEntity
    {
        public int Id { get; set; }
        public string AccessTokenHash { get; set; }
        public DateTime AccessTokenExpiresDateTime { get; set; }
        public string RefreshTokenIdHash { get; set; }
        public string RefreshTokenIdHashSource { get; set; }
        public DateTime RefreshTokenExpiresDateTime { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
