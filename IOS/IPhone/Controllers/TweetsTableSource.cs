using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public class TweetsTableSource : UITableViewSource
	{
		private List<TweetInfo> _items;
		private static string _tableCellId = "tweetCell";
		private const int _tweetTextLenght = 30;


		public TweetsTableSource (List<TweetInfo> tweets)
		{
			_items = tweets;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _items.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var tweetInfo = _items [indexPath.Row];
			var cell = DequeueOrCreateCell (tableView);	

			SetTweetInfoInCell (tweetInfo, cell);
			return cell;
		}

		private UITableViewCell DequeueOrCreateCell (UITableView tableView)
		{
			var cell = tableView.DequeueReusableCell (_tableCellId);
			if (cell == null) 
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, _tableCellId);

			return cell;
		}

		private void SetTweetInfoInCell (TweetInfo tweetInfo, UITableViewCell cell)
		{
			cell.ImageView.Image = tweetInfo.UserImage;
			cell.TextLabel.Text = tweetInfo.UserName;

			var tweetText = tweetInfo.TweetText.Length > _tweetTextLenght ? 
				tweetInfo.TweetText.Substring (0, _tweetTextLenght) : tweetInfo.TweetText;

			cell.DetailTextLabel.Text = tweetText + "...";
		}
	}
}

