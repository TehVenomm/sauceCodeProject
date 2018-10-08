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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
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
			this.StartCoroutine("AutoJoinParty");
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
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null && MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID))
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

	public unsafe override void UpdateUI()
	{
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		SetFullScreenButton((Enum)UI.BTN_SKIP_FULL_SCREEN);
		SetHeight((Enum)UI.BTN_SKIP_IN_SCROLL, dropLineNum * 100);
		SetHeight((Enum)UI.BTN_SKIP_IN_SCROLL_2, dropLineNum * 100);
		SetActive((Enum)UI.BTN_NEXT, false);
		SetFontStyle((Enum)UI.STR_TITLE_EXP, 2);
		SetFontStyle((Enum)UI.STR_TITLE_MISSION, 2);
		SetFontStyle((Enum)UI.STR_TITLE_REWARD, 2);
		if (MonoBehaviourSingleton<QuestManager>.I.missionNewClearFlag != null)
		{
			missionNewClear = MonoBehaviourSingleton<QuestManager>.I.missionNewClearFlag.ToArray();
		}
		int my_user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		InGameRecorder.PlayerRecord playerRecord = MonoBehaviourSingleton<InGameRecorder>.I.players.Find((InGameRecorder.PlayerRecord data) => data.charaInfo.userId == my_user_id);
		int before_level = playerRecord.beforeLevel;
		SetLabelText((Enum)UI.LBL_PLAYER_LV, before_level.ToString());
		InitDeactive((Enum)UI.LBL_PLAYER_LVUP);
		SetProgressValue((Enum)UI.PBR_EXP, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.ExpProgress01);
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		SetLabelText((Enum)UI.LBL_QUEST_NAME, questData.questText);
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
			for (int i = 0; i < 3; i++)
			{
				bool flag = array[i] != null;
				SetActive((Enum)array2[i], flag);
				if (flag)
				{
					SetLabelText((Enum)array3[i], array[i].tableData.missionText);
					bool flag2 = missionNewClear != null && missionNewClear[i] > 0;
					bool flag3 = array[i].state >= CLEAR_STATUS.CLEAR || flag2;
					if (flag2)
					{
						isValidMissionNewClearAnim = true;
					}
					if (missionPointData != null)
					{
						SetActive((Enum)array6[i], true);
						SetActive((Enum)array7[i], false);
						SetActive((Enum)array4[i], false);
						SetActive((Enum)array5[i], false);
						UITexture component = GetCtrl(array6[i]).GetComponent<UITexture>();
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
						SetActive((Enum)array6[i], false);
						SetActive((Enum)array7[i], true);
						if (flag2)
						{
							SetActive((Enum)array4[i], true);
							SetActive((Enum)array5[i], false);
						}
						else if (flag3)
						{
							SetActive((Enum)array4[i], false);
							SetActive((Enum)array5[i], true);
						}
						else
						{
							SetActive((Enum)array4[i], false);
							SetActive((Enum)array5[i], false);
						}
					}
				}
			}
		}
		else
		{
			for (int j = 0; j < 3; j++)
			{
				SetActive((Enum)array2[j], false);
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
		SetLabelText((Enum)UI.LBL_EXP, 0.ToString());
		SetLabelText((Enum)UI.LBL_REWARD_GOLD, num.ToString("N0"));
		bool is_mission_visible = true;
		QuestTable.QuestTableData questData2 = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		if (questData2 != null)
		{
			is_mission_visible = (questData2.questType == QUEST_TYPE.HAPPEN || questData2.questType == QUEST_TYPE.EVENT);
		}
		dropItemGRD = ((!is_mission_visible) ? UI.GRD_DROP_ITEM_2 : UI.GRD_DROP_ITEM);
		dropItemSCR = ((!is_mission_visible) ? UI.OBJ_SCROLL_VIEW_2 : UI.OBJ_SCROLL_VIEW);
		animScrollValue = ((!is_mission_visible) ? (-1.5f) : (-1.38f));
		_003CUpdateUI_003Ec__AnonStorey2B2 _003CUpdateUI_003Ec__AnonStorey2B;
		SetGrid(dropItemGRD, null, dropItemIconData.Length, true, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey2B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level > before_level)
		{
			if (string.IsNullOrEmpty(lvupTextFormat))
			{
				UILabel component2 = base.GetComponent<UILabel>((Enum)UI.LBL_LVUP_NUM);
				if (component2 != null)
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
		expGauge = base.GetComponent<ResultExpGaugeCtrl>((Enum)UI.OBJ_RESULT_EXP_GAUGE_CTRL);
		expGauge.InitDirection(delegate(ResultExpGaugeCtrl gauge)
		{
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Expected O, but got Unknown
			gauge.getExp = (float)exp;
			gauge.startExp = (float)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.Exp - gauge.getExp;
			gauge.nowLevel = before_level;
			gauge.remainLevelUpCnt = (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level - before_level;
			gauge.OnUpdate = new Action<bool, int, ResultExpGaugeCtrl>((object)_003CUpdateUI_003Ec__AnonStorey2B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			gauge.callBack = new Action((object)_003CUpdateUI_003Ec__AnonStorey2B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		});
		SetActive((Enum)UI.STR_EMPTY_MISSION, !isValidMission);
		SetActive((Enum)UI.OBJ_MISSION_ROOT, is_mission_visible);
		SetActive((Enum)UI.GET_ITEM, is_mission_visible);
		SetActive((Enum)UI.GET_ITEM_2, !is_mission_visible);
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
		SetActive((Enum)UI.OBJ_POINT_SHOP_RESULT_ROOT, flag4);
		if (flag4)
		{
			SetGrid(UI.OBJ_POINT_SHOP_RESULT_ROOT, string.Empty, pointNumber, true, new Func<int, Transform, Transform>((object)_003CUpdateUI_003Ec__AnonStorey2B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey2B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
		if (!is_mission_visible)
		{
			GetCtrl(UI.OBJ_TREASURE_ROOT).set_localPosition(GetCtrl(UI.OBJ_TREASURE_ROOT_NON_MISSION).get_localPosition());
		}
		this.StartCoroutine(ResultAnim1(exp != 0));
	}

	private IEnumerator ResultAnim1(bool is_wait)
	{
		PlayAudio(AUDIO.ADVENT);
		animState = RESULT_ANIM_STATE.EXP_GAUGE;
		bool wait = true;
		PlayTween((Enum)UI.OBJ_GET_EXP_ROOT, true, (EventDelegate.Callback)delegate
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

	private unsafe void ResultAnim2()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		PlayAudio(AUDIO.MISSION);
		animState = RESULT_ANIM_STATE.MISSION;
		MissionClearStart(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected void MissionClearStart(Action callback)
	{
		if (!isValidMissionNewClearAnim)
		{
			PlayTween((Enum)UI.OBJ_MISSION_ROOT, true, (EventDelegate.Callback)delegate
			{
				if (callback != null)
				{
					callback.Invoke();
				}
			}, true, 0);
		}
		else
		{
			PlayTween((Enum)UI.OBJ_MISSION_ROOT, true, (EventDelegate.Callback)null, false, 0);
			PlayTween((Enum)UI.OBJ_MISSION_NEW_CLEAR_ROOT, true, (EventDelegate.Callback)delegate
			{
				if (callback != null)
				{
					callback.Invoke();
				}
			}, false, 0);
		}
	}

	protected virtual void TreasureStart()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		animState = RESULT_ANIM_STATE.TREASURE;
		PlayAudio(AUDIO.MONEY);
		PlayAudio(AUDIO.MONEY_WH);
		PlayAudio(AUDIO.ACHIEVEMENT);
		if (pointShopResultData.Count > 0)
		{
			foreach (Transform item in GetCtrl(UI.OBJ_POINT_SHOP_RESULT_ROOT).get_transform())
			{
				Transform t = item;
				PlayTween(t, true, null, true, 0);
			}
		}
		PlayTween((Enum)UI.OBJ_TREASURE_ROOT, true, (EventDelegate.Callback)delegate
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
				animTimer += Time.get_deltaTime();
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
							SetScroll((Enum)dropItemSCR, animScrollValue);
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
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (index < dropItemIconData.Length && dropItemIconData[index] != null && dropItemIconData[index].GetTableID() != 0)
		{
			Transform child = GetChild((Enum)dropItemGRD, index);
			if (!(child == null))
			{
				QuestResultDropIconOpener componentInChildren = child.get_gameObject().GetComponentInChildren<QuestResultDropIconOpener>();
				if (!(componentInChildren == null))
				{
					PlayAudio(AUDIO.DROPITEM);
					componentInChildren.StartEffect(is_skip);
				}
			}
		}
	}

	protected void OpenedIconEndEff(int index)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Transform child = GetChild((Enum)dropItemGRD, index);
		if (!(child == null))
		{
			QuestResultDropIconOpener componentInChildren = child.get_gameObject().GetComponentInChildren<QuestResultDropIconOpener>();
			if (!(componentInChildren == null))
			{
				componentInChildren.StartEffect(true);
			}
		}
	}

	protected unsafe virtual void AnimationEnd()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		animState = RESULT_ANIM_STATE.END;
		animationEnd = true;
		OpenMissionClearRewardDialog(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected virtual void VisibleEndButton()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
			{
				SetActive((Enum)UI.BTN_END_HUNT_LEFT, animationEnd);
				SetActive((Enum)UI.BTN_REPEAT_HUNT, animationEnd);
				this.StartCoroutine("WaitForRepeatHunt");
			}
			else
			{
				SetActive((Enum)UI.BTN_END_HUNT_CENTER, animationEnd);
				this.StartCoroutine("WaitForHost");
			}
		}
		else
		{
			SetActive((Enum)UI.BTN_NEXT, animationEnd);
		}
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, false);
		SetActive((Enum)UI.BTN_SKIP_IN_SCROLL, false);
		SetActive((Enum)UI.BTN_SKIP_IN_SCROLL_2, false);
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
					SetScroll((Enum)dropItemSCR, -100f);
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
				SkipTween((Enum)UI.OBJ_GET_EXP_ROOT, true, 0);
				expGauge.Skip();
				break;
			case RESULT_ANIM_STATE.LVUP:
				MonoBehaviourSingleton<UIManager>.I.levelUp.SkipAnim();
				break;
			case RESULT_ANIM_STATE.MISSION:
				animState = RESULT_ANIM_STATE.MISSION_REWARD;
				SkipTween((Enum)UI.OBJ_MISSION_ROOT, true, 0);
				SkipTween((Enum)UI.OBJ_MISSION_NEW_CLEAR_ROOT, true, 0);
				break;
			case RESULT_ANIM_STATE.TREASURE:
				SkipTween((Enum)UI.OBJ_TREASURE_ROOT, true, 0);
				SkipTween((Enum)UI.OBJ_POINT_SHOP_RESULT_ROOT, true, 0);
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

	protected unsafe void OpenDropSell(Action callback)
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if (!isNeedOpenDropSellDialog)
		{
			if (callback != null)
			{
				callback.Invoke();
			}
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
				_003COpenDropSell_003Ec__AnonStorey2B4 _003COpenDropSell_003Ec__AnonStorey2B;
				this.StartCoroutine(ExecEndDialogEvent(GetSceneName(), new Action((object)_003COpenDropSell_003Ec__AnonStorey2B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (dropSellCallback != null)
		{
			this.StartCoroutine(OnCloseDropSellClearCoroutine());
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
			dropSellCallback.Invoke();
		}
	}

	protected unsafe void OpenPointEvent(Action callback)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (!isNeedPointResult)
		{
			if (callback != null)
			{
				callback.Invoke();
			}
		}
		else
		{
			pointEventCallback = callback;
			if (!IsExecuteNowSceneEvent(GetSceneName()))
			{
				this.StartCoroutine(ExecEndDialogEvent(GetSceneName(), new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
			}
			else
			{
				DispatchEvent("CARNIVAL_POINT", MonoBehaviourSingleton<QuestManager>.I.compData.pointEvent[0]);
			}
		}
	}

	protected void OnCloseDialog_QuestResultPointEvent()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (pointEventCallback != null)
		{
			this.StartCoroutine(OnCloseQuestResultPointEventClearCoroutine());
		}
	}

	protected void OnCloseDialog_CarnivalResultPoint()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (pointEventCallback != null)
		{
			this.StartCoroutine(OnCloseQuestResultPointEventClearCoroutine());
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
			pointEventCallback.Invoke();
		}
	}

	private unsafe void OpenMissionClearRewardDialog(Action end_callback)
	{
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		bool flag = animState == RESULT_ANIM_STATE.MISSION || animState == RESULT_ANIM_STATE.END;
		if (!isNeedOpenMissionClearDialog || !flag || isOpenedMissionClearDialog)
		{
			if (end_callback != null)
			{
				end_callback.Invoke();
			}
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
					if (end_callback != null)
					{
						end_callback.Invoke();
					}
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
				if (end_callback != null)
				{
					end_callback.Invoke();
				}
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
					_003COpenMissionClearRewardDialog_003Ec__AnonStorey2B5 _003COpenMissionClearRewardDialog_003Ec__AnonStorey2B;
					this.StartCoroutine(ExecEndDialogEvent(GetSceneName(), new Action((object)_003COpenMissionClearRewardDialog_003Ec__AnonStorey2B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (missionClearRewardCallback != null)
		{
			this.StartCoroutine(OnCloseMissionClearCoroutine());
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

	private unsafe void OpenMutualFollowBonusDialog(Action end_callback)
	{
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		bool flag = animState == RESULT_ANIM_STATE.FOLLOW_BONUS || animState == RESULT_ANIM_STATE.END;
		if (!isNeedOpenFollowBonusDialog || !flag)
		{
			if (end_callback != null)
			{
				end_callback.Invoke();
			}
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
				if (end_callback != null)
				{
					end_callback.Invoke();
				}
			}
			else
			{
				followBonusCallback = end_callback;
				if (!IsExecuteNowSceneEvent(GetSceneName()))
				{
					_003COpenMutualFollowBonusDialog_003Ec__AnonStorey2B6 _003COpenMutualFollowBonusDialog_003Ec__AnonStorey2B;
					this.StartCoroutine(ExecEndDialogEvent(GetSceneName(), new Action((object)_003COpenMutualFollowBonusDialog_003Ec__AnonStorey2B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
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
			if (end_callback != null)
			{
				end_callback.Invoke();
			}
		}
	}

	protected void OnCloseDialog_QuestResultMutualFollowBonusDialog()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (followBonusCallback != null)
		{
			this.StartCoroutine(OnCloseFollowBonusCoroutine());
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
			endCallback.Invoke();
		}
		else
		{
			OpenEventRewardDialog(eventRewardList[eventRewardIndex], eventRewardTitles[eventRewardIndex], endCallback);
			eventRewardIndex++;
		}
	}

	protected unsafe void OpenEventRewardDialog(QuestCompleteReward reward, string title, Action end_callback)
	{
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
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
			if (end_callback != null)
			{
				end_callback.Invoke();
			}
		}
		else
		{
			eventRewardCallback = end_callback;
			if (!IsExecuteNowSceneEvent(GetSceneName()))
			{
				_003COpenEventRewardDialog_003Ec__AnonStorey2B7 _003COpenEventRewardDialog_003Ec__AnonStorey2B;
				this.StartCoroutine(ExecEndDialogEvent(GetSceneName(), new Action((object)_003COpenEventRewardDialog_003Ec__AnonStorey2B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (eventRewardCallback != null)
		{
			this.StartCoroutine(OnCloseEventRewardCoroutine());
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
			eventRewardCallback.Invoke();
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

	protected unsafe void OpenFirstClearRewardDialog(Action callback)
	{
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		if (!isNeedOpenFirstClearDialog)
		{
			if (callback != null)
			{
				callback.Invoke();
			}
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
				_003COpenFirstClearRewardDialog_003Ec__AnonStorey2B8 _003COpenFirstClearRewardDialog_003Ec__AnonStorey2B;
				this.StartCoroutine(ExecEndDialogEvent(GetSceneName(), new Action((object)_003COpenFirstClearRewardDialog_003Ec__AnonStorey2B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (firstClearRewardCallback != null)
		{
			this.StartCoroutine(OnCloseFirstClearCoroutine());
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
			firstClearRewardCallback.Invoke();
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
		callback.Invoke();
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
		this.StopCoroutine("AutoJoinParty");
		this.StopCoroutine("WaitForHost");
		DispatchEvent("FRIEND", null);
	}

	protected void OnQuery_END_HUNT()
	{
		this.StopCoroutine("WaitForRepeatHunt");
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendRepeat(false, delegate(bool is_success)
		{
			GameSection.ChangeStayEvent("FRIEND", null);
			GameSection.ResumeEvent(is_success, null);
		});
	}

	protected void OnQuery_REPEAT_HUNT()
	{
		this.StopCoroutine("WaitForRepeatHunt");
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
		SetLabelText((Enum)UI.LBL_WAIT_FOR_HOST, "Wait For Host...");
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
		SetLabelText((Enum)UI.LBL_BTN_REPEAT_HUNT, "Repeat(0s)");
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
