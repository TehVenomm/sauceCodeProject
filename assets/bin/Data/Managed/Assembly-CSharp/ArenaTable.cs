// Decompiled with JetBrains decompiler
// Type: ArenaTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ArenaTable : Singleton<ArenaTable>, IDataTable
{
  public const int WAVE_NUM = 5;
  public const int LIMIT_NUM = 3;
  public const int CONDITION_NUM = 3;
  private UIntKeyTable<ArenaTable.ArenaData> arenaDataTable;

  public void CreateTable(string csv_text)
  {
    this.arenaDataTable = TableUtility.CreateUIntKeyTable<ArenaTable.ArenaData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<ArenaTable.ArenaData>(ArenaTable.ArenaData.cb), "id,groupId,rank,limit_0,limit_1,limit_2,condition_0,condition_1,condition_2,timeLimit,level,questId_0,questId_1,questId_2,questId_3,questId_4");
    this.arenaDataTable.TrimExcess();
  }

  public ArenaTable.ArenaData GetArenaData(int arenaId) => this.arenaDataTable.Get((uint) arenaId);

  public class ArenaData
  {
    public int id;
    public ARENA_GROUP group;
    public ARENA_RANK rank;
    public ARENA_LIMIT[] limits;
    public ARENA_CONDITION[] conditions;
    public int timeLimit;
    public int level;
    public int[] questIds;
    public const string NT = "id,groupId,rank,limit_0,limit_1,limit_2,condition_0,condition_1,condition_2,timeLimit,level,questId_0,questId_1,questId_2,questId_3,questId_4";

    public static bool cb(CSVReader csv_reader, ArenaTable.ArenaData data, ref uint key)
    {
      data.id = (int) key;
      csv_reader.PopEnum<ARENA_GROUP>(ref data.group, ARENA_GROUP.E);
      csv_reader.PopEnum<ARENA_RANK>(ref data.rank, ARENA_RANK.C);
      data.limits = new ARENA_LIMIT[3];
      for (int index = 0; index < 3; ++index)
        csv_reader.PopEnum<ARENA_LIMIT>(ref data.limits[index], ARENA_LIMIT.NONE);
      data.conditions = new ARENA_CONDITION[3];
      for (int index = 0; index < 3; ++index)
        csv_reader.PopEnum<ARENA_CONDITION>(ref data.conditions[index], ARENA_CONDITION.NONE);
      csv_reader.Pop(ref data.timeLimit);
      csv_reader.Pop(ref data.level);
      data.questIds = new int[5];
      for (int index = 0; index < 5; ++index)
        csv_reader.Pop(ref data.questIds[index]);
      return true;
    }

    public List<QuestTable.QuestTableData> GetQuestDataArray()
    {
      List<QuestTable.QuestTableData> questDataArray = new List<QuestTable.QuestTableData>(5);
      int index1 = 0;
      for (int index2 = 5; index1 < index2; ++index1)
      {
        int questId = this.questIds[index1];
        if (questId > 0)
        {
          QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) questId);
          if (questData == null)
            Debug.LogError((object) $"ArenaTableに存在しないクエストId: {(object) this.questIds[index1]}が設定されています");
          else
            questDataArray.Add(questData);
        }
      }
      return questDataArray;
    }
  }
}
