using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helpers
{
   public class PagedList<T>: List<T>
   {
      public int CurrentPage { get; private set; }
      public int TotalPages { get; private set; }
      public int PageSize { get; private set; }
      public int TotalCount { get; private set; }

      public bool hasPrevious {
         get
         {
            return (CurrentPage > 1);
         }
      }

      public bool hasNext
      {
         get
         {
            return (CurrentPage < TotalPages);
         }
      }

      public PagedList(List<T> items, int count, int pageNumber, int pageSize)
      {
         TotalCount = count;
         PageSize = pageSize;
         CurrentPage = pageNumber;
         TotalPages = (int)Math.Ceiling(count / (double)pageSize);
         AddRange(items);
      }

      public static PagedList<T> Create(List<T> source, int pageNumber, int pageSize)
      {
         var count = source.Count();
         var items = source.Skip((pageNumber) * pageSize).Take(pageSize).ToList();
         return new PagedList<T>(items, count, pageNumber, pageSize);
      }
   }
}
