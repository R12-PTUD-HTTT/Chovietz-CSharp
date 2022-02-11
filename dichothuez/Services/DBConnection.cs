using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dichothuez.Services
{
    public class DBConnection
         
    { 
        private static MongoDB.Driver.MongoClient dbClient = null;
        private DBConnection(IConfiguration configuration)
        {
            
        }

        public static MongoDB.Driver.MongoClient getInstance(IConfiguration configuration)
        {
            if (dbClient == null)
            {
                dbClient = new MongoDB.Driver.MongoClient(configuration.GetConnectionString("dichothuezConnection"));
            }
            return dbClient;
        }
    }
}
