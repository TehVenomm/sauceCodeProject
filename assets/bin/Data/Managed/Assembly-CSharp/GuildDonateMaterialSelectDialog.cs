// Decompiled with JetBrains decompiler
// Type: GuildDonateMaterialSelectDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GuildDonateMaterialSelectDialog : GameSection
{
  private int[] materialList = new int[46]
  {
    1000007,
    1000008,
    1000009,
    1000010,
    1000011,
    1000012,
    1000013,
    1000014,
    1000015,
    1000004,
    2010002,
    2010003,
    2010004,
    2010005,
    2010006,
    2010102,
    2010103,
    2010104,
    2010105,
    2010106,
    2010202,
    2010203,
    2010204,
    2010205,
    2010206,
    2010302,
    2010303,
    2010304,
    2010305,
    2010306,
    2010402,
    2010403,
    2010404,
    2010405,
    2010406,
    2010502,
    2010503,
    2010504,
    2010505,
    2010506,
    2010000,
    2010100,
    2010200,
    2010300,
    2010400,
    2010500
  };
  private List<ItemInfo> itemList = new List<ItemInfo>();
  private ItemStorageTop.MaterialInventory inventory;
  private int chooseIndex = -1;
  private bool backSection;

  public override void Initialize()
  {
    this.inventory = new ItemStorageTop.MaterialInventory(true, false, false);
    this.itemList.Clear();
    Array.ForEach<int>(this.materialList, (Action<int>) (id =>
    {
      ItemInfo itemInfo = new ItemInfo()
      {
        tableID = (uint) id
      };
      itemInfo.tableData = Singleton<ItemTable>.I.GetItemData(itemInfo.tableID);
      itemInfo.num = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) id), 1);
      this.itemList.Add(itemInfo);
    }));
    this.SetLabelText((Enum) GuildDonateMaterialSelectDialog.UI.LBL_NUMBER_REQUEST, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 34U), (object) MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateCap, (object) MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap));
    this.SetSupportEncoding((Enum) GuildDonateMaterialSelectDialog.UI.LBL_NUMBER_REQUEST, true);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetDynamicList((Enum) GuildDonateMaterialSelectDialog.UI.GRD_INVENTORY, "GuildDonateMaterialItem", this.itemList.Count, false, (Func<int, bool>) (i => true), (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycre) =>
    {
      this.SetSprite(t, (Enum) GuildDonateMaterialSelectDialog.UI.SPR_RARITY_TEXT_ICON, ItemIcon.ITEM_ICON_ITEM_RARITY_ICON_SPRITE[(int) this.itemList[i].tableData.rarity]);
      this.SetSprite(t, (Enum) GuildDonateMaterialSelectDialog.UI.SPR_RARITY, ItemIcon.ITEM_ICON_EQUIP_RARITY_FRAME_SPRITE[(int) this.itemList[i].tableData.rarity]);
      this.SetLabelText(t, (Enum) GuildDonateMaterialSelectDialog.UI.LBL_NAME, this.itemList[i].tableData.name);
      this.SetLabelText(t, (Enum) GuildDonateMaterialSelectDialog.UI.lbl_item_num, this.itemList[i].GetNum().ToString());
      ResourceLoad.LoadIconTexture((MonoBehaviour) this, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(this.itemList[i].tableData.iconID), (System.Action) null, (Action<Texture>) (tex => this.SetTexture(t, (Enum) GuildDonateMaterialSelectDialog.UI.ICON, tex)));
      ResourceLoad.LoadIconTexture((MonoBehaviour) this, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(ItemIcon.GetIconBGID(ITEM_ICON_TYPE.ITEM, this.itemList[i].tableData.iconID, new RARITY_TYPE?(this.itemList[i].tableData.rarity))), (System.Action) null, (Action<Texture>) (tex => this.SetTexture(t, (Enum) GuildDonateMaterialSelectDialog.UI.icon_bg, tex)));
      this.SetEvent(t, "CHOSE_MATERIAL", i);
    }));
  }

  private void OnQuery_CHOSE_MATERIAL()
  {
    this.chooseIndex = (int) GameSection.GetEventData();
    GameSection.ChangeEvent("OPEN_SEND_DIALOG");
  }

  private void OnQuery_ARMORY()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) SmithEquipBase.SmithType.GROW,
      (object) EQUIPMENT_TYPE.ONE_HAND_SWORD
    });
  }

  private void OnCloseDialog_GuildDonateSendDialog()
  {
    string eventData = GameSection.GetEventData() as string;
    try
    {
      int numRequest = int.Parse(eventData);
      if (numRequest > 0)
      {
        if (this.chooseIndex >= 0)
        {
          this.StartCoroutine(this.CRSendDonateRequest((int) this.itemList[this.chooseIndex].tableData.id, this.itemList[this.chooseIndex].tableData.name, "", numRequest));
          this.chooseIndex = -1;
        }
      }
    }
    catch
    {
    }
    this.chooseIndex = -1;
  }

  private IEnumerator CRSendDonateRequest(
    int itemID,
    string itemName,
    string request,
    int numRequest)
  {
    yield return (object) new WaitUntil((Func<bool>) (() => !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible()));
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendDonateRequest(itemID, itemName, request, numRequest, (Action<bool>) (success =>
    {
      GameSection.ResumeEvent(success);
      if (!success)
        return;
      this.backSection = true;
    }));
  }

  private void Update()
  {
    if (!this.backSection || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      return;
    this.backSection = false;
    GameSection.BackSection();
  }

  private enum UI
  {
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    TGL_CHANGE_INVENTORY,
    TGL_ICON_ASC,
    LBL_SORT,
    BTN_SORT,
    SPR_INVALID_SORT,
    LBL_INVALID_SORT,
    OBJ_CAPTION_3,
    LBL_CAPTION,
    SPR_RARITY_TEXT_ICON,
    LBL_NAME,
    lbl_item_num,
    ICON,
    SPR_RARITY,
    icon_bg,
    LBL_NUMBER_REQUEST,
  }
}
