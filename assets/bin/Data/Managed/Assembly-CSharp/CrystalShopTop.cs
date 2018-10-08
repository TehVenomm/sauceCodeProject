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
		SPR_ONETIMESOFFER
	}

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

	public unsafe override void Initialize()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		_objBundle = GetCtrl(UI.OBJ_BUNDLE);
		ShopReceiver i = MonoBehaviourSingleton<ShopReceiver>.I;
		i.onBillingUnavailable = Delegate.Combine((Delegate)i.onBillingUnavailable, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		ShopReceiver i2 = MonoBehaviourSingleton<ShopReceiver>.I;
		i2.onBuyItem = (Action<string>)Delegate.Combine(i2.onBuyItem, new Action<string>(OnBuyItem));
		ShopReceiver i3 = MonoBehaviourSingleton<ShopReceiver>.I;
		i3.onBuySpecialItem = (Action<ShopReceiver.PaymentPurchaseData>)Delegate.Combine(i3.onBuySpecialItem, new Action<ShopReceiver.PaymentPurchaseData>(OnBuyBundle));
		ShopReceiver i4 = MonoBehaviourSingleton<ShopReceiver>.I;
		i4.onBuyMaterialItem = (Action<ShopReceiver.PaymentPurchaseData>)Delegate.Combine(i4.onBuyMaterialItem, new Action<ShopReceiver.PaymentPurchaseData>(OnBuyMaterial));
		ShopReceiver i5 = MonoBehaviourSingleton<ShopReceiver>.I;
		i5.onGetProductDatas = (Action<StoreDataList>)Delegate.Combine(i5.onGetProductDatas, new Action<StoreDataList>(OnGetProductDatas));
		this.StartCoroutine(DoInitialize());
		isPurchase = false;
	}

	private unsafe IEnumerator DoInitialize()
	{
		bool wait = true;
		MonoBehaviourSingleton<ShopManager>.I.SendGetGoldPurchaseItemList(delegate
		{
			((_003CDoInitialize_003Ec__Iterator39)/*Error near IL_0031: stateMachine*/)._003Cwait_003E__0 = false;
		});
		while (wait)
		{
			yield return (object)null;
		}
		if (_nativeStoreList != null && _nativeStoreList.shopList != null && _nativeStoreList.shopList.Count > 0)
		{
			_isFinishGetNativeProductlist = true;
		}
		else
		{
			List<ProductData> shopList = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList;
			if (_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache6 == null)
			{
				_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache6 = new Func<ProductData, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			List<string> item_ids = shopList.Select<ProductData, string>(_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache6).ToList();
			Native.GetProductDatas(string.Join("----", item_ids.ToArray()));
		}
		while (!_isFinishGetNativeProductlist)
		{
			yield return (object)null;
		}
		List<ProductData> shopList2 = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList;
		if (_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache7 == null)
		{
			_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache7 = new Func<ProductData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		_purchaseGemList = shopList2.Where(_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache7).ToList();
		List<ProductData> shopList3 = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList;
		if (_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache8 == null)
		{
			_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache8 = new Func<ProductData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		_purchaseBundleList = shopList3.Where(_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache8).ToList();
		List<ProductData> shopList4 = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.shopList;
		if (_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache9 == null)
		{
			_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache9 = new Func<ProductData, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		_purchaseMaterialList = shopList4.Where(_003CDoInitialize_003Ec__Iterator39._003C_003Ef__am_0024cache9).ToList();
		gemTab = GetCtrl(UI.OBJ_GEM_TAB);
		bundleTab = GetCtrl(UI.OBJ_BUNDLE_TAB);
		materialTab = GetCtrl(UI.OBJ_MATERIAL_TAB);
		MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("IAP_open", "Functionality");
		CheckHightlightBtnTab();
		string pId = GameSection.GetEventData() as string;
		if (pId != null)
		{
			_viewType = VIEW_TYPE.BUNDLE;
		}
		base.Initialize();
	}

	protected unsafe override void OnDestroy()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		if (MonoBehaviourSingleton<ShopReceiver>.IsValid())
		{
			ShopReceiver i = MonoBehaviourSingleton<ShopReceiver>.I;
			i.onBillingUnavailable = Delegate.Remove((Delegate)i.onBillingUnavailable, (Delegate)new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	private unsafe void _viewGemTab()
	{
		SetActive(gemTab, true);
		SetActive(bundleTab, false);
		SetActive(materialTab, false);
		CheckOpenedGemTab();
		int i = 0;
		_003C_viewGemTab_003Ec__AnonStorey2DF _003C_viewGemTab_003Ec__AnonStorey2DF;
		SetTable(gemTab, UI.TBL_LIST, "CrystalShopListItem", _purchaseGemList.Count, false, new Func<int, Transform, Transform>((object)_003C_viewGemTab_003Ec__AnonStorey2DF, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003C_viewGemTab_003Ec__AnonStorey2DF, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void _viewBundleTab()
	{
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		SetActive(gemTab, false);
		SetActive(materialTab, false);
		SetActive(bundleTab, true);
		CheckOpenedBundleTab();
		if (_purchaseBundleList.Count == 0)
		{
			SetActive((Enum)UI.OBJ_AIM, false);
			SetActive((Enum)UI.TEX_NPCMODEL, false);
			SetActive((Enum)UI.SPR_FRAME_DRAGON, false);
			SetActive((Enum)UI.LBL_NONE, true);
		}
		else
		{
			SetActive((Enum)UI.OBJ_AIM, true);
			SetActive((Enum)UI.TEX_NPCMODEL, true);
			SetActive((Enum)UI.SPR_FRAME_DRAGON, true);
			if (_currentPageIndex < _purchaseBundleList.Count)
			{
				ProductData productData = _purchaseBundleList[_currentPageIndex];
				GlobalSettingsManager.PackParam.PackInfo pack = MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.GetPack(productData.productId);
				Transform root = SetPrefab(_objBundle, MonoBehaviourSingleton<GlobalSettingsManager>.I.packParam.prefabBundleName, true);
				Utility.ToggleActiveChildren(_objBundle, _currentPageIndex);
				UITexture sprw = FindCtrl(root, UI.SPR_WINDOW).GetComponent<UITexture>();
				ResourceLoad.LoadShopImageTexture(sprw, pack.bundleImageId, delegate(Texture tex)
				{
					if (sprw != null)
					{
						sprw.mainTexture = tex;
					}
				});
				SetTexture(root, UI.SPR_OFFER, null);
				if (productData.offerType > 0)
				{
					UITexture spro = FindCtrl(root, UI.SPR_OFFER).GetComponent<UITexture>();
					ResourceLoad.LoadShopImageOfferTexture(sprw, (uint)productData.offerType, delegate(Texture tex)
					{
						if (sprw != null)
						{
							spro.width = tex.get_width();
							spro.height = tex.get_height();
							spro.mainTexture = tex;
						}
					});
				}
				RemoveModel(root, UI.OBJ_MODEL);
				if (!string.IsNullOrEmpty(pack.chestName))
				{
					SetModel(root, UI.OBJ_MODEL, pack.chestName);
				}
				SetActive(GetChildSafe(root, UI.OBJ_OFFER, productData.offerType - 1), true);
				if (productData.remainingDay > 0)
				{
					SetButtonEnabled(root, UI.BTN_BUY, false);
					SetActive(root, UI.LBL_BUNDLE_PRICE, false);
					SetActive(root, UI.LBL_DAY_LEFT, true);
					SetLabelText(root, UI.LBL_DAY_LEFT, (productData.remainingDay <= 1) ? string.Format(base.sectionData.GetText("DAY_LEFT"), productData.remainingDay) : string.Format(base.sectionData.GetText("DAYS_LEFT"), productData.remainingDay));
				}
				else
				{
					SetButtonEnabled(root, UI.BTN_BUY, true);
					SetActive(root, UI.LBL_BUNDLE_PRICE, true);
					SetActive(root, UI.LBL_DAY_LEFT, false);
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
			SetLabelText(UI.LBL_CURRENT, (_purchaseBundleList.Count <= 0) ? _currentPageIndex : (_currentPageIndex + 1));
			SetLabelText(UI.LBL_TOTAL, _purchaseBundleList.Count);
			bool flag = _currentPageIndex > 0;
			bool flag2 = _currentPageIndex < _purchaseBundleList.Count - 1;
			SetColor((Enum)UI.SPR_AIM_L, (!flag) ? Color.get_clear() : Color.get_white());
			SetColor((Enum)UI.SPR_AIM_R, (!flag2) ? Color.get_clear() : Color.get_white());
			SetButtonEnabled((Enum)UI.BTN_AIM_L, flag);
			SetButtonEnabled((Enum)UI.BTN_AIM_R, flag2);
			SetActive((Enum)UI.BTN_AIM_L_INACTIVE, !flag);
			SetActive((Enum)UI.BTN_AIM_R_INACTIVE, !flag2);
			SetRepeatButton((Enum)UI.BTN_AIM_L, "AIM_L", (object)null);
			SetRepeatButton((Enum)UI.BTN_AIM_R, "AIM_R", (object)null);
			_updateNPC();
		}
	}

	private unsafe void _viewMaterialTab()
	{
		SetActive(gemTab, false);
		SetActive(bundleTab, false);
		SetActive(materialTab, true);
		CheckOpenedMaterialTab();
		int i = 0;
		_003C_viewMaterialTab_003Ec__AnonStorey2E3 _003C_viewMaterialTab_003Ec__AnonStorey2E;
		SetTable(materialTab, UI.TBL_LIST, "CrystalShopListItemMaterial", _purchaseMaterialList.Count, false, new Action<int, Transform, bool>((object)_003C_viewMaterialTab_003Ec__AnonStorey2E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void OnGetProductDatas(StoreDataList list)
	{
		_isFinishGetNativeProductlist = true;
		_nativeStoreList = list;
	}

	private void _updateNPC()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(base.sectionData.sectionName + "_TEXT");
		if (section != null)
		{
			NPCMessageTable.Message message = section.GetNPCMessage();
			if (message != null)
			{
				SetRenderNPCModel((Enum)UI.TEX_NPCMODEL, message.npc, message.pos, message.rot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>)delegate(NPCLoader loader)
				{
					loader.GetAnimator().Play(message.animationStateName);
				});
			}
		}
	}

	private void _disableChest()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		if (!(_objBundle == null))
		{
			foreach (Transform item in _objBundle)
			{
				Transform root = item;
				SetActiveModel(root, UI.OBJ_MODEL, false);
			}
		}
	}

	private void _enableChest()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		if (!(_objBundle == null) && _viewType != 0 && _viewType != VIEW_TYPE.MATERIAL)
		{
			foreach (Transform item in _objBundle)
			{
				Transform root = item;
				SetActiveModel(root, UI.OBJ_MODEL, true);
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
		}
		else
		{
			pp = string.Empty;
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isParentPassSet != 0)
			{
				GameSection.ChangeEvent("PP_INPUT", null);
			}
			else
			{
				SendGoldCanPurchase();
			}
		}
	}

	private void OnQuery_PP_TO_BUY()
	{
		pp = (GameSection.GetEventData() as string);
		SendGoldCanPurchase();
	}

	private void OnQuery_CrystalShopStopper_YES()
	{
		RequestEvent("STOPPER_TO_BUY", null);
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
				GameSection.ChangeStayEvent("STOPPER", null);
				GameSection.ResumeEvent(true, null);
				break;
			default:
				GameSection.ResumeEvent(false, null);
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
		GameSection.ChangeStayEvent("BILLING_UNAVAILABLE", null);
		GameSection.ResumeEvent(true, null);
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
			GameSection.ResumeEvent(false, null);
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
			GameSection.ResumeEvent(false, null);
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
					GameSection.ChangeStayEvent("SPECIAL_NOTICE", product_data);
					GameSection.ResumeEvent(true, null);
				});
			}
		}
		if (isPurchase)
		{
			GameSection.ResumeEvent(false, null);
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
				onFinish.Invoke();
			});
		}
		if (isPurchase)
		{
			GameSection.ResumeEvent(false, null);
		}
	}

	private unsafe Action GetFinishAction(int index, ProductData product_data)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		_003CGetFinishAction_003Ec__AnonStorey2E8 _003CGetFinishAction_003Ec__AnonStorey2E;
		return new Action((object)_003CGetFinishAction_003Ec__AnonStorey2E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
	}

	private unsafe Action GetFinishActionBundle(ShopReceiver.PaymentPurchaseData purchaseData)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		_003CGetFinishActionBundle_003Ec__AnonStorey2E9 _003CGetFinishActionBundle_003Ec__AnonStorey2E;
		return new Action((object)_003CGetFinishActionBundle_003Ec__AnonStorey2E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
	}

	private unsafe Action GetFinishActionMaterial(ShopReceiver.PaymentPurchaseData purchaseData)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		_003CGetFinishActionMaterial_003Ec__AnonStorey2EA _003CGetFinishActionMaterial_003Ec__AnonStorey2EA;
		return new Action((object)_003CGetFinishActionMaterial_003Ec__AnonStorey2EA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
	}

	private void SendRequestCurrentCrystal(Action onFinish)
	{
		Protocol.Send(OnceStatusInfoModel.URL, delegate(OnceStatusInfoModel result)
		{
			CheckCrystalNum(result, onFinish);
		}, string.Empty);
	}

	private unsafe void SendRequestCurrentPresentAndShopList(Action onFinish)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		_003CSendRequestCurrentPresentAndShopList_003Ec__AnonStorey2EC _003CSendRequestCurrentPresentAndShopList_003Ec__AnonStorey2EC;
		SendRequestCurrentCrystal(new Action((object)_003CSendRequestCurrentPresentAndShopList_003Ec__AnonStorey2EC, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void CheckCrystalNum(OnceStatusInfoModel ret, Action onFinish)
	{
		if (ret.Error == Error.None)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal = ret.result.userStatus.crystal;
			MonoBehaviourSingleton<UserInfoManager>.I.DirtyUserStatus();
			onFinish.Invoke();
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
			int num = string2.IndexOf(_purchaseMaterialList[i].productId);
			if (num < 0)
			{
				isHighlightMaterial = true;
				break;
			}
		}
		count = _purchaseBundleList.Count;
		int num2 = 0;
		while (true)
		{
			if (num2 >= count)
			{
				return;
			}
			int num3 = @string.IndexOf(_purchaseBundleList[num2].productId);
			if (num3 < 0)
			{
				break;
			}
			num2++;
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
			SetBadge(t, -1, 3, -8, -8, false);
		}
		else
		{
			SetBadge(t, 0, 3, -8, -8, false);
		}
		if (isHighlightBundle)
		{
			SetBadge(t2, -1, 3, -8, -8, false);
		}
		else
		{
			SetBadge(t2, 0, 3, -8, -8, false);
		}
		string text = PlayerPrefs.GetString("Purchase_Item_List_Tab_Gem", string.Empty);
		int count = _purchaseGemList.Count;
		for (int i = 0; i < count; i++)
		{
			int num = text.IndexOf(_purchaseGemList[i].productId);
			if (num < 0)
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
		SetBadge(t, 0, 3, -8, -8, false);
		if (isHighlightBundle)
		{
			SetBadge(t2, -1, 3, -8, -8, false);
		}
		else
		{
			SetBadge(t2, 0, 3, -8, -8, false);
		}
		string text = PlayerPrefs.GetString("Purchase_Item_List_Tab_Material", string.Empty);
		int count = _purchaseMaterialList.Count;
		for (int i = 0; i < count; i++)
		{
			int num = text.IndexOf(_purchaseMaterialList[i].productId);
			if (num < 0)
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
			SetBadge(t, -1, 3, -8, -8, false);
		}
		else
		{
			SetBadge(t, 0, 3, -8, -8, false);
		}
		SetBadge(t2, 0, 3, -8, -8, false);
		string text = PlayerPrefs.GetString("Purchase_Item_List_Tab_Bundle", string.Empty);
		int count = _purchaseBundleList.Count;
		for (int i = 0; i < count; i++)
		{
			int num = text.IndexOf(_purchaseBundleList[i].productId);
			if (num < 0)
			{
				text = text + "/" + _purchaseBundleList[i].productId;
			}
		}
		PlayerPrefs.SetString("Purchase_Item_List_Tab_Bundle", text);
	}
}
