using dichothuez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            
            var order = dbClient.GetDatabase("dichothuez").GetCollection<Order>("Order").AsQueryable().Where(c => c.Id == id); ;
            return new JsonResult(order.ToString());
        }

        [HttpPost]
        public ActionResult Create(Order order)
        {
            try
            {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
            if(order.shipper != null ) order.shipperID = order.shipper.Id.ToString();
            if (order.shipper != null)  order.customerID = order.customer.Id.ToString();
            if (order.shipper != null) order.shopID = order.shop.Id;

            DateTime date = new DateTime();
            order.created_date = date;
            order.month = DateTime.Now.Date.Month;
            order.quater = DateTime.Now.Date.Month / 4 + 1;
            order.year = DateTime.Now.Date.Year;

            dbClient.GetDatabase("dichothuez").GetCollection<Order>("Order").InsertOne(order);

            return new JsonResult(order);
            }
            catch (Exception ) {
                var result = new JsonResult("Có lỗi trong quá trình tạo");
                result.StatusCode = 500;
                return result;
            }
          

        }
    }
}
