// Decompiled with JetBrains decompiler
// Type: ItemDetailUseItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ItemDetailUseItem : GameSection
{
  private SortCompareData data;

  public override void Initialize()
  {
    this.data = GameSection.GetEventData() as SortCompareData;
    GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.USE_ITEM, this.data.GetUniqID());
    base.Initialize();
  }

  public override void UpdateUI()
  {
    ItemInfo itemData = this.data.GetItemData() as ItemInfo;
    this.SetActive((Enum) ItemDetailUseItem.UI.STR_SELL, this.data.CanSale());
    this.SetActive((Enum) ItemDetailUseItem.UI.BTN_DETAIL_SELL, this.data.CanSale() && MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
    this.SetLabelText((Enum) ItemDetailUseItem.UI.LBL_NAME, this.data.GetName());
    this.SetLabelText((Enum) ItemDetailUseItem.UI.LBL_HAVE_NUM, "Qty:" + this.data.GetNum().ToString());
    this.SetLabelText((Enum) ItemDetailUseItem.UI.LBL_DESCRIPTION, itemData.tableData.text);
    this.SetLabelText((Enum) ItemDetailUseItem.UI.LBL_SELL, this.data.GetSalePrice().ToString());
    int enemyIconId = itemData.tableData.enemyIconID;
    int enemyIconId2 = itemData.tableData.enemyIconID2;
    ItemIcon.Create(this.data.GetIconType(), this.data.GetIconID(), new RARITY_TYPE?(this.data.GetRarity()), this.GetCtrl((Enum) ItemDetailUseItem.UI.OBJ_ICON_ROOT), this.data.GetIconElement(), this.data.GetIconMagiEnableType(), enemy_icon_id: enemyIconId, enemy_icon_id2: enemyIconId2, getType: this.data.GetGetType());
  }

  public void OnQuery_USE()
  {
    if (MonoBehaviourSingleton<StatusManager>.I.IsEffectedItem(this.data.GetItemData() as ItemInfo))
      GameSection.ChangeEvent("OVER_WRITE_BOOST", (object) new object[1]
      {
        (object) this.data.GetName()
      });
    else
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.data.GetName()
      });
  }

  protected void OnQuery_ItemDetailUseConfirm_YES() => this.SendUseItem();

  protected void OnQuery_ItemDetailUseOverWriteConfirm_YES() => this.SendUseItem();

  protected void SendUseItem()
  {
    GameSection.StayEvent();
    if (!(this.data.GetItemData() is ItemInfo itemData) || itemData.tableData == null)
      return;
    if (itemData.tableData.id == 7500101U || itemData.tableData.id == 7500102U)
      MonoBehaviourSingleton<InventoryManager>.I.SendInventoryAutoItem(this.data.GetUniqID().ToString(), (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    else
      MonoBehaviourSingleton<InventoryManager>.I.SendInventoryUseItem(this.data.GetUniqID().ToString(), (Action<bool>) (is_success =>
      {
        if (is_success && FieldManager.IsValidInGame() && MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
          MonoBehaviourSingleton<CoopNetworkManager>.I.UpdateBoost();
        GameSection.ResumeEvent(is_success);
      }));
  }

  private void OnQuery_SELL()
  {
    if (!this.CanSell())
      GameSection.ChangeEvent("NOT_SELL");
    GameSection.SetEventData((object) this.data);
  }

  private bool CanSell() => this.data != null && this.data.CanSale();

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (GameSection.NOTIFY_FLAG) 0)
    {
      ItemInfo itemInfo = MonoBehaviourSingleton<InventoryManager>.I.itemInventory.Find(this.data.GetUniqID());
      if (itemInfo != null && itemInfo.num > 0)
      {
        this.data = (SortCompareData) new ItemSortData();
        this.data.SetItem((object) itemInfo);
      }
    }
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY;
  }

  protected enum UI
  {
    OBJ_DETAIL_ROOT,
    BTN_DETAIL_SELL,
    TEX_MODEL,
    OBJ_ICON_ROOT,
    LBL_NAME,
    LBL_SELL,
    LBL_HAVE_NUM,
    LBL_DESCRIPTION,
    STR_SELL,
    OBJ_BACK,
    BTN_USE,
  }
}
