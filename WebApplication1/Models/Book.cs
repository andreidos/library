using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
   public class Book
   {
      public Book()
      {
         Id = Guid.NewGuid();
      }
      public Guid Id { get; set; }

      public string Title { get; set; }

      public int NumberOfPages { get; set; }

      public Writer Writer { get; set; }
   }
}
