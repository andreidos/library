using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
   public interface IWritersRepository
   {
      PagedList<WriterMongoDTO> GetAllWriters(int page, int pageSize);

      WriterMongoDTO FindWriter(string id);

      string AddWriter(WriterMongoDTO writer);

      bool UpdateWriter(WriterMongoDTO writer);

      bool DeleteWriter(string id);
   }
}
