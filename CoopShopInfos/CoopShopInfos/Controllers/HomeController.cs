using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoopShopInfos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CoopShopInfos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Product model, IFormFile fichier, string codeBarre)
        {
            
            if (codeBarre != null)
            {
                var values = new RouteValueDictionary
                {
                    
                    { "barcode", codeBarre }
                };

                //return Content($"{model.Barcode}");
                return RedirectToAction("ShowProduct", "OpenFoodFacts", values);
            }
            else
            {
                {
                    return View("DecodingError");
                }
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
