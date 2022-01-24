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
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string customerID { get; set; }
        public Customer customer { get; set; }
        public string shipperID { get; set; }
        public Shipper shipper { get; set; }
        public string shopID { get; set; }
        public Store shop { get; set; }
        public Receiver receiver { get; set; }
        public string payment_type { get; set; }
        public string is_paid { get; set; }
        public object[] product { get; set; }
        public string status { get; set; }
        public string typeOrder { get; set; }
        public string total_price { get; set; }
        public int month { get; set; }
        public int quater { get; set; }
        public int year { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set; }
    }

}
