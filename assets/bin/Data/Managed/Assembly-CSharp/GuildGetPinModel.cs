using Network;
using System;

public class GuildGetPinModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int fromUserId;

		public int id;

		public int type;

		public string message;

		public string uuid;

		public CharaInfo charInfo;
	}

	public class PinData
	{
		public int fromUserId;

		public int id;

		public int type;

		public string message;

		public string uuid;

		public CharaInfo charInfo;
	}

	public static string URL = "clan/ClanPin.go";

	public Param result = new Param();
}
