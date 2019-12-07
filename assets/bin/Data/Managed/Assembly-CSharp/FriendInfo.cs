using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendInfo : SkillInfoBase
{
	protected enum UI
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
		SPR_SAME_CLAN_ICON,
		LBL_LEVEL_WEAPON_1,
		LBL_LEVEL_WEAPON_2,
		LBL_LEVEL_WEAPON_3,
		LBL_LEVEL_ARMOR,
		LBL_LEVEL_HELM,
		LBL_LEVEL_ARM,
		LBL_LEVEL_LEG,
		LBL_CHANGE_MODE,
		BTN_MAGI,
		LBL_SET_NAME,
		OBJ_DEGREE_PLATE_ROOT,
		BTN_DELETEFOLLOWER,
		BTN_KICK,
		BTN_JOIN,
		BTN_MOVE_TO_MSG,
		OBJ_CLAN_ROOT,
		BTN_CLAN_SCOUT,
		SPR_CLAN_SCOUT,
		BTN_CLAN_DETAIL,
		TXT_CLAN_TITLE,
		SPR_CLAN_NAME,
		BTN_CLAN_SCOUT_CANCEL,
		BTN_CLAN_SCOUT_OFF,
		OBJ_SYMBOL,
		OBJ_SYMBOL_MARK
	}

	protected UI[] icons = new UI[7]
	{
		UI.OBJ_ICON_WEAPON_1,
		UI.OBJ_ICON_WEAPON_2,
		UI.OBJ_ICON_WEAPON_3,
		UI.OBJ_ICON_ARMOR,
		UI.OBJ_ICON_HELM,
		UI.OBJ_ICON_ARM,
		UI.OBJ_ICON_LEG
	};

	protected UI[] icons_btn = new UI[7]
	{
		UI.BTN_ICON_WEAPON_1,
		UI.BTN_ICON_WEAPON_2,
		UI.BTN_ICON_WEAPON_3,
		UI.BTN_ICON_ARMOR,
		UI.BTN_ICON_HELM,
		UI.BTN_ICON_ARM,
		UI.BTN_ICON_LEG
	};

	protected UI[] icons_level = new UI[7]
	{
		UI.LBL_LEVEL_WEAPON_1,
		UI.LBL_LEVEL_WEAPON_2,
		UI.LBL_LEVEL_WEAPON_3,
		UI.LBL_LEVEL_ARMOR,
		UI.LBL_LEVEL_HELM,
		UI.LBL_LEVEL_ARM,
		UI.LBL_LEVEL_LEG
	};

	protected CharaInfo data;

	protected FriendCharaInfo friendCharaInfo;

	protected FriendMessageUserListModel.MessageUserInfo m_msgUserInfo;

	protected CharaInfo clanCharaInfo;

	protected ClanData userClanData;

	protected PlayerLoader loader;

	protected Transform transRoot;

	protected string nowSectionName = string.Empty;

	protected DegreePlate degree;

	protected bool isVisualMode;

	protected const string STR_VISUAL_EQUIP_EVENT_NAME = "VISUAL_DETAIL";

	protected bool isFollowerList;

	protected bool isFollowerListChengeTrans;

	protected bool dataFollower;

	protected bool dataFollowing;

	protected bool m_isInitMoveMessageButton;

	private SymbolMarkCtrl symbolMark;

	protected virtual bool IsFriendInfo => true;

	protected virtual List<int> SelectedDegrees
	{
		get
		{
			if (IsFriendInfo)
			{
				return data.selectedDegrees;
			}
			return MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds;
		}
	}

	protected virtual bool showMagiButton => false;

	protected bool IsInitMoveMessageButton => m_isInitMoveMessageButton;

	protected virtual string GetCreatePrefabName()
	{
		return "FriendInfoBase";
	}

	public override void Initialize()
	{
		InitializeBase();
	}

	protected void InitializeBase()
	{
		StartCoroutine(DoInitialize());
	}

	protected IEnumerator DoInitialize()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
		{
			bool isWait = true;
			MonoBehaviourSingleton<ClanMatchingManager>.I.RequestDetail("0", delegate
			{
				isWait = false;
			});
			while (isWait)
			{
				yield return null;
			}
			userClanData = MonoBehaviourSingleton<ClanMatchingManager>.I.clanData;
		}
		base.Initialize();
	}

	protected override void OnOpen()
	{
		ReOpen();
		base.OnOpen();
	}

	protected void ReOpen()
	{
		if (GameSection.GetEventData() is CharaInfo)
		{
			friendCharaInfo = (GameSection.GetEventData() as FriendCharaInfo);
			data = (GameSection.GetEventData() as CharaInfo);
			m_msgUserInfo = (GameSection.GetEventData() as FriendMessageUserListModel.MessageUserInfo);
			if (friendCharaInfo != null)
			{
				dataFollower = friendCharaInfo.follower;
				dataFollowing = friendCharaInfo.following;
			}
			nowSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
			isFollowerList = UnityEngine.Object.FindObjectOfType(typeof(FriendFollowerList));
			DisableClanInfo();
		}
	}

	protected void OnEnable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
	}

	protected void OnDisable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
		nowSectionName = string.Empty;
	}

	private void OnDrag(InputManager.TouchInfo touch_info)
	{
		if (!(loader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == nowSectionName)
			{
				loader.transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
			}
			else if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "QuestResultFriendDetail")
			{
				loader.transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
			}
			else if (nowSectionName == "ClanDetail")
			{
				loader.transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
			}
		}
	}

	public override void UpdateUI()
	{
		transRoot = SetPrefab(UI.OBJ_EQUIP_SET_ROOT, GetCreatePrefabName());
		UpdateUserIDLabel();
		UpdateHeader();
		LoadModel();
		UpdateEquipIcon(data.equipSet);
		UpdateBottomButton();
		CreateDegree();
		if (data != null && data.userClanData != null)
		{
			UpdateClanInfo(data);
		}
		else
		{
			DisableClanInfo();
		}
	}

	protected virtual void UpdateUserIDLabel()
	{
		SetLabelText(transRoot, UI.LBL_USER_ID, data.code);
	}

	protected void UpdateHeader()
	{
		EquipSetCalculator otherEquipSetCalculator;
		if (MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex == -1)
		{
			MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = 0;
			otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(0);
			otherEquipSetCalculator.SetEquipSet(data.equipSet);
		}
		else
		{
			otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex);
		}
		SimpleStatus finalStatus = otherEquipSetCalculator.GetFinalStatus(0, data.hp, data.atk, data.def);
		SetActive(transRoot, UI.OBJ_LAST_LOGIN, is_visible: true);
		SetLabelText(transRoot, UI.LBL_NAME, data.name);
		SetLabelText(transRoot, UI.LBL_COMMENT, data.comment);
		SetLabelText(transRoot, UI.LBL_LAST_LOGIN, base.sectionData.GetText("LAST_LOGIN"));
		SetLabelText(transRoot, UI.LBL_LAST_LOGIN_TIME, data.lastLogin);
		SetLabelText(transRoot, UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
		SetLabelText(transRoot, UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
		SetLabelText(transRoot, UI.LBL_HP, finalStatus.hp.ToString());
		SetLabelText(transRoot, UI.LBL_LEVEL, data.level.ToString());
		bool black_list_user = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(data.userId);
		bool same_clan_user = false;
		if (data.userClanData != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
		{
			same_clan_user = (data.userClanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId);
		}
		SetFollowStatus(dataFollowing, dataFollower, black_list_user, same_clan_user);
	}

	protected void SetFollowStatus(bool following, bool follower, bool black_list_user, bool same_clan_user)
	{
		SetActive(transRoot, UI.SPR_FOLLOW_ARROW, !black_list_user && following);
		SetActive(transRoot, UI.SPR_FOLLOWER_ARROW, !black_list_user && follower);
		SetActive(transRoot, UI.SPR_BLACKLIST_ICON, black_list_user);
		SetActive(transRoot, UI.SPR_SAME_CLAN_ICON, same_clan_user);
	}

	protected virtual void LoadModel()
	{
		SetRenderPlayerModel(transRoot, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(data, need_weapon: true, need_helm: true, need_leg: true, isVisualMode), PLAYER_ANIM_TYPE.GetStatus(data.sex), new Vector3(0f, -0.75f, 14f), new Vector3(0f, 180f, 0f), isVisualMode, delegate(PlayerLoader player_loader)
		{
			if (player_loader != null)
			{
				loader = player_loader;
			}
		});
	}

	protected virtual void CreateDegree()
	{
		GetCtrl(UI.OBJ_DEGREE_PLATE_ROOT).GetComponent<DegreePlate>().Initialize(SelectedDegrees, isButton: false, delegate
		{
		});
	}

	protected virtual void UpdateEquipIcon(List<CharaInfo.EquipItem> equip_set_info)
	{
		int weapon_cnt = 0;
		SetActive(transRoot, UI.LBL_CHANGE_MODE, isVisualMode);
		int i = 0;
		for (int num = 7; i < num; i++)
		{
			SetEvent(FindCtrl(transRoot, icons[i]), "EMPTY", 0);
			SetEvent(FindCtrl(transRoot, icons_btn[i]), "EMPTY", 0);
			SetLabelText(FindCtrl(transRoot, icons_level[i]), string.Empty);
			SetActive(FindCtrl(transRoot, icons[i]), is_visible: false);
		}
		bool need_visual_helm_icon = isVisualMode;
		bool need_visual_armor_icon = isVisualMode;
		bool need_visual_arm_icon = isVisualMode;
		bool need_visual_leg_icon = isVisualMode;
		equip_set_info.ForEach(delegate(CharaInfo.EquipItem equip_data)
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)equip_data.eId);
			if (equipItemData != null)
			{
				if (isVisualMode && !equipItemData.IsWeapon())
				{
					equipItemData = GetVisualModeTargetTable(equipItemData.id, equipItemData.type, data);
					if (equipItemData == null)
					{
						return;
					}
				}
				Transform transform = null;
				int num2 = -1;
				if (equipItemData.IsWeapon())
				{
					transform = FindCtrl(transRoot, icons[weapon_cnt]);
					num2 = weapon_cnt;
					int num3 = ++weapon_cnt;
				}
				else
				{
					switch (equipItemData.type)
					{
					case EQUIPMENT_TYPE.ARMOR:
					case EQUIPMENT_TYPE.VISUAL_ARMOR:
						num2 = 3;
						need_visual_armor_icon = false;
						break;
					case EQUIPMENT_TYPE.HELM:
					case EQUIPMENT_TYPE.VISUAL_HELM:
						num2 = 4;
						need_visual_helm_icon = false;
						break;
					case EQUIPMENT_TYPE.ARM:
					case EQUIPMENT_TYPE.VISUAL_ARM:
						num2 = 5;
						need_visual_arm_icon = false;
						break;
					case EQUIPMENT_TYPE.LEG:
					case EQUIPMENT_TYPE.VISUAL_LEG:
						num2 = 6;
						need_visual_leg_icon = false;
						break;
					}
					if (num2 != -1)
					{
						transform = FindCtrl(transRoot, icons[num2]);
					}
				}
				if (!(transform == null))
				{
					SetActive(FindCtrl(transRoot, icons[num2]), is_visible: true);
					string event_name = isVisualMode ? "VISUAL_DETAIL" : "DETAIL";
					ItemIcon itemIcon = ItemIcon.CreateEquipItemIconByEquipItemTable(equipItemData, GetCharaSex(), transform, null, -1, event_name, num2);
					SetLongTouch(itemIcon.transform, event_name, num2);
					SetEvent(FindCtrl(transRoot, icons_btn[num2]), event_name, num2);
					SetLongTouch(FindCtrl(transRoot, icons_btn[num2]), event_name, num2);
					EquipItemInfo info = new EquipItemInfo(equip_data);
					itemIcon.SetEquipExtInvertedColor(info, GetComponent<UILabel>(icons_level[num2]));
					SetActive(FindCtrl(transRoot, icons_level[num2]), !isVisualMode);
					if (equip_data != null)
					{
						string text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1u), equip_data.lv.ToString());
						SetLabelText(FindCtrl(transRoot, icons_level[num2]), text);
					}
				}
			}
		});
		if (need_visual_helm_icon && data.hId != 0)
		{
			int index = 4;
			int hId = data.hId;
			EQUIPMENT_TYPE e_type = EQUIPMENT_TYPE.HELM;
			CharaInfo chara_info = data;
			SetVisualModeIcon(index, hId, e_type, chara_info);
		}
		if (need_visual_armor_icon && data.aId != 0)
		{
			int index2 = 3;
			int aId = data.aId;
			EQUIPMENT_TYPE e_type2 = EQUIPMENT_TYPE.ARMOR;
			CharaInfo chara_info2 = data;
			SetVisualModeIcon(index2, aId, e_type2, chara_info2);
		}
		if (need_visual_arm_icon && data.rId != 0)
		{
			int index3 = 5;
			int rId = data.rId;
			EQUIPMENT_TYPE e_type3 = EQUIPMENT_TYPE.ARM;
			CharaInfo chara_info3 = data;
			SetVisualModeIcon(index3, rId, e_type3, chara_info3);
		}
		if (need_visual_leg_icon && data.lId != 0)
		{
			int index4 = 6;
			int lId = data.lId;
			EQUIPMENT_TYPE e_type4 = EQUIPMENT_TYPE.LEG;
			CharaInfo chara_info4 = data;
			SetVisualModeIcon(index4, lId, e_type4, chara_info4);
		}
	}

	protected void SetVisualModeIcon(int index, int table_id, EQUIPMENT_TYPE e_type, CharaInfo chara_info)
	{
		string event_name = "VISUAL_DETAIL";
		Transform transform = FindCtrl(transRoot, icons[index]);
		EquipItemTable.EquipItemData visualModeTargetTable = GetVisualModeTargetTable((uint)table_id, e_type, chara_info);
		if (visualModeTargetTable != null)
		{
			transform.GetComponentsInChildren(includeInactive: true, Temporary.itemIconList);
			int i = 0;
			for (int count = Temporary.itemIconList.Count; i < count; i++)
			{
				Temporary.itemIconList[i].gameObject.SetActive(value: true);
			}
			Temporary.itemIconList.Clear();
			SetActive(FindCtrl(transRoot, icons[index]), is_visible: true);
			ItemIcon itemIcon = ItemIcon.CreateEquipItemIconByEquipItemTable(visualModeTargetTable, GetCharaSex(), transform, null, -1, event_name, index);
			SetLongTouch(itemIcon.transform, event_name, index);
			SetEvent(FindCtrl(transRoot, icons_btn[index]), event_name, index);
			SetLongTouch(FindCtrl(transRoot, icons_btn[index]), event_name, index);
			SetActive(FindCtrl(transRoot, icons_level[index]), !isVisualMode);
		}
	}

	protected void UpdateBottomButton()
	{
		SetActive(transRoot, UI.OBJ_FRIEND_INFO_ROOT, IsFriendInfo);
		SetActive(transRoot, UI.OBJ_CHANGE_EQUIP_INFO_ROOT, !IsFriendInfo);
		SetActive(transRoot, UI.BTN_MAGI, showMagiButton);
		bool flag = MonoBehaviourSingleton<FriendManager>.I.followNum == MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow;
		SetEvent(transRoot, UI.BTN_FOLLOW, "FOLLOW", 0);
		if (!isFollowerList)
		{
			if (flag && !dataFollowing)
			{
				SetActive(transRoot, UI.BTN_FOLLOW, is_visible: true);
				SetActive(transRoot, UI.BTN_UNFOLLOW, is_visible: false);
				SetEvent(transRoot, UI.BTN_FOLLOW, "INVALID_FOLLOW", 0);
			}
			else
			{
				SetActive(transRoot, UI.BTN_FOLLOW, !dataFollowing);
				SetActive(transRoot, UI.BTN_UNFOLLOW, dataFollowing);
			}
			SetActive(transRoot, UI.BTN_DELETEFOLLOWER, is_visible: false);
		}
		else
		{
			SetActive(transRoot, UI.BTN_DELETEFOLLOWER, dataFollower);
			if (!flag && !dataFollowing)
			{
				SetActive(transRoot, UI.BTN_FOLLOW, is_visible: true);
				if (!isFollowerListChengeTrans)
				{
					Transform transform = FindCtrl(transRoot, UI.BTN_FOLLOW);
					transform.localPosition = new Vector3(transform.localPosition.x - 167f, transform.localPosition.y - 502f, transform.localPosition.z);
					transform.localScale = new Vector3(1f, 1f, 1f);
					isFollowerListChengeTrans = true;
				}
			}
			else
			{
				SetActive(transRoot, UI.BTN_FOLLOW, is_visible: false);
			}
			SetActive(transRoot, UI.BTN_UNFOLLOW, is_visible: false);
		}
		SetActive(transRoot, UI.OBJ_BLACKLIST_ROOT, is_visible: true);
		bool flag2 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(data.userId);
		SetActive(transRoot, UI.BTN_BLACKLIST_IN, !flag2);
		SetActive(transRoot, UI.BTN_BLACKLIST_OUT, flag2);
		SetMoveMessageButton();
	}

	protected void SetMoveMessageButton()
	{
		if (!IsInitMoveMessageButton)
		{
			m_isInitMoveMessageButton = true;
			bool flag = UserInfoManager.IsEnableCommunication();
			bool flag2 = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("FriendInfo");
			bool is_visible = (m_msgUserInfo != null && flag) & flag2;
			SetActive(transRoot, UI.BTN_MOVE_TO_MSG, is_visible);
		}
	}

	protected EquipItemTable.EquipItemData GetVisualModeTargetTable(uint base_table_id, EQUIPMENT_TYPE e_type, CharaInfo chara_info)
	{
		EquipItemTable.EquipItemData result = null;
		if (isVisualMode)
		{
			uint num = base_table_id;
			switch (e_type)
			{
			case EQUIPMENT_TYPE.ARMOR:
			case EQUIPMENT_TYPE.VISUAL_ARMOR:
				if (chara_info.aId != 0)
				{
					num = (uint)chara_info.aId;
				}
				break;
			case EQUIPMENT_TYPE.HELM:
			case EQUIPMENT_TYPE.VISUAL_HELM:
				if (chara_info.showHelm != 0)
				{
					if (chara_info.hId != 0)
					{
						num = (uint)chara_info.hId;
					}
				}
				else
				{
					num = 0u;
				}
				break;
			case EQUIPMENT_TYPE.ARM:
			case EQUIPMENT_TYPE.VISUAL_ARM:
				if (chara_info.rId != 0)
				{
					num = (uint)chara_info.rId;
				}
				break;
			case EQUIPMENT_TYPE.LEG:
			case EQUIPMENT_TYPE.VISUAL_LEG:
				if (chara_info.lId != 0)
				{
					num = (uint)chara_info.lId;
				}
				break;
			}
			if (num != 0)
			{
				result = Singleton<EquipItemTable>.I.GetEquipItemData(num);
			}
		}
		return result;
	}

	private bool IsLoopClanDetail()
	{
		List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
		for (int i = 0; i < historyList.Count; i++)
		{
			if (historyList[i].sectionName == "ClanDetail")
			{
				return true;
			}
		}
		return false;
	}

	protected void UpdateClanInfo(CharaInfo charaInfo)
	{
		clanCharaInfo = charaInfo;
		SetActive(transRoot, UI.OBJ_CLAN_ROOT, is_visible: false);
		if (clanCharaInfo == null || IsLoopClanDetail())
		{
			return;
		}
		if (charaInfo.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			if (clanCharaInfo.userClanData != null && clanCharaInfo.userClanData.stat != 0)
			{
				_showClanDetail();
			}
		}
		else if (clanCharaInfo.userClanData != null && clanCharaInfo.userClanData.stat != 0)
		{
			_showClanDetail();
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsSubLeader() || MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader()))
		{
			_showClanScout();
		}
	}

	protected void DisableClanInfo()
	{
		SetActive(transRoot, UI.OBJ_CLAN_ROOT, is_visible: false);
	}

	private void _showClanDetail()
	{
		if (clanCharaInfo != null)
		{
			SetActive(transRoot, UI.OBJ_CLAN_ROOT, is_visible: true);
			SetActive(transRoot, UI.BTN_CLAN_SCOUT, is_visible: false);
			SetActive(transRoot, UI.BTN_CLAN_SCOUT_CANCEL, is_visible: false);
			SetActive(transRoot, UI.BTN_CLAN_DETAIL, is_visible: true);
			SetLabelText(transRoot, UI.SPR_CLAN_NAME, clanCharaInfo.userClanData.name);
			if (FindCtrl(transRoot, UI.OBJ_SYMBOL_MARK) == null)
			{
				StartCoroutine(CreateSymbolMark());
			}
		}
	}

	private IEnumerator CreateSymbolMark()
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject symbolMarkLoadObj = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
		yield return loadingQueue.Wait();
		Transform transform = ResourceUtility.Realizes(symbolMarkLoadObj.loadedObject as GameObject, 5);
		transform.parent = FindCtrl(transRoot, UI.OBJ_SYMBOL);
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
		transform.name = "OBJ_SYMBOL_MARK";
		symbolMark = transform.GetComponent<SymbolMarkCtrl>();
		symbolMark.Initilize();
		symbolMark.LoadSymbol(clanCharaInfo.userClanData.sym);
		symbolMark.SetSize(20);
	}

	private void _showClanScout()
	{
		if (clanCharaInfo != null)
		{
			SetActive(transRoot, UI.OBJ_CLAN_ROOT, is_visible: true);
			SetActive(transRoot, UI.BTN_CLAN_SCOUT, is_visible: false);
			SetActive(transRoot, UI.BTN_CLAN_SCOUT_CANCEL, is_visible: false);
			SetActive(transRoot, UI.BTN_CLAN_SCOUT_OFF, is_visible: false);
			SetActive(transRoot, UI.BTN_CLAN_DETAIL, is_visible: false);
			if ((int)clanCharaInfo.level < 15)
			{
				SetActive(transRoot, UI.BTN_CLAN_SCOUT_OFF, is_visible: true);
			}
			else if (userClanData != null && userClanData.num >= MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_MAX_MEMBER_NUM)
			{
				SetActive(transRoot, UI.BTN_CLAN_SCOUT_OFF, is_visible: true);
				SetEvent(transRoot, UI.BTN_CLAN_SCOUT_OFF, "CLAN_SCOUT_MAX_MEMBER", 0);
			}
			else if (clanCharaInfo.isInviteToClan)
			{
				SetActive(transRoot, UI.BTN_CLAN_SCOUT_CANCEL, is_visible: true);
			}
			else
			{
				SetActive(transRoot, UI.BTN_CLAN_SCOUT, is_visible: true);
			}
		}
	}

	public virtual int GetCharaSex()
	{
		return data.sex;
	}

	protected virtual void OnQuery_FOLLOW()
	{
		GameSection.SetEventData(new object[1]
		{
			data.name
		});
		List<int> list = new List<int>();
		list.Add(data.userId);
		SendFollow(list, delegate(bool is_success)
		{
			if (is_success)
			{
				UpdateFollowing();
			}
		});
	}

	protected virtual void OnQuery_UNFOLLOW()
	{
		GameSection.SetEventData(new object[1]
		{
			data.name
		});
	}

	protected virtual void OnQuery_FriendUnFollowMessage_YES()
	{
		GameSection.SetEventData(new object[1]
		{
			data.name
		});
		SendUnFollow(data.userId, delegate(bool is_success)
		{
			if (is_success)
			{
				UpdateFollowing();
			}
		});
	}

	private void UpdateFollowing()
	{
		dataFollowing = !dataFollowing;
		if (friendCharaInfo != null)
		{
			friendCharaInfo.following = dataFollowing;
		}
	}

	private void UpdateFollower()
	{
		dataFollower = !dataFollower;
		if (friendCharaInfo != null)
		{
			friendCharaInfo.follower = dataFollower;
		}
	}

	private void UpdateClanInvite()
	{
		clanCharaInfo.isInviteToClan = !clanCharaInfo.isInviteToClan;
	}

	protected virtual void OnQuery_DELETEFOLLOWER()
	{
		GameSection.SetEventData(new object[1]
		{
			data.name
		});
	}

	protected virtual void OnQuery_FriendDeleteFollowerMessage_YES()
	{
		GameSection.SetEventData(new object[1]
		{
			data.name
		});
		SendDeleteFollower(data.userId, delegate(bool is_success)
		{
			if (is_success)
			{
				UpdateFollower();
			}
		});
	}

	protected void SendFollow(List<int> send_follow_list, Action<bool> callback = null)
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(send_follow_list, delegate(Error err, List<int> follow_list)
		{
			bool flag = err == Error.None && follow_list.Count > 0;
			if (callback != null)
			{
				callback(flag);
			}
			GameSection.ResumeEvent(flag);
		});
	}

	protected void SendUnFollow(int send_unfollow, Action<bool> callback = null)
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendUnfollowUser(send_unfollow, delegate(bool is_success)
		{
			if (callback != null)
			{
				callback(is_success);
			}
			GameSection.ResumeEvent(is_success);
		});
	}

	protected void SendDeleteFollower(int send_deletefollower, Action<bool> callback = null)
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendDeleteFollower(send_deletefollower, delegate(bool is_success)
		{
			if (callback != null)
			{
				callback(is_success);
			}
			GameSection.ResumeEvent(is_success);
		});
	}

	protected void SendClanInvite(int send_userid, Action<bool> callback = null)
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ClanMatchingManager>.I.SendClanInvite(send_userid, delegate(bool is_success)
		{
			if (callback != null)
			{
				callback(is_success);
			}
			GameSection.ResumeEvent(is_success);
		});
	}

	protected void SendClanCancelInvite(int send_userid, Action<bool> callback = null)
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ClanMatchingManager>.I.SendClanCancelInvite(send_userid, delegate(bool is_success)
		{
			if (callback != null)
			{
				callback(is_success);
			}
			GameSection.ResumeEvent(is_resume: true);
		});
	}

	protected virtual void OnQuery_BLACK_LIST_IN()
	{
		GameSection.SetEventData(new object[1]
		{
			data.name
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<BlackListManager>.I.SendAdd(data.userId, delegate(bool is_success)
		{
			if (is_success)
			{
				dataFollowing = false;
				if (friendCharaInfo != null)
				{
					friendCharaInfo.following = false;
				}
			}
			GameSection.ResumeEvent(is_success);
		});
	}

	protected virtual void OnQuery_BLACK_LIST_OUT()
	{
		GameSection.SetEventData(new object[1]
		{
			data.name
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<BlackListManager>.I.SendDelete(data.userId, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	protected virtual void OnQuery_CHANGE_MODE()
	{
		RefreshUI();
		LoadModel();
	}

	protected virtual void OnQuery_DETAIL()
	{
		if (isVisualMode)
		{
			GameSection.ChangeEvent("VISUAL_DETAIL");
			OnQuery_VISUAL_DETAIL();
		}
		else
		{
			int num = (int)GameSection.GetEventData();
			object[] array = new object[4]
			{
				ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
				GetEquipSetAttachSkillListData(data.equipSet)[num],
				data.sex,
				data.faceId
			};
			GameSection.SetEventData(new object[3]
			{
				array,
				false,
				false
			});
		}
	}

	protected virtual void OnQuery_VISUAL_DETAIL()
	{
		GameSection.StopEvent();
	}

	protected virtual void OnQuery_SKILL_LIST()
	{
		object[] array = new object[5]
		{
			ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
			GetEquipSetAttachSkillListData(data.equipSet),
			true,
			data.sex,
			data.faceId
		};
		GameSection.SetEventData(new object[3]
		{
			array,
			false,
			false
		});
	}

	protected virtual void OnQuery_ABILITY()
	{
		EquipSetInfo equipSetInfo = MonoBehaviourSingleton<StatusManager>.I.CreateEquipSetData(data.equipSet);
		object[] array = new object[3]
		{
			equipSetInfo,
			MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(equipSetInfo),
			new EquipSetDetailStatusAndAbilityTable.BaseStatus(data.atk, data.def, data.hp, data.equipSet)
		};
		GameSection.SetEventData(new object[3]
		{
			array,
			false,
			false
		});
	}

	protected virtual void OnQuery_STATUS()
	{
		EquipSetInfo equipSetInfo = MonoBehaviourSingleton<StatusManager>.I.CreateEquipSetData(data.equipSet);
		object[] array = new object[3]
		{
			equipSetInfo,
			MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(equipSetInfo),
			new EquipSetDetailStatusAndAbilityTable.BaseStatus(data.atk, data.def, data.hp, data.equipSet)
		};
		GameSection.SetEventData(new object[3]
		{
			array,
			false,
			false
		});
	}

	protected virtual void OnQuery_SECTION_BACK()
	{
		MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = -1;
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
	}

	public void OnQuery_TO_MESSAGE()
	{
		if (m_msgUserInfo == null)
		{
			GameSection.StopEvent();
		}
		else if (!m_msgUserInfo.isPermitted)
		{
			GameSection.ChangeEvent("NOT_PERMITTED");
		}
	}

	public void OnQuery_FriendMessageConfirmMessage_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(m_msgUserInfo.userId, 0, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
		MonoBehaviourSingleton<FriendManager>.I.SetNoReadMessageNum(MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum - m_msgUserInfo.noReadNum);
		m_msgUserInfo.noReadNum = 0;
	}

	protected virtual void OnQuery_CLAN_SCOUT()
	{
		GameSection.SetEventData(new object[1]
		{
			clanCharaInfo.name
		});
	}

	protected virtual void OnQuery_CLAN_SCOUT_OFF()
	{
		GameSection.SetEventData(new object[1]
		{
			clanCharaInfo.name
		});
	}

	protected virtual void OnQuery_ClanScoutDialog_YES()
	{
		GameSection.SetEventData(new object[1]
		{
			clanCharaInfo.name
		});
		SendClanInvite(clanCharaInfo.userId, delegate(bool is_success)
		{
			if (is_success)
			{
				UpdateClanInvite();
				GameSection.ChangeStayEvent("COMPLETE");
				MonoBehaviourSingleton<FriendManager>.I.SetClanInviteToHomeCharaInfo(clanCharaInfo.userId, clanCharaInfo.isInviteToClan);
			}
			else
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
				{
					new EventData("[BACK]")
				});
				_showClanScout();
			}
		});
	}

	protected virtual void OnQuery_CLAN_SCOUT_CANCEL()
	{
		GameSection.SetEventData(new object[1]
		{
			clanCharaInfo.name
		});
	}

	protected virtual void OnQuery_ClanScoutCancelDialog_YES()
	{
		GameSection.SetEventData(new object[1]
		{
			clanCharaInfo.name
		});
		SendClanCancelInvite(clanCharaInfo.userId, delegate(bool is_success)
		{
			if (is_success)
			{
				UpdateClanInvite();
				GameSection.ChangeStayEvent("COMPLETE");
				MonoBehaviourSingleton<FriendManager>.I.SetClanInviteToHomeCharaInfo(clanCharaInfo.userId, clanCharaInfo.isInviteToClan);
			}
		});
	}

	public void OnQuery_CLAN_DETAIL()
	{
		if (clanCharaInfo != null)
		{
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 15)
			{
				GameSection.StopEvent();
			}
			else
			{
				GameSection.SetEventData(clanCharaInfo.userClanData.cId);
			}
		}
	}
}
