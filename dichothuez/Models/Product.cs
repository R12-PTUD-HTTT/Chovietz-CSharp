using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dichothuez.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int sale_price { get; set; }
        public string type { get; set; }
        public string image_link { get; set; }
        public int stock { get; set; }
        public int quantity { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
        public string store_id { get; set; }
        public string store_name { get; set; }
        public string store_phone_number { get; set; }

    }
}
