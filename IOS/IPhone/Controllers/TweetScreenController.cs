using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

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
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

