using System;

namespace Network
{
	[Serializable]
	public class PartyInviteCharaInfo : FriendCharaInfo
	{
		public bool invite;

		public bool canEntry;
	}
}
