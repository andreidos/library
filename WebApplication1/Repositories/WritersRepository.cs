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
       
      public async Task<string> AddWriter(WriterMongoDTO writer)
      {
         try
         {
            var findQuery = await libraryContext.Writers.Find(w => w.Name == writer.Name && w.BirthDate == writer.BirthDate).ToListAsync();

            if(findQuery.Count() > 0)
            {
               return null;
            }

            await libraryContext.Writers.InsertOneAsync(writer);
            return writer.Id;
         }
         catch (Exception e)
         {
            return null;
         }
      }

      public async Task<bool> DeleteWriter(string id)
      {
         var result = await libraryContext.Writers.DeleteOneAsync(Builders<WriterMongoDTO>.Filter.Eq(b => b.Id, id));
         return result.IsAcknowledged && result.DeletedCount > 0;
      }

      public async Task<PagedList<WriterMongoDTO>> GetAllWriters(int page, int pageSize)
      {
         //return libraryContext.Writers.Find(_ => true).ToList().OrderBy(a => a.Name).Skip(page);
         var initialCollection = await libraryContext.Writers.Find(_ => true).ToListAsync();

         return PagedList<WriterMongoDTO>.Create(initialCollection, page, pageSize);
      }

      public async Task<WriterMongoDTO> FindWriter(string id)
      {
         var filter = Builders<WriterMongoDTO>.Filter.Eq("Id", id);

         try
         {
            return await libraryContext.Writers
                            .Find(filter)
                            .FirstOrDefaultAsync();
         }
         catch (Exception)
         {
            return null;
         }
      }

      public async Task<bool> UpdateWriter(WriterMongoDTO writer)
      {
         ReplaceOneResult updateResult = await
              libraryContext
                      .Writers
                      .ReplaceOneAsync(
                          filter: g => g.Id == writer.Id,
                          replacement: writer);
         return updateResult.IsAcknowledged
                 && updateResult.ModifiedCount > 0;
      }
   }
}
