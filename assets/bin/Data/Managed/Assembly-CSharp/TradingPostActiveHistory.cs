// Decompiled with JetBrains decompiler
// Type: TradingPostActiveHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TradingPostActiveHistory : GameSection
{
  private TradingPostActiveHistory.VIEW_TYPE _viewType;
  private TradingPostTransactionLog loginfo;
  private TradingPostTransactionLog.ActiveLog currentLog;

  public override void Initialize()
  {
    if (GameSection.GetEventData() is 1)
    {
      this._viewType = TradingPostActiveHistory.VIEW_TYPE.HISTORY;
      if (MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSoldNum > 0)
        MonoBehaviourSingleton<TradingPostManager>.I.RemoveTradingPostSoldCount();
      if (TradingPostManager.IsNewTradingPostSold())
        MonoBehaviourSingleton<TradingPostManager>.I.SaveTradingPostLastSoldTime();
    }
    this.StartCoroutine(this.DoInitialize());
    base.Initialize();
  }

  private IEnumerator DoInitialize()
  {
    bool isRequestDone = false;
    MonoBehaviourSingleton<TradingPostManager>.I.SendRequestLogInfo((Action<bool, TradingPostTransactionLog>) ((isSuccess, ret) =>
    {
      if (!isSuccess)
        return;
      isRequestDone = true;
      if (this.loginfo != null)
        this.loginfo = (TradingPostTransactionLog) null;
      this.loginfo = ret;
      this.currentLog = (TradingPostTransactionLog.ActiveLog) null;
    }));
    while (isRequestDone)
      yield return (object) null;
  }

  public override void UpdateUI()
  {
    List<TradingPostTransactionLog.ActiveLog> showLogs = this._viewType == TradingPostActiveHistory.VIEW_TYPE.ACTIVE ? this.loginfo.activeList : this.loginfo.historyList;
    this.SetActive((Enum) TradingPostActiveHistory.UI.OBJ_ON_TAB_ACTIVE, this._viewType == TradingPostActiveHistory.VIEW_TYPE.ACTIVE);
    this.SetActive((Enum) TradingPostActiveHistory.UI.OBJ_ON_TAB_HISTORY, this._viewType == TradingPostActiveHistory.VIEW_TYPE.HISTORY);
    this.SetGrid((Enum) TradingPostActiveHistory.UI.GRD_POST, "TradingPostListLogItem", showLogs.Count, true, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      TradingPostTransactionLog.ActiveLog activeLog = showLogs[i];
      this.SetLabelText(t, (Enum) TradingPostActiveHistory.UI.LBL_NAME, Singleton<ItemTable>.I.GetItemData((uint) activeLog.itemId).name);
      this.SetLabelText(t, (Enum) TradingPostActiveHistory.UI.LBL_QUATITY, (object) activeLog.quantity);
      this.SetLabelText(t, (Enum) TradingPostActiveHistory.UI.LBL_PRICE, (object) activeLog.price);
      this.SetLabelText(t, (Enum) TradingPostActiveHistory.UI.LBL_TRANSACTION, string.Format(this.sectionData.GetText("STR_TRANSACTION_ID"), (object) activeLog.transactionId));
      string text;
      if (this._viewType == TradingPostActiveHistory.VIEW_TYPE.ACTIVE)
      {
        text = TimeManager.GetRemainTimeToText(activeLog.expiredTime, 2);
        this.SetLabelText(t, (Enum) TradingPostActiveHistory.UI.LBL_STATUS_TEXT, this.sectionData.GetText("STR_EXPIRED"));
        this.SetEvent(t, "REMOVE", i);
      }
      else
      {
        this.SetEvent(t, "", i);
        DateTime result;
        if (DateTime.TryParse(activeLog.createdAt, out result))
        {
          text = TimeManager.GetRemainTimeToText(TimeManager.GetNow() - result, 2);
        }
        else
        {
          text = "Can not parse time";
          Debug.LogError((object) text);
        }
        this.SetLabelText(t, (Enum) TradingPostActiveHistory.UI.LBL_STATUS_TEXT, this.sectionData.GetText("STR_STATUS_" + (object) activeLog.status));
      }
      this.SetLabelText(t, (Enum) TradingPostActiveHistory.UI.LBL_DAY, text);
      ItemInfo itemInfo = ItemInfo.CreateItemInfo(activeLog.itemId);
      ItemSortData data = new ItemSortData();
      data.SetItem((object) itemInfo);
      this.SetItemIcon(this.FindCtrl(t, (Enum) TradingPostActiveHistory.UI.OBJ_ICON), data);
    }));
  }

  private void OnQuery_REMOVE()
  {
    int eventData = (int) GameSection.GetEventData();
    this.currentLog = (this._viewType == TradingPostActiveHistory.VIEW_TYPE.ACTIVE ? this.loginfo.activeList : this.loginfo.historyList)[eventData];
  }

  private void OnQuery_TradingPostRemoveLogConfirm_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<TradingPostManager>.I.SendRequestRemoveTransaction(this.currentLog.transactionId, (Action<bool, Error>) ((isSuccess, ret) =>
    {
      if (isSuccess)
      {
        this.loginfo.activeList.Remove(this.currentLog);
        this.currentLog = (TradingPostTransactionLog.ActiveLog) null;
        this.RefreshUI();
      }
      GameSection.ResumeEvent(isSuccess);
    }));
  }

  private void OnQuery_ACTIVE()
  {
    if (this._viewType == TradingPostActiveHistory.VIEW_TYPE.ACTIVE)
      return;
    this._viewType = TradingPostActiveHistory.VIEW_TYPE.ACTIVE;
    this.RefreshUI();
  }

  private void OnQuery_HISTORY()
  {
    if (this._viewType == TradingPostActiveHistory.VIEW_TYPE.HISTORY)
      return;
    this._viewType = TradingPostActiveHistory.VIEW_TYPE.HISTORY;
    this.RefreshUI();
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
        this.SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), this.GetCtrl((Enum) TradingPostActiveHistory.UI.PNL_MATERIAL_INFO));
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

  private enum VIEW_TYPE
  {
    ACTIVE,
    HISTORY,
  }

  private enum UI
  {
    OBJ_ON_TAB_ACTIVE,
    OBJ_ON_TAB_HISTORY,
    SCR_POST,
    GRD_POST,
    PNL_MATERIAL_INFO,
    LBL_TRANSACTION,
    LBL_NAME,
    LBL_STATUS_TEXT,
    LBL_DAY,
    LBL_PRICE,
    LBL_QUATITY,
    OBJ_ICON,
  }
}
