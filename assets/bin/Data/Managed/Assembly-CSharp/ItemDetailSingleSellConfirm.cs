// Decompiled with JetBrains decompiler
// Type: ItemDetailSingleSellConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ItemDetailSingleSellConfirm : GameSection
{
  private SortCompareData item;
  private int num;
  private int price;
  private ItemDetailEquip.CURRENT_SECTION? callSection;
  private int? setNo;
  private bool is_exchange;

  public override string overrideBackKeyEvent => "NO";

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.item = eventData[0] as SortCompareData;
    this.num = (int) eventData[1];
    this.price = (int) eventData[2];
    this.callSection = eventData[3] is ItemDetailEquip.CURRENT_SECTION ? eventData[3] as ItemDetailEquip.CURRENT_SECTION? : new ItemDetailEquip.CURRENT_SECTION?();
    this.setNo = eventData[4] is int ? (int?) eventData[4] : new int?();
    this.is_exchange = false;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) ItemDetailSingleSellConfirm.UI.LBL_ITEM_NAME, this.item.GetName());
    this.SetLabelText((Enum) ItemDetailSingleSellConfirm.UI.LBL_TOTAL, $"{this.price:N0}");
    int enemy_icon_id = 0;
    int enemy_icon_id2 = 0;
    if (this.item is ItemSortData)
    {
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(this.item.GetTableID());
      if (itemData != null)
      {
        enemy_icon_id = itemData.enemyIconID;
        enemy_icon_id2 = itemData.enemyIconID2;
      }
    }
    ItemIcon.Create(this.item.GetIconType(), this.item.GetIconID(), new RARITY_TYPE?(this.item.GetRarity()), this.GetCtrl((Enum) ItemDetailSingleSellConfirm.UI.OBJ_ICON_ROOT), this.item.GetIconElement(), this.item.GetIconMagiEnableType(), this.num, event_data: -1, enemy_icon_id: enemy_icon_id, enemy_icon_id2: enemy_icon_id2, getType: this.item.GetGetType()).SetRewardBG(true);
    base.UpdateUI();
  }

  private void sellConfirm(Action<bool> callback)
  {
    if (!this.setNo.HasValue || !this.callSection.HasValue)
    {
      Debug.LogWarning((object) $"data = null : setNo =null? {(!this.setNo.HasValue).ToString()} : callsection=null? {(!this.callSection.HasValue).ToString()}");
      callback(false);
    }
    else
    {
      switch (this.callSection.Value)
      {
        case ItemDetailEquip.CURRENT_SECTION.STATUS_TOP:
        case ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP:
        case ItemDetailEquip.CURRENT_SECTION.STATUS_AVATAR:
          MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquip(this.setNo.Value, (Action<bool>) (is_success =>
          {
            if (callback == null)
              return;
            callback(is_success);
          }));
          break;
        default:
          if (callback == null)
            break;
          callback(true);
          break;
      }
    }
  }

  public void OnQuery_YES()
  {
    if (GameDefine.IsRequiredAlertByRarity(this.item.GetRarity()))
    {
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.item.GetRarity().ToString()
      });
      GameSection.ChangeEvent("INCLUDE_RARE_CONFIRM");
    }
    else
      this.PrepareForSellItem();
  }

  protected void PrepareForSellItem()
  {
    if (this.num >= this.item.GetNum())
      GameSection.ChangeEvent("CLOSE_DETAIL");
    if (this.item is ItemSortData)
    {
      GameSection.StayEvent();
      this.SendItem((Action<bool>) (b => GameSection.ResumeEvent(b)));
    }
    else if (this.item is EquipItemSortData)
    {
      GameSection.StayEvent();
      this.sellConfirm((Action<bool>) (b =>
      {
        if (!b)
        {
          Debug.LogWarning((object) "sellConfirm = false");
          GameSection.ResumeEvent(false);
        }
        else
        {
          GameSection.ChangeStayEvent("NON_STACK_SELL");
          this.SendEquip(new List<string>()
          {
            this.item.GetUniqID().ToString()
          }, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
        }
      }));
    }
    else if (this.item is SkillItemSortData)
    {
      GameSection.ChangeEvent("NON_STACK_SELL");
      List<string> uniqs = new List<string>();
      uniqs.Add(this.item.GetUniqID().ToString());
      GameSection.StayEvent();
      this.SendSkill(uniqs, (Action<bool>) (b => GameSection.ResumeEvent(b)));
    }
    else
    {
      if (!(this.item is AbilityItemSortData))
        return;
      GameSection.StayEvent();
      this.SendAbilityItem(new List<string>()
      {
        this.item.GetUniqID().ToString()
      }, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    }
  }

  private void SendItem(Action<bool> callback)
  {
    List<string> uids = new List<string>();
    List<int> nums = new List<int>();
    uids.Add(this.item.GetUniqID().ToString());
    nums.Add(this.num);
    MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellItem(uids, nums, (Action<bool>) (is_success =>
    {
      if (callback == null)
        return;
      callback(is_success);
    }));
  }

  private void SendEquip(List<string> uniqs, Action<bool> callback)
  {
    MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellEquipItem(uniqs, (Action<bool>) (is_success =>
    {
      if (callback == null)
        return;
      callback(is_success);
    }));
  }

  private void SendSkill(List<string> uniqs, Action<bool> callback)
  {
    if (this.is_exchange)
    {
      GameSection.StopEvent();
      GameSection.ResumeEvent(false);
    }
    else
      MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellSkillItem(uniqs, (Action<bool>) (is_success =>
      {
        if (callback == null)
          return;
        callback(is_success);
      }));
  }

  private void SendAbilityItem(List<string> uniqs, Action<bool> callback)
  {
    MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellAbilityItem(uniqs, (Action<bool>) (is_success =>
    {
      if (callback == null)
        return;
      callback(is_success);
    }));
  }

  public void OnQuery_ItemDetailConfirmSellHighRareItem_YES() => this.PrepareForSellItem();

  public void OnQuery_ItemDetailConfirmSellHighRareItem_NO()
  {
  }

  private enum UI
  {
    LBL_ITEM_NAME,
    LBL_TOTAL,
    OBJ_ICON_ROOT,
  }
}
