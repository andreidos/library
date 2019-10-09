using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
   public class WriterMongoDTO
   {
      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }

      public string Name { get; set; }

      public DateTime BirthDate { get; set; }
   }
}
