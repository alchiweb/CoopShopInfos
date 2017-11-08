using CoopShopInfos.Models;
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
    }
}