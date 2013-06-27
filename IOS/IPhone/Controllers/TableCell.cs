using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public class TableCell : UITableViewCell
	{
		private UILabel _userName;
		private UILabel _userTwite;

		public TableCell (NSString cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Blue;
			ContentView.BackgroundColor = UIColor.LightGray;

			_userName = new UILabel () 
			{
				Font = UIFont.SystemFontOfSize(34),
				TextColor = UIColor.FromRGB (0, 0, 0),
				BackgroundColor = UIColor.Clear,
			};

			_userTwite = new UILabel () 
			{
				Font = UIFont.SystemFontOfSize(26),
				TextColor = UIColor.FromRGB (0, 0, 0),
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Clear,
			};

			ContentView.Add (_userName);
			ContentView.Add (_userTwite);
		}

		public void UpdateCell(TweetInfo twiteInfo)
		{
			_userName.Text = twiteInfo.UserName;
			_userTwite.Text = twiteInfo.TweetText;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			_userName.Frame = new RectangleF(5, 4, ContentView.Bounds.Width - 63, 25);
			_userTwite.Frame = new RectangleF(100, 18, 100, 20);
		}
	}
}

