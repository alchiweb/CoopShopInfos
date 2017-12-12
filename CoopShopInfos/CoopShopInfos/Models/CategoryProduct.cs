using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoopShopInfos.Models
{
    public class CategoryProduct
    {
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
