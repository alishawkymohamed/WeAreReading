using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DbModels
{
    public class Status : BaseEntity
    {
        public Status()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
