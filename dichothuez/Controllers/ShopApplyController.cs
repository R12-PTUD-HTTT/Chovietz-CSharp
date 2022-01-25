using Microsoft.AspNetCore.Mvc;
using dichothuez.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace dichothuez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopApplyController : Controller
    {
        private readonly IConfiguration _configuration;

        public ShopApplyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
            var ShopApply = dbClient.GetDatabase("dichothuez").GetCollection<ShopApply>("ShopApply").AsQueryable();
            return new JsonResult(ShopApply);
        }
        [HttpPost]
        public JsonResult Post(ShopApply form)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

            dbClient.GetDatabase("dichothuez").GetCollection<ShopApply>("ShopApply").InsertOne(form);

            var result = new JsonResult("Added Successfully");
            result.StatusCode = 201;
            return result;
        }
    }
}
