using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.DbModels.TrackingInterfaces;

namespace Models.DbModels
{
    public class Book : BaseEntity, IAuditableDelete
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int CopiesCount { get; set; }
        public decimal Price { get; set; }
        public string CoverPhotoId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("User")]
        public int OwnerId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
