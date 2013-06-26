// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Bot
{
	[Register ("MainScreen")]
	partial class MainScreen
	{
		[Outlet]
		MonoTouch.UIKit.UITableView TableOfTwits { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ButtonShowMore { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TableOfTwits != null) {
				TableOfTwits.Dispose ();
				TableOfTwits = null;
			}

			if (ButtonShowMore != null) {
				ButtonShowMore.Dispose ();
				ButtonShowMore = null;
			}
		}
	}
}
