using Network;
using System;
using System.Collections.Generic;
using System.IO;

public class DeliveryTable : Singleton<DeliveryTable>, IDataTable
{
	public enum UIType
	{
		NONE,
		STORY,
		EVENT,
		DAILY,
		WEEKLY,
		HARD,
		SUB_EVENT
	}

	public enum DELIVERY_JUMPTYPE
	{
		UNDEFINED,
		TO_GACHA,
		TO_SMITH,
		TO_STATUS,
		TO_STORAGE,
		TO_POINT_SHOP,
		TO_WORLD_MAP
	}

	public enum CLEAR_INGAME
	{
		DEAFULT,
		VALID,
		INVALID
	}

	public class DeliveryData : IUIntKeyBinaryTableData
	{
		public class NeedData
		{
			public DELIVERY_CONDITION_TYPE conditionType;

			public uint enemyId;

			public uint mapId;

			public uint questId;

			public DELIVERY_RATE_TYPE rateType;

			public string needName;

			public XorUInt needNum = 0u;

			public XorUInt needId;

			public NeedData(DELIVERY_CONDITION_TYPE _conditionType, uint _enemyId, uint _mapId, uint _questId, DELIVERY_RATE_TYPE _rateType, string _name, uint _num, uint _id)
			{
				conditionType = _conditionType;
				enemyId = _enemyId;
				mapId = _mapId;
				questId = _questId;
				rateType = _rateType;
				needName = _name;
				needNum = _num;
				needId = _id;
			}

			public bool IsValid()
			{
				if (conditionType == DELIVERY_CONDITION_TYPE.NONE && enemyId == 0 && mapId == 0)
				{
					return false;
				}
				if ((uint)needNum == 0)
				{
					return false;
				}
				return true;
			}

			public bool IsNeedTarget(uint enemyId, uint mapId)
			{
				if (this.enemyId != 0)
				{
					if (this.enemyId != enemyId)
					{
						return false;
					}
					if (this.mapId != 0 && this.mapId != mapId)
					{
						return false;
					}
				}
				else
				{
					if (this.mapId == 0)
					{
						return false;
					}
					if (this.mapId != mapId)
					{
						return false;
					}
				}
				return true;
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				NeedData needData = obj as NeedData;
				if (needData == null)
				{
					return false;
				}
				return conditionType == needData.conditionType && enemyId == needData.enemyId && mapId == needData.mapId && questId == needData.questId && rateType == needData.rateType && needName == needData.needName && needNum.value == needData.needNum.value;
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override string ToString()
			{
				return "conditionType:" + conditionType + ", enemyId:" + enemyId + ", mapId:" + mapId + ", questId:" + questId + ", rateType:" + rateType + ", needName:" + needName + ", needNum:" + needNum;
			}
		}

		public const string NT = "id,locaitonNum,questID,name,type,subType,textType,clearIngameType,eventID,fieldMode,difficulty,npcID,npcComment,npcClearComment,clearEventID,clearEventTitle,jumpType,jumpMapID,targetPortalID_1,targetPortalID_2,targetPortalID_3,placeName,enemyName,appearQuestId,appearDeliveryId,conditionType_0,enemyId_0,mapId_0,questId_0,rateType_0,needName_0,needNum_0,needId_0,conditionType_1,enemyId_1,mapId_1,rateType_1,needName_1,needNum_1,needId_1,conditionType_2,enemyId_2,mapId_2,rateType_2,needName_2,needNum_2,needId_2,conditionType_3,enemyId_3,mapId_3,rateType_3,needName_3,needNum_3,needId_3,conditionType_4,enemyId_4,mapId_4,rateType_4,needName_4,needNum_4,needId_4,regionId,appearRegionId";

		private const string DEFEAT_CONDITION = "DEFEAT_QUEST";

		private const string DEFEAT_FIELD_CONDITION = "DEFEAT_FIELD";

		private const DELIVERY_RATE_TYPE DROP_DIFFICULTY_RARE = DELIVERY_RATE_TYPE.RATE_1500;

		private const DELIVERY_RATE_TYPE DROP_DIFFICULTY_SUPER_RARE = DELIVERY_RATE_TYPE.RATE_500;

		public uint id;

		public string locationNumber;

		public string deliveryNumber;

		public string name;

		public DELIVERY_TYPE type;

		public DELIVERY_SUB_TYPE subType;

		public DELIVERY_TYPE textType;

		public CLEAR_INGAME clearIngameType;

		public int eventID;

		public DIFFICULTY_MODE fieldMode;

		public DIFFICULTY_MODE difficulty;

		public bool eventFlag;

		public uint npcID;

		public string npcComment;

		public string npcClearComment;

		public uint clearEventID;

		public string clearEventTitle;

		public int jumpType;

		public int jumpMapID;

		public int[] targetPortalID = new int[3];

		public string placeName;

		public string enemyName;

		public uint appearQuestId;

		public uint appearDeliveryId;

		public NeedData[] needs;

		public int regionId;

		public int appearRegionId;

		public static bool cb(CSVReader csv_reader, DeliveryData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.locationNumber);
			csv_reader.Pop(ref data.deliveryNumber);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.type);
			csv_reader.PopEnum(ref data.subType, DELIVERY_SUB_TYPE.NONE);
			csv_reader.PopEnum(ref data.textType, DELIVERY_TYPE.ETC);
			csv_reader.PopEnum(ref data.clearIngameType, CLEAR_INGAME.DEAFULT);
			csv_reader.Pop(ref data.eventID);
			csv_reader.Pop(ref data.fieldMode);
			csv_reader.Pop(ref data.difficulty);
			csv_reader.Pop(ref data.npcID);
			csv_reader.Pop(ref data.npcComment);
			csv_reader.Pop(ref data.npcClearComment);
			csv_reader.Pop(ref data.clearEventID);
			csv_reader.Pop(ref data.clearEventTitle);
			csv_reader.Pop(ref data.jumpType);
			csv_reader.Pop(ref data.jumpMapID);
			csv_reader.Pop(ref data.targetPortalID[0]);
			csv_reader.Pop(ref data.targetPortalID[1]);
			csv_reader.Pop(ref data.targetPortalID[2]);
			csv_reader.Pop(ref data.placeName);
			csv_reader.Pop(ref data.enemyName);
			csv_reader.Pop(ref data.appearQuestId);
			csv_reader.Pop(ref data.appearDeliveryId);
			List<NeedData> list = new List<NeedData>();
			int i = 0;
			for (int num = 5; i < num; i++)
			{
				DELIVERY_CONDITION_TYPE value = DELIVERY_CONDITION_TYPE.NONE;
				uint value2 = 0u;
				uint value3 = 0u;
				uint value4 = 0u;
				DELIVERY_RATE_TYPE value5 = DELIVERY_RATE_TYPE.RATE_10000;
				string value6 = string.Empty;
				uint value7 = 0u;
				uint value8 = 0u;
				csv_reader.PopEnum(ref value, DELIVERY_CONDITION_TYPE.NONE);
				csv_reader.Pop(ref value2);
				csv_reader.Pop(ref value3);
				if (i == 0)
				{
					csv_reader.Pop(ref value4);
				}
				csv_reader.Pop(ref value5);
				csv_reader.Pop(ref value6);
				csv_reader.Pop(ref value7);
				csv_reader.Pop(ref value8);
				NeedData needData = new NeedData(value, value2, value3, value4, value5, value6, value7, value8);
				if (needData.IsValid())
				{
					list.Add(needData);
				}
			}
			data.needs = list.ToArray();
			if (string.IsNullOrEmpty(data.locationNumber))
			{
				data.locationNumber = (data.id / 100u % 1000u).ToString();
			}
			if (string.IsNullOrEmpty(data.deliveryNumber))
			{
				data.deliveryNumber = (data.id % 100u).ToString();
			}
			csv_reader.Pop(ref data.regionId);
			csv_reader.Pop(ref data.appearRegionId);
			return true;
		}

		public void LoadFromBinary(BinaryTableReader reader, ref uint key)
		{
			id = key;
			locationNumber = reader.ReadString(string.Empty);
			deliveryNumber = reader.ReadString(string.Empty);
			name = reader.ReadString(string.Empty);
			type = (DELIVERY_TYPE)reader.ReadUInt32(0u);
			subType = (DELIVERY_SUB_TYPE)reader.ReadUInt32(0u);
			textType = (DELIVERY_TYPE)reader.ReadUInt32(0u);
			eventID = reader.ReadInt32(0);
			fieldMode = (DIFFICULTY_MODE)reader.ReadUInt32(0u);
			difficulty = (DIFFICULTY_MODE)reader.ReadUInt32(0u);
			npcID = reader.ReadUInt32(0u);
			npcComment = reader.ReadString(string.Empty);
			npcClearComment = reader.ReadString(string.Empty);
			clearEventID = reader.ReadUInt32(0u);
			clearEventTitle = reader.ReadString(string.Empty);
			jumpType = reader.ReadInt32(0);
			jumpMapID = reader.ReadInt32(0);
			targetPortalID[0] = reader.ReadInt32(0);
			targetPortalID[1] = reader.ReadInt32(0);
			targetPortalID[2] = reader.ReadInt32(0);
			placeName = reader.ReadString(string.Empty);
			enemyName = reader.ReadString(string.Empty);
			appearQuestId = reader.ReadUInt32(0u);
			appearDeliveryId = reader.ReadUInt32(0u);
			List<NeedData> list = new List<NeedData>();
			int i = 0;
			for (int num = 5; i < num; i++)
			{
				DELIVERY_CONDITION_TYPE dELIVERY_CONDITION_TYPE = DELIVERY_CONDITION_TYPE.NONE;
				uint num2 = 0u;
				uint num3 = 0u;
				uint questId = 0u;
				DELIVERY_RATE_TYPE dELIVERY_RATE_TYPE = DELIVERY_RATE_TYPE.RATE_10000;
				string empty = string.Empty;
				uint num4 = 0u;
				uint num5 = 0u;
				dELIVERY_CONDITION_TYPE = (DELIVERY_CONDITION_TYPE)reader.ReadUInt32(0u);
				num2 = reader.ReadUInt32(0u);
				num3 = reader.ReadUInt32(0u);
				if (i == 0)
				{
					questId = reader.ReadUInt32(0u);
				}
				dELIVERY_RATE_TYPE = (DELIVERY_RATE_TYPE)reader.ReadUInt32(0u);
				empty = reader.ReadString(string.Empty);
				num4 = reader.ReadUInt32(0u);
				num5 = reader.ReadUInt32(0u);
				NeedData needData = new NeedData(dELIVERY_CONDITION_TYPE, num2, num3, questId, dELIVERY_RATE_TYPE, empty, num4, num5);
				if (needData.IsValid())
				{
					list.Add(needData);
				}
			}
			needs = list.ToArray();
			if (string.IsNullOrEmpty(locationNumber))
			{
				locationNumber = (id / 100u % 1000u).ToString();
			}
			if (string.IsNullOrEmpty(deliveryNumber))
			{
				deliveryNumber = (id % 100u).ToString();
			}
		}

		public void DumpBinary(BinaryWriter writer)
		{
			writer.Write(id);
			writer.Write(locationNumber);
			writer.Write(deliveryNumber);
			writer.Write(name);
			writer.Write((int)type);
			writer.Write((int)subType);
			writer.Write((int)textType);
			writer.Write(eventID);
			writer.Write((int)fieldMode);
			writer.Write((int)difficulty);
			writer.Write(npcID);
			writer.Write(npcComment);
			writer.Write(npcClearComment);
			writer.Write(clearEventID);
			writer.Write(clearEventTitle);
			writer.Write(jumpType);
			writer.Write(jumpMapID);
			for (int i = 0; i < targetPortalID.Length; i++)
			{
				writer.Write(targetPortalID[i]);
			}
			writer.Write(placeName);
			writer.Write(enemyName);
			writer.Write(appearQuestId);
			writer.Write(appearDeliveryId);
			for (int j = 0; j < needs.Length; j++)
			{
				NeedData needData = needs[j];
				writer.Write((int)needData.conditionType);
				writer.Write(needData.enemyId);
				writer.Write(needData.mapId);
				writer.Write(needData.questId);
				writer.Write((int)needData.rateType);
				writer.Write(needData.needName);
				writer.Write(needData.needNum);
			}
		}

		public bool IsStoryDelivery()
		{
			return appearQuestId != 0;
		}

		public bool IsEvent()
		{
			return type == DELIVERY_TYPE.EVENT || type == DELIVERY_TYPE.SUB_EVENT;
		}

		public bool IsClearDialogInGame()
		{
			if (clearIngameType == CLEAR_INGAME.VALID)
			{
				return false;
			}
			if (clearIngameType == CLEAR_INGAME.INVALID)
			{
				return true;
			}
			if (type == DELIVERY_TYPE.ONCE && fieldMode == DIFFICULTY_MODE.HARD)
			{
				return true;
			}
			return type == DELIVERY_TYPE.EVENT || type == DELIVERY_TYPE.STORY;
		}

		public bool IsInvalidClearIngame()
		{
			if (clearIngameType == CLEAR_INGAME.VALID)
			{
				return false;
			}
			if (clearIngameType == CLEAR_INGAME.INVALID)
			{
				return true;
			}
			return DeliveryManager.IsInvalidClearInGame(type, fieldMode);
		}

		public DELIVERY_CONDITION_TYPE GetConditionType(uint idx = 0)
		{
			if (needs == null)
			{
				return DELIVERY_CONDITION_TYPE.NONE;
			}
			if (idx >= needs.Length)
			{
				return DELIVERY_CONDITION_TYPE.NONE;
			}
			return needs[idx].conditionType;
		}

		public bool IsDefeatCondition(uint idx = 0)
		{
			if (needs == null)
			{
				return false;
			}
			if (idx >= needs.Length)
			{
				return false;
			}
			if (GetConditionType(idx) == DELIVERY_CONDITION_TYPE.NONE)
			{
				return true;
			}
			string text = needs[idx].conditionType.ToString();
			if (text.Contains("DEFEAT_QUEST"))
			{
				return true;
			}
			if (text.Contains("DEFEAT_FIELD"))
			{
				return true;
			}
			return false;
		}

		public uint GetEnemyID(uint idx = 0)
		{
			if (needs == null)
			{
				return 0u;
			}
			if (idx >= needs.Length)
			{
				return 0u;
			}
			return needs[idx].enemyId;
		}

		public List<uint> GetEnemyIdList()
		{
			if (needs == null)
			{
				return null;
			}
			List<uint> list = new List<uint>();
			int i = 0;
			for (int num = needs.Length; i < num; i++)
			{
				uint enemyId = needs[i].enemyId;
				if (enemyId >= 1)
				{
					list.Add(enemyId);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list;
		}

		public uint GetMapID(uint idx = 0)
		{
			if (needs == null)
			{
				return 0u;
			}
			if (idx >= needs.Length)
			{
				return 0u;
			}
			return needs[idx].mapId;
		}

		public List<uint> GetMapIdList()
		{
			if (needs == null)
			{
				return null;
			}
			List<uint> list = new List<uint>();
			int i = 0;
			for (int num = needs.Length; i < num; i++)
			{
				uint mapId = needs[i].mapId;
				if (mapId >= 1)
				{
					list.Add(mapId);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list;
		}

		public DELIVERY_RATE_TYPE GetRateType(uint idx = 0)
		{
			if (needs == null)
			{
				return DELIVERY_RATE_TYPE.RATE_10000;
			}
			if (idx >= needs.Length)
			{
				return DELIVERY_RATE_TYPE.RATE_10000;
			}
			return needs[idx].rateType;
		}

		public bool IsNeedTarget(uint idx, uint enemyId, uint mapId)
		{
			if (needs == null)
			{
				return false;
			}
			if (idx >= needs.Length)
			{
				return false;
			}
			return needs[idx].IsNeedTarget(enemyId, mapId);
		}

		public string GetNeedItemName(uint idx = 0)
		{
			if (needs == null)
			{
				return string.Empty;
			}
			if (idx >= needs.Length)
			{
				return string.Empty;
			}
			return needs[idx].needName;
		}

		public uint GetNeedItemNum(uint idx = 0)
		{
			if (needs == null)
			{
				return 0u;
			}
			if (idx >= needs.Length)
			{
				return 0u;
			}
			return needs[idx].needNum;
		}

		public uint GetAllNeedItemNum()
		{
			if (needs == null)
			{
				return 0u;
			}
			XorUInt xor = 0u;
			int i = 0;
			for (int num = needs.Length; i < num; i++)
			{
				xor = (uint)xor + (uint)needs[i].needNum;
			}
			return xor;
		}

		public UIType GetUIType()
		{
			if (type == DELIVERY_TYPE.EVENT)
			{
				return UIType.EVENT;
			}
			if (fieldMode != DIFFICULTY_MODE.HARD)
			{
				switch (type)
				{
				case DELIVERY_TYPE.STORY:
					return UIType.STORY;
				case DELIVERY_TYPE.DAILY:
				case DELIVERY_TYPE.MON:
				case DELIVERY_TYPE.TUE:
				case DELIVERY_TYPE.WED:
				case DELIVERY_TYPE.THU:
				case DELIVERY_TYPE.FRI:
				case DELIVERY_TYPE.SAT:
				case DELIVERY_TYPE.SUN:
				case DELIVERY_TYPE.DAY_OF_WEEK:
					return UIType.DAILY;
				case DELIVERY_TYPE.WEEKLY:
					return UIType.WEEKLY;
				case DELIVERY_TYPE.SUB_EVENT:
					return UIType.SUB_EVENT;
				default:
					return UIType.NONE;
				}
			}
			return UIType.HARD;
		}

		public UIType GetUITextType()
		{
			if (textType == DELIVERY_TYPE.ETC)
			{
				return UIType.NONE;
			}
			if (textType == DELIVERY_TYPE.DAILY)
			{
				return UIType.DAILY;
			}
			if (textType == DELIVERY_TYPE.WEEKLY)
			{
				return UIType.WEEKLY;
			}
			if (textType == DELIVERY_TYPE.EVENT)
			{
				return UIType.EVENT;
			}
			return UIType.NONE;
		}

		public DELIVERY_JUMPTYPE GetDeliveryJumpType()
		{
			DELIVERY_JUMPTYPE result = DELIVERY_JUMPTYPE.UNDEFINED;
			if (Enum.IsDefined(typeof(DELIVERY_JUMPTYPE), jumpType))
			{
				result = (DELIVERY_JUMPTYPE)jumpType;
			}
			return result;
		}

		public int DeliveryTypeIndex()
		{
			switch (GetUIType())
			{
			default:
				return 0;
			case UIType.EVENT:
			case UIType.DAILY:
			case UIType.WEEKLY:
				return 1;
			case UIType.STORY:
				return 2;
			case UIType.HARD:
				return 3;
			case UIType.SUB_EVENT:
				return 4;
			}
		}

		public DELIVERY_DROP_DIFFICULTY GetDeliveryDropRarity()
		{
			DELIVERY_RATE_TYPE rateType = GetRateType(0u);
			if (rateType >= DELIVERY_RATE_TYPE.RATE_500)
			{
				return DELIVERY_DROP_DIFFICULTY.SUPER_RARE;
			}
			if (rateType >= DELIVERY_RATE_TYPE.RATE_1500)
			{
				return DELIVERY_DROP_DIFFICULTY.RARE;
			}
			return DELIVERY_DROP_DIFFICULTY.NORMAL;
		}

		public QuestTable.QuestTableData GetQuestData()
		{
			if (needs.Length == 0)
			{
				return null;
			}
			uint questId = needs[0].questId;
			if (questId == 0)
			{
				return null;
			}
			return Singleton<QuestTable>.I.GetQuestData(questId);
		}

		public ArenaTable.ArenaData GetArenaData()
		{
			if (GetConditionType(0u) == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
			{
				return GetArenaDataWithRankUpDelivery();
			}
			return GetArenaDataWithArenaDelivery();
		}

		private ArenaTable.ArenaData GetArenaDataWithArenaDelivery()
		{
			if (needs.Length == 0)
			{
				return null;
			}
			XorUInt needId = needs[0].needId;
			if ((uint)needId == 0)
			{
				return null;
			}
			return Singleton<ArenaTable>.I.GetArenaData((int)needId);
		}

		private ArenaTable.ArenaData GetArenaDataWithRankUpDelivery()
		{
			if (needs.Length == 0)
			{
				return null;
			}
			XorUInt needId = needs[0].needId;
			if ((uint)needId == 0)
			{
				return null;
			}
			return Singleton<DeliveryTable>.I.GetDeliveryTableData(needId)?.GetArenaData();
		}

		public REGION_DIFFICULTY_TYPE GetRegionDifficultyType()
		{
			return Singleton<RegionTable>.I.GetData((uint)regionId)?.difficulty ?? REGION_DIFFICULTY_TYPE.NORMAL;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			DeliveryData deliveryData = obj as DeliveryData;
			if (deliveryData == null)
			{
				return false;
			}
			bool flag = id == deliveryData.id && locationNumber == deliveryData.locationNumber && deliveryNumber == deliveryData.deliveryNumber && name == deliveryData.name && type == deliveryData.type && textType == deliveryData.textType && eventID == deliveryData.eventID && fieldMode == deliveryData.fieldMode && difficulty == deliveryData.difficulty && eventFlag == deliveryData.eventFlag && npcID == deliveryData.npcID && npcComment == deliveryData.npcComment && npcClearComment == deliveryData.npcClearComment && clearEventID == deliveryData.clearEventID && clearEventTitle == deliveryData.clearEventTitle && jumpType == deliveryData.jumpType && jumpMapID == deliveryData.jumpMapID && targetPortalID[0] == deliveryData.targetPortalID[0] && targetPortalID[1] == deliveryData.targetPortalID[1] && targetPortalID[2] == deliveryData.targetPortalID[2] && placeName == deliveryData.placeName && enemyName == deliveryData.enemyName && appearQuestId == deliveryData.appearQuestId && appearDeliveryId == deliveryData.appearDeliveryId;
			if (needs.Length == deliveryData.needs.Length)
			{
				for (int i = 0; i < needs.Length; i++)
				{
					flag = (flag && needs[i].Equals(deliveryData.needs[i]));
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return "id:" + id + ", locationNumber:" + locationNumber + ", deliveryNumber:" + deliveryNumber + ", name:" + name + ", type:" + type + ", subType:" + subType + ", textType:" + textType + ", eventID:" + eventID + ", fieldMode:" + fieldMode + ", difficulty:" + difficulty + ", eventFlag:" + eventFlag + ", npcID:" + npcID + ", npcComment:" + npcComment + ", npcClearComment:" + npcClearComment + ", clearEventID:" + clearEventID + ", clearEventTitle:" + clearEventTitle + ", jumpType:" + jumpType + ", jumpMapID:" + jumpMapID + ", targetPortalID[0]" + targetPortalID[0] + ", targetPortalID[1]:" + targetPortalID[1] + ", targetPortalID[2]:" + targetPortalID[2] + ", placeName:" + placeName + ", enemyName:" + enemyName + ", appearQuestId:" + appearQuestId + ", appearDeliveryId:" + appearDeliveryId;
		}
	}

	private UIntKeyTable<DeliveryData> tableData;

	public static UIntKeyTable<DeliveryData> CreateTableCSV(string csv_text)
	{
		return TableUtility.CreateUIntKeyTable<DeliveryData>(csv_text, DeliveryData.cb, "id,locaitonNum,questID,name,type,subType,textType,clearIngameType,eventID,fieldMode,difficulty,npcID,npcComment,npcClearComment,clearEventID,clearEventTitle,jumpType,jumpMapID,targetPortalID_1,targetPortalID_2,targetPortalID_3,placeName,enemyName,appearQuestId,appearDeliveryId,conditionType_0,enemyId_0,mapId_0,questId_0,rateType_0,needName_0,needNum_0,needId_0,conditionType_1,enemyId_1,mapId_1,rateType_1,needName_1,needNum_1,needId_1,conditionType_2,enemyId_2,mapId_2,rateType_2,needName_2,needNum_2,needId_2,conditionType_3,enemyId_3,mapId_3,rateType_3,needName_3,needNum_3,needId_3,conditionType_4,enemyId_4,mapId_4,rateType_4,needName_4,needNum_4,needId_4,regionId,appearRegionId", null);
	}

	public void CreateTable(string csv_text)
	{
		tableData = CreateTableCSV(csv_text);
		tableData.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(tableData, csv_text, DeliveryData.cb, "id,locaitonNum,questID,name,type,subType,textType,clearIngameType,eventID,fieldMode,difficulty,npcID,npcComment,npcClearComment,clearEventID,clearEventTitle,jumpType,jumpMapID,targetPortalID_1,targetPortalID_2,targetPortalID_3,placeName,enemyName,appearQuestId,appearDeliveryId,conditionType_0,enemyId_0,mapId_0,questId_0,rateType_0,needName_0,needNum_0,needId_0,conditionType_1,enemyId_1,mapId_1,rateType_1,needName_1,needNum_1,needId_1,conditionType_2,enemyId_2,mapId_2,rateType_2,needName_2,needNum_2,needId_2,conditionType_3,enemyId_3,mapId_3,rateType_3,needName_3,needNum_3,needId_3,conditionType_4,enemyId_4,mapId_4,rateType_4,needName_4,needNum_4,needId_4,regionId,appearRegionId", null);
	}

	public static UIntKeyTable<DeliveryData> CreateTableBinary(byte[] bytes)
	{
		return TableUtility.CreateUIntKeyTableFromBinary<DeliveryData>(bytes);
	}

	public void CreateTable(byte[] bytes)
	{
		tableData = CreateTableBinary(bytes);
	}

	public DeliveryData GetDeliveryTableData(uint id)
	{
		if (tableData == null)
		{
			return null;
		}
		DeliveryData deliveryData = tableData.Get(id);
		if (deliveryData == null)
		{
			Log.TableError(this, id);
			deliveryData = new DeliveryData();
			deliveryData.name = Log.NON_DATA_NAME;
		}
		return deliveryData;
	}

	public DeliveryData[] GetDeliveryTableDataArray(List<Delivery> delivery_list)
	{
		if (tableData == null)
		{
			return null;
		}
		List<DeliveryData> list = new List<DeliveryData>();
		delivery_list.ForEach(delegate(Delivery data)
		{
			if (data.dId > 0)
			{
				DeliveryData deliveryTableData = GetDeliveryTableData((uint)data.dId);
				if (deliveryTableData != null)
				{
					list.Add(deliveryTableData);
				}
			}
		});
		if (list.Count == 0)
		{
			return null;
		}
		return list.ToArray();
	}

	public void AllDeliveryData(Action<DeliveryData> call_back)
	{
		if (tableData != null && call_back != null)
		{
			tableData.ForEach(delegate(DeliveryData data)
			{
				call_back(data);
			});
		}
	}

	public int GetSortPriority(DELIVERY_TYPE type)
	{
		switch (type)
		{
		default:
			return 5;
		case DELIVERY_TYPE.WEEKLY:
			return 4;
		case DELIVERY_TYPE.ONCE:
			return 3;
		case DELIVERY_TYPE.DAILY:
		case DELIVERY_TYPE.MON:
		case DELIVERY_TYPE.TUE:
		case DELIVERY_TYPE.WED:
		case DELIVERY_TYPE.THU:
		case DELIVERY_TYPE.FRI:
		case DELIVERY_TYPE.SAT:
		case DELIVERY_TYPE.SUN:
		case DELIVERY_TYPE.SUB_EVENT:
		case DELIVERY_TYPE.DAY_OF_WEEK:
			return 2;
		case DELIVERY_TYPE.STORY:
			return 1;
		}
	}
}
