using System;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public class TabBarController : UITabBarController
	{
		public TabBarController () : base("TabBarController", null)
		{
			SetTabControllers();
		}

		private void SetTabControllers()
		{
			ViewControllers = new UIViewController[] {
				CreateController("#Twitter", "Images/TabBar/icon_twitter.png"),
				CreateController("#Dribbble", "Images/TabBar/icon_dribbble.png"),
				CreateController("#Apple", "Images/TabBar/icon_apple.png"),
				CreateController("#GitHub", "Images/TabBar/icon_github.png")
			};
		}

		private UINavigationController CreateController(string tabTitle, string imagePath)
		{
			var tweetsController = new TweetsTableViewController();
			tweetsController.Title = tabTitle;

			var navController = new UINavigationController(tweetsController);
			navController.TabBarItem.Title = tabTitle;
			navController.TabBarItem.Image = UIImage.FromFile(imagePath);

			return navController;
		}
	}
}

