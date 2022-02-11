using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace dichothuez.Models
{
    public class ShopApply
    {
        public ObjectId _id { get; set; }
        public string hoten { get; set; }
        public string email { get; set; }
        public string cmnd { get; set; }
        public string sdt { get; set; }
        public string ngaysinh { get; set; }
        public string diachi { get; set; }
        public Boolean hopdong { get; set; }
    }
}
