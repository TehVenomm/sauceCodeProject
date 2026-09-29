// Decompiled with JetBrains decompiler
// Type: SmithGuildRequestDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class SmithGuildRequestDialog : GameSection
{
  private int needNum;
  private UIInput m_Input;
  private SortCompareData data;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.data = eventData[0] as SortCompareData;
    this.needNum = (int) eventData[1];
    this.m_Input = ((Component) this.GetCtrl((Enum) SmithGuildRequestDialog.UI.IPT_POST)).GetComponent<UIInput>();
    base.Initialize();
  }

  public override void StartSection()
  {
    ((Component) this.m_Input).SendMessage("OnSelect", (object) true);
  }

  public override void UpdateUI()
  {
    this.SetInputSubmitEvent((Enum) SmithGuildRequestDialog.UI.IPT_POST, new EventDelegate((EventDelegate.Callback) (() => this.OnTouchPost())));
    this.SetActive((Enum) SmithGuildRequestDialog.UI.OBJ_TARGET, true);
    this.SetActive((Enum) SmithGuildRequestDialog.UI.OBJ_OWNER, false);
    this.SetInputValue((Enum) SmithGuildRequestDialog.UI.IPT_POST, this.sectionData.GetText("TEXT_HELP"));
    this.SetInputLabel((Enum) SmithGuildRequestDialog.UI.IPT_POST, this.sectionData.GetText("TEXT_HELP"));
    this.SetLabelText((Enum) SmithGuildRequestDialog.UI.LBL_USER_NAME, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
    this.SetLabelText((Enum) SmithGuildRequestDialog.UI.LBL_MATERIAL_NAME, this.data.GetName());
    this.SetLabelText((Enum) SmithGuildRequestDialog.UI.LBL_QUATITY, (object) this.data.GetNum());
    this.SetLabelText((Enum) SmithGuildRequestDialog.UI.LBL_DONATE_NUM, (object) this.data.GetNum());
    this.SetLabelText((Enum) SmithGuildRequestDialog.UI.LBL_DONATE_MAX, (object) this.needNum);
    this.SetButtonEnabled((Enum) SmithGuildRequestDialog.UI.BTN_GIFT, false);
    this.SetSliderValue((Enum) SmithGuildRequestDialog.UI.SLD_PROGRESS, (float) this.data.GetNum() / (float) this.needNum);
    Transform ctrl = this.GetCtrl((Enum) SmithGuildRequestDialog.UI.OBJ_MATERIAL_ICON);
    ItemInfo itemInfo = ItemInfo.CreateItemInfo(new Network.Item()
    {
      uniqId = "0",
      itemId = (int) this.data.GetTableID(),
      num = this.data.GetNum()
    });
    ItemSortData data = new ItemSortData();
    data.SetItem((object) itemInfo);
    this.SetItemIcon(ctrl, data, ctrl);
  }

  private void OnTouchPost()
  {
    this.SetLabelText((Enum) SmithGuildRequestDialog.UI.LBL_CHAT_MESSAGE, this.m_Input.value);
  }

  private void OnQuery_SUBMIT()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendDonateRequest((int) this.data.GetTableID(), this.data.GetName(), this.GetLabelText((Enum) SmithGuildRequestDialog.UI.LBL_CHAT_MESSAGE), this.needNum - this.data.GetNum(), (Action<bool>) (success =>
    {
      if (success)
        this.RequestEvent("CHAT", (object) GuildMessage.VIEW_TYPE.DONATE);
      GameSection.ResumeEvent(success);
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
    IPT_POST,
    LBL_USER_NAME,
    LBL_CHAT_MESSAGE,
    OBJ_TARGET,
    OBJ_OWNER,
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
