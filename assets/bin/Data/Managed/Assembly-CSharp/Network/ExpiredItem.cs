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
			return (string.IsNullOrEmpty(expiredAt) || TimeManager.GetRemainTime(expiredAt).CompareTo(TimeSpan.Zero) > 0) && used == 0;
		}
	}
}
