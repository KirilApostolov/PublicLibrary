namespace Library.Data.Model
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("User Books")]
    public class IdentityUserBook
    {
        [Comment("Book Collector")]
        [ForeignKey("IdentityUser")]
        public string CollectorId { get; set; } = null!;

        [Comment("Collector")]
        public IdentityUser Collector { get; set; } = null!;

        [Comment("Book ID")]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Comment("Book")]
        public Book Book { get; set; } = null!;
    }
}


//• CollectorId – a string, Primary Key, foreign key (required)
//• Collector – IdentityUser
//• BookId – an integer, Primary Key, foreign key (required)
//• Book – Book