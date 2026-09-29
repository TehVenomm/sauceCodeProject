// Decompiled with JetBrains decompiler
// Type: HomePointShopEventDetail
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
public class HomePointShopEventDetail : GameSection
{
  private PointShop data;
  private List<PointShopItem> currentPointShopItem = new List<PointShopItem>();
  protected int currentPage;
  protected int maxPage;
  private PointShopFilterBase.Filter filter;

  public override void Initialize()
  {
    this.data = GameSection.GetEventData() as PointShop;
    this.currentPage = 1;
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    loadingQueue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName(this.data.pointShopId));
    loadingQueue.Load(true, RESOURCE_CATEGORY.COMMON, ResourceName.GetPointSHopBGImageName(this.data.pointShopId));
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    UITexture component1 = ((Component) this.GetCtrl((Enum) HomePointShopEventDetail.UI.TEX_POINT_ICON)).GetComponent<UITexture>();
    UITexture component2 = ((Component) this.GetCtrl((Enum) HomePointShopEventDetail.UI.TEX_EVENT_POP)).GetComponent<UITexture>();
    int pointShopId = this.data.pointShopId;
    ResourceLoad.LoadPointIconImageTexture(component1, (uint) pointShopId);
    ResourceLoad.LoadPointShopBGTexture(component2, (uint) this.data.pointShopId);
    this.SetLabelText((Enum) HomePointShopEventDetail.UI.LBL_POINT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) this.data.userPoint));
    this.SetList();
  }

  private void SetList()
  {
    this.currentPointShopItem = this.GetBuyableItemList();
    if (this.filter != null)
      this.filter.DoFiltering(ref this.currentPointShopItem);
    this.maxPage = this.currentPointShopItem.Count / GameDefine.POINT_SHOP_LIST_COUNT;
    if (this.currentPointShopItem.Count % GameDefine.POINT_SHOP_LIST_COUNT > 0)
      ++this.maxPage;
    this.SetLabelText((Enum) HomePointShopEventDetail.UI.LBL_ARROW_NOW, this.maxPage > 0 ? this.currentPage.ToString() : "0");
    this.SetLabelText((Enum) HomePointShopEventDetail.UI.LBL_ARROW_MAX, this.maxPage.ToString());
    this.SetGrid((Enum) HomePointShopEventDetail.UI.GRD_LIST, "PointShopListItem", Mathf.Min(GameDefine.POINT_SHOP_LIST_COUNT, this.currentPointShopItem.Count - (this.currentPage - 1) * GameDefine.POINT_SHOP_LIST_COUNT), true, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      PointShopItem pointShopItem = this.currentPointShopItem[i + (this.currentPage - 1) * GameDefine.POINT_SHOP_LIST_COUNT];
      object event_data = (object) new object[3]
      {
        (object) pointShopItem,
        (object) this.data,
        (object) new Action<PointShopItem, int>(this.OnBuy)
      };
      this.SetEvent(t, "CONFIRM_BUY", event_data);
      ((Component) t).GetComponent<PointShopItemList>().SetUp(pointShopItem, (uint) this.data.pointShopId, pointShopItem.needPoint <= this.data.userPoint);
      int num = -1;
      if (pointShopItem.type == 3)
        num = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum((uint) pointShopItem.itemId);
      this.SetLabelText(t, (Enum) HomePointShopEventDetail.UI.LBL_HAVE, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 6U), (object) num.ToString()));
      this.SetActive(t, (Enum) HomePointShopEventDetail.UI.LBL_HAVE, num >= 0);
    }));
  }

  private void OnQuery_CONFIRM_BUY()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    if ((eventData[1] as PointShop).userPoint >= (eventData[0] as PointShopItem).needPoint)
      return;
    GameSection.ChangeEvent("SHORTAGE_POINT");
  }

  private void OnQuery_HOW_TO() => GameSection.SetEventData((object) WebViewManager.PointShop);

  private void OnBuy(PointShopItem item, int num)
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) item.name,
      (object) num
    });
    GameSection.StayEvent();
    Protocol.Send<PointShopBuyModel.SendForm, PointShopBuyModel>(PointShopBuyModel.URL, new PointShopBuyModel.SendForm()
    {
      uid = item.pointShopItemId,
      num = num
    }, (Action<PointShopBuyModel>) (result =>
    {
      if (result != null && result.Error == Error.None)
      {
        item.buyCount += num;
        this.data.userPoint -= item.needPoint * num;
        this.RefreshUI();
      }
      GameSection.ResumeEvent(result != null && result.Error == Error.None);
    }));
  }

  private void OnQuery_PAGE_NEXT()
  {
    ++this.currentPage;
    if (this.currentPage > this.maxPage)
      this.currentPage = 1;
    this.RefreshUI();
  }

  private void OnQuery_PAGE_PREV()
  {
    --this.currentPage;
    if (this.currentPage < 1)
      this.currentPage = this.maxPage > 0 ? this.maxPage : 1;
    this.RefreshUI();
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
    return this.data.items.Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.isBuyable)).Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.type != 8 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedStamp(x.itemId))).Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.type != 9 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedDegree(x.itemId))).Where<PointShopItem>((Func<PointShopItem, bool>) (x => x.type != 7 || !MonoBehaviourSingleton<GlobalSettingsManager>.I.IsUnlockedAvatar(x.itemId))).ToList<PointShopItem>();
  }

  private enum UI
  {
    TEX_EVENT_POP,
    LBL_POINT,
    GRD_LIST,
    TEX_POINT_ICON,
    LBL_ARROW_NOW,
    LBL_ARROW_MAX,
    LBL_FILTER,
    LBL_HAVE,
  }
}
