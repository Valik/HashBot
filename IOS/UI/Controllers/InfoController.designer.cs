// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Touchin.HashBot
{
	[Register ("InfoController")]
	partial class InfoController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView CompanyLogoImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton CallButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton MailButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CompanyLogoImage != null) {
				CompanyLogoImage.Dispose ();
				CompanyLogoImage = null;
			}

			if (CallButton != null) {
				CallButton.Dispose ();
				CallButton = null;
			}

			if (MailButton != null) {
				MailButton.Dispose ();
				MailButton = null;
			}
		}
	}
}
