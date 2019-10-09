using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Collections;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class LibraryController : ControllerBase
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
         return new ObjectResult(await booksRepository.GetAllBooks());
      }

      [HttpPost]
      public async Task AddBook(BookMongoDTO book)
      {
         var writers = await writersRepository.GetAllWriters();
         var author = writers.ToList().Find(w => w.Name == book.Writer.Name);
         if(author != null)
         {
            book.Writer = author;
         }
         else
         {
            await writersRepository.AddWriter(book.Writer);
            var newWriter = writers.ToList().Find(w => w.Name == book.Writer.Name);
            book.Writer = newWriter;
         }
         await booksRepository.AddBook(book);
      }

      [HttpPut]
      public async Task UpdateBook(BookMongoDTO book)
      {
         await booksRepository.UpdateBook(book);
      }

      [HttpDelete]
      [Route("{id}")]
      public async Task RemoveBook(string id)
      {
         await booksRepository.DeleteBook(id);
      }

      [HttpGet]
      [Route("Writers")]
      public async Task<ActionResult<IEnumerable<WriterMongoDTO>>> GetWriters()
      {
         return new ObjectResult(await writersRepository.GetAllWriters());
      }

      [HttpGet]
      [Route("Writers/{id}")]
      public async Task<ActionResult<WriterMongoDTO>> FindWriter(WriterMongoDTO writer)
      {
         return new ObjectResult(await writersRepository.FindWriter(writer));
      }

      [HttpPost]
      [Route("Writers")]
      public async Task AddWriter(WriterMongoDTO writer)
      {
         await writersRepository.AddWriter(writer);
      }
   }
}
