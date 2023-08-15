namespace Library.Data.Model
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.GeneralConstants;

    [Comment("Books for library")]
    public class Book
    {
        public Book()
        {
            this.UsersBooks = new HashSet<IdentityUserBook>();
        }
        [Comment("Primary Key")]
        [Required]
        public int Id { get; set; }

        [Comment("Book Title")]
        [Required]
        [MaxLength(BookNameMaxLength)]       
        public string Title { get; set; } = null!;

        [Comment("Book Author")]
        [Required]
        [MaxLength(BookNameMaxLength)]
        public string Author { get; set; } = null!;

        [Comment("Book Description")]
        [Required]
        [MaxLength(BookDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Comment("Book ImageUrl")]
        [Required]
        public string ImageUrl { get; set; } = null!;

        [Comment("Book Rating")]
        [Required]
        public decimal Rating { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<IdentityUserBook> UsersBooks { get; set; }
    }
}


//• Has Id – a unique integer, Primary Key
//• Has Title – a string with min length 10 and max length 50 (required)
//• Has Author – a string with min length 5 and max length 50 (required)
//• Has Description – a string with min length 5 and max length 5000 (required)
//• Has ImageUrl – a string (required)
//• Has Rating – a decimal with min value 0.00 and max value 10.00 (required)
//• Has CategoryId – an integer, foreign key (required)
//• Has Category – a Category (required)
//• Has UsersBooks – a collection of type IdentityUserBook