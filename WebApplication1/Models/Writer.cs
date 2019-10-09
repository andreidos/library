using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
   public class Writer
   {
      public Writer()
      {
         Id = Guid.NewGuid();
      }
      public Guid Id { get; set; }

      public string Name { get; set; }

      public DateTime BirthDate { get; set; }
   }
}
