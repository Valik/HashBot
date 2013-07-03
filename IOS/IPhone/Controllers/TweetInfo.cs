using System;
using MonoTouch.UIKit;
using System.Globalization;

namespace Touchin.HashBot
{
	public class TweetInfo
	{
		private string _createdAt;

		public string UserName { get; private set;}

		public string TweetText { get; private set;}

		public string ImageSrc { get; private set; }

		public DateTime TimeOfCreating 
		{ 
			get
			{
				CultureInfo provider = CultureInfo.InvariantCulture;
				DateTime date = DateTime.ParseExact(_createdAt, "ddd MMM d HH:mm:ss %zzzz yyyy", provider);

				return date;
			}
			private set
			{

			}
		}

		public UIImage UserImage
		{
			get 
			{ 

				return UIImage.FromFile ("Images/Main/avatar_big.png"); 
			} 
		}

		public UIImage Avatar
		{
			get { return UIImage.FromFile ("Images/Main/avatar.png"); } 
		}

		public UIImage AvatarBig
		{
			get { return UIImage.FromFile ("Images/Main/avatar_big.png"); } 
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
			_createdAt = timeOfCreating;
			ImageSrc = imageSrc;
		}
	}
}

