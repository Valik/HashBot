using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public partial class InfoController : UIViewController
	{
		private string _companyTelNumber = "89500000000";
		public InfoController() : base ("InfoController", null)
		{
			HidesBottomBarWhenPushed = true;
			Title = "Инфо";
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			CompanyLogoImage.Image = UIImage.FromFile("Images/Info/logo.png");

			SetButtonsStyle();
		}
		
		private void SetButtonsStyle()
		{
			UIImage image = UIImage.FromFile("Images/Info/button.png");
			CallButton.SetBackgroundImage(UIImage.FromFile("Images/Info/button.png"), UIControlState.Normal);
			CallButton.SetBackgroundImage(UIImage.FromFile("Images/Info/button_pressed.png"), UIControlState.Selected);
			CallButton.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Images/Info/button.png"));
			CallButton.TouchUpInside += OnCallButtonClicked;

			MailButton.ContentMode = UIViewContentMode.Center;
			MailButton.Image = image;
		}

		private void OnCallButtonClicked(object sender, EventArgs e)
		{
			NSUrl companyPhoneURl = new NSUrl("tel:" + _companyTelNumber);
			if (!UIApplication.SharedApplication.OpenUrl(companyPhoneURl))
			{
				int i = 0;
				i++;
			}

			NSUrl testUrl = new NSUrl("http://ya.ru");

			if (!UIApplication.SharedApplication.OpenUrl(testUrl))
			{
				int i = 0;
				i++;
			}
		}
	}
}

