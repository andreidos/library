using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Collections;
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
       
      public async Task AddWriter(WriterMongoDTO writer)
      {
         await libraryContext.Writers.InsertOneAsync(writer);
      }

      public async Task DeleteWriter(string id)
      {
         await libraryContext.Writers.DeleteOneAsync(Builders<WriterMongoDTO>.Filter.Eq(b => b.Id, id));
      }

      public async Task<IEnumerable<WriterMongoDTO>> GetAllWriters()
      {
         return await libraryContext.Writers.Find(_ => true).ToListAsync();
      }

      public async Task<WriterMongoDTO> FindWriter(WriterMongoDTO writer)
      {
         var writers = await GetAllWriters();
         return writers.Single(w => w.Name == writer.Name && w.BirthDate == writer.BirthDate);
      }

      public async Task<bool> UpdateWriter(WriterMongoDTO writer)
      {
         ReplaceOneResult updateResult =
              await libraryContext
                      .Writers
                      .ReplaceOneAsync(
                          filter: g => g.Id == writer.Id,
                          replacement: writer);
         return updateResult.IsAcknowledged
                 && updateResult.ModifiedCount > 0;
      }
   }
}
