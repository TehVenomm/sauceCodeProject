// Decompiled with JetBrains decompiler
// Type: BlackMarketTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class BlackMarketTop : GameSection
{
  private bool _isFinishGetNativeProductlist;
  private StoreDataList _nativeStoreList;
  private int timeResetMarket;
  private int currentNPCMessageIndex;
  private Color normalSale = new Color(0.0470588244f, 0.6039216f, 0.003921569f, 1f);
  private Color mediumSale = new Color(1f, 0.403921574f, 0.168627456f, 1f);
  private Color hotSale = Color.red;
  private Color disableTintColor = new Color(0.4f, 0.4f, 0.4f, 1f);
  private DarkMarketItem currentItemChoosed;
  private bool isPurchase;
  private bool isRelloading;
  private bool isReseting;

  public override void Initialize()
  {
    GameSaveData.instance.canShowNoteDarkMarket = false;
    MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.UpdateNoteMarket();
    MonoBehaviourSingleton<ShopReceiver>.I.onBillingUnavailable += new System.Action(this.OnBillingUnavailable);
    MonoBehaviourSingleton<ShopReceiver>.I.onBuyItem += new Action<string>(this.OnBuyItem);
    MonoBehaviourSingleton<ShopReceiver>.I.onBuySpecialItem += new Action<ShopReceiver.PaymentPurchaseData>(this.OnBuyBundle);
    MonoBehaviourSingleton<ShopReceiver>.I.onBuyMaterialItem += new Action<ShopReceiver.PaymentPurchaseData>(this.OnBuyMaterial);
    MonoBehaviourSingleton<ShopReceiver>.I.onGetProductDatas += new Action<StoreDataList>(this.OnGetProductDatas);
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    bool wait = true;
    MonoBehaviourSingleton<ShopManager>.I.SendGetDarkMarketItemList((Action<bool>) (b => wait = false));
    while (wait)
      yield return (object) null;
    if (this._nativeStoreList != null && this._nativeStoreList.shopList != null && this._nativeStoreList.shopList.Count > 0)
    {
      this._isFinishGetNativeProductlist = true;
    }
    else
    {
      string listProductData = MonoBehaviourSingleton<ShopManager>.I.GetListProductData();
      if (!string.IsNullOrEmpty(listProductData))
        Native.GetProductDatas(listProductData);
      else
        this._isFinishGetNativeProductlist = true;
    }
    while (!this._isFinishGetNativeProductlist)
      yield return (object) null;
    this.LoadDarkMarketUI(true);
    this.UpdateNPC();
    base.Initialize();
    this.StartCoroutine("TimeCountDown");
  }

  protected override void OnDestroy()
  {
    if (MonoBehaviourSingleton<ShopReceiver>.IsValid())
    {
      MonoBehaviourSingleton<ShopReceiver>.I.onBillingUnavailable -= new System.Action(this.OnBillingUnavailable);
      MonoBehaviourSingleton<ShopReceiver>.I.onBuyItem -= new Action<string>(this.OnBuyItem);
      MonoBehaviourSingleton<ShopReceiver>.I.onBuySpecialItem -= new Action<ShopReceiver.PaymentPurchaseData>(this.OnBuyBundle);
      MonoBehaviourSingleton<ShopReceiver>.I.onBuyMaterialItem -= new Action<ShopReceiver.PaymentPurchaseData>(this.OnBuyMaterial);
      MonoBehaviourSingleton<ShopReceiver>.I.onGetProductDatas -= new Action<StoreDataList>(this.OnGetProductDatas);
    }
    base.OnDestroy();
  }

  private void LoadDarkMarketUI(bool isReloadIcon)
  {
    this.SetLabelText((Enum) BlackMarketTop.UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString("N0"));
    this.SetLabelText((Enum) BlackMarketTop.UI.LBL_GOLD_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money.ToString("N0"));
    int count = MonoBehaviourSingleton<ShopManager>.I.darkMarketItemList.items.Count;
    int index1 = 1;
    bool flag = false;
    for (int index2 = 0; index2 < 7; ++index2)
    {
      if (index2 < count)
      {
        DarkMarketItem data = MonoBehaviourSingleton<ShopManager>.I.darkMarketItemList.items[index2];
        if (data.feature == 1)
        {
          flag = true;
          this.InitDrakMarketFeatured(data, isReloadIcon);
        }
        else
        {
          this.InitDrakMarketItem(index1, data, isReloadIcon);
          ++index1;
        }
      }
      else
      {
        this.InitDrakMarketItem(index1, (DarkMarketItem) null);
        ++index1;
      }
    }
    if (flag)
      return;
    this.SetActive((Enum) BlackMarketTop.UI.BTN_FEATURED_OFFER, false);
  }

  private void InitDrakMarketFeatured(DarkMarketItem data, bool isReloadIcon = true)
  {
    this.SetActive((Enum) BlackMarketTop.UI.BTN_FEATURED_OFFER, true);
    if (data.saleType == 1)
      this.SetEvent((Enum) BlackMarketTop.UI.BTN_FEATURED_OFFER, "BUY_NORMAL", data.id);
    else if (data.saleType == 2)
      this.SetEvent((Enum) BlackMarketTop.UI.BTN_FEATURED_OFFER, "BUY_NORMAL", data.id);
    else if (data.saleType == 200)
      this.SetEvent((Enum) BlackMarketTop.UI.BTN_FEATURED_OFFER, "BUY_IAP", data.id);
    this.SetFontStyle((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_VALUE, (FontStyle) 2);
    this.SetLabelText((Enum) BlackMarketTop.UI.LBL_OFFER_NAME, data.name);
    if (!string.IsNullOrEmpty(data.refProductId))
    {
      this.SetActive((Enum) BlackMarketTop.UI.LBL_OFFER_OLD_PRICE, true);
      this.SetLabelText((Enum) BlackMarketTop.UI.LBL_OFFER_OLD_PRICE, $"[s]${data.baseNum}[/s]");
      this.SetSupportEncoding((Enum) BlackMarketTop.UI.LBL_OFFER_OLD_PRICE, true);
    }
    else
      this.SetActive((Enum) BlackMarketTop.UI.LBL_OFFER_OLD_PRICE, false);
    this.SetLabelText((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_PRICE, $"${data.saleNum}");
    int num = 100 - Mathf.RoundToInt((float) ((double) data.saleNum / (double) data.baseNum * 100.0));
    if (this._nativeStoreList != null)
    {
      StoreData product1 = this._nativeStoreList.getProduct(data.saleoffProductId);
      if (product1 != null)
        this.SetLabelText((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_PRICE, product1.price.ToString());
      StoreData product2 = this._nativeStoreList.getProduct(data.refProductId);
      if (product2 != null)
        this.SetLabelText((Enum) BlackMarketTop.UI.LBL_OFFER_OLD_PRICE, $"[s]{product2.price}[/s]");
      if (product1 != null && product2 != null)
        num = Mathf.FloorToInt(Mathf.Clamp((float) (100.0 - product1.priceMicros / product2.priceMicros * 100.0), 0.0f, 100f));
    }
    if (num < 30)
      this.SetColor((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_VALUE, this.normalSale);
    else if (num < 70)
      this.SetColor((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_VALUE, this.mediumSale);
    else
      this.SetColor((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_VALUE, this.hotSale);
    if (num > 0)
      this.SetLabelText((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_VALUE, $"-{num}%");
    else
      this.SetLabelText((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_VALUE, $"{num}%");
    if (data.usedCount >= data.limit)
    {
      this.SetSliderValue((Enum) BlackMarketTop.UI.SLD_OFFER_BUY_PROGRESS, 0.0f);
      this.SetActive((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_PROGRESS, false);
      this.SetActive((Enum) BlackMarketTop.UI.OBJ_OFFER_OUT_OFF_STOCK, true);
      this.SetColor((Enum) BlackMarketTop.UI.FEATURED_OFFER_BANNER, this.disableTintColor);
      this.SetButtonEnabled((Enum) BlackMarketTop.UI.BTN_FEATURED_OFFER, false);
    }
    else
    {
      this.SetSliderValue((Enum) BlackMarketTop.UI.SLD_OFFER_BUY_PROGRESS, 1f - (float) data.usedCount / (float) data.limit);
      this.SetActive((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_PROGRESS, true);
      this.SetLabelText((Enum) BlackMarketTop.UI.LBL_OFFER_SALE_PROGRESS, $"{data.limit - data.usedCount}/{data.limit}");
      this.SetActive((Enum) BlackMarketTop.UI.OBJ_OFFER_OUT_OFF_STOCK, false);
      this.SetColor((Enum) BlackMarketTop.UI.FEATURED_OFFER_BANNER, Color.white);
      this.SetButtonEnabled((Enum) BlackMarketTop.UI.BTN_FEATURED_OFFER, true);
    }
    if (!isReloadIcon)
      return;
    UITexture spro = ((Component) this.FindCtrl(this.GetCtrl((Enum) BlackMarketTop.UI.BTN_FEATURED_OFFER), (Enum) BlackMarketTop.UI.FEATURED_OFFER_BANNER)).GetComponent<UITexture>();
    ResourceLoad.LoadBlackMarketOfferTexture(spro, data.imgId, (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) spro, (Object) null))
        return;
      spro.mainTexture = tex;
    }));
  }

  private void InitDrakMarketItem(int index, DarkMarketItem data, bool isReloadIcon = true)
  {
    BlackMarketTop.UI ui = (BlackMarketTop.UI) index;
    if (data == null)
    {
      this.SetActive((Enum) ui, false);
    }
    else
    {
      Transform ctrl = this.GetCtrl((Enum) ui);
      if (data.saleType == 1)
      {
        this.SetEvent((Enum) ui, "BUY_NORMAL", data.id);
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.SPR_GOLD_ITEM_BG, false);
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.SPR_GEM_ITEM_BG, true);
        this.SetLabelText(ctrl, (Enum) BlackMarketTop.UI.LBL_GEM_SALE_PRICE, $"x{data.saleNum}");
      }
      else if (data.saleType == 2)
      {
        this.SetEvent((Enum) ui, "BUY_NORMAL", data.id);
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.SPR_GOLD_ITEM_BG, true);
        this.SetLabelText(ctrl, (Enum) BlackMarketTop.UI.LBL_GOLD_SALE_PRICE, $"x{data.saleNum.ToString("N0")}");
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.SPR_GEM_ITEM_BG, false);
      }
      else if (data.saleType == 200)
        this.SetEvent((Enum) ui, "BUY_IAP", data.id);
      int num1 = 100 - Mathf.RoundToInt((float) ((double) data.saleNum / (double) data.baseNum * 100.0));
      if (num1 < 30)
        this.SetColor(ctrl, (Enum) BlackMarketTop.UI.LBL_SALE_PERCENT, this.normalSale);
      else if (num1 < 70)
        this.SetColor(ctrl, (Enum) BlackMarketTop.UI.LBL_SALE_PERCENT, this.mediumSale);
      else
        this.SetColor(ctrl, (Enum) BlackMarketTop.UI.LBL_SALE_PERCENT, this.hotSale);
      if (num1 > 60)
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.OBJ_HOT, true);
      else
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.OBJ_HOT, false);
      if (num1 > 0)
        this.SetLabelText(ctrl, (Enum) BlackMarketTop.UI.LBL_SALE_PERCENT, $"-{num1}%");
      else
        this.SetLabelText(ctrl, (Enum) BlackMarketTop.UI.LBL_SALE_PERCENT, $"{num1}%");
      this.SetFontStyle(ctrl, (Enum) BlackMarketTop.UI.LBL_SALE_PERCENT, (FontStyle) 2);
      this.SetLabelText(ctrl, (Enum) BlackMarketTop.UI.LBL_ITEM_NAME, data.name);
      if (data.rewards != null && data.rewards.Count > 0)
      {
        string empty = string.Empty;
        string text = data.rewards[0].num <= 1000000 ? (data.rewards[0].num <= 1000 ? $"x{data.rewards[0].num}" : $"x{(float) data.rewards[0].num / 1000f}K") : $"x{(float) data.rewards[0].num / 1000000f}M";
        this.SetLabelText(ctrl, (Enum) BlackMarketTop.UI.LBL_ITEM_SALE_NUM, text);
      }
      else
        this.SetLabelText(ctrl, (Enum) BlackMarketTop.UI.LBL_ITEM_SALE_NUM, string.Empty);
      bool flag1 = false;
      bool flag2;
      if (data.usedCount >= data.limit)
      {
        flag2 = true;
        this.SetSliderValue(ctrl, (Enum) BlackMarketTop.UI.SLD_BUY_PROGRESS, 0.0f);
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.LBL_SALE_PROGRESS, false);
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.OBJ_OUT_OFF_STOCK, true);
      }
      else
      {
        flag2 = false;
        float num2 = 1f - (float) data.usedCount / (float) data.limit;
        this.SetSliderValue(ctrl, (Enum) BlackMarketTop.UI.SLD_BUY_PROGRESS, num2);
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.LBL_SALE_PROGRESS, true);
        this.SetLabelText(ctrl, (Enum) BlackMarketTop.UI.LBL_SALE_PROGRESS, $"{data.limit - data.usedCount}/{data.limit}");
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.OBJ_OUT_OFF_STOCK, false);
      }
      if (!flag2)
      {
        if (data.remain == 0)
        {
          flag1 = true;
          this.SetActive(ctrl, (Enum) BlackMarketTop.UI.OBJ_SOLD_OUT, true);
        }
        else
        {
          flag1 = false;
          this.SetActive(ctrl, (Enum) BlackMarketTop.UI.OBJ_SOLD_OUT, false);
        }
      }
      if (flag1 | flag2)
      {
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.SPR_DISABLE_MASK, true);
        this.SetButtonEnabled((Enum) ui, false);
      }
      else
      {
        this.SetActive(ctrl, (Enum) BlackMarketTop.UI.SPR_DISABLE_MASK, false);
        this.SetButtonEnabled((Enum) ui, true);
      }
      if (!isReloadIcon)
        return;
      UITexture spro = ((Component) this.FindCtrl(ctrl, (Enum) BlackMarketTop.UI.IMG_ICON)).GetComponent<UITexture>();
      ResourceLoad.LoadBlackMarketIconTexture(spro, data.imgId, (Action<Texture>) (tex =>
      {
        if (!Object.op_Inequality((Object) spro, (Object) null))
          return;
        spro.mainTexture = tex;
      }));
    }
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_DARK_MARKET) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (!this.isRelloading && !this.isReseting)
        this.LoadDarkMarketUI(false);
    }
    else if ((flags & GameSection.NOTIFY_FLAG.RESET_DARK_MARKET) != (GameSection.NOTIFY_FLAG) 0 && !string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
    {
      this.timeResetMarket = (int) GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
      if (this.timeResetMarket > 0)
      {
        this.isRelloading = false;
        this.StopAllCoroutines();
        this.StartCoroutine("TimeCountDown");
        this.StartCoroutine("DoResetMarketData");
      }
    }
    base.OnNotify(flags);
  }

  private void UpdateNPC()
  {
    string empty = string.Empty;
    NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(this.sectionData.sectionName + "_TEXT");
    if (section == null)
      return;
    NPCMessageTable.Message message = section.messages[this.currentNPCMessageIndex];
    if (message == null)
      return;
    string message1 = message.message;
    this.SetRenderNPCModel((Enum) BlackMarketTop.UI.TEX_NPCMODEL, message.npc, message.pos, message.rot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>) (loader => loader.GetAnimator().Play(message.animationStateName)));
    this.SetLabelText((Enum) BlackMarketTop.UI.LBL_NPC_MESSAGE, message1);
  }

  private void OnQuery_BUY_NORMAL()
  {
    this.currentItemChoosed = MonoBehaviourSingleton<ShopManager>.I.GetDarkMarketItem((int) GameSection.GetEventData());
    string str = "Gems";
    if (this.currentItemChoosed.saleType == 2)
      str = "Gold";
    GameSection.SetEventData((object) new object[3]
    {
      (object) this.currentItemChoosed.name,
      (object) this.currentItemChoosed.saleNum,
      (object) str
    });
  }

  private void OnQuery_BlackMarketItemConfirm_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ShopManager>.I.SendBuyDarkMarket(this.currentItemChoosed.id, (Action<Error>) (error =>
    {
      switch (error)
      {
        case Error.None:
          GameSection.ChangeStayEvent("BUY_SUCCESS");
          this.StartCoroutine("DoReloadMarketData");
          break;
        case Error.ERR_BM_NOT_ENOUGH_GOLD:
        case Error.ERR_BM_NOT_ENOUGH_GEM:
          GameSection.ChangeStayEvent("BUY_ERROR", (object) StringTable.GetErrorMessage((uint) error));
          GameSection.ResumeEvent(true);
          break;
        case Error.ERR_BM_ITEM_UNAVAILABLE:
        case Error.ERR_BLACK_MARKET_BUY:
        case Error.ERR_BM_ITEM_SOLD_OUT:
          GameSection.ChangeStayEvent("BUY_ERROR", (object) StringTable.GetErrorMessage((uint) error));
          this.StartCoroutine("DoReloadMarketData");
          break;
        default:
          GameSection.ResumeEvent(false);
          break;
      }
    }));
  }

  private IEnumerator DoReloadMarketData()
  {
    this.isRelloading = true;
    bool wait = true;
    MonoBehaviourSingleton<ShopManager>.I.SendGetDarkMarketItemList((Action<bool>) (b => wait = false));
    while (wait)
      yield return (object) null;
    this.LoadDarkMarketUI(false);
    this.isPurchase = false;
    this.isRelloading = false;
    GameSection.ResumeEvent(true);
  }

  private IEnumerator DoResetMarketData()
  {
    this.isReseting = true;
    GameSection.StayEvent();
    bool wait = true;
    MonoBehaviourSingleton<ShopManager>.I.SendGetDarkMarketItemList((Action<bool>) (b => wait = false));
    while (wait)
      yield return (object) null;
    this.LoadDarkMarketUI(true);
    this.isPurchase = false;
    this.isReseting = false;
    GameSection.ResumeEvent(true);
  }

  private void OnQuery_BUY_IAP()
  {
    this.currentItemChoosed = MonoBehaviourSingleton<ShopManager>.I.GetDarkMarketItem((int) GameSection.GetEventData());
    this.SendGoldCanPurchase();
  }

  private void SendGoldCanPurchase()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ShopManager>.I.SendDarkMarketCanPurchase(this.currentItemChoosed.saleoffProductId, this.currentItemChoosed.id, string.Empty, (Action<Error>) (ret =>
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
    Native.RequestPurchase(this.currentItemChoosed.saleoffProductId, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString(), MonoBehaviourSingleton<UserInfoManager>.I.userIdHash);
  }

  private void OnBillingUnavailable()
  {
  }

  private void OnBuyItem(string productId)
  {
    if (!string.IsNullOrEmpty(productId))
    {
      this.isPurchase = false;
      this.SendRequestCurrentPresentAndShopList((System.Action) (() =>
      {
        GameSection.ChangeStayEvent("BUY_SUCCESS");
        this.StartCoroutine("DoReloadMarketData");
      }));
    }
    if (!this.isPurchase)
      return;
    GameSection.ResumeEvent(false);
  }

  private void OnBuyBundle(ShopReceiver.PaymentPurchaseData purchaseData)
  {
    if (purchaseData != null && purchaseData.bundle != null)
    {
      this.isPurchase = false;
      this.SendRequestCurrentPresentAndShopList((System.Action) (() =>
      {
        GameSection.ChangeStayEvent("BUY_SUCCESS");
        this.StartCoroutine("DoReloadMarketData");
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
      this.SendRequestCurrentPresentAndShopList((System.Action) (() =>
      {
        GameSection.ChangeStayEvent("BUY_SUCCESS");
        this.StartCoroutine("DoReloadMarketData");
      }));
    }
    if (!this.isPurchase)
      return;
    GameSection.ResumeEvent(false);
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

  private void OnGetProductDatas(StoreDataList list)
  {
    this._isFinishGetNativeProductlist = true;
    this._nativeStoreList = list;
  }

  private IEnumerator TimeCountDown()
  {
    UILabel timeLbl = ((Component) this.GetCtrl((Enum) BlackMarketTop.UI.LBL_TIME_COUNT)).GetComponent<UILabel>();
    if (string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
    {
      timeLbl.color = Color.red;
      timeLbl.text = "00:00:00";
    }
    else
    {
      this.timeResetMarket = (int) GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
      this.currentNPCMessageIndex = 0;
      for (; this.timeResetMarket > 3600; this.timeResetMarket = (int) GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds)
      {
        timeLbl.text = UIUtility.TimeFormat(this.timeResetMarket, true);
        yield return (object) new WaitForSeconds(0.25f);
      }
      timeLbl.color = Color.red;
      this.currentNPCMessageIndex = 1;
      NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(this.sectionData.sectionName + "_TEXT");
      if (section != null)
      {
        NPCMessageTable.Message message = section.messages[this.currentNPCMessageIndex];
        if (message != null)
          this.SetLabelText((Enum) BlackMarketTop.UI.LBL_NPC_MESSAGE, message.message);
      }
      for (; this.timeResetMarket > 0; this.timeResetMarket = (int) GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds)
      {
        timeLbl.text = this.SecondToTime(this.timeResetMarket);
        yield return (object) new WaitForSeconds(0.25f);
      }
      yield return (object) null;
      MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.UpdateDrakMarketState(false);
      yield return (object) this.StartCoroutine(this._DoCloseDialog());
      GameSection.BackSection();
    }
  }

  private IEnumerator _DoCloseDialog()
  {
    while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      yield return (object) null;
    if (!MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Equals(nameof (BlackMarketTop)))
    {
      GameSection.BackSection();
      yield return (object) this.StartCoroutine(this._DoCloseDialog());
    }
  }

  private string SecondToTime(int time)
  {
    int num1 = time % 60;
    int num2 = time / 60 % 60;
    return $"{time / 3600:D2}:{num2:D2}:{num1:D2}";
  }

  private enum UI
  {
    GRD_MARKET_ITEM_1 = 1,
    GRD_MARKET_ITEM_2 = 2,
    GRD_MARKET_ITEM_3 = 3,
    GRD_MARKET_ITEM_4 = 4,
    GRD_MARKET_ITEM_5 = 5,
    GRD_MARKET_ITEM_6 = 6,
    LBL_TIME_COUNT = 7,
    TEX_NPCMODEL = 8,
    LBL_NPC_MESSAGE = 9,
    LBL_GOLD_NUM = 10, // 0x0000000A
    LBL_CRYSTAL_NUM = 11, // 0x0000000B
    SPR_GOLD_ITEM_BG = 12, // 0x0000000C
    LBL_GOLD_SALE_PRICE = 13, // 0x0000000D
    SPR_GEM_ITEM_BG = 14, // 0x0000000E
    LBL_GEM_SALE_PRICE = 15, // 0x0000000F
    LBL_ITEM_SALE_NUM = 16, // 0x00000010
    BTN_FEATURED_OFFER = 17, // 0x00000011
    FEATURED_OFFER_BANNER = 18, // 0x00000012
    LBL_OFFER_NAME = 19, // 0x00000013
    LBL_OFFER_SALE_VALUE = 20, // 0x00000014
    LBL_OFFER_SALE_PROGRESS = 21, // 0x00000015
    SLD_OFFER_BUY_PROGRESS = 22, // 0x00000016
    OBJ_OFFER_OUT_OFF_STOCK = 23, // 0x00000017
    LBL_OFFER_OLD_PRICE = 24, // 0x00000018
    LBL_OFFER_SALE_PRICE = 25, // 0x00000019
    LBL_SALE_PERCENT = 26, // 0x0000001A
    LBL_ITEM_NAME = 27, // 0x0000001B
    OBJ_SOLD = 28, // 0x0000001C
    OBJ_OUT_OFF_STOCK = 29, // 0x0000001D
    SLD_BUY_PROGRESS = 30, // 0x0000001E
    LBL_SALE_PROGRESS = 31, // 0x0000001F
    OBJ_HOT = 32, // 0x00000020
    IMG_ICON = 33, // 0x00000021
    SPR_DISABLE_MASK = 34, // 0x00000022
    OBJ_SOLD_OUT = 35, // 0x00000023
  }

  public enum SALE_TYPE
  {
    GEM = 1,
    MONEY = 2,
    IAP = 200, // 0x000000C8
  }
}
