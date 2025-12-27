using System.ComponentModel.DataAnnotations;

namespace BookManangementSystem.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}
