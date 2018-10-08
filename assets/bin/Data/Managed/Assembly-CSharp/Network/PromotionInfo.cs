using System;

namespace Network
{
	[Serializable]
	public class PromotionInfo
	{
		public int promotionMaxCnt;

		public int promotionCnt;

		public int promotionReceivedCnt;

		public int promotionBannerId;

		public string newsUrl;

		public bool isPromotionEvent;
	}
}
