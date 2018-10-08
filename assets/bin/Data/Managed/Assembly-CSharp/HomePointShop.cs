using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomePointShop : GameSection
{
	private enum VIEW_TYPE
	{
		NORMAL,
		EVENT_LIST
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
		LBL_ARROW_MAX
	}

	private List<PointShop> pointShop = new List<PointShop>();

	private UIModelRenderTexture modelTexture;

	private List<PointShopItem> currentPointShopItem = new List<PointShopItem>();

	protected int currentPage;

	protected int maxPage;

	private PointShopFilterBase.Filter filter;

	private VIEW_TYPE currentType;

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	public IEnumerator DoInitialize()
	{
		currentType = VIEW_TYPE.NORMAL;
		currentPage = 1;
		bool hasList = false;
		LoadingQueue loadingQueue = new LoadingQueue(this);
		loadingQueue.Load(RESOURCE_CATEGORY.UI, "PointShopListItem", false);
		MonoBehaviourSingleton<UserInfoManager>.I.PointShopManager.SendGetPointShops(delegate(bool isSuccess, List<PointShop> resultList)
		{
			if (isSuccess)
			{
				((_003CDoInitialize_003Ec__Iterator91)/*Error near IL_0073: stateMachine*/)._003C_003Ef__this.pointShop = resultList;
				foreach (PointShop item in ((_003CDoInitialize_003Ec__Iterator91)/*Error near IL_0073: stateMachine*/)._003C_003Ef__this.pointShop)
				{
					((_003CDoInitialize_003Ec__Iterator91)/*Error near IL_0073: stateMachine*/)._003CloadingQueue_003E__1.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName(item.pointShopId), false);
				}
				((_003CDoInitialize_003Ec__Iterator91)/*Error near IL_0073: stateMachine*/)._003ChasList_003E__0 = true;
			}
		});
		while (!hasList)
		{
			yield return (object)null;
		}
		if (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateNPC();
		UpdateTab();
	}

	protected void UpdateNPC()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		string empty = string.Empty;
		NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(base.sectionData.sectionName + "_TEXT");
		if (section != null)
		{
			NPCMessageTable.Message message = section.GetNPCMessage();
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

	private void UpdateTab()
	{
		switch (currentType)
		{
		default:
			ViewNormalTab();
			break;
		case VIEW_TYPE.EVENT_LIST:
			ViewEventTab();
			break;
		}
	}

	private void ViewNormalTab()
	{
		SetActive((Enum)UI.OBJ_NORMAL, true);
		SetActive((Enum)UI.OBJ_TAB_ROOT, true);
		SetActive((Enum)UI.OBJ_ON_TAB_NORMAL, true);
		SetActive((Enum)UI.OBJ_ON_TAB_EVENT, false);
		SetActive((Enum)UI.OBJ_EVENT_LIST, false);
		PointShop shop = pointShop.First((PointShop x) => !x.isEvent);
		currentPointShopItem = GetBuyableItemList();
		if (filter != null)
		{
			filter.DoFiltering(ref currentPointShopItem);
		}
		SetLabelText((Enum)UI.LBL_NORMAL_POINT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), shop.userPoint));
		UITexture component = GetCtrl(UI.TEX_NORMAL_POINT_ICON).GetComponent<UITexture>();
		ResourceLoad.LoadPointIconImageTexture(component, (uint)shop.pointShopId);
		maxPage = currentPointShopItem.Count / GameDefine.POINT_SHOP_LIST_COUNT;
		if (currentPointShopItem.Count % GameDefine.POINT_SHOP_LIST_COUNT > 0)
		{
			maxPage++;
		}
		SetLabelText((Enum)UI.LBL_ARROW_NOW, (maxPage <= 0) ? "0" : currentPage.ToString());
		SetLabelText((Enum)UI.LBL_ARROW_MAX, maxPage.ToString());
		int item_num = Mathf.Min(GameDefine.POINT_SHOP_LIST_COUNT, currentPointShopItem.Count - (currentPage - 1) * GameDefine.POINT_SHOP_LIST_COUNT);
		SetGrid(UI.GRD_NORMAL, "PointShopListItem", item_num, true, delegate(int i, Transform t, bool b)
		{
			int index = i + (currentPage - 1) * GameDefine.POINT_SHOP_LIST_COUNT;
			object event_data = new object[3]
			{
				currentPointShopItem[index],
				shop,
				new Action<PointShopItem, int>(OnBuy)
			};
			SetEvent(t, "CONFIRM_BUY", event_data);
			PointShopItemList component2 = t.GetComponent<PointShopItemList>();
			component2.SetUp(currentPointShopItem[index], (uint)shop.pointShopId, currentPointShopItem[index].needPoint <= shop.userPoint);
			int num = -1;
			REWARD_TYPE type = (REWARD_TYPE)currentPointShopItem[index].type;
			if (type == REWARD_TYPE.ITEM)
			{
				num = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum((uint)currentPointShopItem[index].itemId);
			}
			SetLabelText(t, UI.LBL_HAVE, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 6u), num.ToString()));
			SetActive(t, UI.LBL_HAVE, num >= 0);
		});
		bool flag = pointShop.Any((PointShop x) => x.isEvent);
		SetActive((Enum)UI.OBJ_EVENT_NON_ACTIVE, !flag);
		SetActive((Enum)UI.BTN_EVENT, flag);
	}

	private void ViewEventTab()
	{
		SetActive((Enum)UI.OBJ_NORMAL, false);
		SetActive((Enum)UI.OBJ_TAB_ROOT, true);
		SetActive((Enum)UI.OBJ_ON_TAB_NORMAL, false);
		SetActive((Enum)UI.OBJ_ON_TAB_EVENT, true);
		SetActive((Enum)UI.OBJ_EVENT_LIST, true);
		List<PointShop> current = (from x in pointShop
		where x.isEvent
		select x).ToList();
		SetGrid(UI.GRD_EVENT_LIST, "PointShopEventList", current.Count, true, delegate(int i, Transform t, bool b)
		{
			PointShop pointShop = current[i];
			UITexture component = FindCtrl(t, UI.TEX_EVENT_LIST_BANNER).GetComponent<UITexture>();
			UITexture component2 = FindCtrl(t, UI.TXT_EVENT_LIST_POINT_ICON).GetComponent<UITexture>();
			SetLabelText(t, UI.LBL_EVENT_LIST_POINT, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShop.userPoint));
			ResourceLoad.LoadPointIconImageTexture(component2, (uint)pointShop.pointShopId);
			ResourceLoad.LoadPointShopBannerTexture(component, (uint)pointShop.pointShopId);
			SetEvent(FindCtrl(t, UI.TEX_EVENT_LIST_BANNER), "EVENT_SHOP", pointShop);
			int num = (from x in pointShop.items
			where x.isBuyable
			where x.type != 8 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedStamp(x.itemId)
			where x.type != 9 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedDegree(x.itemId)
			where x.type != 7 || !MonoBehaviourSingleton<GlobalSettingsManager>.I.IsUnlockedAvatar(x.itemId)
			select x).Count();
			bool flag = num == 0;
			SetActive(t, UI.LBL_EVENT_LIST_SOLD_OUT, flag);
			SetButtonEnabled(t, UI.TEX_EVENT_LIST_BANNER, !flag);
			SetLabelText(t, UI.LBL_EVENT_LIST_REMAINING_TIME, pointShop.expire);
		});
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

	private void OnQuery_ON_EVENT()
	{
		currentType = VIEW_TYPE.EVENT_LIST;
		UpdateTab();
	}

	private void OnQuery_ON_NORMAL()
	{
		currentType = VIEW_TYPE.NORMAL;
		UpdateTab();
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.PointShop);
	}

	private void OnBuy(PointShopItem item, int num)
	{
		string boughtMessage = PointShopManager.GetBoughtMessage(item, num);
		GameSection.SetEventData(boughtMessage);
		GameSection.StayEvent();
		PointShop pointShop = this.pointShop.First((PointShop x) => x.items.Contains(item));
		MonoBehaviourSingleton<UserInfoManager>.I.PointShopManager.SendPointShopBuy(item, pointShop, num, delegate(bool isSuccess)
		{
			if (isSuccess)
			{
				UpdateTab();
			}
			GameSection.ResumeEvent(isSuccess, null);
		});
	}

	private void OnQuery_PAGE_NEXT()
	{
		currentPage++;
		if (currentPage > maxPage)
		{
			currentPage = 1;
		}
		UpdateTab();
	}

	private void OnQuery_PAGE_PREV()
	{
		currentPage--;
		if (currentPage < 1)
		{
			currentPage = ((maxPage <= 0) ? 1 : maxPage);
		}
		UpdateTab();
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
		return (from x in (from x in pointShop
		where !x.isEvent
		select x).SelectMany((PointShop x) => x.items)
		where x.isBuyable
		where x.type != 8 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedStamp(x.itemId)
		where x.type != 9 || !MonoBehaviourSingleton<UserInfoManager>.I.IsUnlockedDegree(x.itemId)
		where x.type != 7 || !MonoBehaviourSingleton<GlobalSettingsManager>.I.IsUnlockedAvatar(x.itemId)
		select x).ToList();
	}
}
