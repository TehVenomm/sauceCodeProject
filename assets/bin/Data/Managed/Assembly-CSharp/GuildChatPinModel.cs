using Network;
using System;

public class GuildChatPinModel : BaseModel
{
	[Serializable]
	public class Param
	{
		public int id;

		public string uuid;

		public string message;

		public int type;

		public CharaInfo charInfo;
	}

	public class SendForm
	{
		public int id;

		public string uuid;

		public string message;

		public int type;

		public int fromUserId;
	}

	public static string URL = "clan/ChatPin.go";

	public Param result = new Param();
}
