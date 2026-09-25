// Decompiled with JetBrains decompiler
// Type: CrystalShopTop
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
public class CrystalShopTop : GameSection
{
  private const string TRADING_POST_LICENSE = "TradingPostLicense";
  private List<Network.ProductData> _purchaseGemList = new List<Network.ProductData>();
  private List<Network.ProductData> _purchaseBundleList = new List<Network.ProductData>();
  private List<Network.ProductData> _purchaseMaterialList = new List<Network.ProductData>();
  private Transform gemTab;
  private Transform bundleTab;
  private Transform materialTab;
  private int _currentPageIndex;
  private Transform _objBundle;
  private CrystalShopTop.VIEW_TYPE _viewType;
  private bool _isFinishGetNativeProductlist;
  private StoreDataList _nativeStoreList;
  private object[] selectEventData;
  private Network.ProductData selectProductData;
  private bool isPurchase;
  private string pp;
  private bool isSuccessBuyClose;
  private bool isSuccessCrystalBuy;
  private int currentCrystalRequestCount;
  private bool isHighlightMaterial;
  private bool isHighlightBundle;

  public override void Initialize()
  {
    this._objBundle = this.GetCtrl((Enum) CrystalShopTop.UI.OBJ_BUNDLE);
    MonoBehaviourSingleton<ShopReceiver>.I.onBillingUnavailable += new System.Action(this.onBillingUnavailable);
    MonoBehaviourSingleton<ShopReceiver>.I.onBuyItem += new Action<string>(this.OnBuyItem);
    MonoBehaviourSingleton<ShopReceiver>.I.onBuySpecialItem += new Action<ShopReceiver.PaymentPurchaseData>(this.OnBuyBundle);
    MonoBehaviourSingleton<ShopReceiver>.I.onBuyMaterialItem += new Action<ShopReceiver.PaymentPurchaseData>(this.OnBuyMaterial);
    MonoBehaviourSingleton<ShopReceiver>.I.onGetProductDatas += new Action<StoreDataList>(this.OnGetProductDatas);
    this.StartCoroutine(this.DoInitialize());
    this.isPurchase = false;
  }

  private IEnumerator DoInitialize()
  {
    bool wait = true;
    MonoBehaviourSingleton<ShopManager>.I.SendGetGoldPurchaseItemList((Action<bool>) (b => wait = false));
    while (wait)
      yield return (object) null;
    if (this._nativeStoreList != null && this._nativeStoreList.shopList != null && this._nativeStoreList.shopList.Count > 0)
      this._isFinishGetNativeProductlist = true;
    else
      Native.GetProductDatas(string.Join("----", MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Select<Network.ProductData, string>((Func<Network.ProductData, string>) (o => o.productId)).ToList<string>().ToArray()));
    while (!this._isFinishGetNativeProductlist)
      yield return (object) null;
    this._purchaseGemList = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Where<Network.ProductData>((Func<Network.ProductData, bool>) (o => o.productType == 1)).ToList<Network.ProductData>();
    this._purchaseBundleList = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Where<Network.ProductData>((Func<Network.ProductData, bool>) (o => o.productType == 2)).ToList<Network.ProductData>();
    this._purchaseMaterialList = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Where<Network.ProductData>((Func<Network.ProductData, bool>) (o => o.productType == 3)).ToList<Network.ProductData>();
    this.gemTab = this.GetCtrl((Enum) CrystalShopTop.UI.OBJ_GEM_TAB);
    this.bundleTab = this.GetCtrl((Enum) CrystalShopTop.UI.OBJ_BUNDLE_TAB);
    this.materialTab = this.GetCtrl((Enum) CrystalShopTop.UI.OBJ_MATERIAL_TAB);
    MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("IAP_open", "Functionality");
    this.CheckHightlightBtnTab();
    string pId = GameSection.GetEventData() as string;
    if (pId != null)
    {
      this._viewType = CrystalShopTop.VIEW_TYPE.BUNDLE;
      int index = this._purchaseBundleList.FindIndex((Predicate<Network.ProductData>) (_purchaseBundle => _purchaseBundle.productId == pId));
      if (index >= 0)
        this._currentPageIndex = index;
    }
    this.SetActive((Enum) CrystalShopTop.UI.LBL_SERVICE_MESSAGE, MonoBehaviourSingleton<AccountManager>.I.usageLimitMode);
    this.SetLabelText((Enum) CrystalShopTop.UI.LBL_SERVICE_MESSAGE, string.Format(this.sectionData.GetText("SERVICE_LIMITED")));
    base.Initialize();
  }

  protected override void OnDestroy()
  {
    if (MonoBehaviourSingleton<ShopReceiver>.IsValid())
    {
      MonoBehaviourSingleton<ShopReceiver>.I.onBillingUnavailable -= new System.Action(this.onBillingUnavailable);
      MonoBehaviourSingleton<ShopReceiver>.I.onBuyItem -= new Action<string>(this.OnBuyItem);
      MonoBehaviourSingleton<ShopReceiver>.I.onBuySpecialItem -= new Action<ShopReceiver.PaymentPurchaseData>(this.OnBuyBundle);
      MonoBehaviourSingleton<ShopReceiver>.I.onBuyMaterialItem -= new Action<ShopReceiver.PaymentPurchaseData>(this.OnBuyMaterial);
      MonoBehaviourSingleton<ShopReceiver>.I.onGetProductDatas -= new Action<StoreDataList>(this.OnGetProductDatas);
    }
    base.OnDestroy();
  }

  public override void StartSection()
  {
  }

  public override void UpdateUI() => this._updateTab();

  private void _updateTab()
  {
    switch (this._viewType)
    {
      case CrystalShopTop.VIEW_TYPE.BUNDLE:
        this._viewBundleTab();
        break;
      case CrystalShopTop.VIEW_TYPE.MATERIAL:
        this._viewMaterialTab();
        break;
      default:
        this._viewGemTab();
        break;
    }
  }

  private void _viewGemTab()
  {
    this.SetActive(this.gemTab, true);
    this.SetActive(this.bundleTab, false);
    this.SetActive(this.materialTab, false);
    this.CheckOpenedGemTab();
    int j = 0;
    this.SetTable(this.gemTab, (Enum) CrystalShopTop.UI.TBL_LIST, "CrystalShopListItem", this._purchaseGemList.Count, false, (Func<int, Transform, Transform>) ((i, p) =>
    {
      Network.ProductData purchaseGem = this._purchaseGemList[j];
      return MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.HasSpecial(purchaseGem.productId) || MonoBehaviourSingleton<GoGameSettingsManager>.IsValid() && MonoBehaviourSingleton<GoGameSettingsManager>.I.UseShopUI3(purchaseGem.productId) ? this.Realizes("CrystalShopListItem2", p) : (Transform) null;
    }), (Action<int, Transform, bool>) ((i, t, b) =>
    {
      Network.ProductData purchaseGem = this._purchaseGemList[j++];
      this.SetSprite(t, (Enum) CrystalShopTop.UI.SPR_THUMB, purchaseGem.iconImg);
      this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_NAME, purchaseGem.name);
      this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_PRICE, string.Format(this.sectionData.GetText("PRICE"), (object) purchaseGem.priceIncludeTax));
      this.SetSupportEncoding(t, (Enum) CrystalShopTop.UI.LBL_PROMO, true);
      this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_PROMO, purchaseGem.promo.Replace("\\n", "\n"));
      if (purchaseGem.remainingDay > 0)
      {
        this.SetActive(t, (Enum) CrystalShopTop.UI.SPR_SOLD, true);
        this.SetActive(t, (Enum) CrystalShopTop.UI.SPR_SOLD_MASK, true);
      }
      else
      {
        this.SetActive(t, (Enum) CrystalShopTop.UI.SPR_SOLD, false);
        this.SetActive(t, (Enum) CrystalShopTop.UI.SPR_SOLD_MASK, false);
        if (purchaseGem.offerType > 0)
        {
          UITexture spro = ((Component) this.FindCtrl(t, (Enum) CrystalShopTop.UI.OBJ_OFFER)).GetComponent<UITexture>();
          ResourceLoad.LoadShopImageGemOfferTexture(spro, (uint) purchaseGem.offerType, (Action<Texture>) (tex =>
          {
            if (!Object.op_Inequality((Object) spro, (Object) null))
              return;
            spro.mainTexture = tex;
          }));
        }
      }
      if (this._nativeStoreList != null)
      {
        StoreData product = this._nativeStoreList.getProduct(purchaseGem.productId);
        if (product != null)
          this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_PRICE, product.price.ToString());
      }
      this.SetEvent(t, "BUY", i);
    }));
  }

  private void _viewBundleTab()
  {
    this.SetActive(this.gemTab, false);
    this.SetActive(this.materialTab, false);
    this.SetActive(this.bundleTab, true);
    this.CheckOpenedBundleTab();
    if (this._purchaseBundleList.Count == 0)
    {
      this.SetActive((Enum) CrystalShopTop.UI.OBJ_AIM, false);
      this.SetActive((Enum) CrystalShopTop.UI.TEX_NPCMODEL, false);
      this.SetActive((Enum) CrystalShopTop.UI.SPR_FRAME_DRAGON, false);
      this.SetActive((Enum) CrystalShopTop.UI.LBL_NONE, true);
    }
    else
    {
      this.SetActive((Enum) CrystalShopTop.UI.OBJ_AIM, true);
      this.SetActive((Enum) CrystalShopTop.UI.TEX_NPCMODEL, true);
      this.SetActive((Enum) CrystalShopTop.UI.SPR_FRAME_DRAGON, true);
      if (this._currentPageIndex < this._purchaseBundleList.Count)
      {
        Network.ProductData purchaseBundle = this._purchaseBundleList[this._currentPageIndex];
        ProductDataTable.PackInfo pack = Singleton<ProductDataTable>.I.GetPack(purchaseBundle.productId);
        Transform root = this.SetPrefab(this._objBundle, MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.prefabBundleName);
        Utility.ToggleActiveChildren(this._objBundle, this._currentPageIndex);
        UITexture sprw = ((Component) this.FindCtrl(root, (Enum) CrystalShopTop.UI.SPR_WINDOW)).GetComponent<UITexture>();
        string shopImageName = ResourceName.GetShopImageName((int) pack.bundleImageId);
        Hash128 hash128 = new Hash128();
        if (Object.op_Inequality((Object) MonoBehaviourSingleton<ResourceManager>.I.event_manifest, (Object) null))
          hash128 = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.SHOP_IMG.ToAssetBundleName(shopImageName));
        if (!((Hash128) ref hash128).isValid)
          sprw.mainTexture = Resources.Load("Texture/White") as Texture;
        else
          ResourceLoad.LoadShopImageTexture(sprw, pack.bundleImageId, (Action<Texture>) (tex =>
          {
            if (!Object.op_Inequality((Object) sprw, (Object) null))
              return;
            sprw.mainTexture = tex;
          }));
        this.SetTexture(root, (Enum) CrystalShopTop.UI.SPR_OFFER, (Texture) null);
        if (purchaseBundle.offerType > 0)
        {
          UITexture spro = ((Component) this.FindCtrl(root, (Enum) CrystalShopTop.UI.SPR_OFFER)).GetComponent<UITexture>();
          ResourceLoad.LoadShopImageOfferTexture(sprw, (uint) purchaseBundle.offerType, (Action<Texture>) (tex =>
          {
            if (!Object.op_Inequality((Object) sprw, (Object) null))
              return;
            spro.width = tex.width;
            spro.height = tex.height;
            spro.mainTexture = tex;
          }));
        }
        this.RemoveModel(root, (Enum) CrystalShopTop.UI.OBJ_MODEL);
        if (!string.IsNullOrEmpty(pack.chestName))
          this.SetModel(root, (Enum) CrystalShopTop.UI.OBJ_MODEL, pack.chestName);
        this.SetActive(this.GetChildSafe(root, (Enum) CrystalShopTop.UI.OBJ_OFFER, purchaseBundle.offerType - 1), true);
        if (purchaseBundle.remainingDay > 0)
        {
          this.SetButtonEnabled(root, (Enum) CrystalShopTop.UI.BTN_BUY, false);
          this.SetActive(root, (Enum) CrystalShopTop.UI.LBL_BUNDLE_PRICE, false);
          this.SetActive(root, (Enum) CrystalShopTop.UI.LBL_DAY_LEFT, true);
          this.SetLabelText(root, (Enum) CrystalShopTop.UI.LBL_DAY_LEFT, purchaseBundle.remainingDay > 1 ? string.Format(this.sectionData.GetText("DAYS_LEFT"), (object) purchaseBundle.remainingDay) : string.Format(this.sectionData.GetText("DAY_LEFT"), (object) purchaseBundle.remainingDay));
        }
        else
        {
          this.SetButtonEnabled(root, (Enum) CrystalShopTop.UI.BTN_BUY, true);
          this.SetActive(root, (Enum) CrystalShopTop.UI.LBL_BUNDLE_PRICE, true);
          this.SetActive(root, (Enum) CrystalShopTop.UI.LBL_DAY_LEFT, false);
        }
        this.SetLabelText(root, (Enum) CrystalShopTop.UI.LBL_BUNDLE_PRICE, string.Format(this.sectionData.GetText("PRICE"), (object) purchaseBundle.priceIncludeTax));
        if (this._nativeStoreList != null)
        {
          StoreData product = this._nativeStoreList.getProduct(purchaseBundle.productId);
          if (product != null)
            this.SetLabelText(root, (Enum) CrystalShopTop.UI.LBL_BUNDLE_PRICE, product.price.ToString());
        }
      }
      this.SetLabelText((Enum) CrystalShopTop.UI.LBL_CURRENT, (object) (this._purchaseBundleList.Count > 0 ? this._currentPageIndex + 1 : this._currentPageIndex));
      this.SetLabelText((Enum) CrystalShopTop.UI.LBL_TOTAL, (object) this._purchaseBundleList.Count);
      bool is_enabled1 = this._currentPageIndex > 0;
      bool is_enabled2 = this._currentPageIndex < this._purchaseBundleList.Count - 1;
      this.SetColor((Enum) CrystalShopTop.UI.SPR_AIM_L, is_enabled1 ? Color.white : Color.clear);
      this.SetColor((Enum) CrystalShopTop.UI.SPR_AIM_R, is_enabled2 ? Color.white : Color.clear);
      this.SetButtonEnabled((Enum) CrystalShopTop.UI.BTN_AIM_L, is_enabled1);
      this.SetButtonEnabled((Enum) CrystalShopTop.UI.BTN_AIM_R, is_enabled2);
      this.SetActive((Enum) CrystalShopTop.UI.BTN_AIM_L_INACTIVE, !is_enabled1);
      this.SetActive((Enum) CrystalShopTop.UI.BTN_AIM_R_INACTIVE, !is_enabled2);
      this.SetRepeatButton((Enum) CrystalShopTop.UI.BTN_AIM_L, "AIM_L");
      this.SetRepeatButton((Enum) CrystalShopTop.UI.BTN_AIM_R, "AIM_R");
      this._updateNPC();
    }
  }

  private void _viewMaterialTab()
  {
    this.SetActive(this.gemTab, false);
    this.SetActive(this.bundleTab, false);
    this.SetActive(this.materialTab, true);
    this.CheckOpenedMaterialTab();
    int j = 0;
    this.SetTable(this.materialTab, (Enum) CrystalShopTop.UI.TBL_LIST, "CrystalShopListItemMaterial", this._purchaseMaterialList.Count, false, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      Network.ProductData purchaseMaterial = this._purchaseMaterialList[j];
      string text1 = string.Format(this.sectionData.GetText("PRICE"), (object) purchaseMaterial.priceIncludeTax);
      UITexture spro = ((Component) this.FindCtrl(t, (Enum) CrystalShopTop.UI.SPR_THUMB)).GetComponent<UITexture>();
      ResourceLoad.LoadShopImageMaterialTexture(spro, purchaseMaterial.iconImg, (Action<Texture>) (tex =>
      {
        if (!Object.op_Inequality((Object) spro, (Object) null))
          return;
        spro.mainTexture = tex;
      }));
      this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_NAME, purchaseMaterial.name);
      this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_PRICE, text1);
      this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_PRICE_OLD, purchaseMaterial.oldPrice > 0.0 ? string.Format(this.sectionData.GetText("PRICE_STRETCH"), (object) purchaseMaterial.oldPrice) : string.Empty);
      this.SetSupportEncoding(t, (Enum) CrystalShopTop.UI.LBL_PRICE_OLD, true);
      this.SetSupportEncoding(t, (Enum) CrystalShopTop.UI.LBL_PROMO, true);
      this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_PROMO, purchaseMaterial.promo.Replace("\\n", "\n"));
      if (purchaseMaterial.remainingDay > 0)
      {
        string empty = string.Empty;
        TimeSpan timeSpan = TimeSpan.FromSeconds((double) purchaseMaterial.remainingDay);
        string text2 = timeSpan.Days <= 1 ? string.Format(this.sectionData.GetText("TIME_REMAIN"), (object) $"{timeSpan.Hours:d2}:{timeSpan.Minutes:d2}:{timeSpan.Seconds:d2}") : string.Format(this.sectionData.GetText("DAY_REMAIN"), (object) timeSpan.Days);
        this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_REMAIN, text2);
      }
      else
        this.SetActive(t, (Enum) CrystalShopTop.UI.LBL_REMAIN, false);
      this.SetActive(this.GetChildSafe(t, (Enum) CrystalShopTop.UI.OBJ_OFFER, purchaseMaterial.offerType - 1), true);
      if (this._nativeStoreList != null)
      {
        StoreData product1 = this._nativeStoreList.getProduct(purchaseMaterial.productId);
        if (product1 != null)
        {
          text1 = product1.price.ToString();
          this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_PRICE, text1);
        }
        StoreData product2 = this._nativeStoreList.getProduct(purchaseMaterial.skipId);
        if (product2 != null)
          this.SetLabelText(t, (Enum) CrystalShopTop.UI.LBL_PRICE_OLD, product2.price.ToString());
      }
      this.SetEvent(t, "MATERIAL_DETAIL", (object) new object[3]
      {
        (object) purchaseMaterial,
        (object) text1,
        (object) j
      });
      ++j;
    }));
  }

  private void OnGetProductDatas(StoreDataList list)
  {
    this._isFinishGetNativeProductlist = true;
    this._nativeStoreList = list;
  }

  private void _updateNPC()
  {
    NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(this.sectionData.sectionName + "_TEXT");
    if (section == null)
      return;
    NPCMessageTable.Message message = section.GetNPCMessage();
    if (message == null)
      return;
    this.SetRenderNPCModel((Enum) CrystalShopTop.UI.TEX_NPCMODEL, message.npc, message.pos, message.rot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>) (loader => loader.GetAnimator().Play(message.animationStateName)));
  }

  private void _disableChest()
  {
    if (Object.op_Equality((Object) this._objBundle, (Object) null))
      return;
    foreach (Transform root in this._objBundle)
      this.SetActiveModel(root, (Enum) CrystalShopTop.UI.OBJ_MODEL, false);
  }

  private void _enableChest()
  {
    if (Object.op_Equality((Object) this._objBundle, (Object) null) || this._viewType == CrystalShopTop.VIEW_TYPE.GEM || this._viewType == CrystalShopTop.VIEW_TYPE.MATERIAL)
      return;
    foreach (Transform root in this._objBundle)
      this.SetActiveModel(root, (Enum) CrystalShopTop.UI.OBJ_MODEL, true);
  }

  private void OnQuery_GEM_TAB()
  {
    this._viewType = CrystalShopTop.VIEW_TYPE.GEM;
    this.RefreshUI();
  }

  private void OnQuery_BUNDLE_TAB()
  {
    this._viewType = CrystalShopTop.VIEW_TYPE.BUNDLE;
    this.RefreshUI();
  }

  private void OnQuery_MATERIAL_TAB()
  {
    this._viewType = CrystalShopTop.VIEW_TYPE.MATERIAL;
    this.RefreshUI();
  }

  private void OnQuery_AIM_R()
  {
    ++this._currentPageIndex;
    this.RefreshUI();
  }

  private void OnQuery_AIM_L()
  {
    --this._currentPageIndex;
    this.RefreshUI();
  }

  private void OnQuery_CrystalShopMaterialDetail_Buy() => this.OnQuery_BUY();

  private void OnQuery_BUY()
  {
    switch (this._viewType)
    {
      case CrystalShopTop.VIEW_TYPE.GEM:
        this.selectProductData = this._purchaseGemList[(int) GameSection.GetEventData()];
        break;
      case CrystalShopTop.VIEW_TYPE.BUNDLE:
        this.selectProductData = this._purchaseBundleList[this._currentPageIndex];
        this._disableChest();
        break;
      case CrystalShopTop.VIEW_TYPE.MATERIAL:
        this.selectProductData = this._purchaseMaterialList[(int) GameSection.GetEventData()];
        break;
    }
    this.selectEventData = new object[7]
    {
      GameSection.GetEventData(),
      (object) this.selectProductData,
      (object) this.selectProductData.name,
      (object) this.selectProductData.discount,
      (object) this.selectProductData.price,
      (object) 10,
      (object) 5
    };
    if (MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.HasSpecial(this.selectProductData.productId) && this.selectProductData.remainingDay > 0)
    {
      GameSection.ChangeEvent("SPECIAL_SOLD", (object) this.selectProductData);
    }
    else
    {
      this.pp = string.Empty;
      if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isParentPassSet != 0)
        GameSection.ChangeEvent("PP_INPUT");
      else
        this.SendGoldCanPurchase();
    }
  }

  private void OnQuery_PP_TO_BUY()
  {
    this.pp = GameSection.GetEventData() as string;
    this.SendGoldCanPurchase();
  }

  private void OnQuery_CrystalShopStopper_YES() => this.RequestEvent("STOPPER_TO_BUY");

  private void OnQuery_STOPPER_TO_BUY()
  {
    GameSection.SetEventData((object) this.selectEventData);
    GameSection.StayEvent();
    this.DoPurchase();
  }

  private void OnQuery_CURRENCY()
  {
    GameSection.ChangeEvent("INFO", (object) WebViewManager.Currency);
  }

  private void OnQuery_COMMERCIAL()
  {
    GameSection.ChangeEvent("INFO", (object) WebViewManager.Commercial);
  }

  private void OnQuery_FUND() => GameSection.ChangeEvent("INFO", (object) WebViewManager.Found);

  private void OnQuery_EULA() => GameSection.ChangeEvent("INFO", (object) WebViewManager.eula);

  private void OnQuery_SECTION_BACK()
  {
    if (!this.isSuccessBuyClose)
      MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("IAP_close", "Functionality");
    this.isSuccessBuyClose = false;
  }

  private void OnCloseDialog_CrystalShopMessage() => this.OnCloseBuyDialog();

  private void OnCloseDialog_CrystalShopDebugMessage() => this.OnCloseBuyDialog();

  private void OnCloseDialog_CrystalShopSpecialNotice() => this.OnCloseBuyDialog();

  private void OnCloseDialog_CrystalShopBundleNotice() => this.OnCloseBuyDialog();

  private void OnCloseDialog_CrystalShopPendingNotice() => this.OnCloseBuyDialog();

  private void OnCloseBuyDialog()
  {
    this.isSuccessBuyClose = true;
    this.isSuccessCrystalBuy = true;
    MonoBehaviourSingleton<ShopManager>.I.SendGetGoldPurchaseItemList((Action<bool>) null);
  }

  public void Update()
  {
    if (!this.isSuccessCrystalBuy || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      return;
    this.isSuccessCrystalBuy = false;
    GameSection.BackSection();
  }

  private void SendGoldCanPurchase()
  {
    GameSection.SetEventData((object) this.selectEventData);
    GameSection.StayEvent();
    MonoBehaviourSingleton<ShopManager>.I.SendGoldCanPurchase(this.selectProductData.productId, this.pp, (Action<Error>) (ret =>
    {
      if (ret != Error.None)
      {
        if (ret == Error.WRN_GOLD_OVER_LIMITTER_OVERUSE)
        {
          GameSection.ChangeStayEvent("STOPPER");
          GameSection.ResumeEvent(true);
        }
        else
          GameSection.ResumeEvent(false);
      }
      else
        this.DoPurchase();
    }));
  }

  private void DoPurchase()
  {
    this.isPurchase = true;
    Native.RequestPurchase(this.selectProductData.productId, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString(), MonoBehaviourSingleton<UserInfoManager>.I.userIdHash);
  }

  private void onBillingUnavailable()
  {
    this.isPurchase = false;
    GameSection.ChangeStayEvent("BILLING_UNAVAILABLE");
    GameSection.ResumeEvent(true);
  }

  private void OnBuyItem(string productId)
  {
    if (!string.IsNullOrEmpty(productId))
    {
      if (MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.HasSpecial(productId))
      {
        this.OnBuySpecial(productId);
        return;
      }
      this.isPurchase = false;
      int index = 0;
      Network.ProductData data = (Network.ProductData) null;
      MonoBehaviourSingleton<ShopManager>.I.GetPurchaseItem(productId, ref data, ref index);
      if (data != null)
      {
        this.SendRequestCurrentCrystal(this.GetFinishAction(index, data));
        return;
      }
    }
    if (!this.isPurchase)
      return;
    this._enableChest();
    GameSection.ResumeEvent(false);
  }

  private void OnBuyBundle(ShopReceiver.PaymentPurchaseData purchaseData)
  {
    if (purchaseData != null && purchaseData.bundle != null)
    {
      this.isPurchase = false;
      this.SendRequestCurrentPresentAndShopList(this.GetFinishActionBundle(purchaseData));
    }
    if (!this.isPurchase)
      return;
    this._enableChest();
    GameSection.ResumeEvent(false);
  }

  private void OnBuySpecial(string productId)
  {
    if (!string.IsNullOrEmpty(productId))
    {
      this.isPurchase = false;
      int index = 0;
      Network.ProductData product_data = (Network.ProductData) null;
      MonoBehaviourSingleton<ShopManager>.I.GetPurchaseItem(productId, ref product_data, ref index);
      if (product_data != null)
        MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, (Action<bool>) (is_success =>
        {
          this._disableChest();
          if (!GameSceneEvent.IsStay())
            GameSection.StayEvent();
          if (product_data.skipId == "TradingPostLicense")
          {
            MonoBehaviourSingleton<TradingPostManager>.I.tradingStatus = 2;
            GameSection.ChangeStayEvent("TRADING_POST_LICENSE");
          }
          else
            GameSection.ChangeStayEvent("SPECIAL_NOTICE", (object) product_data);
          GameSection.ResumeEvent(true);
        }));
    }
    if (!this.isPurchase)
      return;
    GameSection.ResumeEvent(false);
  }

  private void OnBuyMaterial(ShopReceiver.PaymentPurchaseData purchaseData)
  {
    if (purchaseData != null)
    {
      this.isPurchase = false;
      int index = 0;
      Network.ProductData data = (Network.ProductData) null;
      MonoBehaviourSingleton<ShopManager>.I.GetPurchaseItem(purchaseData.productId, ref data, ref index);
      System.Action onFinish = this.GetFinishAction(index, data);
      MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, (Action<bool>) (is_success => onFinish()));
    }
    if (!this.isPurchase)
      return;
    GameSection.ResumeEvent(false);
  }

  private System.Action GetFinishAction(int index, Network.ProductData product_data)
  {
    return (System.Action) (() =>
    {
      this._disableChest();
      if (!GameSceneEvent.IsStay())
        GameSection.StayEvent();
      if (product_data.skipId == "TradingPostLicense")
      {
        MonoBehaviourSingleton<TradingPostManager>.I.tradingStatus = 2;
        GameSection.ChangeStayEvent("TRADING_POST_LICENSE");
      }
      else
        GameSection.ChangeStayEvent("BUY", (object) new object[7]
        {
          (object) index,
          (object) product_data,
          (object) product_data.name,
          (object) product_data.discount,
          (object) product_data.price,
          (object) 10,
          (object) 5
        });
      GameSection.ResumeEvent(true);
    });
  }

  private System.Action GetFinishActionBundle(ShopReceiver.PaymentPurchaseData purchaseData)
  {
    return (System.Action) (() =>
    {
      this._disableChest();
      ProductDataTable.PackInfo pack = Singleton<ProductDataTable>.I.GetPack(purchaseData.productId);
      GameSection.ChangeStayEvent(!string.IsNullOrEmpty(pack.eventName) ? pack.eventName : "BUNDLE_NOTICE", (object) purchaseData);
      GameSection.ResumeEvent(true);
    });
  }

  private System.Action GetFinishActionMaterial(ShopReceiver.PaymentPurchaseData purchaseData)
  {
    return (System.Action) (() =>
    {
      GameSection.ChangeStayEvent("BUNDLE_NOTICE", (object) purchaseData);
      GameSection.ResumeEvent(true);
    });
  }

  private void SendRequestCurrentCrystal(System.Action onFinish)
  {
    Protocol.Send<OnceStatusInfoModel>(OnceStatusInfoModel.URL, (Action<OnceStatusInfoModel>) (result => this.CheckCrystalNum(result, onFinish)));
  }

  private void SendRequestCurrentPresentAndShopList(System.Action onFinish)
  {
    this.SendRequestCurrentCrystal((System.Action) (() => MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, (Action<bool>) (is_success => onFinish()))));
  }

  private void CheckCrystalNum(OnceStatusInfoModel ret, System.Action onFinish)
  {
    if (ret.Error != Error.None)
      return;
    MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal = ret.result.userStatus.crystal;
    MonoBehaviourSingleton<UserInfoManager>.I.DirtyUserStatus();
    onFinish();
  }

  private void CheckHightlightBtnTab()
  {
    this.isHighlightMaterial = false;
    this.isHighlightBundle = false;
    string str1 = PlayerPrefs.GetString("Purchase_Item_List_Tab_Bundle", string.Empty);
    string str2 = PlayerPrefs.GetString("Purchase_Item_List_Tab_Material", string.Empty);
    int count1 = this._purchaseMaterialList.Count;
    for (int index = 0; index < count1; ++index)
    {
      if (str2.IndexOf(this._purchaseMaterialList[index].productId) < 0)
      {
        this.isHighlightMaterial = true;
        break;
      }
    }
    int count2 = this._purchaseBundleList.Count;
    for (int index = 0; index < count2; ++index)
    {
      if (str1.IndexOf(this._purchaseBundleList[index].productId) < 0)
      {
        this.isHighlightBundle = true;
        break;
      }
    }
  }

  private void CheckOpenedGemTab()
  {
    Transform ctrl1 = this.FindCtrl(this.gemTab, (Enum) CrystalShopTop.UI.SPR_BTN_BORDER);
    Transform ctrl2 = this.FindCtrl(ctrl1, (Enum) CrystalShopTop.UI.BTN_MATERIAL_TAB);
    Transform ctrl3 = this.FindCtrl(ctrl1, (Enum) CrystalShopTop.UI.BTN_BUNDLE_TAB);
    if (this.isHighlightMaterial)
      this.SetBadge(ctrl2, -1, (SpriteAlignment) 3, -8, -8);
    else
      this.SetBadge(ctrl2, 0, (SpriteAlignment) 3, -8, -8);
    if (this.isHighlightBundle)
      this.SetBadge(ctrl3, -1, (SpriteAlignment) 3, -8, -8);
    else
      this.SetBadge(ctrl3, 0, (SpriteAlignment) 3, -8, -8);
    string str = PlayerPrefs.GetString("Purchase_Item_List_Tab_Gem", string.Empty);
    int count = this._purchaseGemList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (str.IndexOf(this._purchaseGemList[index].productId) < 0)
        str = $"{str}/{this._purchaseGemList[index].productId}";
    }
    PlayerPrefs.SetString("Purchase_Item_List_Tab_Gem", str);
  }

  private void CheckOpenedMaterialTab()
  {
    this.isHighlightMaterial = false;
    Transform ctrl1 = this.FindCtrl(this.materialTab, (Enum) CrystalShopTop.UI.SPR_BTN_BORDER);
    Transform ctrl2 = this.FindCtrl(ctrl1, (Enum) CrystalShopTop.UI.BTN_MATERIAL_TAB);
    Transform ctrl3 = this.FindCtrl(ctrl1, (Enum) CrystalShopTop.UI.BTN_BUNDLE_TAB);
    this.SetBadge(ctrl2, 0, (SpriteAlignment) 3, -8, -8);
    if (this.isHighlightBundle)
      this.SetBadge(ctrl3, -1, (SpriteAlignment) 3, -8, -8);
    else
      this.SetBadge(ctrl3, 0, (SpriteAlignment) 3, -8, -8);
    string str = PlayerPrefs.GetString("Purchase_Item_List_Tab_Material", string.Empty);
    int count = this._purchaseMaterialList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (str.IndexOf(this._purchaseMaterialList[index].productId) < 0)
        str = $"{str}/{this._purchaseMaterialList[index].productId}";
    }
    PlayerPrefs.SetString("Purchase_Item_List_Tab_Material", str);
  }

  private void CheckOpenedBundleTab()
  {
    this.isHighlightBundle = false;
    Transform ctrl1 = this.FindCtrl(this.bundleTab, (Enum) CrystalShopTop.UI.SPR_BTN_BORDER);
    Transform ctrl2 = this.FindCtrl(ctrl1, (Enum) CrystalShopTop.UI.BTN_MATERIAL_TAB);
    Transform ctrl3 = this.FindCtrl(ctrl1, (Enum) CrystalShopTop.UI.BTN_BUNDLE_TAB);
    if (this.isHighlightMaterial)
      this.SetBadge(ctrl2, -1, (SpriteAlignment) 3, -8, -8);
    else
      this.SetBadge(ctrl2, 0, (SpriteAlignment) 3, -8, -8);
    this.SetBadge(ctrl3, 0, (SpriteAlignment) 3, -8, -8);
    string str = PlayerPrefs.GetString("Purchase_Item_List_Tab_Bundle", string.Empty);
    int count = this._purchaseBundleList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (str.IndexOf(this._purchaseBundleList[index].productId) < 0)
        str = $"{str}/{this._purchaseBundleList[index].productId}";
    }
    PlayerPrefs.SetString("Purchase_Item_List_Tab_Bundle", str);
  }

  private enum VIEW_TYPE
  {
    GEM,
    BUNDLE,
    MATERIAL,
  }

  public enum PRODUCT_TYPE
  {
    Gem = 1,
    Bundle = 2,
    Material = 3,
    Gacha = 4,
  }

  public enum OFFER_TYPE
  {
    GoodBuy = 1,
    BestValue = 2,
    OneTimeOffer = 3,
    BestDeal = 4,
    MostPopular = 5,
    BlackFriday = 6,
  }

  public enum BUNDLE_TYPE
  {
    OneTimesOnly = 1,
    OneTimesPerMonth = 2,
    FiveTimesPerWeek = 3,
    ThreeTimesOnly = 4,
    FiveTimesOnly = 5,
    Unlimited = 6,
    FiveTimesPerWeekRound = 7,
    OneTimeLeft = 8,
    TwoTimesLeft = 9,
    ThreeTimesLeft = 10, // 0x0000000A
    FourTimesLeft = 11, // 0x0000000B
    FiveTimesLeft = 12, // 0x0000000C
  }

  private enum UI
  {
    OBJ_BUNDLE_TAB,
    OBJ_GEM_TAB,
    OBJ_MATERIAL_TAB,
    SPR_BTN_BORDER,
    BTN_BUNDLE_TAB,
    BTN_GEM_TAB,
    BTN_MATERIAL_TAB,
    TBL_LIST,
    OBJ_AGREEMENT,
    OBJ_BUNDLE,
    SPR_FRAME_DRAGON,
    OBJ_AIM,
    BTN_AIM_L,
    BTN_AIM_R,
    BTN_AIM_L_INACTIVE,
    BTN_AIM_R_INACTIVE,
    SPR_AIM_L,
    SPR_AIM_R,
    LBL_CURRENT,
    LBL_TOTAL,
    LBL_NONE,
    TEX_NPCMODEL,
    SPR_WINDOW,
    SPR_OFFER,
    OBJ_MODEL,
    LBL_BUNDLE_PRICE,
    BTN_BUY,
    LBL_DAY_LEFT,
    TBL_MATERIAL_LIST,
    LBL_PRICE_OLD,
    LBL_REMAIN,
    BTN_DETAIL,
    LBL_NAME,
    LBL_PRICE,
    SPR_THUMB,
    LBL_PROMO,
    SPR_SOLD,
    SPR_SOLD_MASK,
    OBJ_OFFER,
    SPR_GOODBUY,
    SPR_BESTVALUE,
    SPR_ONETIMESOFFER,
    LBL_SERVICE_MESSAGE,
  }
}
