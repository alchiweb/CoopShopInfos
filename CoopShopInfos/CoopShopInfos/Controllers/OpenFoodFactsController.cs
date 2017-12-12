using CoopShopInfos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenFoodAPI.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            // Search if the product that was scanned already is in Product table
            var product = _context.Product.FirstOrDefault(e => e.Barcode == barcode);

            // Get data from Shop table using Ef Core and inserting Select item into shopList
            var shopList = (from shop in _context.Shop select shop).ToList();

            // Get units list
            var units = from Unit u in Enum.GetValues(typeof(Unit))
                select new { ID = (int)u, Name = u.ToString() };

            ViewData["Unit"] = new SelectList(units, "ID", "Name");

            // If the product is already registred in database, show data
            if (product != null)
            {
                var productSheetVM = new ProductSheetViewModel
                {
                    ProductName = product.ProductName,
                    BarCode = product.Barcode,
                    Quantity = product.Quantity,
                    
                    Unit = product.Unit,
                    ShopList = shopList,
                    SelectedAnswer = string.Empty

                };

                //Get category
                    var productCategoryId = _context.CategoryProduct
                        ?.FirstOrDefault(p => p.ProductId == product.ProductId)?.CategoryId;
                    var category = _context.Category.FirstOrDefault(cat => cat.CategoryId == productCategoryId);
                    productSheetVM.Categories = category?.CategoryName;

                return View(productSheetVM);
            }

            // If the product is not registred in database, try to find it on OpenFoodFact API
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
                shopList = (from shop in _context.Shop select shop).ToList();



                // If the product is found in OpenFoodFacts, show data
                switch (data.status)
                {
                    case 1:
                    {
                        var productSheetVM = new ProductSheetViewModel
                        {
                            ProductName = data.product?.product_name_fr,
                            ImageUrl = data.product?.image_url,
                            BarCode = data.product?.code,
                            
                            Categories = data.product?.categories,
                            ShopList = shopList,
                            SelectedAnswer = string.Empty

                        };
                        return View(productSheetVM);
                    }
                    case 0:
                    {
                            // if the product is not found at all, show the form to create a new one
                        var productSheetVM = new ProductSheetViewModel
                        {
                            BarCode = barcode,
                            ShopList = shopList,
                            SelectedAnswer = string.Empty

                        };
                        return View(productSheetVM);
                    }
                }
                

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(int productid, string barcode, string productname, decimal price, float quantity, string imageurl, Unit unit, string categories,
            int shopId, string shopName)
        {
            var product = _context.Product.FirstOrDefault(e => e.Barcode == barcode);

            // if the product doesn't already exist, create it
            if (product == null)
            {
                product = new Models.Product
                {
                    Barcode = barcode,
                    ProductName = productname,
                    Quantity = quantity,
                    ImageUrl = imageurl,
                    Unit = unit
                };
                _context.Add(product);

                // Add or update the category if there is one
                AddOrUpdatecategory(categories, product);

                // Save DataContext
                await SaveContext();

                //Add or update the price
                AddOrUpdatePrice(product, shopId, price);

                // Save Data Context
                await SaveContext();
                
            }
            //else update it
            else
            {
                // Update product table
               _context.Update(product);
         
                // Add or update the category if there is one
                AddOrUpdatecategory(categories, product);

                // Save Data Context
                await SaveContext();

                //Add or update the price
                AddOrUpdatePrice(product, shopId, price);

                // Save Data Context
                await SaveContext();

                // Return to Index page
                return RedirectToAction("Index", "BarcodeScan");
                
            }
            
            return RedirectToAction("Index", "BarcodeScan");
        }


        private void AddOrUpdatecategory(string categories, Models.Product product)
        {
            if (categories != null)
            {
                // Try to find it in the Category table
                var categoryToFind = _context.Category.FirstOrDefault(c => c.CategoryName == categories);

                // Category doesn't exist yet, add it to Category table
                if (categoryToFind == null)
                {
                    var category = new Category
                    {
                        CategoryName = categories,

                    };
                    _context.Add(category);

                    categoryToFind = category;
                }

                
                // Create new CategoryProduct object if it not already exists
                var categoryProductToFind = _context.CategoryProduct.Find(categoryToFind.CategoryId,product.ProductId);

                if (categoryProductToFind == null)
                {
                    var categoryProduct = new CategoryProduct
                    {
                        ProductId = product.ProductId,
                        Product = product,
                        CategoryId = categoryToFind.CategoryId,
                        Category = categoryToFind

                    };
                    
                    // Add new Categoryproduct object to Data Context
                    _context.Add(categoryProduct);
                }

            }
        }

        private void AddOrUpdatePrice(Models.Product product, int shopId, decimal priceAmount)
        {
            // Find Shop
            var shopToFind = _context.Shop.FirstOrDefault(s => s.ShopId == shopId);
            // Find price 
            var priceToFind = _context.Price.FirstOrDefault(p => p.PriceAmount == priceAmount);
            // If price doesn't already exist, create new price with the given price amount
            if (priceToFind == null)
            {
                var price = new Price
                {
                    PriceAmount = priceAmount
                };
                _context.Add(price);
                priceToFind = price;
            }

            //Create new ShopProduct object if it not already exists, then update DateTime
            var shopProductToFind = _context.ShopProduct.Find(shopToFind.ShopId,product.ProductId, priceToFind.PriceId);

            if (shopProductToFind == null)
            {
                var shopProduct = new ShopProduct
                {
                    PriceId = priceToFind.PriceId,
                    Price = priceToFind,
                    ProductId = product.ProductId,
                    Product = product,
                    ShopId = shopToFind.ShopId,
                    Shop = shopToFind
                    
                };
                shopProductToFind = shopProduct;
            }

            // Add new ShopProduct object to DataContext
            shopProductToFind.PriceDateTime = DateTime.Now;
            _context.Add(shopProductToFind);
        }

        private async Task SaveContext()
        {
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
        }
    }

}