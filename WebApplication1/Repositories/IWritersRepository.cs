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
      Task<PagedList<WriterMongoDTO>> GetAllWriters(int page, int pageSize);

      Task<WriterMongoDTO> FindWriter(string id);

      Task<string> AddWriter(WriterMongoDTO writer);

      Task<bool> UpdateWriter(WriterMongoDTO writer);

      Task<bool> DeleteWriter(string id);
   }
}
