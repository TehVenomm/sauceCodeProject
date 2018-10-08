using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFriendDetailBase : FriendInfo
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
		BTN_MAGI,
		LBL_SET_NAME,
		OBJ_DEGREE_PLATE_ROOT,
		BTN_DELETEFOLLOWER
	}

	protected bool isLoading;

	protected bool reloadModel;

	protected InGameRecorder.PlayerRecord record;

	protected bool isSelfData;

	protected int detailUserID;

	protected EquipSetInfo localEquipSet;

	protected int selfCharaEquipSetNo;

	protected bool isQuestResult;

	protected bool isChangeEquip;

	protected List<int> mSelectedDegrees;

	protected bool AlwaysNowStatusModel
	{
		get;
		private set;
	}

	protected override bool showMagiButton => !IsFriendInfo && isSelfData;

	protected override List<int> SelectedDegrees => mSelectedDegrees;

	public override void Initialize()
	{
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		detailUserID = 0;
		isSelfData = false;
		isQuestResult = false;
		if (record == null)
		{
			record = (GameSection.GetEventData() as InGameRecorder.PlayerRecord);
			if (record != null)
			{
				detailUserID = ((record.id == 0) ? MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id : record.charaInfo.userId);
				isSelfData = (detailUserID == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
				mSelectedDegrees = ((!isSelfData) ? record.charaInfo.selectedDegrees : MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds);
				if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName().Contains("InGame"))
				{
					isQuestResult = true;
				}
				else if (!isChangeEquip)
				{
					AlwaysNowStatusModel = true;
				}
			}
		}
		selfCharaEquipSetNo = ((!isSelfData) ? (-1) : MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo);
		transRoot = SetPrefab((Enum)UI.OBJ_EQUIP_SET_ROOT, "FriendInfoBase");
		this.StartCoroutine(DoInitialize());
	}

	protected IEnumerator DoInitialize()
	{
		LoadModel();
		while (isLoading)
		{
			yield return (object)null;
		}
		GameSection.SetEventData(null);
		base.Initialize();
	}

	protected override void LoadModel()
	{
		if (record != null)
		{
			PlayerLoadInfo playerLoadInfo = record.playerLoadInfo;
			if (isSelfData)
			{
				if (reloadModel)
				{
					if (isQuestResult)
					{
						playerLoadInfo = PlayerLoadInfo.FromCharaInfo(record.charaInfo, true, true, true, isVisualMode);
						EquipItemTable.EquipItemData equipItemData = null;
						if (playerLoadInfo.weaponModelID == -1)
						{
							EquipSetInfo equipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(selfCharaEquipSetNo);
							equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipSet.item[0].tableID);
							if (equipItemData != null)
							{
								playerLoadInfo.weaponModelID = equipItemData.GetModelID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
								playerLoadInfo.weaponColor0 = equipItemData.modelColor0;
								playerLoadInfo.weaponColor1 = equipItemData.modelColor1;
								playerLoadInfo.weaponColor2 = equipItemData.modelColor2;
								playerLoadInfo.weaponEffectID = equipItemData.effectID;
								playerLoadInfo.weaponEffectColor = equipItemData.effectColor;
								playerLoadInfo.weaponEffectParam = equipItemData.effectParam;
								playerLoadInfo.weaponSpAttackType = (uint)equipItemData.spAttackType;
							}
						}
						else
						{
							playerLoadInfo.weaponModelID = record.playerLoadInfo.weaponModelID;
							playerLoadInfo.weaponColor0 = record.playerLoadInfo.weaponColor0;
							playerLoadInfo.weaponColor1 = record.playerLoadInfo.weaponColor1;
							playerLoadInfo.weaponColor2 = record.playerLoadInfo.weaponColor2;
							playerLoadInfo.weaponEffectID = record.playerLoadInfo.weaponEffectID;
							playerLoadInfo.weaponEffectColor = record.playerLoadInfo.weaponEffectColor;
							playerLoadInfo.weaponEffectParam = record.playerLoadInfo.weaponEffectParam;
							playerLoadInfo.weaponSpAttackType = record.playerLoadInfo.weaponSpAttackType;
						}
						record.animID = -1;
					}
					else
					{
						playerLoadInfo = PlayerLoadInfo.FromUserStatus(true, isVisualMode, selfCharaEquipSetNo);
					}
				}
				else if (AlwaysNowStatusModel || record.playerLoadInfo.weaponModelID == -1)
				{
					record.playerLoadInfo = PlayerLoadInfo.FromUserStatus(true, isVisualMode, -1);
					record.animID = -1;
					playerLoadInfo = record.playerLoadInfo;
				}
			}
			else if (isVisualMode)
			{
				playerLoadInfo = record.playerLoadInfo;
			}
			else
			{
				playerLoadInfo = PlayerLoadInfo.FromCharaInfo(record.charaInfo, true, true, true, isVisualMode);
				playerLoadInfo.weaponModelID = record.playerLoadInfo.weaponModelID;
				playerLoadInfo.weaponColor0 = record.playerLoadInfo.weaponColor0;
				playerLoadInfo.weaponColor1 = record.playerLoadInfo.weaponColor1;
				playerLoadInfo.weaponColor2 = record.playerLoadInfo.weaponColor2;
				playerLoadInfo.weaponEffectID = record.playerLoadInfo.weaponEffectID;
				playerLoadInfo.weaponEffectColor = record.playerLoadInfo.weaponEffectColor;
				playerLoadInfo.weaponEffectParam = record.playerLoadInfo.weaponEffectParam;
				playerLoadInfo.weaponSpAttackType = record.playerLoadInfo.weaponSpAttackType;
			}
			SetRenderPlayerModel(playerLoadInfo);
		}
	}

	protected void SetRenderPlayerModel(PlayerLoadInfo load_player_info)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		SetRenderPlayerModel(transRoot, UI.TEX_MODEL, load_player_info, record.animID, new Vector3(0f, -0.75f, 14f), new Vector3(0f, 180f, 0f), isVisualMode, delegate(PlayerLoader player_loader)
		{
			if (player_loader != null)
			{
				loader = player_loader;
			}
			if (loader != null && loader.animator != null)
			{
				if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
				{
					if (MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
					{
						loader.animator.Play("win_loop");
					}
				}
				else
				{
					PlayerAnimCtrl.Get(loader.animator, PlayerAnimCtrl.battleAnims[record.playerLoadInfo.weaponModelID / 1000], null, null, null);
				}
			}
		});
	}

	protected override void UpdateUserIDLabel()
	{
		if (!isSelfData)
		{
			bool flag = !record.isNPC;
			SetActive(transRoot, UI.OBJ_USER_ID_ROOT, flag);
			if (flag)
			{
				SetLabelText(transRoot, UI.LBL_USER_ID, record.charaInfo.code);
			}
		}
		else
		{
			SetLabelText(transRoot, UI.LBL_USER_ID, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code);
		}
	}

	public override int GetCharaSex()
	{
		return record.charaInfo.sex;
	}

	public virtual void SetupCommentText()
	{
		bool flag = !record.isNPC;
		SetActive(transRoot, UI.SPR_COMMENT, flag);
		if (flag)
		{
			SetLabelText(transRoot, UI.LBL_COMMENT, record.charaInfo.comment);
		}
	}

	public virtual void SetupFollowButton()
	{
		bool isNPC = record.isNPC;
		QuestResultUserCollection.ResultUserInfo userInfo = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserInfo(detailUserID);
		if (record.isSelf || isNPC || userInfo == null)
		{
			SetActive(transRoot, UI.BTN_FOLLOW, false);
			SetActive(transRoot, UI.BTN_UNFOLLOW, false);
			SetActive(transRoot, UI.OBJ_FOLLOW_ARROW_ROOT, false);
			SetActive(transRoot, UI.OBJ_BLACKLIST_ROOT, false);
		}
		else
		{
			bool flag = !userInfo.CanSendFollow;
			bool isFollower = userInfo.IsFollower;
			SetEvent(transRoot, UI.BTN_FOLLOW, "FOLLOW", 0);
			if (MonoBehaviourSingleton<FriendManager>.I.followNum == MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow && !flag)
			{
				SetActive(transRoot, UI.BTN_FOLLOW, true);
				SetActive(transRoot, UI.BTN_UNFOLLOW, false);
				SetEvent(transRoot, UI.BTN_FOLLOW, "INVALID_FOLLOW", 0);
			}
			else
			{
				bool flag2 = !record.isNPC;
				SetActive(transRoot, UI.BTN_FOLLOW, flag2 && !flag);
				SetActive(transRoot, UI.BTN_UNFOLLOW, flag2 && flag);
			}
			bool flag3 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(record.charaInfo.userId);
			SetActive(transRoot, UI.OBJ_BLACKLIST_ROOT, true);
			SetActive(transRoot, UI.BTN_BLACKLIST_IN, !flag3);
			SetActive(transRoot, UI.BTN_BLACKLIST_OUT, flag3);
			SetActive(transRoot, UI.SPR_FOLLOW_ARROW, !flag3 && flag);
			SetActive(transRoot, UI.SPR_FOLLOWER_ARROW, !flag3 && isFollower);
			SetActive(transRoot, UI.SPR_BLACKLIST_ICON, flag3);
		}
	}

	protected virtual void SetupLastLogin()
	{
		SetActive(transRoot, UI.OBJ_LAST_LOGIN, false);
	}

	public override void UpdateUI()
	{
		if (isSelfData)
		{
			localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(selfCharaEquipSetNo);
		}
		else
		{
			localEquipSet = MonoBehaviourSingleton<StatusManager>.I.CreateEquipSetData(record.charaInfo.equipSet);
		}
		OnUpdateFriendDetailUI();
	}

	protected void OnUpdateFriendDetailUI()
	{
		int num;
		int num2;
		int num3;
		int num4;
		if (!record.isSelf)
		{
			if (record.isNPC)
			{
				num = record.charaInfo.atk;
				num2 = record.charaInfo.def;
				num3 = record.charaInfo.hp;
			}
			else
			{
				EquipSetCalculator otherEquipSetCalculator;
				if (MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex == -1)
				{
					MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = 0;
					otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(0);
					otherEquipSetCalculator.SetEquipSet(record.charaInfo.equipSet, false);
				}
				else
				{
					otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex);
				}
				SimpleStatus finalStatus = otherEquipSetCalculator.GetFinalStatus(0, record.charaInfo.hp, record.charaInfo.atk, record.charaInfo.def);
				num = finalStatus.GetAttacksSum();
				num2 = finalStatus.GetDefencesSum();
				num3 = finalStatus.hp;
			}
			num4 = record.charaInfo.level;
			SetActive(transRoot, UI.OBJ_LEVEL_ROOT, !record.isNPC);
		}
		else
		{
			EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(selfCharaEquipSetNo);
			SimpleStatus finalStatus2 = equipSetCalculator.GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
			num = finalStatus2.GetAttacksSum();
			num2 = finalStatus2.GetDefencesSum();
			num3 = finalStatus2.hp;
			num4 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
		}
		SetLabelText(transRoot, UI.LBL_ATK, num.ToString());
		SetLabelText(transRoot, UI.LBL_DEF, num2.ToString());
		SetLabelText(transRoot, UI.LBL_HP, num3.ToString());
		SetLabelText(transRoot, UI.LBL_LEVEL, num4.ToString());
		SetupInfo();
		UpdateEquipIcon(null);
		SetActive(transRoot, UI.BTN_MAGI, showMagiButton);
		CreateDegree();
	}

	protected void SetupInfo()
	{
		SetActive(transRoot, UI.OBJ_FRIEND_INFO_ROOT, IsFriendInfo);
		SetActive(transRoot, UI.OBJ_CHANGE_EQUIP_INFO_ROOT, !IsFriendInfo);
		if (IsFriendInfo)
		{
			UpdateUserIDLabel();
			CharaInfo.ClanInfo clanInfo = record.charaInfo.clanInfo;
			if (clanInfo == null)
			{
				clanInfo = new CharaInfo.ClanInfo();
				clanInfo.clanId = -1;
				clanInfo.tag = string.Empty;
			}
			bool isSameTeam = clanInfo.clanId > -1 && MonoBehaviourSingleton<GuildManager>.I.guildData != null && clanInfo.clanId == MonoBehaviourSingleton<GuildManager>.I.guildData.clanId;
			SetSupportEncoding(transRoot, UI.LBL_NAME, true);
			SetLabelText(transRoot, UI.LBL_NAME, Utility.GetNameWithColoredClanTag(clanInfo.tag, record.charaInfo.name, record.id == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, isSameTeam));
			SetupCommentText();
			SetupLastLogin();
			SetupFollowButton();
		}
	}

	protected override void UpdateEquipIcon(List<CharaInfo.EquipItem> equip_set_info)
	{
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		SetActive(transRoot, UI.LBL_CHANGE_MODE, isVisualMode);
		int i = 0;
		for (int num = 7; i < num; i++)
		{
			SetEvent(FindCtrl(transRoot, icons[i]), "EMPTY", 0);
			SetEvent(FindCtrl(transRoot, icons_btn[i]), "EMPTY", 0);
			SetLabelText(FindCtrl(transRoot, icons_level[i]), string.Empty);
		}
		bool flag = isVisualMode;
		bool flag2 = isVisualMode;
		bool flag3 = isVisualMode;
		bool flag4 = isVisualMode;
		int j = 0;
		for (int num2 = localEquipSet.item.Length; j < num2; j++)
		{
			ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
			RARITY_TYPE? nullable = null;
			ELEMENT_TYPE eLEMENT_TYPE = ELEMENT_TYPE.MAX;
			int num3 = -1;
			EquipItemInfo equipItemInfo = localEquipSet.item[j];
			EquipItemTable.EquipItemData equipItemData = null;
			if (equipItemInfo != null)
			{
				switch (equipItemInfo.tableData.type)
				{
				case EQUIPMENT_TYPE.ARMOR:
					flag2 = false;
					break;
				case EQUIPMENT_TYPE.HELM:
					flag = false;
					break;
				case EQUIPMENT_TYPE.ARM:
					flag3 = false;
					break;
				case EQUIPMENT_TYPE.LEG:
					flag4 = false;
					break;
				}
				equipItemData = ((!isVisualMode) ? Singleton<EquipItemTable>.I.GetEquipItemData(equipItemInfo.tableID) : GetVisualModeTargetTable(equipItemInfo.tableData.id, equipItemInfo.tableData.type, record.charaInfo));
			}
			if (isVisualMode)
			{
				if (equipItemData != null)
				{
					num3 = equipItemData.GetIconID(GetCharaSex());
					SetActive(FindCtrl(transRoot, icons_level[j]), false);
				}
			}
			else if (equipItemInfo != null && equipItemInfo.tableID != 0)
			{
				num3 = equipItemData.GetIconID(GetCharaSex());
				SetActive(FindCtrl(transRoot, icons_level[j]), true);
				string text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1u), equipItemInfo.level.ToString());
				SetLabelText(FindCtrl(transRoot, icons_level[j]), text);
			}
			Transform parent = FindCtrl(transRoot, icons[j]);
			ItemIcon itemIcon = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, GetCharaSex(), parent, null, -1, "EQUIP", j, false, -1, false, null, false, false);
			SetLongTouch(itemIcon.transform, "DETAIL", j);
			SetEvent(FindCtrl(transRoot, icons_btn[j]), "DETAIL", j);
			SetEvent(itemIcon.transform, "DETAIL", j);
			itemIcon.get_gameObject().SetActive(num3 != -1);
			if (num3 != -1)
			{
				itemIcon.SetEquipExtInvertedColor(equipItemInfo, base.GetComponent<UILabel>(transRoot, (Enum)icons_level[j]));
			}
		}
		if (flag && record.charaInfo.hId != 0)
		{
			int index = 4;
			int hId = record.charaInfo.hId;
			EQUIPMENT_TYPE e_type = EQUIPMENT_TYPE.HELM;
			CharaInfo charaInfo = record.charaInfo;
			SetVisualModeIcon(index, hId, e_type, charaInfo);
		}
		if (flag2 && record.charaInfo.aId != 0)
		{
			int index2 = 3;
			int aId = record.charaInfo.aId;
			EQUIPMENT_TYPE e_type2 = EQUIPMENT_TYPE.ARMOR;
			CharaInfo charaInfo2 = record.charaInfo;
			SetVisualModeIcon(index2, aId, e_type2, charaInfo2);
		}
		if (flag3 && record.charaInfo.rId != 0)
		{
			int index3 = 5;
			int rId = record.charaInfo.rId;
			EQUIPMENT_TYPE e_type3 = EQUIPMENT_TYPE.ARM;
			CharaInfo charaInfo3 = record.charaInfo;
			SetVisualModeIcon(index3, rId, e_type3, charaInfo3);
		}
		if (flag4 && record.charaInfo.lId != 0)
		{
			int index4 = 6;
			int lId = record.charaInfo.lId;
			EQUIPMENT_TYPE e_type4 = EQUIPMENT_TYPE.LEG;
			CharaInfo charaInfo4 = record.charaInfo;
			SetVisualModeIcon(index4, lId, e_type4, charaInfo4);
		}
	}

	protected override void OnQuery_DETAIL()
	{
		if (isVisualMode)
		{
			GameSection.ChangeEvent("VISUAL_DETAIL", null);
			OnQuery_VISUAL_DETAIL();
		}
		else
		{
			int num = (int)GameSection.GetEventData();
			if (localEquipSet.item[num] == null)
			{
				GameSection.StopEvent();
			}
			else if (isSelfData)
			{
				GameSection.SetEventData(CreateSelfEventData(num));
			}
			else
			{
				GameSection.SetEventData(new object[4]
				{
					ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
					GetEquipSetAttachSkillListData(record.charaInfo.equipSet)[num],
					record.charaInfo.sex,
					record.charaInfo.faceId
				});
			}
		}
	}

	protected object[] CreateSelfEventData(int index)
	{
		return new object[4]
		{
			ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
			GetEquipSetAttachSkillListData(selfCharaEquipSetNo)[index],
			record.charaInfo.sex,
			record.charaInfo.faceId
		};
	}

	protected override void OnQuery_SKILL_LIST()
	{
		if (isSelfData)
		{
			GameSection.SetEventData(new object[4]
			{
				ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
				GetEquipSetAttachSkillListData(selfCharaEquipSetNo),
				true,
				record.charaInfo.sex
			});
		}
		else
		{
			GameSection.SetEventData(new object[4]
			{
				ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT,
				GetEquipSetAttachSkillListData(record.charaInfo.equipSet),
				true,
				record.charaInfo.sex
			});
		}
	}

	protected override void OnQuery_ABILITY()
	{
		List<CharaInfo.EquipItem> chara_list_equip_data = (!isSelfData) ? record.charaInfo.equipSet : null;
		GameSection.SetEventData(new object[3]
		{
			localEquipSet,
			MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(localEquipSet, null),
			new EquipSetDetailStatusAndAbilityTable.BaseStatus(record.charaInfo.atk, record.charaInfo.def, record.charaInfo.hp, chara_list_equip_data)
		});
	}

	protected override void OnQuery_STATUS()
	{
		List<CharaInfo.EquipItem> chara_list_equip_data = (!isSelfData) ? record.charaInfo.equipSet : null;
		GameSection.SetEventData(new object[3]
		{
			localEquipSet,
			MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAbility(localEquipSet, null),
			new EquipSetDetailStatusAndAbilityTable.BaseStatus(record.charaInfo.atk, record.charaInfo.def, record.charaInfo.hp, chara_list_equip_data)
		});
	}

	protected override void OnQuery_FOLLOW()
	{
		GameSection.SetEventData(new object[1]
		{
			record.charaInfo.name
		});
		List<int> list = new List<int>();
		list.Add(record.charaInfo.userId);
		if (isQuestResult)
		{
			SendFollow(list, delegate
			{
				if (MonoBehaviourSingleton<CoopApp>.IsValid())
				{
					CoopApp.UpdateField(null);
				}
			});
		}
		else
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<PartyManager>.I.SendFollowAgency(list, delegate(bool is_success)
			{
				if (isQuestResult && is_success)
				{
					MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(record.charaInfo.userId, true);
				}
				GameSection.ResumeEvent(is_success, null);
			});
		}
	}

	protected override void OnQuery_UNFOLLOW()
	{
		GameSection.SetEventData(new object[1]
		{
			record.charaInfo.name
		});
	}

	protected virtual void OnQuery_QuestResultFriendUnFollow_YES()
	{
		GameSection.SetEventData(new object[1]
		{
			record.charaInfo.name
		});
		if (isQuestResult)
		{
			SendUnFollow(record.charaInfo.userId, delegate
			{
			});
		}
		else
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<PartyManager>.I.SendUnFollowAgency(record.charaInfo.userId, delegate(bool is_success)
			{
				if (isQuestResult && is_success)
				{
					MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(record.charaInfo.userId, false);
				}
				GameSection.ResumeEvent(is_success, null);
			});
		}
	}

	protected override void OnQuery_BLACK_LIST_IN()
	{
		GameSection.SetEventData(new object[1]
		{
			record.charaInfo.name
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<BlackListManager>.I.SendAdd(record.charaInfo.userId, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	protected override void OnQuery_BLACK_LIST_OUT()
	{
		GameSection.SetEventData(new object[1]
		{
			record.charaInfo.name
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<BlackListManager>.I.SendDelete(record.charaInfo.userId, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	protected override void OnQuery_CHANGE_MODE()
	{
		reloadModel = true;
		base.OnQuery_CHANGE_MODE();
	}

	protected override void OnQuery_SECTION_BACK()
	{
		MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = -1;
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
	}
}
