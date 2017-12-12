using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoopShopInfos.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required, StringLength(13)]
        public string Barcode { get; set; }
        [Required, StringLength(100)]
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public float Quantity { get; set; }     
        public Unit Unit { get; set; }
        public ICollection<CategoryProduct> CategoryProduct { get; set; }    = new List<CategoryProduct>();   
        public ICollection<ShopProduct> ShopProduct { get; set; }    = new List<ShopProduct>();
    }
}
