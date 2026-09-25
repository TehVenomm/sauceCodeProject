// Decompiled with JetBrains decompiler
// Type: GuildDonateInvitationList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GuildDonateInvitationList : GameSection
{
  private List<DonateInvitationInfo> _donateList;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    bool finish_donate_list = false;
    MonoBehaviourSingleton<GuildManager>.I.SendDonateInvitationList((Action<bool>) (success =>
    {
      finish_donate_list = true;
      this._donateList = MonoBehaviourSingleton<GuildManager>.I.donateInviteList;
    }));
    while (!finish_donate_list)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) GuildDonateInvitationList.UI.STR_NON_LIST, this._donateList.Count <= 0);
    this.SetTable((Enum) GuildDonateInvitationList.UI.TBL_QUEST, "GuildDonateInvitationListItem", this._donateList.Count, true, (Func<int, Transform, Transform>) ((i, t) => (Transform) null), (Action<int, Transform, bool>) ((i, t, b) =>
    {
      DonateInvitationInfo info = this._donateList[i];
      int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) info.itemId), 1);
      bool is_visible = info.itemNum >= info.quantity;
      this.SetActive(t, (Enum) GuildDonateInvitationList.UI.OBJ_FULL, is_visible);
      this.SetActive(t, (Enum) GuildDonateInvitationList.UI.OBJ_NORMAL, !is_visible);
      this.SetLabelText((Enum) GuildDonateInvitationList.UI.LBL_CHAT_MESSAGE, info.msg);
      this.SetLabelText((Enum) GuildDonateInvitationList.UI.LBL_USER_NAME, info.nickName);
      this.SetLabelText((Enum) GuildDonateInvitationList.UI.LBL_MATERIAL_NAME, info.itemName);
      this.SetLabelText((Enum) GuildDonateInvitationList.UI.LBL_QUATITY, (object) itemNum);
      this.SetLabelText((Enum) GuildDonateInvitationList.UI.LBL_DONATE_NUM, (object) info.itemNum);
      this.SetLabelText((Enum) GuildDonateInvitationList.UI.LBL_DONATE_MAX, (object) info.quantity);
      this.SetSliderValue((Enum) GuildDonateInvitationList.UI.SLD_PROGRESS, (float) info.itemNum / (float) info.quantity);
      if (!is_visible && itemNum > 0 && info.itemNum < info.quantity)
        this.SetButtonEvent(t, (Enum) GuildDonateInvitationList.UI.BTN_GIFT, new EventDelegate((EventDelegate.Callback) (() => this.DispatchEvent("SEND", (object) info.ParseDonateInfo()))));
      else
        this.SetButtonEnabled(t, (Enum) GuildDonateInvitationList.UI.BTN_GIFT, false);
      Transform ctrl = this.GetCtrl((Enum) GuildDonateInvitationList.UI.OBJ_MATERIAL_ICON);
      ItemInfo itemInfo = ItemInfo.CreateItemInfo(new Network.Item()
      {
        uniqId = "0",
        itemId = info.itemId,
        num = info.itemNum
      });
      ItemSortData data = new ItemSortData();
      data.SetItem((object) itemInfo);
      this.SetItemIcon(ctrl, data, ctrl);
    }));
  }

  private void OnQuery_RELOAD()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendDonateInvitationList((Action<bool>) (success =>
    {
      this._donateList = MonoBehaviourSingleton<GuildManager>.I.donateInviteList;
      GameSection.ResumeEvent(false);
      this.RefreshUI();
    }));
  }

  private void SetItemIcon(Transform holder, ItemSortData data, Transform parent_scroll)
  {
    ITEM_ICON_TYPE itemIconType = ITEM_ICON_TYPE.NONE;
    RARITY_TYPE? rarity = new RARITY_TYPE?();
    ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
    EQUIPMENT_TYPE? magi_enable_icon_type = new EQUIPMENT_TYPE?();
    int icon_id = -1;
    if (data != null)
    {
      itemIconType = data.GetIconType();
      icon_id = data.GetIconID();
      rarity = new RARITY_TYPE?(data.GetRarity());
      element = data.GetIconElement();
      magi_enable_icon_type = data.GetIconMagiEnableType();
      data.GetNum();
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
          itemIcon = ItemIcon.Create(itemIconType, icon_id, rarity, holder, element, magi_enable_icon_type, event_name: "DROP", is_new: is_new, enemy_icon_id: enemy_icon_id);
        this.SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), parent_scroll);
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
    SCR_QUEST,
    TBL_QUEST,
    STR_NON_LIST,
    LBL_USER_NAME,
    LBL_CHAT_MESSAGE,
    LBL_MATERIAL_NAME,
    SLD_PROGRESS,
    OBJ_MATERIAL_ICON,
    LBL_QUATITY,
    OBJ_FULL,
    OBJ_NORMAL,
    LBL_DONATE_NUM,
    LBL_DONATE_MAX,
    BTN_GIFT,
  }
}
