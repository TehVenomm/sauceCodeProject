using System;

namespace Network
{
	[Serializable]
	public class FortuneHistoryData
	{
		public string lastUpdateTime = "";

		public FortuneWheelHistory history;
	}
}
