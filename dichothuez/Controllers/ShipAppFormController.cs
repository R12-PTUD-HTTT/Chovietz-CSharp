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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
            var shipAppForm = dbClient.GetDatabase("dichothuez").GetCollection<Shipping_Application_Form>("Shipping_Application_Form").AsQueryable();
            return new JsonResult(shipAppForm);
        }
    }
}
