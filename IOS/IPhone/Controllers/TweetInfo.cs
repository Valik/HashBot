using System;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public class TweetInfo
	{
		public string UserName { get; private set;}

		public string TweetText { get; private set;}

		public string TimeOfCreating { get; private set; }

		public string ImageSrc { get; private set; }

		public UIImage UserImage
		{
			get { return UIImage.FromFile ("Images/Main/avatar.png"); } 
		}

		public TweetInfo (string userName, string twiteText)
		{
			UserName = userName;
			TweetText = twiteText;
		}

		public TweetInfo (string userName, string twiteText, string timeOfCreating, string imageSrc)
		{
			UserName = userName;
			TweetText = twiteText;
			TimeOfCreating = timeOfCreating;
			ImageSrc = imageSrc;
		}
	}
}

