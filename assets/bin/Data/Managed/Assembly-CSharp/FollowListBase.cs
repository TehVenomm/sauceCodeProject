using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FollowListBase : UserListBase<FriendCharaInfo>
{
	protected enum UI
	{
		SPR_TITLE_FOLLOW_LIST,
		SPR_TITLE_FOLLOWER_LIST,
		SPR_TITLE_MESSAGE,
		SPR_TITLE_BLACKLIST,
		OBJ_FOLLOW_NUMBER_ROOT,
		LBL_FOLLOW_NUMBER_NOW,
		LBL_FOLLOW_NUMBER_MAX,
		OBJ_DISABLE_USER_MASK,
		LBL_NAME,
		GRD_LIST,
		TEX_MODEL,
		STR_NON_LIST,
		GRD_FOLLOW_ARROW,
		OBJ_FOLLOW,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
		SPR_SAME_CLAN_ICON,
		OBJ_COMMENT,
		LBL_COMMENT,
		LBL_LAST_LOGIN,
		LBL_LAST_LOGIN_TIME,
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
		STR_TITLE,
		STR_TITLE_REFLECT,
		OBJ_DEGREE_FRAME_ROOT,
		SPR_ICON_FIRST_MET,
		OBJ_SWITCH_INFO,
		DEFAULT_STATUS_ROOT,
		JOIN_STATUS_ROOT,
		ONLINE_TEXT_ROOT,
		ONLINE_TEXT,
		DETAIL_TEXT,
		JOIN_BUTTON_ROOT,
		BTN_JOIN_BUTTON,
		LBL_BUTTON_TEXT,
		BTN_SORT,
		LBL_SORT
	}

	protected enum TITLE_TYPE
	{
		FOLLOW,
		FOLLOWER,
		MESSAGE,
		BLACKLIST
	}

	protected const int ITEM_COUNT_PER_PAGE = 10;

	public static readonly Color TEXT_BASE_COLOR_DEFAULT = new Color(1f, 1f, 1f, 1f);

	public static readonly Color TEXT_BASE_COLOR_ONLINE = new Color(73f / 85f, 1f, 44f / 51f, 1f);

	public static readonly Color TEXT_BASE_COLOR_GO_FIELD = new Color(1f, 227f / 255f, 227f / 255f, 1f);

	public static readonly Color TEXT_BASE_COLOR_GO_QUEST = new Color(1f, 227f / 255f, 227f / 255f, 1f);

	public static readonly Color TEXT_BASE_COLOR_GO_LOUNGE = new Color(73f / 85f, 1f, 44f / 51f, 1f);

	public static readonly Color TEXT_OUTLINE_COLOR_DEFAULT = new Color(0.3529412f, 0.3529412f, 0.3529412f, 1f);

	public static readonly Color TEXT_OUTLINE_COLOR_ONLINE = new Color(2f / 51f, 43f / 85f, 5f / 51f, 1f);

	public static readonly Color TEXT_OUTLINE_COLOR_GO_FIELD = new Color(14f / 15f, 122f / 255f, 11f / 85f, 1f);

	public static readonly Color TEXT_OUTLINE_COLOR_GO_QUEST = new Color(14f / 15f, 122f / 255f, 11f / 85f, 1f);

	public static readonly Color TEXT_OUTLINE_COLOR_GO_LOUNGE = new Color(2f / 51f, 43f / 85f, 5f / 51f, 1f);

	private List<Transform> m_generatedItemList = new List<Transform>();

	protected bool m_isVisibleDefaultInfo = true;

	private Action<int> m_onJoinButtonPressedCallback;

	protected TITLE_TYPE titleType;

	protected USER_SORT_TYPE m_currentSortType;

	private UIGrid m_scrollGrid;

	protected UIGrid ScrollGrid => m_scrollGrid ?? (m_scrollGrid = GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>());

	protected virtual string GetListItemName => "FollowListBaseItem";

	public override void Initialize()
	{
		SetActive(UI.SPR_TITLE_FOLLOW_LIST, titleType == TITLE_TYPE.FOLLOW);
		SetActive(UI.SPR_TITLE_FOLLOWER_LIST, titleType == TITLE_TYPE.FOLLOWER);
		SetActive(UI.SPR_TITLE_MESSAGE, titleType == TITLE_TYPE.MESSAGE);
		SetActive(UI.SPR_TITLE_BLACKLIST, titleType == TITLE_TYPE.BLACKLIST);
		SetActive(UI.OBJ_FOLLOW_NUMBER_ROOT, is_visible: false);
		if (IsHideSwitchInfoButton())
		{
			SetActive(base._transform, UI.OBJ_SWITCH_INFO, is_visible: false);
		}
		base.Initialize();
	}

	public virtual void ListUI()
	{
		SetLabelText(UI.STR_TITLE, base.sectionData.GetText("STR_TITLE"));
		SetLabelText(UI.STR_TITLE_REFLECT, base.sectionData.GetText("STR_TITLE"));
		if (GetCurrentUserArray().IsNullOrEmpty())
		{
			SetActive(UI.STR_NON_LIST, is_visible: true);
			SetActive(UI.GRD_LIST, is_visible: false);
			SetActive(UI.OBJ_ACTIVE_ROOT, is_visible: false);
			SetActive(UI.OBJ_INACTIVE_ROOT, is_visible: true);
			SetLabelText(UI.LBL_NOW, "0");
			SetLabelText(UI.LBL_MAX, "0");
		}
		else
		{
			SetLabelText(UI.LBL_SORT, StringTable.Get(STRING_CATEGORY.USER_SORT, (uint)m_currentSortType));
			SetPageNumText(UI.LBL_NOW, nowPage + 1);
			SetPageNumText(UI.LBL_MAX, pageNumMax);
			SetActive(UI.STR_NON_LIST, is_visible: false);
			SetActive(UI.GRD_LIST, is_visible: true);
			SetActive(UI.OBJ_ACTIVE_ROOT, pageNumMax != 1);
			SetActive(UI.OBJ_INACTIVE_ROOT, pageNumMax == 1);
			UpdateDynamicList();
		}
	}

	protected virtual void UpdateDynamicList()
	{
		FriendCharaInfo[] info = GetCurrentUserArray();
		int item_num = (!info.IsNullOrEmpty()) ? info.Length : 0;
		if (GameDefine.ACTIVE_DEGREE)
		{
			ScrollGrid.cellHeight = GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
		}
		CleanItemList();
		SetDynamicList(UI.GRD_LIST, GetListItemName, item_num, reset: false, null, null, delegate(int i, Transform t, bool is_recycle)
		{
			SetListItem(i, t, is_recycle, info[i]);
		});
	}

	protected virtual void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
	{
		if (data != null)
		{
			SetFollowStatus(t, data.userId, data.following, data.follower, data.userClanData.cId);
			SetCharaInfo(data, i, t, is_recycle, data.userId == 0);
			if (LoungeMatchingManager.IsValidInLounge())
			{
				SetActive(t, UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(data.userId));
			}
		}
	}

	protected virtual void SetCharaInfo(FriendCharaInfo data, int i, Transform t, bool is_recycle, bool isGM)
	{
		if (isGM)
		{
			SetEvent(t, "DIRECT_VIEW_MESSAGE", i);
			SetRenderNPCModel(t, UI.TEX_MODEL, 0, new Vector3(0f, -1.49f, 1.87f), new Vector3(0f, 154f, 0f), 10f);
		}
		else
		{
			SetEvent(t, "FOLLOW_INFO", i);
			ForceSetRenderPlayerModel(t, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(data, need_weapon: false, need_helm: true, need_leg: false, is_priority_visual_equip: true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), is_priority_visual_equip: true);
		}
		CharaInfo.ClanInfo clanInfo = data.clanInfo;
		if (clanInfo == null)
		{
			clanInfo = new CharaInfo.ClanInfo();
			clanInfo.clanId = -1;
			clanInfo.tag = string.Empty;
		}
		bool isSameTeam = clanInfo.clanId > -1 && MonoBehaviourSingleton<GuildManager>.I.guildData != null && clanInfo.clanId == MonoBehaviourSingleton<GuildManager>.I.guildData.clanId;
		SetSupportEncoding(t, UI.LBL_NAME, isEnable: true);
		SetLabelText(t, UI.LBL_NAME, Utility.GetNameWithColoredClanTag(clanInfo.tag, data.name, data.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
		SetLabelText(t, UI.LBL_LEVEL, data.level.ToString());
		SetLabelText(t, UI.LBL_COMMENT, data.comment);
		SetLabelText(t, UI.LBL_LAST_LOGIN, base.sectionData.GetText("LAST_LOGIN"));
		SetLabelText(t, UI.LBL_LAST_LOGIN_TIME, data.lastLogin);
		EquipSetCalculator otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4);
		otherEquipSetCalculator.SetEquipSet(data.equipSet);
		SimpleStatus finalStatus = otherEquipSetCalculator.GetFinalStatus(0, data.hp, data.atk, data.def);
		SetLabelText(t, UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
		SetLabelText(t, UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
		SetLabelText(t, UI.LBL_HP, finalStatus.hp.ToString());
		FindCtrl(t, UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>().Initialize(data.selectedDegrees, isButton: false, delegate
		{
			ScrollGrid.Reposition();
		});
		SetJoinInfo(t, i, data.joinStatus, data.lastLogin);
		if (!m_generatedItemList.Contains(t))
		{
			m_generatedItemList.Add(t);
		}
	}

	protected void SetFollowStatus(Transform t, int user_id, bool following, bool follower, string clanId)
	{
		bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
		bool is_visible = false;
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
		{
			is_visible = (clanId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId);
		}
		bool flag2 = !flag && (following | follower);
		SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
		SetActive(t, UI.OBJ_FOLLOW, flag2);
		SetActive(t, UI.SPR_FOLLOW, flag2 && following);
		SetActive(t, UI.SPR_FOLLOWER, flag2 && follower);
		SetActive(t, UI.SPR_SAME_CLAN_ICON, is_visible);
		UIGrid component = GetComponent<UIGrid>(t, UI.GRD_FOLLOW_ARROW);
		if (component != null)
		{
			component.Reposition();
		}
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
					isInviteToClan = chara_info.isInviteToClan,
					aId = chara_info.aId,
					hId = chara_info.hId,
					rId = chara_info.rId,
					lId = chara_info.lId,
					showHelm = chara_info.showHelm,
					equipSet = chara_info.equipSet,
					following = chara_info.following,
					follower = chara_info.follower,
					selectedDegrees = chara_info.selectedDegrees,
					clanInfo = chara_info.clanInfo,
					accessory = chara_info.accessory,
					userClanData = chara_info.userClanData
				};
				new_list.Add(item);
			}
		});
		return new_list;
	}

	public virtual void OnQuery_FOLLOW_INFO()
	{
		int num = (int)GameSection.GetEventData();
		FriendCharaInfo[] currentUserArray = GetCurrentUserArray();
		if (num >= 0 && !currentUserArray.IsNullOrEmpty() && currentUserArray.Length > num)
		{
			FriendCharaInfo eventData = currentUserArray[num];
			MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = num + 4;
			GameSection.SetEventData(eventData);
		}
	}

	protected void SetDirtyTable()
	{
		SetDirty(UI.GRD_LIST);
	}

	protected virtual FriendCharaInfo[] GetCurrentUserArray()
	{
		if (recvList == null)
		{
			return null;
		}
		return recvList.ToArray();
	}

	protected virtual List<FriendCharaInfo> GetCurrentUserList()
	{
		return recvList;
	}

	protected virtual bool IsHideSwitchInfoButton()
	{
		return true;
	}

	protected void SwitchUserStatusInfo(bool _isVisibleDefaultInfo)
	{
		if (m_isVisibleDefaultInfo == _isVisibleDefaultInfo)
		{
			return;
		}
		m_isVisibleDefaultInfo = _isVisibleDefaultInfo;
		int i = 0;
		for (int count = m_generatedItemList.Count; i < count; i++)
		{
			if (!(m_generatedItemList[i] == null))
			{
				SwitchInfoRootObject(m_generatedItemList[i], m_isVisibleDefaultInfo);
			}
		}
	}

	protected virtual void SetJoinInfo(Transform t, int _itemIndex, FriendCharaInfo.JoinInfo _joinStatus, string _lastLoginText)
	{
		if (!(t == null))
		{
			SwitchInfoRootObject(t, m_isVisibleDefaultInfo);
			if (_joinStatus != null)
			{
				SetJoinTextInfo(t, _joinStatus);
				SetJoinButtonSettings(t, (ONLINE_STATUS)_joinStatus.joinType, _itemIndex);
				SetEvent(t, UI.BTN_JOIN_BUTTON, "JOIN_FRIEND", _itemIndex);
			}
		}
	}

	private void SetJoinTextInfo(Transform _target, FriendCharaInfo.JoinInfo _joinStatus)
	{
		UILabel component = FindCtrl(_target, UI.ONLINE_TEXT).GetComponent<UILabel>();
		UILabel component2 = FindCtrl(_target, UI.DETAIL_TEXT).GetComponent<UILabel>();
		if (component != null)
		{
			component.text = StringTable.Get(STRING_CATEGORY.FRIEND_JOIN, (uint)_joinStatus.joinType);
		}
		if (component2 != null)
		{
			component2.text = "";
		}
		switch (_joinStatus.joinType)
		{
		case 4:
			if (component != null)
			{
				component.color = TEXT_BASE_COLOR_GO_FIELD;
				component.effectColor = TEXT_OUTLINE_COLOR_GO_FIELD;
			}
			if (component2 != null)
			{
				component2.text = GetMapFieldNameText(_joinStatus.targetParam);
			}
			break;
		case 2:
			if (component != null)
			{
				component.color = TEXT_BASE_COLOR_GO_LOUNGE;
				component.effectColor = TEXT_OUTLINE_COLOR_GO_LOUNGE;
			}
			break;
		case 3:
			if (component != null)
			{
				component.color = TEXT_BASE_COLOR_GO_QUEST;
				component.effectColor = TEXT_OUTLINE_COLOR_GO_QUEST;
			}
			if (component2 != null)
			{
				component2.text = GetQuestName(_joinStatus.targetParam);
			}
			break;
		case 1:
			if (component != null)
			{
				component.color = TEXT_BASE_COLOR_ONLINE;
				component.effectColor = TEXT_OUTLINE_COLOR_ONLINE;
			}
			break;
		default:
			if (component != null)
			{
				component.color = TEXT_BASE_COLOR_DEFAULT;
				component.effectColor = TEXT_OUTLINE_COLOR_DEFAULT;
			}
			break;
		}
	}

	private string GetMapFieldNameText(int _fieldMapId)
	{
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)_fieldMapId);
		if (fieldMapData == null)
		{
			return string.Empty;
		}
		RegionTable.Data data = Singleton<RegionTable>.I.GetData(fieldMapData.regionId);
		if (data == null)
		{
			return fieldMapData.mapName;
		}
		return $"{data.regionName}-{fieldMapData.mapName}";
	}

	private string GetQuestName(int _questId)
	{
		if (!Singleton<QuestTable>.IsValid())
		{
			return string.Empty;
		}
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)_questId);
		if (questData != null)
		{
			return questData.questText;
		}
		return string.Empty;
	}

	protected void SwitchInfoRootObject(Transform t, bool _activeFlag)
	{
		if (!(t == null))
		{
			SetActive(t, UI.DEFAULT_STATUS_ROOT, _activeFlag);
			SetActive(t, UI.JOIN_STATUS_ROOT, !_activeFlag);
		}
	}

	protected void SetJoinButtonSettings(Transform _t, ONLINE_STATUS _status, int _itemIndex)
	{
		if (_t == null)
		{
			return;
		}
		UIButton component = FindCtrl(_t, UI.BTN_JOIN_BUTTON).GetComponent<UIButton>();
		if (!(component == null))
		{
			if ((uint)(_status - 2) <= 2u)
			{
				component.gameObject.SetActive(value: true);
			}
			else
			{
				component.gameObject.SetActive(value: false);
			}
			UIGameSceneEventSender componentInChildren = component.GetComponentInChildren<UIGameSceneEventSender>(includeInactive: true);
			if (componentInChildren != null)
			{
				componentInChildren.eventName = "JOIN_FRIEND";
				componentInChildren.eventData = _itemIndex;
			}
		}
	}

	protected void OnQuery_SWITCH_INFO()
	{
		SwitchUserStatusInfo(!m_isVisibleDefaultInfo);
	}

	protected virtual void OnQuery_JOIN_FRIEND()
	{
		int num = (int)GameSection.GetEventData();
		FriendCharaInfo[] currentUserArray = GetCurrentUserArray();
		if (currentUserArray == null || currentUserArray.Length <= num || num < 0)
		{
			return;
		}
		FriendCharaInfo.JoinInfo joinStatus = currentUserArray[num].joinStatus;
		if (joinStatus == null)
		{
			return;
		}
		GameSection.StayEvent();
		switch (joinStatus.joinType)
		{
		case 0:
			if (MonoBehaviourSingleton<PartyManager>.IsValid())
			{
				MonoBehaviourSingleton<PartyManager>.I.SendApply(joinStatus.conditionParam, delegate
				{
					GameSection.ResumeEvent(is_resume: true);
				}, joinStatus.targetParam);
			}
			break;
		case 1:
			JoinLounge(joinStatus);
			break;
		case 2:
		{
			int toUserId = int.Parse(joinStatus.conditionParam);
			JoinField(joinStatus.targetParam, toUserId, delegate(bool is_matching, bool is_connect, bool is_regist)
			{
				if (is_matching)
				{
					if (!is_connect)
					{
						GameSection.ResumeEvent(is_resume: true);
						AppMain i = MonoBehaviourSingleton<AppMain>.I;
						i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
						{
							DispatchEvent("CLOSE");
						});
					}
					else
					{
						GameSection.ResumeEvent(is_regist);
						if (is_regist)
						{
							MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
						}
					}
				}
			});
			break;
		}
		}
	}

	protected void CleanItemList()
	{
		int num = m_generatedItemList.Count - 1;
		while (0 <= num)
		{
			if (m_generatedItemList[num] == null)
			{
				m_generatedItemList.RemoveAt(num);
			}
			num--;
		}
	}

	private bool IsEnableJoinField(int _fieldMapId, out FieldMapTable.FieldMapTableData _fieldMapTableData)
	{
		_fieldMapTableData = null;
		if (!MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			return false;
		}
		FieldManager i = MonoBehaviourSingleton<FieldManager>.I;
		if (_fieldMapId == i.currentMapID)
		{
			return false;
		}
		_fieldMapTableData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)_fieldMapId);
		if (_fieldMapTableData == null || _fieldMapTableData.jumpPortalID == 0)
		{
			Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
			return false;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(_fieldMapTableData.jumpPortalID, check_dst_quest: false))
		{
			return false;
		}
		if (!i.CanJumpToMap(_fieldMapTableData))
		{
			GameSceneEvent.PushStay();
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.GetErrorCodeText(30301u)), delegate
			{
				GameSceneEvent.PopStay();
				GameSection.ResumeEvent(is_resume: false);
			});
			return false;
		}
		return true;
	}

	protected void JoinField(int fieldMapId, int _toUserId, Action<bool, bool, bool> _callback)
	{
		FieldMapTable.FieldMapTableData _fieldMapTableData = null;
		if (!IsEnableJoinField(fieldMapId, out _fieldMapTableData))
		{
			_callback?.Invoke(arg1: false, arg2: false, arg3: false);
		}
		else
		{
			CoopApp.EnterField(_fieldMapTableData.jumpPortalID, 0u, _toUserId, _callback);
		}
	}

	protected void JoinLounge(FriendCharaInfo.JoinInfo _joinData)
	{
		if (!MonoBehaviourSingleton<LoungeMatchingManager>.IsValid())
		{
			GameSection.ResumeEvent(is_resume: true);
		}
		else if (_joinData == null)
		{
			GameSection.ResumeEvent(is_resume: true);
		}
		else if (!LoungeMatchingManager.IsValidInLounge())
		{
			GameSection.ResumeEvent(is_resume: true);
			GameSection.SetEventData(_joinData.conditionParam);
			OnQuery_FORCE_MOVETO_LOUNGE();
		}
		else if (MonoBehaviourSingleton<LoungeMatchingManager>.I.GetLoungeNumber() == _joinData.conditionParam)
		{
			GameSection.ResumeEvent(is_resume: true);
		}
		else
		{
			EventData[] autoEvents = new EventData[2]
			{
				new EventData("FORCE_MOVETO_HOME", null),
				new EventData("FORCE_MOVETO_LOUNGE", _joinData.conditionParam)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			GameSection.ResumeEvent(is_resume: true);
		}
	}

	protected void Sort<T>(List<T> _userList) where T : FriendCharaInfo
	{
		if (_userList != null && _userList.Count != 0)
		{
			switch (m_currentSortType)
			{
			case USER_SORT_TYPE.NAME:
				_userList.Sort(UserCompareByName);
				break;
			case USER_SORT_TYPE.LEVEL:
				_userList.Sort(UserCompareByLevel);
				break;
			case USER_SORT_TYPE.LOGIN:
				_userList.Sort(UserCompareByLoginTime);
				break;
			case USER_SORT_TYPE.PLAY_COUNT:
				_userList.Sort(UserCompareByPlayCount);
				break;
			case USER_SORT_TYPE.REGISTER:
				_userList.Sort(UserCompareByResistered);
				break;
			}
		}
	}

	protected int UserCompareByName<T>(T a, T b) where T : CharaInfo
	{
		return string.Compare(a.name, b.name);
	}

	protected int UserCompareByLevel<T>(T a, T b) where T : CharaInfo
	{
		return (int)b.level - (int)a.level;
	}

	protected int UserCompareByLoginTime<T>(T a, T b) where T : CharaInfo
	{
		return b.lastLoginTm - a.lastLoginTm;
	}

	protected int UserCompareByPlayCount<T>(T a, T b) where T : FriendCharaInfo
	{
		return b.playedCount - a.playedCount;
	}

	protected int UserCompareByResistered<T>(T a, T b) where T : FriendCharaInfo
	{
		if (a.follower_id > 0 && b.follower_id > 0)
		{
			return b.follower_id - a.follower_id;
		}
		return b.following_id - a.following_id;
	}

	protected virtual void OnQuery_SORT()
	{
		UpdateSortType();
		Sort(GetCurrentUserList());
		RefreshUI();
	}

	protected virtual void UpdateSortType()
	{
		m_currentSortType++;
		if (m_currentSortType == USER_SORT_TYPE.PLAY_COUNT)
		{
			m_currentSortType++;
		}
		if (m_currentSortType >= USER_SORT_TYPE.MAX)
		{
			m_currentSortType = USER_SORT_TYPE.NAME;
		}
	}
}
