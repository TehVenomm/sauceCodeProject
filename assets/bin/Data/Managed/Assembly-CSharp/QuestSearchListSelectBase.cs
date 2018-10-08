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
			((_003CDoInitialize_003Ec__IteratorC6)/*Error near IL_003d: stateMachine*/)._003Cis_recv_003E__0 = true;
		}, null);
		while (!is_recv)
		{
			yield return (object)null;
		}
		yield return (object)StartCoroutine(Reload(null));
		base.Initialize();
	}

	private IEnumerator Reload(Action<bool> cb = null)
	{
		bool is_recv = false;
		SendSearchRequest(delegate
		{
			((_003CReload_003Ec__IteratorC7)/*Error near IL_002e: stateMachine*/)._003Cis_recv_003E__0 = true;
		}, cb);
		while (!is_recv)
		{
			yield return (object)null;
		}
		SetDirty(UI.GRD_QUEST);
		RefreshUI();
	}

	protected void SetNpcMessage()
	{
		string nPCMessageBySectionData = Singleton<NPCMessageTable>.I.GetNPCMessageBySectionData(base.sectionData);
		SetRenderNPCModel(UI.TEX_NPCMODEL, 2, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCFOV, null);
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
					member_num++;
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
		if (!((UnityEngine.Object)_targetObject == (UnityEngine.Object)null) && _partyParam != null)
		{
			QuestUserStatusIconController componentInChildren = _targetObject.GetComponentInChildren<QuestUserStatusIconController>();
			if ((UnityEngine.Object)componentInChildren != (UnityEngine.Object)null)
			{
				componentInChildren.Initialize(new QuestUserStatusIconController.InitParam
				{
					StatusBit = _partyParam.iconBit
				});
			}
		}
	}

	public virtual void OnQuery_SELECT_ROOM()
	{
		int index = (int)GameSection.GetEventData();
		if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog((uint)MonoBehaviourSingleton<PartyManager>.I.partys[index].quest.questId, true))
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[1]
			{
				false
			});
			GameSection.StayEvent();
			MonoBehaviourSingleton<PartyManager>.I.SendEntry(MonoBehaviourSingleton<PartyManager>.I.partys[index].id, false, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
			});
		}
	}

	public virtual void OnQuery_RELOAD()
	{
		GameSection.StayEvent();
		StartCoroutine(Reload(delegate(bool b)
		{
			GameSection.ResumeEvent(b, null);
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
			GameSection.ResumeEvent(b, null);
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
			if (table.userNumLimit < 4)
			{
				SetActive(t, UI.TGL_MEMBER_3, false);
			}
			if (table.userNumLimit < 3)
			{
				SetActive(t, UI.TGL_MEMBER_2, false);
			}
			if (table.userNumLimit < 2)
			{
				SetActive(t, UI.TGL_MEMBER_1, false);
			}
		}
	}
}
