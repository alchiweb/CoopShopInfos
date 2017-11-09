using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CoopShopInfos.Models
{
    public class ProductSheetViewModel
    {
        //[DataMember(Name = "product.product_name_fr")]
        public string ProductName { get; set; }
        //[DataMember(Name = "product.image_url")]
        public string ImageUrl { get; set; }
        //[DataMember(Name = "product.code")]
        public string BarCode { get; set; }
        //[DataMember(Name = "product.brands")]
        public string Brand { get; set; }
        //[DataMember(Name = "product.categories")]
        public string Categories { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int ShopId { get; set; }

        public string ShopName{ get; set; }
    }
}
