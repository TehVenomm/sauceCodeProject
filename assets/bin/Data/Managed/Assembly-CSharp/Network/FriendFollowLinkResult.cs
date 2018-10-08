using System;

namespace Network
{
	[Serializable]
	public class FriendFollowLinkResult
	{
		public string link;

		public string linkUrl;

		public string followCode;

		public int followMaxCnt;

		public int followCnt;

		public int remainedCampaignNum;

		public int remainedLoungeFirstMetNum;

		public string message;

		public PromotionInfo promotionInfo;
	}
}
