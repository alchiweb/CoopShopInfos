using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using CoopShopInfos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ZXing;

namespace CoopShopInfos.Controllers
{
    public class BarcodeScanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Product model, IFormFile fichier, string codeBarre)
        {


            // Server Side barcode scanning
            //List<BarcodeFormat> codeFormats = new List<BarcodeFormat>();
            //codeFormats.Add(BarcodeFormat.EAN_13);

            //// create a barcode reader instance
            //var barcodeReader = new BarcodeReader
            //{

            //    //AutoRotate = true,
            //    //TryInverted = true,
            //    Options = {PossibleFormats = codeFormats,
            //        //                    TryHarder = true,
            //        //                    ReturnCodabarStartEnd = true,
                    
            //        PureBarcode = true}
            //};

            //// create an in memory bitmap
            ////            var barcodeBitmap = (Bitmap)Image.FromFile(@"C:\Users\marin\Source\Repos\CoopShopInfo\CoopShopInfos\CoopShopInfos\wwwroot\images\IMG_20171120_122847_petit.jpg");

            //Result barcodeResult = null;
            //using (var mStream = new MemoryStream())
            //{
            //    fichier.CopyTo(mStream);
            //    // create an in memory bitmap
            //    var barcodeBitmap = (Bitmap)Image.FromStream(mStream);

            //    // decode the barcode from the in memory bitmap
            //    barcodeReader.ResultPointFound += BarcodeReader_ResultPointFound;
            //    barcodeReader.ResultFound += BarcodeReader_ResultFound;
            //    barcodeResult = barcodeReader.Decode(barcodeBitmap);

            //}
            //// output results to console
            //Console.WriteLine($"Decoded barcode text: {barcodeResult?.Text}");
            //Console.WriteLine($"Barcode format: {barcodeResult?.BarcodeFormat}");



            if (codeBarre != null)
            {
                var values = new RouteValueDictionary
                {

                    //values.Add("barcode",$"{model.Barcode}");
                    //values.Add("barcode", $"{barcodeResult.Text}");
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

        private void BarcodeReader_ResultPointFound(ResultPoint param1)
        {
          //  throw new NotImplementedException();
        }

        private void BarcodeReader_ResultFound(Result param1)
        {
            //throw new NotImplementedException();
        }


        [HttpPost]
        public IActionResult DecodingError()
        {
            return RedirectToAction("Index");
        }
    }
}