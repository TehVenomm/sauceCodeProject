using Network;
using System;
using System.Collections;
using UnityEngine;

public class ClanScoutList : GameSection
{
	protected enum UI
	{
		GRD_CLAN,
		STR_NON_LIST,
		STR_SORT,
		LBL_CLAN_LV,
		LBL_CLAN_NAME,
		LBL_HOST_NAME,
		LBL_CLAN_LABEL,
		LBL_CLAN_COMMENT,
		LBL_CLAN_MAX_MEMBER_NUM,
		LBL_CLAN_MEMBER_NUM,
		LBL_LIMIT_TIME,
		BTN_DELETE,
		OBJ_SYMBOL,
		TEX_STAMP,
		LBL_NOW,
		LBL_MAX,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		OBJ_NEW,
		OBJ_SYMBOL_MARK
	}

	private int scoutRejectClanId;

	private int nowPage = 1;

	private int pageMax = 1;

	private const int SHOW_NUM = 10;

	private ClanData[] clans;

	public override void UpdateUI()
	{
		if (!ClanMatchingManager.IsScoutValidNotEmptyList())
		{
			SetActive(UI.GRD_CLAN, is_visible: false);
			SetActive(UI.STR_NON_LIST, is_visible: true);
			SetActive(UI.OBJ_ACTIVE_ROOT, is_visible: false);
			SetActive(UI.OBJ_INACTIVE_ROOT, is_visible: true);
			SetLabelText(UI.LBL_MAX, "0");
			SetLabelText(UI.LBL_NOW, "0");
			return;
		}
		clans = MonoBehaviourSingleton<ClanMatchingManager>.I.scoutClans.ToArray();
		Array.Sort(clans, delegate(ClanData a, ClanData b)
		{
			if (a.expiredAt > b.expiredAt)
			{
				return -1;
			}
			return (a.expiredAt < b.expiredAt) ? 1 : 0;
		});
		SetActive(UI.GRD_CLAN, is_visible: true);
		SetActive(UI.STR_NON_LIST, is_visible: false);
		pageMax = 1 + (clans.Length - 1) / 10;
		bool flag = pageMax > 1;
		SetActive(UI.OBJ_ACTIVE_ROOT, flag);
		SetActive(UI.OBJ_INACTIVE_ROOT, !flag);
		SetLabelText(UI.LBL_MAX, pageMax.ToString());
		SetLabelText(UI.LBL_NOW, nowPage.ToString());
		int num = 10 * (nowPage - 1);
		if (clans.Length <= num)
		{
			num = 0;
			nowPage = 1;
		}
		int num2 = (nowPage == pageMax) ? (clans.Length - num) : 10;
		ClanData[] showList = new ClanData[num2];
		Array.Copy(clans, num, showList, 0, num2);
		SetGrid(UI.GRD_CLAN, "ClanScoutListItem", showList.Length, reset: true, delegate(int i, Transform t, bool is_recycle)
		{
			SetEvent(t, "SELECT_SCOUT", i);
			SetListItemData(showList[i], t);
			SetEvent(FindCtrl(t, UI.BTN_DELETE), "DELETE_SCOUT", i);
		});
		base.UpdateUI();
	}

	public override void Initialize()
	{
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
		SetDirty(UI.GRD_CLAN);
		RefreshUI();
	}

	private void SendRequest(Action onFinish, Action<bool> cb)
	{
		MonoBehaviourSingleton<ClanMatchingManager>.I.SendClanInviteList(delegate(bool isSuccess)
		{
			onFinish();
			if (cb != null)
			{
				cb(isSuccess);
			}
		});
	}

	private void SendInviteReject(Action cb)
	{
		int clanId = scoutRejectClanId;
		MonoBehaviourSingleton<ClanMatchingManager>.I.SendClanRejectInvite(clanId, delegate(bool isSuccess)
		{
			if (isSuccess)
			{
				GameSection.ChangeStayEvent("COMPLETE");
			}
			if (cb != null)
			{
				cb();
			}
		});
	}

	private void SetListItemData(ClanData clan, Transform t)
	{
		ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
		SetLabelText(t, UI.LBL_CLAN_NAME, clan.name);
		SetLabelText(t, UI.LBL_CLAN_LV, clan.lv.ToString());
		SetLabelText(t, UI.LBL_HOST_NAME, clan.mName);
		SetLabelText(t, UI.LBL_CLAN_COMMENT, clan.cmt);
		SetLabelText(t, UI.LBL_CLAN_LABEL, StringTable.Get(STRING_CATEGORY.CLAN_LABEL, (uint)clan.lbl));
		SetLabelText(t, UI.LBL_CLAN_MEMBER_NUM, clan.num.ToString());
		SetLabelText(t, UI.LBL_CLAN_MAX_MEMBER_NUM, constDefine.CLAN_MAX_MEMBER_NUM.ToString());
		SetLabelText(t, UI.LBL_LIMIT_TIME, MonoBehaviourSingleton<ClanMatchingManager>.I.ConvertDateIntToString("", clan.expiredAt));
		bool is_visible = GameSaveData.instance.isNewClanScout(clan.cId, clan.expiredAt);
		SetActive(t, UI.OBJ_NEW, is_visible);
		if (FindCtrl(t, UI.OBJ_SYMBOL_MARK) == null)
		{
			StartCoroutine(CreateSymbolMark(clan, t));
		}
		else
		{
			GetComponent<SymbolMarkCtrl>(t, UI.OBJ_SYMBOL_MARK).LoadSymbol(clan.sym);
		}
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

	private void OnQuery_RELOAD()
	{
		GameSection.StayEvent();
		StartCoroutine(Reload(delegate(bool b)
		{
			GameSection.ResumeEvent(b);
		}));
	}

	private void OnQuery_SELECT_SCOUT()
	{
		int num = (int)GameSection.GetEventData();
		int num2 = 10 * (nowPage - 1) + num;
		GameSection.SetEventData(clans[num2].cId);
	}

	private void OnQuery_DELETE_SCOUT()
	{
		int num = (int)GameSection.GetEventData();
		int num2 = 10 * (nowPage - 1) + num;
		scoutRejectClanId = int.Parse(clans[num2].cId);
		GameSection.SetEventData(new string[1]
		{
			clans[num2].name
		});
	}

	private void OnQuery_ClanScoutDeleteDialog_YES()
	{
		GameSection.StayEvent();
		SendInviteReject(delegate
		{
			StartCoroutine(Reload(delegate(bool b)
			{
				GameSection.ResumeEvent(b);
			}));
		});
	}

	private void OnCloseDialog_ClanSearchSettings()
	{
		RefreshUI();
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
		component.SetSize(90);
	}
}
