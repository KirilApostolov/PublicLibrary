namespace Library.Controllers
{
    using Library.Contracts;
    using Library.Models;
    using Microsoft.AspNetCore.Mvc;

    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public async Task<IActionResult> All()
        {
            IEnumerable<AllBookViewModel> model = await this.bookService.GetAllBooksAsync();
            return View(model);
        }
        public async Task<IActionResult> AddToCollection(int id)
        {
            var book = await this.bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return RedirectToAction("All");
            }
            var userID = GetUserID();
            await bookService.AddBookToCollection(userID, book);
            return RedirectToAction("All");
        }

        public async Task<IActionResult> Mine()
        {
            IEnumerable<AllBookViewModel> model = await this.bookService.GetMyBooksAsync(GetUserID());
            return View(model);
        }

        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var book = await this.bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return RedirectToAction("All");
            }
            var userID = GetUserID();
            await bookService.RemoveBookFromCollection(userID, book);
            return RedirectToAction("Mine");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddBookViewModel model = await bookService.GetNewAddBookModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            decimal rate;
            if (!decimal.TryParse(model.Rating, out rate) || rate < 0 || rate > 10)
            {
                ModelState.AddModelError(nameof(model.Rating), "Rating must be a number between 0 and 10");
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await bookService.AddBookAsync(model);
            return RedirectToAction("All");
        }
    }
}
