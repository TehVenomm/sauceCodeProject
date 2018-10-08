using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultTop : GameSection
{
	private enum UI
	{
		LBL_QUEST_NAME,
		LBL_PLAYER_LV,
		LBL_PLAYER_LVUP,
		SPR_LEVELUP,
		LBL_LVUP_NUM,
		OBJ_GET_EXP_ROOT,
		OBJ_MISSION_ROOT,
		OBJ_MISSION_NEW_CLEAR_ROOT,
		OBJ_TREASURE_ROOT,
		STR_TITLE_EXP,
		STR_TITLE_MISSION,
		STR_TITLE_REWARD,
		OBJ_EXP_REWARD_FRAME,
		LBL_EXP,
		SPR_GAUGE_UPPER,
		PBR_EXP,
		OBJ_RESULT_EXP_GAUGE_CTRL,
		OBJ_MISSION_INFO_FRAME,
		OBJ_MISSION_01,
		OBJ_MISSION_02,
		OBJ_MISSION_03,
		LBL_MISSION_NAME_01,
		LBL_MISSION_NAME_02,
		LBL_MISSION_NAME_03,
		SPR_CROWN_01,
		SPR_CROWN_02,
		SPR_CROWN_03,
		SPR_CLEARED_CROWN_01,
		SPR_CLEARED_CROWN_02,
		SPR_CLEARED_CROWN_03,
		STR_EMPTY_MISSION,
		GET_ITEM,
		GET_ITEM_2,
		OBJ_QUEST_REWARD_FRAME,
		LBL_REWARD_GOLD,
		TBL_ITEM,
		OBJ_SCROLL_VIEW,
		OBJ_SCROLL_VIEW_2,
		GRD_DROP_ITEM,
		GRD_DROP_ITEM_2,
		BTN_NEXT,
		BTN_SKIP_FULL_SCREEN,
		BTN_SKIP_IN_SCROLL,
		BTN_SKIP_IN_SCROLL_2,
		PNL_MATERIAL_INFO,
		PNL_MATERIAL_INFO_2,
		OBJ_TREASURE_ROOT_NON_MISSION,
		OBJ_POINT_SHOP_RESULT_ROOT,
		OBJ_NORMAL_POINT_SHOP_ROOT,
		OBJ_EVENT_POINT_SHOP_ROOT,
		LBL_NORMAL_GET_POINT_SHOP,
		LBL_NORMAL_TOTAL_POINT_SHOP,
		TEX_NORMAL_POINT_SHOP_ICON,
		LBL_EVENT_GET_POINT_SHOP,
		LBL_EVENT_TOTAL_POINT_SHOP,
		TEX_EVENT_POINT_SHOP_ICON,
		LBL_GUILD_REQUEST_GET_POINT,
		OBJ_TITLE,
		OBJ_WAVE,
		LBL_WAVE,
		OBJ_TIME,
		LBL_TIME,
		OBJ_MONEY,
		OBJ_COIN,
		OBJ_ARRIVAL_EFFECT_ROOT,
		OBJ_ARRIVAL_EFFECT,
		OBJ_ARRIVAL_BONUS,
		GRD_ARRIVAL_ITEM_ICON,
		STR_REWARD_TITLE,
		SPR_WAVE_01,
		SPR_WAVE_10,
		SPR_WAVE_100,
		TBL_DROP_ITEM,
		LBL_DROP_ITEM_WAVE,
		STR_TITLE_WAVE,
		STR_TITLE_TIME,
		LBL_EXPLORE_GET_POINT,
		LBL_EXPLORE_TOTAL_POINT,
		SPR_TITLE,
		OBJ_EXP,
		OBJ_REMAIN_TIME,
		OBJ_CLEAR_TIME,
		STR_CLEAR_TIME_NAME,
		LBL_CLEAR_TIME,
		OBJ_BEFORE_TIME,
		SPR_BEFORE_TIME_NAME,
		LBL_BEFORE_TIME,
		SPR_BESTSCORE,
		OBJ_CLEAR_EFFECT_ROOT,
		OBJ_CLEAR_EFFECT,
		OBJ_RANK_UP_ROOT,
		OBJ_RANK_UP,
		TEX_RANK_PRE,
		TEX_RANK_NEW,
		OBJ_PARTICLE,
		OBJ_CONGRATULATIONS_ROOT,
		OBJ_CONGRATULATIONS,
		OBJ_CONGRATULATIONS_PARTICLE,
		LBL_BOSS_NAME,
		TBL_GUILD_REQUEST_RESULT,
		OBJ_BONUS_POINT_SHOP,
		TXT_BONUS_POINT_ICON,
		LBL_BONUS_POINT_NUM,
		TEX_MISSION_COIN_01,
		TEX_MISSION_COIN_02,
		TEX_MISSION_COIN_03,
		SPR_CROWN01_OFF,
		SPR_CROWN02_OFF,
		SPR_CROWN03_OFF,
		BTN_NEXT_ALL,
		BTN_END_HUNT_CENTER,
		BTN_END_HUNT_LEFT,
		BTN_REPEAT_HUNT,
		LBL_BTN_REPEAT_HUNT,
		LBL_WAIT_FOR_HOST
	}

	private enum AUDIO
	{
		COUNTUP = 40000012,
		MISSION = 40000013,
		DROPITEM_BREAK = 40000014,
		DROPITEM = 40000015,
		MONEY = 40000016,
		LEVELUP = 40000017,
		ADVENT = 40000026,
		MONEY_WH = 40000027,
		ACHIEVEMENT = 40000028,
		GET_REWARD = 40000155,
		RESULT = 40000049,
		TITLE_LOGO = 40000227,
		CATEGORY = 40000228,
		POINTUP = 40000229,
		POINTREWARD = 40000230,
		START_BONUS_TIME = 40000268
	}

	protected enum RESULT_ANIM_STATE
	{
		WAIT,
		EXP_GAUGE,
		LVUP,
		MISSION,
		MISSION_REWARD,
		FOLLOW_BONUS,
		TREASURE,
		END,
		FIRST_CLEAR_REWARD
	}

	protected const float ANIM_INDEX_STEP_TIME = 0.4f;

	private const float ICON_SCROLL_VALUE = -1.38f;

	private const float ICON_SCROLL_VALUE_2 = -1.5f;

	protected const int DROP_ITEM_LIST_NUM_X = 5;

	protected const int DROP_ITEM_LIST_MIN_Y = 1;

	protected const int DROP_ITEM_LIST_MIN = 5;

	protected const string NORMAL_DROP_EFF_NAME = "ef_ui_dropitem_silver_01";

	protected const string RARE_DROP_EFF_NAME = "ef_ui_dropitem_gold_01";

	protected const string BREAK_DROP_EFF_NAME = "ef_ui_dropitem_red_01";

	protected SortCompareData[] dropItemIconData;

	private UI dropItemGRD = UI.GRD_DROP_ITEM;

	private UI dropItemSCR = UI.OBJ_SCROLL_VIEW;

	protected bool isVictory;

	protected List<PointShopResultData> pointShopResultData;

	protected PointShopResultData missionPointData;

	protected bool is_open_get_rare_item;

	protected int dropItemNum;

	protected int dropLineNum;

	protected float animTimer;

	private int animIndex = -1;

	protected bool animationEnd;

	private bool isValidMission;

	protected bool startDropDirection;

	private string lvupTextFormat = string.Empty;

	private int[] missionNewClear;

	private bool isValidMissionNewClearAnim;

	protected float animScrollValue = -1.38f;

	protected QuestCompleteReward dropReward;

	protected QuestCompleteReward eventReward;

	protected List<string> eventRewardTitles;

	private PointEventCurrentData exploreResultData;

	private int guildPoint;

	protected RESULT_ANIM_STATE animState;

	private ResultExpGaugeCtrl expGauge;

	protected Action dropSellCallback;

	protected Action pointEventCallback;

	private QuestCompleteReward missionReward;

	private QuestCompleteReward missionCompleteReward;

	private bool isOpenedMissionClearDialog;

	private Action missionClearRewardCallback;

	protected QuestCompleteReward followReward;

	protected Action followBonusCallback;

	protected Action eventRewardCallback;

	protected int eventRewardIndex;

	protected List<QuestCompleteReward> eventRewardList;

	protected Action firstClearRewardCallback;

	private bool canEnterParty;

	public static bool isNeedOpenDropSellDialog
	{
		get
		{
			if (MonoBehaviourSingleton<QuestManager>.I.compData == null || MonoBehaviourSingleton<QuestManager>.I.compData.reward == null)
			{
				return false;
			}
			List<QuestCompleteReward.SellItem> sell = MonoBehaviourSingleton<QuestManager>.I.compData.reward.sell;
			if (sell.Count <= 0)
			{
				return false;
			}
			return true;
		}
	}

	public static bool isNeedPointResult
	{
		get
		{
			if (MonoBehaviourSingleton<QuestManager>.I.compData == null || MonoBehaviourSingleton<QuestManager>.I.compData.pointEvent == null || MonoBehaviourSingleton<QuestManager>.I.compData.pointEvent.Count == 0)
			{
				return false;
			}
			return true;
		}
	}

	private bool isNeedOpenMissionClearDialog
	{
		get
		{
			if (MonoBehaviourSingleton<QuestManager>.I.compData == null || MonoBehaviourSingleton<QuestManager>.I.compData.reward == null)
			{
				return false;
			}
			bool result = false;
			if (missionNewClear != null && missionNewClear.Length > 0)
			{
				result = (Array.FindIndex(missionNewClear, (int flag) => flag > 0) != -1);
			}
			if (missionPointData != null)
			{
				result = true;
			}
			return result;
		}
	}

	public static bool isNeedOpenFollowBonusDialog
	{
		get
		{
			if (MonoBehaviourSingleton<QuestManager>.I.compData == null || MonoBehaviourSingleton<QuestManager>.I.compData.reward == null)
			{
				return false;
			}
			bool result = false;
			List<int> userIdList = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserIdList(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
			for (int i = 0; i < userIdList.Count; i++)
			{
				if (userIdList[i] != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
				{
					QuestResultUserCollection.ResultUserInfo userInfo = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserInfo(userIdList[i]);
					bool flag = !userInfo.CanSendFollow;
					bool isFollower = userInfo.IsFollower;
					if (flag && isFollower)
					{
						result = true;
					}
				}
			}
			return result;
		}
	}

	protected bool isNeedOpenFirstClearDialog
	{
		get
		{
			if (MonoBehaviourSingleton<QuestManager>.I.compData == null || MonoBehaviourSingleton<QuestManager>.I.compData.reward == null)
			{
				return false;
			}
			bool result = false;
			QuestCompleteReward first = MonoBehaviourSingleton<QuestManager>.I.compData.reward.first;
			if (first.money > 0 || first.crystal > 0 || first.exp > 0 || first.item.Count > 0 || first.equipItem.Count > 0 || first.skillItem.Count > 0 || first.accessoryItem.Count > 0)
			{
				result = true;
			}
			return result;
		}
	}

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		yield return (object)new WaitForEndOfFrame();
		yield return (object)MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(true);
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		InitReward();
		LoadingQueue load_queue = new LoadingQueue(this);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_dropitem_silver_01");
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_dropitem_gold_01");
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_dropitem_red_01");
		CacheAudio(load_queue);
		if (pointShopResultData.Count > 0)
		{
			foreach (PointShopResultData pointShopResultDatum in pointShopResultData)
			{
				load_queue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetPointIconImageName(pointShopResultDatum.pointShopId), false);
			}
		}
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		GC.Collect();
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
		base.Initialize();
		if (MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id != MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
		{
			StartCoroutine("AutoJoinParty");
		}
	}

	protected virtual void InitReward()
	{
		if (MonoBehaviourSingleton<QuestManager>.I.compData != null)
		{
			isVictory = true;
			int start_ary_index = 0;
			QuestCompleteRewardList reward = MonoBehaviourSingleton<QuestManager>.I.compData.reward;
			dropReward = new QuestCompleteReward();
			eventReward = new QuestCompleteReward();
			ResultUtility.DevideRewardDropAndEvent(reward.drop, ref dropReward, ref eventReward, ref eventRewardTitles);
			QuestCompleteReward breakPartsReward = reward.breakPartsReward;
			QuestCompleteReward breakReward = reward.breakReward;
			QuestCompleteReward order = reward.order;
			exploreResultData = MonoBehaviourSingleton<QuestManager>.I.compData.pointExplore;
			if (exploreResultData != null)
			{
				QuestCompleteReward questCompleteReward = CreateExploreReward();
				if (questCompleteReward != null)
				{
					ResultUtility.DevideRewardDropAndEvent(questCompleteReward, ref dropReward, ref eventReward, ref eventRewardTitles);
				}
			}
			guildPoint = MonoBehaviourSingleton<QuestManager>.I.compData.guildPoint;
			missionReward = reward.mission;
			missionCompleteReward = reward.missionComplete;
			followReward = reward.followReward;
			pointShopResultData = (MonoBehaviourSingleton<QuestManager>.I.compData.pointShop ?? new List<PointShopResultData>());
			for (int i = 0; i < pointShopResultData.Count; i++)
			{
				if (pointShopResultData[i].missionPoint > 0)
				{
					missionPointData = pointShopResultData[i];
					break;
				}
			}
			List<SortCompareData> list = new List<SortCompareData>();
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, order.item, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, order.equipItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, order.skillItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, order.accessoryItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, dropReward.item, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, dropReward.equipItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, dropReward.skillItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, dropReward.questItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, dropReward.accessoryItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, breakReward.item, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, breakReward.equipItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, breakReward.skillItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, breakReward.accessoryItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, breakPartsReward.item, REWARD_CATEGORY.BREAK);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, breakPartsReward.equipItem, REWARD_CATEGORY.BREAK);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, breakPartsReward.skillItem, REWARD_CATEGORY.BREAK);
			start_ary_index = ResultUtility.SetDropData(list, start_ary_index, breakPartsReward.accessoryItem, REWARD_CATEGORY.BREAK);
			list.Sort((SortCompareData l, SortCompareData r) => r.GetSortValueQuestResult() - l.GetSortValueQuestResult());
			dropItemIconData = list.ToArray();
			dropItemNum = dropItemIconData.Length;
			dropLineNum = (dropItemNum - 1) / 5 + 1;
		}
		else
		{
			dropItemIconData = new SortCompareData[0];
		}
		if (dropItemNum == 0)
		{
			animationEnd = true;
		}
	}

	protected override void OnClose()
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null && MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID))
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.ShowOpenButton();
		}
		base.OnClose();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (MonoBehaviourSingleton<FilterManager>.IsValid())
		{
			MonoBehaviourSingleton<FilterManager>.I.StopBlur();
		}
	}

	protected virtual void Update()
	{
		if (base.isInitialized)
		{
			ResultItemAnimation();
		}
	}

	public override void UpdateUI()
	{
		SetFullScreenButton(UI.BTN_SKIP_FULL_SCREEN);
		SetHeight(UI.BTN_SKIP_IN_SCROLL, dropLineNum * 100);
		SetHeight(UI.BTN_SKIP_IN_SCROLL_2, dropLineNum * 100);
		SetActive(UI.BTN_NEXT, false);
		SetFontStyle(UI.STR_TITLE_EXP, FontStyle.Italic);
		SetFontStyle(UI.STR_TITLE_MISSION, FontStyle.Italic);
		SetFontStyle(UI.STR_TITLE_REWARD, FontStyle.Italic);
		if (MonoBehaviourSingleton<QuestManager>.I.missionNewClearFlag != null)
		{
			missionNewClear = MonoBehaviourSingleton<QuestManager>.I.missionNewClearFlag.ToArray();
		}
		int my_user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		InGameRecorder.PlayerRecord playerRecord = MonoBehaviourSingleton<InGameRecorder>.I.players.Find((InGameRecorder.PlayerRecord data) => data.charaInfo.userId == my_user_id);
		int before_level = playerRecord.beforeLevel;
		SetLabelText(UI.LBL_PLAYER_LV, before_level.ToString());
		InitDeactive(UI.LBL_PLAYER_LVUP);
		SetProgressValue(UI.PBR_EXP, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ExpProgress01);
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		SetLabelText(UI.LBL_QUEST_NAME, questData.questText);
		QuestInfoData.Mission[] array = null;
		UI[] array2 = new UI[3]
		{
			UI.OBJ_MISSION_01,
			UI.OBJ_MISSION_02,
			UI.OBJ_MISSION_03
		};
		UI[] array3 = new UI[3]
		{
			UI.LBL_MISSION_NAME_01,
			UI.LBL_MISSION_NAME_02,
			UI.LBL_MISSION_NAME_03
		};
		UI[] array4 = new UI[3]
		{
			UI.SPR_CROWN_01,
			UI.SPR_CROWN_02,
			UI.SPR_CROWN_03
		};
		UI[] array5 = new UI[3]
		{
			UI.SPR_CLEARED_CROWN_01,
			UI.SPR_CLEARED_CROWN_02,
			UI.SPR_CLEARED_CROWN_03
		};
		UI[] array6 = new UI[3]
		{
			UI.TEX_MISSION_COIN_01,
			UI.TEX_MISSION_COIN_02,
			UI.TEX_MISSION_COIN_03
		};
		UI[] array7 = new UI[3]
		{
			UI.SPR_CROWN01_OFF,
			UI.SPR_CROWN02_OFF,
			UI.SPR_CROWN03_OFF
		};
		array = QuestInfoData.CreateMissionData(Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID));
		if (array != null)
		{
			isValidMission = true;
			for (int j = 0; j < 3; j++)
			{
				bool flag = array[j] != null;
				SetActive(array2[j], flag);
				if (flag)
				{
					SetLabelText(array3[j], array[j].tableData.missionText);
					bool flag2 = missionNewClear != null && missionNewClear[j] > 0;
					bool flag3 = array[j].state >= CLEAR_STATUS.CLEAR || flag2;
					if (flag2)
					{
						isValidMissionNewClearAnim = true;
					}
					if (missionPointData != null)
					{
						SetActive(array6[j], true);
						SetActive(array7[j], false);
						SetActive(array4[j], false);
						SetActive(array5[j], false);
						UITexture component = GetCtrl(array6[j]).GetComponent<UITexture>();
						if (flag2)
						{
							ResourceLoad.LoadPointIconImageTexture(component, (uint)missionPointData.pointShopId);
						}
						else
						{
							ResourceLoad.LoadGrayPointIconImageTexture(component, (uint)missionPointData.pointShopId);
						}
					}
					else
					{
						SetActive(array6[j], false);
						SetActive(array7[j], true);
						if (flag2)
						{
							SetActive(array4[j], true);
							SetActive(array5[j], false);
						}
						else if (flag3)
						{
							SetActive(array4[j], false);
							SetActive(array5[j], true);
						}
						else
						{
							SetActive(array4[j], false);
							SetActive(array5[j], false);
						}
					}
				}
			}
		}
		else
		{
			for (int k = 0; k < 3; k++)
			{
				SetActive(array2[k], false);
			}
		}
		int num = 0;
		int exp = 0;
		if (isVictory)
		{
			QuestCompleteRewardList reward = MonoBehaviourSingleton<QuestManager>.I.compData.reward;
			QuestCompleteReward breakReward = reward.breakReward;
			QuestCompleteReward order = reward.order;
			num = dropReward.money + breakReward.money + order.money;
			exp = dropReward.exp + breakReward.exp + order.exp;
		}
		SetLabelText(UI.LBL_EXP, 0.ToString());
		SetLabelText(UI.LBL_REWARD_GOLD, num.ToString("N0"));
		bool is_mission_visible = true;
		QuestTable.QuestTableData questData2 = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		if (questData2 != null)
		{
			is_mission_visible = (questData2.questType == QUEST_TYPE.HAPPEN || questData2.questType == QUEST_TYPE.EVENT);
		}
		dropItemGRD = ((!is_mission_visible) ? UI.GRD_DROP_ITEM_2 : UI.GRD_DROP_ITEM);
		dropItemSCR = ((!is_mission_visible) ? UI.OBJ_SCROLL_VIEW_2 : UI.OBJ_SCROLL_VIEW);
		animScrollValue = ((!is_mission_visible) ? (-1.5f) : (-1.38f));
		SetGrid(dropItemGRD, null, dropItemIconData.Length, true, delegate(int i, Transform o, bool is_recycle)
		{
			ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
			RARITY_TYPE? rarity = null;
			ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
			EQUIPMENT_TYPE? magi_enable_icon_type = null;
			int icon_id = -1;
			int num6 = -1;
			if (i < dropItemIconData.Length && dropItemIconData[i] != null)
			{
				iTEM_ICON_TYPE = dropItemIconData[i].GetIconType();
				icon_id = dropItemIconData[i].GetIconID();
				rarity = dropItemIconData[i].GetRarity();
				element = dropItemIconData[i].GetIconElement();
				magi_enable_icon_type = dropItemIconData[i].GetIconMagiEnableType();
				num6 = dropItemIconData[i].GetNum();
				if (num6 == 1)
				{
					num6 = -1;
				}
			}
			bool is_new = false;
			switch (iTEM_ICON_TYPE)
			{
			case ITEM_ICON_TYPE.ITEM:
			case ITEM_ICON_TYPE.QUEST_ITEM:
			{
				ulong uniqID = dropItemIconData[i].GetUniqID();
				if (uniqID != 0L)
				{
					is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, dropItemIconData[i].GetUniqID());
				}
				break;
			}
			default:
				is_new = true;
				break;
			case ITEM_ICON_TYPE.NONE:
				break;
			}
			int enemy_icon_id = 0;
			int enemy_icon_id2 = 0;
			if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
			{
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(dropItemIconData[i].GetTableID());
				enemy_icon_id = itemData.enemyIconID;
				enemy_icon_id2 = itemData.enemyIconID2;
			}
			ItemIcon itemIcon = null;
			if (dropItemIconData[i].GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
			{
				itemIcon = ItemIcon.Create(new ItemIcon.ItemIconCreateParam
				{
					icon_type = dropItemIconData[i].GetIconType(),
					icon_id = dropItemIconData[i].GetIconID(),
					rarity = new RARITY_TYPE?(dropItemIconData[i].GetRarity()),
					parent = o,
					element = dropItemIconData[i].GetIconElement(),
					magi_enable_equip_type = dropItemIconData[i].GetIconMagiEnableType(),
					num = dropItemIconData[i].GetNum(),
					enemy_icon_id = enemy_icon_id,
					enemy_icon_id2 = enemy_icon_id2,
					questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
				});
			}
			else
			{
				GET_TYPE getType = dropItemIconData[i].GetGetType();
				itemIcon = ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, o, element, magi_enable_icon_type, num6, "DROP", i, is_new, -1, false, null, false, enemy_icon_id, enemy_icon_id2, false, getType, ELEMENT_TYPE.MAX);
			}
			itemIcon.SetRewardBG(true);
			itemIcon.SetRewardCategoryInfo(dropItemIconData[i].GetCategory());
			SetMaterialInfo(itemIcon.transform, dropItemIconData[i].GetMaterialType(), dropItemIconData[i].GetTableID(), GetCtrl((!is_mission_visible) ? UI.PNL_MATERIAL_INFO_2 : UI.PNL_MATERIAL_INFO));
			Transform transform = SetPrefab(o, "QuestResultDropIconOpener", true);
			QuestResultDropIconOpener.Info info2 = new QuestResultDropIconOpener.Info
			{
				IsRare = ResultUtility.IsRare(dropItemIconData[i]),
				IsBroken = ResultUtility.IsBreakReward(dropItemIconData[i])
			};
			transform.GetComponent<QuestResultDropIconOpener>().Initialized(itemIcon, info2, delegate(Transform t, QuestResultDropIconOpener.Info info, bool is_skip)
			{
				string ui_effect_name = "ef_ui_dropitem_silver_01";
				if (info.IsBroken)
				{
					ui_effect_name = "ef_ui_dropitem_red_01";
				}
				else if (info.IsRare)
				{
					ui_effect_name = "ef_ui_dropitem_gold_01";
				}
				SetVisibleWidgetOneShotEffect(GetCtrl(dropItemSCR), t, ui_effect_name);
			});
		});
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level > before_level)
		{
			if (string.IsNullOrEmpty(lvupTextFormat))
			{
				UILabel component2 = GetComponent<UILabel>(UI.LBL_LVUP_NUM);
				if ((UnityEngine.Object)component2 != (UnityEngine.Object)null)
				{
					lvupTextFormat = component2.text;
				}
			}
			if (GameSaveData.instance.lvupMessageFlag != 1)
			{
				GameSaveData.instance.lvupMessageFlag = 1;
				GameSaveData.Save();
			}
		}
		expGauge = GetComponent<ResultExpGaugeCtrl>(UI.OBJ_RESULT_EXP_GAUGE_CTRL);
		expGauge.InitDirection(delegate(ResultExpGaugeCtrl gauge)
		{
			gauge.getExp = (float)exp;
			gauge.startExp = (float)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.Exp - gauge.getExp;
			gauge.nowLevel = before_level;
			gauge.remainLevelUpCnt = (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level - before_level;
			gauge.OnUpdate = delegate(bool is_lvup, int now_gauge_exp, ResultExpGaugeCtrl _gauge)
			{
				SetLabelText(UI.LBL_EXP, now_gauge_exp.ToString("N0"));
				PlayAudio(AUDIO.COUNTUP);
				if (is_lvup)
				{
					SetLabelText(UI.LBL_PLAYER_LV, _gauge.nowLevel.ToString());
				}
			};
			gauge.callBack = delegate
			{
				int num5 = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
				if (animState == RESULT_ANIM_STATE.EXP_GAUGE && (num5 > before_level || MonoBehaviourSingleton<UIManager>.I.levelUp.IsLevelUp()))
				{
					animState = RESULT_ANIM_STATE.LVUP;
					MonoBehaviourSingleton<UIManager>.I.levelUp.PlayLevelUpForce(delegate
					{
						if (is_mission_visible)
						{
							ResultAnim2();
						}
						else
						{
							TreasureStart();
						}
					});
				}
				else if (is_mission_visible)
				{
					ResultAnim2();
				}
				else
				{
					TreasureStart();
				}
			};
		});
		SetActive(UI.STR_EMPTY_MISSION, !isValidMission);
		SetActive(UI.OBJ_MISSION_ROOT, is_mission_visible);
		SetActive(UI.GET_ITEM, is_mission_visible);
		SetActive(UI.GET_ITEM_2, !is_mission_visible);
		int pointNumber = pointShopResultData.Count;
		List<string> pointShopObjNames = new List<string>();
		if (exploreResultData != null)
		{
			pointNumber++;
			pointShopObjNames.Add("QuestResultExplorePoint");
		}
		if (guildPoint > 0)
		{
			pointNumber++;
			pointShopObjNames.Add("QuestResultGuildRequestPoint");
		}
		pointNumber = Mathf.Min(pointNumber, 4);
		bool flag4 = pointNumber > 0;
		SetActive(UI.OBJ_POINT_SHOP_RESULT_ROOT, flag4);
		if (flag4)
		{
			SetGrid(UI.OBJ_POINT_SHOP_RESULT_ROOT, string.Empty, pointNumber, true, delegate(int i, Transform parent)
			{
				if (pointShopObjNames.Count == 0)
				{
					return Realizes("QuestResultPointShop", parent, true);
				}
				int num4 = -(i - pointNumber) - 1;
				if (num4 >= 0 && num4 < pointShopObjNames.Count)
				{
					string prefab_name = pointShopObjNames[-(i - pointNumber) - 1];
					return Realizes(prefab_name, parent, true);
				}
				return Realizes("QuestResultPointShop", parent, true);
			}, delegate(int i, Transform t, bool b)
			{
				ResetTween(t, 0);
				int num2 = -(i - pointNumber) - 1;
				if (pointShopObjNames.Count <= 0 || num2 < 0 || num2 >= pointShopObjNames.Count)
				{
					PointShopResultData pointShopResultData = this.pointShopResultData[i];
					SetActive(t, UI.OBJ_NORMAL_POINT_SHOP_ROOT, !pointShopResultData.isEvent);
					if (!pointShopResultData.isEvent)
					{
						SetLabelText(t, UI.LBL_NORMAL_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.getPoint));
						SetLabelText(t, UI.LBL_NORMAL_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.totalPoint));
						UITexture component3 = FindCtrl(t, UI.TEX_NORMAL_POINT_SHOP_ICON).GetComponent<UITexture>();
						ResourceLoad.LoadPointIconImageTexture(component3, (uint)pointShopResultData.pointShopId);
					}
					SetActive(t, UI.OBJ_EVENT_POINT_SHOP_ROOT, pointShopResultData.isEvent);
					if (pointShopResultData.isEvent)
					{
						SetLabelText(t, UI.LBL_EVENT_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.getPoint));
						SetLabelText(t, UI.LBL_EVENT_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.totalPoint));
						UITexture component4 = FindCtrl(t, UI.TEX_EVENT_POINT_SHOP_ICON).GetComponent<UITexture>();
						ResourceLoad.LoadPointIconImageTexture(component4, (uint)pointShopResultData.pointShopId);
					}
				}
				else
				{
					switch (pointShopObjNames[num2])
					{
					case "QuestResultExplorePoint":
					{
						int getPoint = exploreResultData.pointRankingData.getPoint;
						int num3 = getPoint + exploreResultData.pointRankingData.userPoint;
						SetLabelText(t, UI.LBL_EXPLORE_GET_POINT, string.Format("+" + StringTable.Get(STRING_CATEGORY.EXPLORE, 0u), getPoint));
						SetLabelText(t, UI.LBL_EXPLORE_TOTAL_POINT, string.Format(StringTable.Get(STRING_CATEGORY.EXPLORE, 0u), num3));
						break;
					}
					case "QuestResultGuildRequestPoint":
						SetLabelText(t, UI.LBL_GUILD_REQUEST_GET_POINT, guildPoint.ToString());
						break;
					}
				}
			});
		}
		if (!is_mission_visible)
		{
			GetCtrl(UI.OBJ_TREASURE_ROOT).localPosition = GetCtrl(UI.OBJ_TREASURE_ROOT_NON_MISSION).localPosition;
		}
		StartCoroutine(ResultAnim1(exp != 0));
	}

	private IEnumerator ResultAnim1(bool is_wait)
	{
		PlayAudio(AUDIO.ADVENT);
		animState = RESULT_ANIM_STATE.EXP_GAUGE;
		bool wait = true;
		PlayTween(UI.OBJ_GET_EXP_ROOT, true, delegate
		{
			((_003CResultAnim1_003Ec__Iterator6)/*Error near IL_0055: stateMachine*/)._003Cwait_003E__0 = false;
		}, false, 0);
		while (wait)
		{
			yield return (object)null;
		}
		if (is_wait)
		{
			yield return (object)new WaitForSeconds(0.1f);
		}
		expGauge.StartAnim();
	}

	private void ResultAnim2()
	{
		PlayAudio(AUDIO.MISSION);
		animState = RESULT_ANIM_STATE.MISSION;
		MissionClearStart(delegate
		{
			OpenMissionClearRewardDialog(delegate
			{
				TreasureStart();
			});
		});
	}

	protected void MissionClearStart(Action callback)
	{
		if (!isValidMissionNewClearAnim)
		{
			PlayTween(UI.OBJ_MISSION_ROOT, true, delegate
			{
				if (callback != null)
				{
					callback();
				}
			}, true, 0);
		}
		else
		{
			PlayTween(UI.OBJ_MISSION_ROOT, true, null, false, 0);
			PlayTween(UI.OBJ_MISSION_NEW_CLEAR_ROOT, true, delegate
			{
				if (callback != null)
				{
					callback();
				}
			}, false, 0);
		}
	}

	protected virtual void TreasureStart()
	{
		animState = RESULT_ANIM_STATE.TREASURE;
		PlayAudio(AUDIO.MONEY);
		PlayAudio(AUDIO.MONEY_WH);
		PlayAudio(AUDIO.ACHIEVEMENT);
		if (pointShopResultData.Count > 0)
		{
			foreach (Transform item in GetCtrl(UI.OBJ_POINT_SHOP_RESULT_ROOT).transform)
			{
				PlayTween(item, true, null, true, 0);
			}
		}
		PlayTween(UI.OBJ_TREASURE_ROOT, true, delegate
		{
			startDropDirection = true;
		}, false, 0);
	}

	protected virtual void ResultItemAnimation()
	{
		if (startDropDirection)
		{
			if (animationEnd && animState < RESULT_ANIM_STATE.END)
			{
				AnimationEnd();
			}
			else if (!animationEnd && !IsOpenGetRareItem())
			{
				animTimer += Time.deltaTime;
				if (animTimer > 0.4f)
				{
					animIndex++;
					animTimer = 0f;
					VisibleItemIcon(animIndex, false);
					int num = animIndex + 1;
					if (num >= dropItemNum)
					{
						AnimationEnd();
					}
					else
					{
						bool flag = false;
						if (MonoBehaviourSingleton<QuestManager>.IsValid())
						{
							flag = MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
						}
						if (!flag && num >= 5 && num % 5 == 0)
						{
							SetScroll(dropItemSCR, animScrollValue);
						}
					}
				}
			}
		}
	}

	protected void OpenRareItemDialog(SortCompareData icon_base)
	{
		if (!IsOpenGetRareItem() && icon_base != null)
		{
			is_open_get_rare_item = true;
			DispatchEvent("RARE", new object[1]
			{
				icon_base.GetName()
			});
		}
	}

	protected bool IsOpenGetRareItem()
	{
		return is_open_get_rare_item;
	}

	public void OnCloseDialog_QuestResultGetRareItem()
	{
		is_open_get_rare_item = false;
	}

	private void VisibleItemIcon(int index, bool is_skip = false)
	{
		if (index < dropItemIconData.Length && dropItemIconData[index] != null && dropItemIconData[index].GetTableID() != 0)
		{
			Transform child = GetChild(dropItemGRD, index);
			if (!((UnityEngine.Object)child == (UnityEngine.Object)null))
			{
				QuestResultDropIconOpener componentInChildren = child.gameObject.GetComponentInChildren<QuestResultDropIconOpener>();
				if (!((UnityEngine.Object)componentInChildren == (UnityEngine.Object)null))
				{
					PlayAudio(AUDIO.DROPITEM);
					componentInChildren.StartEffect(is_skip);
				}
			}
		}
	}

	protected void OpenedIconEndEff(int index)
	{
		Transform child = GetChild(dropItemGRD, index);
		if (!((UnityEngine.Object)child == (UnityEngine.Object)null))
		{
			QuestResultDropIconOpener componentInChildren = child.gameObject.GetComponentInChildren<QuestResultDropIconOpener>();
			if (!((UnityEngine.Object)componentInChildren == (UnityEngine.Object)null))
			{
				componentInChildren.StartEffect(true);
			}
		}
	}

	protected virtual void AnimationEnd()
	{
		animState = RESULT_ANIM_STATE.END;
		animationEnd = true;
		OpenMissionClearRewardDialog(delegate
		{
			OpenFirstClearRewardDialog(delegate
			{
				OpenMutualFollowBonusDialog(delegate
				{
					OpenAllEventRewardDialog(delegate
					{
						OpenDropSell(delegate
						{
							OpenPointEvent(delegate
							{
								VisibleEndButton();
							});
						});
					});
				});
			});
		});
	}

	protected virtual void VisibleEndButton()
	{
		if (MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
			{
				SetActive(UI.BTN_END_HUNT_LEFT, animationEnd);
				SetActive(UI.BTN_REPEAT_HUNT, animationEnd);
				StartCoroutine("WaitForRepeatHunt");
			}
			else
			{
				SetActive(UI.BTN_END_HUNT_CENTER, animationEnd);
				StartCoroutine("WaitForHost");
			}
		}
		else
		{
			SetActive(UI.BTN_NEXT, animationEnd);
		}
		SetActive(UI.BTN_SKIP_FULL_SCREEN, false);
		SetActive(UI.BTN_SKIP_IN_SCROLL, false);
		SetActive(UI.BTN_SKIP_IN_SCROLL_2, false);
		DispatchEvent("RESULT_TUTORIAL", null);
	}

	protected void AnimSkip()
	{
		if (!animationEnd)
		{
			if (animIndex < 0)
			{
				animIndex = 0;
			}
			for (int i = 0; i < dropItemIconData.Length; i++)
			{
				if (i < animIndex)
				{
					OpenedIconEndEff(i);
				}
				else
				{
					VisibleItemIcon(i, true);
				}
			}
			if (dropLineNum > 1)
			{
				bool flag = false;
				if (MonoBehaviourSingleton<QuestManager>.IsValid())
				{
					flag = MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
				}
				if (!flag)
				{
					SetScroll(dropItemSCR, -100f);
				}
			}
			animIndex = dropItemIconData.Length - 1;
			AnimationEnd();
			animTimer = 0f;
		}
	}

	private void OnQuery_SKIP()
	{
		if (animState != RESULT_ANIM_STATE.END)
		{
			switch (animState)
			{
			case RESULT_ANIM_STATE.EXP_GAUGE:
				SkipTween(UI.OBJ_GET_EXP_ROOT, true, 0);
				expGauge.Skip();
				break;
			case RESULT_ANIM_STATE.LVUP:
				MonoBehaviourSingleton<UIManager>.I.levelUp.SkipAnim();
				break;
			case RESULT_ANIM_STATE.MISSION:
				animState = RESULT_ANIM_STATE.MISSION_REWARD;
				SkipTween(UI.OBJ_MISSION_ROOT, true, 0);
				SkipTween(UI.OBJ_MISSION_NEW_CLEAR_ROOT, true, 0);
				break;
			case RESULT_ANIM_STATE.TREASURE:
				SkipTween(UI.OBJ_TREASURE_ROOT, true, 0);
				SkipTween(UI.OBJ_POINT_SHOP_RESULT_ROOT, true, 0);
				AnimSkip();
				break;
			}
			GameSection.StopEvent();
		}
	}

	private void OnQuery_FRIEND()
	{
		if (animState < RESULT_ANIM_STATE.END)
		{
			OnQuery_SKIP();
		}
	}

	protected REWARD_TYPE GetRewardType(ITEM_ICON_TYPE icon_type)
	{
		switch (icon_type)
		{
		default:
			return REWARD_TYPE.EQUIP_ITEM;
		case ITEM_ICON_TYPE.SKILL_ATTACK:
		case ITEM_ICON_TYPE.SKILL_SUPPORT:
		case ITEM_ICON_TYPE.SKILL_HEAL:
		case ITEM_ICON_TYPE.SKILL_PASSIVE:
		case ITEM_ICON_TYPE.SKILL_GROW:
			return REWARD_TYPE.SKILL_ITEM;
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.ABILITY_ITEM:
			return REWARD_TYPE.ITEM;
		case ITEM_ICON_TYPE.COMMON:
			return REWARD_TYPE.COMMON;
		case ITEM_ICON_TYPE.STAMP:
			return REWARD_TYPE.STAMP;
		case ITEM_ICON_TYPE.ACCESSORY:
			return REWARD_TYPE.ACCESSORY;
		}
	}

	protected void OpenDropSell(Action callback)
	{
		if (!isNeedOpenDropSellDialog)
		{
			callback?.Invoke();
		}
		else
		{
			dropSellCallback = callback;
			int total_sell = 0;
			List<SortCompareData> list = new List<SortCompareData>();
			List<QuestCompleteReward.SellItem> sell = MonoBehaviourSingleton<QuestManager>.I.compData.reward.sell;
			sell.ForEach(delegate(QuestCompleteReward.SellItem data)
			{
				ItemSortData itemSortData = new ItemSortData();
				ItemInfo itemInfo = new ItemInfo();
				itemInfo.tableID = (uint)data.itemId;
				itemInfo.tableData = Singleton<ItemTable>.I.GetItemData(itemInfo.tableID);
				itemInfo.num = data.num;
				itemSortData.SetItem(itemInfo);
				list.Add(itemSortData);
				total_sell += data.price;
			});
			if (!IsExecuteNowSceneEvent(GetSceneName()))
			{
				StartCoroutine(ExecEndDialogEvent(GetSceneName(), delegate
				{
					DispatchEvent("DROP_SELL", new object[2]
					{
						list,
						total_sell
					});
				}));
			}
			else
			{
				DispatchEvent("DROP_SELL", new object[2]
				{
					list,
					total_sell
				});
			}
		}
	}

	protected void OnCloseDialog_QuestResultDropSellConfirm()
	{
		if (dropSellCallback != null)
		{
			StartCoroutine(OnCloseDropSellClearCoroutine());
		}
	}

	protected IEnumerator OnCloseDropSellClearCoroutine()
	{
		if (!IsExecuteNowSceneEvent(GetSceneName()))
		{
			yield return (object)null;
		}
		if (dropSellCallback != null)
		{
			dropSellCallback();
		}
	}

	protected void OpenPointEvent(Action callback)
	{
		if (!isNeedPointResult)
		{
			callback?.Invoke();
		}
		else
		{
			pointEventCallback = callback;
			if (!IsExecuteNowSceneEvent(GetSceneName()))
			{
				StartCoroutine(ExecEndDialogEvent(GetSceneName(), delegate
				{
					DispatchEvent("CARNIVAL_POINT", MonoBehaviourSingleton<QuestManager>.I.compData.pointEvent[0]);
				}));
			}
			else
			{
				DispatchEvent("CARNIVAL_POINT", MonoBehaviourSingleton<QuestManager>.I.compData.pointEvent[0]);
			}
		}
	}

	protected void OnCloseDialog_QuestResultPointEvent()
	{
		if (pointEventCallback != null)
		{
			StartCoroutine(OnCloseQuestResultPointEventClearCoroutine());
		}
	}

	protected void OnCloseDialog_CarnivalResultPoint()
	{
		if (pointEventCallback != null)
		{
			StartCoroutine(OnCloseQuestResultPointEventClearCoroutine());
		}
	}

	protected IEnumerator OnCloseQuestResultPointEventClearCoroutine()
	{
		if (!IsExecuteNowSceneEvent(GetSceneName()))
		{
			yield return (object)null;
		}
		if (pointEventCallback != null)
		{
			pointEventCallback();
		}
	}

	private void OpenMissionClearRewardDialog(Action end_callback)
	{
		bool flag = animState == RESULT_ANIM_STATE.MISSION || animState == RESULT_ANIM_STATE.END;
		if (!isNeedOpenMissionClearDialog || !flag || isOpenedMissionClearDialog)
		{
			end_callback?.Invoke();
		}
		else
		{
			QuestCompleteReward questCompleteReward = null;
			PointShopResultData missionPoint = null;
			bool isCompleteReward = false;
			if (missionPointData != null || missionReward != null)
			{
				if (missionReward != null)
				{
					questCompleteReward = missionReward;
					missionReward = null;
				}
				if (missionPointData != null)
				{
					missionPoint = missionPointData;
					missionPointData = null;
				}
			}
			else
			{
				if (missionCompleteReward == null)
				{
					isOpenedMissionClearDialog = true;
					if (animState < RESULT_ANIM_STATE.END)
					{
						animState = RESULT_ANIM_STATE.MISSION_REWARD;
					}
					end_callback?.Invoke();
					return;
				}
				questCompleteReward = missionCompleteReward;
				missionCompleteReward = null;
				isCompleteReward = true;
				if (animState < RESULT_ANIM_STATE.END)
				{
					animState = RESULT_ANIM_STATE.MISSION_REWARD;
				}
			}
			List<SortCompareData> tmp = new List<SortCompareData>();
			int num = 0;
			int gold = questCompleteReward?.money ?? 0;
			int crystal = questCompleteReward?.crystal ?? 0;
			if (questCompleteReward != null)
			{
				num = ResultUtility.SetDropData(tmp, num, questCompleteReward.item, REWARD_CATEGORY.DROP);
				num = ResultUtility.SetDropData(tmp, num, questCompleteReward.equipItem, REWARD_CATEGORY.DROP);
				num = ResultUtility.SetDropData(tmp, num, questCompleteReward.skillItem, REWARD_CATEGORY.DROP);
				num = ResultUtility.SetDropData(tmp, num, questCompleteReward.accessoryItem, REWARD_CATEGORY.DROP);
			}
			if (num == 0 && crystal == 0 && missionPoint == null)
			{
				end_callback?.Invoke();
			}
			else
			{
				missionClearRewardCallback = end_callback;
				if (missionPoint == null)
				{
					missionPoint = new PointShopResultData();
				}
				if (!IsExecuteNowSceneEvent(GetSceneName()))
				{
					StartCoroutine(ExecEndDialogEvent(GetSceneName(), delegate
					{
						DispatchEvent("MISSION_CLEAR_REWARD", new object[5]
						{
							tmp,
							gold,
							crystal,
							isCompleteReward,
							missionPoint
						});
					}));
				}
				else
				{
					DispatchEvent("MISSION_CLEAR_REWARD", new object[5]
					{
						tmp,
						gold,
						crystal,
						isCompleteReward,
						missionPoint
					});
				}
			}
		}
	}

	private void OnCloseDialog_QuestResultMissionClearRewardDialog()
	{
		if (missionClearRewardCallback != null)
		{
			StartCoroutine(OnCloseMissionClearCoroutine());
		}
	}

	private IEnumerator OnCloseMissionClearCoroutine()
	{
		if (!IsExecuteNowSceneEvent(GetSceneName()))
		{
			yield return (object)null;
		}
		OpenMissionClearRewardDialog(missionClearRewardCallback);
	}

	private void OpenMutualFollowBonusDialog(Action end_callback)
	{
		bool flag = animState == RESULT_ANIM_STATE.FOLLOW_BONUS || animState == RESULT_ANIM_STATE.END;
		if (!isNeedOpenFollowBonusDialog || !flag)
		{
			end_callback?.Invoke();
		}
		else if (followReward != null)
		{
			QuestCompleteReward questCompleteReward = followReward;
			followReward = null;
			List<SortCompareData> tmp = new List<SortCompareData>();
			int start_ary_index = 0;
			int gold = questCompleteReward.money;
			int crystal = questCompleteReward.crystal;
			int exp = questCompleteReward.exp;
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, questCompleteReward.item, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, questCompleteReward.equipItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, questCompleteReward.skillItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, questCompleteReward.questItem, REWARD_CATEGORY.DROP);
			if (ResultUtility.SetDropData(tmp, start_ary_index, questCompleteReward.accessoryItem, REWARD_CATEGORY.DROP) == 0 && crystal == 0 && gold == 0 && exp == 0)
			{
				end_callback?.Invoke();
			}
			else
			{
				followBonusCallback = end_callback;
				if (!IsExecuteNowSceneEvent(GetSceneName()))
				{
					StartCoroutine(ExecEndDialogEvent(GetSceneName(), delegate
					{
						DispatchEvent("MUTUAL_FOLLOW_BONUS", new object[3]
						{
							tmp,
							gold,
							crystal
						});
					}));
				}
				else
				{
					DispatchEvent("MUTUAL_FOLLOW_BONUS", new object[3]
					{
						tmp,
						gold,
						crystal
					});
				}
			}
		}
		else
		{
			if (animState < RESULT_ANIM_STATE.END)
			{
				animState = RESULT_ANIM_STATE.FOLLOW_BONUS;
			}
			end_callback?.Invoke();
		}
	}

	protected void OnCloseDialog_QuestResultMutualFollowBonusDialog()
	{
		if (followBonusCallback != null)
		{
			StartCoroutine(OnCloseFollowBonusCoroutine());
		}
	}

	protected IEnumerator OnCloseFollowBonusCoroutine()
	{
		if (!IsExecuteNowSceneEvent(GetSceneName()))
		{
			yield return (object)null;
		}
		OpenMutualFollowBonusDialog(followBonusCallback);
	}

	private void OpenAllEventRewardDialog(Action endCallback)
	{
		eventRewardIndex = 0;
		eventRewardList = new List<QuestCompleteReward>();
		for (int i = 0; i < eventRewardTitles.Count; i++)
		{
			QuestCompleteReward item = new QuestCompleteReward();
			eventRewardList.Add(item);
		}
		for (int j = 0; j < eventReward.eventPrice.Count; j++)
		{
			for (int k = 0; k < eventRewardTitles.Count; k++)
			{
				if (eventRewardTitles[k] == eventReward.eventPrice[j].rewardTitle)
				{
					eventRewardList[k].eventPrice.Add(eventReward.eventPrice[j]);
				}
			}
		}
		for (int l = 0; l < eventReward.item.Count; l++)
		{
			for (int m = 0; m < eventRewardTitles.Count; m++)
			{
				if (eventRewardTitles[m] == eventReward.item[l].rewardTitle)
				{
					eventRewardList[m].item.Add(eventReward.item[l]);
				}
			}
		}
		for (int n = 0; n < eventReward.skillItem.Count; n++)
		{
			for (int num = 0; num < eventRewardTitles.Count; num++)
			{
				if (eventRewardTitles[num] == eventReward.skillItem[n].rewardTitle)
				{
					eventRewardList[num].skillItem.Add(eventReward.skillItem[n]);
				}
			}
		}
		for (int num2 = 0; num2 < eventReward.equipItem.Count; num2++)
		{
			for (int num3 = 0; num3 < eventRewardTitles.Count; num3++)
			{
				if (eventRewardTitles[num3] == eventReward.equipItem[num2].rewardTitle)
				{
					eventRewardList[num3].equipItem.Add(eventReward.equipItem[num2]);
				}
			}
		}
		for (int num4 = 0; num4 < eventReward.questItem.Count; num4++)
		{
			for (int num5 = 0; num5 < eventRewardTitles.Count; num5++)
			{
				if (eventRewardTitles[num5] == eventReward.questItem[num4].rewardTitle)
				{
					eventRewardList[num5].questItem.Add(eventReward.questItem[num4]);
				}
			}
		}
		for (int num6 = 0; num6 < eventReward.accessoryItem.Count; num6++)
		{
			for (int num7 = 0; num7 < eventRewardTitles.Count; num7++)
			{
				if (eventRewardTitles[num7] == eventReward.accessoryItem[num6].rewardTitle)
				{
					eventRewardList[num7].accessoryItem.Add(eventReward.accessoryItem[num6]);
				}
			}
		}
		if (eventRewardList.Count == 0)
		{
			endCallback();
		}
		else
		{
			OpenEventRewardDialog(eventRewardList[eventRewardIndex], eventRewardTitles[eventRewardIndex], endCallback);
			eventRewardIndex++;
		}
	}

	protected void OpenEventRewardDialog(QuestCompleteReward reward, string title, Action end_callback)
	{
		List<SortCompareData> tmp = new List<SortCompareData>();
		int start_ary_index = 0;
		int gold = 0;
		int crystal = 0;
		for (int i = 0; i < reward.eventPrice.Count; i++)
		{
			gold += reward.eventPrice[i].gold;
			crystal += reward.eventPrice[i].crystal;
		}
		start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, reward.item, REWARD_CATEGORY.DROP);
		start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, reward.equipItem, REWARD_CATEGORY.DROP);
		start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, reward.skillItem, REWARD_CATEGORY.DROP);
		start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, reward.questItem, REWARD_CATEGORY.DROP);
		if (ResultUtility.SetDropData(tmp, start_ary_index, reward.accessoryItem, REWARD_CATEGORY.DROP) == 0 && gold == 0 && crystal == 0)
		{
			end_callback?.Invoke();
		}
		else
		{
			eventRewardCallback = end_callback;
			if (!IsExecuteNowSceneEvent(GetSceneName()))
			{
				StartCoroutine(ExecEndDialogEvent(GetSceneName(), delegate
				{
					DispatchEvent("EVENT_REWARD", new object[4]
					{
						tmp,
						gold,
						crystal,
						title
					});
				}));
			}
			else
			{
				DispatchEvent("EVENT_REWARD", new object[4]
				{
					tmp,
					gold,
					crystal,
					title
				});
			}
		}
	}

	protected void OnCloseDialog_QuestResultEventRewardDialog()
	{
		if (eventRewardCallback != null)
		{
			StartCoroutine(OnCloseEventRewardCoroutine());
		}
	}

	protected IEnumerator OnCloseEventRewardCoroutine()
	{
		if (!IsExecuteNowSceneEvent(GetSceneName()))
		{
			yield return (object)null;
		}
		if (eventRewardIndex > eventRewardList.Count - 1)
		{
			eventRewardCallback();
		}
		else
		{
			OpenEventRewardDialog(eventRewardList[eventRewardIndex], eventRewardTitles[eventRewardIndex], eventRewardCallback);
			eventRewardIndex++;
		}
	}

	private QuestCompleteReward CreateExploreReward()
	{
		if (exploreResultData == null)
		{
			return null;
		}
		List<PointEventCurrentData.Reward> list = new List<PointEventCurrentData.Reward>();
		List<PointEventCurrentData.PointRewardData> getReward = exploreResultData.pointRankingData.getReward;
		for (int i = 0; i < getReward.Count; i++)
		{
			List<PointEventCurrentData.Reward> reward = getReward[i].reward;
			list.AddRange(reward);
		}
		string rewardTitle = exploreResultData.rewardTitle;
		QuestCompleteReward questCompleteReward = new QuestCompleteReward();
		for (int j = 0; j < list.Count; j++)
		{
			switch (list[j].type)
			{
			case 1:
				questCompleteReward.crystal += list[j].num;
				if (!string.IsNullOrEmpty(rewardTitle))
				{
					QuestCompleteReward.EventPrice eventPrice2 = new QuestCompleteReward.EventPrice();
					eventPrice2.rewardTitle = rewardTitle;
					eventPrice2.crystal = list[j].num;
					questCompleteReward.eventPrice.Add(eventPrice2);
				}
				break;
			case 2:
				questCompleteReward.money += list[j].num;
				if (!string.IsNullOrEmpty(rewardTitle))
				{
					QuestCompleteReward.EventPrice eventPrice = new QuestCompleteReward.EventPrice();
					eventPrice.rewardTitle = rewardTitle;
					eventPrice.gold = list[j].num;
					questCompleteReward.eventPrice.Add(eventPrice);
				}
				break;
			case 3:
			{
				QuestCompleteReward.Item item = new QuestCompleteReward.Item();
				item.rewardTitle = rewardTitle;
				item.itemId = list[j].itemId;
				item.num = list[j].num;
				questCompleteReward.item.Add(item);
				break;
			}
			case 4:
			{
				QuestCompleteReward.EquipItem equipItem = new QuestCompleteReward.EquipItem();
				equipItem.rewardTitle = rewardTitle;
				equipItem.equipItemId = list[j].itemId;
				equipItem.num = list[j].num;
				questCompleteReward.equipItem.Add(equipItem);
				break;
			}
			case 5:
			{
				QuestCompleteReward.SkillItem skillItem = new QuestCompleteReward.SkillItem();
				skillItem.rewardTitle = rewardTitle;
				skillItem.skillItemId = list[j].itemId;
				skillItem.num = list[j].num;
				questCompleteReward.skillItem.Add(skillItem);
				break;
			}
			case 6:
			{
				QuestCompleteReward.QuestItem questItem = new QuestCompleteReward.QuestItem();
				questItem.rewardTitle = rewardTitle;
				questItem.questId = list[j].itemId;
				questItem.num = list[j].num;
				questCompleteReward.questItem.Add(questItem);
				break;
			}
			case 14:
			{
				QuestCompleteReward.AccessoryItem accessoryItem = new QuestCompleteReward.AccessoryItem();
				accessoryItem.rewardTitle = rewardTitle;
				accessoryItem.accessoryId = list[j].itemId;
				accessoryItem.num = list[j].num;
				questCompleteReward.accessoryItem.Add(accessoryItem);
				break;
			}
			}
		}
		return questCompleteReward;
	}

	protected void OpenFirstClearRewardDialog(Action callback)
	{
		if (!isNeedOpenFirstClearDialog)
		{
			callback?.Invoke();
		}
		else
		{
			animState = RESULT_ANIM_STATE.FIRST_CLEAR_REWARD;
			firstClearRewardCallback = callback;
			QuestCompleteReward first = MonoBehaviourSingleton<QuestManager>.I.compData.reward.first;
			List<SortCompareData> tmp = new List<SortCompareData>();
			int start_ary_index = 0;
			int gold = first.money;
			int crystal = first.crystal;
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, first.item, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, first.equipItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, first.skillItem, REWARD_CATEGORY.DROP);
			start_ary_index = ResultUtility.SetDropData(tmp, start_ary_index, first.accessoryItem, REWARD_CATEGORY.DROP);
			if (!IsExecuteNowSceneEvent(GetSceneName()))
			{
				StartCoroutine(ExecEndDialogEvent(GetSceneName(), delegate
				{
					DispatchEvent("FIRST_CLEAR_REWARD", new object[3]
					{
						tmp,
						gold,
						crystal
					});
				}));
			}
			else
			{
				DispatchEvent("FIRST_CLEAR_REWARD", new object[3]
				{
					tmp,
					gold,
					crystal
				});
			}
		}
	}

	protected void OnCloseDialog_QuestResultFirstClearRewardDialog()
	{
		if (firstClearRewardCallback != null)
		{
			StartCoroutine(OnCloseFirstClearCoroutine());
		}
	}

	protected IEnumerator OnCloseFirstClearCoroutine()
	{
		if (!IsExecuteNowSceneEvent(GetSceneName()))
		{
			yield return (object)null;
		}
		if (firstClearRewardCallback != null)
		{
			firstClearRewardCallback();
		}
	}

	public static bool IsExecuteNowSceneEvent(string section_name)
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == section_name && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			return true;
		}
		return false;
	}

	protected IEnumerator ExecEndDialogEvent(string section_name, Action callback)
	{
		if (!IsExecuteNowSceneEvent(section_name))
		{
			yield return (object)null;
		}
		callback();
	}

	private void PlayAudio(AUDIO type)
	{
		SoundManager.PlayOneShotUISE((int)type);
	}

	protected virtual string GetSceneName()
	{
		return "QuestResultTop";
	}

	protected void OnQuery_LEAVE_HUNT()
	{
		StopCoroutine("AutoJoinParty");
		StopCoroutine("WaitForHost");
		DispatchEvent("FRIEND", null);
	}

	protected void OnQuery_END_HUNT()
	{
		StopCoroutine("WaitForRepeatHunt");
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendRepeat(false, delegate(bool is_success)
		{
			GameSection.ChangeStayEvent("FRIEND", null);
			GameSection.ResumeEvent(is_success, null);
		});
	}

	protected void OnQuery_REPEAT_HUNT()
	{
		StopCoroutine("WaitForRepeatHunt");
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<PartyManager>.I.GetQuestId());
		bool is_free_join = true;
		if (questData.questType == QUEST_TYPE.EVENT)
		{
			is_free_join = !MonoBehaviourSingleton<PartyManager>.I.IsPayingQuest();
		}
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID, is_free_join);
		MonoBehaviourSingleton<InGameProgress>.I.QuestRepeat();
	}

	private IEnumerator WaitForHost()
	{
		canEnterParty = false;
		for (int wait = 10; wait > 0; wait--)
		{
			SetLabelText(text: $"Wait For Host({wait}s)", label_enum: UI.LBL_WAIT_FOR_HOST);
			yield return (object)new WaitForSeconds(1f);
		}
		yield return (object)null;
		SetLabelText(UI.LBL_WAIT_FOR_HOST, "Wait For Host...");
		canEnterParty = true;
	}

	private IEnumerator AutoJoinParty()
	{
		bool wait3 = true;
		bool waitGetData2 = false;
		while (MonoBehaviourSingleton<PartyManager>.I.repeatPartyStatus == 0)
		{
			if (!waitGetData2)
			{
				waitGetData2 = true;
				MonoBehaviourSingleton<PartyManager>.I.SendGetNextParty(delegate
				{
					((_003CAutoJoinParty_003Ec__IteratorF)/*Error near IL_005f: stateMachine*/)._003CwaitGetData_003E__1 = false;
				});
			}
			else
			{
				yield return (object)new WaitForSeconds(2f);
			}
		}
		if (MonoBehaviourSingleton<PartyManager>.I.repeatPartyStatus >= 0)
		{
			MonoBehaviourSingleton<PartyManager>.I.SendEntry(MonoBehaviourSingleton<PartyManager>.I.partyData.id, false, delegate
			{
				((_003CAutoJoinParty_003Ec__IteratorF)/*Error near IL_00ca: stateMachine*/)._003Cwait_003E__0 = false;
			});
			while (wait3)
			{
				yield return (object)null;
			}
			wait3 = true;
			MonoBehaviourSingleton<PartyManager>.I.SendReady(true, delegate
			{
				((_003CAutoJoinParty_003Ec__IteratorF)/*Error near IL_010b: stateMachine*/)._003Cwait_003E__0 = false;
			});
			while (wait3)
			{
				yield return (object)null;
			}
			wait3 = true;
			waitGetData2 = false;
			while (wait3)
			{
				if (!waitGetData2)
				{
					waitGetData2 = true;
					MonoBehaviourSingleton<PartyManager>.I.SendInfo(delegate
					{
						((_003CAutoJoinParty_003Ec__IteratorF)/*Error near IL_0169: stateMachine*/)._003CwaitGetData_003E__1 = false;
						if (PartyManager.IsValidInParty())
						{
							if (MonoBehaviourSingleton<PartyManager>.I.partyData.status == 100)
							{
								((_003CAutoJoinParty_003Ec__IteratorF)/*Error near IL_0169: stateMachine*/)._003Cwait_003E__0 = false;
							}
							else if (!MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest)
							{
								((_003CAutoJoinParty_003Ec__IteratorF)/*Error near IL_0169: stateMachine*/)._003Cwait_003E__0 = false;
							}
						}
						else
						{
							((_003CAutoJoinParty_003Ec__IteratorF)/*Error near IL_0169: stateMachine*/)._003Cwait_003E__0 = false;
						}
					});
					yield return (object)null;
				}
				else
				{
					yield return (object)new WaitForSeconds(2f);
				}
			}
			while (!canEnterParty)
			{
				if (!PartyManager.IsValidInParty())
				{
					canEnterParty = true;
				}
				else if (MonoBehaviourSingleton<PartyManager>.I.partyData.status != 100 && !MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest)
				{
					canEnterParty = true;
				}
				yield return (object)null;
			}
			if (PartyManager.IsValidInParty())
			{
				if (MonoBehaviourSingleton<PartyManager>.I.partyData.status == 100)
				{
					EnterPartyQuest();
				}
				else if (!MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest)
				{
					DispatchEvent("HOST_LEFT", null);
				}
			}
			else
			{
				DispatchEvent("HOST_LEFT", null);
			}
		}
	}

	private IEnumerator WaitForRepeatHunt()
	{
		for (int wait = 5; wait > 0; wait--)
		{
			SetLabelText(text: $"Repeat({wait}s)", label_enum: UI.LBL_BTN_REPEAT_HUNT);
			yield return (object)new WaitForSeconds(1f);
		}
		SetLabelText(UI.LBL_BTN_REPEAT_HUNT, "Repeat(0s)");
		yield return (object)null;
		QuestTable.QuestTableData table = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<PartyManager>.I.GetQuestId());
		bool free_join = true;
		if (table.questType == QUEST_TYPE.EVENT)
		{
			free_join = !MonoBehaviourSingleton<PartyManager>.I.IsPayingQuest();
		}
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(table.questID, free_join);
		MonoBehaviourSingleton<InGameProgress>.I.QuestRepeat();
	}

	private void EnterPartyQuest()
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<PartyManager>.I.GetQuestId());
		bool is_free_join = true;
		if (questData.questType == QUEST_TYPE.EVENT)
		{
			is_free_join = !MonoBehaviourSingleton<PartyManager>.I.IsPayingQuest();
		}
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questData.questID, is_free_join);
		MonoBehaviourSingleton<InGameProgress>.I.QuestRepeat();
	}
}
