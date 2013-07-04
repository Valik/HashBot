using System;
using System.Net;
using RestSharp;
using System.IO;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Touchin.HashBot.IPhone
{
	public delegate void ImageDownloadedForTwittHandler(UIImage image, TweetInfo twitt);

	public class ImageLoader
	{
		private RestClient client = new RestClient();

		public void DownloadImageForTwitt(TweetInfo twitt, ImageDownloadedForTwittHandler callBack)
		{
			var request = new RestRequest(twitt.ImageSrc);

			client.ExecuteAsync(request, response =>
			{
				using (var data = NSData.FromArray(response.RawBytes))
				{
					callBack(UIImage.LoadFromData(data), twitt);
				}
			});
		}
	}
}

