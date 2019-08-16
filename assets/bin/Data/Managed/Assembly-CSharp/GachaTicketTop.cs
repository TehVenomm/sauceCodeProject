using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GachaTicketTop : GameSection
{
	private enum UI
	{
		STR_TITLE,
		STR_TITLE_REFLECT,
		BTN_TO_GACHA,
		SCR_LIST,
		GRD_LIST,
		LBL_NOW,
		LBL_MAX,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		BTN_PAGE_NEXT,
		BTN_PAGE_PREV,
		STR_ORDER_NON_LIST,
		LBL_CAUTION,
		TEX_ICON,
		LBL_NAME,
		LBL_LIMIT,
		LBL_COUNTDOWN
	}

	private int nowPage = 1;

	private int pageMax = 1;

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.STR_TITLE, StringTable.Get(STRING_CATEGORY.COMMON, 103u));
		SetLabelText((Enum)UI.STR_TITLE_REFLECT, StringTable.Get(STRING_CATEGORY.COMMON, 103u));
		List<ItemInfo> itemList = MonoBehaviourSingleton<InventoryManager>.I.GetItemList((ItemInfo x) => x.tableData.type == ITEM_TYPE.TICKET);
		ExpiredItem[] showList = GetItemList(itemList).ToArray();
		GetCtrl(UI.LBL_CAUTION).GetComponent<UILabel>().supportEncoding = true;
		string text = StringTable.Get(STRING_CATEGORY.SHOP, 14u);
		SetLabelText((Enum)UI.LBL_CAUTION, text);
		SetActive((Enum)UI.BTN_TO_GACHA, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "ShopScene");
		SetActive((Enum)UI.GRD_LIST, showList.Length > 0);
		SetActive((Enum)UI.STR_ORDER_NON_LIST, showList.Length == 0);
		SetActive((Enum)UI.OBJ_ACTIVE_ROOT, showList.Length > 0);
		SetActive((Enum)UI.OBJ_INACTIVE_ROOT, showList.Length == 0);
		if (showList.Length == 0)
		{
			SetLabelText((Enum)UI.LBL_MAX, "0");
			SetLabelText((Enum)UI.LBL_NOW, "0");
			UIScrollView component = GetCtrl(UI.SCR_LIST).GetComponent<UIScrollView>();
			if (component != null)
			{
				component.set_enabled(false);
				component.verticalScrollBar.alpha = 0f;
			}
		}
		else
		{
			pageMax = 1 + (showList.Length - 1) / 10;
			bool flag = pageMax > 1;
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, flag);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, !flag);
			SetLabelText((Enum)UI.LBL_MAX, pageMax.ToString());
			SetLabelText((Enum)UI.LBL_NOW, nowPage.ToString());
			int num = 10 * (nowPage - 1);
			int num2 = (nowPage != pageMax) ? 10 : (showList.Length - num);
			ExpiredItem[] array = new ExpiredItem[num2];
			Array.Copy(showList, num, array, 0, num2);
			showList = array;
			SetGrid(UI.GRD_LIST, "GachaTicketListItem", showList.Length, reset: true, delegate(int i, Transform t, bool is_recycle)
			{
				ExpiredItem expiredItem = showList[i];
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)expiredItem.itemId);
				UITexture component2 = FindCtrl(t, UI.TEX_ICON).GetComponent<UITexture>();
				ResourceLoad.LoadItemIconTexture(component2, itemData.iconID);
				SetLabelText(t, UI.LBL_NAME, itemData.name);
				string empty = string.Empty;
				string empty2 = string.Empty;
				if (string.IsNullOrEmpty(expiredItem.expiredAt))
				{
					empty = "-";
					empty2 = "-";
				}
				else
				{
					empty = expiredItem.expiredAt;
					empty2 = TimeManager.GetRemainTimeToText(expiredItem.expiredAt, 1);
				}
				SetLabelText(t, UI.LBL_LIMIT, empty);
				SetLabelText(t, UI.LBL_COUNTDOWN, empty2);
			});
		}
	}

	private List<ExpiredItem> GetItemList(List<ItemInfo> list)
	{
		List<ExpiredItem> list2 = new List<ExpiredItem>();
		foreach (ItemInfo item in list)
		{
			if (item.expiredAtItem != null)
			{
				foreach (ExpiredItem item2 in item.expiredAtItem)
				{
					if (item2.CanUse())
					{
						list2.Add(item2);
					}
				}
			}
		}
		list2.Sort((ExpiredItem a, ExpiredItem b) => a.expiredAt.CompareTo(b.expiredAt));
		return list2;
	}

	private void OnQuery_CAUTION()
	{
		GameSection.SetEventData(WebViewManager.GachaTicket);
	}

	private void OnQuery_PAGE_PREV()
	{
		nowPage = ((nowPage <= 1) ? pageMax : (nowPage - 1));
		RefreshUI();
	}

	private void OnQuery_PAGE_NEXT()
	{
		nowPage = ((nowPage >= pageMax) ? 1 : (nowPage + 1));
		RefreshUI();
	}
}
