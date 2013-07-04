using System;
using System.Net;
using RestSharp;
using System.IO;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Touchin.HashBot.IPhone
{
	public delegate void ImageDownloadedForTwittHandler(UIImage image, Status twitt);

	public class ImageLoader
	{
		private RestClient client = new RestClient();

		public void DownloadImageForTwitt(Status twitt, ImageDownloadedForTwittHandler callBack)
		{
			var request = new RestRequest(twitt.User.ProfileImageUrl);

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

