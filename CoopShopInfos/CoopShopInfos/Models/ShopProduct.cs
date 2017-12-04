using System;

namespace CoopShopInfos.Models
{
    public class ShopProduct
    {
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public decimal Price { get; set; }
        //public DateTime Time { get; set; }
    }
}
