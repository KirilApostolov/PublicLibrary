namespace Library.Data.Model
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    using static Common.GeneralConstants;

    [Comment("Category for Books")]
    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        }

        [Comment("Primary Key")]
        [Required]
        public int Id { get; set; }

        [Comment("Book Title")]
        [Required]
        [MaxLength(BookNameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; }
    }
}

//• Has Id – a unique integer, Primary Key
//• Has Name – a string with min length 5 and max length 50 (required)
//• Has Books – a collection of type Books