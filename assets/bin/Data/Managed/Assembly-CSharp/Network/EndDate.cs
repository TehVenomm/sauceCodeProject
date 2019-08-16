using System;

namespace Network
{
	[Serializable]
	public class EndDate
	{
		public string date;

		public int timezone_type;

		public string timezone;

		public DateTime ConvToDateTime()
		{
			return DateTime.Parse(date).AddSeconds(1.0);
		}

		public TimeSpan CalcRemainTime()
		{
			DateTime now = TimeManager.GetNow();
			return ConvToDateTime().Subtract(now);
		}
	}
}
