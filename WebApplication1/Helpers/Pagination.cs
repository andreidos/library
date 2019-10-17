using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helpers
{
   public class Pagination
   {
      public Pagination()
      {

      }

      public int PageNumber { get; set; } = 0;

      public int PageSize { get; set; } = 50;
   }
}
