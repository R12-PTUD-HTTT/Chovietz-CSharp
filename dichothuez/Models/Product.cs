using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace dichothuez.Models
{
    public class Product
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int sale_price { get; set; }
        public string type { get; set; }
        public string image_link { get; set; }
        public int stock { get; set; }
        public object rate { get; set; }
        public string description { get; set; }
    }
}
