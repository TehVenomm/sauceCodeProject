using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomePointShopEventDetail : GameSection
{
	private enum UI
	{
		TEX_EVENT_POP,
		LBL_POINT,
		GRD_LIST,
		TEX_POINT_ICON,
		LBL_ARROW_NOW,
		LBL_ARROW_MAX,
		LBL_FILTER,
		LBL_HAVE
	}

	private PointShop data;

	private List<PointShopItem> currentPointShopItem = new List<PointShopItem>();

	protected int currentPage;

	protected int maxPage;

	private PointShopFilterBase.Filter filter;

	public override void Initialize()
	{
		data = (GameSection.GetEventData() as PointShop);
		currentPage = 1;
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		loadingQueue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName(data.pointShopId));
		loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.COMMON, ResourceName.GetPointSHopBGImageName(data.pointShopId));
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		UITexture component = GetCtrl(UI.TEX_POINT_ICON).GetComponent<UITexture>();
		UITexture component2 = GetCtrl(UI.TEX_EVENT_POP).GetComponent<UITexture>();
		ResourceLoad.LoadPointIconImageTexture(component, (uint)data.pointShopId);
		ResourceLoad.LoadPointShopBGTexture(component2, (uint)data.pointShopId);
		SetLabelText(UI.LBL_POINT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), data.userPoint));
		SetList();
	}

	private void SetList()
	{
		currentPointShopItem = GetBuyableItemList();
		if (filter != null)
		{
			filter.DoFiltering(ref currentPointShopItem);
		}
		maxPage = currentPointShopItem.Count / GameDefine.POINT_SHOP_LIST_COUNT;
		if (currentPointShopItem.Count % GameDefine.POINT_SHOP_LIST_COUNT > 0)
		{
			maxPage++;
		}
		SetLabelText(UI.LBL_ARROW_NOW, (maxPage > 0) ? currentPage.ToString() : "0");
		SetLabelText(UI.LBL_ARROW_MAX, maxPage.ToString());
		int item_num = Mathf.Min(GameDefine.POINT_SHOP_LIST_COUNT, currentPointShopItem.Count - (currentPage - 1) * GameDefine.POINT_SHOP_LIST_COUNT);
		SetGrid(UI.GRD_LIST, "PointShopListItem", item_num, reset: true, delegate(int i, Transform t, bool b)
		{
			int index = i + (currentPage - 1) * GameDefine.POINT_SHOP_LIST_COUNT;
			PointShopItem pointShopItem = currentPointShopItem[index];
			object event_data = new object[3]
			{
				pointShopItem,
				data,
				new Action<PointShopItem, int>(OnBuy)
			};
			SetEvent(t, "CONFIRM_BUY", event_data);
			t.GetComponent<PointShopItemList>().SetUp(pointShopItem, (uint)data.pointShopId, pointShopItem.needPoint <= data.userPoint);
			int num = -1;
			REWARD_TYPE type = (REWARD_TYPE)pointShopItem.type;
			if (type == REWARD_TYPE.ITEM)
			{
				num = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum((uint)pointShopItem.itemId);
			}
			SetLabelText(t, UI.LBL_HAVE, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 6u), num.ToString()));
			SetActive(t, UI.LBL_HAVE, num >= 0);
		});
	}

	private void OnQuery_CONFIRM_BUY()
	{
		object[] obj = GameSection.GetEventData() as object[];
		PointShopItem pointShopItem = obj[0] as PointShopItem;
		if ((obj[1] as PointShop).userPoint < pointShopItem.needPoint)
		{
			GameSection.ChangeEvent("SHORTAGE_POINT");
		}
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.PointShop);
	}

	private void OnBuy(PointShopItem item, int num)
	{
		GameSection.SetEventData(new object[2]
		{
			item.name,
			num
		});
		GameSection.StayEvent();
		PointShopBuyModel.SendForm sendForm = new PointShopBuyModel.SendForm();
		sendForm.uid = item.pointShopItemId;
		sendForm.num = num;
		Protocol.Send(PointShopBuyModel.URL, sendForm, delegate(PointShopBuyModel result)
		{
			if (result != null && result.Error == Error.None)
			{
				item.buyCount += num;
				data.userPoint -= item.needPoint * num;
				RefreshUI();
			}
			GameSection.ResumeEvent(result != null && result.Error == Error.None);
		});
	}

	private void OnQuery_PAGE_NEXT()
	{
		currentPage++;
		if (currentPage > maxPage)
		{
			currentPage = 1;
		}
		RefreshUI();
	}

	private void OnQuery_PAGE_PREV()
	{
		currentPage--;
		if (currentPage < 1)
		{
			currentPage = ((maxPage <= 0) ? 1 : maxPage);
		}
		RefreshUI();
	}

	private void OnQuery_FILTER()
	{
		List<PointShopItem> buyableItemList = GetBuyableItemList();
		GameSection.SetEventData(new object[2]
		{
			filter,
			buyableItemList
		});
	}

	private void OnCloseDialog_PointShopFilter()
	{
		PointShopFilterBase.Filter filter = GameSection.GetEventData() as PointShopFilterBase.Filter;
		if (filter != null)
		{
			this.filter = filter;
			currentPage = 1;
			RefreshUI();
		}
	}

	private List<PointShopItem> GetBuyableItemList()
	{
		return (from x in data.items
			where x.isBuyable
			where x.type != 8 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedStamp(x.itemId)
			where x.type != 9 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedDegree(x.itemId)
			where x.type != 7 || !MonoBehaviourSingleton<GlobalSettingsManager>.I.IsUnlockedAvatar(x.itemId)
			select x).ToList();
	}
}
