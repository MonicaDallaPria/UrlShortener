using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Linq;
using LiteDB;
using URLPayRoc.Models;
using URLPayRoc;
using System.Collections.Generic;

namespace URLDTO.Controllers
{

    // This will be needed later in the PostURL() method
    public class LiteDBURLResponse: ILiteDbContext
    {
        public string url { get; set; }
        public string status { get; set; }
        public string token { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly Shortener _shortener;
        private readonly ILiteDbContext _liteDbContext;

        public HomeController(Shortener shortener,
            ILiteDbContext liteDbContext)
        {
           _shortener = shortener;
            _liteDbContext = liteDbContext;
        }
        [HttpGet, Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]

        //public string GetFullUrl(string token)
        //{
        //    //connect to the database, use token to fetch 1x UrlDTO record form the db of many UrlDTO records,
        //    //return the url property from that 1x UrlDTO
        //    Shortener test = new Shortener();
        //    var dbInstance = test.UrlData(token);
        //    return dbInstance.First().ShortenedURL;
        //}


        //[HttpPost, Route("/x")]
        //public string PostUrl([FromBody]string url)
        //{
        //    var DB = new LiteDB.LiteDatabase("Data/Urls.db");
        //        DB.GetCollection<UrlDTO>();
        //    return _shortener.SaveUrl(url);
        //}
        //[HttpPost, Route("/x/")]
        //public string PostURL(dynamic url)
        //{
        //    {
        //    var newObj = Json(url)
        //    var DB = new LiteDB.LiteDatabase("Data/Urls.db");
        //    DB.GetCollection<UrlDTO>();

        [HttpPost, Route("/")]
        public IActionResult PostURL([FromBody] string url)
        {
            var DB = new LiteDatabase("Data/Urls.db").GetCollection<LiteDbOptions>();
            // Connect to the database

            try
            {
                // If the url does not contain HTTP prefix it with it
                if (!url.Contains("http"))
                {
                    url = "http://" + url;
                }
                // check if the shortened URL already exists within our database
                if (DB.Exists(u => u.ShortenedURL == url))
                {
                    Response.StatusCode = 405;
                    return Json(new LiteDBURLResponse()
                    {
                        url = url,
                        status = "already shortenedd",
                        token = null
                    });
                }
                // Shorten the URL and return the token as a json string
                return Json(_shortener.GenerateToken());
                // Catch and react to exceptions
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
                .GetCollection<UrlDTO>()
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







