using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
   public interface IBooksRepository
   {
      Task<List<BookMongoDTO>> GetAllBooks();

      Task<BookMongoDTO> GetBook(string id);

      Task<BookMongoDTO> AddBook(BookMongoDTO book);

      Task<bool> UpdateBook(BookMongoDTO book);

      Task<bool> DeleteBook(string id);

   }
}
