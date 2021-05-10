using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Linq;
using LiteDB;
using URLPayRoc.Models;
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
            var DB = new LiteDatabase("Data/Urls.db").GetCollection<LiteDbOptions>();
            // Connect to the database

            try
            {
                if (!url.Contains("http"))
                {
                    url = "http://" + url;
                }

                if (DB.Exists(u => u.ShortenedURL == url))
                {
                    Response.StatusCode = 405;
                    return Json(new LiteDBURLResponse()
                    {
                        url = url,
                        status = "already shortened",
                        token = null
                    });
                }

                var generatedToken = _shortener.GenerateToken();
                var host = _shortener.Path(url);
                return Json(host + "/" + generatedToken);             
            }
            catch (Exception ex)
            {
                if (ex.Message == "URL already exists")
                {
                    Response.StatusCode = 400;
                    return Json(new LiteDBURLResponse()
                    {
                        url = url,
                        status = "URL already exists",
                        token = DB.Find(u => u.URL == url).FirstOrDefault().Token
                    });
                }
                throw new Exception(ex.Message);
            }
        }
  
        [HttpGet, Route("/{token}")]
        public IActionResult NewRedirect([FromRoute] string token)
        {
            return Redirect(
                new LiteDB.LiteDatabase("Data/Urls.db")
                .GetCollection<LiteDbOptions>()
                .FindOne(u => u.Token == token).URL
            );
        }
        public string FindRedirect(string url)
        {
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Headers.Location.ToString();
                }

            }
            return result;
        }
    }
}







