using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace Touchin.HashBot
{
	public partial class TweetScreenController : UIViewController
	{
		TweetInfo _tweetInfo;

		public TweetScreenController(TweetInfo tweetInfo) : base ("TweetScreenController", null)
		{
			_tweetInfo = tweetInfo;
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			SetStyle();
		}

		private void SetStyle()
		{
			SetVieStyle();
			SetUserImageStyle();

			UserName.Text = _tweetInfo.UserName;

			TweetLabel.Text = _tweetInfo.TweetText;
		}

		void SetVieStyle()
		{
			View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Images/Tweets/bg.png"));
			NavigationItem.LeftBarButtonItem = CreateBackButton();
			NavigationItem.Title = "Твит";
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

		private void SetUserImageStyle()
		{
			var image = UIImage.FromFile("Images/Main/avatar_big.png");
			UserImage.Image = CreateUserImage(image);
			UserImage.Layer.CornerRadius = 5;
			UserImage.Layer.MasksToBounds = true;
			UserImage.Layer.BorderWidth = 1;
			UserImage.Layer.BorderColor = UIColor.Black.CGColor;
			UserImage.Layer.ShadowOffset = new SizeF(1, 1);
			UserImage.Layer.ShadowColor = UIColor.FromRGBA(0, 0, 0, 50).CGColor;
		}

		private void OnBarButtonClicked (object sender, EventArgs e)
		{
			NavigationController.PopViewControllerAnimated(true);
		}

		private UIImage CreateUserImage(UIImage targetImage)
		{
			/*
			var maskImage = UIImage.FromFile("Images/Main/mask_avatar.png").CGImage;

			maskImage.
			var userImage = targetImage.CGImage.WithMask(maskImage);
			var retImage = new UIImage(userImage);

			return retImage;*/

			CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();
		
			// create a bitmap graphics context the size of the image
			var mainViewContentContext = new CGBitmapContext(null, (int)targetImage.Size.Width, (int)targetImage.Size.Height,
			                                             8, 0, colorSpace, CGImageAlphaInfo.PremultipliedFirst);
			// free the rgb colorspace
			colorSpace.Dispose();    

			if (mainViewContentContext == null)
				return null;

			var maskImage = UIImage.FromFile("Images/Main/mask_avatar.png").CGImage;

			mainViewContentContext.ClipToMask(new RectangleF(0, 0, targetImage.Size.Width, targetImage.Size.Height), targetImage.CGImage);
			mainViewContentContext.DrawImage(new RectangleF(0, 0, targetImage.Size.Width, targetImage.Size.Height), targetImage.CGImage);

			// Create CGImageRef of the main view bitmap content, and then
			// release that bitmap context

			var cgImage = mainViewContentContext.ToImage();
			mainViewContentContext.Dispose();

			// convert the finished resized image to a UIImage 
			var userImage = new UIImage(cgImage);

			return userImage;
		}
	}
}

