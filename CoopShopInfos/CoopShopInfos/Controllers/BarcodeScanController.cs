using System.Threading.Tasks;
using CoopShopInfos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CoopShopInfos.Controllers
{
    public class BarcodeScanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Product model)
        {
            var values = new RouteValueDictionary();
            
            values.Add("barcode",$"{model.Barcode}");
            
            //return Content($"{model.Barcode}");
            return RedirectToAction("ShowProduct", "OpenFoodFacts", values);
        }

        //[HttpPost]
        //public async Task<IActionResult> ProcessText(IFormFile file)
        //{
            
        //}
    }
}