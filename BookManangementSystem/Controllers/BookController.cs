using BookManangementSystem.Dto;
using BookManangementSystem.Helper;
using BookManangementSystem.Models;
using BookManangementSystem.Repository.Defination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookManangementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository repo;
        public BookController(IBookRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet("GetById{id}")]
        public async Task<IActionResult> GetBookByID(int id)
        {
           var lists=await this.repo.GetAllBooksAsync();
           var book= lists.FirstOrDefault(ob=> ob.Id==id);
            return book != null ?
                  Ok(ApiResponce<Book>.Success("Data Fetch ", book)) :
                  NotFound(ApiResponce<string>.Failure("Data not Found"));
        }
        [HttpDelete("delete{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           bool isDeleted=  await this.repo.DeleteBookAsync(id);
            return isDeleted ?
                  Ok(ApiResponce<int>.Success("Data Deleted ", id)) :
                  BadRequest(ApiResponce<string>.Failure("Data Not Deleted"));
                
        }
        [HttpPut]
        public async Task<ActionResult> Update(int id , [FromBody] BookDto b)
        {
            Book update= new Book
            {
                Name= b.Name,
                Author= b.Author,
                Price= b.Price
            };
           var res= await this.repo.UpdateBookAsync(id, update);
          return  res!=null ?
                Ok(ApiResponce<Book>.Success("Book Updated Successfully",res)) :
                NotFound(ApiResponce<string>.Failure("Book Not Found"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
          IList<Book> books=  await this.repo.GetAllBooksAsync();
          return  books.Count>0 ? 
                Ok(ApiResponce<IList<Book>>.Success("Data Fetch",books)) : 
                NotFound(ApiResponce<string>.Failure("Data Not Found"));
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            Book b1 = await this.repo.AddBookAsync(book);
            return b1 != null ?
                Ok(ApiResponce<Book>.Success("Book Added Successfully", b1)) :
                BadRequest(ApiResponce<string>.Failure("Book Not Added"));
        }

    }
}
