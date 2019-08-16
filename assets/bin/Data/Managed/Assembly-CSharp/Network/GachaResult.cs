using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class GachaResult
	{
		public class GachaReward
		{
			public int rewardType;

			public int itemId;

			public int param_0;

			public int lotGroupNo;
		}

		public class OncePurchaseItemToShop
		{
			public string productId = string.Empty;
		}

		public List<GachaReward> reward;

		public int remainCount = -1;

		public int counter;

		public bool oncePurchasedState;

		public GachaGuaranteeCampaignInfo gachaGuaranteeCampaignInfo;

		public OncePurchaseItemToShop oncePurchaseItemToShop;

		public string buttonImg = string.Empty;

		public string detailButtonImg = string.Empty;
	}
}
