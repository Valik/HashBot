using System;
using System.Globalization;

namespace Touchin.HashBot.IPhone
{
	public class Status
	{
		public string CreatedAt { get; set; }
		public string Text { get; set; }
		public User User { get; set; }

		public DateTime TimeOfCreating 
		{ 
			get
			{
				if (CreatedAt == null)
					return DateTime.Now;

				var provider = CultureInfo.InvariantCulture;
				var date = DateTime.ParseExact(CreatedAt, "ddd MMM d HH:mm:ss %zzzz yyyy", provider);

				return date;
			}
		}
	}
}

