using Models.DbModels.TrackingInterfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DbModels
{
    public class Request : BaseEntity, IAuditableDelete
    {
        public int Id { get; set; }
        public bool? IsAccepted { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }

        public virtual Book Book { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
