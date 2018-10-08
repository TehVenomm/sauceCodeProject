using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameDefine
{
	public const int NAME_MAX_LENGTH = 14;

	public const string SYSTEM_INIT_NAME = "/colopl_rob";

	public const string GG_SYSTEM_INIT_NAME = "DPRO Hunter ";

	public const int COMMENT_MAX_LENGTH = 30;

	public const string SAVE_LAST_NEWS_ID = "LastNewsID";

	public const float CHARA_DRAG_ROTATE_Y_MAX_ANGLE = 35f;

	public const int REGIST_ACCOUNT_ADDRESS_MIN_LENGTH = 6;

	public const int REGIST_ACCOUNT_ADDRESS_MAX_LENGTH = 255;

	public const int REGIST_ACCOUNT_SECRET_QUESTION_MAX_LENGTH = 45;

	public const int REGIST_ACCOUNT_PASS_MIN_LENGTH = 8;

	public const int REGIST_ACCOUNT_PASS_MAX_LENGTH = 255;

	public const string OPEN_APP_INVITE_PARTY = "im";

	public const string OPEN_APP_MUTUAL_FOLLOW = "fc";

	public const string OPEN_APP_INVITE_LOUNGE = "il";

	public const string OPEN_APP_INVITE_PROMOTION = "fp";

	public const string OPEN_APP_INVITE_CLAN = "ic";

	public const string OPEN_NOTIFICATION_URL = "ur";

	public const string OPEN_GACHA = "gc";

	public const int OPINION_MAX_LENGTH = 511;

	public const int EQUIP_SET_NAME_MAX_LENGTH = 13;

	public const string SYSTEM_INIT_EQUIP_SET_NAME = "装備セット";

	public const string LIMITED_WORD = "Valid till";

	public const int SHOW_QUEST_REMAIN_LIMIT_DAY = 5;

	public const int FAVORITE_STAMP_NUM = 10;

	public const string FAVORITE_STAMP_KEY = "FAVORITE_STAMP_KEY";

	public const string GACHA_BUTTON_NORAML1 = "BTN_GACHA_NORMAL1";

	public const string GACHA_BUTTON_PURCHASE_STEPUP = "BTN_GACHA_STEP10_Pay";

	public const string GACHA_BUTTON_RESULT_SUFFIX = "_RESULT";

	public const string GACHA_BUTTON_VERSION = "_VER2";

	public const string SHOP_BUTTON_NORMAL1 = "BTN_SHOP_NORMAL1";

	public const int QUEST_SHOW_NUM = 10;

	public const int RUSH_EVENT_ID = 10004400;

	public const string GACHA_SEARCH_RAIRTY_KEY = "GACHA_SEARCH_RAIRTY_KEY";

	public const string GACHA_SEARCH_ELEMENT_KEY = "GACHA_SEARCH_ELEMENT_KEY";

	public const string GACHA_SEARCH_SPECIES_KEY = "GACHA_SEARCH_SPECIES_KEY";

	public const string GACHA_SEARCH_LEVEL_MIN_KEY = "GACHA_SEARCH_LEVEL_MIN_KEY";

	public const string GACHA_SEARCH_LEVEL_MAX_KEY = "GACHA_SEARCH_LEVEL_MAX_KEY";

	public const string MY_GACHA_SEARCH_RAIRTY_KEY = "MY_GACHA_SEARCH_RAIRTY_KEY";

	public const string MY_GACHA_SEARCH_ELEMENT_KEY = "MY_GACHA_SEARCH_ELEMENT_KEY";

	public const string MY_GACHA_SEARCH_SPECIES_KEY = "MY_GACHA_SEARCH_SPECIES_KEY";

	public const string MY_GACHA_SEARCH_LEVEL_MIN_KEY = "MY_GACHA_SEARCH_LEVEL_MIN_KEY";

	public const string MY_GACHA_SEARCH_LEVEL_MAX_KEY = "MY_GACHA_SEARCH_LEVEL_MAX_KEY";

	public const string CHALLENGE_SEARCH_RAIRTY_KEY = "CHALLENGE_SEARCH_RAIRTY_KEY";

	public const string CHALLENGE_SEARCH_ELEMENT_KEY = "CHALLENGE_SEARCH_ELEMENT_KEY";

	public const string CHALLENGE_SEARCH_SPECIES_KEY = "CHALLENGE_SEARCH_SPECIES_KEY";

	public const string CHALLENGE_SEARCH_USER_LEVEL_KEY = "CHALLENGE_SEARCH_USER_LEVEL_KEY";

	public const string CHALLENGE_SEARCH_ENEMY_LEVEL_KEY = "CHALLENGE_SEARCH_ENEMY_LEVEL_KEY";

	public const string LOUNGE_CREATE_STAMP_KEY = "LOUNGE_CREATE_STAMP_KEY";

	public const string LOUNGE_CREATE_LEVEL_MIN_KEY = "LOUNGE_CREATE_LEVEL_MIN_KEY";

	public const string LOUNGE_CREATE_LEVEL_MAX_KEY = "LOUNGE_CREATE_LEVEL_MAX_KEY";

	public const string LOUNGE_CREATE_CAPACITY_KEY = "LOUNGE_CREATE_CAPACITY_KEY";

	public const string LOUNGE_CREATE_LABEL_KEY = "LOUNGE_CREATE_LABEL_KEY";

	public const string LOUNGE_CREATE_LOCK_KEY = "LOUNGE_CREATE_LOCK_KEY";

	public const string LOUNGE_CREATE_NAME_KEY = "LOUNGE_CREATE_NAME_KEY";

	public const string LOUNGE_SEARCH_LABEL_KEY = "LOUNGE_SEARCH_LABEL_KEY";

	public const string LOUNGE_SEARCH_NAME_KEY = "LOUNGE_SEARCH_NAME_KEY";

	public const string RUSH_SEARCH_MAX_QUEAT_ID_KEY = "RUSH_SEARCH_MAX_QUESTID_KEY";

	public const string RUSH_SEARCH_MIN_QUEST_ID_KEY = "RUSH_SEARCH_MIN_QUESTID_KEY";

	public const string GATHER_ITEM_KEY_PREFIX = "gik_";

	public const string NORMAL_HOME_THEME_NAME = "NORMAL";

	public const int LOUNGE_CAPACITY = 8;

	public const int LOUNGE_MIN_CAPACITY = 2;

	public const int LOUNGE_NEED_LEVEL = 15;

	public const int LOUNGE_NAME_MAX_LENGTH = 16;

	public const int GUILD_NAME_MAX_LENGTH = 30;

	public const int GUILD_TAG_MAX_LENGTH = 4;

	public const int GUILD_EXPIRED_TIME_MIN = 15;

	public const int GUILD_EXPIRED_SHOWLAST_TIME_MIN = 3;

	public const string CLAN_ID = "CLAN_ID";

	public const int GACHA_TICKET_SHOW_NUM = 10;

	public const int ELEMENT_NUM_WITH_NORMAL = 7;

	public const string SOUL_ENERGY_EFFECT_NAME = "ef_btl_soul_energy_01";

	public const string THS_SOUL_BOOST_START_EFFECT_NAME = "ef_btl_wsk2_longsword_02_01";

	public const string THS_SOUL_BOOST_LOOP_EFFECT_NAME = "ef_btl_wsk2_longsword_03_01";

	public const string BURST_BOOST_LOOP_EFFECT_NAME = "ef_btl_wsk3_sword_aura_01";

	public const string PAIR_SWORDS_HEAT_MODE_TRAIL_EFFECT_NAME = "ef_btl_wsk_twinsword_01_03";

	public const string PAIR_SWORDS_HEAT_MODE_ATKUP_LEVELUP_EFFECT_NAME = "ef_btl_wsk_twinsword_01_04";

	public const string PAIR_SWORDS_HEAT_MODE_ATKUP_LEVELMAX_EFFECT_NAME = "ef_btl_wsk_twinsword_01_05";

	public const int ONE_HAND_SWORD_JUSTGUARD_SEID = 10000042;

	public const string ONE_HAND_SWORD_JUSTGUARD_EFFECT_NAME = "ef_btl_wsk_sword_01_01";

	public const string ONE_HAND_SWORD_CHARGE_EFFECT_NAME = "ef_btl_pl05_attack_01";

	public const string ONE_HAND_SWORD_MAXCHARGE_EFFECT_NAME = "ef_btl_wsk_sword_01_04";

	public const string ARROW_BUFF_SHADOWSEALING_EFFECT_NAME = "ef_btl_wsk_bow_01_02";

	public const string ARROW_SOUL_TARGET_EFFECT_NAME = "ef_btl_wsk2_bow_lock_02";

	public const string ARROW_SOUL_RIGHT_HAND_EFFECT_NAME = "ef_btl_wsk2_bow_01_01";

	public const string SPEAR_JUMP_CURSOR_EFFECT_NAME = "ef_btl_target_e_01";

	public const string SPEAR_JUMP_MAX_EFFECT_NAME = "ef_btl_wsk_spear_01_03";

	public const string SPEAR_SOUL_SP_BLADE_EFFECT_NAME = "ef_btl_wsk2_spear_02_02";

	public const string COMMON_CHARGE_EFFECT = "ef_btl_wsk_charge_loop_01";

	public const string COMMON_MAXCHARGE_EFFECT = "ef_btl_wsk_charge_end_01";

	public const string COMMON_MAXCHARGE_LOOP_EFFECT = "ef_btl_wsk_charge_loop_02";

	public const string COMMON_HEAT_MODE_AURA_EFFECT_NAME = "ef_btl_wsk_twinsword_01_02";

	public const string RESURRECTION_RECEIVE_EFFECT_NAME = "ef_btl_sk_heal_04_03";

	public const string HEAL_EFFECT_NAME_BASIS = "ef_btl_sk_heal_01";

	public const string HEAL_EFFECT_NAME_ABSORB = "ef_btl_sk_heal_02";

	public const string HEAL_EFFECT_NAME_HIT_ABSORB = "ef_btl_sk_drain_01_01";

	public const string HIT_ABSORB_EFFECT_NAME = "ef_btl_sk_drain_01_01";

	public const int UNKNOWN_ENEMY_ICON_ID = 10999;

	public const int SKILL_GROW_MATERIAL_NUM_MAX = 10;

	public const int SKILL_EXCEED_MATERIAL_NUM_MAX = 10;

	public const float EXPLORE_HOST_DC_TIME = 30f;

	public const int ARENA_ALLOWED_LEVEL = 50;

	public const ARENA_RANK ARENA_TIME_ATTACK_RANK = ARENA_RANK.S;

	public const int ARENA_DISABLE_BANNER_ID = 10012201;

	public const int ARENA_DISABLE_BG_ID = 10012200;

	public const string FCM_REGISTED = "fcm_registed";

	public const string SHARE_SCREENSHOT = "share_screenshot";

	public const string COUNTDOWN_SHOWED_REMAIN = "COUNTDOWN_SHOWED_REMAIN";

	public const string INIT_SE_VOLUME = "INIT_SE_VOLUME";

	public const string LOCAL_PUSH_GUILD_REQUEST_KEY = "LOCAL_PUSH_GUILD_REQUEST_KEY";

	public const int AREA_REGION_MAX = 100;

	public const int SECOND_WORLD_REGION_ID = 9;

	public const int ENABLE_HARD_LEVEL = 150;

	public const string IS_POP_FOOTER_MENU = "IS_POP_FOOTER_MENU";

	public const string SHOP_TOP_ADVERTISEMENT = "SHOP_TOP_ADVERTISEMENT";

	public const string IS_SHOWED_RAID_BOSS_DIRECTION = "IS_SHOWED_RAID_BOSS_DIRECTION";

	public const float PERCENT = 0.01f;

	public const int DEFAULT_EXCEEDABLE_NUM = 4;

	public const string TUTORIAL_WEAPON_PREF_ARMOR = "Tut_Armor";

	public const string TUTORIAL_WEAPON_PREF_ARM = "Tut_Arm";

	public const string TUTORIAL_WEAPON_PREF_HEAD = "Tut_Head";

	public const string TUTORIAL_WEAPON_PREF_LEG = "Tut_Leg";

	public const string TUTORIAL_WEAPON_PREF_WEAPON = "Tut_Weapon";

	public const string TUTORIAL_WEAPON_PREF_SEX = "Tut_Sex";

	public const string TUTORIAL_WEAPON_PREF_WEAPON_TYPE = "Tut_Weapon_Type";

	public const string TUTORIAL_PLAYER_NAME = "Tut_Name";

	public static readonly int[] NOTIFY_QUEST_REMAIN_DAY = new int[3]
	{
		1,
		3,
		7
	};

	public static readonly List<int> FAVORITE_STAMP_DEFAULT = new List<int>(10)
	{
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		10
	};

	public static int GACHA_VIEW_PROBABILITY = 12;

	public static int POINT_SHOP_MAX_BUY_LIMIT = 99;

	public static int DEGREE_PART_COUNT = 4;

	public static int DEGREE_WORD_CHANGE_LIST_COUNT = 20;

	public static int DEGREE_FRAME_CHANGE_LIST_COUNT = 10;

	public static int DEGREE_FRIEND_LIST_HEIGHT = 130;

	public static int POINT_SHOP_LIST_COUNT = 10;

	public static bool ACTIVE_DEGREE => MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds != null && MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds.Count == DEGREE_PART_COUNT;

	public static Vector3 GetCharaRotateVector(InputManager.TouchInfo touch_info)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return GetCharaRotateVector(touch_info.move);
	}

	public static Vector3 GetCharaRotateVector(Vector2 move)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		Vector3 result = default(Vector3);
		result._002Ector(0f, (0f - move.x) * 0.5f, 0f);
		if (Mathf.Abs(result.y) > 35f)
		{
			result.y = Mathf.Sign(result.y) * 35f;
		}
		return result;
	}

	public static MAIN_SCENE SceneNameToEnum(string scene_name)
	{
		switch (scene_name)
		{
		case "HomeScene":
			return MAIN_SCENE.HOME;
		case "LoungeScene":
			return MAIN_SCENE.LOUNGE;
		case "GuildScene":
			return MAIN_SCENE.GUILD;
		case "SmithScene":
		case "StatusScene":
			return MAIN_SCENE.STUDIO;
		case "ShopScene":
			return MAIN_SCENE.SHOP;
		case "GatherScene":
			return MAIN_SCENE.GATHER;
		case "FriendScene":
			return MAIN_SCENE.FRIEND;
		default:
			return MAIN_SCENE.MAX;
		}
	}

	public static int ToRarityExpressionID(this RARITY_TYPE type)
	{
		switch (type)
		{
		default:
			return 0;
		case RARITY_TYPE.A:
			return 1;
		case RARITY_TYPE.S:
		case RARITY_TYPE.SS:
		case RARITY_TYPE.SSS:
			return 2;
		}
	}

	public static int ToRarityExpressionID2(this RARITY_TYPE type)
	{
		switch (type)
		{
		default:
			return 0;
		case RARITY_TYPE.A:
			return 1;
		case RARITY_TYPE.S:
		case RARITY_TYPE.SS:
			return 2;
		case RARITY_TYPE.SSS:
			return 3;
		}
	}

	public static bool IsRare(RARITY_TYPE rarity)
	{
		return rarity >= RARITY_TYPE.B;
	}

	public static bool IsRequiredAlertByRarity(RARITY_TYPE _rarity)
	{
		return _rarity >= RARITY_TYPE.SS;
	}

	public static bool IsRareLithograph(RARITY_TYPE rarity)
	{
		return rarity >= RARITY_TYPE.SS;
	}

	public static bool IsFieldBossTreasureBox(int typeInt)
	{
		if (Enum.IsDefined(typeof(TREASURE_BOX_TYPE), typeInt))
		{
			switch (typeInt)
			{
			case 1:
			case 2:
			case 3:
			case 5:
			case 6:
			case 7:
				return true;
			default:
				return false;
			}
		}
		return false;
	}
}
