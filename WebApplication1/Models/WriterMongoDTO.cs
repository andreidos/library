using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helpers;

namespace WebApplication1.Models
{
   public class WriterMongoDTO : LinkedResourceBaseDTO
   {
      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }

      [Required]
      public string Name { get; set; }

      [Required]
      public DateTime BirthDate { get; set; }
   }
}
