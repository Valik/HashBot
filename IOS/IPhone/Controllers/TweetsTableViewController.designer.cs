// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Touchin.HashBot
{
	[Register ("TweetsTableViewController")]
	partial class TweetsTableViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView TableWithTweets { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ButtonShowMore { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TableWithTweets != null) {
				TableWithTweets.Dispose ();
				TableWithTweets = null;
			}

			if (ButtonShowMore != null) {
				ButtonShowMore.Dispose ();
				ButtonShowMore = null;
			}
		}
	}
}
