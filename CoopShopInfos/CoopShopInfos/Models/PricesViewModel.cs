using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoopShopInfos.Models
{
    public class PricesViewModel
    {
        [JsonIgnore]
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public ICollection<Price> Prices { get; set; }
    }
}
