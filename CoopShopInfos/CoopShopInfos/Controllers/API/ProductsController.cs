using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoopShopInfos.Models;
using Newtonsoft.Json;

namespace CoopShopInfos.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly CoopShopInfosContext _context;

        

        public ProductsController(CoopShopInfosContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> GetProduct()
        {
            return _context.Product;
        }

        // GET: api/Products/ProductSheet/5
        [HttpGet("ProductSheet/{id}")]
        public async Task<IActionResult> GetProductSheet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Get data from Shop table using Ef Core and inserting Select item into shopList
            var shopList = _context.Shop.ToList();

            IList<PricesViewModel> pricesList = new List<PricesViewModel>();

            // Get prices for every shop
            foreach (var shop in shopList)
            {
                var prices = _context.Price
                    .Include(sp => sp.ShopProduct)
                    .Where(s => s.ShopProduct.All(sid => sid.ShopId == shop.ShopId))
                    .Select(p => p.ShopProduct.FirstOrDefault(prod => prod.ProductId == product.ProductId).Price)
                    .OrderByDescending(d => d.PriceDateTime)
                    .ToList();

                prices?.RemoveAll(item => item == null);

                var pricesViewModel = new PricesViewModel
                {
                    ShopName = shop.ShopName,
                    Prices = prices
                };

                pricesList.Add(pricesViewModel);
            }

            var productSheetVM = new ProductSheetViewModel
            {
                ProductName = product.ProductName,
                BarCode = product.Barcode,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl,
                Unit = product.Unit,
                ShopPricesList = pricesList
            };

            //Get category
            var productCategoryId = _context.CategoryProduct
                ?.FirstOrDefault(p => p.ProductId == product.ProductId)?.CategoryId;
            var category = _context.Category.FirstOrDefault(cat => cat.CategoryId == productCategoryId);
            productSheetVM.Categories = category?.CategoryName;

            //var json = JsonConvert.SerializeObject(productSheetVM);

            return Ok(productSheetVM);
        }

        // GET: api/Products/ProductSheets
        [HttpGet("ProductSheets")]
        public async Task<IActionResult> GetProductSheets()
        {
            var productList = _context.Product.ToList();

            var productSheetList = new List<ProductSheetViewModel>();

            // Get data from Shop table using Ef Core and inserting Select item into shopList
            var shopList = _context.Shop.ToList();

            foreach (var product in productList)
            {
               
                IList<PricesViewModel> pricesList = new List<PricesViewModel>();

                // Get prices for every shop
                foreach (var shop in shopList)
                {
                    var prices = _context.Price
                        .Include(sp => sp.ShopProduct)
                        .Where(s => s.ShopProduct.All(sid => sid.ShopId == shop.ShopId))
                        .Select(p => p.ShopProduct.FirstOrDefault(prod => prod.ProductId == product.ProductId).Price)
                        .OrderByDescending(d => d.PriceDateTime)
                        .ToList();

                    prices?.RemoveAll(item => item == null);

                    var pricesViewModel = new PricesViewModel
                    {
                        ShopName = shop.ShopName,
                        Prices = prices
                    };

                    pricesList.Add(pricesViewModel);
                }

                var productSheetVM = new ProductSheetViewModel
                {
                    ProductName = product.ProductName,
                    BarCode = product.Barcode,
                    Quantity = product.Quantity,
                    ImageUrl = product.ImageUrl,
                    Unit = product.Unit,
                    ShopPricesList = pricesList
                };

                productSheetList.Add(productSheetVM);

                //Get category
                var productCategoryId = _context.CategoryProduct
                    ?.FirstOrDefault(p => p.ProductId == product.ProductId)?.CategoryId;
                var category = _context.Category.FirstOrDefault(cat => cat.CategoryId == productCategoryId);
                productSheetVM.Categories = category?.CategoryName;


            }

           

            return Ok(productSheetList);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}