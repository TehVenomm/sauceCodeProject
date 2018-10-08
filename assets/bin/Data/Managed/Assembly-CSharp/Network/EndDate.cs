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
			DateTime result = DateTime.Parse(date);
			result = result.AddSeconds(1.0);
			return result;
		}

		public TimeSpan CalcRemainTime()
		{
			DateTime now = TimeManager.GetNow();
			return ConvToDateTime().Subtract(now);
		}
	}
}
