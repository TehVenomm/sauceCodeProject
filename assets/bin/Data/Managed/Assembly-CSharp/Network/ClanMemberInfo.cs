namespace Network
{
	public class ClanMemberInfo : FriendCharaInfo
	{
		public new class ClanInfo
		{
			public int ClanId = -1;

			public string ClanName = string.Empty;

			public int ClanMemberId = -1;
		}

		public class CurrentActivityInfo
		{
			public int LoungeNo = -1;

			public int RoomNo = -1;

			public int DeliveryId = -1;
		}

		public ClanInfo myClanInfo;

		public CurrentActivityInfo myActivityInfo;

		public bool IsJoinClan()
		{
			return myClanInfo != null;
		}

		public bool IsJoinClan(int _alianceId)
		{
			return IsJoinClan() && _alianceId == myClanInfo.ClanId;
		}
	}
}
