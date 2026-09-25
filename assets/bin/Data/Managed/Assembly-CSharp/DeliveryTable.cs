// Decompiled with JetBrains decompiler
// Type: DeliveryTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
public class DeliveryTable : Singleton<DeliveryTable>, IDataTable
{
  private UIntKeyTable<DeliveryTable.DeliveryData> tableData;

  public static UIntKeyTable<DeliveryTable.DeliveryData> CreateTableCSV(string csv_text)
  {
    return TableUtility.CreateUIntKeyTable<DeliveryTable.DeliveryData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<DeliveryTable.DeliveryData>(DeliveryTable.DeliveryData.cb), "id,locaitonNum,questID,name,type,subType,textType,clearIngameType,displayOrder,eventID,fieldMode,difficulty,npcID,npcComment,npcClearComment,readScriptId,clearEventID,clearEventTitle,jumpType,jumpMapID,targetPortalID_1,targetPortalID_2,targetPortalID_3,placeName,enemyName,appearQuestId,appearDeliveryId,conditionType_0,enemyId_0,mapId_0,questId_0,rateType_0,needName_0,needNum_0,needId_0,conditionType_1,enemyId_1,mapId_1,rateType_1,needName_1,needNum_1,needId_1,conditionType_2,enemyId_2,mapId_2,rateType_2,needName_2,needNum_2,needId_2,conditionType_3,enemyId_3,mapId_3,rateType_3,needName_3,needNum_3,needId_3,conditionType_4,enemyId_4,mapId_4,rateType_4,needName_4,needNum_4,needId_4,regionId,tipsIds");
  }

  public void CreateTable(string csv_text)
  {
    this.tableData = DeliveryTable.CreateTableCSV(csv_text);
    this.tableData.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<DeliveryTable.DeliveryData>(this.tableData, csv_text, new TableUtility.CallBackUIntKeyReadCSV<DeliveryTable.DeliveryData>(DeliveryTable.DeliveryData.cb), "id,locaitonNum,questID,name,type,subType,textType,clearIngameType,displayOrder,eventID,fieldMode,difficulty,npcID,npcComment,npcClearComment,readScriptId,clearEventID,clearEventTitle,jumpType,jumpMapID,targetPortalID_1,targetPortalID_2,targetPortalID_3,placeName,enemyName,appearQuestId,appearDeliveryId,conditionType_0,enemyId_0,mapId_0,questId_0,rateType_0,needName_0,needNum_0,needId_0,conditionType_1,enemyId_1,mapId_1,rateType_1,needName_1,needNum_1,needId_1,conditionType_2,enemyId_2,mapId_2,rateType_2,needName_2,needNum_2,needId_2,conditionType_3,enemyId_3,mapId_3,rateType_3,needName_3,needNum_3,needId_3,conditionType_4,enemyId_4,mapId_4,rateType_4,needName_4,needNum_4,needId_4,regionId,tipsIds");
  }

  public static UIntKeyTable<DeliveryTable.DeliveryData> CreateTableBinary(byte[] bytes)
  {
    return TableUtility.CreateUIntKeyTableFromBinary<DeliveryTable.DeliveryData>(bytes);
  }

  public void CreateTable(byte[] bytes) => this.tableData = DeliveryTable.CreateTableBinary(bytes);

  public DeliveryTable.DeliveryData GetDeliveryTableData(uint id)
  {
    if (this.tableData == null)
      return (DeliveryTable.DeliveryData) null;
    DeliveryTable.DeliveryData deliveryTableData = this.tableData.Get(id);
    if (deliveryTableData == null)
    {
      Log.TableError((object) this, id);
      deliveryTableData = new DeliveryTable.DeliveryData();
      deliveryTableData.name = Log.NON_DATA_NAME;
    }
    return deliveryTableData;
  }

  public DeliveryTable.DeliveryData[] GetDeliveryTableDataArray(List<Delivery> delivery_list)
  {
    if (this.tableData == null)
      return (DeliveryTable.DeliveryData[]) null;
    List<DeliveryTable.DeliveryData> list = new List<DeliveryTable.DeliveryData>();
    delivery_list.ForEach((Action<Delivery>) (data =>
    {
      if (data.dId <= 0)
        return;
      DeliveryTable.DeliveryData deliveryTableData = this.GetDeliveryTableData((uint) data.dId);
      if (deliveryTableData == null)
        return;
      list.Add(deliveryTableData);
    }));
    return list.Count == 0 ? (DeliveryTable.DeliveryData[]) null : list.ToArray();
  }

  public List<int> GetTipsList(uint questId)
  {
    List<int> tipsList = new List<int>();
    DeliveryTable.DeliveryData tableDataFromQuestId = this.GetDeliveryTableDataFromQuestId(questId);
    if (tableDataFromQuestId != null)
      tipsList.AddRange(tableDataFromQuestId.tipsIdList.Where<int>((Func<int, bool>) (num => num > 0)));
    return tipsList;
  }

  public void AllDeliveryData(Action<DeliveryTable.DeliveryData> call_back)
  {
    if (this.tableData == null || call_back == null)
      return;
    this.tableData.ForEach((Action<DeliveryTable.DeliveryData>) (data => call_back(data)));
  }

  public void AllDeliveryDataAsc(Action<DeliveryTable.DeliveryData> call_back)
  {
    if (this.tableData == null || call_back == null)
      return;
    this.tableData.ForEachAsc((Action<DeliveryTable.DeliveryData>) (data => call_back(data)));
  }

  public void AllDeliveryDataDesc(Action<DeliveryTable.DeliveryData> call_back)
  {
    if (this.tableData == null || call_back == null)
      return;
    this.tableData.ForEachDesc((Action<DeliveryTable.DeliveryData>) (data => call_back(data)));
  }

  public DeliveryTable.DeliveryData GetDeliveryTableDataFromQuestId(uint questId)
  {
    return this.tableData == null ? (DeliveryTable.DeliveryData) null : this.tableData.Find((Predicate<DeliveryTable.DeliveryData>) (d => d.needs.Length != 0 && (int) d.needs[0].questId == (int) questId));
  }

  public int GetSortPriority(DELIVERY_TYPE type)
  {
    switch (type)
    {
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
      case DELIVERY_TYPE.ONCE:
        return 3;
      case DELIVERY_TYPE.WEEKLY:
        return 4;
      case DELIVERY_TYPE.EVENT:
      case DELIVERY_TYPE.FRI | DELIVERY_TYPE.STORY:
      case DELIVERY_TYPE.SAT | DELIVERY_TYPE.STORY:
      case DELIVERY_TYPE.SUN | DELIVERY_TYPE.STORY:
      case (DELIVERY_TYPE) 16 /*0x10*/:
      case (DELIVERY_TYPE) 17:
      case (DELIVERY_TYPE) 18:
      case (DELIVERY_TYPE) 19:
      case DELIVERY_TYPE.ETC:
        return 5;
      default:
        goto case DELIVERY_TYPE.EVENT;
    }
  }

  public enum UIType
  {
    NONE,
    STORY,
    EVENT,
    DAILY,
    WEEKLY,
    HARD,
    SUB_EVENT,
  }

  public enum DELIVERY_JUMPTYPE
  {
    UNDEFINED,
    TO_GACHA,
    TO_SMITH,
    TO_STATUS,
    TO_STORAGE,
    TO_POINT_SHOP,
    TO_WORLD_MAP,
  }

  public enum CLEAR_INGAME
  {
    DEAFULT,
    VALID,
    INVALID,
  }

  public class DeliveryData : IUIntKeyBinaryTableData
  {
    public uint id;
    public string locationNumber;
    public string deliveryNumber;
    public string name;
    public DELIVERY_TYPE type;
    public DELIVERY_SUB_TYPE subType;
    public DELIVERY_TYPE textType;
    public DeliveryTable.CLEAR_INGAME clearIngameType;
    public int displayOrder;
    public int eventID;
    public DIFFICULTY_MODE fieldMode;
    public DIFFICULTY_MODE difficulty;
    public bool eventFlag;
    public uint npcID;
    public string npcComment;
    public string npcClearComment;
    public uint readScriptId;
    public uint clearEventID;
    public string clearEventTitle;
    public int jumpType;
    public int jumpMapID;
    public int[] targetPortalID = new int[3];
    public string placeName;
    public string enemyName;
    public uint appearQuestId;
    public uint appearDeliveryId;
    public DeliveryTable.DeliveryData.NeedData[] needs;
    public int regionId;
    public List<int> tipsIdList = new List<int>();
    public int appearRegionId;
    public const string NT = "id,locaitonNum,questID,name,type,subType,textType,clearIngameType,displayOrder,eventID,fieldMode,difficulty,npcID,npcComment,npcClearComment,readScriptId,clearEventID,clearEventTitle,jumpType,jumpMapID,targetPortalID_1,targetPortalID_2,targetPortalID_3,placeName,enemyName,appearQuestId,appearDeliveryId,conditionType_0,enemyId_0,mapId_0,questId_0,rateType_0,needName_0,needNum_0,needId_0,conditionType_1,enemyId_1,mapId_1,rateType_1,needName_1,needNum_1,needId_1,conditionType_2,enemyId_2,mapId_2,rateType_2,needName_2,needNum_2,needId_2,conditionType_3,enemyId_3,mapId_3,rateType_3,needName_3,needNum_3,needId_3,conditionType_4,enemyId_4,mapId_4,rateType_4,needName_4,needNum_4,needId_4,regionId,tipsIds";
    private const string DEFEAT_CONDITION = "DEFEAT_QUEST";
    private const string DEFEAT_FIELD_CONDITION = "DEFEAT_FIELD";
    private const DELIVERY_RATE_TYPE DROP_DIFFICULTY_RARE = DELIVERY_RATE_TYPE.RATE_1500;
    private const DELIVERY_RATE_TYPE DROP_DIFFICULTY_SUPER_RARE = DELIVERY_RATE_TYPE.RATE_500;

    public static bool cb(CSVReader csv_reader, DeliveryTable.DeliveryData data, ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.locationNumber);
      csv_reader.Pop(ref data.deliveryNumber);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop<DELIVERY_TYPE>(ref data.type);
      csv_reader.PopEnum<DELIVERY_SUB_TYPE>(ref data.subType, DELIVERY_SUB_TYPE.NONE);
      csv_reader.PopEnum<DELIVERY_TYPE>(ref data.textType, DELIVERY_TYPE.ETC);
      csv_reader.PopEnum<DeliveryTable.CLEAR_INGAME>(ref data.clearIngameType, DeliveryTable.CLEAR_INGAME.DEAFULT);
      csv_reader.Pop(ref data.displayOrder);
      csv_reader.Pop(ref data.eventID);
      csv_reader.Pop<DIFFICULTY_MODE>(ref data.fieldMode);
      csv_reader.Pop<DIFFICULTY_MODE>(ref data.difficulty);
      csv_reader.Pop(ref data.npcID);
      csv_reader.Pop(ref data.npcComment);
      csv_reader.Pop(ref data.npcClearComment);
      csv_reader.Pop(ref data.readScriptId);
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
      List<DeliveryTable.DeliveryData.NeedData> needDataList = new List<DeliveryTable.DeliveryData.NeedData>();
      int num1 = 0;
      for (int index = 5; num1 < index; ++num1)
      {
        DELIVERY_CONDITION_TYPE _conditionType = DELIVERY_CONDITION_TYPE.NONE;
        uint _enemyId = 0;
        uint _mapId = 0;
        uint _questId = 0;
        DELIVERY_RATE_TYPE _rateType = DELIVERY_RATE_TYPE.RATE_10000;
        string _name = "";
        uint _num = 0;
        uint _id = 0;
        csv_reader.PopEnum<DELIVERY_CONDITION_TYPE>(ref _conditionType, DELIVERY_CONDITION_TYPE.NONE);
        csv_reader.Pop(ref _enemyId);
        csv_reader.Pop(ref _mapId);
        if (num1 == 0)
          csv_reader.Pop(ref _questId);
        csv_reader.Pop<DELIVERY_RATE_TYPE>(ref _rateType);
        csv_reader.Pop(ref _name);
        csv_reader.Pop(ref _num);
        csv_reader.Pop(ref _id);
        DeliveryTable.DeliveryData.NeedData needData = new DeliveryTable.DeliveryData.NeedData(_conditionType, _enemyId, _mapId, _questId, _rateType, _name, _num, _id);
        if (needData.IsValid())
          needDataList.Add(needData);
      }
      data.needs = needDataList.ToArray();
      uint num2;
      if (string.IsNullOrEmpty(data.locationNumber))
      {
        DeliveryTable.DeliveryData deliveryData = data;
        num2 = data.id / 100U % 1000U;
        string str = num2.ToString();
        deliveryData.locationNumber = str;
      }
      if (string.IsNullOrEmpty(data.deliveryNumber))
      {
        DeliveryTable.DeliveryData deliveryData = data;
        num2 = data.id % 100U;
        string str = num2.ToString();
        deliveryData.deliveryNumber = str;
      }
      csv_reader.Pop(ref data.regionId);
      string buff = "";
      csv_reader.Pop(ref buff);
      data.tipsIdList = new List<int>();
      int[] stringToIntArray = TableUtility.ParseStringToIntArray(buff);
      if (stringToIntArray != null)
        data.tipsIdList.AddRange((IEnumerable<int>) stringToIntArray);
      csv_reader.Pop(ref data.appearRegionId);
      return true;
    }

    public void LoadFromBinary(BinaryTableReader reader, ref uint key)
    {
      this.id = key;
      this.locationNumber = reader.ReadString();
      this.deliveryNumber = reader.ReadString();
      this.name = reader.ReadString();
      this.type = (DELIVERY_TYPE) reader.ReadUInt32();
      this.subType = (DELIVERY_SUB_TYPE) reader.ReadUInt32();
      this.textType = (DELIVERY_TYPE) reader.ReadUInt32();
      this.displayOrder = reader.ReadInt32();
      this.eventID = reader.ReadInt32();
      this.fieldMode = (DIFFICULTY_MODE) reader.ReadUInt32();
      this.difficulty = (DIFFICULTY_MODE) reader.ReadUInt32();
      this.npcID = reader.ReadUInt32();
      this.npcComment = reader.ReadString();
      this.npcClearComment = reader.ReadString();
      this.readScriptId = reader.ReadUInt32();
      this.clearEventID = reader.ReadUInt32();
      this.clearEventTitle = reader.ReadString();
      this.jumpType = reader.ReadInt32();
      this.jumpMapID = reader.ReadInt32();
      this.targetPortalID[0] = reader.ReadInt32();
      this.targetPortalID[1] = reader.ReadInt32();
      this.targetPortalID[2] = reader.ReadInt32();
      this.placeName = reader.ReadString();
      this.enemyName = reader.ReadString();
      this.appearQuestId = reader.ReadUInt32();
      this.appearDeliveryId = reader.ReadUInt32();
      List<DeliveryTable.DeliveryData.NeedData> needDataList = new List<DeliveryTable.DeliveryData.NeedData>();
      int num1 = 0;
      for (int index = 5; num1 < index; ++num1)
      {
        uint num2 = 0;
        int _conditionType = (int) reader.ReadUInt32();
        uint num3 = reader.ReadUInt32();
        uint num4 = reader.ReadUInt32();
        if (num1 == 0)
          num2 = reader.ReadUInt32();
        DELIVERY_RATE_TYPE deliveryRateType = (DELIVERY_RATE_TYPE) reader.ReadUInt32();
        string str = reader.ReadString();
        uint num5 = reader.ReadUInt32();
        uint num6 = reader.ReadUInt32();
        int _enemyId = (int) num3;
        int _mapId = (int) num4;
        int _questId = (int) num2;
        int _rateType = (int) deliveryRateType;
        string _name = str;
        int _num = (int) num5;
        int _id = (int) num6;
        DeliveryTable.DeliveryData.NeedData needData = new DeliveryTable.DeliveryData.NeedData((DELIVERY_CONDITION_TYPE) _conditionType, (uint) _enemyId, (uint) _mapId, (uint) _questId, (DELIVERY_RATE_TYPE) _rateType, _name, (uint) _num, (uint) _id);
        if (needData.IsValid())
          needDataList.Add(needData);
      }
      this.needs = needDataList.ToArray();
      uint num7;
      if (string.IsNullOrEmpty(this.locationNumber))
      {
        num7 = this.id / 100U % 1000U;
        this.locationNumber = num7.ToString();
      }
      if (!string.IsNullOrEmpty(this.deliveryNumber))
        return;
      num7 = this.id % 100U;
      this.deliveryNumber = num7.ToString();
    }

    public void DumpBinary(BinaryWriter writer)
    {
      writer.Write(this.id);
      writer.Write(this.locationNumber);
      writer.Write(this.deliveryNumber);
      writer.Write(this.name);
      writer.Write((int) this.type);
      writer.Write((int) this.subType);
      writer.Write((int) this.textType);
      writer.Write(this.displayOrder);
      writer.Write(this.eventID);
      writer.Write((int) this.fieldMode);
      writer.Write((int) this.difficulty);
      writer.Write(this.npcID);
      writer.Write(this.npcComment);
      writer.Write(this.npcClearComment);
      writer.Write(this.readScriptId);
      writer.Write(this.clearEventID);
      writer.Write(this.clearEventTitle);
      writer.Write(this.jumpType);
      writer.Write(this.jumpMapID);
      for (int index = 0; index < this.targetPortalID.Length; ++index)
        writer.Write(this.targetPortalID[index]);
      writer.Write(this.placeName);
      writer.Write(this.enemyName);
      writer.Write(this.appearQuestId);
      writer.Write(this.appearDeliveryId);
      for (int index = 0; index < this.needs.Length; ++index)
      {
        DeliveryTable.DeliveryData.NeedData need = this.needs[index];
        writer.Write((int) need.conditionType);
        writer.Write(need.enemyId);
        writer.Write(need.mapId);
        writer.Write(need.questId);
        writer.Write((int) need.rateType);
        writer.Write(need.needName);
        writer.Write((uint) need.needNum);
      }
    }

    public bool IsStoryDelivery() => this.appearQuestId > 0U;

    public bool IsEvent()
    {
      return this.type == DELIVERY_TYPE.EVENT || this.type == DELIVERY_TYPE.SUB_EVENT;
    }

    public bool IsClearDialogInGame()
    {
      if (this.clearIngameType == DeliveryTable.CLEAR_INGAME.VALID)
        return false;
      return this.clearIngameType == DeliveryTable.CLEAR_INGAME.INVALID || this.type == DELIVERY_TYPE.ONCE && this.fieldMode == DIFFICULTY_MODE.HARD || this.type == DELIVERY_TYPE.EVENT || this.type == DELIVERY_TYPE.STORY;
    }

    public bool IsInvalidClearIngame()
    {
      if (this.clearIngameType == DeliveryTable.CLEAR_INGAME.VALID)
        return false;
      return this.clearIngameType == DeliveryTable.CLEAR_INGAME.INVALID || DeliveryManager.IsInvalidClearInGame(this.type, this.fieldMode);
    }

    public DELIVERY_CONDITION_TYPE GetConditionType(uint idx = 0)
    {
      return this.needs == null || (long) idx >= (long) this.needs.Length ? DELIVERY_CONDITION_TYPE.NONE : this.needs[(int) idx].conditionType;
    }

    public bool IsDefeatCondition(uint idx = 0)
    {
      if (this.needs == null || (long) idx >= (long) this.needs.Length)
        return false;
      if (this.GetConditionType(idx) == DELIVERY_CONDITION_TYPE.NONE)
        return true;
      string str = this.needs[(int) idx].conditionType.ToString();
      return str.Contains("DEFEAT_QUEST") || str.Contains("DEFEAT_FIELD");
    }

    public uint GetEnemyID(uint idx = 0)
    {
      return this.needs == null || (long) idx >= (long) this.needs.Length ? 0U : this.needs[(int) idx].enemyId;
    }

    public List<uint> GetEnemyIdList()
    {
      if (this.needs == null)
        return (List<uint>) null;
      List<uint> uintList = new List<uint>();
      int index = 0;
      for (int length = this.needs.Length; index < length; ++index)
      {
        uint enemyId = this.needs[index].enemyId;
        if (enemyId >= 1U)
          uintList.Add(enemyId);
      }
      return uintList.Count <= 0 ? (List<uint>) null : uintList;
    }

    public uint GetMapID(uint idx = 0)
    {
      return this.needs == null || (long) idx >= (long) this.needs.Length ? 0U : this.needs[(int) idx].mapId;
    }

    public List<uint> GetMapIdList()
    {
      if (this.needs == null)
        return (List<uint>) null;
      List<uint> uintList = new List<uint>();
      int index = 0;
      for (int length = this.needs.Length; index < length; ++index)
      {
        uint mapId = this.needs[index].mapId;
        if (mapId >= 1U)
          uintList.Add(mapId);
      }
      return uintList.Count <= 0 ? (List<uint>) null : uintList;
    }

    public DELIVERY_RATE_TYPE GetRateType(uint idx = 0)
    {
      return this.needs == null || (long) idx >= (long) this.needs.Length ? DELIVERY_RATE_TYPE.RATE_10000 : this.needs[(int) idx].rateType;
    }

    public bool IsNeedTarget(uint idx, uint enemyId, uint mapId)
    {
      return this.needs != null && (long) idx < (long) this.needs.Length && this.needs[(int) idx].IsNeedTarget(enemyId, mapId);
    }

    public string GetNeedItemName(uint idx = 0)
    {
      return this.needs == null || (long) idx >= (long) this.needs.Length ? string.Empty : this.needs[(int) idx].needName;
    }

    public uint GetNeedItemNum(uint idx = 0)
    {
      return this.needs == null || (long) idx >= (long) this.needs.Length ? 0U : (uint) this.needs[(int) idx].needNum;
    }

    public uint GetAllNeedItemNum()
    {
      if (this.needs == null)
        return 0;
      XorUInt allNeedItemNum = (XorUInt) 0U;
      int index = 0;
      for (int length = this.needs.Length; index < length; ++index)
        allNeedItemNum = (XorUInt) ((uint) allNeedItemNum + (uint) this.needs[index].needNum);
      return (uint) allNeedItemNum;
    }

    public DeliveryTable.UIType GetUIType()
    {
      if (this.type == DELIVERY_TYPE.EVENT)
        return DeliveryTable.UIType.EVENT;
      if (this.fieldMode == DIFFICULTY_MODE.HARD)
        return DeliveryTable.UIType.HARD;
      switch (this.type)
      {
        case DELIVERY_TYPE.DAILY:
        case DELIVERY_TYPE.MON:
        case DELIVERY_TYPE.TUE:
        case DELIVERY_TYPE.WED:
        case DELIVERY_TYPE.THU:
        case DELIVERY_TYPE.FRI:
        case DELIVERY_TYPE.SAT:
        case DELIVERY_TYPE.SUN:
        case DELIVERY_TYPE.DAY_OF_WEEK:
          return DeliveryTable.UIType.DAILY;
        case DELIVERY_TYPE.STORY:
          return DeliveryTable.UIType.STORY;
        case DELIVERY_TYPE.WEEKLY:
          return DeliveryTable.UIType.WEEKLY;
        case DELIVERY_TYPE.SUB_EVENT:
          return DeliveryTable.UIType.SUB_EVENT;
        default:
          return DeliveryTable.UIType.NONE;
      }
    }

    public DeliveryTable.UIType GetUITextType()
    {
      if (this.textType == DELIVERY_TYPE.ETC)
        return DeliveryTable.UIType.NONE;
      if (this.textType == DELIVERY_TYPE.DAILY)
        return DeliveryTable.UIType.DAILY;
      if (this.textType == DELIVERY_TYPE.WEEKLY)
        return DeliveryTable.UIType.WEEKLY;
      return this.textType == DELIVERY_TYPE.EVENT ? DeliveryTable.UIType.EVENT : DeliveryTable.UIType.NONE;
    }

    public DeliveryTable.DELIVERY_JUMPTYPE GetDeliveryJumpType()
    {
      DeliveryTable.DELIVERY_JUMPTYPE deliveryJumpType = DeliveryTable.DELIVERY_JUMPTYPE.UNDEFINED;
      if (Enum.IsDefined(typeof (DeliveryTable.DELIVERY_JUMPTYPE), (object) this.jumpType))
        deliveryJumpType = (DeliveryTable.DELIVERY_JUMPTYPE) this.jumpType;
      return deliveryJumpType;
    }

    public int DeliveryTypeIndex()
    {
      switch (this.GetUIType())
      {
        case DeliveryTable.UIType.STORY:
          return 2;
        case DeliveryTable.UIType.EVENT:
        case DeliveryTable.UIType.DAILY:
        case DeliveryTable.UIType.WEEKLY:
          return 1;
        case DeliveryTable.UIType.HARD:
          return 3;
        case DeliveryTable.UIType.SUB_EVENT:
          return 4;
        default:
          return 0;
      }
    }

    public DELIVERY_DROP_DIFFICULTY GetDeliveryDropRarity()
    {
      DELIVERY_RATE_TYPE rateType = this.GetRateType();
      if (rateType >= DELIVERY_RATE_TYPE.RATE_500)
        return DELIVERY_DROP_DIFFICULTY.SUPER_RARE;
      return rateType >= DELIVERY_RATE_TYPE.RATE_1500 ? DELIVERY_DROP_DIFFICULTY.RARE : DELIVERY_DROP_DIFFICULTY.NORMAL;
    }

    public QuestTable.QuestTableData GetQuestData()
    {
      if (this.needs == null || this.needs.Length == 0)
        return (QuestTable.QuestTableData) null;
      uint questId = this.needs[0].questId;
      return questId <= 0U ? (QuestTable.QuestTableData) null : Singleton<QuestTable>.I.GetQuestData(questId);
    }

    public ArenaTable.ArenaData GetArenaData()
    {
      return this.GetConditionType() == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID ? this.GetArenaDataWithRankUpDelivery() : this.GetArenaDataWithArenaDelivery();
    }

    private ArenaTable.ArenaData GetArenaDataWithArenaDelivery()
    {
      if (this.needs.Length == 0)
        return (ArenaTable.ArenaData) null;
      XorUInt needId = this.needs[0].needId;
      return (uint) needId <= 0U ? (ArenaTable.ArenaData) null : Singleton<ArenaTable>.I.GetArenaData((int) needId);
    }

    private ArenaTable.ArenaData GetArenaDataWithRankUpDelivery()
    {
      if (this.needs.Length == 0)
        return (ArenaTable.ArenaData) null;
      XorUInt needId = this.needs[0].needId;
      if ((uint) needId <= 0U)
        return (ArenaTable.ArenaData) null;
      return Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) needId)?.GetArenaData();
    }

    public REGION_DIFFICULTY_TYPE GetRegionDifficultyType()
    {
      RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint) this.regionId);
      return data == null ? REGION_DIFFICULTY_TYPE.NORMAL : data.difficulty;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is DeliveryTable.DeliveryData deliveryData))
        return false;
      bool flag = (int) this.id == (int) deliveryData.id && this.locationNumber == deliveryData.locationNumber && this.deliveryNumber == deliveryData.deliveryNumber && this.name == deliveryData.name && this.type == deliveryData.type && this.textType == deliveryData.textType && this.eventID == deliveryData.eventID && this.fieldMode == deliveryData.fieldMode && this.difficulty == deliveryData.difficulty && this.eventFlag == deliveryData.eventFlag && (int) this.npcID == (int) deliveryData.npcID && this.npcComment == deliveryData.npcComment && this.npcClearComment == deliveryData.npcClearComment && (int) this.readScriptId == (int) deliveryData.readScriptId && (int) this.clearEventID == (int) deliveryData.clearEventID && this.clearEventTitle == deliveryData.clearEventTitle && this.jumpType == deliveryData.jumpType && this.jumpMapID == deliveryData.jumpMapID && this.targetPortalID[0] == deliveryData.targetPortalID[0] && this.targetPortalID[1] == deliveryData.targetPortalID[1] && this.targetPortalID[2] == deliveryData.targetPortalID[2] && this.placeName == deliveryData.placeName && this.enemyName == deliveryData.enemyName && (int) this.appearQuestId == (int) deliveryData.appearQuestId && (int) this.appearDeliveryId == (int) deliveryData.appearDeliveryId;
      if (this.needs.Length == deliveryData.needs.Length)
      {
        for (int index = 0; index < this.needs.Length; ++index)
          flag = flag && this.needs[index].Equals((object) deliveryData.needs[index]);
      }
      else
        flag = false;
      return flag;
    }

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString()
    {
      return $"id:{(object) this.id}, locationNumber:{this.locationNumber}, deliveryNumber:{this.deliveryNumber}, name:{this.name}, type:{(object) this.type}, subType:{(object) this.subType}, textType:{(object) this.textType}, eventID:{(object) this.eventID}, fieldMode:{(object) this.fieldMode}, difficulty:{(object) this.difficulty}, eventFlag:{this.eventFlag.ToString()}, npcID:{(object) this.npcID}, npcComment:{this.npcComment}, npcClearComment:{this.npcClearComment}, readScriptId:{(object) this.readScriptId}, clearEventID:{(object) this.clearEventID}, clearEventTitle:{this.clearEventTitle}, jumpType:{(object) this.jumpType}, jumpMapID:{(object) this.jumpMapID}, targetPortalID[0]{(object) this.targetPortalID[0]}, targetPortalID[1]:{(object) this.targetPortalID[1]}, targetPortalID[2]:{(object) this.targetPortalID[2]}, placeName:{this.placeName}, enemyName:{this.enemyName}, appearQuestId:{(object) this.appearQuestId}, appearDeliveryId:{(object) this.appearDeliveryId}";
    }

    public class NeedData
    {
      public DELIVERY_CONDITION_TYPE conditionType;
      public uint enemyId;
      public uint mapId;
      public uint questId;
      public DELIVERY_RATE_TYPE rateType;
      public string needName;
      public XorUInt needNum = (XorUInt) 0U;
      public XorUInt needId;

      public NeedData(
        DELIVERY_CONDITION_TYPE _conditionType,
        uint _enemyId,
        uint _mapId,
        uint _questId,
        DELIVERY_RATE_TYPE _rateType,
        string _name,
        uint _num,
        uint _id)
      {
        this.conditionType = _conditionType;
        this.enemyId = _enemyId;
        this.mapId = _mapId;
        this.questId = _questId;
        this.rateType = _rateType;
        this.needName = _name;
        this.needNum = (XorUInt) _num;
        this.needId = (XorUInt) _id;
      }

      public bool IsValid()
      {
        return (this.conditionType != DELIVERY_CONDITION_TYPE.NONE || this.enemyId != 0U || this.mapId != 0U) && (uint) this.needNum != 0U;
      }

      public bool IsNeedTarget(uint enemyId, uint mapId)
      {
        if (this.enemyId > 0U)
        {
          if ((int) this.enemyId != (int) enemyId || this.mapId > 0U && (int) this.mapId != (int) mapId)
            return false;
        }
        else if (this.mapId <= 0U || (int) this.mapId != (int) mapId)
          return false;
        return true;
      }

      public override bool Equals(object obj)
      {
        return obj != null && obj is DeliveryTable.DeliveryData.NeedData needData && this.conditionType == needData.conditionType && (int) this.enemyId == (int) needData.enemyId && (int) this.mapId == (int) needData.mapId && (int) this.questId == (int) needData.questId && this.rateType == needData.rateType && this.needName == needData.needName && (int) this.needNum.value == (int) needData.needNum.value;
      }

      public override int GetHashCode() => base.GetHashCode();

      public override string ToString()
      {
        return $"conditionType:{(object) this.conditionType}, enemyId:{(object) this.enemyId}, mapId:{(object) this.mapId}, questId:{(object) this.questId}, rateType:{(object) this.rateType}, needName:{this.needName}, needNum:{(object) this.needNum}";
      }
    }
  }
}
