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
		private TwitterWorker _worker;
		private UIAlertView _alert;
		private bool _isInitState = true;

		public TweetsTableViewController (TabBarController tabController) : base ("TweetsTableViewController", null)
		{ }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			CreateAlert();
			StartShowAlert();
			SetRightBarButton();
			StartShowAlert();
			FillTweets();
			ConfigureTable();
			AddButtonShowMore();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		private void CreateAlert()
		{
			_alert = new UIAlertView(new RectangleF(10, 56, 300, 200));
			_alert.AlertViewStyle = UIAlertViewStyle.Default;
			_alert.Title = "HashBot";
			_alert.Message = "Загрузка данных...";
			var indicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
			indicator.Center = new PointF(139.5f, 100);
			indicator.StartAnimating();
			_alert.AddSubview(indicator);
		}

		private void StartShowAlert()
		{
			_alert.Show();
		}

		private void StopShowAlert()
		{
			_alert.DismissWithClickedButtonIndex(-1, true);
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
			StartShowAlert();
			AddMoreTwitts();
		}

		private void FillTweets()
		{
			_worker = new TwitterWorker();

			_worker.FillTwittsByHashTag(Title.TrimStart(new char[]{'#'}), OnTwittsCompleted);
		}

		private void OnTwittsCompleted (IEnumerable<TweetInfo> twitts)
		{
			InvokeOnMainThread(delegate 
			{
				AddTwitts(twitts);
				StopShowAlert();
				DowloadImagesForTwitts(twitts);
			});
		}

		private void AddTwitts(IEnumerable<TweetInfo> twitts)
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
			_worker.FillTwittsByHashTag(Title.TrimStart(new char[]{'#'}), OnTwittsCompleted);
		}

		private void RefreshTable(int scrollIndex)
		{
			TableWithTweets.ReloadData();
			if(_tweets.Count > 0)
				TableWithTweets.ScrollToRow(NSIndexPath.FromRowSection(scrollIndex, 0), UITableViewScrollPosition.Top, true);
		}

		private void DowloadImagesForTwitts(IEnumerable<TweetInfo> twitts)
		{
			foreach (var curTwittInfo in twitts)
				if (curTwittInfo.UserImage == null)
					DownloadUserImage(curTwittInfo);
		}

		private void DownloadUserImage(TweetInfo tweetInfo)
		{
			var imageWorker = new ImageWorker();
			imageWorker.DownloadImageForTwitt(tweetInfo, OnImageDownloadedForTwitt);
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
