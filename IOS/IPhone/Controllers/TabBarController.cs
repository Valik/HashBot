using System;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public class TabBarController : UITabBarController
	{
		TweetsTableViewController _twitterTagController;
		TweetsTableViewController _dribbbleTagController;
		TweetsTableViewController _appleTagController;
		TweetsTableViewController _githubTagController;

		private UILabel _navBarTitle;

		public TabBarController () : base("TabBarController", null)
		{
			_twitterTagController = new TweetsTableViewController ();
			_twitterTagController.TabBarItem.Title = "#Twitter";
			_twitterTagController.TabBarItem.Image = UIImage.FromFile("Images/TabBar/icon_twitter.png");

			_dribbbleTagController = new TweetsTableViewController ();
			_dribbbleTagController.TabBarItem.Title = "#Dribbble";
			_dribbbleTagController.TabBarItem.Image = UIImage.FromFile("Images/TabBar/icon_dribbble.png");

			_appleTagController = new TweetsTableViewController ();
			_appleTagController.TabBarItem.Title = "#Apple";
			_appleTagController.TabBarItem.Image = UIImage.FromFile("Images/TabBar/icon_apple.png");

			_githubTagController = new TweetsTableViewController ();
			_githubTagController.TabBarItem.Title = "#GitHub";
			_githubTagController.TabBarItem.Image = UIImage.FromFile("Images/TabBar/icon_github.png");

			this.ViewControllers = new UIViewController[] 
			{
				_twitterTagController,
				_dribbbleTagController,
				_appleTagController,
				_githubTagController
			};

			_navBarTitle = new UILabel ();
			_navBarTitle.Text = "#Twitter";


			_navBarTitle = new UILabel();
			_navBarTitle.Frame = new System.Drawing.RectangleF(0, 0, 150, 44);
			_navBarTitle.TextAlignment = UITextAlignment.Center;
			_navBarTitle.BackgroundColor = UIColor.Clear;
			
			_navBarTitle.Font = UIFont.BoldSystemFontOfSize (20);
			_navBarTitle.TextColor = UIColor.White;
			_navBarTitle.ShadowColor = UIColor.FromRGBA(256, 256, 256, 50);
			_navBarTitle.ShadowOffset = new System.Drawing.SizeF (1, 0);


			_navBarTitle.Text = "#Twitter";

			this.NavigationItem.TitleView = _navBarTitle;

			var rightBurButton = new UIBarButtonItem ();
			rightBurButton.Title = "Инфо";

			this.NavigationItem.RightBarButtonItem = rightBurButton;

			this.ViewControllerSelected += (sender, e) => 
			{
				switch(SelectedIndex)
				{
					case 0:
						_navBarTitle.Text = "#Twitter";
						break;
					case 1:
						_navBarTitle.Text = "#Dribbble";
						break;
					case 2:
						_navBarTitle.Text = "#Apple";
						break;
					case 3:
						_navBarTitle.Text = "#GitHub";
						break;

				}
			};
		}


	}
}

