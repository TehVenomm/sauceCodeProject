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
			SetActive((Enum)UI.GRD_CLAN, is_visible: false);
			SetActive((Enum)UI.STR_NON_LIST, is_visible: true);
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, is_visible: false);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, is_visible: true);
			SetLabelText((Enum)UI.LBL_MAX, "0");
			SetLabelText((Enum)UI.LBL_NOW, "0");
			return;
		}
		clans = MonoBehaviourSingleton<ClanMatchingManager>.I.scoutClans.ToArray();
		Array.Sort(clans, delegate(ClanData a, ClanData b)
		{
			if (a.expiredAt > b.expiredAt)
			{
				return -1;
			}
			if (a.expiredAt < b.expiredAt)
			{
				return 1;
			}
			return 0;
		});
		SetActive((Enum)UI.GRD_CLAN, is_visible: true);
		SetActive((Enum)UI.STR_NON_LIST, is_visible: false);
		pageMax = 1 + (clans.Length - 1) / 10;
		bool flag = pageMax > 1;
		SetActive((Enum)UI.OBJ_ACTIVE_ROOT, flag);
		SetActive((Enum)UI.OBJ_INACTIVE_ROOT, !flag);
		SetLabelText((Enum)UI.LBL_MAX, pageMax.ToString());
		SetLabelText((Enum)UI.LBL_NOW, nowPage.ToString());
		int num = 10 * (nowPage - 1);
		if (clans.Length <= num)
		{
			num = 0;
			nowPage = 1;
		}
		int num2 = (nowPage != pageMax) ? 10 : (clans.Length - num);
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
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		yield return this.StartCoroutine(Reload());
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
		SetLabelText(t, UI.LBL_LIMIT_TIME, MonoBehaviourSingleton<ClanMatchingManager>.I.ConvertDateIntToString(string.Empty, clan.expiredAt));
		bool is_visible = GameSaveData.instance.isNewClanScout(clan.cId, clan.expiredAt);
		SetActive(t, UI.OBJ_NEW, is_visible);
		if (FindCtrl(t, UI.OBJ_SYMBOL_MARK) == null)
		{
			this.StartCoroutine(CreateSymbolMark(clan, t));
		}
		else
		{
			base.GetComponent<SymbolMarkCtrl>(t, (Enum)UI.OBJ_SYMBOL_MARK).LoadSymbol(clan.sym);
		}
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

	private void OnQuery_RELOAD()
	{
		GameSection.StayEvent();
		this.StartCoroutine(Reload(delegate(bool b)
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
			this.StartCoroutine(Reload(delegate(bool b)
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
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject symbolMarkLoadObj = load_queue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
		yield return load_queue.Wait();
		GameObject obj = symbolMarkLoadObj.loadedObject as GameObject;
		Transform item = ResourceUtility.Realizes(obj, 5);
		item.set_parent(FindCtrl(t, UI.OBJ_SYMBOL));
		item.set_localScale(Vector3.get_one());
		item.set_localPosition(Vector3.get_zero());
		item.set_name("OBJ_SYMBOL_MARK");
		SymbolMarkCtrl symbolMark = item.GetComponent<SymbolMarkCtrl>();
		symbolMark.Initilize();
		symbolMark.LoadSymbol(clan.sym);
		symbolMark.SetSize(90);
	}
}
