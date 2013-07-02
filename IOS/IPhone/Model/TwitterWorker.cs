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

		private const int _countOfTwitts = 5;

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

			var response = GetResponse(tag);

			var complitedTwitts = FillTwits(response);

			OnTwittsComplited(complitedTwitts);
		}

		private IRestResponse<RootObject> GetResponse(string tag)
		{
			var client = new RestClient();
			var request = new RestRequest("https://api.twitter.com/1.1/search/tweets.json");

			client.Authenticator = OAuth1Authenticator.ForProtectedResource(_consumerKey, _consumerSecret, _accessToken, _accesTokenSecret);
			request.AddParameter("q", tag);
			request.AddParameter("count", _countOfTwitts);
			request.RequestFormat = DataFormat.Json;
			request.RootElement = "RootObject";

			return client.Execute<RootObject>(request);
		}

		private List<TweetInfo> FillTwits(IRestResponse<RootObject> response)
		{
			var result = new List<TweetInfo>();

			foreach (var curStatus in response.Data.statuses)
			{
				var curTwittInfo = new TweetInfo(curStatus.user.name, curStatus.text, curStatus.created_at, curStatus.user.profile_image_url);
				result.Add(curTwittInfo);
			}

			return result;
		}

		private void OnTwittsComplited(List<TweetInfo> complitedTwitts)
		{
			if (TwittsCompleted != null)
			{
				TwittsCompleted.Invoke(complitedTwitts);
			}
		}
	}
}

