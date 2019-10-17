using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Collections;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class LibraryController : Controller
   {
      private readonly IBooksRepository booksRepository;
      private readonly IWritersRepository writersRepository;

      public LibraryController(IBooksRepository booksRepo, IWritersRepository writersRepo)
      {
         booksRepository = booksRepo;
         writersRepository = writersRepo;
      }

      [HttpGet]
      public async Task<ActionResult<IEnumerable<BookMongoDTO>>> GetBooks()
      {
         var books = await booksRepository.GetAllBooks();

         return Ok(books.Select((book) =>
                      CreateLinksForBook(book)));
      }

      [HttpGet("{id}", Name ="GetBook")]
      public async Task<ActionResult<BookMongoDTO>> GetBook(string id)
      {
         var result = await booksRepository.GetBook(id);
         if(result != null)
         {
            return Ok(CreateLinksForBook(result));
         }
         else{
            return NotFound("No result");
         }
      }

      [HttpPost]
      public async Task<ActionResult<BookMongoDTO>> AddBook([FromBody] BookMongoDTO book)
      {
         var author = await writersRepository.FindWriter(book.Writer.Id);
         if(author != null)
         {
            book.Writer = author;
         }
         else
         {
            var addWriterResult = writersRepository.AddWriter(book.Writer);
            if(addWriterResult == null)
            {
               return BadRequest();
            }
         }
         var addBookResult = await booksRepository.AddBook(book);
         if (addBookResult != null)
         {
            return Ok(CreateLinksForBook(addBookResult));
         }
         else
         {
            await writersRepository.DeleteWriter(book.Writer.Id);
            return BadRequest();
         }
      }

      [HttpPut(Name ="UpdateBook")]
      public async Task<IActionResult> UpdateBook(BookMongoDTO book)
      {
         var result = await booksRepository.UpdateBook(book);
         if (result)
         {
            return Ok("Resource updated!");
         }
         else
         {
            return BadRequest();
         }
      }

      [HttpPut("Writers", Name ="UpdateWriter")]
      public async Task<IActionResult> UpdateWriter([FromBody] WriterMongoDTO writer)
      {
         var result = await writersRepository.UpdateWriter(writer);
         if (result == true)
         {
            return Ok(result);
         }
         else
         {
            return BadRequest();
         }
      }

      [HttpDelete("{id}", Name ="DeleteBook")]
      public async Task<IActionResult> RemoveBook(string id)
      {
         var result = await booksRepository.DeleteBook(id);
         if (result)
         {
            return Ok("Content removed");
         }
         else
         {
            return NotFound("No resource matches the specified ID");
         }
      }

      [HttpDelete("writers/{id}", Name ="DeleteWriter")]
      public async Task<IActionResult> RemoveWriter(string id)
      {
         var result = await writersRepository.DeleteWriter(id);
         if (result)
         {
            return Ok("Content removed");
         }
         else
         {
            return NotFound("No resource matches the specified ID");
         }
      }

      [HttpGet("Writers")]
      public async Task<IActionResult> GetWriters([FromQuery] Pagination page)
      {
         if(page == null)
         {
            page = new Pagination();
         }
         var result = await writersRepository.GetAllWriters(page.PageNumber, page.PageSize);
         if(result.Count() > 0)
         {
            return Ok(result.OrderBy(w => w.Name).Select((writer) => CreateLinksForWriter(writer)));
         }
         else
         {
            return NoContent();
         }
      }

      [HttpGet("Writers/{id}", Name = "GetWriter")]
      public async Task<IActionResult> FindWriter(string id)
      {
         var result = await writersRepository.FindWriter(id);
         if(result != null)
         {
            return Ok(CreateLinksForWriter(result));
         }
         else
         {
            return NotFound("No value found");
         }
      }

      [HttpPost("Writers")]
      public IActionResult AddWriter([FromBody] WriterMongoDTO writer)
      {
         if(writer == null)
         {
            return BadRequest();
         }

         var result = writersRepository.AddWriter(writer);
         if (result != null)
         {
            return CreatedAtRoute("GetWriter", new { writer.Id }, CreateLinksForWriter(writer));
         }
         else
         {
            return BadRequest();
         }
      }

      private BookMongoDTO CreateLinksForBook(BookMongoDTO book)
      {
         book.Links.Add(new LinkDTO(Url.Link("GetBook", new { id = book.Id }), "self", "GET"));
         book.Links.Add(new LinkDTO(Url.Link("UpdateBook", null), "update_book", "PUT"));
         book.Links.Add(new LinkDTO(Url.Link("DeleteBook", new { id = book.Id }), "delete_book", "DELETE"));
         if (book.Writer != null)
         {
            book.Writer = CreateLinksForWriter(book.Writer);
         }
         return book;
      }

      private WriterMongoDTO CreateLinksForWriter(WriterMongoDTO writer)
      {
         writer.Links.Add(new LinkDTO(Url.Link("GetWriter", new { id = writer.Id }), "self", "GET"));
         writer.Links.Add(new LinkDTO(Url.Link("UpdateWriter", null), "update_writer", "PUT"));
         writer.Links.Add(new LinkDTO(Url.Link("DeleteWriter", new { id = writer.Id }), "delete_writer", "DELETE"));
         return writer;
      }
   }
}
