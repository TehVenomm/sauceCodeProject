using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanDetail : GameSection
{
	private enum UI
	{
		LBL_CLAN_NAME,
		LBL_CLAN_LEVEL,
		LBL_APLLY_TYPE,
		LBL_MODE,
		LBL_COMMENT,
		OBJ_STAMP,
		BTN_STAMP,
		LBL_MEMBER_NUMBER_NOW,
		LBL_MEMBER_NUMBER_MAX,
		GRD_LIST,
		TEX_MODEL,
		LBL_LEVEL,
		LBL_NAME,
		OBJ_FOLLOW,
		GRD_FOLLOW_ARROW,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
		SPR_SAME_CLAN_ICON,
		SPR_STATUS,
		JOIN_STATUS_ROOT,
		ONLINE_TEXT_ROOT,
		ONLINE_TEXT,
		DETAIL_TEXT,
		JOIN_BUTTON_ROOT,
		BTN_JOIN_BUTTON,
		LBL_BUTTON_TEXT,
		LBL_PLAYING_FIELD,
		LBL_AREA_NAME,
		LBL_PLAYING_QUEST,
		LBL_PLAYING_READY,
		LBL_QUEST_NAME,
		LBL_IN_LOUNGE,
		LBL_PLAYING_ARENA,
		BTN_MEMBER_SETTING,
		LBL_NON_LIST,
		STR_LIST_NUM,
		OBJ_DEGREE_FRAME_ROOT,
		BTN_LEAVE,
		BTN_APPLY,
		BTN_APPLY_CANCEL,
		LBL_NOW,
		LBL_MAX,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		BTN_INVITE,
		OBJ_SYMBOL
	}

	private const int ITEM_COUNT_PER_PAGE = 10;

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

	private string sendClanId;

	private List<FriendCharaInfo> members;

	private ClanData clanData;

	private bool isRequested;

	private bool isInvited;

	private SymbolMarkCtrl symbolMark;

	private int nowPage;

	private int pageNumMax;

	public override void Initialize()
	{
		sendClanId = (string)GameSection.GetEventData();
		StartCoroutine(Reload(delegate
		{
			base.Initialize();
		}));
	}

	private IEnumerator Reload(Action cb = null)
	{
		bool is_recv = false;
		SendRequest(delegate
		{
			is_recv = true;
		});
		while (!is_recv)
		{
			yield return null;
		}
		SetDirtyTable();
		RefreshUI();
		cb?.Invoke();
	}

	private void SendRequest(Action onFinish)
	{
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestDetail(sendClanId, delegate(ClanDetailModel.Param result)
		{
			members = result.memberList;
			clanData = result.clan;
			isRequested = result.isRequested;
			isInvited = result.isInvited;
			nowPage = 0;
			pageNumMax = Mathf.CeilToInt((float)members.Count / 10f);
			onFinish();
		});
	}

	public override void UpdateUI()
	{
		UpdateHeaderUI();
		UpdateListUI();
		UpdateFooterUI();
		SetActive(UI.LBL_NON_LIST, members.Count <= 0);
	}

	public void UpdateHeaderUI()
	{
		SetLabelText(UI.LBL_CLAN_NAME, clanData.name);
		SetLabelText(UI.LBL_CLAN_LEVEL, clanData.lv.ToString());
		SetLabelText(UI.LBL_APLLY_TYPE, StringTable.Get(STRING_CATEGORY.JOIN_TYPE, (uint)clanData.jt));
		SetLabelText(UI.LBL_MODE, StringTable.Get(STRING_CATEGORY.CLAN_LABEL, (uint)clanData.lbl));
		SetLabelText(UI.LBL_COMMENT, clanData.cmt);
		SetLabelText(UI.LBL_MEMBER_NUMBER_NOW, members.Count.ToString());
		SetLabelText(UI.LBL_MEMBER_NUMBER_MAX, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_MAX_MEMBER_NUM.ToString());
		StartCoroutine(CreateSymbolMark());
	}

	private IEnumerator CreateSymbolMark()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject symbolMarkLoadObj = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
		yield return loadingQueue.Wait();
		Transform transform = ResourceUtility.Realizes(symbolMarkLoadObj.loadedObject as GameObject, 5);
		transform.parent = GetCtrl(UI.OBJ_SYMBOL);
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
		symbolMark = transform.GetComponent<SymbolMarkCtrl>();
		symbolMark.Initilize();
		symbolMark.LoadSymbol(clanData.sym);
	}

	private void UpdateListUI()
	{
		SetPageNumText(UI.LBL_NOW, nowPage + 1);
		SetPageNumText(UI.LBL_MAX, pageNumMax);
		SetActive(UI.OBJ_ACTIVE_ROOT, pageNumMax != 1);
		SetActive(UI.OBJ_INACTIVE_ROOT, pageNumMax == 1);
		UpdateDynamicList();
	}

	private void UpdateFooterUI()
	{
		bool is_visible = clanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
		MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsNotRegistered();
		SetActive(UI.BTN_LEAVE, is_visible);
		SetActive(UI.BTN_INVITE, is_visible: false);
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered() || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene")
		{
			SetActive(UI.BTN_APPLY, is_visible: false);
			SetActive(UI.BTN_APPLY_CANCEL, is_visible: false);
			return;
		}
		if (isRequested)
		{
			SetActive(UI.BTN_APPLY, is_visible: false);
			SetActive(UI.BTN_APPLY_CANCEL, is_visible: true);
			return;
		}
		Enum ctrl_enum = UI.BTN_APPLY;
		Enum ctrl_enum2 = UI.BTN_INVITE;
		if (isInvited)
		{
			ctrl_enum = UI.BTN_INVITE;
			ctrl_enum2 = UI.BTN_APPLY;
		}
		if (members.Count < MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_MAX_MEMBER_NUM)
		{
			SetActive(ctrl_enum, is_visible: true);
			SetActive(ctrl_enum2, is_visible: false);
		}
		else
		{
			SetActive(UI.BTN_APPLY, is_visible: false);
		}
		SetActive(UI.BTN_APPLY_CANCEL, is_visible: false);
	}

	protected virtual void UpdateDynamicList()
	{
		FriendCharaInfo[] currentUserArray = GetCurrentUserArray();
		int pageItemLength = GetPageItemLength(nowPage);
		int num = nowPage * 10;
		FriendCharaInfo[] info = new FriendCharaInfo[pageItemLength];
		for (int j = 0; j < pageItemLength; j++)
		{
			info[j] = currentUserArray[num + j];
		}
		SetDynamicList(UI.GRD_LIST, "ClanMemberListItem", pageItemLength, reset: false, null, null, delegate(int i, Transform t, bool is_recycle)
		{
			SetupListItem(info[i], i, t, is_recycle);
		});
	}

	protected virtual FriendCharaInfo[] GetCurrentUserArray()
	{
		if (members == null)
		{
			return null;
		}
		return members.ToArray();
	}

	private int GetPageItemLength(int currentPage)
	{
		if (currentPage + 1 < pageNumMax || members.Count % 10 <= 0)
		{
			return 10;
		}
		return members.Count % 10;
	}

	private void SetupListItem(FriendCharaInfo data, int i, Transform t, bool is_recycle)
	{
		SetEvent(t, "DETAIL", i);
		SetMemberInfo(data, i, t);
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			SetButtonColliderEnabled(t, is_enabled: false);
		}
	}

	private void SetMemberInfo(FriendCharaInfo data, int i, Transform t)
	{
		MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4).SetEquipSet(data.equipSet);
		SetRenderPlayerModel(t, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(data, need_weapon: false, need_helm: true, need_leg: false, is_priority_visual_equip: true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), is_priority_visual_equip: true);
		SetLabelText(t, UI.LBL_NAME, data.name);
		SetLabelText(t, UI.LBL_LEVEL, data.level.ToString());
		SetFollowStatus(t, data.userId, data.following, data.follower);
		SetJoinInfo(t, i, data.userId, data.joinStatus, "");
		SetStatusSprite(t, data.userClanData);
		SetBtnMemberSetting(t, i, data);
		FindCtrl(t, UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>().Initialize(data.selectedDegrees, isButton: false, delegate
		{
			GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>().Reposition();
		});
	}

	private void SetStatusSprite(Transform root, UserClanData userClan)
	{
		if (userClan.IsLeader())
		{
			SetActive(root, UI.SPR_STATUS, is_visible: true);
			SetSprite(root, UI.SPR_STATUS, "Clan_HeadmasterIcon");
		}
		else if (userClan.IsSubLeader())
		{
			SetActive(root, UI.SPR_STATUS, is_visible: true);
			SetSprite(root, UI.SPR_STATUS, "Clan_DeputyHeadmasterIcon");
		}
		else
		{
			SetActive(root, UI.SPR_STATUS, is_visible: false);
		}
	}

	private void SetBtnMemberSetting(Transform root, int index, FriendCharaInfo userInfo)
	{
		if (!(userInfo.userClanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId) || !MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader())
		{
			SetActive(root, UI.BTN_MEMBER_SETTING, is_visible: false);
			return;
		}
		SetActive(root, UI.BTN_MEMBER_SETTING, !userInfo.userClanData.IsLeader());
		SetEvent(root, UI.BTN_MEMBER_SETTING, "MEMBER_SETTING", index);
	}

	private void SetFollowStatus(Transform t, int user_id, bool following, bool follower)
	{
		bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
		bool is_visible = false;
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
		{
			is_visible = (clanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId);
		}
		bool flag2 = !flag && (following | follower);
		SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
		SetActive(t, UI.OBJ_FOLLOW, flag2);
		SetActive(t, UI.SPR_FOLLOW, flag2 && following);
		SetActive(t, UI.SPR_FOLLOWER, flag2 && follower);
		SetActive(t, UI.SPR_SAME_CLAN_ICON, is_visible);
		SetActive(t, UI.OBJ_FOLLOW, following | follower);
		SetActive(t, UI.SPR_FOLLOW, following);
		SetActive(t, UI.SPR_FOLLOWER, follower);
		UIGrid component = GetComponent<UIGrid>(t, UI.GRD_FOLLOW_ARROW);
		if (component != null)
		{
			component.Reposition();
		}
	}

	protected virtual void SetJoinInfo(Transform t, int _itemIndex, int userId, FriendCharaInfo.JoinInfo _joinStatus, string _lastLoginText)
	{
		if (!(t == null))
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == userId)
			{
				SetActive(t, UI.JOIN_STATUS_ROOT, is_visible: false);
				return;
			}
			if (_joinStatus == null)
			{
				SetActive(t, UI.JOIN_STATUS_ROOT, is_visible: false);
				return;
			}
			SetActive(t, UI.JOIN_STATUS_ROOT, is_visible: true);
			SetJoinTextInfo(t, _joinStatus);
			SetJoinButtonSettings(t, (ONLINE_STATUS)_joinStatus.joinType, _itemIndex);
			SetEvent(t, UI.BTN_JOIN_BUTTON, "JOIN_FRIEND", _itemIndex);
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

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (NOTIFY_FLAG)0L)
		{
			SetDirtyTable();
		}
		base.OnNotify(flags);
	}

	protected void SetJoinButtonSettings(Transform _t, ONLINE_STATUS _status, int _itemIndex)
	{
		if (_t == null)
		{
			return;
		}
		UIButton component = FindCtrl(_t, UI.BTN_JOIN_BUTTON).GetComponent<UIButton>();
		if (component == null)
		{
			return;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.IsCurrentSceneMejorOutGameScene())
		{
			component.gameObject.SetActive(value: false);
			return;
		}
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

	protected void SetDirtyTable()
	{
		SetDirty(UI.GRD_LIST);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_LIST;
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

	private void OnQuery_APPLY()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, delegate(UserClanData userClanData)
		{
			if (userClanData.IsNotRegistered())
			{
				GameSection.ResumeEvent(is_resume: true);
				GameSection.SetEventData(new string[1]
				{
					clanData.name
				});
			}
			else
			{
				GameSection.ResumeEvent(is_resume: false);
				MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.GetErrorCodeText(42007u)), delegate
				{
					RefreshUI();
				});
			}
		});
	}

	private void OnQuery_ClanApplyConfirmDialog_YES()
	{
		GameSection.StayEvent();
		ClanApplyModel.RequestSendForm requestSendForm = new ClanApplyModel.RequestSendForm();
		requestSendForm.cId = clanData.cId;
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestApply(requestSendForm, delegate(bool isSuccess)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsNotRegistered())
			{
				isRequested = true;
				RefreshUI();
				GameSection.ChangeStayEvent("APPLIED", new string[1]
				{
					clanData.name
				});
				GameSection.ResumeEvent(isSuccess);
			}
			else
			{
				GameSection.ChangeStayEvent("REGISTERED", new string[1]
				{
					clanData.name
				});
				GameSection.ResumeEvent(isSuccess);
			}
		});
	}

	private void OnQuery_APPLY_CANCEL()
	{
		GameSection.SetEventData(new string[1]
		{
			clanData.name
		});
	}

	private void OnQuery_ClanApplyCancelConfirmDialog_YES()
	{
		GameSection.SetEventData(new string[1]
		{
			clanData.name
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestApplyCancel(clanData.cId, delegate(bool isSuccess)
		{
			if (isSuccess)
			{
				isRequested = false;
				RefreshUI();
			}
			GameSection.ResumeEvent(isSuccess);
		});
	}

	private void OnQuery_LEAVE()
	{
		string text = "";
		if (members.Count > 1)
		{
			text = StringTable.Get(STRING_CATEGORY.CLAN_LEAVE, 0u);
			text = string.Format(text, clanData.name);
		}
		else
		{
			text = StringTable.Get(STRING_CATEGORY.CLAN_LEAVE, 1u);
			text = string.Format(text, clanData.name);
		}
		GameSection.SetEventData(text);
	}

	private void OnQuery_ClanLeaveConfirmDialog_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ClanMatchingManager>.I.RequestLeave(delegate(bool isSuccess)
		{
			GameSection.ResumeEvent(isSuccess);
			if (isSuccess)
			{
				MonoBehaviourSingleton<UserInfoManager>.I.LeaveClan();
				MonoBehaviourSingleton<ClanMatchingManager>.I.ClearClanData();
			}
		});
	}

	private void OnQuery_ClanLeaveCompleteDialog_OK()
	{
		string name = (!MonoBehaviourSingleton<ClanManager>.IsValid()) ? GameSection.GetGoingHomeEvent() : "MAIN_MENU_HOME";
		EventData[] autoEvents = new EventData[1]
		{
			new EventData(name, null)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private void OnQuery_DETAIL()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			GameSection.StopEvent();
			return;
		}
		int num = (int)GameSection.GetEventData();
		int index = 10 * nowPage + num;
		FriendCharaInfo friendCharaInfo = members[index];
		MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = num + 4;
		if (friendCharaInfo.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			GameSection.StopEvent();
		}
		GameSection.SetEventData(friendCharaInfo);
	}

	private void OnQuery_MEMBER_SETTING()
	{
		int num = (int)GameSection.GetEventData();
		int index = 10 * nowPage + num;
		GameSection.SetEventData(members[index]);
	}

	protected virtual void OnQuery_JOIN_FRIEND()
	{
		int num = (int)GameSection.GetEventData();
		int index = 10 * nowPage + num;
		FriendCharaInfo.JoinInfo joinStatus = members[index].joinStatus;
		if (joinStatus == null)
		{
			return;
		}
		GameSection.StayEvent();
		switch (joinStatus.joinType)
		{
		case 3:
			if (MonoBehaviourSingleton<PartyManager>.IsValid())
			{
				MonoBehaviourSingleton<PartyManager>.I.SendApply(joinStatus.conditionParam, delegate(bool isSucceed, Error error)
				{
					if (isSucceed)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("QuestAccept", "QuestAcceptRoom");
					}
					GameSection.ResumeEvent(is_resume: true);
				}, joinStatus.targetParam);
			}
			break;
		case 2:
			JoinLounge(joinStatus);
			break;
		case 4:
		{
			int toUserId = int.Parse(joinStatus.conditionParam);
			JoinField(joinStatus.targetParam, toUserId, delegate(bool is_matching, bool is_connect, bool is_regist)
			{
				if (!is_matching)
				{
					GameSection.StopEvent();
				}
				else if (!is_connect)
				{
					GameSection.StopEvent();
				}
				else
				{
					GameSection.ResumeEvent(is_regist);
					if (is_regist)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
					}
				}
			});
			break;
		}
		default:
			GameSection.ResumeEvent(is_resume: true);
			break;
		}
	}

	protected void OnQuery_PAGE_PREV()
	{
		int num = nowPage = (nowPage - 1 + pageNumMax) % pageNumMax;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GetUpdateUINotifyFlags());
	}

	protected void OnQuery_PAGE_NEXT()
	{
		int num = nowPage = (nowPage + 1) % pageNumMax;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GetUpdateUINotifyFlags());
	}

	protected void OnQuery_SCOUT_APPLY()
	{
		GameSection.SetEventData(new string[1]
		{
			clanData.name
		});
	}

	protected void OnQuery_ClanScoutApplyConfirmDialog_YES()
	{
		GameSection.StayEvent();
		int cId = int.Parse(clanData.cId);
		MonoBehaviourSingleton<ClanMatchingManager>.I.SendClanAcceptInvite(cId, delegate(bool isSuccess)
		{
			if (isSuccess)
			{
				GameSection.ChangeStayEvent("REGISTERED", new string[1]
				{
					clanData.name
				});
				GameSection.ResumeEvent(is_resume: true);
			}
			else
			{
				MonoBehaviourSingleton<ClanMatchingManager>.I.RemoveClanScoutList(clanData.cId);
				GameSection.ChangeStayEvent("ERROR");
				GameSection.ResumeEvent(is_resume: true);
			}
		});
	}

	private void OnCloseDialog_ClanMemberStatusChangeConfirmDialog()
	{
		StartCoroutine(Reload(delegate
		{
		}));
	}

	private void OnCloseDialog_ClanKickConfirmDialog()
	{
		StartCoroutine(Reload(delegate
		{
		}));
	}
}
