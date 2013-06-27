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

		public TweetsTableViewController () : base ("TweetsTableViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_tweets = new List<TweetInfo> () 
			{
				new TweetInfo("name1" , "text1"),
				new TweetInfo("name2" , "text2"),
				new TweetInfo("name3" , "text3aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"),
				new TweetInfo("name4" , "text4"),
				new TweetInfo("name5" , "text5"),
				new TweetInfo("name6" , "text6aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
			};

			TableWithTweets.Source = new TweetsTableSource (_tweets);

			ButtonShowMore.TouchUpInside += (sender, e) => 
			{
				ShowMoreTweets ();
			};
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void ShowMoreTweets ()
		{
			int scrollIndex = _tweets.Count - 1;
			_tweets.Add (new TweetInfo ("name7", "text7"));
			_tweets.Add (new TweetInfo ("name8", "text8"));
			_tweets.Add (new TweetInfo ("name9", "text9"));
			_tweets.Add (new TweetInfo ("name10", "text10"));
			_tweets.Add (new TweetInfo ("name11", "text11"));
			_tweets.Add (new TweetInfo ("name12", "text12"));
			TableWithTweets.ReloadData ();
			TableWithTweets.ScrollToRow (NSIndexPath.FromRowSection (scrollIndex, 0), UITableViewScrollPosition.Middle, true);
		}
	}
}
