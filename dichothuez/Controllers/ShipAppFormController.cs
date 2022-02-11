using dichothuez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dichothuez.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipAppFormController : Controller
    {
        private readonly IConfiguration _configuration;

        public ShipAppFormController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
            var shipAppForm = dbClient.GetDatabase("dichothuez").GetCollection<Shipping_Application_Form>("Shipping_Application_Form").AsQueryable();
            return new JsonResult(shipAppForm);
        }
        [HttpPost]
        public JsonResult Post(Shipping_Application_Form form)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

            dbClient.GetDatabase("dichothuez").GetCollection<Shipping_Application_Form>("Shipping_Application_Form").InsertOne(form);

            var result = new JsonResult("Added Successfully");
            result.StatusCode = 201;
            return result;
        }
    }
}
