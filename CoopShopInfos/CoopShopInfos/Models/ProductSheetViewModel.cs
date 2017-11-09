using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CoopShopInfos.Models
{
    public class ProductSheetViewModel
    {
        [DisplayName("Nom du produit")]
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        [DisplayName("Code barre")]
        public string BarCode { get; set; }
        public string Brand { get; set; }
        public string Categories { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int ShopId { get; set; }
        public string ShopName{ get; set; }
    }
}
