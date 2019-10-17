using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helpers
{
   public abstract class LinkedResourceBaseDTO
   {
      public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
   }
}
