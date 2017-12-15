using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoopShopInfos.Models
{
    public class Price
    {
        public int PriceId { get; set; }
        public decimal PriceAmount { get; set; }
        public DateTime PriceDateTime { get; set; }
        public ICollection<ShopProduct> ShopProduct { get; set; }
    }
}
