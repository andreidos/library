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
      Task<IEnumerable<BookMongoDTO>> GetAllBooks();

      Task AddBook(BookMongoDTO book);

      Task<bool> UpdateBook(BookMongoDTO book);

      Task DeleteBook(string id);

   }
}
