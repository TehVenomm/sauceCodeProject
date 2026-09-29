// Decompiled with JetBrains decompiler
// Type: HomePointShop
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
public class HomePointShop : GameSection
{
  private List<PointShop> pointShop = new List<PointShop>();
  private UIModelRenderTexture modelTexture;
  private List<PointShopItem> currentPointShopItem = new List<PointShopItem>();
  protected int currentPage;
  protected int maxPage;
  private PointShopFilterBase.Filter filter;
  private HomePointShop.VIEW_TYPE currentType;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  public IEnumerator DoInitialize()
  {
    this.currentType = HomePointShop.VIEW_TYPE.NORMAL;
    this.currentPage = 1;
    bool hasList = false;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    loadingQueue.Load(RESOURCE_CATEGORY.UI, "PointShopListItem");
    MonoBehaviourSingleton<UserInfoManager>.I.PointShopManager.SendGetPointShops((Action<bool, List<PointShop>>) ((isSuccess, resultList) =>
    {
      if (!isSuccess)
        return;
      this.pointShop = resultList;
      foreach (PointShop pointShop in this.pointShop)
        loadingQueue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName(pointShop.pointShopId));
      hasList = true;
    }));
    while (!hasList)
      yield return (object) null;
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.UpdateNPC();
    this.UpdateTab();
  }

  protected void UpdateNPC()
  {
    string empty = string.Empty;
    NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(this.sectionData.sectionName + "_TEXT");
    if (section == null)
      return;
    NPCMessageTable.Message message = section.GetNPCMessage();
    if (message == null)
      return;
    string message1 = message.message;
    this.SetRenderNPCModel((Enum) HomePointShop.UI.TEX_NPCMODEL, message.npc, message.pos, message.rot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>) (loader => loader.GetAnimator().Play(message.animationStateName)));
    this.SetLabelText((Enum) HomePointShop.UI.LBL_NPC_MESSAGE, message1);
  }

  private void UpdateTab()
  {
    switch (this.currentType)
    {
      case HomePointShop.VIEW_TYPE.EVENT_LIST:
        this.ViewEventTab();
        break;
      default:
        this.ViewNormalTab();
        break;
    }
  }

  private void ViewNormalTab()
  {
    this.SetActive((Enum) HomePointShop.UI.OBJ_NORMAL, true);
    this.SetActive((Enum) HomePointShop.UI.OBJ_TAB_ROOT, true);
    this.SetActive((Enum) HomePointShop.UI.OBJ_ON_TAB_NORMAL, true);
    this.SetActive((Enum) HomePointShop.UI.OBJ_ON_TAB_EVENT, false);
    this.SetActive((Enum) HomePointShop.UI.OBJ_EVENT_LIST, false);
    PointShop shop = this.pointShop.First<PointShop>((Func<PointShop, bool>) (x => !x.isEvent));
    this.currentPointShopItem = this.GetBuyableItemList();
    if (this.filter != null)
      this.filter.DoFiltering(ref this.currentPointShopItem);
    this.SetLabelText((Enum) HomePointShop.UI.LBL_NORMAL_POINT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) shop.userPoint));
    ResourceLoad.LoadPointIconImageTexture(((Component) this.GetCtrl((Enum) HomePointShop.UI.TEX_NORMAL_POINT_ICON)).GetComponent<UITexture>(), (uint) shop.pointShopId);
    this.maxPage = this.currentPointShopItem.Count / GameDefine.POINT_SHOP_LIST_COUNT;
    if (this.currentPointShopItem.Count % GameDefine.POINT_SHOP_LIST_COUNT > 0)
      ++this.maxPage;
    this.SetLabelText((Enum) HomePointShop.UI.LBL_ARROW_NOW, this.maxPage > 0 ? this.currentPage.ToString() : "0");
    this.SetLabelText((Enum) HomePointShop.UI.LBL_ARROW_MAX, this.maxPage.ToString());
    this.SetGrid((Enum) HomePointShop.UI.GRD_NORMAL, "PointShopListItem", Mathf.Min(GameDefine.POINT_SHOP_LIST_COUNT, this.currentPointShopItem.Count - (this.currentPage - 1) * GameDefine.POINT_SHOP_LIST_COUNT), true, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      PointShopItem pointShopItem = this.currentPointShopItem[i + (this.currentPage - 1) * GameDefine.POINT_SHOP_LIST_COUNT];
      object event_data = (object) new object[3]
      {
        (object) pointShopItem,
        (object) shop,
        (object) new Action<PointShopItem, int>(this.OnBuy)
      };
      this.SetEvent(t, "CONFIRM_BUY", event_data);
      ((Component) t).GetComponent<PointShopItemList>().SetUp(pointShopItem, (uint) shop.pointShopId, pointShopItem.needPoint <= shop.userPoint);
      int num = -1;
      if (pointShopItem.type == 3)
        num = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum((uint) pointShopItem.itemId);
      this.SetLabelText(t, (Enum) HomePointShop.UI.LBL_HAVE, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 6U), (object) num.ToString()));
      this.SetActive(t, (Enum) HomePointShop.UI.LBL_HAVE, num >= 0);
    }));
    bool is_visible = this.pointShop.Any<PointShop>((Func<PointShop, bool>) (x => x.isEvent));
    this.SetActive((Enum) HomePointShop.UI.OBJ_EVENT_NON_ACTIVE, !is_visible);
    this.SetActive((Enum) HomePointShop.UI.BTN_EVENT, is_visible);
  }

  private void ViewEventTab()
  {
    this.SetActive((Enum) HomePointShop.UI.OBJ_NORMAL, false);
    this.SetActive((Enum) HomePointShop.UI.OBJ_TAB_ROOT, true);
    this.SetActive((Enum) HomePointShop.UI.OBJ_ON_TAB_NORMAL, false);
    this.SetActive((Enum) HomePointShop.UI.OBJ_ON_TAB_EVENT, true);
    this.SetActive((Enum) HomePointShop.UI.OBJ_EVENT_LIST, true);
    List<PointShop> current = this.pointShop.Where<PointShop>((Func<PointShop, bool>) (x => x.isEvent)).ToList<PointShop>();
    this.SetGrid((Enum) HomePointShop.UI.GRD_EVENT_LIST, "PointShopEventList", current.Count, true, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      PointShop event_data = current[i];
      UITexture component1 = ((Component) this.FindCtrl(t, (Enum) HomePointShop.UI.TEX_EVENT_LIST_BANNER)).GetComponent<UITexture>();
      UITexture component2 = ((Component) this.FindCtrl(t, (Enum) HomePointShop.UI.TXT_EVENT_LIST_POINT_ICON)).GetComponent<UITexture>();
      this.SetLabelText(t, (Enum) HomePointShop.UI.LBL_EVENT_LIST_POINT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) event_data.userPoint));
      int pointShopId1 = event_data.pointShopId;
      ResourceLoad.LoadPointIconImageTexture(component2, (uint) pointShopId1);
      int pointShopId2 = event_data.pointShopId;
      ResourceLoad.LoadPointShopBannerTexture(component1, (uint) pointShopId2);
      this.SetEvent(this.FindCtrl(t, (Enum) HomePointShop.UI.TEX_EVENT_LIST_BANNER), "EVENT_SHOP", (object) event_data);
      bool is_visible = event_data.items.Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.isBuyable)).Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.type != 8 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedStamp(x.itemId))).Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.type != 9 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedDegree(x.itemId))).Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.type != 7 || !MonoBehaviourSingleton<GlobalSettingsManager>.I.IsUnlockedAvatar(x.itemId))).Count<PointShopItem>() == 0;
      this.SetActive(t, (Enum) HomePointShop.UI.LBL_EVENT_LIST_SOLD_OUT, is_visible);
      this.SetButtonEnabled(t, (Enum) HomePointShop.UI.TEX_EVENT_LIST_BANNER, !is_visible);
      this.SetLabelText(t, (Enum) HomePointShop.UI.LBL_EVENT_LIST_REMAINING_TIME, event_data.expire);
    }));
  }

  private void OnQuery_CONFIRM_BUY()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    if ((eventData[1] as PointShop).userPoint >= (eventData[0] as PointShopItem).needPoint)
      return;
    GameSection.ChangeEvent("SHORTAGE_POINT");
  }

  private void OnQuery_ON_EVENT()
  {
    this.currentType = HomePointShop.VIEW_TYPE.EVENT_LIST;
    this.UpdateTab();
  }

  private void OnQuery_ON_NORMAL()
  {
    this.currentType = HomePointShop.VIEW_TYPE.NORMAL;
    this.UpdateTab();
  }

  private void OnQuery_HOW_TO() => GameSection.SetEventData((object) WebViewManager.PointShop);

  private void OnBuy(PointShopItem item, int num)
  {
    GameSection.SetEventData((object) PointShopManager.GetBoughtMessage(item, num));
    GameSection.StayEvent();
    PointShop pointShop = this.pointShop.First<PointShop>((Func<PointShop, bool>) (x => x.items.Contains(item)));
    MonoBehaviourSingleton<UserInfoManager>.I.PointShopManager.SendPointShopBuy(item, pointShop, num, (Action<bool>) (isSuccess =>
    {
      if (isSuccess)
        this.UpdateTab();
      GameSection.ResumeEvent(isSuccess);
    }));
  }

  private void OnQuery_PAGE_NEXT()
  {
    ++this.currentPage;
    if (this.currentPage > this.maxPage)
      this.currentPage = 1;
    this.UpdateTab();
  }

  private void OnQuery_PAGE_PREV()
  {
    --this.currentPage;
    if (this.currentPage < 1)
      this.currentPage = this.maxPage > 0 ? this.maxPage : 1;
    this.UpdateTab();
  }

  private void OnQuery_FILTER()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.filter,
      (object) this.GetBuyableItemList()
    });
  }

  private void OnCloseDialog_PointShopFilter()
  {
    if (!(GameSection.GetEventData() is PointShopFilterBase.Filter eventData))
      return;
    this.filter = eventData;
    this.currentPage = 1;
    this.RefreshUI();
  }

  private List<PointShopItem> GetBuyableItemList()
  {
    return this.pointShop.Where<PointShop>((Func<PointShop, bool>) (x => !x.isEvent)).SelectMany<PointShop, PointShopItem>((Func<PointShop, IEnumerable<PointShopItem>>) (x => (IEnumerable<PointShopItem>) x.items)).Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.isBuyable)).Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.type != 8 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedStamp(x.itemId))).Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.type != 9 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedDegree(x.itemId))).Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.type != 7 || !MonoBehaviourSingleton<GlobalSettingsManager>.I.IsUnlockedAvatar(x.itemId))).ToList<PointShopItem>();
  }

  private enum VIEW_TYPE
  {
    NORMAL,
    EVENT_LIST,
  }

  private enum UI
  {
    OBJ_TAB_ROOT,
    OBJ_ON_TAB_EVENT,
    OBJ_ON_TAB_NORMAL,
    TEX_NPCMODEL,
    LBL_NPC_MESSAGE,
    GRD_NORMAL,
    GRD_EVENT_LIST,
    LBL_NORMAL_POINT,
    TEX_NORMAL_POINT_ICON,
    LBL_HAVE,
    LBL_FILTER,
    LBL_EVENT_LIST_POINT,
    LBL_EVENT_LIST_POINT_TITLE,
    TEX_EVENT_LIST_BANNER,
    LBL_EVENT_LIST_SOLD_OUT,
    LBL_EVENT_LIST_REMAINING_TIME,
    TXT_EVENT_LIST_POINT_ICON,
    OBJ_NORMAL,
    OBJ_EVENT_LIST,
    BTN_EVENT,
    OBJ_EVENT_NON_ACTIVE,
    OBJ_NPC,
    LBL_ARROW_NOW,
    LBL_ARROW_MAX,
  }
}
