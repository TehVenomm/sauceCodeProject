using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class FortuneWheelData
	{
		public bool isOpen;

		public int loyaltyPoint;

		public int loyaltyPointRequired;

		public FortuneWheelInfo vaultInfo;

		public FortuneWheelHistory history;

		public List<FortuneWheelReward> spinRewards;

		public string lastUpdateTime = string.Empty;
	}
}
