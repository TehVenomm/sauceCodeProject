using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildListBase : UserListBase<FriendCharaInfo>
{
	protected enum UI
	{
		OBJ_GUILD_NUMBER_ROOT,
		LBL_GUILD_NUMBER_NOW,
		LBL_GUILD_NUMBER_MAX,
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
		LBL_NAME,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_LEVEL,
		LBL_NOW,
		LBL_MAX,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		BTN_PAGE_PREV,
		BTN_PAGE_NEXT,
		OBJ_DEGREE_FRAME_ROOT,
		SPR_ICON_FIRST_MET
	}

	protected virtual string GetListItemName => "GuildListBaseItem";

	public override void Initialize()
	{
		SetActive((Enum)UI.OBJ_GUILD_NUMBER_ROOT, false);
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
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, false);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, true);
			SetLabelText((Enum)UI.LBL_NOW, "0");
			SetLabelText((Enum)UI.LBL_MAX, "0");
		}
		else
		{
			SetPageNumText((Enum)UI.LBL_NOW, nowPage + 1);
			SetPageNumText((Enum)UI.LBL_MAX, pageNumMax);
			SetActive((Enum)UI.STR_NON_LIST, false);
			SetActive((Enum)UI.GRD_LIST, true);
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, pageNumMax != 1);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, pageNumMax == 1);
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
		if (GameDefine.ACTIVE_DEGREE)
		{
			UIGrid component = GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>();
			component.cellHeight = (float)GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
		}
		_003CUpdateDynamicList_003Ec__AnonStorey34D _003CUpdateDynamicList_003Ec__AnonStorey34D;
		SetDynamicList((Enum)UI.GRD_LIST, GetListItemName, item_num, false, null, null, new Action<int, Transform, bool>((object)_003CUpdateDynamicList_003Ec__AnonStorey34D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected virtual void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
	{
		SetFollowStatus(t, data.userId, data.following, data.follower);
		SetCharaInfo(data, i, t, is_recycle, 0 == data.userId);
		if (LoungeMatchingManager.IsValidInLounge())
		{
			SetActive(t, UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(data.userId));
		}
	}

	protected void SetCharaInfo(FriendCharaInfo data, int i, Transform t, bool is_recycle, bool isGM)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		SetEvent(t, "GUILD_INFO", i);
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
		DegreePlate component = FindCtrl(t, UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>();
		component.Initialize(data.selectedDegrees, false, delegate
		{
			GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>().Reposition();
		});
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

	public virtual void OnQuery_GUILD_INFO()
	{
		int index = (int)GameSection.GetEventData();
		FriendCharaInfo eventData = recvList[index];
		GameSection.SetEventData(eventData);
	}

	protected void SetDirtyTable()
	{
		SetDirty(UI.GRD_LIST);
	}
}
