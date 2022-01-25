using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dichothuez.Models
{
    public class Shipper
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string work_city { get; set; }
        public string work_district { get; set; }
    }
}
