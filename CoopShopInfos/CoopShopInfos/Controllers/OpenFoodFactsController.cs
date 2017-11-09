using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using CoopShopInfos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenFoodAPI.Models;

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
                var stringData = response.Content.
                    ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject
                    <OpenFoodProduct>(stringData);


                var productSheetVM = new ProductSheetViewModel
                {
                    ProductName = data.product.product_name_fr,
                    ImageUrl = data.product.image_url,
                    BarCode = data.product.code,
                    Brand = data.product.brands,
                    Categories = data.product.categories
                    
                };


                // Getting data from Shop table using Ef Core and inserting Select item into shopList
                var shopList = new List<Shop>();
                shopList = (from shop in _context.Shop select shop).ToList();
                shopList.Insert(0, new Shop { ShopId = 0, ShopName = "Select" });

                // Assigning Shop List to WiewBag.ShopList
                ViewBag.ShopList = shopList;


                return View(productSheetVM);
            }
        }

        [HttpPost]
        public IActionResult ShowProduct(ProductSheetViewModel model)
        {
           


            //ViewBag.Result = i > 0 ? "Data Saved Successfully" : "Something Went Wrong";

            return RedirectToAction("Index", "BarcodeScan");


        }

        private bool ProductExists(string barcode)
        {
            return _context.Product.Any(e => e.Barcode == barcode);
        }
    }
}