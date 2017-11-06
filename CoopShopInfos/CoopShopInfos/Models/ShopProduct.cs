﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoopShopInfos.Models
{
    public class ShopProduct
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public decimal Price { get; set; }
    }
}