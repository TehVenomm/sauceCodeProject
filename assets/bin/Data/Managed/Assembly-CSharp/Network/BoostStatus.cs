using System;

namespace Network
{
	[Serializable]
	public class BoostStatus
	{
		public int type;

		public int value;

		public EndDate endDate;

		public int endTimestamp;

		public USE_ITEM_EFFECT_TYPE Type => (USE_ITEM_EFFECT_TYPE)type;

		public string GetBoostRateText()
		{
			return ((float)(100 + value) / 100f).ToString(".#") + StringTable.Get(STRING_CATEGORY.STATUS, 1000u);
		}

		public string GetRemainTime()
		{
			return UIUtility.TimeFormat(endDate.CalcRemainTime());
		}

		public bool IsRemain()
		{
			if (endDate == null)
			{
				return value != 0;
			}
			DateTime now = TimeManager.GetNow();
			if (DateTime.Parse(endDate.date).Subtract(now).TotalSeconds > 0.0)
			{
				return true;
			}
			return false;
		}
	}
}
