using System;

namespace CommonBase.Extensions
{
	public static class DateTimeExtensions
	{
		public static long ToYearStamp(this DateTime source)
		{
			return source.Year;
		}
		public static long ToMonthStamp(this DateTime source)
		{
			return source.ToYearStamp() * 10_000 + source.Month;
		}
		public static long ToDayStamp(this DateTime source)
		{
			return source.ToMonthStamp() * 100 + source.Day;
		}
		public static long ToDateHourStamp(this DateTime source)
		{
			return source.ToDayStamp() * 100 + source.Hour;
		}
		public static long ToDateMinuteStamp(this DateTime source)
		{
			return source.ToDateHourStamp() * 100 + source.Minute;
		}
		public static long ToDateSecondStamp(this DateTime source)
		{
			return source.ToDateMinuteStamp() * 100 + source.Second;
		}

		public static long ToTimeHourStamp(this DateTime source)
		{
			return source.Hour;
		}
		public static long ToTimeMinuteStamp(this DateTime source)
		{
			return source.ToTimeHourStamp() * 100 + source.Minute;
		}
		public static long ToTimeSecondStamp(this DateTime source)
		{
			return source.ToTimeMinuteStamp() * 100 + source.Second;
		}

        public static DateTime SetTimeSpan(this DateTime now, TimeSpan timeSpan)
        {
            DateTime dateTime = now.Date;
            dateTime = dateTime.AddHours(timeSpan.Hours);
            dateTime = dateTime.AddMinutes(timeSpan.Minutes);
            dateTime = dateTime.AddSeconds(timeSpan.Seconds);
            return dateTime;
        }
	}
}