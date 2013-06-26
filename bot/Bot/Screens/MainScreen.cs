using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Bot
{
	public partial class MainScreen : UIViewController
	{
		private List<TwiteInfo> _tableItems;

		public MainScreen () : base ("MainScreen", null)
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

			_tableItems = new List<TwiteInfo> () 
			{
				new TwiteInfo("name1" , "text1"),
				new TwiteInfo("name2" , "text2"),
				new TwiteInfo("name3" , "text3"),
				new TwiteInfo("name4" , "text4"),
				new TwiteInfo("name5" , "text5"),
				new TwiteInfo("name6" , "text6")
			};



			TableOfTwits.Source = new TableSource(_tableItems);



			ButtonShowMore.TouchUpInside += (sender, e) => 
			{
				_tableItems.RemoveAt(_tableItems.Count - 1);
			};
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

