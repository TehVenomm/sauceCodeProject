// Decompiled with JetBrains decompiler
// Type: ItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class ItemInfo : ItemInfoBase<Network.Item>
{
  public int num;
  public ItemTable.ItemData tableData;
  public List<ExpiredItem> expiredAtItem;

  public ItemInfo()
  {
  }

  public ItemInfo(Network.Item recv_data) => this.SetValue(recv_data);

  public override void SetValue(Network.Item recv_data)
  {
    this.uniqueID = ulong.Parse(recv_data.uniqId);
    this.tableID = (uint) recv_data.itemId;
    this.num = recv_data.num;
    this.tableData = Singleton<ItemTable>.I.GetItemData(this.tableID);
  }

  public static InventoryList<ItemInfo, Network.Item> CreateList(List<Network.Item> recv_list)
  {
    InventoryList<ItemInfo, Network.Item> list = new InventoryList<ItemInfo, Network.Item>();
    recv_list.ForEach((Action<Network.Item>) (o => list.Add(o)));
    return list;
  }

  public static ItemInfo CreateItemInfo(Network.Item item)
  {
    ItemInfo itemInfo = new ItemInfo();
    itemInfo.SetValue(item);
    return itemInfo;
  }

  public static ItemInfo CreateItemInfo(int itemId)
  {
    return ItemInfo.CreateItemInfo(new Network.Item()
    {
      uniqId = "0",
      itemId = itemId,
      num = 0
    });
  }

  public ITEM_TYPE GetType() => this.tableData.type;

  public int GetNum()
  {
    return this.expiredAtItem == null ? this.num : this.expiredAtItem.FindAll((Predicate<ExpiredItem>) (x => x.CanUse())).Count;
  }
}
