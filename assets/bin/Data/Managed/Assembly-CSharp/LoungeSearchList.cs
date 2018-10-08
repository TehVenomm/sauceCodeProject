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
			SetActive((Enum)UI.GRD_LOUNGE, false);
			SetActive((Enum)UI.STR_NON_LIST, true);
		}
		else
		{
			lounges = MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges.ToArray();
			SetActive((Enum)UI.GRD_LOUNGE, true);
			SetActive((Enum)UI.STR_NON_LIST, false);
			SetGrid(UI.GRD_LOUNGE, "LoungeSearchListItem", lounges.Length, false, delegate(int i, Transform t, bool is_recycle)
			{
				SetEvent(t, "SELECT_LOUNGE", i);
				SetLoungeData(lounges[i], t);
			});
			base.UpdateUI();
		}
	}

	public override void Initialize()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		MonoBehaviourSingleton<LoungeMatchingManager>.I.ResetLoungeSearchRequest();
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		yield return (object)this.StartCoroutine(Reload(null));
		base.Initialize();
	}

	private IEnumerator Reload(Action<bool> cb = null)
	{
		bool is_recv = false;
		SendRequest(delegate
		{
			((_003CReload_003Ec__IteratorD1)/*Error near IL_002e: stateMachine*/)._003Cis_recv_003E__0 = true;
		}, cb);
		while (!is_recv)
		{
			yield return (object)null;
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
		}, false);
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
		int num2 = lounge.slotInfos.Count((LoungeModel.SlotInfo slotInfo) => slotInfo != null && slotInfo.userInfo != null && slotInfo.userInfo.userId != lounge.ownerUserId);
		for (int j = 0; j < 7; j++)
		{
			bool is_visible = j < num - 1;
			SetActive(t, loungeMembers[j], is_visible);
			SetToggle(t, loungeMembers[j], j < num2);
		}
	}

	private void OnQuery_RELOAD()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		GameSection.StayEvent();
		this.StartCoroutine(Reload(delegate(bool b)
		{
			GameSection.ResumeEvent(b, null);
		}));
	}

	private void OnQuery_SELECT_LOUNGE()
	{
		int num = (int)GameSection.GetEventData();
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEntry(lounges[num].id, delegate(bool isSuccess)
		{
			GameSection.ResumeEvent(isSuccess, null);
		});
	}

	private void OnCloseDialog_LoungeSearchSettings()
	{
		RefreshUI();
	}

	private void SetStamp(Transform root, int stampId)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		StampTable.Data data = Singleton<StampTable>.I.GetData((uint)stampId);
		if (data != null)
		{
			this.StartCoroutine(LoadStamp(root, stampId));
		}
	}

	private IEnumerator LoadStamp(Transform root, int stampId)
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_stamp = load_queue.LoadChatStamp(stampId, false);
		while (load_queue.IsLoading())
		{
			yield return (object)null;
		}
		if (lo_stamp.loadedObject != null)
		{
			Texture2D stamp = lo_stamp.loadedObject as Texture2D;
			SetActive((Enum)UI.OBJ_SYMBOL, true);
			SetTexture(root, UI.TEX_STAMP, stamp);
		}
	}
}
