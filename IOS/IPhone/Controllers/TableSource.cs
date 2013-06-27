using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Touchin.HashBot
{
	public class TableSource : UITableViewSource
	{
		List<TweetInfo> _tableItems;
		static NSString _cellIdentifier = new NSString ("TableCell");


		public TableSource(List<TweetInfo> items)
		{
			_tableItems = items;
		}

		public override int RowsInSection(UITableView tableview, int section)
		{
			return _tableItems.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			TableCell cell = tableView.DequeueReusableCell (_cellIdentifier) as TableCell;
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new TableCell (_cellIdentifier);

			cell.UpdateCell (_tableItems[indexPath.Row]);

			return cell;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			switch (editingStyle) 
			{
				case UITableViewCellEditingStyle.Delete:
				{
					// remove the item from the underlying data source
					_tableItems.RemoveAt(indexPath.Row);
					// delete the row from the table
					tableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
					break;
				}	
				case UITableViewCellEditingStyle.None:
				{
					Console.WriteLine ("CommitEditingStyle:None called");
					break;
				}
			}
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			new UIAlertView("Row Selected", indexPath.Row.ToString(), null, "OK", null).Show();
			tableView.DeselectRow(indexPath, true);
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you wish to disable editing for a specific indexPath or for all rows
		}

		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you don't allow re-ordering
		}

		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete; // this example doesn't suppport Insert
		}


	}
}

