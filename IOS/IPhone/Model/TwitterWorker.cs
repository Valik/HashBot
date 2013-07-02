using System;
using System.Collections.Generic;
using System.Threading;
using RestSharp;
using RestSharp.Authenticators;

namespace Touchin.HashBot
{
	public delegate void TwittsCompletedHandler(List<TweetInfo> twitts);

	public class TwitterWorker
	{
		public event TwittsCompletedHandler TwittsCompleted;

		private const string _consumerKey = "rn0pnadaymIuTek11Whg";
		private const string _consumerSecret = "27ajTVlYyk7U7QOK8SeZhYfe8st9uOr1kdAQ2bupU";
		private const string _accessToken = "1558296156-QCtKDAnKDtuJXMrXiTGxzYKIX5DzZy51qUelysB";
		private const string _accesTokenSecret = "RJDwv3IpIEpN6ADJvuQtShLk9lwmP2Y6O7q7zJxtOU";

		public TwitterWorker()
		{

		}

		public void FillTwittsByHashTag(string hashTag)
		{
			var searchThread = new Thread(SearchTwittsByHashTag);
			searchThread.Start(hashTag);
		}

		public void SearchTwittsByHashTag(object hashTag)
		{
			var tag = hashTag as String;

			var client = new RestClient();
			client.Authenticator = OAuth1Authenticator.ForProtectedResource
				(_consumerKey, _consumerSecret, _accessToken, _accesTokenSecret);

			var request = new RestRequest ("https://api.twitter.com/1.1/search/tweets.json");
			request.AddParameter("q", "GitHub");
			request.AddParameter("count", "10");

			var response = client.Execute(request);
			var responseText = response.Content;


		}

	}
}

