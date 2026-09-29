// Decompiled with JetBrains decompiler
// Type: AccessoryInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class AccessoryInfo : ItemInfoBase<Accessory>
{
  public bool isFavorite;
  public AccessoryTable.AccessoryData tableData;
  public List<AccessoryTable.AccessoryInfoData> tableInfos;

  public static InventoryList<AccessoryInfo, Accessory> CreateList(List<Accessory> recv_list)
  {
    InventoryList<AccessoryInfo, Accessory> list = new InventoryList<AccessoryInfo, Accessory>();
    if (!recv_list.IsNullOrEmpty<Accessory>())
      recv_list.ForEach((Action<Accessory>) (o => list.Add(o)));
    return list;
  }

  public override void SetValue(Accessory recieve)
  {
    if (!Singleton<AccessoryTable>.IsValid())
      return;
    this.tableData = Singleton<AccessoryTable>.I.GetData((uint) recieve.accessoryId);
    if (this.tableData == null)
      return;
    this.tableInfos = Singleton<AccessoryTable>.I.GetInfoList((uint) recieve.accessoryId);
    this.uniqueID = ulong.Parse(recieve.uniqId);
    this.tableID = (uint) recieve.accessoryId;
    this.isFavorite = recieve.is_locked != 0;
  }

  public void SetValue(uint aid)
  {
    if (!Singleton<AccessoryTable>.IsValid())
      return;
    this.tableData = Singleton<AccessoryTable>.I.GetData(aid);
    if (this.tableData == null)
      return;
    this.tableInfos = Singleton<AccessoryTable>.I.GetInfoList(aid);
    this.tableID = aid;
    this.isFavorite = false;
  }

  public AccessoryTable.AccessoryInfoData GetInfo(ACCESSORY_PART part)
  {
    return this.tableInfos.IsNullOrEmpty<AccessoryTable.AccessoryInfoData>() ? (AccessoryTable.AccessoryInfoData) null : this.tableInfos.Find((Predicate<AccessoryTable.AccessoryInfoData>) (i => i.attachPlace == part));
  }
}
