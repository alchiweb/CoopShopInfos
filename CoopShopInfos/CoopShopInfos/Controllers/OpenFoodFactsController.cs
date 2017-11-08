using System.Net.Http;
using System.Net.Http.Headers;
using CoopShopInfos.Models;
using Microsoft.AspNetCore.Mvc;
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
        //public IActionResult Index(string searchedBarcode)
        //{
            //using (var client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(
            //        new MediaTypeWithQualityHeaderValue("application/json"));

            //    var url = string.Concat("https://fr.openfoodfacts.org/api/v0/produit/", searchedBarcode, ".json");

            //    var response = client.GetAsync
            //        (url).Result;
            //    var stringData = response.Content.
            //        ReadAsStringAsync().Result;
            //    var data = JsonConvert.DeserializeObject
            //        <OpenFoodProduct>(stringData);

            //    //JsonConvert.PopulateObject(stringData, data);
            //    return View(data);
            //}



        //}


        public IActionResult ShowProduct(string barcode)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var url = string.Concat("https://fr.openfoodfacts.org/api/v0/produit/", barcode, ".json");
                var response = client.GetAsync
                    (url).Result;
                var stringData = response.Content.
                    ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject
                    <OpenFoodProduct>(stringData);

                //JsonConvert.PopulateObject(stringData, data);
                return View(data);
            }



        }



    }
}