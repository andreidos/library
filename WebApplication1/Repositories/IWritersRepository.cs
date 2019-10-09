using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
   public interface IWritersRepository
   {

      Task<IEnumerable<WriterMongoDTO>> GetAllWriters();

      Task<WriterMongoDTO> FindWriter(WriterMongoDTO writer);

      Task AddWriter(WriterMongoDTO writer);

      Task<bool> UpdateWriter(WriterMongoDTO writer);

      Task DeleteWriter(string id);
   }
}
