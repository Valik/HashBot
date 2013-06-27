using System;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public class TweetInfo
	{
		private string _userName;
		private string _tweetText;

		public string UserName { get { return _userName; } }

		public string TweetText { get { return _tweetText; } }

		public UIImage UserImage
		{
			get { return UIImage.FromFile ("Resources/Main/avatar.png"); } 
		}

		public TweetInfo (string userName, string twiteText)
		{
			_userName = userName;
			_tweetText = twiteText;
		}
	}
}

