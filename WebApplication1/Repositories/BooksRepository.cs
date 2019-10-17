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
      public async Task<BookMongoDTO> AddBook(BookMongoDTO book)
      {
         try
         {
            await libraryContext.Books.InsertOneAsync(book);
            return book;
         }
         catch (Exception)
         {
            return null;
         }
      }

      public async Task<bool> DeleteBook(string id)
      {
         var result = await libraryContext.Books.DeleteOneAsync(Builders<BookMongoDTO>.Filter.Eq(b => b.Id, id));
         return result.IsAcknowledged && result.DeletedCount > 0;
      }

      public Task<List<BookMongoDTO>> GetAllBooks()
      {
         return libraryContext.Books.Find(_ => true).ToListAsync();
      }

      public async Task<BookMongoDTO> GetBook(string id)
      {
         var filter = Builders<BookMongoDTO>.Filter.Eq("Id", id);

         try
         {
            return await libraryContext.Books
                            .Find(filter)
                            .FirstOrDefaultAsync();
         }
         catch (Exception)
         {
            return null;
         }
      }

      public async Task<bool> UpdateBook(BookMongoDTO book)
      {
         var updateResult = await
              libraryContext
                      .Books
                      .ReplaceOneAsync(
                          filter: g => g.Id == book.Id,
                          replacement: book);
         return updateResult.IsAcknowledged
                 && updateResult.ModifiedCount > 0;
      }
   }
}
