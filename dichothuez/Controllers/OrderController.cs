using dichothuez.Models;
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
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
            var order = dbClient.GetDatabase("dichothuez").GetCollection<Order>("Order").AsQueryable();
            return new JsonResult(order);
        }
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
            var order = dbClient.GetDatabase("dichothuez").GetCollection<Order>("Order").AsQueryable().Where(c => c._id == id); ;
            return new JsonResult(order);
        }

        [HttpPut]
        public ActionResult Update(Order order)
        {
            var orderID = order._id;
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

            dbClient.GetDatabase("dichothuez").GetCollection<Order>("Order").ReplaceOne(order => order._id == orderID, order);
            var result = new JsonResult("Update Successfully");
            result.StatusCode = 201;
            return result;
        }
    }
}
