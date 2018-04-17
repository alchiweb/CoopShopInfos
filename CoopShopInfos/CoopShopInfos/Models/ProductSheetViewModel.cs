using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CoopShopInfos.Models
{
    public class ProductSheetViewModel
    {   
        [JsonIgnore]
        public int ProductId { get; set; }
        [DisplayName("Nom du produit"), Required]
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        [DisplayName("Code barre")]
        public string BarCode { get; set; }
        public string CategoryId { get; set; }
        [DisplayName("Catégories")]
        public string Categories { get; set; }
        [DisplayName("Quantité")]
        public float Quantity { get; set; }
        [DisplayName("Unité")]
        public Unit Unit { get; set; }
        [DisplayName("Prix")]
        public decimal Price { get; set; }
        [JsonIgnore]
        public string SelectedAnswer { get; set; }

        public IList<Shop> ShopList { get; set; }
        public IList<PricesViewModel> ShopPricesList { get; set; }       
    }
}
