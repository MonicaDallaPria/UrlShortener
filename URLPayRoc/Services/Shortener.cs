using LiteDB;
using System;
using System.Linq;

namespace URLPayRoc
{
	public class Shortener
	{
		public string Token { get; set; }
		public string url2 { get; set; }

		public Shortener()
		{

		}
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

		public string Path(string url)
        {
			//string baseUrl = biturl.URL;
			var uri = new Uri(url);
			return uri.Host;
		}
	}
}
 
    