// Decompiled with JetBrains decompiler
// Type: TradingPostTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class TradingPostTop : GameSection
{
  private const string BTN_NORMAL_SPRITE = "ItemBoxExtentBtn_half";
  private const string BTN_ACTIVE_SPRITE = "ItemBoxExtentBtn2_half";
  private TradingPostTop.VIEW_TYPE _viewType;
  private TradingPostTop.SORT_TYPE _sortType;
  private List<TradingPostInfo> allInfos = new List<TradingPostInfo>();
  private List<TradingPostInfo> currentInfos;
  private System.Action[] tutorialArr;
  private int currentTutorialIndex;
  private int currentPage = 1;
  private bool endPage;
  private UIScrollView scrollview;
  private bool refresh;
  private bool next;

  public override void Initialize()
  {
    if (!TradingPostManager.IsFinishTradingPostTutorial())
    {
      this.tutorialArr = new System.Action[3];
      this.tutorialArr[0] = new System.Action(this.tutorialStep1);
      this.tutorialArr[1] = new System.Action(this.tutorialStep2);
      this.tutorialArr[2] = new System.Action(this.tutorialStep3);
    }
    this.scrollview = this.GetComponent<UIScrollView>((Enum) TradingPostTop.UI.SCR_POST);
    this.scrollview.onReachBottom += new UIScrollView.OnDragNotification(this.OnScrollViewReachBottom);
    this.StartCoroutine(this.DoInitialize());
  }

  protected override void OnDestroy()
  {
    if (Object.op_Inequality((Object) this.scrollview, (Object) null))
      this.scrollview.onReachBottom -= new UIScrollView.OnDragNotification(this.OnScrollViewReachBottom);
    base.OnDestroy();
  }

  private void OnScrollViewReachBottom()
  {
    if (!TradingPostManager.IsFulfillRequirement() || !TradingPostManager.IsFinishTradingPostTutorial())
      return;
    this.Next();
  }

  private IEnumerator DoInitialize()
  {
    bool wait = true;
    MonoBehaviourSingleton<TradingPostManager>.I.SendRequestInfo(this.currentPage, (Action<bool, List<TradingPostInfo>>) ((b, list) =>
    {
      wait = false;
      this.allInfos.AddRange((IEnumerable<TradingPostInfo>) list);
    }));
    while (wait)
      yield return (object) null;
    base.Initialize();
    if (MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSoldNum > 0)
      this.RequestEvent("HISTORY");
  }

  private void Refresh() => this.refresh = true;

  private void Next() => this.next = true;

  private void Update()
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

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    switch (flags)
    {
      case GameSection.NOTIFY_FLAG.UPDATE_TRADING_POST:
        this.UpdateUI();
        this.StartSection();
        break;
      case GameSection.NOTIFY_FLAG.UPDATE_TRADING_POST_ITEM_DETAIL:
        this.Refresh();
        break;
      case GameSection.NOTIFY_FLAG.UPDATE_TRADING_POST_SOLD:
        this.UpdateUI();
        break;
    }
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) TradingPostTop.UI.LBL_SELL_ITEM, this.sectionData.GetText("STR_SELL_ITEM"));
    this.SetLabelText((Enum) TradingPostTop.UI.LBL_QTY_NORMAL, this.sectionData.GetText("TEXT_BTN_QTY"));
    this.SetLabelText((Enum) TradingPostTop.UI.LBL_QTY_SELECT, this.sectionData.GetText("TEXT_BTN_QTY"));
    this.SetLabelText((Enum) TradingPostTop.UI.LBL_PRICE_NORMAL, this.sectionData.GetText("TEXT_BTN_PRICE"));
    this.SetLabelText((Enum) TradingPostTop.UI.LBL_PRICE_SELECT, this.sectionData.GetText("TEXT_BTN_PRICE"));
    this.SetLabelText((Enum) TradingPostTop.UI.LBL_REFRESH, this.sectionData.GetText("TEXT_BTN_REFRESH"));
    bool flag = TradingPostManager.IsPurchasedLicense() || TradingPostManager.IsLoginRequireFinish();
    this.SetActive((Enum) TradingPostTop.UI.OBJ_NOT_LICENSED, !flag);
    if (!flag)
    {
      this.SetLabelText((Enum) TradingPostTop.UI.LBL_LOGIN, string.Format(this.sectionData.GetText("STR_LOGIN"), (object) MonoBehaviourSingleton<TradingPostManager>.I.tradingDay, (object) MonoBehaviourSingleton<TradingPostManager>.I.tradingConditionDay));
      this.SetLabelText((Enum) TradingPostTop.UI.LBL_NOTICE, string.Format(this.sectionData.GetText("STR_NOTICE"), (object) MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
      this.SetLabelText((Enum) TradingPostTop.UI.LBL_REQ_LOGIN, this.sectionData.GetText("STR_REQ_LOGIN"));
      this.SetLabelText((Enum) TradingPostTop.UI.LBL_REQ_LICENSE, this.sectionData.GetText("STR_REQ_LICENSE"));
    }
    List<ITEM_TYPE> itemTypes;
    switch (this._viewType)
    {
      case TradingPostTop.VIEW_TYPE.MATERIAL:
        itemTypes = MonoBehaviourSingleton<GlobalSettingsManager>.I.itemMaterialType;
        break;
      case TradingPostTop.VIEW_TYPE.ITEM:
        itemTypes = MonoBehaviourSingleton<GlobalSettingsManager>.I.itemItemType;
        break;
      case TradingPostTop.VIEW_TYPE.MAGI:
        itemTypes = MonoBehaviourSingleton<GlobalSettingsManager>.I.itemMagiType;
        break;
      case TradingPostTop.VIEW_TYPE.LAPIS:
        itemTypes = MonoBehaviourSingleton<GlobalSettingsManager>.I.itemLapisType;
        break;
      default:
        itemTypes = Enum.GetValues(typeof (ITEM_TYPE)).Cast<ITEM_TYPE>().ToList<ITEM_TYPE>();
        break;
    }
    this.currentInfos = this.allInfos.Where<TradingPostInfo>((Func<TradingPostInfo, bool>) (info => itemTypes.Contains(ItemInfo.CreateItemInfo(info.itemId).GetType()))).ToList<TradingPostInfo>();
    if (this._sortType != TradingPostTop.SORT_TYPE.NONE)
    {
      if (this._sortType == TradingPostTop.SORT_TYPE.QUANTITY)
        this.currentInfos.Sort((Comparison<TradingPostInfo>) ((e1, e2) => e1.totalQuantity.CompareTo(e2.totalQuantity)));
      else if (this._sortType == TradingPostTop.SORT_TYPE.PRICE_UP)
        this.currentInfos.Sort((Comparison<TradingPostInfo>) ((e1, e2) => e1.unitPrice.CompareTo(e2.unitPrice)));
      else
        this.currentInfos.Sort((Comparison<TradingPostInfo>) ((e1, e2) => e2.unitPrice.CompareTo(e1.unitPrice)));
      this.SetSortButtonUIStatus();
    }
    this.SetGrid((Enum) TradingPostTop.UI.GRD_POST, "TradingPostListBaseItem", this.currentInfos.Count, true, (Action<int, Transform, bool>) ((i, t, b) => this.InitItemList(this.currentInfos[i], t, i)));
    this.SetBadge((Enum) TradingPostTop.UI.BTN_STORAGE, MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSoldNum, (SpriteAlignment) 1, 8, -8);
  }

  public override void StartSection()
  {
    if (TradingPostManager.IsFulfillRequirement() && !TradingPostManager.IsFinishTradingPostTutorial())
    {
      this.startTutorial();
    }
    else
    {
      if (MonoBehaviourSingleton<TradingPostManager>.I.tradingPostFindItemId == 0)
        return;
      this.DispatchEvent("FIND_ITEM");
    }
  }

  private void AddList(List<TradingPostInfo> list)
  {
    this.AddItemList(this.GetCtrl((Enum) TradingPostTop.UI.GRD_POST), "TradingPostListBaseItem", list.Count, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, b) => this.InitItemList(list[i], t, i)));
  }

  private void InitItemList(TradingPostInfo info, Transform t, int i)
  {
    ItemInfo itemInfo = ItemInfo.CreateItemInfo(info.itemId);
    ItemSortData data = new ItemSortData();
    data.SetItem((object) itemInfo);
    this.SetLabelText(t, (Enum) TradingPostTop.UI.LBL_NAME, data.GetName());
    this.SetLabelText(t, (Enum) TradingPostTop.UI.LBL_TOTAL_NUM, (object) info.totalQuantity);
    this.SetLabelText(t, (Enum) TradingPostTop.UI.LBL_COST, (object) info.unitPrice);
    this.SetItemIcon(this.FindCtrl(t, (Enum) TradingPostTop.UI.OBJ_ICON), data);
    this.SetEvent(t, "DETAIL", i);
  }

  private void OnQuery_TAB_0()
  {
    if (this._viewType == TradingPostTop.VIEW_TYPE.ALL)
      return;
    this._viewType = TradingPostTop.VIEW_TYPE.ALL;
    this.RefreshUI();
  }

  private void ResetSort()
  {
    this._sortType = TradingPostTop.SORT_TYPE.NONE;
    this.SetSortButtonUIStatus();
  }

  private void SetBtnActive(TradingPostTop.UI ui, bool active)
  {
    string sprite_name = active ? "ItemBoxExtentBtn2_half" : "ItemBoxExtentBtn_half";
    this.SetSprite((Enum) ui, sprite_name);
    this.SetButtonSprite((Enum) ui, sprite_name, true);
  }

  private void SetSortButtonUIStatus()
  {
    BoxCollider component = ((Component) this.GetCtrl((Enum) TradingPostTop.UI.BTN_QUATITY)).gameObject.GetComponent<BoxCollider>();
    switch (this._sortType)
    {
      case TradingPostTop.SORT_TYPE.QUANTITY:
        this.SetActive((Enum) TradingPostTop.UI.LBL_PRICE_NORMAL, true);
        this.SetActive((Enum) TradingPostTop.UI.LBL_PRICE_SELECT, false);
        this.SetBtnActive(TradingPostTop.UI.BTN_QUATITY, true);
        this.SetBtnActive(TradingPostTop.UI.BTN_PRICE, false);
        ((Collider) component).enabled = false;
        this.SetPriceArrowStatus(false, false);
        break;
      case TradingPostTop.SORT_TYPE.PRICE_UP:
        this.SetActive((Enum) TradingPostTop.UI.LBL_PRICE_NORMAL, false);
        this.SetActive((Enum) TradingPostTop.UI.LBL_PRICE_SELECT, true);
        this.SetBtnActive(TradingPostTop.UI.BTN_QUATITY, false);
        this.SetBtnActive(TradingPostTop.UI.BTN_PRICE, true);
        ((Collider) component).enabled = true;
        this.SetPriceArrowStatus(true, true);
        break;
      case TradingPostTop.SORT_TYPE.PRICE_DOWN:
        this.SetActive((Enum) TradingPostTop.UI.LBL_PRICE_NORMAL, false);
        this.SetActive((Enum) TradingPostTop.UI.LBL_PRICE_SELECT, true);
        this.SetBtnActive(TradingPostTop.UI.BTN_QUATITY, false);
        this.SetBtnActive(TradingPostTop.UI.BTN_PRICE, true);
        ((Collider) component).enabled = true;
        this.SetPriceArrowStatus(true, false);
        break;
      default:
        this.SetActive((Enum) TradingPostTop.UI.LBL_PRICE_NORMAL, true);
        this.SetActive((Enum) TradingPostTop.UI.LBL_PRICE_SELECT, false);
        this.SetBtnActive(TradingPostTop.UI.BTN_QUATITY, false);
        this.SetBtnActive(TradingPostTop.UI.BTN_PRICE, false);
        ((Collider) component).enabled = true;
        this.SetPriceArrowStatus(false, false);
        break;
    }
  }

  private void SetPriceArrowStatus(bool isVisible, bool isUp)
  {
    this.SetActive((Enum) TradingPostTop.UI.SPR_PRICE_ARROW, isVisible);
    Vector3 one = Vector3.one;
    one.y = !isUp ? 1f : -1f;
    ((Component) this.GetCtrl((Enum) TradingPostTop.UI.SPR_PRICE_ARROW)).transform.localScale = one;
  }

  private void OnQuery_TAB_1()
  {
    if (this._viewType == TradingPostTop.VIEW_TYPE.MATERIAL)
      return;
    this._viewType = TradingPostTop.VIEW_TYPE.MATERIAL;
    this.RefreshUI();
  }

  private void OnQuery_TAB_2()
  {
    if (this._viewType == TradingPostTop.VIEW_TYPE.ITEM)
      return;
    this._viewType = TradingPostTop.VIEW_TYPE.ITEM;
    this.RefreshUI();
  }

  private void OnQuery_TAB_3()
  {
    if (this._viewType == TradingPostTop.VIEW_TYPE.MAGI)
      return;
    this._viewType = TradingPostTop.VIEW_TYPE.MAGI;
    this.RefreshUI();
  }

  private void OnQuery_TAB_4()
  {
    if (this._viewType == TradingPostTop.VIEW_TYPE.LAPIS)
      return;
    this._viewType = TradingPostTop.VIEW_TYPE.LAPIS;
    this.RefreshUI();
  }

  private void OnQuery_REFRESH()
  {
    this.currentPage = 1;
    this.endPage = false;
    this.allInfos.Clear();
    this.ResetSort();
    GameSection.StayEvent();
    MonoBehaviourSingleton<TradingPostManager>.I.SendRequestInfo(this.currentPage, (Action<bool, List<TradingPostInfo>>) ((b, list) =>
    {
      if (list.Count == 0)
        this.endPage = true;
      else
        this.allInfos.AddRange((IEnumerable<TradingPostInfo>) list);
      this.RefreshUI();
      GameSection.ResumeEvent(false);
    }));
  }

  private void OnQuery_NEXT()
  {
    ++this.currentPage;
    GameSection.StayEvent();
    MonoBehaviourSingleton<TradingPostManager>.I.SendRequestInfo(this.currentPage, (Action<bool, List<TradingPostInfo>>) ((b, list) =>
    {
      if (list.Count == 0)
        this.endPage = true;
      else
        this.allInfos.AddRange((IEnumerable<TradingPostInfo>) list);
      this.RefreshUI();
      GameSection.ResumeEvent(false);
    }));
  }

  private void OnQuery_QUATITY()
  {
    this._sortType = TradingPostTop.SORT_TYPE.QUANTITY;
    this.RefreshUI();
  }

  private void OnQuery_PRICE()
  {
    switch (this._sortType)
    {
      case TradingPostTop.SORT_TYPE.NONE:
      case TradingPostTop.SORT_TYPE.QUANTITY:
      case TradingPostTop.SORT_TYPE.PRICE_UP:
        this._sortType = TradingPostTop.SORT_TYPE.PRICE_DOWN;
        break;
      case TradingPostTop.SORT_TYPE.PRICE_DOWN:
        this._sortType = TradingPostTop.SORT_TYPE.PRICE_UP;
        break;
      default:
        this._sortType = TradingPostTop.SORT_TYPE.PRICE_DOWN;
        break;
    }
    this.RefreshUI();
  }

  private void OnQuery_SELL() => this.RequestEvent("TO_STORAGE");

  private void OnQuery_DETAIL()
  {
    MonoBehaviourSingleton<TradingPostManager>.I.Viewinfo = this.currentInfos[(int) GameSection.GetEventData()];
  }

  private void OnQuery_HELP() => GameSection.SetEventData((object) WebViewManager.TradingPost);

  private void OnQuery_HISTORY() => GameSection.SetEventData((object) 1);

  private void OnQuery_FIND_ITEM()
  {
    if (!TradingPostManager.IsFinishTradingPostTutorial())
    {
      GameSection.StopEvent();
    }
    else
    {
      GameSection.StayEvent();
      int tradingPostFindItemId = MonoBehaviourSingleton<TradingPostManager>.I.tradingPostFindItemId;
      MonoBehaviourSingleton<TradingPostManager>.I.RemoveTradingPostFindData();
      MonoBehaviourSingleton<TradingPostManager>.I.SendRequestFindItem(tradingPostFindItemId, (Action<bool, TradingPostInfo>) ((b, info) =>
      {
        if (!b || info == null)
        {
          GameSection.ChangeStayEvent("MISSING");
        }
        else
        {
          GameSection.ChangeStayEvent("DETAIL");
          MonoBehaviourSingleton<TradingPostManager>.I.Viewinfo = info;
        }
        GameSection.ResumeEvent(true);
      }));
    }
  }

  private void OnCloseDialog_CrystalShopTradingPostLicense()
  {
    this.RefreshUI();
    this.StartSection();
  }

  private void OnCloseDialog_TradingPostActiveHistory()
  {
    this.Refresh();
    this.UpdateUI();
  }

  private void OnCloseDialog_TradingPostInventoryDialog()
  {
    if (!MonoBehaviourSingleton<TradingPostManager>.I.isRefreshTradingPost)
      return;
    MonoBehaviourSingleton<TradingPostManager>.I.isRefreshTradingPost = false;
    this.Refresh();
  }

  private void startTutorial()
  {
    this.currentTutorialIndex = 0;
    this.StartCoroutine(this.DoTutorial());
  }

  private IEnumerator DoTutorial()
  {
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.MOMENT, true);
    yield return (object) new WaitForSeconds(1f);
    this.nextStep();
    yield return (object) new WaitForSeconds(2f);
    while (this.HasStepTutorial())
    {
      if (Input.GetMouseButtonDown(0))
      {
        this.nextStep();
        yield return (object) new WaitForSeconds(2f);
      }
      else
        yield return (object) null;
    }
    while (!Input.GetMouseButtonDown(0))
      yield return (object) null;
    this.endTutorial();
  }

  private bool HasStepTutorial()
  {
    return this.tutorialArr != null && this.currentTutorialIndex < this.tutorialArr.Length;
  }

  private void nextStep()
  {
    this.tutorialArr[this.currentTutorialIndex]();
    ++this.currentTutorialIndex;
  }

  private void tutorialStep1()
  {
    this.SetLabelText(this.GetCtrl((Enum) TradingPostTop.UI.OBJ_TUTORIAL_1), (Enum) TradingPostTop.UI.LBL_MESSAGE, string.Format(this.sectionData.GetText("STR_TUTORIAL_1"), (object) MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
    this.SetActive((Enum) TradingPostTop.UI.OBJ_TUTORIAL_1, true);
    this.SetActive((Enum) TradingPostTop.UI.OBJ_TUTORIAL_2, false);
    this.SetActive((Enum) TradingPostTop.UI.OBJ_TUTORIAL_3, false);
  }

  private void tutorialStep2()
  {
    this.SetLabelText(this.GetCtrl((Enum) TradingPostTop.UI.OBJ_TUTORIAL_2), (Enum) TradingPostTop.UI.LBL_MESSAGE, this.sectionData.GetText("STR_TUTORIAL_2"));
    this.SetActive((Enum) TradingPostTop.UI.OBJ_TUTORIAL_1, false);
    this.SetActive((Enum) TradingPostTop.UI.OBJ_TUTORIAL_2, true);
    this.SetActive((Enum) TradingPostTop.UI.OBJ_TUTORIAL_3, false);
  }

  private void tutorialStep3()
  {
    this.SetLabelText(this.GetCtrl((Enum) TradingPostTop.UI.OBJ_TUTORIAL_3), (Enum) TradingPostTop.UI.LBL_MESSAGE, this.sectionData.GetText("STR_TUTORIAL_3"));
    this.SetActive((Enum) TradingPostTop.UI.OBJ_TUTORIAL_1, false);
    this.SetActive((Enum) TradingPostTop.UI.OBJ_TUTORIAL_2, false);
    this.SetActive((Enum) TradingPostTop.UI.OBJ_TUTORIAL_3, true);
  }

  private void endTutorial()
  {
    GameSaveData.instance.isFinishTradingPostTutorial = true;
    this.SetActive((Enum) TradingPostTop.UI.OBJ_TUTORIAL, false);
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.MOMENT, false);
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
        this.SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), this.GetCtrl((Enum) TradingPostTop.UI.PNL_MATERIAL_INFO));
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
    BTN_STORAGE,
    BTN_SELL,
    BTN_QUATITY,
    BTN_PRICE,
    SPR_PRICE_ARROW,
    BTN_REFRESH,
    LBL_SELL_ITEM,
    LBL_QTY_NORMAL,
    LBL_QTY_SELECT,
    LBL_PRICE_NORMAL,
    LBL_PRICE_SELECT,
    LBL_REFRESH,
    TGL_TAB0,
    TGL_TAB1,
    TGL_TAB2,
    TGL_TAB3,
    TGL_TAB4,
    SCR_POST,
    GRD_POST,
    GRD_POST_SMALL,
    PNL_MATERIAL_INFO,
    LBL_NAME,
    LBL_TOTAL_NUM,
    LBL_COST,
    OBJ_ICON,
    OBJ_NOT_LICENSED,
    LBL_NOTICE,
    LBL_LOGIN,
    LBL_REQ_LOGIN,
    LBL_REQ_LICENSE,
    OBJ_TUTORIAL,
    OBJ_TUTORIAL_1,
    OBJ_TUTORIAL_2,
    OBJ_TUTORIAL_3,
    LBL_MESSAGE,
  }

  private enum VIEW_TYPE
  {
    ALL,
    MATERIAL,
    ITEM,
    MAGI,
    LAPIS,
  }

  private enum SORT_TYPE
  {
    NONE,
    QUANTITY,
    PRICE_UP,
    PRICE_DOWN,
  }
}
