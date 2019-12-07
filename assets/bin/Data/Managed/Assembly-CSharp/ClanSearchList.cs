using Network;
using System;
using System.Collections;
using UnityEngine;

public class ClanSearchList : GameSection
{
	protected enum UI
	{
		GRD_CLAN,
		STR_NON_LIST,
		STR_SORT,
		STR_CLAN_LV,
		LBL_CLAN_LV,
		LBL_CLAN_NAME,
		LBL_HOST_NAME,
		LBL_CLAN_LABEL,
		LBL_CLAN_COMMENT,
		LBL_CLAN_MAX_MEMBER_NUM,
		LBL_CLAN_MEMBER_NUM,
		OBJ_SYMBOL,
		OBJ_SYMBOL_MARK,
		TEX_STAMP,
		LBL_NOW,
		LBL_MAX,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT
	}

	public enum SortType
	{
		CreateOld,
		CreateNew,
		MemberMany,
		MemberFew
	}

	private ClanData[] clans;

	private SortType sortType;

	private const int SHOW_NUM = 10;

	private int nowPage = 1;

	private int pageMax = 1;

	public override void UpdateUI()
	{
		if (!ClanMatchingManager.IsValidNotEmptyList())
		{
			SetActive(UI.GRD_CLAN, is_visible: false);
			SetActive(UI.STR_NON_LIST, is_visible: true);
			SetActive(UI.OBJ_ACTIVE_ROOT, is_visible: false);
			SetActive(UI.OBJ_INACTIVE_ROOT, is_visible: true);
			SetLabelText(UI.LBL_MAX, "0");
			SetLabelText(UI.LBL_NOW, "0");
			return;
		}
		clans = MonoBehaviourSingleton<ClanMatchingManager>.I.clans.ToArray();
		SetActive(UI.GRD_CLAN, is_visible: true);
		SetActive(UI.STR_NON_LIST, is_visible: false);
		ClanData[] showList = clans;
		Array.Sort(showList, delegate(ClanData a, ClanData b)
		{
			switch (sortType)
			{
			case SortType.CreateOld:
				if (a.createdAt < b.createdAt)
				{
					return -1;
				}
				if (a.createdAt > b.createdAt)
				{
					return 1;
				}
				return 0;
			case SortType.CreateNew:
				if (a.createdAt < b.createdAt)
				{
					return 1;
				}
				if (a.createdAt > b.createdAt)
				{
					return -1;
				}
				return 0;
			case SortType.MemberFew:
				if (a.num < b.num)
				{
					return -1;
				}
				if (a.num > b.num)
				{
					return 1;
				}
				return 0;
			case SortType.MemberMany:
				if (a.num < b.num)
				{
					return 1;
				}
				if (a.num > b.num)
				{
					return -1;
				}
				return 0;
			default:
				return 0;
			}
		});
		pageMax = 1 + (showList.Length - 1) / 10;
		bool flag = pageMax > 1;
		SetActive(UI.OBJ_ACTIVE_ROOT, flag);
		SetActive(UI.OBJ_INACTIVE_ROOT, !flag);
		SetLabelText(UI.LBL_MAX, pageMax.ToString());
		SetLabelText(UI.LBL_NOW, nowPage.ToString());
		int num = 10 * (nowPage - 1);
		if (showList.Length <= num)
		{
			num = 0;
			nowPage = 1;
		}
		int num2 = (nowPage == pageMax) ? (showList.Length - num) : 10;
		ClanData[] array = new ClanData[num2];
		Array.Copy(showList, num, array, 0, num2);
		showList = array;
		SetGrid(UI.GRD_CLAN, "ClanSearchListItem", showList.Length, reset: true, delegate(int i, Transform t, bool is_recycle)
		{
			SetEvent(t, "SELECT", i);
			SetListItemData(showList[i], t);
		});
		SetLabelText(UI.STR_SORT, GetSortString(sortType));
		base.UpdateUI();
	}

	public override void Initialize()
	{
		MonoBehaviourSingleton<ClanMatchingManager>.I.ResetSearchRequest();
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		yield return StartCoroutine(Reload());
		base.Initialize();
	}

	private IEnumerator Reload(Action<bool> cb = null)
	{
		bool is_recv = false;
		SendRequest(delegate
		{
			is_recv = true;
		}, cb);
		while (!is_recv)
		{
			yield return null;
		}
		nowPage = 1;
		SetDirty(UI.GRD_CLAN);
		RefreshUI();
	}

	private void SendRequest(Action onFinish, Action<bool> cb)
	{
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestSearch(delegate(bool isSuccess, Error error)
		{
			onFinish();
			if (cb != null)
			{
				cb(isSuccess);
			}
		}, saveSettings: false);
	}

	private void SetListItemData(ClanData clan, Transform t)
	{
		ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
		SetLabelText(t, UI.LBL_CLAN_NAME, clan.name);
		SetLabelText(t, UI.LBL_CLAN_LV, clan.lv.ToString());
		SetLabelText(t, UI.LBL_HOST_NAME, clan.mName);
		SetLabelText(t, UI.LBL_CLAN_MEMBER_NUM, clan.num.ToString());
		SetLabelText(t, UI.LBL_CLAN_MAX_MEMBER_NUM, constDefine.CLAN_MAX_MEMBER_NUM.ToString());
		SetLabelText(t, UI.LBL_CLAN_COMMENT, clan.cmt);
		SetLabelText(t, UI.LBL_CLAN_LABEL, StringTable.Get(STRING_CATEGORY.CLAN_LABEL, (uint)clan.lbl));
		if (FindCtrl(t, UI.OBJ_SYMBOL_MARK) == null)
		{
			StartCoroutine(CreateSymbolMark(clan, t));
		}
		else
		{
			GetComponent<SymbolMarkCtrl>(t, UI.OBJ_SYMBOL_MARK).LoadSymbol(clan.sym);
		}
	}

	private IEnumerator CreateSymbolMark(ClanData clan, Transform t)
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject symbolMarkLoadObj = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
		yield return loadingQueue.Wait();
		Transform transform = ResourceUtility.Realizes(symbolMarkLoadObj.loadedObject as GameObject, 5);
		transform.parent = FindCtrl(t, UI.OBJ_SYMBOL);
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
		transform.name = "OBJ_SYMBOL_MARK";
		SymbolMarkCtrl component = transform.GetComponent<SymbolMarkCtrl>();
		component.Initilize();
		component.LoadSymbol(clan.sym);
	}

	private void OnQuery_RELOAD()
	{
		GameSection.StayEvent();
		StartCoroutine(Reload(delegate(bool b)
		{
			GameSection.ResumeEvent(b);
		}));
	}

	private void OnQuery_SELECT()
	{
		int num = (int)GameSection.GetEventData();
		int num2 = 10 * (nowPage - 1) + num;
		GameSection.SetEventData(clans[num2].cId);
	}

	private void OnQuery_SORT()
	{
		int num = (int)sortType;
		num++;
		if (num >= Enum.GetNames(typeof(SortType)).Length)
		{
			num = 0;
		}
		sortType = (SortType)num;
		nowPage = 1;
		RefreshUI();
	}

	private void OnCloseDialog_ClanSearchSettings()
	{
		nowPage = 1;
		RefreshUI();
	}

	private void OnQuery_PAGE_PREV()
	{
		nowPage = ((nowPage > 1) ? (nowPage - 1) : pageMax);
		RefreshUI();
	}

	private void OnQuery_PAGE_NEXT()
	{
		nowPage = ((nowPage >= pageMax) ? 1 : (nowPage + 1));
		RefreshUI();
	}

	private string GetSortString(SortType type)
	{
		switch (type)
		{
		case SortType.CreateOld:
			return "Oldest";
		case SortType.CreateNew:
			return "Newest";
		case SortType.MemberMany:
			return "Most Members";
		case SortType.MemberFew:
			return "Fewest Members";
		default:
			return string.Empty;
		}
	}
}
