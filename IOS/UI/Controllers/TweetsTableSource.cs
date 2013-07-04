using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using Touchin.HashBot.IPhone;

namespace Touchin.HashBot
{
	public delegate void CellSelectedHandler(Twitt twitt);

	public class TweetsTableSource : UITableViewSource
	{
		public event CellSelectedHandler CellSelected;

		private List<Twitt> _items;
		private static string _tableCellId = "tweetCell";

		public TweetsTableSource (List<Twitt> items)
		{
			_items = items;
		}

		public override int RowsInSection (UITableView tableView, int section)
		{
			return _items.Count;
		}

		public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			OnCellSelected(_items[indexPath.Row]);
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var twitt = _items[indexPath.Row];
			var cell = DequeueOrCreateCell(tableView);	

			cell.UpdateCell(twitt);
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

		private void OnCellSelected(Twitt twitt)
		{
			if (CellSelected != null)
				CellSelected.Invoke(twitt);
		}
	}
}

