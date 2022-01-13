using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace dichothuez.Models
{
    public class Shipping_Application_Form
    {
        public ObjectId _id { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string fullname { get; set; }
        public string gender { get; set; }
        public string tel { get; set; }
        public string Work_area { get; set; }
    }
}
