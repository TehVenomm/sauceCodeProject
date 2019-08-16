using Network;
using System;
using System.Collections;
using UnityEngine;

public class ClanMemberSettings : GameSection
{
	private enum UI
	{
		TEX_MODEL,
		LBL_NAME,
		LBL_LEVEL,
		SPR_STATUS,
		LBL_TARGET_STATUS,
		POP_TARGET_STATUS
	}

	private FriendCharaInfo userInfo;

	private string[] statuses;

	private string[] dispStatuses;

	private int selectedStatus;

	private Transform statusPopup;

	public override void Initialize()
	{
		FriendCharaInfo friendCharaInfo = GameSection.GetEventData() as FriendCharaInfo;
		if (friendCharaInfo != null)
		{
			userInfo = friendCharaInfo;
		}
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		statuses = StringTable.GetAllInCategory(STRING_CATEGORY.CLAN_STATUS);
		dispStatuses = new string[3]
		{
			StringTable.Get(STRING_CATEGORY.CLAN_STATUS, 1u),
			StringTable.Get(STRING_CATEGORY.CLAN_STATUS, 2u),
			StringTable.Get(STRING_CATEGORY.CLAN_STATUS, 3u)
		};
		if (statuses.Length > userInfo.userClanData.stat)
		{
			string value = statuses[userInfo.userClanData.stat];
			selectedStatus = Array.IndexOf(dispStatuses, value);
		}
		yield return null;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		SetRenderPlayerModel((Enum)UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(userInfo, need_weapon: false, need_helm: true, need_leg: false, is_priority_visual_equip: true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), is_priority_visual_equip: true, (Action<PlayerLoader>)null);
		SetLabelText((Enum)UI.LBL_NAME, userInfo.name);
		SetLabelText((Enum)UI.LBL_LEVEL, userInfo.level.ToString());
		SetStatusSprite(userInfo.userClanData);
		UpdateStatus();
	}

	private void UpdateStatus()
	{
		int num = selectedStatus;
		SetLabelText((Enum)UI.LBL_TARGET_STATUS, dispStatuses[num]);
	}

	private void SetStatusSprite(UserClanData userClan)
	{
		if (userClan.IsLeader())
		{
			SetActive((Enum)UI.SPR_STATUS, is_visible: true);
			SetSprite((Enum)UI.SPR_STATUS, "Clan_HeadmasterIcon");
		}
		else if (userClan.IsSubLeader())
		{
			SetActive((Enum)UI.SPR_STATUS, is_visible: true);
			SetSprite((Enum)UI.SPR_STATUS, "Clan_DeputyHeadmasterIcon");
		}
		else
		{
			SetActive((Enum)UI.SPR_STATUS, is_visible: false);
		}
	}

	private void OnQuery_TARGET_STATUS()
	{
		if (statusPopup == null)
		{
			statusPopup = Realizes("ScrollablePopupList", GetCtrl(UI.POP_TARGET_STATUS), check_panel: false);
		}
		if (!(statusPopup == null))
		{
			bool[] array = new bool[dispStatuses.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = true;
			}
			int select_index = selectedStatus;
			UIScrollablePopupList.CreatePopup(statusPopup, GetCtrl(UI.POP_TARGET_STATUS), 5, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, adjust_size: true, dispStatuses, array, select_index, delegate(int index)
			{
				selectedStatus = index;
				RefreshUI();
			});
		}
	}

	private void OnQuery_CHANGE()
	{
		string text = dispStatuses[selectedStatus];
		GameSection.SetEventData(new string[2]
		{
			userInfo.name,
			text
		});
	}

	private void OnQuery_ClanMemberStatusChangeConfirmDialog_YES()
	{
		GameSection.StayEvent();
		string value = dispStatuses[selectedStatus];
		int stat = Array.IndexOf(statuses, value);
		ClanEditMemberModel.RequestSendForm requestSendForm = new ClanEditMemberModel.RequestSendForm();
		requestSendForm.uId = userInfo.userId;
		requestSendForm.stat = stat;
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestEditMember(requestSendForm, delegate(bool isSuccess)
		{
			if (isSuccess)
			{
				MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, delegate(UserClanData userClanData)
				{
					MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
					GameSection.ResumeEvent(isSuccess);
				});
			}
		});
	}

	private void OnQuery_KICK()
	{
		GameSection.SetEventData(new string[2]
		{
			userInfo.name,
			userInfo.userClanData.name
		});
	}

	private void OnQuery_ClanKickConfirmDialog_YES()
	{
		GameSection.StayEvent();
		ClanKickModel.RequestSendForm requestSendForm = new ClanKickModel.RequestSendForm();
		requestSendForm.uId = userInfo.userId;
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestKick(requestSendForm, delegate(bool isSuccess)
		{
			if (MonoBehaviourSingleton<ClanManager>.IsValid())
			{
				MonoBehaviourSingleton<ClanManager>.I.IHomePeople.CastToLoungePeople().DestroyLoungePlayer(userInfo.userId);
			}
			GameSection.ResumeEvent(isSuccess);
		});
	}
}
