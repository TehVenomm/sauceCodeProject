using System;

namespace Network
{
	[Serializable]
	public class ExpiredItem
	{
		public string uniqId;

		public int itemId;

		public int used;

		public string expiredAt;

		public bool CanUse()
		{
			if (string.IsNullOrEmpty(expiredAt) || TimeManager.GetRemainTime(expiredAt).CompareTo(TimeSpan.Zero) > 0)
			{
				return used == 0;
			}
			return false;
		}
	}
}
