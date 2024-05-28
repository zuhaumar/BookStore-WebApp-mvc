using Ecommerce.Models;
using MongoDB.Driver;
using Ecommerce.Data;

namespace Ecommerce.Repositories
{
    public class BookRepository
    {
        private readonly IMongoCollection<Book> _books;

        public BookRepository(MongoDbContext context)
        {
            _books = context.Books;
        }

        // Get all books
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _books.Find(book => true).ToListAsync();
        }

        // Get a book by its ID
        public async Task<Book> GetBookByIdAsync(string id)
        {
            return await _books.Find<Book>(book => book.Id == id).FirstOrDefaultAsync();
        }

        // Create a new book
        public async Task CreateBookAsync(Book book)
        {
            await _books.InsertOneAsync(book);
        }

        // Update an existing book
        public async Task UpdateBookAsync(string id, Book bookIn)
        {
            await _books.ReplaceOneAsync(book => book.Id == id, bookIn);
        }

        // Remove a book by its ID
        public async Task RemoveBookAsync(string id)
        {
            await _books.DeleteOneAsync(book => book.Id == id);
        }
    }
}
