using dichothuez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.Core;

namespace dichothuez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Read()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
            var products = dbClient.GetDatabase("dichothuez").GetCollection<Product>("Product").AsQueryable();
            return new JsonResult(products);
        }

        [HttpGet("{id}", Name = "ReadDetail")]
        public ActionResult ReadDetail(string id)
        {
            var productID = new MongoDB.Bson.ObjectId(id);
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
            var products = dbClient.GetDatabase("dichothuez").GetCollection<Product>("Product").AsQueryable().SingleOrDefault(product => product._id == productID);

            return new JsonResult(products);
        }


        [HttpPost]
        public ActionResult Create(Product product)
        {
            
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

            dbClient.GetDatabase("dichothuez").GetCollection<Product>("Product").InsertOne(product);

            return new JsonResult("Added Successfully");

        }

        [HttpPut]
        public ActionResult Update(Product product)
        {
            var productID = product._id;
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

            dbClient.GetDatabase("dichothuez").GetCollection<Product>("Product").ReplaceOne(product => product._id == productID, product);
            var result = new JsonResult("Update Successfully");
            result.StatusCode = 201;
            return result;
        }

        [HttpPost("{id}")]
        public ActionResult Delete(string id)
        {
            var productID = new MongoDB.Bson.ObjectId(id);
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
            var products = dbClient.GetDatabase("dichothuez").GetCollection<Product>("Product").DeleteOne(product => product._id == productID);
            
            var result = new JsonResult("Delete Successfully");
            result.StatusCode = 201;
            return result;
        }
    }
   
}
