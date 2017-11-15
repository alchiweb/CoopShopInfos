using System;
using CoopShopInfos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using OpenFoodAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoopShopInfos.Controllers
{
    public class OpenFoodFactsController : Controller
    {
        private readonly CoopShopInfosContext _context;

        public OpenFoodFactsController(CoopShopInfosContext context)
        {
            _context = context;
        }


        public IActionResult ShowProduct(string barcode)
        {
            using (var client = new HttpClient())
            {
                // Define request header
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                // Get product data on OpenFoodFacts web API
                var url = string.Concat("https://fr.openfoodfacts.org/api/v0/produit/", barcode, ".json");
                var response = client.GetAsync
                    (url).Result;
                var stringData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject
                    <OpenFoodProduct>(stringData);

                // Getting data from Shop table using Ef Core and inserting Select item into shopList
                var shopList = (from shop in _context.Shop select shop).ToList();

                var productSheetVM = new ProductSheetViewModel
                {
                    ProductName = data?.product?.product_name_fr,
                    ImageUrl = data?.product?.image_url,
                    BarCode = data?.product?.code,
                    Brand = data?.product?.brands,
                    Categories = data?.product?.categories,
                    ShopList = shopList,
                    SelectedAnswer = string.Empty

                };

                return View(productSheetVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(int productid, string barcode, string productname, decimal price,
            int shopid)
        {
            var product = _context.Product.FirstOrDefault(e => e.Barcode == barcode);

            if (product == null)
            {
                product = new Models.Product
                {
                    Barcode = barcode,
                    ProductName = productname
                };

                try
                {
                    _context.Add(product);

                    //SavePrice(price, product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.)
                    ModelState.AddModelError("",
                        "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }

            }
            else
            {
                _context.Update(product);
                try
                {
                    await _context.SaveChangesAsync();

                }
                catch (Exception /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " +
                                                 "see your system administrator.");
                }
                //todo: cas du prix qui existe éventuellement
                // si existe ->update puis return
                var priceToUpdate =
                    _context.ShopProduct.Find(shopid, product.ProductId);

                if (priceToUpdate != null)
                {
                    priceToUpdate.Price = price;
                    _context.Update(priceToUpdate);
                    try
                    {
                        await _context.SaveChangesAsync();

                    }
                    catch (Exception /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                                                     "Try again, and if the problem persists, " +
                                                     "see your system administrator.");
                    }

                    
                    return RedirectToAction("Index", "BarcodeScan");
                }
            }
            //todo
            //nouveau prix qui n'existe pas

            var shopProduct = new ShopProduct
            {
                ShopId = shopid,
                ProductId = product.ProductId,
                Product = product,
                Price = price
            };

            _context.Add(shopProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception /* ex */)
            {

                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                                                "Try again, and if the problem persists, " +
                                                "see your system administrator.");
            }

            return RedirectToAction("Index", "BarcodeScan");
        }


        //private bool ProductExists(string barcode)
        //{
        //    return _context.Product.Any(e => e.Barcode == barcode);
        //}

        //private void SavePrice(decimal price, Models.Product product)
        //{
        //    var result = _context.Product.Where(p => p.ProductId == product.ProductId).Single().ShopProduct;

        //    var shopProduct = new ShopProduct
        //    {
        //        Price = price
        //    };
        //    _context.ShopProduct.Add(shopProduct);
        //}
    }

}