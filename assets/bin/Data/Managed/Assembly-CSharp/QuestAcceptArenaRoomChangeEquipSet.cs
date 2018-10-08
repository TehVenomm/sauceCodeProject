using System;

public class QuestAcceptArenaRoomChangeEquipSet : QuestOffLineChangeEquipSet
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
		LBL_LIMIT,
		OBJ_OK,
		OBJ_NG
	}

	private ArenaTable.ArenaData arenaData;

	public override void Initialize()
	{
		base.Initialize();
		arenaData = Singleton<ArenaTable>.I.GetArenaData(MonoBehaviourSingleton<QuestManager>.I.currentArenaId);
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		UpdateLimit();
	}

	private void UpdateLimit()
	{
		SetLabelText((Enum)UI.LBL_LIMIT, QuestUtility.GetLimitText(arenaData));
		bool flag = QuestUtility.JudgeLimit(arenaData, localEquipSet);
		SetActive((Enum)UI.OBJ_OK, flag);
		SetActive((Enum)UI.OBJ_NG, !flag);
	}
}
