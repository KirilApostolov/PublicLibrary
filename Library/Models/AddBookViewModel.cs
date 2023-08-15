namespace Library.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.GeneralConstants;
    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            this.Categories = new HashSet<CategoryViewMode>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(BookNameMaxLength, MinimumLength = BookNameMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AuthorNameMaxLength, MinimumLength = AuthorNameMinLength)]
        public string Author { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Url { get; set; } = null!;

        [Required]
        public string Rating { get; set; } = null!;

        [Required]
        [StringLength(BookDescriptionMaxLength, MinimumLength = MinLength)]
        public string Description { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewMode> Categories { get; set; }
    }
}
