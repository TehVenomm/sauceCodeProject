using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrystalShopTop : GameSection
{
	private enum VIEW_TYPE
	{
		GEM,
		BUNDLE,
		MATERIAL
	}

	public enum PRODUCT_TYPE
	{
		Gem = 1,
		Bundle,
		Material,
		Gacha
	}

	public enum OFFER_TYPE
	{
		GoodBuy = 1,
		BestValue,
		OneTimeOffer,
		BestDeal,
		MostPopular,
		BlackFriday
	}

	public enum BUNDLE_TYPE
	{
		OneTimesOnly = 1,
		OneTimesPerMonth,
		FiveTimesPerWeek,
		ThreeTimesOnly,
		FiveTimesOnly,
		Unlimited,
		FiveTimesPerWeekRound,
		OneTimeLeft,
		TwoTimesLeft,
		ThreeTimesLeft,
		FourTimesLeft,
		FiveTimesLeft
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
		LBL_SERVICE_MESSAGE
	}

	private const string TRADING_POST_LICENSE = "TradingPostLicense";

	private List<ProductData> _purchaseGemList = new List<ProductData>();

	private List<ProductData> _purchaseBundleList = new List<ProductData>();

	private List<ProductData> _purchaseMaterialList = new List<ProductData>();

	private Transform gemTab;

	private Transform bundleTab;

	private Transform materialTab;

	private int _currentPageIndex;

	private Transform _objBundle;

	private VIEW_TYPE _viewType;

	private bool _isFinishGetNativeProductlist;

	private StoreDataList _nativeStoreList;

	private object[] selectEventData;

	private ProductData selectProductData;

	private bool isPurchase;

	private string pp;

	private bool isSuccessBuyClose;

	private bool isSuccessCrystalBuy;

	private int currentCrystalRequestCount;

	private bool isHighlightMaterial;

	private bool isHighlightBundle;

	public override void Initialize()
	{
		_objBundle = GetCtrl(UI.OBJ_BUNDLE);
		ShopReceiver i = MonoBehaviourSingleton<ShopReceiver>.I;
		i.onBillingUnavailable = (Action)Delegate.Combine(i.onBillingUnavailable, new Action(onBillingUnavailable));
		ShopReceiver i2 = MonoBehaviourSingleton<ShopReceiver>.I;
		i2.onBuyItem = (Action<string>)Delegate.Combine(i2.onBuyItem, new Action<string>(OnBuyItem));
		ShopReceiver i3 = MonoBehaviourSingleton<ShopReceiver>.I;
		i3.onBuySpecialItem = (Action<ShopReceiver.PaymentPurchaseData>)Delegate.Combine(i3.onBuySpecialItem, new Action<ShopReceiver.PaymentPurchaseData>(OnBuyBundle));
		ShopReceiver i4 = MonoBehaviourSingleton<ShopReceiver>.I;
		i4.onBuyMaterialItem = (Action<ShopReceiver.PaymentPurchaseData>)Delegate.Combine(i4.onBuyMaterialItem, new Action<ShopReceiver.PaymentPurchaseData>(OnBuyMaterial));
		ShopReceiver i5 = MonoBehaviourSingleton<ShopReceiver>.I;
		i5.onGetProductDatas = (Action<StoreDataList>)Delegate.Combine(i5.onGetProductDatas, new Action<StoreDataList>(OnGetProductDatas));
		StartCoroutine(DoInitialize());
		isPurchase = false;
	}

	private IEnumerator DoInitialize()
	{
		bool wait = true;
		MonoBehaviourSingleton<ShopManager>.I.SendGetGoldPurchaseItemList(delegate
		{
			wait = false;
		});
		while (wait)
		{
			yield return null;
		}
		if (_nativeStoreList != null && _nativeStoreList.shopList != null && _nativeStoreList.shopList.Count > 0)
		{
			_isFinishGetNativeProductlist = true;
		}
		else
		{
			List<string> list = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Select((ProductData o) => o.productId).ToList();
			Native.GetProductDatas(string.Join("----", list.ToArray()));
		}
		while (!_isFinishGetNativeProductlist)
		{
			yield return null;
		}
		_purchaseGemList = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Where((ProductData o) => o.productType == 1).ToList();
		_purchaseBundleList = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Where((ProductData o) => o.productType == 2).ToList();
		_purchaseMaterialList = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList.Where((ProductData o) => o.productType == 3).ToList();
		gemTab = GetCtrl(UI.OBJ_GEM_TAB);
		bundleTab = GetCtrl(UI.OBJ_BUNDLE_TAB);
		materialTab = GetCtrl(UI.OBJ_MATERIAL_TAB);
		MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("IAP_open", "Functionality");
		CheckHightlightBtnTab();
		string pId = GameSection.GetEventData() as string;
		if (pId != null)
		{
			_viewType = VIEW_TYPE.BUNDLE;
			int num = _purchaseBundleList.FindIndex((ProductData _purchaseBundle) => _purchaseBundle.productId == pId);
			if (num >= 0)
			{
				_currentPageIndex = num;
			}
		}
		SetActive(UI.LBL_SERVICE_MESSAGE, MonoBehaviourSingleton<AccountManager>.I.usageLimitMode);
		SetLabelText(UI.LBL_SERVICE_MESSAGE, string.Format(base.sectionData.GetText("SERVICE_LIMITED")));
		base.Initialize();
	}

	protected override void OnDestroy()
	{
		if (MonoBehaviourSingleton<ShopReceiver>.IsValid())
		{
			ShopReceiver i = MonoBehaviourSingleton<ShopReceiver>.I;
			i.onBillingUnavailable = (Action)Delegate.Remove(i.onBillingUnavailable, new Action(onBillingUnavailable));
			ShopReceiver i2 = MonoBehaviourSingleton<ShopReceiver>.I;
			i2.onBuyItem = (Action<string>)Delegate.Remove(i2.onBuyItem, new Action<string>(OnBuyItem));
			ShopReceiver i3 = MonoBehaviourSingleton<ShopReceiver>.I;
			i3.onBuySpecialItem = (Action<ShopReceiver.PaymentPurchaseData>)Delegate.Remove(i3.onBuySpecialItem, new Action<ShopReceiver.PaymentPurchaseData>(OnBuyBundle));
			ShopReceiver i4 = MonoBehaviourSingleton<ShopReceiver>.I;
			i4.onBuyMaterialItem = (Action<ShopReceiver.PaymentPurchaseData>)Delegate.Remove(i4.onBuyMaterialItem, new Action<ShopReceiver.PaymentPurchaseData>(OnBuyMaterial));
			ShopReceiver i5 = MonoBehaviourSingleton<ShopReceiver>.I;
			i5.onGetProductDatas = (Action<StoreDataList>)Delegate.Remove(i5.onGetProductDatas, new Action<StoreDataList>(OnGetProductDatas));
		}
		base.OnDestroy();
	}

	public override void StartSection()
	{
	}

	public override void UpdateUI()
	{
		_updateTab();
	}

	private void _updateTab()
	{
		switch (_viewType)
		{
		default:
			_viewGemTab();
			break;
		case VIEW_TYPE.BUNDLE:
			_viewBundleTab();
			break;
		case VIEW_TYPE.MATERIAL:
			_viewMaterialTab();
			break;
		}
	}

	private void _viewGemTab()
	{
		SetActive(gemTab, is_visible: true);
		SetActive(bundleTab, is_visible: false);
		SetActive(materialTab, is_visible: false);
		CheckOpenedGemTab();
		int j = 0;
		SetTable(gemTab, UI.TBL_LIST, "CrystalShopListItem", _purchaseGemList.Count, reset: false, delegate(int i, Transform p)
		{
			ProductData productData2 = _purchaseGemList[j];
			return (MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.HasSpecial(productData2.productId) || (MonoBehaviourSingleton<GoGameSettingsManager>.IsValid() && MonoBehaviourSingleton<GoGameSettingsManager>.I.UseShopUI3(productData2.productId))) ? Realizes("CrystalShopListItem2", p) : null;
		}, delegate(int i, Transform t, bool b)
		{
			ProductData productData = _purchaseGemList[j++];
			SetSprite(t, UI.SPR_THUMB, productData.iconImg);
			SetLabelText(t, UI.LBL_NAME, productData.name);
			SetLabelText(t, UI.LBL_PRICE, string.Format(base.sectionData.GetText("PRICE"), productData.priceIncludeTax));
			SetSupportEncoding(t, UI.LBL_PROMO, isEnable: true);
			SetLabelText(t, UI.LBL_PROMO, productData.promo.Replace("\\n", "\n"));
			if (productData.remainingDay > 0)
			{
				SetActive(t, UI.SPR_SOLD, is_visible: true);
				SetActive(t, UI.SPR_SOLD_MASK, is_visible: true);
			}
			else
			{
				SetActive(t, UI.SPR_SOLD, is_visible: false);
				SetActive(t, UI.SPR_SOLD_MASK, is_visible: false);
				if (productData.offerType > 0)
				{
					UITexture spro = FindCtrl(t, UI.OBJ_OFFER).GetComponent<UITexture>();
					ResourceLoad.LoadShopImageGemOfferTexture(spro, (uint)productData.offerType, delegate(Texture tex)
					{
						if (spro != null)
						{
							spro.mainTexture = tex;
						}
					});
				}
			}
			if (_nativeStoreList != null)
			{
				StoreData product = _nativeStoreList.getProduct(productData.productId);
				if (product != null)
				{
					SetLabelText(t, UI.LBL_PRICE, product.price.ToString());
				}
			}
			SetEvent(t, "BUY", i);
		});
	}

	private void _viewBundleTab()
	{
		SetActive(gemTab, is_visible: false);
		SetActive(materialTab, is_visible: false);
		SetActive(bundleTab, is_visible: true);
		CheckOpenedBundleTab();
		if (_purchaseBundleList.Count == 0)
		{
			SetActive(UI.OBJ_AIM, is_visible: false);
			SetActive(UI.TEX_NPCMODEL, is_visible: false);
			SetActive(UI.SPR_FRAME_DRAGON, is_visible: false);
			SetActive(UI.LBL_NONE, is_visible: true);
			return;
		}
		SetActive(UI.OBJ_AIM, is_visible: true);
		SetActive(UI.TEX_NPCMODEL, is_visible: true);
		SetActive(UI.SPR_FRAME_DRAGON, is_visible: true);
		if (_currentPageIndex < _purchaseBundleList.Count)
		{
			ProductData productData = _purchaseBundleList[_currentPageIndex];
			ProductDataTable.PackInfo pack = Singleton<ProductDataTable>.I.GetPack(productData.productId);
			Transform root = SetPrefab(_objBundle, MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.prefabBundleName);
			Utility.ToggleActiveChildren(_objBundle, _currentPageIndex);
			UITexture sprw = FindCtrl(root, UI.SPR_WINDOW).GetComponent<UITexture>();
			string shopImageName = ResourceName.GetShopImageName((int)pack.bundleImageId);
			Hash128 hash = default(Hash128);
			if (MonoBehaviourSingleton<ResourceManager>.I.event_manifest != null)
			{
				hash = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.SHOP_IMG.ToAssetBundleName(shopImageName));
			}
			if (!hash.isValid)
			{
				sprw.mainTexture = (Resources.Load("Texture/White") as Texture);
			}
			else
			{
				ResourceLoad.LoadShopImageTexture(sprw, pack.bundleImageId, delegate(Texture tex)
				{
					if (sprw != null)
					{
						sprw.mainTexture = tex;
					}
				});
			}
			SetTexture(root, UI.SPR_OFFER, null);
			if (productData.offerType > 0)
			{
				UITexture spro = FindCtrl(root, UI.SPR_OFFER).GetComponent<UITexture>();
				ResourceLoad.LoadShopImageOfferTexture(sprw, (uint)productData.offerType, delegate(Texture tex)
				{
					if (sprw != null)
					{
						spro.width = tex.width;
						spro.height = tex.height;
						spro.mainTexture = tex;
					}
				});
			}
			RemoveModel(root, UI.OBJ_MODEL);
			if (!string.IsNullOrEmpty(pack.chestName))
			{
				SetModel(root, UI.OBJ_MODEL, pack.chestName);
			}
			SetActive(GetChildSafe(root, UI.OBJ_OFFER, productData.offerType - 1), is_visible: true);
			if (productData.remainingDay > 0)
			{
				SetButtonEnabled(root, UI.BTN_BUY, is_enabled: false);
				SetActive(root, UI.LBL_BUNDLE_PRICE, is_visible: false);
				SetActive(root, UI.LBL_DAY_LEFT, is_visible: true);
				SetLabelText(root, UI.LBL_DAY_LEFT, (productData.remainingDay > 1) ? string.Format(base.sectionData.GetText("DAYS_LEFT"), productData.remainingDay) : string.Format(base.sectionData.GetText("DAY_LEFT"), productData.remainingDay));
			}
			else
			{
				SetButtonEnabled(root, UI.BTN_BUY, is_enabled: true);
				SetActive(root, UI.LBL_BUNDLE_PRICE, is_visible: true);
				SetActive(root, UI.LBL_DAY_LEFT, is_visible: false);
			}
			SetLabelText(root, UI.LBL_BUNDLE_PRICE, string.Format(base.sectionData.GetText("PRICE"), productData.priceIncludeTax));
			if (_nativeStoreList != null)
			{
				StoreData product = _nativeStoreList.getProduct(productData.productId);
				if (product != null)
				{
					SetLabelText(root, UI.LBL_BUNDLE_PRICE, product.price.ToString());
				}
			}
		}
		SetLabelText(UI.LBL_CURRENT, (_purchaseBundleList.Count > 0) ? (_currentPageIndex + 1) : _currentPageIndex);
		SetLabelText(UI.LBL_TOTAL, _purchaseBundleList.Count);
		bool flag = _currentPageIndex > 0;
		bool flag2 = _currentPageIndex < _purchaseBundleList.Count - 1;
		SetColor(UI.SPR_AIM_L, flag ? Color.white : Color.clear);
		SetColor(UI.SPR_AIM_R, flag2 ? Color.white : Color.clear);
		SetButtonEnabled(UI.BTN_AIM_L, flag);
		SetButtonEnabled(UI.BTN_AIM_R, flag2);
		SetActive(UI.BTN_AIM_L_INACTIVE, !flag);
		SetActive(UI.BTN_AIM_R_INACTIVE, !flag2);
		SetRepeatButton(UI.BTN_AIM_L, "AIM_L");
		SetRepeatButton(UI.BTN_AIM_R, "AIM_R");
		_updateNPC();
	}

	private void _viewMaterialTab()
	{
		SetActive(gemTab, is_visible: false);
		SetActive(bundleTab, is_visible: false);
		SetActive(materialTab, is_visible: true);
		CheckOpenedMaterialTab();
		int j = 0;
		SetTable(materialTab, UI.TBL_LIST, "CrystalShopListItemMaterial", _purchaseMaterialList.Count, reset: false, delegate(int i, Transform t, bool b)
		{
			ProductData productData = _purchaseMaterialList[j];
			string text = string.Format(base.sectionData.GetText("PRICE"), productData.priceIncludeTax);
			UITexture spro = FindCtrl(t, UI.SPR_THUMB).GetComponent<UITexture>();
			ResourceLoad.LoadShopImageMaterialTexture(spro, productData.iconImg, delegate(Texture tex)
			{
				if (spro != null)
				{
					spro.mainTexture = tex;
				}
			});
			SetLabelText(t, UI.LBL_NAME, productData.name);
			SetLabelText(t, UI.LBL_PRICE, text);
			SetLabelText(t, UI.LBL_PRICE_OLD, (productData.oldPrice > 0.0) ? string.Format(base.sectionData.GetText("PRICE_STRETCH"), productData.oldPrice) : string.Empty);
			SetSupportEncoding(t, UI.LBL_PRICE_OLD, isEnable: true);
			SetSupportEncoding(t, UI.LBL_PROMO, isEnable: true);
			SetLabelText(t, UI.LBL_PROMO, productData.promo.Replace("\\n", "\n"));
			if (productData.remainingDay > 0)
			{
				string empty = string.Empty;
				TimeSpan timeSpan = TimeSpan.FromSeconds(productData.remainingDay);
				empty = ((timeSpan.Days <= 1) ? string.Format(base.sectionData.GetText("TIME_REMAIN"), $"{timeSpan.Hours:d2}:{timeSpan.Minutes:d2}:{timeSpan.Seconds:d2}") : string.Format(base.sectionData.GetText("DAY_REMAIN"), timeSpan.Days));
				SetLabelText(t, UI.LBL_REMAIN, empty);
			}
			else
			{
				SetActive(t, UI.LBL_REMAIN, is_visible: false);
			}
			SetActive(GetChildSafe(t, UI.OBJ_OFFER, productData.offerType - 1), is_visible: true);
			if (_nativeStoreList != null)
			{
				StoreData product = _nativeStoreList.getProduct(productData.productId);
				if (product != null)
				{
					text = product.price.ToString();
					SetLabelText(t, UI.LBL_PRICE, text);
				}
				product = _nativeStoreList.getProduct(productData.skipId);
				if (product != null)
				{
					SetLabelText(t, UI.LBL_PRICE_OLD, product.price.ToString());
				}
			}
			SetEvent(t, "MATERIAL_DETAIL", new object[3]
			{
				productData,
				text,
				j
			});
			j++;
		});
	}

	private void OnGetProductDatas(StoreDataList list)
	{
		_isFinishGetNativeProductlist = true;
		_nativeStoreList = list;
	}

	private void _updateNPC()
	{
		NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(base.sectionData.sectionName + "_TEXT");
		if (section != null)
		{
			NPCMessageTable.Message message = section.GetNPCMessage();
			if (message != null)
			{
				SetRenderNPCModel(UI.TEX_NPCMODEL, message.npc, message.pos, message.rot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, delegate(NPCLoader loader)
				{
					loader.GetAnimator().Play(message.animationStateName);
				});
			}
		}
	}

	private void _disableChest()
	{
		if (!(_objBundle == null))
		{
			foreach (Transform item in _objBundle)
			{
				SetActiveModel(item, UI.OBJ_MODEL, active: false);
			}
		}
	}

	private void _enableChest()
	{
		if (!(_objBundle == null) && _viewType != 0 && _viewType != VIEW_TYPE.MATERIAL)
		{
			foreach (Transform item in _objBundle)
			{
				SetActiveModel(item, UI.OBJ_MODEL, active: true);
			}
		}
	}

	private void OnQuery_GEM_TAB()
	{
		_viewType = VIEW_TYPE.GEM;
		RefreshUI();
	}

	private void OnQuery_BUNDLE_TAB()
	{
		_viewType = VIEW_TYPE.BUNDLE;
		RefreshUI();
	}

	private void OnQuery_MATERIAL_TAB()
	{
		_viewType = VIEW_TYPE.MATERIAL;
		RefreshUI();
	}

	private void OnQuery_AIM_R()
	{
		_currentPageIndex++;
		RefreshUI();
	}

	private void OnQuery_AIM_L()
	{
		_currentPageIndex--;
		RefreshUI();
	}

	private void OnQuery_CrystalShopMaterialDetail_Buy()
	{
		OnQuery_BUY();
	}

	private void OnQuery_BUY()
	{
		switch (_viewType)
		{
		case VIEW_TYPE.GEM:
			selectProductData = _purchaseGemList[(int)GameSection.GetEventData()];
			break;
		case VIEW_TYPE.BUNDLE:
			selectProductData = _purchaseBundleList[_currentPageIndex];
			_disableChest();
			break;
		case VIEW_TYPE.MATERIAL:
			selectProductData = _purchaseMaterialList[(int)GameSection.GetEventData()];
			break;
		}
		selectEventData = new object[7]
		{
			GameSection.GetEventData(),
			selectProductData,
			selectProductData.name,
			selectProductData.discount,
			selectProductData.price,
			10,
			5
		};
		if (MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.HasSpecial(selectProductData.productId) && selectProductData.remainingDay > 0)
		{
			GameSection.ChangeEvent("SPECIAL_SOLD", selectProductData);
			return;
		}
		pp = string.Empty;
		if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isParentPassSet != 0)
		{
			GameSection.ChangeEvent("PP_INPUT");
		}
		else
		{
			SendGoldCanPurchase();
		}
	}

	private void OnQuery_PP_TO_BUY()
	{
		pp = (GameSection.GetEventData() as string);
		SendGoldCanPurchase();
	}

	private void OnQuery_CrystalShopStopper_YES()
	{
		RequestEvent("STOPPER_TO_BUY");
	}

	private void OnQuery_STOPPER_TO_BUY()
	{
		GameSection.SetEventData(selectEventData);
		GameSection.StayEvent();
		DoPurchase();
	}

	private void OnQuery_CURRENCY()
	{
		GameSection.ChangeEvent("INFO", WebViewManager.Currency);
	}

	private void OnQuery_COMMERCIAL()
	{
		GameSection.ChangeEvent("INFO", WebViewManager.Commercial);
	}

	private void OnQuery_FUND()
	{
		GameSection.ChangeEvent("INFO", WebViewManager.Found);
	}

	private void OnQuery_EULA()
	{
		GameSection.ChangeEvent("INFO", WebViewManager.eula);
	}

	private void OnQuery_SECTION_BACK()
	{
		if (!isSuccessBuyClose)
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("IAP_close", "Functionality");
		}
		isSuccessBuyClose = false;
	}

	private void OnCloseDialog_CrystalShopMessage()
	{
		OnCloseBuyDialog();
	}

	private void OnCloseDialog_CrystalShopDebugMessage()
	{
		OnCloseBuyDialog();
	}

	private void OnCloseDialog_CrystalShopSpecialNotice()
	{
		OnCloseBuyDialog();
	}

	private void OnCloseDialog_CrystalShopBundleNotice()
	{
		OnCloseBuyDialog();
	}

	private void OnCloseDialog_CrystalShopPendingNotice()
	{
		OnCloseBuyDialog();
	}

	private void OnCloseBuyDialog()
	{
		isSuccessBuyClose = true;
		isSuccessCrystalBuy = true;
		MonoBehaviourSingleton<ShopManager>.I.SendGetGoldPurchaseItemList(null);
	}

	public void Update()
	{
		if (isSuccessCrystalBuy && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			isSuccessCrystalBuy = false;
			GameSection.BackSection();
		}
	}

	private void SendGoldCanPurchase()
	{
		GameSection.SetEventData(selectEventData);
		GameSection.StayEvent();
		MonoBehaviourSingleton<ShopManager>.I.SendGoldCanPurchase(selectProductData.productId, pp, delegate(Error ret)
		{
			switch (ret)
			{
			case Error.WRN_GOLD_OVER_LIMITTER_OVERUSE:
				GameSection.ChangeStayEvent("STOPPER");
				GameSection.ResumeEvent(is_resume: true);
				break;
			default:
				GameSection.ResumeEvent(is_resume: false);
				break;
			case Error.None:
				DoPurchase();
				break;
			}
		});
	}

	private void DoPurchase()
	{
		isPurchase = true;
		Native.RequestPurchase(selectProductData.productId, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString(), MonoBehaviourSingleton<UserInfoManager>.I.userIdHash);
	}

	private void onBillingUnavailable()
	{
		isPurchase = false;
		GameSection.ChangeStayEvent("BILLING_UNAVAILABLE");
		GameSection.ResumeEvent(is_resume: true);
	}

	private void OnBuyItem(string productId)
	{
		if (!string.IsNullOrEmpty(productId))
		{
			if (MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.HasSpecial(productId))
			{
				OnBuySpecial(productId);
				return;
			}
			isPurchase = false;
			int index = 0;
			ProductData data = null;
			MonoBehaviourSingleton<ShopManager>.I.GetPurchaseItem(productId, ref data, ref index);
			if (data != null)
			{
				Action finishAction = GetFinishAction(index, data);
				SendRequestCurrentCrystal(finishAction);
				return;
			}
		}
		if (isPurchase)
		{
			_enableChest();
			GameSection.ResumeEvent(is_resume: false);
		}
	}

	private void OnBuyBundle(ShopReceiver.PaymentPurchaseData purchaseData)
	{
		if (purchaseData != null && purchaseData.bundle != null)
		{
			isPurchase = false;
			Action finishActionBundle = GetFinishActionBundle(purchaseData);
			SendRequestCurrentPresentAndShopList(finishActionBundle);
		}
		if (isPurchase)
		{
			_enableChest();
			GameSection.ResumeEvent(is_resume: false);
		}
	}

	private void OnBuySpecial(string productId)
	{
		if (!string.IsNullOrEmpty(productId))
		{
			isPurchase = false;
			int index = 0;
			ProductData product_data = null;
			MonoBehaviourSingleton<ShopManager>.I.GetPurchaseItem(productId, ref product_data, ref index);
			if (product_data != null)
			{
				MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, delegate
				{
					_disableChest();
					if (!GameSceneEvent.IsStay())
					{
						GameSection.StayEvent();
					}
					if (product_data.skipId == "TradingPostLicense")
					{
						MonoBehaviourSingleton<TradingPostManager>.I.tradingStatus = 2;
						GameSection.ChangeStayEvent("TRADING_POST_LICENSE");
					}
					else
					{
						GameSection.ChangeStayEvent("SPECIAL_NOTICE", product_data);
					}
					GameSection.ResumeEvent(is_resume: true);
				});
			}
		}
		if (isPurchase)
		{
			GameSection.ResumeEvent(is_resume: false);
		}
	}

	private void OnBuyMaterial(ShopReceiver.PaymentPurchaseData purchaseData)
	{
		if (purchaseData != null)
		{
			isPurchase = false;
			int index = 0;
			ProductData data = null;
			MonoBehaviourSingleton<ShopManager>.I.GetPurchaseItem(purchaseData.productId, ref data, ref index);
			Action onFinish = GetFinishAction(index, data);
			MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, delegate
			{
				onFinish();
			});
		}
		if (isPurchase)
		{
			GameSection.ResumeEvent(is_resume: false);
		}
	}

	private Action GetFinishAction(int index, ProductData product_data)
	{
		return delegate
		{
			_disableChest();
			if (!GameSceneEvent.IsStay())
			{
				GameSection.StayEvent();
			}
			if (product_data.skipId == "TradingPostLicense")
			{
				MonoBehaviourSingleton<TradingPostManager>.I.tradingStatus = 2;
				GameSection.ChangeStayEvent("TRADING_POST_LICENSE");
			}
			else
			{
				GameSection.ChangeStayEvent("BUY", new object[7]
				{
					index,
					product_data,
					product_data.name,
					product_data.discount,
					product_data.price,
					10,
					5
				});
			}
			GameSection.ResumeEvent(is_resume: true);
		};
	}

	private Action GetFinishActionBundle(ShopReceiver.PaymentPurchaseData purchaseData)
	{
		return delegate
		{
			_disableChest();
			ProductDataTable.PackInfo pack = Singleton<ProductDataTable>.I.GetPack(purchaseData.productId);
			GameSection.ChangeStayEvent((!string.IsNullOrEmpty(pack.eventName)) ? pack.eventName : "BUNDLE_NOTICE", purchaseData);
			GameSection.ResumeEvent(is_resume: true);
		};
	}

	private Action GetFinishActionMaterial(ShopReceiver.PaymentPurchaseData purchaseData)
	{
		return delegate
		{
			GameSection.ChangeStayEvent("BUNDLE_NOTICE", purchaseData);
			GameSection.ResumeEvent(is_resume: true);
		};
	}

	private void SendRequestCurrentCrystal(Action onFinish)
	{
		Protocol.Send(OnceStatusInfoModel.URL, delegate(OnceStatusInfoModel result)
		{
			CheckCrystalNum(result, onFinish);
		});
	}

	private void SendRequestCurrentPresentAndShopList(Action onFinish)
	{
		SendRequestCurrentCrystal(delegate
		{
			MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, delegate
			{
				onFinish();
			});
		});
	}

	private void CheckCrystalNum(OnceStatusInfoModel ret, Action onFinish)
	{
		if (ret.Error == Error.None)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal = ret.result.userStatus.crystal;
			MonoBehaviourSingleton<UserInfoManager>.I.DirtyUserStatus();
			onFinish();
		}
	}

	private void CheckHightlightBtnTab()
	{
		isHighlightMaterial = false;
		isHighlightBundle = false;
		string @string = PlayerPrefs.GetString("Purchase_Item_List_Tab_Bundle", string.Empty);
		string string2 = PlayerPrefs.GetString("Purchase_Item_List_Tab_Material", string.Empty);
		int count = _purchaseMaterialList.Count;
		for (int i = 0; i < count; i++)
		{
			if (string2.IndexOf(_purchaseMaterialList[i].productId) < 0)
			{
				isHighlightMaterial = true;
				break;
			}
		}
		count = _purchaseBundleList.Count;
		int num = 0;
		while (true)
		{
			if (num < count)
			{
				if (@string.IndexOf(_purchaseBundleList[num].productId) < 0)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		isHighlightBundle = true;
	}

	private void CheckOpenedGemTab()
	{
		Transform root = FindCtrl(gemTab, UI.SPR_BTN_BORDER);
		Transform t = FindCtrl(root, UI.BTN_MATERIAL_TAB);
		Transform t2 = FindCtrl(root, UI.BTN_BUNDLE_TAB);
		if (isHighlightMaterial)
		{
			SetBadge(t, -1, SpriteAlignment.TopRight, -8, -8);
		}
		else
		{
			SetBadge(t, 0, SpriteAlignment.TopRight, -8, -8);
		}
		if (isHighlightBundle)
		{
			SetBadge(t2, -1, SpriteAlignment.TopRight, -8, -8);
		}
		else
		{
			SetBadge(t2, 0, SpriteAlignment.TopRight, -8, -8);
		}
		string text = PlayerPrefs.GetString("Purchase_Item_List_Tab_Gem", string.Empty);
		int count = _purchaseGemList.Count;
		for (int i = 0; i < count; i++)
		{
			if (text.IndexOf(_purchaseGemList[i].productId) < 0)
			{
				text = text + "/" + _purchaseGemList[i].productId;
			}
		}
		PlayerPrefs.SetString("Purchase_Item_List_Tab_Gem", text);
	}

	private void CheckOpenedMaterialTab()
	{
		isHighlightMaterial = false;
		Transform root = FindCtrl(materialTab, UI.SPR_BTN_BORDER);
		Transform t = FindCtrl(root, UI.BTN_MATERIAL_TAB);
		Transform t2 = FindCtrl(root, UI.BTN_BUNDLE_TAB);
		SetBadge(t, 0, SpriteAlignment.TopRight, -8, -8);
		if (isHighlightBundle)
		{
			SetBadge(t2, -1, SpriteAlignment.TopRight, -8, -8);
		}
		else
		{
			SetBadge(t2, 0, SpriteAlignment.TopRight, -8, -8);
		}
		string text = PlayerPrefs.GetString("Purchase_Item_List_Tab_Material", string.Empty);
		int count = _purchaseMaterialList.Count;
		for (int i = 0; i < count; i++)
		{
			if (text.IndexOf(_purchaseMaterialList[i].productId) < 0)
			{
				text = text + "/" + _purchaseMaterialList[i].productId;
			}
		}
		PlayerPrefs.SetString("Purchase_Item_List_Tab_Material", text);
	}

	private void CheckOpenedBundleTab()
	{
		isHighlightBundle = false;
		Transform root = FindCtrl(bundleTab, UI.SPR_BTN_BORDER);
		Transform t = FindCtrl(root, UI.BTN_MATERIAL_TAB);
		Transform t2 = FindCtrl(root, UI.BTN_BUNDLE_TAB);
		if (isHighlightMaterial)
		{
			SetBadge(t, -1, SpriteAlignment.TopRight, -8, -8);
		}
		else
		{
			SetBadge(t, 0, SpriteAlignment.TopRight, -8, -8);
		}
		SetBadge(t2, 0, SpriteAlignment.TopRight, -8, -8);
		string text = PlayerPrefs.GetString("Purchase_Item_List_Tab_Bundle", string.Empty);
		int count = _purchaseBundleList.Count;
		for (int i = 0; i < count; i++)
		{
			if (text.IndexOf(_purchaseBundleList[i].productId) < 0)
			{
				text = text + "/" + _purchaseBundleList[i].productId;
			}
		}
		PlayerPrefs.SetString("Purchase_Item_List_Tab_Bundle", text);
	}
}
