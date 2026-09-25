// Decompiled with JetBrains decompiler
// Type: QuestItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class QuestItemInfo : ItemInfoBase<QuestItem>
{
  public QuestInfoData infoData;
  public List<QuestItem.SellItem> sellItems;
  public List<float> remainTimes;

  public QuestItemInfo()
  {
  }

  public QuestItemInfo(QuestItem recv_data) => this.SetValue(recv_data);

  public override void SetValue(QuestItem recv_data)
  {
    this.uniqueID = ulong.Parse(recv_data.uniqId);
    this.tableID = (uint) recv_data.questId;
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(this.tableID);
    int[] mission_clear_status = (int[]) null;
    ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) this.tableID));
    if (clearStatusQuest != null)
      mission_clear_status = clearStatusQuest.missionStatus.ToArray();
    this.infoData = new QuestInfoData(questData, recv_data.reward, recv_data.num, 0, mission_clear_status);
    this.sellItems = recv_data.sellItems;
    this.remainTimes = recv_data.remainTimes;
  }

  public static InventoryList<QuestItemInfo, QuestItem> CreateList(List<QuestItem> recv_list)
  {
    InventoryList<QuestItemInfo, QuestItem> list = new InventoryList<QuestItemInfo, QuestItem>();
    recv_list.ForEach((Action<QuestItem>) (o =>
    {
      if (o.num <= 0)
        return;
      list.Add(o);
    }));
    return list;
  }
}
