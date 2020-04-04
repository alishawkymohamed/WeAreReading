using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DbModels
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Book> Books { get; set; }
    }
}
