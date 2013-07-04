using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using Touchin.HashBot.IPhone; 

namespace Touchin.HashBot
{
	public class TableCell : UITableViewCell
	{
		private const int _tweetTextLenght = 30;
		private UILabel _dateTimeLabel;

		public TableCell(UITableViewCellStyle style, string cellId) : base (style, cellId)
		{
			SetCellStyle();
			AddTimeLabel();
		}

		public void UpdateCell(Twitt twitt)
		{
			SetUserImage(twitt);
			
			TextLabel.Text = twitt.User.Name;
			DetailTextLabel.Text = twitt.Text;

			SetTime(twitt);
		}

		private void SetUserImage(Twitt twitt)
		{
			if (twitt.User.UserImage != null)
				ImageView.Image = CreateMaskedImage(twitt.User.UserImage);
			else
			{
				ImageView.Image = CreateMaskedImage(twitt.User.Avatar);
				DownloadAndSetImage(twitt);
			}
		}

		private void DownloadAndSetImage(Twitt twitt)
		{
			var imageLoader = new ImageLoader();
			imageLoader.DownloadImageForTwitt(twitt, (image, sourceTwitt) => 
			{
				InvokeOnMainThread(delegate
				{
					sourceTwitt.User.UserImage = image;
					ImageView.Image = CreateMaskedImage(sourceTwitt.User.UserImage);
				});
			});
		}

		private void SetCellStyle()
		{
			var backgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Images/Main/table.png"));
			var selectedBackground = UIColor.FromPatternImage(UIImage.FromFile("Images/Main/table_pressed.png"));

			var selectionView = new UIView();
			selectionView.BackgroundColor = selectedBackground;

			ContentView.BackgroundColor = backgroundColor;
			SelectedBackgroundView = selectionView;

			TextLabel.BackgroundColor = backgroundColor;
			DetailTextLabel.BackgroundColor = backgroundColor;
		}

		private void AddTimeLabel()
		{
			_dateTimeLabel = new UILabel(new RectangleF(285, 10, 35, 18));
			_dateTimeLabel.Font = UIFont.SystemFontOfSize(11);
			_dateTimeLabel.BackgroundColor = UIColor.Clear;
			_dateTimeLabel.TextColor = UIColor.FromRGB(137, 137, 137);
			AddSubview(_dateTimeLabel);
		}

		private UIImage CreateMaskedImage(UIImage originalImage)
		{
			var maskSource = UIImage.FromFile("Images/Main/mask_avatar_mini.png").CGImage;

			var mask = CGImage.CreateMask(maskSource.Width, 
			                              maskSource.Height, 
			                              maskSource.BitsPerComponent, 
			                              maskSource.BitsPerPixel, 
			                              maskSource.BytesPerRow, 
			                              maskSource.DataProvider,
			                              null,
			                              false);

			var maskedImage = originalImage.CGImage.WithMask(mask);

			return new UIImage(maskedImage);
		}		

		private void SetTime(Twitt twitt)
		{
			var diffTime = DateTime.Now - twitt.TimeOfCreating;

			if (diffTime.Days > 0)
			{
				_dateTimeLabel.Text = diffTime.Days + " д";
				return;
			}
			if (diffTime.Hours > 0)
			{
				_dateTimeLabel.Text = diffTime.Hours + " ч";
				return;
			}
			if (diffTime.Minutes > 0)
			{
				_dateTimeLabel.Text = diffTime.Minutes + " м";
				return;
			}
			if (diffTime.Seconds > 0)
			{
				_dateTimeLabel.Text = diffTime.Seconds + " с";
				return;
			}
		}
	}
}

