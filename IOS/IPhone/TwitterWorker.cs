using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using RestSharp;
using RestSharp.Authenticators;

namespace Touchin.HashBot.IPhone
{
	public class TwitterWorker
	{
		private const int _countOfTwitts = 5;

		private const string _consumerKey = "rn0pnadaymIuTek11Whg";
		private const string _consumerSecret = "27ajTVlYyk7U7QOK8SeZhYfe8st9uOr1kdAQ2bupU";
		private const string _accessToken = "1558296156-QCtKDAnKDtuJXMrXiTGxzYKIX5DzZy51qUelysB";
		private const string _accesTokenSecret = "RJDwv3IpIEpN6ADJvuQtShLk9lwmP2Y6O7q7zJxtOU";

		private RestClient _client;

		public TwitterWorker ()
		{
			_client = new RestClient();
			_client.Authenticator = OAuth1Authenticator.ForProtectedResource(
				_consumerKey, _consumerSecret, _accessToken, _accesTokenSecret);
		}

		public void FillTwittsByHashTag(string hashTag, Action<IEnumerable<Twitt>> callback)
		{
			var request = new RestRequest("https://api.twitter.com/1.1/search/tweets.json");

			request.AddParameter("q", hashTag);
			request.AddParameter("count", _countOfTwitts);

			_client.ExecuteAsync<RootObject>(request, (response) => 
			{
				callback(response.Data.Statuses);
			});

		}
	}
}

