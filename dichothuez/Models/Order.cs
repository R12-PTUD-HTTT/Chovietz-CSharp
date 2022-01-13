using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dichothuez.Models
{
    public class Order
    {
        [BsonId]
        public string _id { get; set; }
        public DateTime created_date { get; set; }
        public object customer { get; set; }
        public object shipper { get; set; }
        public object shop { get; set; }
        public string delivery_address { get; set; }
        public string payment_type { get; set; }
        public string is_paid { get; set; }
        public object[] product { get; set; }
        public string status { get; set; }
        public string total_cost { get; set; }
        public int month { get; set; }
        public int quater { get; set; }
        public int year { get; set; }
    }

}
