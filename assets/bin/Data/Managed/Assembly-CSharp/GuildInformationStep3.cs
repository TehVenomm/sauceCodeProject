using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildInformationStep3 : UserListBase<FriendCharaInfo>
{
	protected enum UI
	{
		GRD_LIST,
		TEX_MODEL,
		STR_NON_LIST,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
		OBJ_COMMENT,
		LBL_COMMENT,
		LBL_LAST_LOGIN,
		LBL_LAST_LOGIN_TIME,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_LEVEL,
		LBL_NAME,
		OBJ_DISABLE_USER_MASK,
		SPR_SELECTED,
		STR_LIMIT_LVL
	}

	private List<int> mInviteFriendIDs = new List<int>();

	protected virtual string GetListItemName => "GuildListBaseItem";

	public override void Initialize()
	{
		base.Initialize();
	}

	public virtual void ListUI()
	{
		FriendCharaInfo[] array = null;
		if (recvList != null && recvList.Count > 0)
		{
			array = recvList.ToArray();
		}
		if (array == null || array.Length == 0)
		{
			SetActive((Enum)UI.STR_NON_LIST, true);
			SetActive((Enum)UI.GRD_LIST, false);
		}
		else
		{
			SetActive((Enum)UI.STR_NON_LIST, false);
			SetActive((Enum)UI.GRD_LIST, true);
			UpdateDynamicList();
		}
	}

	protected unsafe virtual void UpdateDynamicList()
	{
		FriendCharaInfo[] info = null;
		int item_num = 0;
		if (recvList != null && recvList.Count > 0)
		{
			info = recvList.ToArray();
			if (info != null)
			{
				item_num = info.Length;
			}
		}
		_003CUpdateDynamicList_003Ec__AnonStorey318 _003CUpdateDynamicList_003Ec__AnonStorey;
		SetDynamicList((Enum)UI.GRD_LIST, GetListItemName, item_num, false, null, null, new Action<int, Transform, bool>((object)_003CUpdateDynamicList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected virtual void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
	{
		SetFollowStatus(t, data.userId, data.following, data.follower);
		SetCharaInfo(data, i, t, is_recycle, 0 == data.userId);
	}

	protected void SetCharaInfo(FriendCharaInfo data, int i, Transform t, bool is_recycle, bool isGM)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		object[] event_data = new object[2]
		{
			data.userId,
			t
		};
		SetEvent(t, "SELECT_FRIEND", event_data);
		if (isGM)
		{
			SetRenderNPCModel(t, UI.TEX_MODEL, 0, new Vector3(0f, -1.49f, 1.87f), new Vector3(0f, 154f, 0f), 10f, null);
		}
		else
		{
			SetRenderPlayerModel(t, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(data, false, true, false, true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), true, null);
		}
		SetLabelText(t, UI.LBL_NAME, data.name);
		SetLabelText(t, UI.LBL_LEVEL, data.level.ToString());
		SetLabelText(t, UI.LBL_COMMENT, data.comment);
		SetLabelText(t, UI.LBL_LAST_LOGIN, base.sectionData.GetText("LAST_LOGIN"));
		SetLabelText(t, UI.LBL_LAST_LOGIN_TIME, data.lastLogin);
		EquipSetCalculator otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4);
		otherEquipSetCalculator.SetEquipSet(data.equipSet, false);
		SimpleStatus finalStatus = otherEquipSetCalculator.GetFinalStatus(0, data.hp, data.atk, data.def);
		SetLabelText(t, UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
		SetLabelText(t, UI.LBL_DEF, finalStatus.defences[0].ToString());
		SetLabelText(t, UI.LBL_HP, finalStatus.hp.ToString());
		if ((int)data.level < 15)
		{
			SetActive(t, UI.OBJ_DISABLE_USER_MASK, true);
			SetActive(t, UI.SPR_SELECTED, false);
			SetActive(t, UI.STR_LIMIT_LVL, true);
			SetEvent(t, "_", null);
		}
		else if (mInviteFriendIDs.Contains(data.userId))
		{
			SetActive(t, UI.OBJ_DISABLE_USER_MASK, true);
			SetActive(t, UI.SPR_SELECTED, true);
			SetActive(t, UI.STR_LIMIT_LVL, false);
		}
		else
		{
			SetActive(t, UI.OBJ_DISABLE_USER_MASK, false);
			SetActive(t, UI.SPR_SELECTED, false);
			SetActive(t, UI.STR_LIMIT_LVL, false);
		}
	}

	protected void SetFollowStatus(Transform t, int user_id, bool following, bool follower)
	{
		bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
		SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
		SetActive(t, UI.SPR_FOLLOW, !flag && following);
		SetActive(t, UI.SPR_FOLLOWER, !flag && follower);
	}

	protected List<FriendCharaInfo> ChangeData(List<FriendCharaInfo> chara_list)
	{
		List<FriendCharaInfo> new_list = new List<FriendCharaInfo>();
		chara_list.ForEach(delegate(FriendCharaInfo chara_info)
		{
			if (chara_info != null)
			{
				FriendCharaInfo item = new FriendCharaInfo
				{
					userId = chara_info.userId,
					name = chara_info.name,
					comment = chara_info.comment,
					lastLogin = chara_info.lastLogin,
					code = chara_info.code,
					hp = chara_info.hp,
					atk = chara_info.atk,
					def = chara_info.def,
					level = chara_info.level,
					sex = chara_info.sex,
					faceId = chara_info.faceId,
					hairId = chara_info.hairId,
					hairColorId = chara_info.hairColorId,
					skinId = chara_info.skinId,
					voiceId = chara_info.voiceId,
					aId = chara_info.aId,
					hId = chara_info.hId,
					rId = chara_info.rId,
					lId = chara_info.lId,
					showHelm = chara_info.showHelm,
					equipSet = chara_info.equipSet,
					following = chara_info.following,
					follower = chara_info.follower,
					selectedDegrees = chara_info.selectedDegrees
				};
				new_list.Add(item);
			}
		});
		return new_list;
	}

	public virtual void OnQuery_SELECT_FRIEND()
	{
		object[] array = (object[])GameSection.GetEventData();
		if (array != null && array.Length >= 2)
		{
			int item = (int)array[0];
			if (!mInviteFriendIDs.Contains(item))
			{
				mInviteFriendIDs.Add(item);
			}
			Transform root = array[1] as Transform;
			SetActive(root, UI.OBJ_DISABLE_USER_MASK, true);
			SetActive(root, UI.SPR_SELECTED, true);
			SetActive(root, UI.STR_LIMIT_LVL, false);
		}
	}

	protected void SetDirtyTable()
	{
		SetDirty(UI.GRD_LIST);
	}

	public override void UpdateUI()
	{
		ListUI();
	}

	protected unsafe override void SendGetList(int page, Action<bool> callback)
	{
		_003CSendGetList_003Ec__AnonStorey31A _003CSendGetList_003Ec__AnonStorey31A;
		MonoBehaviourSingleton<FriendManager>.I.SendGetFollowList(page, new Action<bool, FriendFollowListModel.Param>((object)_003CSendGetList_003Ec__AnonStorey31A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override void PostSendGetListByReopen(int page)
	{
		SetDirtyTable();
		base.PostSendGetListByReopen(page);
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_FRIEND_PARAM) != (NOTIFY_FLAG)0L)
		{
			isInitializeSendReopen = true;
		}
		if ((flags & NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (NOTIFY_FLAG)0L)
		{
			SetDirtyTable();
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_LIST;
	}

	private unsafe void OnQuery_CREATE()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendCreate(mInviteFriendIDs, new Action<bool, Error>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void OnQuery_CLOSE()
	{
		MonoBehaviourSingleton<GuildManager>.I.ClearCreateGuildRequestParam();
	}
}
