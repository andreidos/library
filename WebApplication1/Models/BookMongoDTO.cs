using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
   public class BookMongoDTO
   {
      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }

      public string Title { get; set; }

      public int NumberOfPages { get; set; }

      public WriterMongoDTO Writer { get; set; }
   }
}
