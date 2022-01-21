using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dichothuez.Models
{
    public class Store
    {
        [BsonId]
        public string _id { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set; }
        public string store_name { get; set; }
        public string phone_number { get; set; }
        public string business_licences { get; set; }
        public string address { get; set; }
        public string avatar { get; set; }
        public string cover_image { get; set; }
    }
}
