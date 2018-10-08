using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class FollowPartyMember
	{
		public int userId;

		public bool following;

		public bool follower;

		public List<int> selectedDegrees;
	}
}
