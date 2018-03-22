using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoopShopInfos.Models
{
    public class PricesViewModel
    {
        public int ShopId { get; set; }
        public ICollection<Price> Prices { get; set; }
    }
}
