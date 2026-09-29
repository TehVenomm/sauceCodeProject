// Decompiled with JetBrains decompiler
// Type: QuestTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

#nullable disable
public class QuestTable : Singleton<QuestTable>
{
  public const int SERIES_STAGE_NUM_MAX = 1;
  public const int SERIES_NUM_MAX = 3;
  public const int MISSION_NUM_MAX = 3;
  private UIntKeyTable<QuestTable.QuestTableData> questTable;
  private UIntKeyTable<QuestTable.MissionTableData> missionTable;

  public static UIntKeyTable<QuestTable.QuestTableData> CreateQuestTableCSV(string csv_text)
  {
    return TableUtility.CreateUIntKeyTable<QuestTable.QuestTableData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<QuestTable.QuestTableData>(QuestTable.QuestTableData.cb), "questId,questType,questStyle,rarity,level,getType,eventId,grade,difficulty,sortPriority,locationNum,questNum,name,appearQuestId,appearDeliveryId,rushId,rushIconId,mapId,stage1Name,enemy1Id,enemy1Lv,enemy2Id,enemy2Lv,enemy3Id,enemy3Lv,bgm1Id,bgm2Id,bgm3Id,time,mission1Id,mission2Id,mission3Id,cantSell,forcedefeat,storyId,userNumLimit");
  }

  public void CreateQuestTable(string csv_text)
  {
    this.questTable = QuestTable.CreateQuestTableCSV(csv_text);
  }

  public void AddQuestTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<QuestTable.QuestTableData>(this.questTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<QuestTable.QuestTableData>(QuestTable.QuestTableData.cb), "questId,questType,questStyle,rarity,level,getType,eventId,grade,difficulty,sortPriority,locationNum,questNum,name,appearQuestId,appearDeliveryId,rushId,rushIconId,mapId,stage1Name,enemy1Id,enemy1Lv,enemy2Id,enemy2Lv,enemy3Id,enemy3Lv,bgm1Id,bgm2Id,bgm3Id,time,mission1Id,mission2Id,mission3Id,cantSell,forcedefeat,storyId,userNumLimit");
  }

  public void CreateMissionTable(string csv_text)
  {
    this.missionTable = TableUtility.CreateUIntKeyTable<QuestTable.MissionTableData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<QuestTable.MissionTableData>(QuestTable.MissionTableData.cb), "missionId,name,type,require,param0");
  }

  public static UIntKeyTable<QuestTable.QuestTableData> CreateQuestTableBinary(byte[] bytes)
  {
    return TableUtility.CreateUIntKeyTableFromBinary<QuestTable.QuestTableData>(bytes);
  }

  public void CreateQuestTable(byte[] bytes)
  {
    this.questTable = QuestTable.CreateQuestTableBinary(bytes);
  }

  public void CreateQuestTable(MemoryStream stream)
  {
    this.questTable = TableUtility.CreateUIntKeyTableFromBinary<QuestTable.QuestTableData>(stream);
  }

  public void InitQuestDependencyData()
  {
    QuestCollection collection = MonoBehaviourSingleton<QuestManager>.I.questCollection;
    if (collection == null)
      return;
    this.questTable.ForEach((Action<QuestTable.QuestTableData>) (data =>
    {
      EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) data.GetMainEnemyID());
      if (enemyData == null)
        return;
      collection.Collect(enemyData.type, data.questType);
    }));
    collection.Sort();
  }

  public void AllQuestData(Action<QuestTable.QuestTableData> call_back)
  {
    if (this.questTable == null || call_back == null)
      return;
    this.questTable.ForEach((Action<QuestTable.QuestTableData>) (data => call_back(data)));
  }

  public void AllQuestDataAsc(Action<QuestTable.QuestTableData> call_back)
  {
    if (this.questTable == null || call_back == null)
      return;
    this.questTable.ForEachAsc((Action<QuestTable.QuestTableData>) (data => call_back(data)));
  }

  public void AllQuestDataDesc(Action<QuestTable.QuestTableData> call_back)
  {
    if (this.questTable == null || call_back == null)
      return;
    this.questTable.ForEachDesc((Action<QuestTable.QuestTableData>) (data => call_back(data)));
  }

  public QuestTable.QuestTableData GetQuestData(uint id)
  {
    if (!Singleton<QuestTable>.IsValid())
      return (QuestTable.QuestTableData) null;
    QuestTable.QuestTableData questData = this.questTable.Get(id);
    if (questData == null)
    {
      Log.TableError((object) this, id);
      questData = new QuestTable.QuestTableData();
      questData.questText = Log.NON_DATA_NAME;
    }
    return questData;
  }

  public static int GetQuestNum(QuestTable.QuestTableData q)
  {
    if (q.questType != QUEST_TYPE.ORDER)
      return -1;
    QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(q.questID);
    return questItem == null ? 0 : QuestTable.GetQuestNum(questItem);
  }

  public static int GetQuestNum(QuestItemInfo quest_item)
  {
    int num1 = quest_item.infoData.questData.num;
    int num2 = 0;
    if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
    {
      uint questId = quest_item.infoData.questData.tableData.questID;
      num2 = MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList.Where<GuildRequestItem>((Func<GuildRequestItem, bool>) (g => g.questId == (int) questId)).Count<GuildRequestItem>();
    }
    int num3 = num2;
    return Mathf.Max(num1 - num3, 0);
  }

  public QuestTable.MissionTableData GetMissionData(uint id)
  {
    return !Singleton<QuestTable>.IsValid() ? (QuestTable.MissionTableData) null : this.missionTable.Get(id);
  }

  public QuestTable.MissionTableData[] GetMissionData(uint[] mission_id)
  {
    QuestTable.MissionTableData[] missionData = new QuestTable.MissionTableData[mission_id.Length];
    for (int index = 0; index < mission_id.Length; ++index)
      missionData[index] = Singleton<QuestTable>.I.GetMissionData(mission_id[index]);
    return missionData;
  }

  public bool FindConditionQuest(QUEST_TYPE?[] type, DIFFICULTY_TYPE? difficulty, ENEMY_TYPE enemy)
  {
    bool is_find = false;
    this.questTable.ForEach((Action<QuestTable.QuestTableData>) (table =>
    {
      if (is_find)
        return;
      EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) table.GetMainEnemyID());
      if (enemyData == null || enemyData.type != enemy)
        return;
      if (difficulty.HasValue)
      {
        DIFFICULTY_TYPE? nullable = difficulty;
        DIFFICULTY_TYPE difficulty1 = table.difficulty;
        if (!(nullable.GetValueOrDefault() == difficulty1 & nullable.HasValue))
          return;
      }
      int index = 0;
      for (int length = type.Length; index < length; ++index)
      {
        if (type[index].HasValue)
        {
          int questType = (int) table.questType;
          QUEST_TYPE? nullable = type[index];
          int valueOrDefault = (int) nullable.GetValueOrDefault();
          if (questType == valueOrDefault & nullable.HasValue)
          {
            is_find = true;
            break;
          }
        }
      }
    }));
    return is_find;
  }

  public bool ContainEnemy(int enemy_id)
  {
    bool is_find = false;
    this.questTable.ForEach((Action<QuestTable.QuestTableData>) (table =>
    {
      if (is_find || table.enemyID == null)
        return;
      int index = 0;
      for (int length = table.enemyID.Length; index < length; ++index)
      {
        if (table.enemyID[index] == enemy_id)
        {
          is_find = true;
          break;
        }
      }
    }));
    return is_find;
  }

  public IEnumerable<QuestTable.QuestTableData> GetEnemyAppearQuestData(uint enemy_id)
  {
    List<QuestTable.QuestTableData> enemyTable = new List<QuestTable.QuestTableData>();
    this.questTable.ForEach((Action<QuestTable.QuestTableData>) (table =>
    {
      if (table.enemyID == null || Array.IndexOf<int>(table.enemyID, (int) enemy_id) < 0)
        return;
      enemyTable.Add(table);
    }));
    foreach (QuestTable.QuestTableData questTableData in enemyTable)
      yield return questTableData;
  }

  public QuestTable.QuestTableData GetEventQuestData(int eventId)
  {
    return this.questTable.Find((Predicate<QuestTable.QuestTableData>) (data => data.eventId == eventId));
  }

  public List<QuestTable.QuestTableData> GetEventQuestDataList(int eventId)
  {
    List<QuestTable.QuestTableData> questList = new List<QuestTable.QuestTableData>();
    this.questTable.ForEach((Action<QuestTable.QuestTableData>) (x =>
    {
      if (x.eventId != eventId)
        return;
      questList.Add(x);
    }));
    return questList;
  }

  public static List<QuestTable.QuestTableData> GetSameRushQuestData(uint searchId)
  {
    List<QuestTable.QuestTableData> list = new List<QuestTable.QuestTableData>();
    Singleton<QuestTable>.I.questTable.ForEach((Action<QuestTable.QuestTableData>) (x =>
    {
      if ((int) x.rushId != (int) searchId)
        return;
      list.Add(x);
    }));
    return list;
  }

  public class QuestTableData : IUIntKeyBinaryTableData
  {
    public uint questID;
    public QUEST_TYPE questType;
    public QUEST_STYLE questStyle;
    public RARITY_TYPE rarity;
    public int level;
    public GET_TYPE getType;
    public int eventId;
    public int grade;
    public DIFFICULTY_TYPE difficulty;
    public int sortPriority;
    public string locationNumber;
    public string questNumber;
    public string questText;
    public uint appearQuestId;
    public uint appearDeliveryId;
    public uint rushId;
    public uint rushIconId;
    public uint mapId;
    public string[] stageName = new string[3];
    public int[] enemyID = new int[3];
    public int[] enemyLv = new int[3];
    public int[] bgmID = new int[3];
    public float limitTime;
    public uint[] missionID = new uint[3];
    public bool cantSale;
    public bool forceDefeat;
    public int storyId;
    public int userNumLimit;
    public int seriesNum;
    public const string NT = "questId,questType,questStyle,rarity,level,getType,eventId,grade,difficulty,sortPriority,locationNum,questNum,name,appearQuestId,appearDeliveryId,rushId,rushIconId,mapId,stage1Name,enemy1Id,enemy1Lv,enemy2Id,enemy2Lv,enemy3Id,enemy3Lv,bgm1Id,bgm2Id,bgm3Id,time,mission1Id,mission2Id,mission3Id,cantSell,forcedefeat,storyId,userNumLimit";

    public QuestTableData()
    {
      for (int index = 0; index < 3; ++index)
        this.stageName[index] = string.Empty;
    }

    public static bool cb(CSVReader csv_reader, QuestTable.QuestTableData data, ref uint key)
    {
      data.questID = key;
      csv_reader.Pop<QUEST_TYPE>(ref data.questType);
      csv_reader.Pop<QUEST_STYLE>(ref data.questStyle);
      csv_reader.PopEnum<RARITY_TYPE>(ref data.rarity, RARITY_TYPE.D);
      csv_reader.Pop(ref data.level);
      csv_reader.Pop<GET_TYPE>(ref data.getType);
      csv_reader.Pop(ref data.eventId);
      csv_reader.Pop(ref data.grade);
      csv_reader.Pop<DIFFICULTY_TYPE>(ref data.difficulty);
      csv_reader.Pop(ref data.sortPriority);
      csv_reader.Pop(ref data.locationNumber);
      csv_reader.Pop(ref data.questNumber);
      csv_reader.Pop(ref data.questText);
      csv_reader.Pop(ref data.appearQuestId);
      csv_reader.Pop(ref data.appearDeliveryId);
      csv_reader.Pop(ref data.rushId);
      csv_reader.Pop(ref data.rushIconId);
      csv_reader.Pop(ref data.mapId);
      for (int index = 0; index < 1; ++index)
        csv_reader.Pop(ref data.stageName[index]);
      for (int index = 0; index < 3; ++index)
      {
        csv_reader.Pop(ref data.enemyID[index]);
        csv_reader.Pop(ref data.enemyLv[index]);
      }
      for (int index = 0; index < 3; ++index)
        csv_reader.Pop(ref data.bgmID[index]);
      csv_reader.Pop(ref data.limitTime);
      csv_reader.Pop(ref data.missionID[0]);
      csv_reader.Pop(ref data.missionID[1]);
      csv_reader.Pop(ref data.missionID[2]);
      csv_reader.Pop(ref data.cantSale);
      csv_reader.Pop(ref data.forceDefeat);
      csv_reader.Pop(ref data.storyId);
      csv_reader.Pop(ref data.userNumLimit);
      if (data.userNumLimit <= 0)
        data.userNumLimit = 4;
      if (data.sortPriority == 0)
        data.sortPriority = (int) key;
      uint num;
      if (string.IsNullOrEmpty(data.locationNumber))
      {
        QuestTable.QuestTableData questTableData = data;
        num = data.questID / 100U % 1000U;
        string str = num.ToString();
        questTableData.locationNumber = str;
      }
      if (string.IsNullOrEmpty(data.questNumber))
      {
        QuestTable.QuestTableData questTableData = data;
        num = data.questID % 100U;
        string str = num.ToString();
        questTableData.questNumber = str;
      }
      data.seriesNum = 0;
      for (int index = 0; index < 3 && data.enemyID[index] != 0; ++index)
        ++data.seriesNum;
      return true;
    }

    public bool IsMissionExist()
    {
      if (this.missionID == null)
        return false;
      int index = 0;
      for (int length = this.missionID.Length; index < length; ++index)
      {
        if (this.missionID[index] == 0U)
          return false;
      }
      return true;
    }

    public int GetMainEnemyID() => this.seriesNum <= 0 ? 0 : this.enemyID[this.seriesNum - 1];

    public int GetEnemyIdByIndex(int index)
    {
      return this.seriesNum <= 0 || index >= this.seriesNum ? 0 : this.enemyID[index];
    }

    public int GetMainEnemyLv()
    {
      if (this.seriesNum <= 0)
        return 0;
      int level = this.enemyLv[this.seriesNum - 1];
      if (level == 0 && Singleton<EnemyTable>.IsValid())
      {
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.GetMainEnemyID());
        if (enemyData != null)
          level = (int) enemyData.level;
      }
      return level;
    }

    public string GetFoundationName()
    {
      return ResourceName.GetFoundationName(this.stageName[this.seriesNum - 1]);
    }

    public void LoadFromBinary(BinaryTableReader reader, ref uint key)
    {
      this.questID = key;
      this.questType = (QUEST_TYPE) reader.ReadInt32();
      this.questStyle = (QUEST_STYLE) reader.ReadInt32();
      this.rarity = (RARITY_TYPE) reader.ReadInt32();
      this.getType = (GET_TYPE) reader.ReadInt32();
      this.eventId = reader.ReadInt32();
      this.grade = reader.ReadInt32();
      this.difficulty = (DIFFICULTY_TYPE) reader.ReadInt32();
      this.sortPriority = reader.ReadInt32();
      this.locationNumber = reader.ReadString();
      this.questNumber = reader.ReadString();
      this.questText = reader.ReadString();
      this.appearQuestId = reader.ReadUInt32();
      this.appearDeliveryId = reader.ReadUInt32();
      this.rushId = reader.ReadUInt32();
      this.mapId = reader.ReadUInt32();
      for (int index = 0; index < 1; ++index)
        this.stageName[index] = reader.ReadString();
      for (int index = 0; index < 3; ++index)
      {
        this.enemyID[index] = reader.ReadInt32();
        this.enemyLv[index] = reader.ReadInt32();
      }
      for (int index = 0; index < 3; ++index)
        this.bgmID[index] = reader.ReadInt32();
      this.limitTime = reader.ReadSingle();
      this.missionID[0] = reader.ReadUInt32();
      this.missionID[1] = reader.ReadUInt32();
      this.missionID[2] = reader.ReadUInt32();
      this.cantSale = reader.ReadBoolean();
      this.forceDefeat = reader.ReadBoolean();
      this.storyId = reader.ReadInt32();
      if (this.sortPriority == 0)
        this.sortPriority = (int) key;
      uint num;
      if (string.IsNullOrEmpty(this.locationNumber))
      {
        num = this.questID / 100U % 1000U;
        this.locationNumber = num.ToString();
      }
      if (string.IsNullOrEmpty(this.questNumber))
      {
        num = this.questID % 100U;
        this.questNumber = num.ToString();
      }
      this.seriesNum = 0;
      for (int index = 0; index < 3 && this.enemyID[index] != 0; ++index)
        ++this.seriesNum;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is QuestTable.QuestTableData questTableData))
        return false;
      bool flag = (int) this.questID == (int) questTableData.questID && this.questType == questTableData.questType && this.questStyle == questTableData.questStyle && this.rarity == questTableData.rarity && this.getType == questTableData.getType && this.eventId == questTableData.eventId && this.grade == questTableData.grade && this.difficulty == questTableData.difficulty && this.sortPriority == questTableData.sortPriority && this.locationNumber == questTableData.locationNumber && this.questNumber == questTableData.questNumber && this.questText == questTableData.questText && (int) this.appearQuestId == (int) questTableData.appearQuestId && (int) this.appearDeliveryId == (int) questTableData.appearDeliveryId && (int) this.mapId == (int) questTableData.mapId && (double) this.limitTime == (double) questTableData.limitTime && this.cantSale == questTableData.cantSale && this.forceDefeat == questTableData.forceDefeat && this.storyId == questTableData.storyId && this.seriesNum == questTableData.seriesNum;
      for (int index = 0; index < this.stageName.Length; ++index)
        flag = flag && this.stageName[index] == questTableData.stageName[index];
      for (int index = 0; index < this.enemyID.Length; ++index)
        flag = flag && this.enemyID[index] == questTableData.enemyID[index];
      for (int index = 0; index < this.enemyLv.Length; ++index)
        flag = flag && this.enemyLv[index] == questTableData.enemyLv[index];
      for (int index = 0; index < this.bgmID.Length; ++index)
        flag = flag && this.bgmID[index] == questTableData.bgmID[index];
      for (int index = 0; index < this.missionID.Length; ++index)
        flag = flag && (int) this.missionID[index] == (int) questTableData.missionID[index];
      return flag;
    }

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString()
    {
      return $"questID:{(object) this.questID}, questType:{(object) this.questType}, questStyle:{(object) this.questStyle}, rarity:{(object) this.rarity}, getType:{(object) this.getType}, eventId:{(object) this.eventId}, grade:{(object) this.grade}, difficulty:{(object) this.difficulty}, sortPriority:{(object) this.sortPriority}, locationNumber:{this.locationNumber}, questNumber:{this.questNumber}, questText:{this.questText}, appearQuestId:{(object) this.appearQuestId}, appearDeliveryId:{(object) this.appearDeliveryId}, mapId:{(object) this.mapId}, limitTime:{(object) this.limitTime}, cantSale:{this.cantSale.ToString()}, forceDefeat:{this.forceDefeat.ToString()}, storyId:{(object) this.storyId}, seriesNum:{(object) this.seriesNum}";
    }
  }

  public class MissionTableData
  {
    public uint missionID;
    public string missionText;
    public MISSION_TYPE missionType;
    public MISSION_REQUIRE missionRequire;
    public int missionParam;
    public const string NT = "missionId,name,type,require,param0";

    public static bool cb(CSVReader csv_reader, QuestTable.MissionTableData data, ref uint key)
    {
      data.missionID = key;
      csv_reader.Pop(ref data.missionText);
      csv_reader.Pop<MISSION_TYPE>(ref data.missionType);
      csv_reader.Pop<MISSION_REQUIRE>(ref data.missionRequire);
      csv_reader.Pop(ref data.missionParam);
      return true;
    }
  }
}
