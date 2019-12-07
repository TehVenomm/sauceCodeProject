using Network;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LoungeSearchList : GameSection
{
	protected enum UI
	{
		GRD_LOUNGE,
		STR_NON_LIST,
		LBL_HOST_NAME,
		LBL_HOST_LV,
		TGL_L_MEMBER_1,
		TGL_L_MEMBER_2,
		TGL_L_MEMBER_3,
		TGL_L_MEMBER_4,
		TGL_L_MEMBER_5,
		TGL_L_MEMBER_6,
		TGL_L_MEMBER_7,
		LBL_LOUNGE_NAME,
		LBL_LABEL,
		LBL_STYLE,
		OBJ_SYMBOL,
		TEX_STAMP
	}

	private LoungeModel.Lounge[] lounges;

	private UI[] loungeMembers = new UI[7]
	{
		UI.TGL_L_MEMBER_1,
		UI.TGL_L_MEMBER_2,
		UI.TGL_L_MEMBER_3,
		UI.TGL_L_MEMBER_4,
		UI.TGL_L_MEMBER_5,
		UI.TGL_L_MEMBER_6,
		UI.TGL_L_MEMBER_7
	};

	public override void UpdateUI()
	{
		if (!LoungeMatchingManager.IsValidNotEmptyList())
		{
			SetActive(UI.GRD_LOUNGE, is_visible: false);
			SetActive(UI.STR_NON_LIST, is_visible: true);
			return;
		}
		lounges = MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges.ToArray();
		SetActive(UI.GRD_LOUNGE, is_visible: true);
		SetActive(UI.STR_NON_LIST, is_visible: false);
		SetGrid(UI.GRD_LOUNGE, "LoungeSearchListItem", lounges.Length, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			SetEvent(t, "SELECT_LOUNGE", i);
			SetLoungeData(lounges[i], t);
		});
		base.UpdateUI();
	}

	public override void Initialize()
	{
		MonoBehaviourSingleton<LoungeMatchingManager>.I.ResetLoungeSearchRequest();
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
		SetDirty(UI.GRD_LOUNGE);
		RefreshUI();
	}

	private void SendRequest(Action onFinish, Action<bool> cb)
	{
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendSearch(delegate(bool isSuccess, Error error)
		{
			onFinish();
			if (cb != null)
			{
				cb(isSuccess);
			}
		}, saveSettings: false);
	}

	private void SetLoungeData(LoungeModel.Lounge lounge, Transform t)
	{
		CharaInfo charaInfo = null;
		for (int i = 0; i < lounge.slotInfos.Count; i++)
		{
			if (lounge.slotInfos[i].userInfo.userId == lounge.ownerUserId)
			{
				charaInfo = lounge.slotInfos[i].userInfo;
				break;
			}
		}
		SetLabelText(t, UI.LBL_HOST_NAME, charaInfo.name);
		SetLabelText(t, UI.LBL_HOST_LV, charaInfo.level.ToString());
		SetLabelText(t, UI.LBL_LOUNGE_NAME, lounge.name);
		string text = StringTable.Get(STRING_CATEGORY.LOUNGE_LABEL, (uint)lounge.label);
		SetLabelText(t, UI.LBL_LABEL, text);
		SetStamp(t, lounge.stampId);
		int num = lounge.num + 1;
		int num2 = lounge.slotInfos.Count((PartyModel.SlotInfo slotInfo) => slotInfo != null && slotInfo.userInfo != null && slotInfo.userInfo.userId != lounge.ownerUserId);
		for (int j = 0; j < 7; j++)
		{
			bool is_visible = j < num - 1;
			SetActive(t, loungeMembers[j], is_visible);
			SetToggle(t, loungeMembers[j], j < num2);
		}
	}

	private void OnQuery_RELOAD()
	{
		GameSection.StayEvent();
		StartCoroutine(Reload(delegate(bool b)
		{
			GameSection.ResumeEvent(b);
		}));
	}

	private void OnQuery_SELECT_LOUNGE()
	{
		int num = (int)GameSection.GetEventData();
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEntry(lounges[num].id, delegate(bool isSuccess)
		{
			GameSection.ResumeEvent(isSuccess);
		});
	}

	private void OnCloseDialog_LoungeSearchSettings()
	{
		RefreshUI();
	}

	private void SetStamp(Transform root, int stampId)
	{
		if (Singleton<StampTable>.I.GetData((uint)stampId) != null)
		{
			StartCoroutine(LoadStamp(root, stampId));
		}
	}

	private IEnumerator LoadStamp(Transform root, int stampId)
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_stamp = load_queue.LoadChatStamp(stampId);
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		if (lo_stamp.loadedObject != null)
		{
			Texture2D texture = lo_stamp.loadedObject as Texture2D;
			SetActive(UI.OBJ_SYMBOL, is_visible: true);
			SetTexture(root, UI.TEX_STAMP, texture);
		}
	}
}
