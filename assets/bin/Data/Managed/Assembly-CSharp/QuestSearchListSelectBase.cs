using Network;
using System;
using System.Collections;
using UnityEngine;

public abstract class QuestSearchListSelectBase : GameSection
{
	protected enum UI
	{
		GRD_QUEST,
		LBL_HOST_NAME,
		LBL_HOST_LV,
		TGL_MEMBER_1,
		TGL_MEMBER_2,
		TGL_MEMBER_3,
		LBL_LV,
		TEX_NPCMODEL,
		LBL_NPC_MESSAGE
	}

	protected UI[] ui = new UI[3]
	{
		UI.TGL_MEMBER_1,
		UI.TGL_MEMBER_2,
		UI.TGL_MEMBER_3
	};

	protected bool recommentUpdate;

	protected abstract void SendSearchRequest(Action onFinish, Action<bool> cb);

	protected abstract void ResetSearchRequest();

	protected abstract void SetQuestData(QuestTable.QuestTableData questData, Transform t);

	protected void CloseSearchRoomCondition()
	{
		recommentUpdate = true;
	}

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		ResetSearchRequest();
		bool is_recv = false;
		SendGetChallengeInfo(delegate
		{
			is_recv = true;
		}, null);
		while (!is_recv)
		{
			yield return null;
		}
		yield return StartCoroutine(Reload());
		base.Initialize();
	}

	private IEnumerator Reload(Action<bool> cb = null)
	{
		bool is_recv = false;
		SendSearchRequest(delegate
		{
			is_recv = true;
		}, cb);
		while (!is_recv)
		{
			yield return null;
		}
		SetDirty(UI.GRD_QUEST);
		RefreshUI();
	}

	protected void SetNpcMessage()
	{
		string nPCMessageBySectionData = Singleton<NPCMessageTable>.I.GetNPCMessageBySectionData(base.sectionData);
		SetRenderNPCModel(UI.TEX_NPCMODEL, 2, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCFOV);
		SetLabelText(UI.LBL_NPC_MESSAGE, nPCMessageBySectionData);
	}

	protected void SetPartyData(PartyModel.Party party, Transform t)
	{
		int member_num = 0;
		party.slotInfos.ForEach(delegate(PartyModel.SlotInfo data)
		{
			if (data != null && data.userInfo != null)
			{
				if (data.userInfo.userId == party.ownerUserId)
				{
					SetLabelText(t, UI.LBL_HOST_NAME, data.userInfo.name);
					SetLabelText(t, UI.LBL_HOST_LV, data.userInfo.level.ToString());
				}
				else
				{
					int num = ++member_num;
				}
			}
		});
		for (int i = 0; i < 3; i++)
		{
			SetToggle(t, ui[i], i < member_num);
		}
		SetLabelText(t, UI.LBL_LV, base.sectionData.GetText("LV"));
	}

	protected void SetStatusIconInfo(PartyModel.Party _partyParam, Transform _targetObject)
	{
		if (!(_targetObject == null) && _partyParam != null)
		{
			QuestUserStatusIconController componentInChildren = _targetObject.GetComponentInChildren<QuestUserStatusIconController>();
			if (componentInChildren != null)
			{
				componentInChildren.Initialize(new QuestUserStatusIconController.InitParam
				{
					StatusBit = (uint)_partyParam.iconBit
				});
			}
		}
	}

	public virtual void OnQuery_SELECT_ROOM()
	{
		int index = (int)GameSection.GetEventData();
		if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog((uint)MonoBehaviourSingleton<PartyManager>.I.partys[index].quest.questId))
		{
			GameSection.StopEvent();
			return;
		}
		GameSection.SetEventData(new object[1]
		{
			false
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendEntry(MonoBehaviourSingleton<PartyManager>.I.partys[index].id, isLoungeBoard: false, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	public virtual void OnQuery_RELOAD()
	{
		GameSection.StayEvent();
		StartCoroutine(Reload(delegate(bool b)
		{
			GameSection.ResumeEvent(b);
		}));
	}

	private void Update()
	{
		if (recommentUpdate)
		{
			recommentUpdate = false;
			RefreshUI();
		}
	}

	public void OnCloseDialog_QuestAcceptRoomInvalid()
	{
		StartCoroutine(Reload(delegate(bool b)
		{
			GameSection.ResumeEvent(b);
		}));
	}

	protected void SendGetChallengeInfo(Action onFinish, Action<bool> cb)
	{
		MonoBehaviourSingleton<PartyManager>.I.SendGetChallengeInfo(delegate(bool is_success, Error err)
		{
			if (onFinish != null)
			{
				onFinish();
			}
			if (cb != null)
			{
				cb(is_success);
			}
		});
	}

	protected void SetMemberIcon(Transform t, QuestTable.QuestTableData table)
	{
		if (table != null)
		{
			SetActive(t, UI.TGL_MEMBER_3, is_visible: true);
			SetActive(t, UI.TGL_MEMBER_2, is_visible: true);
			SetActive(t, UI.TGL_MEMBER_1, is_visible: true);
			if (table.userNumLimit < 4)
			{
				SetActive(t, UI.TGL_MEMBER_3, is_visible: false);
			}
			if (table.userNumLimit < 3)
			{
				SetActive(t, UI.TGL_MEMBER_2, is_visible: false);
			}
			if (table.userNumLimit < 2)
			{
				SetActive(t, UI.TGL_MEMBER_1, is_visible: false);
			}
		}
	}
}
