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
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		data = (GameSection.GetEventData() as PointShop);
		currentPage = 1;
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		loadingQueue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName(data.pointShopId), false);
		loadingQueue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointSHopBGImageName(data.pointShopId), false);
		if (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
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
		SetLabelText((Enum)UI.LBL_POINT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), data.userPoint));
		SetList();
	}

	private unsafe void SetList()
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
		SetLabelText((Enum)UI.LBL_ARROW_NOW, (maxPage <= 0) ? "0" : currentPage.ToString());
		SetLabelText((Enum)UI.LBL_ARROW_MAX, maxPage.ToString());
		int item_num = Mathf.Min(GameDefine.POINT_SHOP_LIST_COUNT, currentPointShopItem.Count - (currentPage - 1) * GameDefine.POINT_SHOP_LIST_COUNT);
		SetGrid(UI.GRD_LIST, "PointShopListItem", item_num, true, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void OnQuery_CONFIRM_BUY()
	{
		object[] array = GameSection.GetEventData() as object[];
		PointShopItem pointShopItem = array[0] as PointShopItem;
		PointShop pointShop = array[1] as PointShop;
		if (pointShop.userPoint < pointShopItem.needPoint)
		{
			GameSection.ChangeEvent("SHORTAGE_POINT", null);
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
			GameSection.ResumeEvent(result != null && result.Error == Error.None, null);
		}, string.Empty);
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

	private unsafe List<PointShopItem> GetBuyableItemList()
	{
		List<PointShopItem> items = data.items;
		if (_003C_003Ef__am_0024cache5 == null)
		{
			_003C_003Ef__am_0024cache5 = new Func<PointShopItem, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<PointShopItem> source = items.Where(_003C_003Ef__am_0024cache5);
		if (_003C_003Ef__am_0024cache6 == null)
		{
			_003C_003Ef__am_0024cache6 = new Func<PointShopItem, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<PointShopItem> source2 = source.Where(_003C_003Ef__am_0024cache6);
		if (_003C_003Ef__am_0024cache7 == null)
		{
			_003C_003Ef__am_0024cache7 = new Func<PointShopItem, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<PointShopItem> source3 = source2.Where(_003C_003Ef__am_0024cache7);
		if (_003C_003Ef__am_0024cache8 == null)
		{
			_003C_003Ef__am_0024cache8 = new Func<PointShopItem, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		return source3.Where(_003C_003Ef__am_0024cache8).ToList();
	}
}
