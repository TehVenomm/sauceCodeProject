using Network;
using System;
using System.Collections.Generic;

public class GuildDonate
{
	public class GuildDonateSendModel : BaseModel
	{
		public class Form
		{
			public int id;

			public int quantity;
		}

		public static string URL = "clan/DonateSend.go";
	}

	public class GuildDonateRequestModel : BaseModel
	{
		public class Form
		{
			public int itemId;

			public string itemName;

			public string msg;

			public int quantity;

			public string nickName;
		}

		public class Param
		{
			public string msg;

			public int itemId;

			public int itemNum;

			public int quantity;

			public string expired;
		}

		public static string URL = "clan/DonateCreate.go";

		public Param result = new Param();
	}

	public class GuildDonateReceiveModel : BaseModel
	{
		public static string URL = "clan/DonateReceive.go";
	}

	public class GuildDonateListModel : BaseModel
	{
		[Serializable]
		public class Param
		{
			public List<DonateInfo> array = new List<DonateInfo>();

			public DonateInfo pinDonate;
		}

		public static string URL = "clan/DonateList.go";

		public Param result = new Param();
	}

	public class GuildDonateInviteModel : BaseModel
	{
		public class Form
		{
			public int id;

			public int userId;
		}

		public static string URL = "clan/DonateInvite.go";
	}

	public class GuildDonateInviteListModel : BaseModel
	{
		public class Form
		{
			public int id;
		}

		[Serializable]
		public class Result
		{
			public List<FriendCharaInfo> list = new List<FriendCharaInfo>();

			public List<DonateListInfo> donate_list = new List<DonateListInfo>();
		}

		[Serializable]
		public class DonateListInfo
		{
			public bool isInvited;

			public int userId;
		}

		public static string URL = "clan/DonateInviteList.go";

		public Result result;
	}

	public class GuildDonateInvitationListModel : BaseModel
	{
		[Serializable]
		public class Result
		{
			public List<DonateInvitationInfo> array = new List<DonateInvitationInfo>();
		}

		public static string URL = "clan/DonateAskList.go";

		public Result result;
	}

	public class GuildDonateFobbidenModel : BaseModel
	{
		[Serializable]
		public class Param
		{
			public List<int> array = new List<int>();
		}

		public static string URL = "clan/DonateForbiddenList.go";

		public Param result = new Param();
	}
}
