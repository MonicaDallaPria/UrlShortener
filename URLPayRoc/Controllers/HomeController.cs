using Microsoft.AspNetCore.Mvc;
using URLPayRoc;

namespace URLDTO.Controllers
{
    public class HomeController : Controller
    {
        private readonly Shortener _shortener;

        public HomeController(Shortener shortener)
        {
           _shortener = shortener;
        }

        [HttpGet, Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("/")]
        public IActionResult PostURL([FromBody] string url)
        {            
            var generatedToken = _shortener.GenerateToken();
            var host = _shortener.Path(url);
            return Json(host + "/" + generatedToken);   
        }
    }
}







