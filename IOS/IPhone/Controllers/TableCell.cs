using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public class TableCell : UITableViewCell
	{
		private const int _tweetTextLenght = 30;

		public TableCell(UITableViewCellStyle style, string cellId) : base (style, cellId)
		{
			SetCellStyle();
		}

		public void UpdateCell(TweetInfo tweetInfo)
		{
			ImageView.Image = tweetInfo.UserImage;
			TextLabel.Text = tweetInfo.UserName;

			var tweetText = tweetInfo.TweetText.Length > _tweetTextLenght ? 
				tweetInfo.TweetText.Substring(0, _tweetTextLenght) : tweetInfo.TweetText;

			DetailTextLabel.Text = tweetText + "...";
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
		}

		void SetCellStyle()
		{
			var backgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Images/Main/table.png"));
			var selectedBackground = UIColor.FromPatternImage(UIImage.FromFile("Images/Main/table_pressed.png"));
			var selectionView = new UIView();
			selectionView.BackgroundColor = selectedBackground;

			ContentView.BackgroundColor = backgroundColor;
			SelectedBackgroundView = selectionView;
			TextLabel.BackgroundColor = backgroundColor;
			DetailTextLabel.BackgroundColor = backgroundColor;
		}
	}
}

