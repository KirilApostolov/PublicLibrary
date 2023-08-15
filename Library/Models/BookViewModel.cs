namespace Library.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.GeneralConstants;

    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(BookNameMaxLength, MinimumLength = BookNameMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AuthorNameMaxLength, MinimumLength = AuthorNameMinLength)]
        public string Author { get; set; } = null!;

        [Required]
        [MinLength(MinLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal Rating { get; set; }

        [Required]
        [StringLength(BookDescriptionMaxLength, MinimumLength = MinLength)]
        public string Description { get; set; } = null!;

        [Range(1,int.MaxValue)]
        public int CategoryId { get; set; }
    }
}

