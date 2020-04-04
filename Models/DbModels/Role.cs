using System;
using System.Collections.Generic;
using Models.DbModels.TrackingInterfaces;

namespace Models.DbModels
{
    public class Role : BaseEntity, IAuditableDelete
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
