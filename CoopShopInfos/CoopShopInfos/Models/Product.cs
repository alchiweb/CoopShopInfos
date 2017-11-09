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
        public ICollection<ShopProduct> ShopProduct { get; set; }


    }
}
