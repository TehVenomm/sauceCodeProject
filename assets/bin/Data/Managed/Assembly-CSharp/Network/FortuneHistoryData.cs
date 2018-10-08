using System;

namespace Network
{
	[Serializable]
	public class FortuneHistoryData
	{
		public string lastUpdateTime = string.Empty;

		public FortuneWheelHistory history;
	}
}
