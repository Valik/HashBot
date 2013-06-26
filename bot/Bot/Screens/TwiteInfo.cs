using System;

namespace Bot
{
	public class TwiteInfo
	{
		private string _userName;
		private string _twiteText;

		public string UserName { get { return _userName; } }

		public string TwiteText { get { return _twiteText; } }

		public TwiteInfo (string userName, string twiteText)
		{
			_userName = userName;
			_twiteText = twiteText;
		}
	}
}

