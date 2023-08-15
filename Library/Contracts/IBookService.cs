namespace Library.Contracts
{
    using Library.Models;

    public interface IBookService
    {
        Task AddBookToCollection(string userID, BookViewModel book);
        Task<BookViewModel?> GetBookByIdAsync(int id);
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();
        Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId);
        Task RemoveBookFromCollection(string userID, BookViewModel book);
        Task<AddBookViewModel> GetNewAddBookModelAsync();
        Task AddBookAsync(AddBookViewModel model);
        Task<AddBookViewModel?> GetBookByIdForEditAsync(int id);
        Task EditBookAsync(AddBookViewModel model, int id);
    }
}
