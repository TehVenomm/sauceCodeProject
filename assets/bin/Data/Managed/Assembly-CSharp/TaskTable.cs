// Decompiled with JetBrains decompiler
// Type: TaskTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class TaskTable : Singleton<TaskTable>, IDataTable
{
  private UIntKeyTable<TaskTable.TaskData> taskDataTable;

  public void CreateTable(string csv_text)
  {
    this.taskDataTable = TableUtility.CreateUIntKeyTable<TaskTable.TaskData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<TaskTable.TaskData>(TaskTable.TaskData.cb), "taskId,title,detail,orderNo,openRequirements,startTime,endTime,goalNum,conditionVal1,conditionVal2,conditionVal3,conditionVal4,conditionVal5,rewardType,itemId,rewardNum,rewardVal1,rewardVal2,minVersion");
    this.taskDataTable.TrimExcess();
  }

  public TaskTable.TaskData Get(uint id)
  {
    if (this.taskDataTable == null)
      return (TaskTable.TaskData) null;
    TaskTable.TaskData taskData = this.taskDataTable.Get(id);
    if (taskData != null)
      return taskData;
    Log.TableError((object) this, id);
    return (TaskTable.TaskData) null;
  }

  public class TaskData
  {
    public int id;
    public string title;
    public string detail;
    public int orderNo;
    public string openRequirements;
    public string startTime;
    public string endTime;
    public int goalNum;
    public string conditionVal1;
    public string conditionVal2;
    public string conditionVal3;
    public string conditionVal4;
    public string conditionVal5;
    public REWARD_TYPE rewardType;
    public int itemId;
    public int rewardNum;
    public int rewartVal1;
    public int rewartVal2;
    public string minVersion;
    public const string NT = "taskId,title,detail,orderNo,openRequirements,startTime,endTime,goalNum,conditionVal1,conditionVal2,conditionVal3,conditionVal4,conditionVal5,rewardType,itemId,rewardNum,rewardVal1,rewardVal2,minVersion";

    public static bool cb(CSVReader csv_reader, TaskTable.TaskData data, ref uint key)
    {
      data.id = (int) key;
      csv_reader.Pop(ref data.title);
      csv_reader.Pop(ref data.detail);
      csv_reader.Pop(ref data.orderNo);
      csv_reader.Pop(ref data.openRequirements);
      csv_reader.Pop(ref data.startTime);
      csv_reader.Pop(ref data.endTime);
      csv_reader.Pop(ref data.goalNum);
      csv_reader.Pop(ref data.conditionVal1);
      csv_reader.Pop(ref data.conditionVal2);
      csv_reader.Pop(ref data.conditionVal3);
      csv_reader.Pop(ref data.conditionVal4);
      csv_reader.Pop(ref data.conditionVal5);
      string empty = string.Empty;
      csv_reader.Pop(ref empty);
      data.rewardType = (REWARD_TYPE) Enum.Parse(typeof (REWARD_TYPE), empty);
      csv_reader.Pop(ref data.itemId);
      csv_reader.Pop(ref data.rewardNum);
      csv_reader.Pop(ref data.rewartVal1);
      csv_reader.Pop(ref data.rewartVal2);
      csv_reader.Pop(ref data.minVersion);
      return true;
    }

    public string GetRewardString()
    {
      return !Singleton<ItemTable>.IsValid() ? string.Empty : MonoBehaviourSingleton<AchievementManager>.I.GetRewardName(this.rewardType, (uint) this.itemId, (uint) this.rewardNum, (uint) this.rewartVal1);
    }
  }
}
