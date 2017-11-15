using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CoopShopInfos.Models
{
    public class ProductSheetViewModel
    {
        public int ProductID { get; set; }
        [DisplayName("Nom du produit"), Required]
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        [DisplayName("Code barre")]
        public string BarCode { get; set; }
        public string Brand { get; set; }
        public string Categories { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string SelectedAnswer { get; set; }
        public List<Shop> ShopList { get; set; }
    }
}
