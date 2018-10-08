using System;

namespace GetSocialSdk.Core
{
	public static class DateUtils
	{
		public static DateTime FromUnixTime(long unixTime)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)unixTime);
		}
	}
}
