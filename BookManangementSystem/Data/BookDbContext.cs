using BookManangementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManangementSystem.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }
        public DbSet<Book> TblBooks { get; set; }
    }
}
