using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenFoodAPI.Models;

namespace CoopShopInfos.Models
{
    public static class ProductConversion
    {
        public static Product ConvertToProduct(OpenFoodProduct openFoodProduct)
        {
            var result = new Product
            {
                Barcode = openFoodProduct?.code,
                ProductName = openFoodProduct?.product.product_name_fr
            };


            return result;
        }
    }
}
