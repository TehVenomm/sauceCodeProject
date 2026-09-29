// Decompiled with JetBrains decompiler
// Type: ItemDetailEquipSetExt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ItemDetailEquipSetExt : GameSection
{
  private SortCompareData data;
  private static object[] equipSetExtEventData;

  public override void Initialize()
  {
    this.data = GameSection.GetEventData() as SortCompareData;
    GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.USE_ITEM, this.data.GetUniqID());
    base.Initialize();
  }

  public override void UpdateUI()
  {
    ItemInfo itemData = this.data.GetItemData() as ItemInfo;
    this.SetRenderItemModel((Enum) ItemDetailEquipSetExt.UI.TEX_MODEL, itemData.tableID);
    this.SetActive((Enum) ItemDetailEquipSetExt.UI.STR_SELL, this.data.CanSale());
    this.SetActive((Enum) ItemDetailEquipSetExt.UI.BTN_DETAIL_SELL, this.data.CanSale() && MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
    this.SetLabelText((Enum) ItemDetailEquipSetExt.UI.LBL_NAME, this.data.GetName());
    this.SetLabelText((Enum) ItemDetailEquipSetExt.UI.LBL_HAVE_NUM, this.data.GetNum().ToString());
    this.SetLabelText((Enum) ItemDetailEquipSetExt.UI.LBL_DESCRIPTION, itemData.tableData.text);
    this.SetLabelText((Enum) ItemDetailEquipSetExt.UI.LBL_SELL, this.data.GetSalePrice().ToString());
    int enemyIconId = itemData.tableData.enemyIconID;
    int enemyIconId2 = itemData.tableData.enemyIconID2;
    ItemIcon.Create(this.data.GetIconType(), this.data.GetIconID(), new RARITY_TYPE?(this.data.GetRarity()), this.GetCtrl((Enum) ItemDetailEquipSetExt.UI.OBJ_ICON_ROOT), this.data.GetIconElement(), this.data.GetIconMagiEnableType(), enemy_icon_id: enemyIconId, enemy_icon_id2: enemyIconId2, getType: this.data.GetGetType());
  }

  public void OnQuery_USE()
  {
    ItemDetailEquipSetExt.equipSetExtEventData = (object[]) null;
    ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
    if (MonoBehaviourSingleton<StatusManager>.I.EquipSetNum() >= constDefine.EQUIP_SET_EXT_MAX)
    {
      GameSection.ChangeEvent("OVER");
    }
    else
    {
      int num1 = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
      int num2 = num1 + constDefine.INVENTORY_EXTEND_EQUIP_SET;
      ItemDetailEquipSetExt.equipSetExtEventData = new object[3]
      {
        (object) this.data.GetName(),
        (object) num1.ToString(),
        (object) num2.ToString()
      };
      GameSection.SetEventData((object) ItemDetailEquipSetExt.equipSetExtEventData);
      GameSection.ChangeEvent("EXTEND");
    }
  }

  protected void OnQuery_ItemDetailEquipSetExtConfirm_YES()
  {
    if (ItemDetailEquipSetExt.equipSetExtEventData == null)
    {
      Log.Error(LOG.OUTGAME, "EQUIP_SET_EXT data is NULL");
      GameSection.StopEvent();
    }
    else
    {
      GameSection.SetEventData((object) ItemDetailEquipSetExt.equipSetExtEventData);
      GameSection.StayEvent();
      MonoBehaviourSingleton<InventoryManager>.I.SendInventoryEquipSetExt(this.data.GetUniqID().ToString(), (Action<bool>) (is_success =>
      {
        if (is_success)
        {
          if (MonoBehaviourSingleton<StatusManager>.IsValid())
            MonoBehaviourSingleton<StatusManager>.I.ResetEquipSetInfo();
          MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO);
        }
        GameSection.ResumeEvent(is_success);
      }));
    }
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
