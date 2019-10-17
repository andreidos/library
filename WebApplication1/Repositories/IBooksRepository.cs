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
      IEnumerable<BookMongoDTO> GetAllBooks();

      BookMongoDTO GetBook(string id);

      BookMongoDTO AddBook(BookMongoDTO book);

      bool UpdateBook(BookMongoDTO book);

      bool DeleteBook(string id);

   }
}
