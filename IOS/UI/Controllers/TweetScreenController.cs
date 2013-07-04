using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using Touchin.HashBot.IPhone;

namespace Touchin.HashBot
{
	public partial class TweetScreenController : UIViewController
	{
		private Status _twitt;

		public TweetScreenController(Status twitt) : base ("TweetScreenController", null)
		{
			_twitt = twitt;
			HidesBottomBarWhenPushed = true;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			SetStyle();
		}

		private void SetStyle()
		{
			SetViewStyle();
			SetUserImageStyle();

			UserName.Text = _twitt.User.Name;

			TweetLabel.Text = _twitt.Text;

			LineImageView.Image = UIImage.FromFile("Images/Tweets/line.png");
		}

		private void SetViewStyle()
		{
			View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Images/Tweets/bg.png"));
			NavigationItem.LeftBarButtonItem = CreateBackButton();
			NavigationItem.Title = "Твит";
		}

		private void SetUserImageStyle()
		{
			if (_twitt.User.UserImage != null)
				UserImage.Image = CreateMaskedImage(_twitt.User.UserImage);
			else
				UserImage.Image = CreateMaskedImage(_twitt.User.AvatarBig);
		}
		
		private UIBarButtonItem CreateBackButton()
		{
			var dummyButton = UIButton.FromType(UIButtonType.Custom);
			var backButton = (UIButton)MonoTouch.ObjCRuntime.Runtime.GetNSObject(
				MonoTouch.ObjCRuntime.Messaging.IntPtr_objc_msgSend_int(
				dummyButton.ClassHandle, MonoTouch.ObjCRuntime.Selector.GetHandle("buttonWithType:"), 101));

			backButton.SetTitle("Твиты", UIControlState.Normal);
			backButton.TouchUpInside += OnBarButtonClicked;

			return new UIBarButtonItem(backButton);;
		}

		private void OnBarButtonClicked (object sender, EventArgs e)
		{
			NavigationController.PopViewControllerAnimated(true);
		}

		private UIImage CreateMaskedImage(UIImage originalImage)
		{
			var maskSource = UIImage.FromFile("Images/Main/mask_avatar.png").CGImage;

			var mask = CGImage.CreateMask(maskSource.Width, 
			                              maskSource.Height, 
			                              maskSource.BitsPerComponent, 
			                              maskSource.BitsPerPixel, 
			                              maskSource.BytesPerRow, 
			                              maskSource.DataProvider,
			                              null,
			                              true);

			var maskedImage = originalImage.CGImage.WithMask(mask);

			return new UIImage(maskedImage);
		}
	}
}

