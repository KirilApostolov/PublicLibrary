namespace Library.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Library.Contracts;
    using Library.Data;
    using Library.Models;
    using Library.Data.Model;

    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;
        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
        {
            var books = await this.dbContext
                    .Books
                    .Select(book => new AllBookViewModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Rating = book.Rating,
                        ImageUrl = book.ImageUrl,
                        Category = book.Category.Name,
                    }).ToListAsync();
            return books;
        }


        public async Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId)
        {
            var books = await this.dbContext
                .IdentityUserBook
                .Where(ub => ub.CollectorId == userId)
                .Select(b => new AllBookViewModel
                {
                    Id = b.Book.Id,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    ImageUrl = b.Book.ImageUrl,
                    Category = b.Book.Category.Name,
                    Description = b.Book.Description,
                }).ToListAsync();
            return books;
        }
        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {
            var book = await this.dbContext.Books
                .Where(b => b.Id == id)
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Rating = b.Rating,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    CategoryId = b.CategoryId,
                    Description = b.Description,
                }).FirstOrDefaultAsync();
            return book;
        }

        public async Task AddBookToCollection(string userID, BookViewModel book)
        {
            bool AlreadyAdded = await dbContext.IdentityUserBook
                    .AnyAsync(ub => ub.CollectorId == userID && ub.BookId == book.Id);

            if (!AlreadyAdded)
            {
                var userBook = new IdentityUserBook { CollectorId = userID, BookId = book.Id };
                await dbContext.IdentityUserBook.AddAsync(userBook);
                await dbContext.SaveChangesAsync();               
            }
        }

        public async Task RemoveBookFromCollection(string userID, BookViewModel book)
        {
            var userBook = await dbContext.IdentityUserBook
                                .FirstOrDefaultAsync(ub => ub.CollectorId == userID && ub.BookId == book.Id);
            if (userBook != null)
            {
                dbContext.IdentityUserBook.Remove(userBook);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<AddBookViewModel> GetNewAddBookModelAsync()
        {
            var categories = await dbContext.Categories
                .Select(c => new CategoryViewMode
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();
            var bookModel = new AddBookViewModel
            {
                Categories = categories,
            };
            return bookModel;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            Book book = new Book
            {
                Title = model.Title,
                ImageUrl = model.Url,
                Author = model.Author,
                Rating = decimal.Parse(model.Rating),
                CategoryId = model.CategoryId,
                Description = model.Description,
            };
            await this.dbContext.Books.AddAsync(book);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<AddBookViewModel?> GetBookByIdForEditAsync(int id)
        {
            var categories = await dbContext.Categories
                .Select(c => new CategoryViewMode
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();
                var bookModel = new AddBookViewModel
                {
                    Categories = categories,
                };
            var book = await this.dbContext.Books
                .Where(b => b.Id == id)
                .Select(b => new AddBookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Rating = b.Rating.ToString(),
                    Author = b.Author,
                    Url = b.ImageUrl,
                    CategoryId = b.CategoryId,
                    Description = b.Description,
                    Categories = categories
                }).FirstOrDefaultAsync();
            return book;
        }

        public async Task EditBookAsync(AddBookViewModel model, int Id)
        {
            var book = await dbContext.Books.FindAsync(Id);
            if (book != null)
            {
                book.Title = model.Title;
                book.Rating = decimal.Parse(model.Rating);
                book.Author = model.Author;
                book.ImageUrl = model.Url;
                book.CategoryId = model.CategoryId;
                book.Description = model.Description;
                await this.dbContext.SaveChangesAsync();
            }


        }
    }
}
