using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public partial class InfoController : UIViewController
	{
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
			var callImage = UIImage.FromFile("Images/Info/icon_phone.png");
			var mailImage = UIImage.FromFile("Images/Info/icon_mail.png"); 
			var bgImage = UIImage.FromFile("Images/Info/button.png");
			var bgPressedImage = UIImage.FromFile("Images/Info/button_pressed.png");

			var buttonImage = bgImage.StretchableImage((int)((bgImage.Size.Width - 1) / 2), (int)CallButton.Frame.Height);
			var pressedButtonImage = bgPressedImage.StretchableImage((int)((bgPressedImage.Size.Width - 1) / 2), (int)CallButton.Frame.Height); 

			CallButton.SetBackgroundImage(buttonImage, UIControlState.Normal);
			CallButton.SetBackgroundImage(pressedButtonImage, UIControlState.Selected);
			CallButton.SetImage(callImage, UIControlState.Normal);
			CallButton.ImageEdgeInsets = new UIEdgeInsets(-7, 0, 0, 0);
			CallButton.TouchUpInside += OnCallButtonClicked;

			MailButton.SetBackgroundImage(buttonImage, UIControlState.Normal);
			MailButton.SetBackgroundImage(pressedButtonImage, UIControlState.Selected);
			MailButton.SetImage(mailImage, UIControlState.Normal);
			MailButton.ImageEdgeInsets = new UIEdgeInsets(-7, 0, 0, 0);
			MailButton.TouchUpInside += OnMailButtonClicked;
		}

		private void OnCallButtonClicked(object sender, EventArgs e)
		{
			TwitterTestMethod();

			var companyPhoneURl = new NSUrl("tel:89500000000");
			if (!UIApplication.SharedApplication.OpenUrl(companyPhoneURl))
			{
			}
		}

		private void OnMailButtonClicked(object sender, EventArgs e)
		{
			var companyUrl = new NSUrl("http://touchin.ru/");
			if (!UIApplication.SharedApplication.OpenUrl(companyUrl))
			{
			}
		}

		private void TwitterTestMethod()
		{
			TwitterWorker worker = new TwitterWorker();
			worker.FillTwittsByHashTag("GitHub");
		}
	}
}

