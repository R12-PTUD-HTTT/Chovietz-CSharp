using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dichothuez.Models
{
    public class ShoppingCart_ViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string cmnd { get; set; }
        public string phoneNumber { get; set; }
        public List<CartItem_ViewModel> cart { get; set; }

        public List<Product_ViewModel> lstCartView { get; set; }

        public DateTime date_of_birth { get; set; }
        public string status { get; set; }
        public string _class { get; set; }
        public Store_ViewModel store { get; set; }
        public string rolename { get; set; }
    }

    public class Product_ViewModel
    {
        public string IdProduct { get; set; }
        public string ItemName { get; set; }
        public string imagelink { get; set; }
        public double saleprice { get; set; }

        public double price { get; set; }
        public int quantity { get; set; }
    }

    public class CartItem_ViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdProduct { get; set; }
        public int quantity { get; set; }
    }


    public class Store_ViewModel
    {
        public string Id { get; set; }
        public string store_name { get; set; }
        public string phone_number { get; set; }
    }
}
