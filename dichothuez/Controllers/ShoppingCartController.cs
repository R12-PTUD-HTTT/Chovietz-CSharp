using dichothuez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dichothuez.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {

        private readonly IConfiguration _configuration;
        public ShoppingCartController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult GetAllDataShoppingCart()
        {
            try
            {
                MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
                var cartUser = dbClient.GetDatabase("dichothuez").GetCollection<ShoppingCart_ViewModel>("user").AsQueryable().ToList();

                var lstProduct = dbClient.GetDatabase("dichothuez").GetCollection<Product>("Product").AsQueryable().ToList();

                foreach (var item in cartUser)
                {
                    var cartView = new List<Product_ViewModel>();

                    if (item.cart != null)
                    {
                        foreach (var it in item.cart)
                        {
                            var checkItem = lstProduct.FirstOrDefault(c => c.Id == it.IdProduct);

                            if (checkItem != null)
                            {
                                cartView.Add(new Product_ViewModel
                                {
                                    IdProduct = checkItem.Id,
                                    ItemName = checkItem.name,
                                    quantity = it.quantity,
                                    price = checkItem.price,
                                    saleprice = checkItem.sale_price,
                                    imagelink = checkItem.image_link
                                });
                            }
                        }
                    }
                    item.lstCartView = cartView;
                    item.cart = null;
                }
                return new JsonResult(cartUser);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public JsonResult GetDataShoppingCartById(string Id)
        {
            try
            {
                MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

                var cartUser = dbClient.GetDatabase("dichothuez").GetCollection<ShoppingCart_ViewModel>("user").AsQueryable().FirstOrDefault(c => c.Id == Id);
                //Lấy danh sách sản phẩm

                var lstProduct = dbClient.GetDatabase("dichothuez").GetCollection<Product>("Product").AsQueryable().ToList();

                var cartView = new List<Product_ViewModel>();

                if (cartUser.cart != null)
                {
                    foreach (var item in cartUser.cart)
                    {
                        var checkItem = lstProduct.FirstOrDefault(c => c.Id == item.IdProduct);
                        if (checkItem != null)
                        {
                            cartView.Add(new Product_ViewModel
                            {
                                IdProduct = checkItem.Id,
                                ItemName = checkItem.name,
                                quantity = item.quantity,
                                price = checkItem.price,
                                saleprice = checkItem.sale_price,
                                imagelink = checkItem.image_link
                            });
                        }
                    }
                }
                cartUser.lstCartView = cartView;
                cartUser.cart = null;
                return new JsonResult(cartUser);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }
        [HttpGet]
        public JsonResult FindProductByKey(string name, double fromPrice, double toPrice, string type)
        {
            try
            {
                MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

                var filter = Builders<Product>.Filter.Or(Builders<Product>.Filter.Gte("price", fromPrice) & Builders<Product>.Filter.Lte("price", toPrice))
                                                           & Builders<Product>.Filter.Eq("type", type) & Builders<Product>.Filter.Regex("name", new BsonRegularExpression(name, "i"));

                var products = dbClient.GetDatabase("dichothuez").GetCollection<Product>("Product").Find(filter).ToList();
                return new JsonResult(products);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult InsertShopCart(string IdUser, string IdProduct, int quantity)
        {
            try
            {
                MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));
                var lstCartNew = new List<Product_ViewModel>();

                if (string.IsNullOrEmpty(IdUser))
                {
                    return new JsonResult("User not exit");
                }
                var filter = Builders<ShoppingCart_ViewModel>.Filter.Eq("Id", IdUser);
                //Lấy danh sản phẩm của 1 user

                var checkExit = dbClient.GetDatabase("dichothuez").GetCollection<ShoppingCart_ViewModel>("user").AsQueryable().FirstOrDefault(c => c.Id == IdUser);
                if (checkExit == null)
                {
                    return new JsonResult("Not found user");
                }

                var checkExitProduct = dbClient.GetDatabase("dichothuez").GetCollection<Product>("Product").AsQueryable().FirstOrDefault(c => c.Id == IdProduct);
                if (checkExitProduct == null)
                {
                    return new JsonResult("Product not exists");
                }

                var lstCart = new List<CartItem_ViewModel>()
                {
                    new CartItem_ViewModel()
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        IdProduct = IdProduct,
                        quantity = quantity
                    }
                };

                if (checkExit.cart != null)
                {
                    if (checkExit.cart.Any(c => c.IdProduct == IdProduct))
                    {
                        return new JsonResult("Product already exists");
                    }
                    lstCart.AddRange(checkExit.cart);
                }


                var update = Builders<ShoppingCart_ViewModel>.Update.Set("cart", lstCart);
                dbClient.GetDatabase("dichothuez").GetCollection<ShoppingCart_ViewModel>("user").UpdateOne(filter, update);
                return new JsonResult("Insert Successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }

        [HttpGet]
        public JsonResult UpdateShopCart(string IdUser, string IdProduct, int quantity)
        {
            try
            {
                if (string.IsNullOrEmpty(IdUser))
                {
                    return new JsonResult("Product not exit");
                }
                MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));

                var checkExitProduct = dbClient.GetDatabase("dichothuez").GetCollection<Product>("Product").AsQueryable().FirstOrDefault(c => c.Id == IdProduct);
                if (checkExitProduct == null)
                {
                    return new JsonResult("Product not exists");
                }


                var filter = Builders<ShoppingCart_ViewModel>.Filter.And(
                                         Builders<ShoppingCart_ViewModel>.Filter.Where(c => c.Id == IdUser),
                                         Builders<ShoppingCart_ViewModel>.Filter.Eq("cart.IdProduct", IdProduct));

                //Lấy danh sản phẩm của 1 user

                var update = Builders<ShoppingCart_ViewModel>.Update.Set("cart.$.quantity", quantity);

                dbClient.GetDatabase("dichothuez").GetCollection<ShoppingCart_ViewModel>("user").UpdateOne(filter, update);

                return new JsonResult("Update Successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult DeleteShopCart(string idUser, string IdProduct)
        {
            try
            {
                MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("dichothuezConnection"));



                var filter = Builders<ShoppingCart_ViewModel>.Filter.Eq(person => person.Id, idUser);

                var update = Builders<ShoppingCart_ViewModel>.Update.PullFilter(p => p.cart, f => f.IdProduct == IdProduct);

                var products = dbClient.GetDatabase("dichothuez").GetCollection<ShoppingCart_ViewModel>("user").UpdateOne(filter, update);

                return new JsonResult("Delete Successfully");

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }
    }
}
