using Network;
using System;
using System.Collections;
using UnityEngine;

public class BlackMarketTop : GameSection
{
	private enum UI
	{
		GRD_MARKET_ITEM_1 = 1,
		GRD_MARKET_ITEM_2,
		GRD_MARKET_ITEM_3,
		GRD_MARKET_ITEM_4,
		GRD_MARKET_ITEM_5,
		GRD_MARKET_ITEM_6,
		LBL_TIME_COUNT,
		TEX_NPCMODEL,
		LBL_NPC_MESSAGE,
		LBL_GOLD_NUM,
		LBL_CRYSTAL_NUM,
		SPR_GOLD_ITEM_BG,
		LBL_GOLD_SALE_PRICE,
		SPR_GEM_ITEM_BG,
		LBL_GEM_SALE_PRICE,
		LBL_ITEM_SALE_NUM,
		BTN_FEATURED_OFFER,
		FEATURED_OFFER_BANNER,
		LBL_OFFER_NAME,
		LBL_OFFER_SALE_VALUE,
		LBL_OFFER_SALE_PROGRESS,
		SLD_OFFER_BUY_PROGRESS,
		OBJ_OFFER_OUT_OFF_STOCK,
		LBL_OFFER_OLD_PRICE,
		LBL_OFFER_SALE_PRICE,
		LBL_SALE_PERCENT,
		LBL_ITEM_NAME,
		OBJ_SOLD,
		OBJ_OUT_OFF_STOCK,
		SLD_BUY_PROGRESS,
		LBL_SALE_PROGRESS,
		OBJ_HOT,
		IMG_ICON,
		SPR_DISABLE_MASK,
		OBJ_SOLD_OUT
	}

	public enum SALE_TYPE
	{
		GEM = 1,
		MONEY = 2,
		IAP = 200
	}

	private bool _isFinishGetNativeProductlist;

	private StoreDataList _nativeStoreList;

	private int timeResetMarket;

	private int currentNPCMessageIndex;

	private Color normalSale = new Color(0.0470588244f, 0.6039216f, 0.003921569f, 1f);

	private Color mediumSale = new Color(1f, 0.403921574f, 0.168627456f, 1f);

	private Color hotSale = Color.get_red();

	private Color disableTintColor = new Color(0.4f, 0.4f, 0.4f, 1f);

	private DarkMarketItem currentItemChoosed;

	private bool isPurchase;

	private bool isRelloading;

	private bool isReseting;

	public unsafe override void Initialize()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		GameSaveData.instance.canShowNoteDarkMarket = false;
		MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.UpdateNoteMarket();
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
	}

	private IEnumerator DoInitialize()
	{
		bool wait = true;
		MonoBehaviourSingleton<ShopManager>.I.SendGetDarkMarketItemList(delegate
		{
			((_003CDoInitialize_003Ec__Iterator14)/*Error near IL_0031: stateMachine*/)._003Cwait_003E__0 = false;
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
			string skus = MonoBehaviourSingleton<ShopManager>.I.GetListProductData();
			if (!string.IsNullOrEmpty(skus))
			{
				Native.GetProductDatas(skus);
			}
			else
			{
				_isFinishGetNativeProductlist = true;
			}
		}
		while (!_isFinishGetNativeProductlist)
		{
			yield return (object)null;
		}
		LoadDarkMarketUI(true);
		UpdateNPC();
		base.Initialize();
		this.StartCoroutine("TimeCountDown");
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

	private void LoadDarkMarketUI(bool isReloadIcon)
	{
		SetLabelText((Enum)UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal.ToString("N0"));
		SetLabelText((Enum)UI.LBL_GOLD_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money.ToString("N0"));
		int count = MonoBehaviourSingleton<ShopManager>.I.darkMarketItemList.items.Count;
		int num = 1;
		bool flag = false;
		for (int i = 0; i < 7; i++)
		{
			if (i < count)
			{
				DarkMarketItem darkMarketItem = MonoBehaviourSingleton<ShopManager>.I.darkMarketItemList.items[i];
				if (darkMarketItem.feature == 1)
				{
					flag = true;
					InitDrakMarketFeatured(darkMarketItem, isReloadIcon);
				}
				else
				{
					InitDrakMarketItem(num, darkMarketItem, isReloadIcon);
					num++;
				}
			}
			else
			{
				InitDrakMarketItem(num, null, true);
				num++;
			}
		}
		if (!flag)
		{
			SetActive((Enum)UI.BTN_FEATURED_OFFER, false);
		}
	}

	private void InitDrakMarketFeatured(DarkMarketItem data, bool isReloadIcon = true)
	{
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		SetActive((Enum)UI.BTN_FEATURED_OFFER, true);
		if (data.saleType == 1)
		{
			SetEvent((Enum)UI.BTN_FEATURED_OFFER, "BUY_NORMAL", data.id);
		}
		else if (data.saleType == 2)
		{
			SetEvent((Enum)UI.BTN_FEATURED_OFFER, "BUY_NORMAL", data.id);
		}
		else if (data.saleType == 200)
		{
			SetEvent((Enum)UI.BTN_FEATURED_OFFER, "BUY_IAP", data.id);
		}
		SetFontStyle((Enum)UI.LBL_OFFER_SALE_VALUE, 2);
		SetLabelText((Enum)UI.LBL_OFFER_NAME, data.name);
		if (!string.IsNullOrEmpty(data.refProductId))
		{
			SetActive((Enum)UI.LBL_OFFER_OLD_PRICE, true);
			SetLabelText((Enum)UI.LBL_OFFER_OLD_PRICE, $"[s]${data.baseNum}[/s]");
			SetSupportEncoding(UI.LBL_OFFER_OLD_PRICE, true);
		}
		else
		{
			SetActive((Enum)UI.LBL_OFFER_OLD_PRICE, false);
		}
		SetLabelText((Enum)UI.LBL_OFFER_SALE_PRICE, $"${data.saleNum}");
		int num = 100 - Mathf.RoundToInt(data.saleNum / data.baseNum * 100f);
		if (_nativeStoreList != null)
		{
			StoreData product = _nativeStoreList.getProduct(data.saleoffProductId);
			if (product != null)
			{
				SetLabelText((Enum)UI.LBL_OFFER_SALE_PRICE, product.price.ToString());
			}
			StoreData product2 = _nativeStoreList.getProduct(data.refProductId);
			if (product2 != null)
			{
				SetLabelText((Enum)UI.LBL_OFFER_OLD_PRICE, $"[s]{product2.price}[/s]");
			}
			if (product != null && product2 != null)
			{
				num = Mathf.FloorToInt(Mathf.Clamp(100f - (float)product.priceMicros / (float)product2.priceMicros * 100f, 0f, 100f));
			}
		}
		if (num < 30)
		{
			SetColor((Enum)UI.LBL_OFFER_SALE_VALUE, normalSale);
		}
		else if (num < 70)
		{
			SetColor((Enum)UI.LBL_OFFER_SALE_VALUE, mediumSale);
		}
		else
		{
			SetColor((Enum)UI.LBL_OFFER_SALE_VALUE, hotSale);
		}
		if (num > 0)
		{
			SetLabelText((Enum)UI.LBL_OFFER_SALE_VALUE, $"-{num}%");
		}
		else
		{
			SetLabelText((Enum)UI.LBL_OFFER_SALE_VALUE, $"{num}%");
		}
		if (data.usedCount >= data.limit)
		{
			SetSliderValue((Enum)UI.SLD_OFFER_BUY_PROGRESS, 0f);
			SetActive((Enum)UI.LBL_OFFER_SALE_PROGRESS, false);
			SetActive((Enum)UI.OBJ_OFFER_OUT_OFF_STOCK, true);
			SetColor((Enum)UI.FEATURED_OFFER_BANNER, disableTintColor);
			SetButtonEnabled((Enum)UI.BTN_FEATURED_OFFER, false);
		}
		else
		{
			float value = 1f - (float)data.usedCount / (float)data.limit;
			SetSliderValue((Enum)UI.SLD_OFFER_BUY_PROGRESS, value);
			SetActive((Enum)UI.LBL_OFFER_SALE_PROGRESS, true);
			SetLabelText((Enum)UI.LBL_OFFER_SALE_PROGRESS, $"{data.limit - data.usedCount}/{data.limit}");
			SetActive((Enum)UI.OBJ_OFFER_OUT_OFF_STOCK, false);
			SetColor((Enum)UI.FEATURED_OFFER_BANNER, Color.get_white());
			SetButtonEnabled((Enum)UI.BTN_FEATURED_OFFER, true);
		}
		if (isReloadIcon)
		{
			Transform ctrl = GetCtrl(UI.BTN_FEATURED_OFFER);
			UITexture spro = FindCtrl(ctrl, UI.FEATURED_OFFER_BANNER).GetComponent<UITexture>();
			ResourceLoad.LoadBlackMarketOfferTexture(spro, data.imgId, delegate(Texture tex)
			{
				if (spro != null)
				{
					spro.mainTexture = tex;
				}
			});
		}
	}

	private void InitDrakMarketItem(int index, DarkMarketItem data, bool isReloadIcon = true)
	{
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		if (data == null)
		{
			SetActive((Enum)(UI)index, false);
		}
		else
		{
			Transform ctrl = GetCtrl((UI)index);
			if (data.saleType == 1)
			{
				SetEvent((Enum)(UI)index, "BUY_NORMAL", data.id);
				SetActive(ctrl, UI.SPR_GOLD_ITEM_BG, false);
				SetActive(ctrl, UI.SPR_GEM_ITEM_BG, true);
				SetLabelText(ctrl, UI.LBL_GEM_SALE_PRICE, $"x{data.saleNum}");
			}
			else if (data.saleType == 2)
			{
				SetEvent((Enum)(UI)index, "BUY_NORMAL", data.id);
				SetActive(ctrl, UI.SPR_GOLD_ITEM_BG, true);
				SetLabelText(ctrl, UI.LBL_GOLD_SALE_PRICE, string.Format("x{0}", data.saleNum.ToString("N0")));
				SetActive(ctrl, UI.SPR_GEM_ITEM_BG, false);
			}
			else if (data.saleType == 200)
			{
				SetEvent((Enum)(UI)index, "BUY_IAP", data.id);
			}
			int num = 100 - Mathf.RoundToInt(data.saleNum / data.baseNum * 100f);
			if (num < 30)
			{
				SetColor(ctrl, UI.LBL_SALE_PERCENT, normalSale);
			}
			else if (num < 70)
			{
				SetColor(ctrl, UI.LBL_SALE_PERCENT, mediumSale);
			}
			else
			{
				SetColor(ctrl, UI.LBL_SALE_PERCENT, hotSale);
			}
			if (num > 60)
			{
				SetActive(ctrl, UI.OBJ_HOT, true);
			}
			else
			{
				SetActive(ctrl, UI.OBJ_HOT, false);
			}
			if (num > 0)
			{
				SetLabelText(ctrl, UI.LBL_SALE_PERCENT, $"-{num}%");
			}
			else
			{
				SetLabelText(ctrl, UI.LBL_SALE_PERCENT, $"{num}%");
			}
			SetFontStyle(ctrl, UI.LBL_SALE_PERCENT, 2);
			SetLabelText(ctrl, UI.LBL_ITEM_NAME, data.name);
			if (data.rewards != null && data.rewards.Count > 0)
			{
				string empty = string.Empty;
				SetLabelText(text: (data.rewards[0].num > 1000000) ? $"x{(float)data.rewards[0].num / 1000000f}M" : ((data.rewards[0].num <= 1000) ? $"x{data.rewards[0].num}" : $"x{(float)data.rewards[0].num / 1000f}K"), root: ctrl, label_enum: UI.LBL_ITEM_SALE_NUM);
			}
			else
			{
				SetLabelText(ctrl, UI.LBL_ITEM_SALE_NUM, string.Empty);
			}
			bool flag = false;
			bool flag2 = false;
			if (data.usedCount >= data.limit)
			{
				flag = true;
				SetSliderValue(ctrl, UI.SLD_BUY_PROGRESS, 0f);
				SetActive(ctrl, UI.LBL_SALE_PROGRESS, false);
				SetActive(ctrl, UI.OBJ_OUT_OFF_STOCK, true);
			}
			else
			{
				flag = false;
				float value = 1f - (float)data.usedCount / (float)data.limit;
				SetSliderValue(ctrl, UI.SLD_BUY_PROGRESS, value);
				SetActive(ctrl, UI.LBL_SALE_PROGRESS, true);
				SetLabelText(ctrl, UI.LBL_SALE_PROGRESS, $"{data.limit - data.usedCount}/{data.limit}");
				SetActive(ctrl, UI.OBJ_OUT_OFF_STOCK, false);
			}
			if (!flag)
			{
				if (data.remain == 0)
				{
					flag2 = true;
					SetActive(ctrl, UI.OBJ_SOLD_OUT, true);
				}
				else
				{
					flag2 = false;
					SetActive(ctrl, UI.OBJ_SOLD_OUT, false);
				}
			}
			if (flag2 || flag)
			{
				SetActive(ctrl, UI.SPR_DISABLE_MASK, true);
				SetButtonEnabled((Enum)(UI)index, false);
			}
			else
			{
				SetActive(ctrl, UI.SPR_DISABLE_MASK, false);
				SetButtonEnabled((Enum)(UI)index, true);
			}
			if (isReloadIcon)
			{
				UITexture spro = FindCtrl(ctrl, UI.IMG_ICON).GetComponent<UITexture>();
				ResourceLoad.LoadBlackMarketIconTexture(spro, data.imgId, delegate(Texture tex)
				{
					if (spro != null)
					{
						spro.mainTexture = tex;
					}
				});
			}
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		if ((flags & NOTIFY_FLAG.UPDATE_DARK_MARKET) != (NOTIFY_FLAG)0L)
		{
			if (!isRelloading && !isReseting)
			{
				LoadDarkMarketUI(false);
			}
		}
		else if ((flags & NOTIFY_FLAG.RESET_DARK_MARKET) != (NOTIFY_FLAG)0L && !string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
		{
			timeResetMarket = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
			if (timeResetMarket > 0)
			{
				isRelloading = false;
				this.StopAllCoroutines();
				this.StartCoroutine("TimeCountDown");
				this.StartCoroutine("DoResetMarketData");
			}
		}
		base.OnNotify(flags);
	}

	private void UpdateNPC()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		string empty = string.Empty;
		NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(base.sectionData.sectionName + "_TEXT");
		if (section != null)
		{
			NPCMessageTable.Message message = section.messages[currentNPCMessageIndex];
			if (message != null)
			{
				empty = message.message;
				SetRenderNPCModel((Enum)UI.TEX_NPCMODEL, message.npc, message.pos, message.rot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>)delegate(NPCLoader loader)
				{
					loader.GetAnimator().Play(message.animationStateName);
				});
				SetLabelText((Enum)UI.LBL_NPC_MESSAGE, empty);
			}
		}
	}

	private void OnQuery_BUY_NORMAL()
	{
		int itemId = (int)GameSection.GetEventData();
		currentItemChoosed = MonoBehaviourSingleton<ShopManager>.I.GetDarkMarketItem(itemId);
		string text = "Gems";
		if (currentItemChoosed.saleType == 2)
		{
			text = "Gold";
		}
		object[] eventData = new object[3]
		{
			currentItemChoosed.name,
			currentItemChoosed.saleNum,
			text
		};
		GameSection.SetEventData(eventData);
	}

	private void OnQuery_BlackMarketItemConfirm_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ShopManager>.I.SendBuyDarkMarket(currentItemChoosed.id, delegate(Error error)
		{
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			switch (error)
			{
			case Error.None:
				GameSection.ChangeStayEvent("BUY_SUCCESS", null);
				this.StartCoroutine("DoReloadMarketData");
				break;
			case Error.ERR_BM_NOT_ENOUGH_GOLD:
			case Error.ERR_BM_NOT_ENOUGH_GEM:
			{
				string errorMessage2 = StringTable.GetErrorMessage((uint)error);
				GameSection.ChangeStayEvent("BUY_ERROR", errorMessage2);
				GameSection.ResumeEvent(true, null);
				break;
			}
			case Error.ERR_BM_ITEM_UNAVAILABLE:
			case Error.ERR_BLACK_MARKET_BUY:
			case Error.ERR_BM_ITEM_SOLD_OUT:
			{
				string errorMessage = StringTable.GetErrorMessage((uint)error);
				GameSection.ChangeStayEvent("BUY_ERROR", errorMessage);
				this.StartCoroutine("DoReloadMarketData");
				break;
			}
			default:
				GameSection.ResumeEvent(false, null);
				break;
			}
		});
	}

	private IEnumerator DoReloadMarketData()
	{
		isRelloading = true;
		bool wait = true;
		MonoBehaviourSingleton<ShopManager>.I.SendGetDarkMarketItemList(delegate
		{
			((_003CDoReloadMarketData_003Ec__Iterator15)/*Error near IL_0039: stateMachine*/)._003Cwait_003E__0 = false;
		});
		while (wait)
		{
			yield return (object)null;
		}
		LoadDarkMarketUI(false);
		isPurchase = false;
		isRelloading = false;
		GameSection.ResumeEvent(true, null);
	}

	private IEnumerator DoResetMarketData()
	{
		isReseting = true;
		GameSection.StayEvent();
		bool wait = true;
		MonoBehaviourSingleton<ShopManager>.I.SendGetDarkMarketItemList(delegate
		{
			((_003CDoResetMarketData_003Ec__Iterator16)/*Error near IL_003e: stateMachine*/)._003Cwait_003E__0 = false;
		});
		while (wait)
		{
			yield return (object)null;
		}
		LoadDarkMarketUI(true);
		isPurchase = false;
		isReseting = false;
		GameSection.ResumeEvent(true, null);
	}

	private void OnQuery_BUY_IAP()
	{
		int itemId = (int)GameSection.GetEventData();
		currentItemChoosed = MonoBehaviourSingleton<ShopManager>.I.GetDarkMarketItem(itemId);
		SendGoldCanPurchase();
	}

	private void SendGoldCanPurchase()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ShopManager>.I.SendDarkMarketCanPurchase(currentItemChoosed.saleoffProductId, currentItemChoosed.id, string.Empty, delegate(Error ret)
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
		Native.RequestPurchase(currentItemChoosed.saleoffProductId, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString(), MonoBehaviourSingleton<UserInfoManager>.I.userIdHash);
	}

	private void OnBillingUnavailable()
	{
	}

	private unsafe void OnBuyItem(string productId)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		if (!string.IsNullOrEmpty(productId))
		{
			isPurchase = false;
			SendRequestCurrentPresentAndShopList(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
		if (isPurchase)
		{
			GameSection.ResumeEvent(false, null);
		}
	}

	private unsafe void OnBuyBundle(ShopReceiver.PaymentPurchaseData purchaseData)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (purchaseData != null && purchaseData.bundle != null)
		{
			isPurchase = false;
			SendRequestCurrentPresentAndShopList(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
		if (isPurchase)
		{
			GameSection.ResumeEvent(false, null);
		}
	}

	private unsafe void OnBuyMaterial(ShopReceiver.PaymentPurchaseData purchaseData)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		if (purchaseData != null)
		{
			isPurchase = false;
			SendRequestCurrentPresentAndShopList(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
		if (isPurchase)
		{
			GameSection.ResumeEvent(false, null);
		}
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
		_003CSendRequestCurrentPresentAndShopList_003Ec__AnonStorey2DF _003CSendRequestCurrentPresentAndShopList_003Ec__AnonStorey2DF;
		SendRequestCurrentCrystal(new Action((object)_003CSendRequestCurrentPresentAndShopList_003Ec__AnonStorey2DF, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	private void OnGetProductDatas(StoreDataList list)
	{
		_isFinishGetNativeProductlist = true;
		_nativeStoreList = list;
	}

	private IEnumerator TimeCountDown()
	{
		UILabel timeLbl = GetCtrl(UI.LBL_TIME_COUNT).GetComponent<UILabel>();
		if (string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
		{
			timeLbl.color = Color.get_red();
			timeLbl.text = "00:00:00";
		}
		else
		{
			timeResetMarket = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
			for (currentNPCMessageIndex = 0; timeResetMarket > 3600; timeResetMarket = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds)
			{
				timeLbl.text = UIUtility.TimeFormat(timeResetMarket, true);
				yield return (object)new WaitForSeconds(0.25f);
			}
			timeLbl.color = Color.get_red();
			currentNPCMessageIndex = 1;
			NPCMessageTable.Section npc_messgae = Singleton<NPCMessageTable>.I.GetSection(base.sectionData.sectionName + "_TEXT");
			if (npc_messgae != null)
			{
				NPCMessageTable.Message message = npc_messgae.messages[currentNPCMessageIndex];
				if (message != null)
				{
					SetLabelText((Enum)UI.LBL_NPC_MESSAGE, message.message);
				}
			}
			while (timeResetMarket > 0)
			{
				timeLbl.text = SecondToTime(timeResetMarket);
				yield return (object)new WaitForSeconds(0.25f);
				timeResetMarket = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
			}
			yield return (object)null;
			MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.UpdateDrakMarketState(false);
			yield return (object)this.StartCoroutine(_DoCloseDialog());
			GameSection.BackSection();
		}
	}

	private IEnumerator _DoCloseDialog()
	{
		while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			yield return (object)null;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Equals("BlackMarketTop"))
		{
			GameSection.BackSection();
			yield return (object)this.StartCoroutine(_DoCloseDialog());
		}
	}

	private string SecondToTime(int time)
	{
		int num = time % 60;
		int num2 = time / 60;
		num2 %= 60;
		int num3 = time / 3600;
		return $"{num3:D2}:{num2:D2}:{num:D2}";
	}
}
