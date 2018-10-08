using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class QuestCompleteData
	{
		public QuestCompleteRewardList reward = new QuestCompleteRewardList();

		public List<FollowPartyMember> friend;

		public int followNum;

		public List<PointEventCurrentData> pointEvent;

		public List<PointShopResultData> pointShop = new List<PointShopResultData>();

		public int guildPoint;

		public PointEventCurrentData pointExplore;

		public PointEventCurrentData waveMatchPoint;

		public PartyModel.Param repeatParty;
	}
}
