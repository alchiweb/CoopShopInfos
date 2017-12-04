using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoopShopInfos.Models
{
    public class Shop
    {
        public int ShopId { get; set; }
        [Required, StringLength(100)]
        public string ShopName { get; set; }
        [StringLength(100)]
        public string ShopAddress { get; set; }
        [StringLength(5)]
        public string ShopPostCode { get; set; }
        [StringLength(100)]
        public string ShopCity { get; set; }
        public string ShopPhoneNumber{ get; set; }
        public ICollection<ShopProduct> ShopProduct { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
