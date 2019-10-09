using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApplication1.Configs;
using WebApplication1.Models;

namespace WebApplication1.Collections
{
   public class RemoteLibraryContext : IRemoteLibraryContext
   {
      private readonly IMongoDatabase dataBase;

      public RemoteLibraryContext(MongoDBConfig config)
      {
         var client = new MongoClient(config.ConnectionString);
         dataBase = client.GetDatabase(config.Database);
      }

      public IMongoCollection<BookMongoDTO> Books => dataBase.GetCollection<BookMongoDTO>("Books");

      public IMongoCollection<WriterMongoDTO> Writers => dataBase.GetCollection<WriterMongoDTO>("Writers");
   }
}
