using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Collections
{
   public static class LibraryCollection
   {

      public static IEnumerable<Writer> Writers { get; set; }
      public static IEnumerable<Book> Books { get; set; }

      static LibraryCollection()
      {
         Writers = new Writer[] 
         {
            new Writer(){Name ="George", BirthDate=new DateTime(1976, 5, 23)},
            new Writer(){Name="Peterson", BirthDate=new DateTime(1966, 8, 13)},
         };
         Books = new Book[]
         {
            new Book(){Title = "Wardstone", NumberOfPages=344, Writer=Writers.First(a => a.Name=="George")},
            new Book(){Title = "Rules", NumberOfPages=567, Writer=Writers.First(a => a.Name=="Peterson")}
         };
      }
   }
}
