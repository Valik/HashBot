using System;
using System.Net;
using RestSharp;
using System.IO;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Touchin.HashBot
{
	public delegate void ImageDownloadedForTwittHandler(UIImage image, TweetInfo twitt);

	public class ImageWorker
	{
		private ImageDownloadedForTwittHandler _imageDownloaded;
		private TweetInfo _twitt;

		public ImageWorker(ImageDownloadedForTwittHandler callBack, TweetInfo twitt)
		{
			_imageDownloaded = callBack;
			_twitt = twitt;
		}

		public void BeginDownloadingImage(string url)
		{
			var uri = new Uri(url);
			var client = new RestClient("http://" + uri.Host);
			var request = CreateRequest(uri);

			client.ExecuteAsync(request, (response) =>
			{
				CreateImage(response);
			});
		}

		private RestRequest CreateRequest(Uri uri)
		{
			var resource = "";
			for (int i = 0; i < uri.Segments.Length - 1; i++)
				resource += uri.Segments[i];

			var request = new RestRequest(resource + "{fileName}");
			request.AddUrlSegment("fileName", uri.Segments[uri.Segments.Length - 1]);

			return request;
		}

		private void CreateImage(IRestResponse response)
		{
			var data = NSData.FromArray(response.RawBytes);
			var image = UIImage.LoadFromData(data);

			OnImageDownloaded(image);
		}

		private void OnImageDownloaded(UIImage image)
		{
			if (_imageDownloaded != null)
				_imageDownloaded.Invoke(image, _twitt);
		}
	}
}

