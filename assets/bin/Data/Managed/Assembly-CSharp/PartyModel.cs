using Network;
using System.Collections.Generic;

public class PartyModel : BaseModel
{
	public class Param
	{
		public Party party;

		public List<FollowPartyMember> friend = new List<FollowPartyMember>();

		public PartyServer partyServer;

		public InviteFriendInfo inviteFriendInfo;

		public RandomMatchingInfo randomMatchingInfo;

		public List<IsEquipPartyMember> isEquipList = new List<IsEquipPartyMember>();

		public int repeatPartyStatus;

		public int repeatStatus;

		public bool repeatFeatureEnable;
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

	public class Party
	{
		public string id;

		public int ownerUserId;

		public int status;

		public int expiredAt;

		public string partyNumber;

		public QuestInfo quest;

		public List<SlotInfo> slotInfos;

		public int iconBit;

		public bool isRepeat;
	}

	public class SlotInfo
	{
		public int status;

		public CharaInfo userInfo;
	}

	public class QuestInfo
	{
		public int questId;

		public int mode;

		public int crystalNum;

		public ExploreInfo explore;

		public RushInfo rush;

		public EventAppear appear;

		public EventPaying paying;
	}

	public class ExploreInfo
	{
		public List<int> mapIds;

		public int isRare;

		public int bossMoveTime;

		public int mainQuestId;
	}

	public class RushInfo
	{
		public class WaveInfo
		{
			public int wave;

			public int questId;

			public int rescueResetNum;

			public int standupHpPer;
		}

		public int continueFlag;

		public List<WaveInfo> waves;
	}

	public class ArenaInfo
	{
		public class WaveInfo
		{
			public int wave;

			public int questId;
		}

		public ArenaTable.ArenaData arenaData;

		public List<WaveInfo> waves;

		public ArenaInfo()
		{
			waves = new List<WaveInfo>();
		}
	}

	public class PartyServer
	{
		public string wsHost;

		public List<int> wsPorts;

		public string token;

		public string signature;
	}

	public class RequestRandomMatching
	{
		public static string path = "ajax/party/randommatching";

		public int qid;

		public string token;

		public int retryCount;

		public int ce;
	}

	public class RequestSearchRandomMatching
	{
		public static string path = "ajax/party/searchrandommatching";

		public string token;

		public int order;

		public int rarityBit;

		public int elementBit;

		public int enemyLevelMin;

		public int enemyLevelMax;

		public int enemySpecies;

		public int questTypeBit;
	}

	public class RequestSearchRushRandomMatching
	{
		public static string path = "ajax/party/towerrandommatching";

		public string token;

		public int floorMinQuestId;

		public int floorMaxQuestId;
	}

	public class RequestMatching
	{
		public static string path = "ajax/party/matching";

		public int qid;

		public string token;
	}

	public class RequestCreate
	{
		public static string path = "ajax/party/create";

		public int qid;

		public int mode;

		public string token;

		public int isLock;

		public int lv = 1;

		public int power;

		public int cs;

		public int ce;
	}

	public class RequestApply
	{
		public static string path = "ajax/party/apply";

		public string token;

		public string partyNumber;

		public int qid;
	}

	public class RequestEntry
	{
		public static string path = "ajax/party/entry";

		public string token;

		public string id;

		public int isLoungeBoard;
	}

	public class RequestInfo
	{
		public static string path = "ajax/party/info";

		public string id;
	}

	public class RequestIsEquip
	{
		public static string path = "ajax/party/isequip";

		public string id;

		public bool isEquip;
	}

	public class RequestReady
	{
		public static string path = "ajax/party/ready";

		public string id;

		public int enable;
	}

	public class RequestEdit
	{
		public static string path = "ajax/party/edit";

		public string id;

		public int isLock;

		public int lv = 1;

		public int power;
	}

	public Param result = new Param();
}
