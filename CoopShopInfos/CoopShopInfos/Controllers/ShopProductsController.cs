using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoopShopInfos.Models;

namespace CoopShopInfos.Controllers
{
    public class ShopProductsController : Controller
    {
        private readonly CoopShopInfosContext _context;

        public ShopProductsController(CoopShopInfosContext context)
        {
            _context = context;
        }

        // GET: ShopProducts
        public async Task<IActionResult> Index()
        {
            var coopShopInfosContext = _context.ShopProduct.Include(s => s.Shop).Include(p => p.Product);
            return View(await coopShopInfosContext.ToListAsync());
        }

        // GET: ShopProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopProduct = await _context.ShopProduct
                .Include(s => s.Shop)
                .SingleOrDefaultAsync(m => m.ShopId == id);
            if (shopProduct == null)
            {
                return NotFound();
            }

            return View(shopProduct);
        }

        // GET: ShopProducts/Create
        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(_context.Shop, "ShopId", "ShopName");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName");
            return View();
        }

        // POST: ShopProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ShopId,Price")] ShopProduct shopProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_context.Shop, "ShopId", "ShopName", shopProduct.ShopId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName", shopProduct.ProductId);
            return View(shopProduct);
        }

        // GET: ShopProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopProduct = await _context.ShopProduct.SingleOrDefaultAsync(m => m.ShopId == id);
            if (shopProduct == null)
            {
                return NotFound();
            }
            ViewData["ShopId"] = new SelectList(_context.Shop, "ShopId", "ShopName", shopProduct.ShopId);
            return View(shopProduct);
        }

        // POST: ShopProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShopProductId,Id,ShopId,Price")] ShopProduct shopProduct)
        {
            if (id != shopProduct.ShopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopProductExists(shopProduct.ShopId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_context.Shop, "ShopId", "ShopName", shopProduct.ShopId);
            return View(shopProduct);
        }

        // GET: ShopProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopProduct = await _context.ShopProduct
                .Include(s => s.Shop)
                .SingleOrDefaultAsync(m => m.ShopId == id);
            if (shopProduct == null)
            {
                return NotFound();
            }

            return View(shopProduct);
        }

        // POST: ShopProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shopProduct = await _context.ShopProduct.SingleOrDefaultAsync(m => m.ShopId == id);
            _context.ShopProduct.Remove(shopProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopProductExists(int id)
        {
            return _context.ShopProduct.Any(e => e.ShopId == id);
        }
    }
}
