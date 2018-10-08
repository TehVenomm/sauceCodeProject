using Network;
using System.Collections.Generic;

public class LoungeModel : BaseModel
{
	public class Param
	{
		public Lounge lounge;

		public List<FollowLoungeMember> friend = new List<FollowLoungeMember>();

		public LoungeServer loungeServer;

		public InviteFriendInfo inviteFriendInfo;

		public RandomMatchingInfo randomMatchingInfo;

		public List<int> firstMetUserIds = new List<int>();
	}

	public class RandomMatchingInfo
	{
		public int maxRetryCount;

		public float waitTime;

		public bool usedSearchRandomMatching;
	}

	public class InviteFriendInfo
	{
		public string inviteMessage;

		public string linkUrl;
	}

	public class Lounge
	{
		public string id;

		public string token;

		public int ownerUserId;

		public int status;

		public int expiredAt;

		public string loungeNumber;

		public List<SlotInfo> slotInfos;

		public int num;

		public int stampId;

		public int label;

		public int isLock;

		public int minLv = 1;

		public int maxLv = -1;

		public string name;
	}

	public class SlotInfo
	{
		public int status;

		public int questId;

		public int fieldId;

		public CharaInfo userInfo;
	}

	public class LoungeServer
	{
		public string wsHost;

		public List<int> wsPorts;

		public string token;

		public string signature;
	}

	public class RequestSearchRandomMatching
	{
		public static string path = "ajax/lounge/searchrandommatching";

		public string token;

		public int order;

		public int label;

		public string name;
	}

	public class RequestCreate
	{
		public static string path = "ajax/lounge/create";

		public string token;

		public int num;

		public int stampId;

		public int label;

		public int isLock;

		public int minLv = 1;

		public int maxLv = -1;

		public string name;
	}

	public class RequestApply
	{
		public static string path = "ajax/lounge/apply";

		public string token;

		public string loungeNumber;
	}

	public class RequestEntry
	{
		public static string path = "ajax/lounge/entry";

		public string token;

		public string id;
	}

	public class RequestInfo
	{
		public static string path = "ajax/lounge/info";

		public string id;
	}

	public class RequestEdit
	{
		public static string path = "ajax/lounge/edit";

		public string id;

		public int num;

		public int stampId;

		public int label;

		public int isLock;

		public int minLv = 1;

		public int maxLv = -1;

		public string name;
	}

	public class RequestKick
	{
		public static string path = "ajax/lounge/kick";

		public string id;

		public int userId;
	}

	public class RequestForceKick
	{
		public static string path = "ajax/lounge/forcekick";

		public string id;

		public int userId;
	}

	public Param result = new Param();
}
