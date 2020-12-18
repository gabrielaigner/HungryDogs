using CommonBase.Extensions;
using HungryDogs.Contracts.Modules.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HungryDogs.Logic.Time
{
	static class TimeTable
	{
		public static IEnumerable<FromToTime> LoadTimeTable(Entities.Persistence.Restaurant restaurant)
		{
			restaurant.CheckArgument(nameof(restaurant));

			var result = new List<FromToTime>();

			if (restaurant.OpeningHours != null)
			{
				foreach (var item in restaurant.OpeningHours)
				{
					result.Add(new FromToTime(item.OpenFrom, item.OpenTo));
				}
			}
			if (restaurant.SepcialOpeningHours != null)
			{
				foreach (var item in restaurant.SepcialOpeningHours)
				{
					result.Add(new FromToTime(item.From, item.To, item.State));
				}
			}
			return result;
		}
		public static IEnumerable<FromToTime> CleanUp(IEnumerable<FromToTime> timeTable)
		{
			timeTable.CheckArgument(nameof(timeTable));

			var result = new List<FromToTime>(timeTable.OrderBy(e => e.From.ToDateSecondStamp()));
			var fromTo = result.FirstOrDefault();
			var now = DateTime.Now;

			if (fromTo != null && fromTo.From.ToTimeSecondStamp() > 0)
			{
				var from = new DateTime(now.Year, now.Month, now.Second, 0, 0, 0);
				var to = fromTo.To.AddSeconds(-1);

				result.Insert(0, new FromToTime(from, to, SpecialOpenState.Closed));
			}

			fromTo = result.LastOrDefault();
			if (fromTo != null && fromTo.To.ToTimeSecondStamp() < 235959)
			{
				var from = fromTo.To.AddSeconds(1);
				var to = new DateTime(from.Year, from.Month, from.Second, 23, 59, 59);

				result.Add(new FromToTime(from, to, SpecialOpenState.Closed));
			}

			return result;
		}
	}
}
