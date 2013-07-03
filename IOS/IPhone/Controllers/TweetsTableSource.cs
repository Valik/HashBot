using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public delegate void CellSelectedHandler(TweetInfo tweetInfo);

	public class TweetsTableSource : UITableViewSource
	{
		public event CellSelectedHandler CellSelected;

		private List<TweetInfo> _items;
		private static string _tableCellId = "tweetCell";

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
			var tweetInfo = _items[indexPath.Row];
			var cell = DequeueOrCreateCell(tableView);	

			cell.UpdateCell(tweetInfo);
			return cell;
		}

		private TableCell DequeueOrCreateCell(UITableView tableView)
		{
			var cell = tableView.DequeueReusableCell(_tableCellId) as TableCell;
			if (cell == null)
			{
				cell = new TableCell(UITableViewCellStyle.Subtitle, _tableCellId);
			}

			return cell;
		}

		public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			OnCellSelected(_items[indexPath.Row]);
		}

		private void OnCellSelected(TweetInfo tweetInfo)
		{
			if (CellSelected != null)
				CellSelected.Invoke(tweetInfo);
		}
	}
}

