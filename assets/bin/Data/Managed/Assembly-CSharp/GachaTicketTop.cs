// Decompiled with JetBrains decompiler
// Type: GachaTicketTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GachaTicketTop : GameSection
{
  private int nowPage = 1;
  private int pageMax = 1;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) GachaTicketTop.UI.STR_TITLE, StringTable.Get(STRING_CATEGORY.COMMON, 103U));
    this.SetLabelText((Enum) GachaTicketTop.UI.STR_TITLE_REFLECT, StringTable.Get(STRING_CATEGORY.COMMON, 103U));
    ExpiredItem[] showList = this.GetItemList(MonoBehaviourSingleton<InventoryManager>.I.GetItemList((Predicate<ItemInfo>) (x => x.tableData.type == ITEM_TYPE.TICKET))).ToArray();
    ((Component) this.GetCtrl((Enum) GachaTicketTop.UI.LBL_CAUTION)).GetComponent<UILabel>().supportEncoding = true;
    this.SetLabelText((Enum) GachaTicketTop.UI.LBL_CAUTION, StringTable.Get(STRING_CATEGORY.SHOP, 14U));
    this.SetActive((Enum) GachaTicketTop.UI.BTN_TO_GACHA, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "ShopScene");
    this.SetActive((Enum) GachaTicketTop.UI.GRD_LIST, showList.Length != 0);
    this.SetActive((Enum) GachaTicketTop.UI.STR_ORDER_NON_LIST, showList.Length == 0);
    this.SetActive((Enum) GachaTicketTop.UI.OBJ_ACTIVE_ROOT, showList.Length != 0);
    this.SetActive((Enum) GachaTicketTop.UI.OBJ_INACTIVE_ROOT, showList.Length == 0);
    if (showList.Length == 0)
    {
      this.SetLabelText((Enum) GachaTicketTop.UI.LBL_MAX, "0");
      this.SetLabelText((Enum) GachaTicketTop.UI.LBL_NOW, "0");
      UIScrollView component = ((Component) this.GetCtrl((Enum) GachaTicketTop.UI.SCR_LIST)).GetComponent<UIScrollView>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Behaviour) component).enabled = false;
      component.verticalScrollBar.alpha = 0.0f;
    }
    else
    {
      this.pageMax = 1 + (showList.Length - 1) / 10;
      bool is_visible = this.pageMax > 1;
      this.SetActive((Enum) GachaTicketTop.UI.OBJ_ACTIVE_ROOT, is_visible);
      this.SetActive((Enum) GachaTicketTop.UI.OBJ_INACTIVE_ROOT, !is_visible);
      this.SetLabelText((Enum) GachaTicketTop.UI.LBL_MAX, this.pageMax.ToString());
      this.SetLabelText((Enum) GachaTicketTop.UI.LBL_NOW, this.nowPage.ToString());
      int sourceIndex = 10 * (this.nowPage - 1);
      int length = this.nowPage == this.pageMax ? showList.Length - sourceIndex : 10;
      ExpiredItem[] destinationArray = new ExpiredItem[length];
      Array.Copy((Array) showList, sourceIndex, (Array) destinationArray, 0, length);
      showList = destinationArray;
      this.SetGrid((Enum) GachaTicketTop.UI.GRD_LIST, "GachaTicketListItem", showList.Length, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        ExpiredItem expiredItem = showList[i];
        ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) expiredItem.itemId);
        ResourceLoad.LoadItemIconTexture(((Component) this.FindCtrl(t, (Enum) GachaTicketTop.UI.TEX_ICON)).GetComponent<UITexture>(), itemData.iconID);
        this.SetLabelText(t, (Enum) GachaTicketTop.UI.LBL_NAME, itemData.name);
        string text1;
        string text2;
        if (string.IsNullOrEmpty(expiredItem.expiredAt))
        {
          text1 = "-";
          text2 = "-";
        }
        else
        {
          text1 = expiredItem.expiredAt;
          text2 = TimeManager.GetRemainTimeToText(expiredItem.expiredAt, 1);
        }
        this.SetLabelText(t, (Enum) GachaTicketTop.UI.LBL_LIMIT, text1);
        this.SetLabelText(t, (Enum) GachaTicketTop.UI.LBL_COUNTDOWN, text2);
      }));
    }
  }

  private List<ExpiredItem> GetItemList(List<ItemInfo> list)
  {
    List<ExpiredItem> itemList = new List<ExpiredItem>();
    foreach (ItemInfo itemInfo in list)
    {
      if (itemInfo.expiredAtItem != null)
      {
        foreach (ExpiredItem expiredItem in itemInfo.expiredAtItem)
        {
          if (expiredItem.CanUse())
            itemList.Add(expiredItem);
        }
      }
    }
    itemList.Sort((Comparison<ExpiredItem>) ((a, b) => a.expiredAt.CompareTo(b.expiredAt)));
    return itemList;
  }

  private void OnQuery_CAUTION() => GameSection.SetEventData((object) WebViewManager.GachaTicket);

  private void OnQuery_PAGE_PREV()
  {
    this.nowPage = this.nowPage > 1 ? this.nowPage - 1 : this.pageMax;
    this.RefreshUI();
  }

  private void OnQuery_PAGE_NEXT()
  {
    this.nowPage = this.nowPage < this.pageMax ? this.nowPage + 1 : 1;
    this.RefreshUI();
  }

  private enum UI
  {
    STR_TITLE,
    STR_TITLE_REFLECT,
    BTN_TO_GACHA,
    SCR_LIST,
    GRD_LIST,
    LBL_NOW,
    LBL_MAX,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    BTN_PAGE_NEXT,
    BTN_PAGE_PREV,
    STR_ORDER_NON_LIST,
    LBL_CAUTION,
    TEX_ICON,
    LBL_NAME,
    LBL_LIMIT,
    LBL_COUNTDOWN,
  }
}
