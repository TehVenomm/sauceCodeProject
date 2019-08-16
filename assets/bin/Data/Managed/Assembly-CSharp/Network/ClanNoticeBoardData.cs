namespace Network
{
	public class ClanNoticeBoardData
	{
		public string clanId = string.Empty;

		public string contributorUserId = string.Empty;

		public string contributorUserName = string.Empty;

		public int version = -1;

		public string body = string.Empty;

		public EndDate updatedAt;

		public UserClanData contributorUserClan;
	}
}
