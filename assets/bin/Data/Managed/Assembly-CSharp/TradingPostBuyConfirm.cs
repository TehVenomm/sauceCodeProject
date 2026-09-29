// Decompiled with JetBrains decompiler
// Type: TradingPostBuyConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class TradingPostBuyConfirm : GameSection
{
  private TradingPostDetail detail;
  private int i;

  public override void Initialize()
  {
    this.detail = GameSection.GetEventData() as TradingPostDetail;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) TradingPostBuyConfirm.UI.LBL_COST, (object) this.detail.price);
    this.SetLabelText((Enum) TradingPostBuyConfirm.UI.LBL_COST_D, (object) this.detail.price);
    this.SetLabelText((Enum) TradingPostBuyConfirm.UI.LBL_GEM, (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal);
    this.SetLabelText((Enum) TradingPostBuyConfirm.UI.LBL_QUATITY, (object) this.detail.quantity);
    this.SetLabelText((Enum) TradingPostBuyConfirm.UI.LBL_NAME, this.detail.from);
    ItemInfo itemInfo = ItemInfo.CreateItemInfo(this.detail.itemId);
    ItemSortData data = new ItemSortData();
    data.SetItem((object) itemInfo);
    this.SetItemIcon(this.GetCtrl((Enum) TradingPostBuyConfirm.UI.OBJ_ICON), data);
  }

  private void OnQuery_YES()
  {
    GameSection.ChangeEvent("[BACK]");
    this.RequestEvent("PURCHASE");
  }

  private void SetItemIcon(Transform holder, ItemSortData data, int event_data = 0)
  {
    ITEM_ICON_TYPE itemIconType = ITEM_ICON_TYPE.NONE;
    RARITY_TYPE? rarity = new RARITY_TYPE?();
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
    EQUIPMENT_TYPE? magi_enable_icon_type = new EQUIPMENT_TYPE?();
    int icon_id = -1;
    int num = -1;
    if (data != null)
    {
      itemIconType = data.GetIconType();
      icon_id = data.GetIconID();
      rarity = new RARITY_TYPE?(data.GetRarity());
      element = data.GetIconElement();
      magi_enable_icon_type = data.GetIconMagiEnableType();
    }
    bool is_new = false;
    switch (itemIconType)
    {
      case ITEM_ICON_TYPE.NONE:
        int enemy_icon_id = 0;
        if (itemIconType == ITEM_ICON_TYPE.ITEM)
          enemy_icon_id = Singleton<ItemTable>.I.GetItemData(data.GetTableID()).enemyIconID;
        ItemIcon itemIcon;
        if (data.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
          itemIcon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam()
          {
            icon_type = data.GetIconType(),
            icon_id = data.GetIconID(),
            rarity = new RARITY_TYPE?(data.GetRarity()),
            parent = holder,
            element = data.GetIconElement(),
            magi_enable_equip_type = data.GetIconMagiEnableType(),
            num = data.GetNum(),
            enemy_icon_id = enemy_icon_id,
            questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
          });
        else
          itemIcon = ItemIcon.Create(itemIconType, icon_id, rarity, holder, element, magi_enable_icon_type, num, "DROP", event_data, is_new, enemy_icon_id: enemy_icon_id);
        itemIcon.SetRewardBG(false);
        this.SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), this.GetCtrl((Enum) TradingPostBuyConfirm.UI.PNL_MATERIAL_INFO));
        break;
      case ITEM_ICON_TYPE.ITEM:
      case ITEM_ICON_TYPE.QUEST_ITEM:
        if (data.GetUniqID() != 0UL)
        {
          is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(itemIconType, data.GetUniqID());
          goto case ITEM_ICON_TYPE.NONE;
        }
        goto case ITEM_ICON_TYPE.NONE;
      default:
        is_new = true;
        goto case ITEM_ICON_TYPE.NONE;
    }
  }

  private enum UI
  {
    LBL_COST,
    LBL_COST_D,
    LBL_GEM,
    LBL_QUATITY,
    LBL_NAME,
    OBJ_ICON,
    PNL_MATERIAL_INFO,
  }
}
