using System;

namespace Models.DbModels.TrackingInterfaces
{
    public interface IAuditableDelete
    {
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
