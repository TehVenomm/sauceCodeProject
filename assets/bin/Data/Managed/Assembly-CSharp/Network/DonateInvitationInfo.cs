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
			DonateInfo donateInfo = new DonateInfo();
			donateInfo.id = id;
			donateInfo.nickName = nickName;
			donateInfo.materialName = itemName;
			donateInfo.userId = userId;
			donateInfo.msg = msg;
			donateInfo.itemId = itemId;
			donateInfo.itemNum = itemNum;
			donateInfo.quantity = quantity;
			return donateInfo;
		}
	}
}
