using Network;
using System;
using System.Collections;

public class QuestAcceptSeriesArenaRoomChangeEquipSet : QuestOffLineChangeEquipSet
{
	protected new enum UI
	{
		LBL_NAME,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		SPR_COMMENT,
		LBL_COMMENT,
		OBJ_LAST_LOGIN,
		LBL_LAST_LOGIN,
		LBL_LAST_LOGIN_TIME,
		LBL_LEVEL,
		OBJ_LEVEL_ROOT,
		LBL_USER_ID,
		OBJ_USER_ID_ROOT,
		TEX_MODEL,
		BTN_FOLLOW,
		BTN_UNFOLLOW,
		OBJ_BLACKLIST_ROOT,
		BTN_BLACKLIST_IN,
		BTN_BLACKLIST_OUT,
		OBJ_ICON_WEAPON_1,
		OBJ_ICON_WEAPON_2,
		OBJ_ICON_WEAPON_3,
		OBJ_ICON_ARMOR,
		OBJ_ICON_HELM,
		OBJ_ICON_ARM,
		OBJ_ICON_LEG,
		BTN_ICON_WEAPON_1,
		BTN_ICON_WEAPON_2,
		BTN_ICON_WEAPON_3,
		BTN_ICON_ARMOR,
		BTN_ICON_HELM,
		BTN_ICON_ARM,
		BTN_ICON_LEG,
		OBJ_EQUIP_ROOT,
		OBJ_EQUIP_SET_ROOT,
		OBJ_FRIEND_INFO_ROOT,
		OBJ_CHANGE_EQUIP_INFO_ROOT,
		LBL_MAX,
		LBL_NOW,
		OBJ_FOLLOW_ARROW_ROOT,
		SPR_FOLLOW_ARROW,
		SPR_FOLLOWER_ARROW,
		SPR_BLACKLIST_ICON,
		LBL_LEVEL_WEAPON_1,
		LBL_LEVEL_WEAPON_2,
		LBL_LEVEL_WEAPON_3,
		LBL_LEVEL_ARMOR,
		LBL_LEVEL_HELM,
		LBL_LEVEL_ARM,
		LBL_LEVEL_LEG,
		LBL_CHANGE_MODE,
		LBL_SET_NAME,
		OBJ_DEGREE_PLATE_ROOT,
		OBJ_SKILL_BUTTON_ROOT,
		OBJ_EQUIP_ROT_ROOT,
		BTN_EQUIP_SET_COPY,
		BTN_EQUIP_SET_PASTE,
		BTN_EQUIP_SET_DELETE,
		BTN_AUTO_EQUIP,
		OBJ_BACK,
		BTN_MAGI_REMOVE
	}

	protected int order;

	public override void Initialize()
	{
		order = (int)GameSection.GetEventData();
		base.Initialize();
		SetActive(transRoot, UI.OBJ_BACK, is_visible: true);
	}

	protected override void GetUserRecordStatus()
	{
		selfCharaEquipSetNo = MonoBehaviourSingleton<StatusManager>.I.selectUniqueEquipSetNo;
		record = InitializePlayerRecord();
	}

	private new void OnEnable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
	}

	private new void OnDisable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
		nowSectionName = string.Empty;
	}

	private void OnDrag(InputManager.TouchInfo touch_info)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (!(loader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && CanRotateSection())
		{
			loader.get_transform().Rotate(GameDefine.GetCharaRotateVector(touch_info));
		}
	}

	private bool CanRotateSection()
	{
		string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
		if (currentSectionName != null && currentSectionName == "QuestAcceptSeriesArenaRoomChangeEquipSet")
		{
			return true;
		}
		return false;
	}

	protected override void UpdateCopyModeButton()
	{
	}

	private bool IsReadyCheck()
	{
		if (localEquipSet.item[0] == null || localEquipSet.item[3] == null || localEquipSet.item[0].uniqueID == 0 || localEquipSet.item[3].uniqueID == 0)
		{
			return false;
		}
		return true;
	}

	protected new InGameRecorder.PlayerRecord InitializePlayerRecord()
	{
		UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		InGameRecorder.PlayerRecord playerRecord = new InGameRecorder.PlayerRecord();
		playerRecord.id = 0;
		playerRecord.isNPC = false;
		playerRecord.isSelf = true;
		MonoBehaviourSingleton<StatusManager>.I.CreateLocalUniqueEquipSetData();
		playerRecord.animID = 90;
		playerRecord.playerLoadInfo = new PlayerLoadInfo();
		localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSet(selfCharaEquipSetNo);
		PlayerLoad(playerRecord);
		playerRecord.charaInfo = new CharaInfo();
		playerRecord.charaInfo.userId = userInfo.id;
		playerRecord.charaInfo.name = userInfo.name;
		playerRecord.charaInfo.comment = userInfo.comment;
		playerRecord.charaInfo.code = userInfo.code;
		playerRecord.charaInfo.level = userStatus.level;
		playerRecord.charaInfo.atk = userStatus.atk;
		playerRecord.charaInfo.def = userStatus.def;
		playerRecord.charaInfo.hp = userStatus.hp;
		playerRecord.charaInfo.faceId = userStatus.faceId;
		playerRecord.charaInfo.sex = userStatus.sex;
		playerRecord.charaInfo.equipSet = null;
		equipSetMax = MonoBehaviourSingleton<StatusManager>.I.UniqueEquipSetNum();
		return playerRecord;
	}

	protected override void PlayerLoad(InGameRecorder.PlayerRecord record)
	{
		PlayerLoadInfo playerLoadInfo = record.playerLoadInfo;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		playerLoadInfo.SetupLoadInfo(localEquipSet, 0uL, 0uL, 0uL, 0uL, 0uL, localEquipSet.showHelm == 1);
		if (playerLoadInfo.bodyModelID <= 0)
		{
			playerLoadInfo.SetEquipBody(userStatus.sex, (uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.playerBodyEquipItemID);
		}
		if (localEquipSet.item[0] == null)
		{
			record.animID = 98;
		}
		else
		{
			record.animID = 90;
		}
	}

	protected override PlayerLoadInfo GetFromUserStatus()
	{
		return PlayerLoadInfo.FromUserUniqueStatus(need_weapon: true, isVisualMode, selfCharaEquipSetNo);
	}

	protected override void OnQuery_SECTION_BACK()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquipSet(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_MAGI_REMOVE()
	{
		GameSection.SetEventData(new object[1]
		{
			selfCharaEquipSetNo + 1
		});
	}

	private void OnQuery_QuestAcceptStatusMagiAllRemoveConfirm_YES()
	{
		this.StartCoroutine(sendMagiAllRemove());
	}

	protected override void OnQuery_DECISION()
	{
		if (!IsReadyCheck())
		{
			GameSection.ChangeEvent("NOT_EQUIP");
			return;
		}
		GameSection.ChangeEvent("[BACK]");
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquipSet(delegate(bool is_success)
		{
			if (is_success)
			{
				ChangeOrderNo();
			}
			else
			{
				GameSection.ResumeEvent(is_resume: false);
			}
		});
	}

	protected override string GetEquipSetBasePrefabName()
	{
		return "QuestSeriesArenaChangeEquipSetBase";
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_SKILL_CHANGE) != (NOTIFY_FLAG)0L)
		{
			MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSets(MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet());
		}
		if ((flags & NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO) != (NOTIFY_FLAG)0L)
		{
			ReloadPlayerModelByLocalEquipSet();
		}
		if ((GetUpdateUINotifyFlags() & flags) != (NOTIFY_FLAG)0L)
		{
			RefreshUI();
		}
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_SKILL_CHANGE | NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
	}

	protected override void ReplaceEquipItem(EquipSetInfo equipSetInfo, int setNo, int index)
	{
		MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipItem(equipSetInfo, setNo, index);
	}

	public override void localEquipCalcUpdate(EquipSetInfo[] equipSets)
	{
		if (MonoBehaviourSingleton<StatusManager>.I.isEquipSetCalcUpdate)
		{
			MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSets(equipSets);
		}
	}

	protected IEnumerator sendMagiAllRemove()
	{
		bool wait = true;
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.RemoveOrderNo(MonoBehaviourSingleton<StatusManager>.I.GetCurrentUniqueEquipSetNo(), delegate
		{
			wait = false;
		});
		while (wait)
		{
			yield return null;
		}
		wait = true;
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeUniqueEquipSet(delegate
		{
			wait = false;
		});
		while (wait)
		{
			yield return null;
		}
		wait = true;
		MonoBehaviourSingleton<StatusManager>.I.SendDetachAllSkillFromEvery(selfCharaEquipSetNo, delegate
		{
			wait = false;
		});
		while (wait)
		{
			yield return null;
		}
		GameSection.ResumeEvent(is_resume: true);
	}

	protected override bool IsNullWeaponSloat(int id)
	{
		return false;
	}

	protected void ChangeOrderNo()
	{
		MonoBehaviourSingleton<StatusManager>.I.ChangeOrderNo(order, selfCharaEquipSetNo, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	protected override void ViewDetailUI()
	{
		EquipSetCalculator uniqueEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetUniqueEquipSetCalculator(selfCharaEquipSetNo);
		SimpleStatus finalStatus = uniqueEquipSetCalculator.GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
		int attacksSum = finalStatus.GetAttacksSum();
		int defencesSum = finalStatus.GetDefencesSum();
		int hp = finalStatus.hp;
		int num = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
		SetLabelText(transRoot, UI.LBL_ATK, attacksSum.ToString());
		SetLabelText(transRoot, UI.LBL_DEF, defencesSum.ToString());
		SetLabelText(transRoot, UI.LBL_HP, hp.ToString());
		SetLabelText(transRoot, UI.LBL_LEVEL, num.ToString());
		SetupInfo();
		UpdateEquipIcon(null);
		CreateDegree();
		SetMoveMessageButton();
		if (record != null && record.charaInfo != null && record.charaInfo.userClanData != null)
		{
			UpdateClanInfo(record.charaInfo);
		}
		else
		{
			DisableClanInfo();
		}
		SetButtonEnabled((Enum)UI.BTN_MAGI_REMOVE, MonoBehaviourSingleton<StatusManager>.I.checkEquipMagi(selfCharaEquipSetNo));
	}
}
