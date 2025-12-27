using BookManangementSystem.Models;

namespace BookManangementSystem.Repository.Defination
{
    public interface IBookRepository
    {
        Task<IList<Book> > GetAllBooksAsync();
       // Task<Book> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(int id,Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}
