using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Collections;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{

   public class WritersRepository : IWritersRepository
   {
      private readonly RemoteLibraryContext libraryContext;

      public WritersRepository(RemoteLibraryContext context)
      {
         libraryContext = context;
      }
       
      public string AddWriter(WriterMongoDTO writer)
      {
         try
         {
            var findQuery = libraryContext.Writers.Find(w => w.Name == writer.Name && w.BirthDate == writer.BirthDate);
            if(findQuery.CountDocuments() > 0)
            {
               return null;
            }
            libraryContext.Writers.InsertOne(writer);
            return writer.Id;
         }
         catch (Exception e)
         {
            return null;
         }
      }

      public bool DeleteWriter(string id)
      {
         var result = libraryContext.Writers.DeleteOne(Builders<WriterMongoDTO>.Filter.Eq(b => b.Id, id));
         return result.IsAcknowledged && result.DeletedCount > 0;
      }

      public PagedList<WriterMongoDTO> GetAllWriters(int page, int pageSize)
      {
         //return libraryContext.Writers.Find(_ => true).ToList().OrderBy(a => a.Name).Skip(page);
         var initialCollection = libraryContext.Writers.Find(_ => true).SortBy(w=>w.Name).ToList();

         return PagedList<WriterMongoDTO>.Create(initialCollection, page, pageSize);
      }

      public WriterMongoDTO FindWriter(string id)
      {
         return libraryContext.Writers.Find(w => w.Id == id).SingleOrDefault();
      }

      public bool UpdateWriter(WriterMongoDTO writer)
      {
         ReplaceOneResult updateResult =
              libraryContext
                      .Writers
                      .ReplaceOne(
                          filter: g => g.Id == writer.Id,
                          replacement: writer);
         return updateResult.IsAcknowledged
                 && updateResult.ModifiedCount > 0;
      }
   }
}
