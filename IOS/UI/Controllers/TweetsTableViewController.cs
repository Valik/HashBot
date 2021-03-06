using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Touchin.HashBot.IPhone;
using System.Linq;

namespace Touchin.HashBot
{
	public partial class TweetsTableViewController : UIViewController
	{
		private TweetsTableSource _tableSource = new TweetsTableSource();
		private TwitterWorker _worker = new TwitterWorker();
		private UIAlertView _alert;
		private string _hashTag;

		public TweetsTableViewController (string hashTag) : base ("TweetsTableViewController", null)
		{
			_hashTag = hashTag;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "#" + _hashTag;
			CreateAlert();
			SetRightBarButton();
			AddTwittsAsync();
			ConfigureTable();
			AddButtonShowMore();
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

		private void AddTwittsAsync()
		{
			StartShowAlert();
			_worker.FillTwittsByHashTag(_hashTag, OnTwittsLoadingCompleted);
		}

		private void StartShowAlert()
		{
			_alert.Show();
		}

		private void StopShowAlert()
		{
			_alert.DismissWithClickedButtonIndex(-1, true);
		}

		private void OnCellSelected(Twitt twitt)
		{
			var tweetController = new TweetScreenController(twitt);
			NavigationController.PushViewController(tweetController, true);
			TableWithTweets.DeselectRow(TableWithTweets.IndexPathForSelectedRow, false);
		}

		private void SetRightBarButton()
		{
			var rightBurButton = new UIBarButtonItem();
			rightBurButton.Title = "Инфо";
			NavigationItem.RightBarButtonItem = rightBurButton;
			rightBurButton.Clicked += (s, e) =>
			{
				NavigationController.PushViewController(new InfoController(), true);
			};
		}

		private void AddButtonShowMore()
		{
			var button = CreateButton();
			var buttonContainer = new UIView(new RectangleF(0, 0, 320, 60));
			buttonContainer.Add(button);

			TableWithTweets.TableFooterView = buttonContainer;

			button.TouchUpInside += (s, e) => 
			{
				AddTwittsAsync();
			};
		}

		private void ConfigureTable()
		{
			_tableSource.CellSelected += OnCellSelected;
			TableWithTweets.Source = _tableSource;
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

		private void OnTwittsLoadingCompleted (IEnumerable<Twitt> twitts)
		{
			InvokeOnMainThread(delegate 
			{
				int scrollIndex = _tableSource.ItemsCount;
				_tableSource.AddTwitts(twitts);

				TableWithTweets.ReloadData();
				TableWithTweets.ScrollToRow(NSIndexPath.FromRowSection(scrollIndex, 0), UITableViewScrollPosition.Top, true);
				StopShowAlert();
			});
		}
	}
}
