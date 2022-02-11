using dichothuez.Models;
using dichothuez.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dichothuez.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShipperController : Controller
    {
        private readonly IConfiguration _configuration;

        public ShipperController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetShipper(string workcity, string workdistrict)
        {
            MongoClient dbClient = DBConnection.getInstance(_configuration);


            var filter = Builders<Shipper>.Filter.And(
                                     Builders<Shipper>.Filter.Where(c => c.work_city == workcity),
                                     Builders<Shipper>.Filter.Eq("work_district", workdistrict));

            var order = dbClient.GetDatabase("dichothuez").GetCollection<Shipper>("User").Find(filter).ToList();
            return new JsonResult(order);
        }


    }
}