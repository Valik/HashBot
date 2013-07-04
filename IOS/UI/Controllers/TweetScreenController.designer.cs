// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Touchin.HashBot
{
	[Register ("TweetScreenController")]
	partial class TweetScreenController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView UserImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel UserName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel TweetLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView LineImageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DateLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (UserImage != null) {
				UserImage.Dispose ();
				UserImage = null;
			}

			if (UserName != null) {
				UserName.Dispose ();
				UserName = null;
			}

			if (TweetLabel != null) {
				TweetLabel.Dispose ();
				TweetLabel = null;
			}

			if (LineImageView != null) {
				LineImageView.Dispose ();
				LineImageView = null;
			}

			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}
		}
	}
}
