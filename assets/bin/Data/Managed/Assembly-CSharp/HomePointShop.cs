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

	public unsafe IEnumerator DoInitialize()
	{
		currentType = VIEW_TYPE.NORMAL;
		currentPage = 1;
		bool hasList = false;
		LoadingQueue loadingQueue = new LoadingQueue(this);
		loadingQueue.Load(RESOURCE_CATEGORY.UI, "PointShopListItem", false);
		MonoBehaviourSingleton<UserInfoManager>.I.PointShopManager.SendGetPointShops(new Action<bool, List<PointShop>>((object)/*Error near IL_0073: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	private unsafe void ViewNormalTab()
	{
		SetActive((Enum)UI.OBJ_NORMAL, true);
		SetActive((Enum)UI.OBJ_TAB_ROOT, true);
		SetActive((Enum)UI.OBJ_ON_TAB_NORMAL, true);
		SetActive((Enum)UI.OBJ_ON_TAB_EVENT, false);
		SetActive((Enum)UI.OBJ_EVENT_LIST, false);
		List<PointShop> source = pointShop;
		if (_003C_003Ef__am_0024cache7 == null)
		{
			_003C_003Ef__am_0024cache7 = new Func<PointShop, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		PointShop shop = source.First(_003C_003Ef__am_0024cache7);
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
		_003CViewNormalTab_003Ec__AnonStorey38A _003CViewNormalTab_003Ec__AnonStorey38A;
		SetGrid(UI.GRD_NORMAL, "PointShopListItem", item_num, true, new Action<int, Transform, bool>((object)_003CViewNormalTab_003Ec__AnonStorey38A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		List<PointShop> source2 = pointShop;
		if (_003C_003Ef__am_0024cache8 == null)
		{
			_003C_003Ef__am_0024cache8 = new Func<PointShop, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		bool flag = source2.Any(_003C_003Ef__am_0024cache8);
		SetActive((Enum)UI.OBJ_EVENT_NON_ACTIVE, !flag);
		SetActive((Enum)UI.BTN_EVENT, flag);
	}

	private unsafe void ViewEventTab()
	{
		SetActive((Enum)UI.OBJ_NORMAL, false);
		SetActive((Enum)UI.OBJ_TAB_ROOT, true);
		SetActive((Enum)UI.OBJ_ON_TAB_NORMAL, false);
		SetActive((Enum)UI.OBJ_ON_TAB_EVENT, true);
		SetActive((Enum)UI.OBJ_EVENT_LIST, true);
		List<PointShop> source = pointShop;
		if (_003C_003Ef__am_0024cache9 == null)
		{
			_003C_003Ef__am_0024cache9 = new Func<PointShop, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		List<PointShop> current = source.Where(_003C_003Ef__am_0024cache9).ToList();
		_003CViewEventTab_003Ec__AnonStorey38B _003CViewEventTab_003Ec__AnonStorey38B;
		SetGrid(UI.GRD_EVENT_LIST, "PointShopEventList", current.Count, true, new Action<int, Transform, bool>((object)_003CViewEventTab_003Ec__AnonStorey38B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	private unsafe void OnBuy(PointShopItem item, int num)
	{
		string boughtMessage = PointShopManager.GetBoughtMessage(item, num);
		GameSection.SetEventData(boughtMessage);
		GameSection.StayEvent();
		_003COnBuy_003Ec__AnonStorey38C _003COnBuy_003Ec__AnonStorey38C;
		PointShop pointShop = this.pointShop.First(new Func<PointShop, bool>((object)_003COnBuy_003Ec__AnonStorey38C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	private unsafe List<PointShopItem> GetBuyableItemList()
	{
		List<PointShop> source = pointShop;
		if (_003C_003Ef__am_0024cacheA == null)
		{
			_003C_003Ef__am_0024cacheA = new Func<PointShop, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<PointShop> source2 = source.Where(_003C_003Ef__am_0024cacheA);
		if (_003C_003Ef__am_0024cacheB == null)
		{
			_003C_003Ef__am_0024cacheB = new Func<PointShop, IEnumerable<PointShopItem>>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<PointShopItem> source3 = source2.SelectMany<PointShop, PointShopItem>(_003C_003Ef__am_0024cacheB);
		if (_003C_003Ef__am_0024cacheC == null)
		{
			_003C_003Ef__am_0024cacheC = new Func<PointShopItem, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<PointShopItem> source4 = source3.Where(_003C_003Ef__am_0024cacheC);
		if (_003C_003Ef__am_0024cacheD == null)
		{
			_003C_003Ef__am_0024cacheD = new Func<PointShopItem, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<PointShopItem> source5 = source4.Where(_003C_003Ef__am_0024cacheD);
		if (_003C_003Ef__am_0024cacheE == null)
		{
			_003C_003Ef__am_0024cacheE = new Func<PointShopItem, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		IEnumerable<PointShopItem> source6 = source5.Where(_003C_003Ef__am_0024cacheE);
		if (_003C_003Ef__am_0024cacheF == null)
		{
			_003C_003Ef__am_0024cacheF = new Func<PointShopItem, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		return source6.Where(_003C_003Ef__am_0024cacheF).ToList();
	}
}
