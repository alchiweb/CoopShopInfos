using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoopShopInfos.Models;

namespace CoopShopInfos.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Shops")]
    public class ShopsController : Controller
    {
        private readonly CoopShopInfosContext _context;

        public ShopsController(CoopShopInfosContext context)
        {
            _context = context;
        }

        // GET: api/Shops
        [HttpGet]
        public IEnumerable<Shop> GetShop()
        {
            return _context.Shop;
        }

        // GET: api/Shops/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShop([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shop = await _context.Shop.SingleOrDefaultAsync(m => m.ShopId == id);

            if (shop == null)
            {
                return NotFound();
            }

            return Ok(shop);
        }

        // PUT: api/Shops/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShop([FromRoute] int id, [FromBody] Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shop.ShopId)
            {
                return BadRequest();
            }

            _context.Entry(shop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
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

        // POST: api/Shops
        [HttpPost]
        public async Task<IActionResult> PostShop([FromBody] Shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Shop.Add(shop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShop", new { id = shop.ShopId }, shop);
        }

        // DELETE: api/Shops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shop = await _context.Shop.SingleOrDefaultAsync(m => m.ShopId == id);
            if (shop == null)
            {
                return NotFound();
            }

            _context.Shop.Remove(shop);
            await _context.SaveChangesAsync();

            return Ok(shop);
        }

        private bool ShopExists(int id)
        {
            return _context.Shop.Any(e => e.ShopId == id);
        }
    }
}