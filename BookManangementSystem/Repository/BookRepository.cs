using BookManangementSystem.Data;
using BookManangementSystem.Models;
using BookManangementSystem.Repository.Defination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookManangementSystem.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext db;
        public BookRepository(BookDbContext db)
        {
            this.db = db;
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            try
            {
                Book exist = await this.db.TblBooks.FirstOrDefaultAsync(ob => ob.Name.Equals(book.Name));
                if (exist != null)
                {
                    throw new Exception("Book with the same name already exists.");
                }
                EntityEntry<Book> b1 = await this.db.TblBooks.AddAsync(book);
                await this.db.SaveChangesAsync();
                return b1.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); 

            }
            return null;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            bool isDeleted = false;
           Book exist= await this.db.TblBooks.FirstOrDefaultAsync(ob => ob.Id == id);
            if (exist != null)
            {
                this.db.TblBooks.Remove(exist);
               isDeleted= await this.db.SaveChangesAsync() > 0;

            }
            return isDeleted;
        }

        public async Task<IList<Book>> GetAllBooksAsync()
        {
          var res= await this.db.TblBooks.ToListAsync();
            return res;
        }

        public async Task<Book> UpdateBookAsync(int id,Book book)
        {
           Book exist=await this.db.TblBooks.FirstOrDefaultAsync(ob=> ob.Id==id);
            if (exist != null)
            {
                exist.Name = book.Name;
                exist.Price = book.Price;
                exist.Author = book.Author;

                this.db.TblBooks.Update(exist);
                await this.db.SaveChangesAsync();
                
            }
            return exist;
        }
    }
}
