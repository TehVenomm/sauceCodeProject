using Network;
using System;
using System.Collections.Generic;

public class FieldModel : BaseModel
{
	public class Param
	{
		public Field field;

		public List<int> gather;

		public List<GatherGrowthInfo> growth;

		public string raidBossHp;

		public string noticeText = "";

		public int mapFlag;
	}

	public class Field
	{
		public string id;

		public int mapId;

		public List<SlotInfo> slotInfos;

		public string wsHost;

		public List<int> wsPorts;

		public int expiredAt;

		public string createdAt;

		public int enableStandby;

		public bool TryGetCreatedAt(out DateTime createdAt)
		{
			return DateTime.TryParse(this.createdAt, out createdAt);
		}
	}

	public class SlotInfo
	{
		public int userId;

		public string token;

		public FriendCharaInfo userInfo;
	}

	public class RequestMatching
	{
		public static string path = "ajax/field/matching";

		public int portalId;

		public string token;

		public int dId;

		public int prevId;

		public int toUserId;
	}

	public class RequestQuest
	{
		public static string path = "ajax/field/quest";

		public string token;

		public int qid;
	}

	public class RequestCreate
	{
		public static string path = "ajax/field/create";

		public string partyId;

		public string token;
	}

	public class RequestEnter
	{
		public static string path = "ajax/field/enter";

		public string partyId;

		public string token;
	}

	public class RequestInfo
	{
		public static string path = "ajax/field/info";
	}

	public Param result = new Param();
}
