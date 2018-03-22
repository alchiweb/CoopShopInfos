using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoopShopInfos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Linq;

namespace CoopShopInfos.ViewComponents
{
    public class PricesViewComponent : ViewComponent
    {
        private readonly CoopShopInfosContext _context;
        public PricesViewComponent(CoopShopInfosContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string barcode, int shopid)
        {
            // Get product
            var product = _context.Product.FirstOrDefault(e => e.Barcode == barcode);

            

            //Get prices list if a product is chosen
            if (product != null)
            {

                var prices = _context.Price
                    .Include(sp => sp.ShopProduct)
                    .Where(s => s.ShopProduct.All(sid => sid.ShopId == shopid))
                    .Select(p => p.ShopProduct.FirstOrDefault(prod => prod.ProductId == product.ProductId).Price)
                    .OrderByDescending(d => d.PriceDateTime)
                    .ToList();
                
                prices?.RemoveAll(item => item == null);

                if (prices != null)
                {
                    var pricesVM = new PricesViewModel
                    {
                        ShopId = shopid,
                        Prices = prices
                    };
                    return View("Prices", pricesVM); 
                }
                else
                {
                    return View("Default");
                }
            }
            else
            {
                return View("Default");
            }
        }
    }
}
