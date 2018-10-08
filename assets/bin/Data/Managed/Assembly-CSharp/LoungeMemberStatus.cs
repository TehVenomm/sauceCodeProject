using System;

public class LoungeMemberStatus
{
	public enum MEMBER_STATUS
	{
		LOUNGE,
		QUEST_READY,
		QUEST,
		FIELD,
		ARENA
	}

	public int userId
	{
		get;
		private set;
	}

	public string partyId
	{
		get;
		private set;
	}

	public int questId
	{
		get;
		private set;
	}

	public string fieldId
	{
		get;
		private set;
	}

	public int fieldMapId
	{
		get;
		private set;
	}

	public DateTime lastExecTime
	{
		get;
		private set;
	}

	public bool isHost
	{
		get;
		private set;
	}

	public int arenaId
	{
		get;
		private set;
	}

	public LoungeMemberStatus(int user_id)
	{
		userId = user_id;
		UpdateLastExecTime(TimeManager.GetNow().ToUniversalTime());
	}

	public LoungeMemberStatus(LoungeModel.SlotInfo info)
		: this(info.userInfo.userId)
	{
	}

	public LoungeMemberStatus(Party_Model_RegisterACK.UserInfo ack)
	{
		userId = ack.userId;
		partyId = ack.partyId;
		questId = ack.questId;
		fieldId = ack.fieldId;
		fieldMapId = ack.fieldMapId;
		UpdateLastExecTime(ack.lastExecTime);
	}

	public void ToQuest(string party, int quest, bool host)
	{
		partyId = party;
		questId = quest;
		isHost = host;
	}

	public void ToField(string field, int fieldMap, string party, int quest, bool host)
	{
		fieldId = field;
		fieldMapId = fieldMap;
		ToQuest(party, quest, host);
	}

	public void ToLounge()
	{
		partyId = null;
		questId = 0;
		fieldId = null;
		fieldMapId = 0;
		isHost = false;
		arenaId = 0;
		UpdateLastExecTime(TimeManager.GetNow().ToUniversalTime());
	}

	public void ToArena(int arenaId)
	{
		partyId = null;
		questId = 0;
		fieldId = null;
		fieldMapId = 0;
		isHost = false;
		this.arenaId = arenaId;
	}

	public MEMBER_STATUS GetStatus()
	{
		if (questId > 0)
		{
			if (fieldMapId > 0)
			{
				return MEMBER_STATUS.QUEST;
			}
			return MEMBER_STATUS.QUEST_READY;
		}
		if (arenaId > 0)
		{
			return MEMBER_STATUS.ARENA;
		}
		if (fieldMapId > 0)
		{
			return MEMBER_STATUS.FIELD;
		}
		return MEMBER_STATUS.LOUNGE;
	}

	public void SetCopy(LoungeMemberStatus copyData)
	{
		userId = copyData.userId;
		partyId = copyData.partyId;
		questId = copyData.questId;
		fieldId = copyData.fieldId;
		fieldMapId = copyData.fieldMapId;
		isHost = copyData.isHost;
		UpdateLastExecTime(copyData.lastExecTime);
	}

	public void UpdateLastExecTime(DateTime time)
	{
		lastExecTime = time;
	}
}
