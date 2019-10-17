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
      public BookMongoDTO AddBook(BookMongoDTO book)
      {
         try
         {
            libraryContext.Books.InsertOne(book);
            return book;
         }
         catch (Exception)
         {
            return null;
         }
      }

      public bool DeleteBook(string id)
      {
         var result = libraryContext.Books.DeleteOne(Builders<BookMongoDTO>.Filter.Eq(b => b.Id, id));
         return result.IsAcknowledged && result.DeletedCount > 0;
      }

      public IEnumerable<BookMongoDTO> GetAllBooks()
      {
         return libraryContext.Books.Find(_ => true).ToList().OrderBy(b=>b.Title);
      }

      public BookMongoDTO GetBook(string id)
      {
         return libraryContext.Books.Find(b => b.Id == id).FirstOrDefault();
      }

      public bool UpdateBook(BookMongoDTO book)
      {
         ReplaceOneResult updateResult =
              libraryContext
                      .Books
                      .ReplaceOne(
                          filter: g => g.Id == book.Id,
                          replacement: book);
         return updateResult.IsAcknowledged
                 && updateResult.ModifiedCount > 0;
      }
   }
}
