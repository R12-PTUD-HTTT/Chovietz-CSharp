using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dichothuez.Models
{
    public class ReturnOrder
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public string _id { get; set; }
        public DateTime created_date { get; set; }
        public object order { get; set; }
        public object shipper { get; set; }
        public object shop { get; set; }
        public string reason_return { get; set; }

    }
}
