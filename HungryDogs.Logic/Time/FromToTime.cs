using CommonBase.Extensions;
using HungryDogs.Contracts.Modules.Common;
using System;

namespace HungryDogs.Logic.Time
{
	class FromToTime
	{
		public FromToTime(DateTime? from, DateTime? to, SpecialOpenState state)
		{
			var now = DateTime.Now;

			From = from.HasValue ? from.Value : new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
			To = to.HasValue ? to.Value : new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
			State = state;
		}
		public FromToTime(TimeSpan from, TimeSpan to)
			: this(from, to, SpecialOpenState.Open)
		{
		}
		public FromToTime(TimeSpan from, TimeSpan to, SpecialOpenState state)
		{
			var now = DateTime.Now;

			From = new DateTime(now.Year, now.Month, now.Day, from.Hours, from.Minutes, 0);
			To = new DateTime(now.Year, now.Month, now.Day, to.Hours, to.Minutes, 0);
			if (To < From)
			{
				To = To.AddDays(1);
			}
			State = state;
		}
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public SpecialOpenState State { get; set; }
		public long DifferenceStamp => To.ToDateSecondStamp() - From.ToDateSecondStamp();
	}
}
