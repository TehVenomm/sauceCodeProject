// Decompiled with JetBrains decompiler
// Type: QuestInfoData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class QuestInfoData
{
  public QuestInfoData.Quest questData;
  public QuestInfoData.Mission[] missionData;
  public bool isExistMission;

  public static QuestInfoData.Mission[] CreateMissionData(QuestTable.QuestTableData quest_table)
  {
    if (quest_table.missionID == null || quest_table.missionID[0] == 0U && quest_table.missionID[1] == 0U && quest_table.missionID[2] == 0U)
      return (QuestInfoData.Mission[]) null;
    QuestInfoData.Mission[] mission_info = new QuestInfoData.Mission[3];
    bool is_find = false;
    MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.ForEach((Action<ClearStatusQuest>) (data =>
    {
      if (is_find || (long) data.questId != (long) quest_table.questID)
        return;
      is_find = true;
      int index = 0;
      data.missionStatus.ForEach((Action<int>) (mission =>
      {
        if (quest_table.missionID[index] == 0U)
          return;
        mission_info[index] = new QuestInfoData.Mission(Singleton<QuestTable>.I.GetMissionData(quest_table.missionID[index]), (CLEAR_STATUS) data.missionStatus[index]);
        ++index;
      }));
    }));
    if (!is_find)
    {
      for (int index = 0; index < 3; ++index)
      {
        if (quest_table.missionID[index] != 0U)
          mission_info[index] = new QuestInfoData.Mission(Singleton<QuestTable>.I.GetMissionData(quest_table.missionID[index]), CLEAR_STATUS.NOT_CLEAR);
      }
    }
    return mission_info;
  }

  public QuestInfoData(
    QuestTable.QuestTableData quest_table_data,
    QuestData quest_list,
    int[] mission_clear_status)
  {
    this.questData = new QuestInfoData.Quest(quest_table_data, quest_list);
    this.MissionInit(mission_clear_status);
  }

  public QuestInfoData(
    QuestTable.QuestTableData quest_table_data,
    QuestData.QuestRewardList reward_list,
    int have_num,
    int crystal_num,
    int[] mission_clear_status)
  {
    this.questData = new QuestInfoData.Quest(quest_table_data, have_num, crystal_num, reward_list);
    this.MissionInit(mission_clear_status);
  }

  private void MissionInit(int[] mission_clear_status)
  {
    this.missionData = new QuestInfoData.Mission[3];
    this.isExistMission = false;
    for (int index = 0; index < 3; ++index)
    {
      if (this.questData.tableData.missionID[index] == 0U)
      {
        this.missionData[index] = (QuestInfoData.Mission) null;
      }
      else
      {
        CLEAR_STATUS _state = mission_clear_status != null ? (CLEAR_STATUS) mission_clear_status[index] : CLEAR_STATUS.NEW;
        this.missionData[index] = new QuestInfoData.Mission(Singleton<QuestTable>.I.GetMissionData(this.questData.tableData.missionID[index]), _state);
        this.isExistMission = true;
      }
    }
  }

  public bool IsMissionEmpty()
  {
    if (this.missionData == null)
      return true;
    bool flag = false;
    int index = 0;
    for (int length = this.missionData.Length; index < length; ++index)
    {
      if (this.missionData[index] != null && this.missionData[index].tableData != null)
      {
        flag = true;
        break;
      }
    }
    return !flag;
  }

  public class Mission
  {
    public QuestTable.MissionTableData tableData;
    public CLEAR_STATUS state;

    public Mission(QuestTable.MissionTableData _table, CLEAR_STATUS _state = CLEAR_STATUS.LOCK)
    {
      this.tableData = _table;
      this.state = _state;
    }
  }

  public class Quest
  {
    public QuestTable.QuestTableData tableData;
    public int useCrystal;
    public int num;
    public QuestInfoData.Quest.Reward[] reward;

    public Quest(QuestTable.QuestTableData quest_table_data, QuestData quest_list)
    {
      int have_num = quest_list.order != null ? quest_list.order.num : 1;
      this.Init(quest_table_data, have_num, quest_list.crystalNum, quest_list.reward);
    }

    public Quest(
      QuestTable.QuestTableData quest_table_data,
      int have_num,
      int crystal_num,
      QuestData.QuestRewardList reward_list)
    {
      this.Init(quest_table_data, have_num, crystal_num, reward_list);
    }

    private void Init(
      QuestTable.QuestTableData quest_table_data,
      int have_num,
      int crystal_num,
      QuestData.QuestRewardList reward_list)
    {
      this.tableData = quest_table_data;
      this.useCrystal = crystal_num;
      int[] array1 = reward_list.itemIds.ToArray();
      int[] array2 = reward_list.types.ToArray();
      int[] array3 = reward_list.pri.ToArray();
      int length = array1.Length;
      if (length > 0)
      {
        this.reward = new QuestInfoData.Quest.Reward[length];
        int index1 = 0;
        for (int index2 = length; index1 < index2; ++index1)
          this.reward[index1] = new QuestInfoData.Quest.Reward(array2[index1], array1[index1], array3[index1]);
        Array.Sort<QuestInfoData.Quest.Reward>(this.reward, (Comparison<QuestInfoData.Quest.Reward>) ((l, r) => l.priority - r.priority));
      }
      else
        this.reward = (QuestInfoData.Quest.Reward[]) null;
      this.num = have_num;
    }

    public class Reward
    {
      public int type;
      public int id;
      public int priority;

      public Reward(int _type, int _id, int _priority)
      {
        this.type = _type;
        this.id = _id;
        this.priority = _priority;
      }
    }
  }
}
