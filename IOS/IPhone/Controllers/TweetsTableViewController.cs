using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Touchin.HashBot
{
	public partial class TweetsTableViewController : UIViewController
	{
		private List<TweetInfo> _tweets = new List<TweetInfo>();
		private bool _isInitState = true;
		private TwitterWorker _worker;

		public TweetsTableViewController (TabBarController tabController) : base ("TweetsTableViewController", null)
		{ }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			SetRightBarButton();
			FillTweets();
			ConfigureTable();
			AddButtonShowMore();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		private void OnCellSelected(TweetInfo tweetInfo)
		{
			var tweetController = new TweetScreenController(tweetInfo);
			NavigationController.PushViewController(tweetController, true);
			TableWithTweets.DeselectRow(TableWithTweets.IndexPathForSelectedRow, false);
		}

		private void SetRightBarButton()
		{
			var rightBurButton = new UIBarButtonItem();
			rightBurButton.Title = "Инфо";
			NavigationItem.RightBarButtonItem = rightBurButton;
			rightBurButton.Clicked += OnInfoButtonClicked;
		}

		private void OnInfoButtonClicked (object sender, EventArgs e)
		{
			NavigationController.PushViewController(new InfoController(), true);
		}

		private void AddButtonShowMore()
		{
			var button = CreateButton();
			var buttonContainer = new UIView(new RectangleF(0, 0, 320, 60));
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
			AddMoreTwitts();
		}

		private void FillTweets()
		{
			_worker = new TwitterWorker();

			_worker.TwittsCompleted += OnTwittsCompleted;
			_worker.FillTwittsByHashTag(Title.TrimStart(new char[]{'#'}));
		}

		private void OnTwittsCompleted (List<TweetInfo> twitts)
		{
			InvokeOnMainThread(delegate 
			{
				AddTwitts(twitts);
				DowloadImagesForTwitts(twitts);
			});
		}

		private void AddTwitts(List<TweetInfo> twitts)
		{
			if (_isInitState)
			{
				_tweets.AddRange(twitts);
				RefreshTable(0);
				_isInitState = false;
			}
			else
			{
				int scrollIndex = _tweets.Count;
				_tweets.AddRange(twitts);
				RefreshTable(scrollIndex);
			}
		}

		private void AddMoreTwitts()
		{
			_worker.FillTwittsByHashTag(Title.TrimStart(new char[]{'#'}));
		}

		private void RefreshTable(int scrollIndex)
		{
			TableWithTweets.ReloadData();
			if(_tweets.Count > 0)
				TableWithTweets.ScrollToRow(NSIndexPath.FromRowSection(scrollIndex, 0), UITableViewScrollPosition.Top, true);
		}

		private void DowloadImagesForTwitts(List<TweetInfo> twitts)
		{
			foreach (var curTwittInfo in twitts)
				if (curTwittInfo.UserImage == null)
					DownloadUserImage(curTwittInfo);
		}

		private void DownloadUserImage(TweetInfo tweetInfo)
		{
			var imageWorker = new ImageWorker(OnImageDownloadedForTwitt, tweetInfo);
			imageWorker.BeginDownloadingImage(tweetInfo.ImageSrc);
		}

		private void OnImageDownloadedForTwitt(UIImage userImage, TweetInfo tweetInfo)
		{
			InvokeOnMainThread(delegate 
			{
				tweetInfo.UserImage = userImage;
				TableWithTweets.ReloadData();
			});
		}
	}
}
