using Network;
using System.Collections.Generic;

public class GuildModel : BaseModel
{
	public class Param
	{
		public Guild guildInfo;

		public ChatChannelInfo chat;
	}

	public class Guild
	{
		public string name;

		public int level;

		public int location;

		public int exp;

		public int clanId;

		public int clanMasterId;

		public int currentMem;

		public int memCap;

		public int privacy;

		public string tag;

		public int[] emblem;

		public string inviteMessage;

		public InviteFriendInfo inviteFriendInfo;
	}

	public class RequestCreateVerify
	{
		public static string verifyPath = "clan/ClanCreationVerify.go";

		public int clanId;

		public string token;

		public string tag;

		public string name;

		public string description;
	}

	public class RequestCreate
	{
		public static string path = "clan/ClanCreate.go";

		public string token;

		public string name;

		public string description;

		public string tag;

		public List<int> emblem;

		public int min_level;

		public int location;

		public int privacy;

		public List<string> inviteList;
	}

	public class RequestDelete
	{
		public static string path = "clan/AtvDisband.go";

		public string token;
	}

	public class RequestJoin
	{
		public static string path = "clan/AtvRequestJoin.go";

		public string token;

		public int clanId;

		public int recommendId;
	}

	public class RequestInvite
	{
		public static string path = "clan/AtvInvite.go";

		public string token;

		public List<string> inviteList;
	}

	public class RequestLeave
	{
		public static string path = "clan/AtvLeave.go";

		public string token;
	}

	public class RequestAcceptOrDenyJoin
	{
		public static string path = "clan/AtvLeave.go";

		public string token;

		public int requestId;

		public int decision;
	}

	public class InviteFriendInfo
	{
		public string inviteMessage;

		public string linkUrl;
	}

	public class GuildChangeSetting
	{
		public static string path = "clan/ClanSettings.go";

		public string name;

		public string description;

		public string tag;

		public int min_level;

		public int[] emblem;

		public int privacy;

		public int location;
	}

	public class RequestKick
	{
		public static string path = "clan/AtvKick.go";

		public string token;

		public int forUserId;

		public string reason;
	}

	public class RequestAdminJoin
	{
		public static string path = "clan/AtvAdminHandleRequest.go";

		public string token;

		public int requestId;

		public int decision;
	}

	public Param result;
}
