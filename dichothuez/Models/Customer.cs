using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dichothuez.Models
{
    public class Customer
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime Date_of_birth { get; set; }
        public String email { get; set; }
        public String PhoneNumber { get; set; }
        public String Gender { get; set; }
        public List<Cart> cart { get; set; }
        public Receiver receiver_infor { get; set; }

    }
}
