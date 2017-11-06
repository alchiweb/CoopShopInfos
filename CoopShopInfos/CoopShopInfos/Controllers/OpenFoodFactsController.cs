using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using CoopShopInfos.Models;
using Newtonsoft.Json;
using WebClientAPI.Models;

namespace WebClientAPI.Controllers
{
    public class OpenFoodFactsController : Controller
    {
        public IActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));


                var response = client.GetAsync
                    ("https://fr.openfoodfacts.org/api/v0/produit/3029330003533.json").Result;
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