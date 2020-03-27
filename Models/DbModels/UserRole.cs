using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.DbModels.TrackingInterfaces;

namespace Models.DbModels
{
    public class UserRole : BaseEntity, IAuditableDelete
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
