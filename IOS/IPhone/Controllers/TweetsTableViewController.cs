using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public partial class TweetsTableViewController : UIViewController
	{
		private List<TweetInfo> _tweets;
		private UITabBarController _ownerTabController;

		public TweetsTableViewController (UITabBarController tabController) : base ("TweetsTableViewController", null)
		{
			_ownerTabController = tabController;
			SetRightBarButton();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			CapFillTweets();
			AddButtonShowMore();
			ConfigureTable();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			UnhideTabBar();
		}

		private void UnhideTabBar()
		{
			if (_ownerTabController.TabBar.Hidden == true)
				_ownerTabController.TabBar.Hidden = false;
		}

		private void OnCellSelected(TweetInfo tweetInfo)
		{			
			_ownerTabController.TabBar.Hidden = true;


			var tweetController = new TweetScreenController(tweetInfo);
			NavigationController.PushViewController(tweetController, true);
		}

		private void SetRightBarButton()
		{
			var rightBurButton = new UIBarButtonItem();
			rightBurButton.Title = "Инфо";
			NavigationItem.RightBarButtonItem = rightBurButton;
		}

		private void AddButtonShowMore()
		{
			var button = CreateButton();
			var buttonContainer = new UIView(new RectangleF(0, 0, 60, 320));
			buttonContainer.Add(button);

			TableWithTweets.TableFooterView = buttonContainer;

			button.TouchUpInside += (sender, e) => 
			{
				ShowMoreTweets();
			};
		}

		private void ConfigureTable()
		{
			var tableSource = new TweetsTableSource(_tweets);
			tableSource.CellSelected += OnCellSelected;
			TableWithTweets.Source = tableSource;
		}

		private UIButton CreateButton()
		{
			UIButton button = new UIButton(UIButtonType.RoundedRect);
			button.Frame = new RectangleF(10, 15, 300, 44);

			button.LineBreakMode = UILineBreakMode.MiddleTruncation;
			button.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
			button.VerticalAlignment = UIControlContentVerticalAlignment.Center;
			button.Font = UIFont.BoldSystemFontOfSize(15);
			button.SetTitleColor(UIColor.Black, UIControlState.Normal);
			button.SetTitle("Показать ещё", UIControlState.Normal);
			button.SetBackgroundImage(UIImage.FromFile("Images/Main/table.png"), UIControlState.Normal);
			button.SetBackgroundImage(UIImage.FromFile("Images/Main/table_pressed.png"), UIControlState.Selected);
			button.Layer.CornerRadius = 8f;
			button.Layer.MasksToBounds = true;
			button.Layer.BorderColor = UIColor.LightGray.CGColor;
			button.Layer.BorderWidth = 1;
			button.Layer.ShadowOffset = new SizeF(2, 1);
			button.Layer.ShadowColor = UIColor.FromRGBA(0, 0, 0, 50).CGColor;

			return button;
		}

		private void ShowMoreTweets ()
		{
			var scrollIndex = CapAddTweets();
			TableWithTweets.ReloadData ();
			TableWithTweets.ScrollToRow (NSIndexPath.FromRowSection (scrollIndex, 0), UITableViewScrollPosition.Top, true);
		}

		private void CapFillTweets()
		{
			_tweets = new List<TweetInfo>() {
				new TweetInfo("name1", "text1"),
				new TweetInfo("name2", "text2"),
				new TweetInfo("name3", "text3aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"),
				new TweetInfo("name4", "text4"),
				new TweetInfo("name6", "text6aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
			};
		}

		private int CapAddTweets()
		{
			int scrollIndex = _tweets.Count - 1;
			_tweets.Add(new TweetInfo("name7", "text7"));
			_tweets.Add(new TweetInfo("name8", "text8"));
			_tweets.Add(new TweetInfo("name9", "text9"));
			_tweets.Add(new TweetInfo("name10", "text10"));
			return scrollIndex;
		}
	}
}
