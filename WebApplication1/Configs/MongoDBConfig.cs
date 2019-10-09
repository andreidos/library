using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Configs
{
   public class MongoDBConfig
   {
      public string Database { get; set; }
      public string Host { get; set; }
      public int Port { get; set; }
      public string User { get; set; }
      public string Password { get; set; }
      public string ConnectionString
      {
         get
         {
            if (User != null && Password != null)
            {
               return $@"mongodb://{User}:{Password}@{Host}:{Port}/?authSource=admin";
            }
            else
            {
               return $@"mongodb://{Host}:{Port}";
            }
         }
      }
   }
}
