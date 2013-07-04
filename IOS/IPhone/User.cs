using System;
using MonoTouch.UIKit;

namespace Touchin.HashBot.IPhone
{
	public class User
	{
		public string Name { get; set; }

		public string ProfileImageUrl { get; set; }

		public UIImage UserImage { get; set; }

		public UIImage Avatar
		{
			get { return UIImage.FromFile ("Images/Main/avatar.png"); } 
		}

		public UIImage AvatarBig
		{
			get { return UIImage.FromFile ("Images/Main/avatar_big.png"); } 
		}
	}
}

