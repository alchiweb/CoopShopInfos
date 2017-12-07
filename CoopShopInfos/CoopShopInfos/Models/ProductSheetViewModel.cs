using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CoopShopInfos.Models
{
    public class ProductSheetViewModel
    {
        public int ProductId { get; set; }
        [DisplayName("Nom du produit"), Required]
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        [DisplayName("Code barre")]
        public string BarCode { get; set; }
        [DisplayName("Marque")]
        public string Brand { get; set; }
        [DisplayName("Catégories")]
        public string Categories { get; set; }
        [DisplayName("Quantité")]
        public float Quantity { get; set; }
        [DisplayName("Unité")]
        public Unit Unit { get; set; }
        [DisplayName("Prix")]
        public decimal Price { get; set; }
        public string SelectedAnswer { get; set; }
        public List<Shop> ShopList { get; set; }
    }
}
