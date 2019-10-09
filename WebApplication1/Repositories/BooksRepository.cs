using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApplication1.Collections;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
   public class BooksRepository : IBooksRepository
   {
      private readonly RemoteLibraryContext libraryContext;

      public BooksRepository(RemoteLibraryContext context)
      {
         libraryContext = context;
      }
      public async Task AddBook(BookMongoDTO book)
      {
         await libraryContext.Books.InsertOneAsync(book);
      }

      public async Task DeleteBook(string id)
      {
         await libraryContext.Books.DeleteOneAsync(Builders<BookMongoDTO>.Filter.Eq(b => b.Id, id));
      }

      public async Task<IEnumerable<BookMongoDTO>> GetAllBooks()
      {
         return await libraryContext.Books.Find(_ => true).ToListAsync();
      }

      public async Task<bool> UpdateBook(BookMongoDTO book)
      {
         ReplaceOneResult updateResult =
              await libraryContext
                      .Books
                      .ReplaceOneAsync(
                          filter: g => g.Id == book.Id,
                          replacement: book);
         return updateResult.IsAcknowledged
                 && updateResult.ModifiedCount > 0;
      }
   }
}
