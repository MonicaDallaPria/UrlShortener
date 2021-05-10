//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using URLPayRoc.Services;
//using URLPayRoc.Models;


//namespace URLPayRoc.Controllers
//{
//    public class ShortenLinkController : Controller
//    {
//        private readonly ShortenerLogic _shortenerLogic;
//        private readonly Urls _urls;

//        public ShortenLinkController(ShortenerLogic shortenerLogic)
//        {
//            _shortenerLogic = shortenerLogic;
//            Urls urls = new Urls();
//            _urls = urls;
//        }
//        public IActionResult Home()
//        {           
//            return View(_urls);
//        }

//        [HttpPost]
//        public IActionResult Home(string OldUrl)
//        {            
//            _urls.NewUrl = _shortenerLogic.GetNewUrl(OldUrl);
//            _urls.OldUrl = _shortenerLogic.GetOldUrl(_urls.NewUrl);

//            return View(_urls);
//        }
//    }
//}
