// Decompiled with JetBrains decompiler
// Type: TradingPostItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TradingPostItemDetail : GameSection
{
  private const string BTN_NORMAL_SPRITE = "ItemBoxExtentBtn_half";
  private const string BTN_ACTIVE_SPRITE = "ItemBoxExtentBtn2_half";
  private TradingPostItemDetail.SORT_TYPE _sortType;
  private TradingPostInfo info;
  private List<TradingPostDetail> details = new List<TradingPostDetail>();
  private TradingPostDetail currentDetail;
  private bool close;
  private int currentPage = 1;
  private bool endPage;
  private UIScrollView scrollview;
  private bool refresh;
  private bool next;
  private ItemSortData itemData;

  public override void Initialize()
  {
    this.scrollview = this.GetComponent<UIScrollView>((Enum) TradingPostItemDetail.UI.SCR_HOWTO);
    this.scrollview.onReachBottom += new UIScrollView.OnDragNotification(this.OnScrollViewReachBottom);
    this.StartCoroutine(this.DoInitialize());
  }

  protected override void OnDestroy()
  {
    if (Object.op_Inequality((Object) this.scrollview, (Object) null))
      this.scrollview.onStoppedMoving -= new UIScrollView.OnDragNotification(this.OnScrollViewReachBottom);
    base.OnDestroy();
  }

  private IEnumerator DoInitialize()
  {
    this.info = MonoBehaviourSingleton<TradingPostManager>.I.Viewinfo;
    ItemInfo itemInfo = ItemInfo.CreateItemInfo(this.info.itemId);
    this.itemData = new ItemSortData();
    this.itemData.SetItem((object) itemInfo);
    bool wait = true;
    MonoBehaviourSingleton<TradingPostManager>.I.SendRequestItemDetail(this.info.itemId, this.currentPage, (Action<bool, List<TradingPostDetail>>) ((success, list) =>
    {
      wait = false;
      this.details.AddRange((IEnumerable<TradingPostDetail>) list);
    }));
    while (wait)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_SELL_ITEM, this.sectionData.GetText("STR_SELL_ITEM"));
    this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_QTY_NORMAL, this.sectionData.GetText("TEXT_BTN_QTY"));
    this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_QTY_SELECT, this.sectionData.GetText("TEXT_BTN_QTY"));
    this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_PRICE_NORMAL, this.sectionData.GetText("TEXT_BTN_PRICE"));
    this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_PRICE_SELECT, this.sectionData.GetText("TEXT_BTN_PRICE"));
    this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_REFRESH, this.sectionData.GetText("TEXT_BTN_REFRESH"));
    bool flag = TradingPostManager.IsPurchasedLicense() || TradingPostManager.IsLoginRequireFinish();
    this.SetActive((Enum) TradingPostItemDetail.UI.OBJ_NOT_LICENSED, !flag);
    if (!flag)
    {
      this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_LOGIN, string.Format(this.sectionData.GetText("STR_LOGIN"), (object) MonoBehaviourSingleton<TradingPostManager>.I.tradingDay, (object) MonoBehaviourSingleton<TradingPostManager>.I.tradingConditionDay));
      this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_NOTICE, string.Format(this.sectionData.GetText("STR_NOTICE"), (object) MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
      this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_REQ_LOGIN, this.sectionData.GetText("STR_REQ_LOGIN"));
      this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_REQ_LICENSE, this.sectionData.GetText("STR_REQ_LICENSE"));
    }
    int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (int) x.tableID == (int) this.itemData.GetTableID()));
    bool is_visible = this.details.Count == 0;
    this.SetLabelText((Enum) TradingPostItemDetail.UI.LBL_CRYSTAL_NUM, (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal);
    Transform ctrl = this.GetCtrl((Enum) TradingPostItemDetail.UI.OBJ_DETAIL_BASE);
    this.SetItemIcon(this.FindCtrl(ctrl, (Enum) TradingPostItemDetail.UI.OBJ_ICON_ROOT), this.itemData);
    this.SetActive(ctrl, (Enum) TradingPostItemDetail.UI.SPR_NEED, false);
    this.SetActive(ctrl, (Enum) TradingPostItemDetail.UI.STR_SELL, this.itemData.CanSale());
    this.SetActive(ctrl, (Enum) TradingPostItemDetail.UI.OBJ_NOT_LICENSED, TradingPostManager.IsPurchasedLicense());
    this.SetActive(ctrl, (Enum) TradingPostItemDetail.UI.STR_NOT_HOWTO, is_visible);
    this.SetLabelText(ctrl, (Enum) TradingPostItemDetail.UI.LBL_NAME, this.itemData.GetName());
    this.SetLabelText(ctrl, (Enum) TradingPostItemDetail.UI.STR_WINDOW_TITLE, string.Format(this.sectionData.GetText("STR_TITLE_TEXT"), (object) this.details.Count));
    this.SetLabelText(ctrl, (Enum) TradingPostItemDetail.UI.STR_HAVE, this.sectionData.GetText("STR_OWN"));
    this.SetLabelText(ctrl, (Enum) TradingPostItemDetail.UI.LBL_HAVE_NUM, (object) itemNum);
    this.SetLabelText(ctrl, (Enum) TradingPostItemDetail.UI.STR_SELL, this.sectionData.GetText("STR_VALUE"));
    this.SetLabelText(ctrl, (Enum) TradingPostItemDetail.UI.LBL_SELL, (object) this.itemData.GetSalePrice());
    if (is_visible)
      this.SetLabelText(ctrl, (Enum) TradingPostItemDetail.UI.STR_NOT_HOWTO, this.sectionData.GetText("STR_EMPTY"));
    if (this._sortType != TradingPostItemDetail.SORT_TYPE.NONE)
    {
      if (this._sortType == TradingPostItemDetail.SORT_TYPE.QUANTITY)
        this.details.Sort((Comparison<TradingPostDetail>) ((e1, e2) => e1.quantity.CompareTo(e2.quantity)));
      else if (this._sortType == TradingPostItemDetail.SORT_TYPE.PRICE_UP)
        this.details.Sort((Comparison<TradingPostDetail>) ((e1, e2) => e1.price.CompareTo(e2.price)));
      else
        this.details.Sort((Comparison<TradingPostDetail>) ((e1, e2) => e2.price.CompareTo(e1.price)));
      this.SetSortButtonUIStatus();
    }
    this.SetGrid(ctrl, (Enum) TradingPostItemDetail.UI.GRD_HOWTO, "TradingPostListBaseItem", this.details.Count, false, (Action<int, Transform, bool>) ((i, t, b) => this.InitItemList(this.details[i], t, i)));
  }

  private void OnScrollViewReachBottom()
  {
    if (!TradingPostManager.IsFulfillRequirement() || !TradingPostManager.IsFinishTradingPostTutorial())
      return;
    this.Next();
  }

  private void Refresh() => this.refresh = true;

  private void Next() => this.next = true;

  private void AddList(List<TradingPostDetail> list)
  {
    this.AddItemList(this.GetCtrl((Enum) TradingPostItemDetail.UI.GRD_HOWTO), "TradingPostListBaseItem", list.Count, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, b) => this.InitItemList(list[i], t, this.details.Count + i)));
  }

  private void InitItemList(TradingPostDetail detail, Transform t, int i)
  {
    this.SetLabelText(t, (Enum) TradingPostItemDetail.UI.LBL_TOTAL_TEXT, this.sectionData.GetText("STR_QUATITY"));
    this.SetLabelText(t, (Enum) TradingPostItemDetail.UI.LBL_START_TEXT, this.sectionData.GetText("STR_PRICE"));
    this.SetLabelText(t, (Enum) TradingPostItemDetail.UI.LBL_NAME, detail.from);
    this.SetLabelText(t, (Enum) TradingPostItemDetail.UI.LBL_TOTAL_NUM, (object) detail.quantity);
    this.SetLabelText(t, (Enum) TradingPostItemDetail.UI.LBL_COST, (object) detail.price);
    this.SetItemIcon(this.FindCtrl(t, (Enum) TradingPostItemDetail.UI.OBJ_ICON), this.itemData);
    this.SetEvent(t, "BUY", i);
  }

  private void Update()
  {
    if (this.close && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
    {
      this.close = false;
      GameSection.BackSection();
      MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_TRADING_POST);
    }
    else
    {
      if (!this.refresh && !this.next || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
        return;
      if (this.refresh)
      {
        this.refresh = false;
        this.DispatchEvent("REFRESH");
      }
      else
      {
        if (!this.next)
          return;
        this.next = false;
        if (this.endPage)
          return;
        this.DispatchEvent("NEXT");
      }
    }
  }

  private void ResetSort()
  {
    this._sortType = TradingPostItemDetail.SORT_TYPE.NONE;
    this.SetSortButtonUIStatus();
  }

  private void SetBtnActive(TradingPostItemDetail.UI ui, bool active)
  {
    string sprite_name = active ? "ItemBoxExtentBtn2_half" : "ItemBoxExtentBtn_half";
    this.SetSprite((Enum) ui, sprite_name);
    this.SetButtonSprite((Enum) ui, sprite_name, true);
  }

  private void SetSortButtonUIStatus()
  {
    BoxCollider component = ((Component) this.GetCtrl((Enum) TradingPostItemDetail.UI.BTN_QUATITY)).gameObject.GetComponent<BoxCollider>();
    switch (this._sortType)
    {
      case TradingPostItemDetail.SORT_TYPE.QUANTITY:
        this.SetActive((Enum) TradingPostItemDetail.UI.LBL_PRICE_NORMAL, true);
        this.SetActive((Enum) TradingPostItemDetail.UI.LBL_PRICE_SELECT, false);
        this.SetBtnActive(TradingPostItemDetail.UI.BTN_QUATITY, true);
        this.SetBtnActive(TradingPostItemDetail.UI.BTN_PRICE, false);
        ((Collider) component).enabled = false;
        this.SetPriceArrowStatus(false, false);
        break;
      case TradingPostItemDetail.SORT_TYPE.PRICE_UP:
        this.SetActive((Enum) TradingPostItemDetail.UI.LBL_PRICE_NORMAL, false);
        this.SetActive((Enum) TradingPostItemDetail.UI.LBL_PRICE_SELECT, true);
        this.SetBtnActive(TradingPostItemDetail.UI.BTN_QUATITY, false);
        this.SetBtnActive(TradingPostItemDetail.UI.BTN_PRICE, true);
        ((Collider) component).enabled = true;
        this.SetPriceArrowStatus(true, true);
        break;
      case TradingPostItemDetail.SORT_TYPE.PRICE_DOWN:
        this.SetActive((Enum) TradingPostItemDetail.UI.LBL_PRICE_NORMAL, false);
        this.SetActive((Enum) TradingPostItemDetail.UI.LBL_PRICE_SELECT, true);
        this.SetBtnActive(TradingPostItemDetail.UI.BTN_QUATITY, false);
        this.SetBtnActive(TradingPostItemDetail.UI.BTN_PRICE, true);
        ((Collider) component).enabled = true;
        this.SetPriceArrowStatus(true, false);
        break;
      default:
        this.SetActive((Enum) TradingPostItemDetail.UI.LBL_PRICE_NORMAL, true);
        this.SetActive((Enum) TradingPostItemDetail.UI.LBL_PRICE_SELECT, false);
        this.SetBtnActive(TradingPostItemDetail.UI.BTN_QUATITY, false);
        this.SetBtnActive(TradingPostItemDetail.UI.BTN_PRICE, false);
        ((Collider) component).enabled = true;
        this.SetPriceArrowStatus(false, false);
        break;
    }
  }

  private void SetPriceArrowStatus(bool isVisible, bool isUp)
  {
    this.SetActive((Enum) TradingPostItemDetail.UI.SPR_PRICE_ARROW, isVisible);
    Vector3 one = Vector3.one;
    one.y = !isUp ? 1f : -1f;
    ((Component) this.GetCtrl((Enum) TradingPostItemDetail.UI.SPR_PRICE_ARROW)).transform.localScale = one;
  }

  private void OnQuery_QUATITY()
  {
    this._sortType = TradingPostItemDetail.SORT_TYPE.QUANTITY;
    this.RefreshUI();
  }

  private void OnQuery_PRICE()
  {
    switch (this._sortType)
    {
      case TradingPostItemDetail.SORT_TYPE.NONE:
      case TradingPostItemDetail.SORT_TYPE.QUANTITY:
      case TradingPostItemDetail.SORT_TYPE.PRICE_UP:
        this._sortType = TradingPostItemDetail.SORT_TYPE.PRICE_DOWN;
        break;
      case TradingPostItemDetail.SORT_TYPE.PRICE_DOWN:
        this._sortType = TradingPostItemDetail.SORT_TYPE.PRICE_UP;
        break;
      default:
        this._sortType = TradingPostItemDetail.SORT_TYPE.PRICE_DOWN;
        break;
    }
    this.RefreshUI();
  }

  private void OnQuery_SELL()
  {
    ItemInfo itemInfo = MonoBehaviourSingleton<InventoryManager>.I.GetItem((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) this.info.itemId));
    if (itemInfo == null)
      GameSection.ChangeEvent("NO_ITEM");
    else
      MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostSellItemData(itemInfo.tableID, itemInfo.uniqueID, MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(itemInfo.tableID));
  }

  private void OnQuery_REFRESH()
  {
    this.currentPage = 1;
    this.endPage = false;
    this.details.Clear();
    this.ResetSort();
    GameSection.StayEvent();
    MonoBehaviourSingleton<TradingPostManager>.I.SendRequestItemDetail((int) this.itemData.GetTableID(), this.currentPage, (Action<bool, List<TradingPostDetail>>) ((b, list) =>
    {
      if (list.Count == 0)
      {
        this.endPage = true;
        GameSection.ResumeEvent(false);
      }
      else
      {
        this.details.AddRange((IEnumerable<TradingPostDetail>) list);
        this.RefreshUI();
        GameSection.ResumeEvent(false);
      }
    }));
  }

  private void OnQuery_NEXT()
  {
    ++this.currentPage;
    GameSection.StayEvent();
    MonoBehaviourSingleton<TradingPostManager>.I.SendRequestItemDetail((int) this.itemData.GetTableID(), this.currentPage, (Action<bool, List<TradingPostDetail>>) ((b, list) =>
    {
      if (list.Count == 0)
      {
        this.endPage = true;
        GameSection.ResumeEvent(false);
      }
      else
      {
        this.details.AddRange((IEnumerable<TradingPostDetail>) list);
        this.RefreshUI();
        GameSection.ResumeEvent(false);
      }
    }));
  }

  private void OnQuery_BUY()
  {
    if (!TradingPostManager.IsFulfillRequirement())
    {
      GameSection.ChangeEvent("LICENSE_REQUIRE");
    }
    else
    {
      this.currentDetail = this.details[(int) GameSection.GetEventData()];
      GameSection.SetEventData((object) this.currentDetail);
    }
  }

  private void OnQuery_PURCHASE()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal < this.currentDetail.price)
    {
      GameSection.ChangeEvent("LACK");
    }
    else
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<TradingPostManager>.I.SendRequestBuyItem(this.currentDetail.transactionId, (Action<bool, Error>) ((isSuccess, ret) =>
      {
        if (isSuccess)
        {
          this.details.Remove(this.currentDetail);
          this.currentDetail = (TradingPostDetail) null;
          this.RefreshUI();
          GameSection.ChangeStayEvent("SUCCESS");
        }
        GameSection.ResumeEvent(isSuccess);
      }));
    }
  }

  private void OnCloseDialog_CrystalShopMessage() => this.RefreshUI();

  private void OnCloseDialog_CrystalShopTradingPostLicense() => this.close = true;

  private void OnCloseDialog_TradingPostSellSuccess() => this.OnQuery_REFRESH();

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
        this.SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), this.GetCtrl((Enum) TradingPostItemDetail.UI.PNL_MATERIAL_INFO));
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
    BTN_SELL,
    BTN_QUATITY,
    BTN_PRICE,
    SPR_PRICE_ARROW,
    BTN_REFRESH,
    PNL_MATERIAL_INFO,
    LBL_CRYSTAL_NUM,
    OBJ_DETAIL_BASE,
    LBL_SELL_ITEM,
    LBL_QTY_NORMAL,
    LBL_QTY_SELECT,
    LBL_PRICE_NORMAL,
    LBL_PRICE_SELECT,
    LBL_REFRESH,
    LBL_SELL,
    STR_SELL,
    SPR_NEED,
    STR_HAVE,
    LBL_HAVE_NUM,
    LBL_NAME,
    OBJ_ICON_ROOT,
    STR_WINDOW_TITLE,
    STR_NOT_HOWTO,
    SCR_HOWTO,
    GRD_HOWTO,
    LBL_TOTAL_TEXT,
    LBL_TOTAL_NUM,
    LBL_START_TEXT,
    LBL_COST,
    OBJ_ICON,
    OBJ_NOT_LICENSED,
    LBL_NOTICE,
    LBL_LOGIN,
    LBL_REQ_LOGIN,
    LBL_REQ_LICENSE,
  }

  private enum SORT_TYPE
  {
    NONE,
    QUANTITY,
    PRICE_UP,
    PRICE_DOWN,
  }
}
