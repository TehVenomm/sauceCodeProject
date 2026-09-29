// Decompiled with JetBrains decompiler
// Type: DeliveryRewardTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class DeliveryRewardTable : Singleton<DeliveryRewardTable>, IDataTable
{
  private DoubleUIntKeyTable<DeliveryRewardTable.DeliveryRewardData> tableData;

  public void CreateTable(string csv_text)
  {
    this.tableData = TableUtility.CreateDoubleUIntKeyTable<DeliveryRewardTable.DeliveryRewardData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<DeliveryRewardTable.DeliveryRewardData>(DeliveryRewardTable.DeliveryRewardData.cb), "id,type,itemId,num,param_0", new TableUtility.CallBackDoubleUIntSecondKey(DeliveryRewardTable.DeliveryRewardData.CBSecondKey));
    this.tableData.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<DeliveryRewardTable.DeliveryRewardData>(this.tableData, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<DeliveryRewardTable.DeliveryRewardData>(DeliveryRewardTable.DeliveryRewardData.cb), "id,type,itemId,num,param_0", new TableUtility.CallBackDoubleUIntSecondKey(DeliveryRewardTable.DeliveryRewardData.CBSecondKey));
  }

  public DeliveryRewardTable.DeliveryRewardData[] GetDeliveryRewardTableData(uint id)
  {
    if (this.tableData == null)
      return (DeliveryRewardTable.DeliveryRewardData[]) null;
    UIntKeyTable<DeliveryRewardTable.DeliveryRewardData> uintKeyTable = this.tableData.Get(id);
    if (uintKeyTable == null)
      return (DeliveryRewardTable.DeliveryRewardData[]) null;
    List<DeliveryRewardTable.DeliveryRewardData> list = new List<DeliveryRewardTable.DeliveryRewardData>();
    uintKeyTable.ForEach((Action<DeliveryRewardTable.DeliveryRewardData>) (data => list.Add(data)));
    if (list.Count == 0)
      return (DeliveryRewardTable.DeliveryRewardData[]) null;
    list.Sort((Comparison<DeliveryRewardTable.DeliveryRewardData>) ((l, r) => (int) l.rewardIndex - (int) r.rewardIndex));
    return list.ToArray();
  }

  public class DeliveryRewardData
  {
    public uint id;
    public uint rewardIndex;
    public DeliveryRewardTable.DeliveryRewardData.Reward reward;
    public const string NT = "id,type,itemId,num,param_0";

    public static bool cb(
      CSVReader csv_reader,
      DeliveryRewardTable.DeliveryRewardData data,
      ref uint key1,
      ref uint key2)
    {
      data.id = key1;
      data.rewardIndex = key2;
      DeliveryRewardTable.DeliveryRewardData.Reward reward = new DeliveryRewardTable.DeliveryRewardData.Reward();
      csv_reader.Pop<REWARD_TYPE>(ref reward.type);
      csv_reader.Pop(ref reward.item_id);
      csv_reader.Pop(ref reward.num);
      csv_reader.Pop(ref reward.param);
      data.reward = reward;
      return true;
    }

    public static string CBSecondKey(CSVReader csv, int table_data_num)
    {
      return table_data_num.ToString();
    }

    public class Reward
    {
      public REWARD_TYPE type;
      public uint item_id;
      public int num;
      public int param;
    }
  }
}
