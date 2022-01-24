using dichothuez.Models;
using dichothuez.Services;
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
    public class StoreController : Controller
    {
        private readonly IConfiguration _configuration;
        public StoreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            MongoClient dbClient = DBConnection.getInstance(_configuration);
            var store = dbClient.GetDatabase("dichothuez").GetCollection<Store>("store").AsQueryable().Where(c => c.Id == id); ;
            return new JsonResult(store);
        }
    }
}
