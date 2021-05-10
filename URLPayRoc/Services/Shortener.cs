using LiteDB;
using System;
using System.Linq;
using URLPayRoc.Models;

namespace URLPayRoc
{
	public class Shortener
	{
		public string Token { get; set; }
		public string url2 { get; set; }

		public LiteDbOptions biturl;

		public Shortener()
		{

		}

		// The method with which we generate the token
		public string GenerateToken()
		{
			string urlsafe = string.Empty;
			Enumerable.Range(48, 75)
			  .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
			  .OrderBy(o => new Random().Next())
			  .ToList()
			  .ForEach(i => urlsafe += Convert.ToChar(i)); // Store each char into urlsafe
			Token = urlsafe.Substring(new Random().Next(0, urlsafe.Length), new Random().Next(2, 6));
			return Token;
		}

		public Shortener(string url)
		{
			var db = new LiteDatabase("Data/Urls.db");
			var urls = db.GetCollection<LiteDbOptions>();
			// While the token exists in our LiteDB we generate a new one
			// It basically means that if a token already exists we simply generate a new one
			while (urls.Exists(u => u.Token == GenerateToken())) ;
			// Store the values in the NixURL model
			string baseUrl = biturl.URL;
			var uri = new Uri(url);
			var host = uri.Host;
			biturl = new LiteDbOptions()
			{
				Token = Token,
				URL = url,
				ShortenedURL = host + Token
			};
			if (urls.Exists(u => u.URL == url))
				throw new Exception("URL already exists");
			// Save the NixURL model to  the DB
			urls.Insert(biturl);
		}

		public string Path(string url)
        {
			//string baseUrl = biturl.URL;
			var uri = new Uri(url);
			return uri.Host;
		}
	}

	internal class NixConf
    {
        public string Host()
        {
			UrlDTO bitUrl = new UrlDTO();
			string baseUrl = bitUrl.URL;
			var uri = new Uri(baseUrl);
			var host = uri.Host;

			return host;

		}
	}
}
 
    