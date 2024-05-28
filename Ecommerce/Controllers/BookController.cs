using Ecommerce.Models;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.CreateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _bookRepository.UpdateBookAsync(id, book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _bookRepository.RemoveBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Book/FetchByGenre/{genre}
        public async Task<IActionResult> FetchByGenre(string genre)
        {
            //var booksByGenre = await _bookRepository.GetBooksByGenreAsync(genre);
            return View(null);
        }
    }
}
