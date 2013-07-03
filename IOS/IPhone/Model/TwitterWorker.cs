using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using RestSharp;
using RestSharp.Authenticators;

namespace Touchin.HashBot
{
//	public delegate void TwittsCompletedHandler(List<TweetInfo> twitts);

	public class TwitterWorker
	{
		private const int _countOfTwitts = 5;

		private const string _consumerKey = "rn0pnadaymIuTek11Whg";
		private const string _consumerSecret = "27ajTVlYyk7U7QOK8SeZhYfe8st9uOr1kdAQ2bupU";
		private const string _accessToken = "1558296156-QCtKDAnKDtuJXMrXiTGxzYKIX5DzZy51qUelysB";
		private const string _accesTokenSecret = "RJDwv3IpIEpN6ADJvuQtShLk9lwmP2Y6O7q7zJxtOU";

		public TwitterWorker()
		{

		}

		public void FillTwittsByHashTag(string hashTag, Action<IEnumerable<TweetInfo>> callback)
		{
			var client = new RestClient();
			var request = new RestRequest("https://api.twitter.com/1.1/search/tweets.json");

			client.Authenticator = OAuth1Authenticator.ForProtectedResource(_consumerKey, _consumerSecret, _accessToken, _accesTokenSecret);
			request.AddParameter("q", hashTag);
			request.AddParameter("count", _countOfTwitts);

			client.ExecuteAsync<RootObject>(request, (response) => 
			{
				var result = new List<TweetInfo>();

				foreach (var curStatus in response.Data.statuses)
					result.Add(new TweetInfo(curStatus.user.name, curStatus.text, curStatus.createdAt, curStatus.user.profileImageUrl));

				callback(result);
			});

		}
	}
}

