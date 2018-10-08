using System;

namespace Network
{
	[Serializable]
	public class FortuneWheelData
	{
		public bool isOpen;

		public int loyaltyPoint;

		public FortuneWheelInfo vaultInfo;

		public FortuneWheelHistory history;
	}
}
