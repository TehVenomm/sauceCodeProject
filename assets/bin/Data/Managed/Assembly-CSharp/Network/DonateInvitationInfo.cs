using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class DonateInvitationInfo
	{
		public int id;

		public int userId;

		public string nickName;

		public string msg;

		public int itemId;

		public string itemName;

		public int itemNum;

		public List<DonateHelperInfo> helpers;

		public int quantity;

		public double expired;

		public int status;

		public DonateInfo ParseDonateInfo()
		{
			return new DonateInfo
			{
				id = id,
				nickName = nickName,
				materialName = itemName,
				userId = userId,
				msg = msg,
				itemId = itemId,
				itemNum = itemNum,
				quantity = quantity
			};
		}
	}
}
