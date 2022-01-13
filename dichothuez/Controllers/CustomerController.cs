using dichothuez.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dichothuez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

            var CusList = dbClient.GetDatabase("dichothuez").GetCollection<Customer>("Customer").AsQueryable();

            return new JsonResult(CusList);
        }

        [HttpPost]
        public JsonResult Post(Customer cus)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

            dbClient.GetDatabase("dichothuez").GetCollection<Customer>("Customer").InsertOne(cus);

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Customer cus)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

            var filter = Builders<Customer>.Filter.Eq("Name", cus.Name);

            var update = Builders<Customer>.Update.Set("Sdt", cus.Sdt);

            dbClient.GetDatabase("dichothuez").GetCollection<Customer>("Customer").UpdateOne(filter, update);

            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{name}")]
        public JsonResult Delete(string name)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

            var filter = Builders<Customer>.Filter.Eq("Name", name);

            dbClient.GetDatabase("dichothuez").GetCollection<Customer>("Customer").DeleteOne(filter);

            return new JsonResult("Deleted Successfully");
        }

    }
}
