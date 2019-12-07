using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSpecialSelect : GameSection
{
	protected enum UI
	{
		TEX_NPCMODEL,
		OBJ_NPC_MESSAGE,
		LBL_NPC_MESSAGE,
		BTN_SEARCH,
		BTN_EVENT,
		BTN_SORT,
		LBL_SORT,
		TGL_ICON_ASC,
		SPR_FRAME,
		SPR_BG_BTN_CLOSE,
		BTN_INPUT_CLOSE,
		BTN_INPUT_CLOSE_BG,
		BG,
		TIELEBAR,
		FRAMEDOWN,
		FRAMEUP,
		LIST_BASE,
		DELIVERY_SCROLLBAR_OVER,
		DELIVERY_SCROLLBAR_BASE,
		OBJ_ORDER_ROOT,
		BTN_ORDER,
		SPR_ORDER_TEXT,
		SPR_ORDER_ICON,
		OBJ_DELIVERY_ROOT,
		BTN_DELIVERY,
		SPR_DELIVERY_TEXT,
		SPR_DELIVERY_ICON,
		OBJ_QUEST_ROOT,
		BTN_QUEST,
		SPR_QUEST_TEXT,
		SPR_QUEST_ICON,
		STR_ORDER_NON_LIST,
		GRD_ORDER_QUEST,
		SCR_ORDER_QUEST2,
		LBL_DELIVERY_NON_LIST,
		SCR_DELIVERY_QUEST,
		GRD_DELIVERY,
		BTN_TAB_NORMAL,
		BTN_TAB_DAILY,
		BTN_TAB_WEEKLY,
		SPR_NEW,
		SPR_TAB_NORMAL,
		SPR_TAB_DAILY,
		SPR_TAB_WEEKLY,
		GRD_QUEST,
		STR_QUEST_NON_LIST,
		GRD_ICON_ROOT,
		OBJ_ICON_ROOT_1,
		OBJ_ICON_ROOT_2,
		OBJ_BUTTON_ROOT,
		SCR_ORDER_QUEST,
		SPR_ORDER_RARITY_FRAME,
		LBL_ORDER_NUM,
		LBL_REMAIN,
		TEX_NPC,
		LBL_DELIVERY_COMMENT,
		LBL_NEED_ITEM_NAME,
		LBL_HAVE,
		LBL_NEED,
		LBL_LIMIT,
		OBJ_REQUEST_OK,
		OBJ_REQUEST_COMPLETED,
		SPR_TYPE_STORY,
		SPR_TYPE_EVENT,
		SPR_TYPE_EVENT_TEXT,
		SPR_TYPE_DAILY_TEXT,
		SPR_TYPE_WEEKLY_TEXT,
		SPR_TYPE_HARD,
		SPR_TYPE_NORMAL,
		SPR_TYPE_SUB_EVENT,
		SPR_DIFFICULTY_EASY,
		SPR_DIFFICULTY_NORMAL,
		SPR_DIFFICULTY_HARD,
		SPR_DROP_DIFFICULTY_RARE,
		SPR_DROP_DIFFICULTY_SUPER_RARE,
		SCR_NORMAL_QUEST,
		OBJ_ROT_SPRITE,
		LBL_QUEST_NAME,
		LBL_QUEST_NUM,
		OBJ_MISSION_INFO_ROOT,
		OBJ_TOP_CROWN_1,
		OBJ_TOP_CROWN_2,
		OBJ_TOP_CROWN_3,
		SPR_CROWN_1,
		SPR_CROWN_2,
		SPR_CROWN_3,
		OBJ_ENEMY,
		SPR_MONSTER_ICON,
		SPR_ELEMENT_ROOT,
		SPR_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		TWN_DIFFICULT_STAR,
		OBJ_DIFFICULT_STAR_1,
		OBJ_DIFFICULT_STAR_2,
		OBJ_DIFFICULT_STAR_3,
		OBJ_DIFFICULT_STAR_4,
		OBJ_DIFFICULT_STAR_5,
		OBJ_DIFFICULT_STAR_6,
		OBJ_DIFFICULT_STAR_7,
		OBJ_DIFFICULT_STAR_8,
		OBJ_DIFFICULT_STAR_9,
		OBJ_DIFFICULT_STAR_10,
		OBJ_ICON,
		OBJ_ICON_NEW,
		OBJ_ICON_CLEARED,
		OBJ_ICON_COMPLETE,
		SPR_ICON_NEW,
		SPR_ICON_CLEARED,
		SPR_ICON_COMPLETE,
		OBJ_BANNER_ROOT,
		SPR_BANNER,
		PORTRAIT_SPR_BG_BTN_CLOSE,
		PORTRAIT_BTN_INPUT_CLOSE,
		PORTRAIT_BTN_INPUT_CLOSE_BG,
		PORTRAIT_OBJ_BUTTON_ROOT,
		PORTRAIT_BG,
		PORTRAIT_TIELEBAR,
		PORTRAIT_FRAMEDOWN,
		PORTRAIT_FRAMEUP,
		PORTRAIT_OBJ_DELIVERY_ROOT,
		PORTRAIT_LIST_BASE,
		PORTRAIT_STR_DELIVERY_NON_LIST,
		PORTRAIT_SCR_DELIVERY_QUEST,
		PORTRAIT_DELIVERY_SCROLLBAR,
		LANDSCAPE_SPR_BG_BTN_CLOSE,
		LANDSCAPE_BTN_INPUT_CLOSE,
		LANDSCAPE_BTN_INPUT_CLOSE_BG,
		LANDSCAPE_OBJ_BUTTON_ROOT,
		LANDSCAPE_BG,
		LANDSCAPE_TIELEBAR,
		LANDSCAPE_FRAMEDOWN,
		LANDSCAPE_FRAMEUP,
		LANDSCAPE_OBJ_DELIVERY_ROOT,
		LANDSCAPE_LIST_BASE,
		LANDSCAPE_STR_DELIVERY_NON_LIST,
		LANDSCAPE_SCR_DELIVERY_QUEST,
		LANDSCAPE_DELIVERY_SCROLLBAR,
		TEX_AREA_BANNER,
		SPR_CLEARED_AREA,
		SPR_NEW_AREA,
		GRD_AREA,
		OBJ_DELIVERY_BAR,
		OBJ_AREA_BAR,
		LBL_DELIVERY_NUM,
		LBL_DELIVERY_REST,
		SCR_AREA
	}

	public enum SHOW_MODE
	{
		DELIVERY,
		ORDER,
		QUEST
	}

	private struct AreaQuestInfo
	{
		public RegionTable.Data regionData;

		public Texture2D bannerTex;

		public bool cleared;

		public bool opened;

		public AreaQuestInfo(RegionTable.Data data, Texture2D tex, bool cleared, bool opened)
		{
			regionData = data;
			bannerTex = tex;
			this.cleared = cleared;
			this.opened = opened;
		}
	}

	protected SHOW_MODE showMode;

	private const string SPR_QUEST_TAB_TEXT = "QuestTabBtnText";

	private const string SPR_QUEST_TAB_ICON = "QuestTabBtnIcon";

	private const string SPR_QUEST_TAB_BASE = "QuestTabBtnBase";

	private readonly string[] SPR_INDEX = new string[3]
	{
		"01",
		"02",
		"03"
	};

	private readonly string[] SPR_ON_OFF = new string[3]
	{
		"_on",
		"_off",
		"_Gray"
	};

	private SortSettings sortSettings;

	private QuestSortData[] questSortData;

	private QuestItemInfo[] questItemAry;

	protected Delivery[] deliveryInfo;

	protected Delivery[] normalDeliveryInfo;

	private Delivery[] dailyDeliveryInfo;

	private Delivery[] weeklyDeliveryInfo;

	private readonly string[] SPR_FRAME_TYPE = new string[5]
	{
		"RequestPlate_Base",
		"RequestPlate_Event",
		"RequestPlate_Story",
		"RequestPlate_Hard",
		"RequestPlate_SubEvent"
	};

	private const string spriteTabName_ON = "PickeShopBtn_Green_on";

	private const string spriteTabName_OFF = "PickeShopBtn_Normal_off";

	private QuestInfoData[] questInfo;

	private UI[] ui_top_crown = new UI[3]
	{
		UI.OBJ_TOP_CROWN_1,
		UI.OBJ_TOP_CROWN_2,
		UI.OBJ_TOP_CROWN_3
	};

	private UI[] ui_crown = new UI[3]
	{
		UI.SPR_CROWN_1,
		UI.SPR_CROWN_2,
		UI.SPR_CROWN_3
	};

	protected bool isInGameScene;

	private string npcText = string.Empty;

	private UI[] difficult = new UI[10]
	{
		UI.OBJ_DIFFICULT_STAR_1,
		UI.OBJ_DIFFICULT_STAR_2,
		UI.OBJ_DIFFICULT_STAR_3,
		UI.OBJ_DIFFICULT_STAR_4,
		UI.OBJ_DIFFICULT_STAR_5,
		UI.OBJ_DIFFICULT_STAR_6,
		UI.OBJ_DIFFICULT_STAR_7,
		UI.OBJ_DIFFICULT_STAR_8,
		UI.OBJ_DIFFICULT_STAR_9,
		UI.OBJ_DIFFICULT_STAR_10
	};

	private DELIVERY_TYPE[][] TAB_TYPES = new DELIVERY_TYPE[3][]
	{
		new DELIVERY_TYPE[2]
		{
			DELIVERY_TYPE.STORY,
			DELIVERY_TYPE.ONCE
		},
		new DELIVERY_TYPE[1],
		new DELIVERY_TYPE[1]
		{
			DELIVERY_TYPE.WEEKLY
		}
	};

	protected UI selectedTab = UI.BTN_TAB_NORMAL;

	private bool isDeliveryGridReset = true;

	private List<Texture2D> areaBanners = new List<Texture2D>();

	private uint[] openRegionIds;

	private uint[] validRegionIds;

	protected int releaseRegionId = -1;

	protected bool changeToDeliveryClearEvent;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "DeliveryRewardTable";
			yield return "FieldMapTable";
		}
	}

	public override void Initialize()
	{
		isInGameScene = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene");
		StartCoroutine(DoInitialize());
	}

	protected virtual IEnumerator DoInitialize()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		if (!isInGameScene)
		{
			bool is_recv_delivery2 = false;
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_new");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_complete");
			MonoBehaviourSingleton<QuestManager>.I.SendGetDeliveryList(delegate
			{
				is_recv_delivery2 = true;
			});
			while (!is_recv_delivery2)
			{
				yield return null;
			}
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.QUEST, SortSettings.SETTINGS_TYPE.ORDER_QUEST);
			InitQuestInfoData();
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
		}
		GetDeliveryList();
		bool is_recv_delivery = false;
		MonoBehaviourSingleton<DeliveryManager>.I.SendEventNormalList(delegate
		{
			is_recv_delivery = true;
		});
		while (!is_recv_delivery)
		{
			yield return null;
		}
		switch (GameSection.GetEventData() as string)
		{
		case "DAILY":
			selectedTab = UI.BTN_TAB_DAILY;
			break;
		case "WEEKLY":
			selectedTab = UI.BTN_TAB_WEEKLY;
			break;
		case "NORMAL":
			selectedTab = UI.BTN_TAB_NORMAL;
			break;
		default:
			selectedTab = SelectTabByPriority();
			break;
		}
		openRegionIds = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE.NORMAL);
		validRegionIds = MonoBehaviourSingleton<WorldMapManager>.I.GetValidRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE.NORMAL);
		Array.Reverse((Array)validRegionIds);
		for (int i = 0; i < validRegionIds.Length; i++)
		{
			int regionId = (int)validRegionIds[i];
			LoadObject bannerObj;
			if (MonoBehaviourSingleton<WorldMapManager>.I.IsOpenRegion((uint)regionId))
			{
				string areaBanner = ResourceName.GetAreaBanner(regionId);
				bannerObj = load_queue.Load(RESOURCE_CATEGORY.AREA_BANNER, areaBanner);
			}
			else
			{
				string areaBanner = ResourceName.GetCloseAreaBanner(regionId);
				bannerObj = load_queue.Load(RESOURCE_CATEGORY.AREA_BANNER_CLOSE, areaBanner);
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			Texture2D item = bannerObj.loadedObject as Texture2D;
			areaBanners.Add(item);
		}
		EndInitialize();
	}

	private UI SelectTabByPriority()
	{
		if (normalDeliveryInfo != null)
		{
			int i = 0;
			for (int num = normalDeliveryInfo.Length; i < num; i++)
			{
				if (Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)normalDeliveryInfo[i].dId).type == DELIVERY_TYPE.STORY)
				{
					return UI.BTN_TAB_NORMAL;
				}
			}
		}
		if (dailyDeliveryInfo != null && dailyDeliveryInfo.Length >= 1)
		{
			return UI.BTN_TAB_DAILY;
		}
		if (normalDeliveryInfo != null && normalDeliveryInfo.Length >= 1)
		{
			return UI.BTN_TAB_NORMAL;
		}
		if (weeklyDeliveryInfo != null && weeklyDeliveryInfo.Length >= 1)
		{
			return UI.BTN_TAB_WEEKLY;
		}
		return UI.BTN_TAB_DAILY;
	}

	protected void EndInitialize()
	{
		base.Initialize();
	}

	private void InitQuestInfoData()
	{
		questInfo = MonoBehaviourSingleton<QuestManager>.I.GetQuestInfoData();
		if (questInfo != null)
		{
			Array.Sort(questInfo, (QuestInfoData l, QuestInfoData r) => (int)(r.questData.tableData.questID - l.questData.tableData.questID));
		}
	}

	private void Update()
	{
		if (MonoBehaviourSingleton<DeliveryManager>.I.dailyUpdateRemainTime > 0f && MonoBehaviourSingleton<DeliveryManager>.I.weeklyUpdateRemainTime > 0f)
		{
			MonoBehaviourSingleton<DeliveryManager>.I.dailyUpdateRemainTime -= Time.deltaTime;
			MonoBehaviourSingleton<DeliveryManager>.I.weeklyUpdateRemainTime -= Time.deltaTime;
			ShowNonDeliveryList();
		}
	}

	public override void UpdateUI()
	{
		if (!changeToDeliveryClearEvent)
		{
			ShowSelectUI();
			UpdateAnchors();
			SetBadge(UI.BTN_EVENT, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(), SpriteAlignment.TopLeft, 24, -10, is_scale_normalize: true);
			SetBadge(UI.BTN_TAB_NORMAL, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(TAB_TYPES[0]), SpriteAlignment.TopRight, -10, -10, is_scale_normalize: true);
			SetBadge(UI.BTN_TAB_DAILY, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(TAB_TYPES[1]), SpriteAlignment.TopRight, -10, -10, is_scale_normalize: true);
			SetBadge(UI.BTN_TAB_WEEKLY, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(TAB_TYPES[2]), SpriteAlignment.TopRight, -10, -10, is_scale_normalize: true);
			SetNew(UI.BTN_TAB_DAILY, GameSaveData.instance.IsRecommendedDailyDeliveryCheck());
			SetNew(UI.BTN_TAB_WEEKLY, GameSaveData.instance.IsRecommendedWeeklyDeliveryCheck());
		}
	}

	private void SetNew(UI btn, bool is_visible)
	{
		SetActive(GetCtrl(btn), UI.SPR_NEW, is_visible);
	}

	private void OpenTutorial()
	{
		if (!isInGameScene && HomeTutorialManager.DoesTutorial())
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep2_1");
		}
	}

	protected void SetupDeliveryListItem(Transform t, DeliveryTable.DeliveryData info)
	{
		QuestRequestItem questRequestItem = t.GetComponent<QuestRequestItem>();
		if (questRequestItem == null)
		{
			questRequestItem = t.gameObject.AddComponent<QuestRequestItem>();
		}
		questRequestItem.InitUI();
		questRequestItem.Setup(t, info);
	}

	protected void SetCompletedHaveCount(Transform t, DeliveryTable.DeliveryData info)
	{
		MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryDataAllNeeds((int)info.id, out int _, out int need, out string _, out string _);
		SetLabelText(t, UI.LBL_HAVE, need.ToString());
	}

	protected virtual void ShowSelectUI()
	{
		int num = (!isInGameScene && TutorialStep.HasQuestSpecialUnlocked()) ? 1 : 2;
		int num2 = (showMode != 0) ? num : 0;
		int num3 = (showMode != SHOW_MODE.QUEST) ? num : 0;
		int num4 = (showMode != SHOW_MODE.ORDER) ? num : 0;
		SetActive(UI.OBJ_DELIVERY_ROOT, showMode == SHOW_MODE.DELIVERY);
		SetActive(UI.OBJ_QUEST_ROOT, showMode == SHOW_MODE.QUEST);
		SetActive(UI.OBJ_ORDER_ROOT, showMode == SHOW_MODE.ORDER);
		SetButtonSprite(UI.BTN_DELIVERY, "QuestTabBtnBase" + SPR_ON_OFF[num2], with_press: true);
		SetSprite(UI.SPR_DELIVERY_TEXT, "QuestTabBtnText" + SPR_INDEX[0] + SPR_ON_OFF[num2]);
		SetSprite(UI.SPR_DELIVERY_ICON, "QuestTabBtnIcon" + SPR_INDEX[0] + SPR_ON_OFF[num2]);
		SetButtonSprite(UI.BTN_QUEST, "QuestTabBtnBase" + SPR_ON_OFF[num3], with_press: true);
		SetSprite(UI.SPR_QUEST_TEXT, "QuestTabBtnText" + SPR_INDEX[1] + SPR_ON_OFF[num3]);
		SetSprite(UI.SPR_QUEST_ICON, "QuestTabBtnIcon" + SPR_INDEX[1] + SPR_ON_OFF[num3]);
		SetButtonSprite(UI.BTN_ORDER, "QuestTabBtnBase" + SPR_ON_OFF[num4], with_press: true);
		SetSprite(UI.SPR_ORDER_TEXT, "QuestTabBtnText" + SPR_INDEX[2] + SPR_ON_OFF[num4]);
		SetSprite(UI.SPR_ORDER_ICON, "QuestTabBtnIcon" + SPR_INDEX[2] + SPR_ON_OFF[num4]);
		if (!TutorialStep.HasQuestSpecialUnlocked())
		{
			SetButtonEnabled(UI.BTN_QUEST, is_enabled: false);
			SetButtonEnabled(UI.BTN_ORDER, is_enabled: false);
			SetActive(UI.BTN_EVENT, is_visible: false);
		}
		if (!isInGameScene)
		{
			SetRenderNPCModel(UI.TEX_NPCMODEL, 0, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV);
			SetLabelText(UI.LBL_NPC_MESSAGE, npcText);
		}
		SetActive(UI.OBJ_NPC_MESSAGE, !isInGameScene);
		if (showMode == SHOW_MODE.DELIVERY)
		{
			SetActive(UI.SPR_TAB_NORMAL, UI.BTN_TAB_NORMAL == selectedTab);
			SetActive(UI.SPR_TAB_DAILY, UI.BTN_TAB_DAILY == selectedTab);
			SetActive(UI.SPR_TAB_WEEKLY, UI.BTN_TAB_WEEKLY == selectedTab);
			SetButtonSprite(UI.BTN_TAB_NORMAL, (UI.BTN_TAB_NORMAL == selectedTab) ? "PickeShopBtn_Green_on" : "PickeShopBtn_Normal_off");
			SetButtonSprite(UI.BTN_TAB_DAILY, (UI.BTN_TAB_DAILY == selectedTab) ? "PickeShopBtn_Green_on" : "PickeShopBtn_Normal_off");
			SetButtonSprite(UI.BTN_TAB_WEEKLY, (UI.BTN_TAB_WEEKLY == selectedTab) ? "PickeShopBtn_Green_on" : "PickeShopBtn_Normal_off");
			GetCtrl(UI.BTN_TAB_NORMAL).gameObject.GetComponent<BoxCollider>().enabled = (UI.BTN_TAB_NORMAL != selectedTab);
			GetCtrl(UI.BTN_TAB_DAILY).gameObject.GetComponent<BoxCollider>().enabled = (UI.BTN_TAB_DAILY != selectedTab);
			GetCtrl(UI.BTN_TAB_WEEKLY).gameObject.GetComponent<BoxCollider>().enabled = (UI.BTN_TAB_WEEKLY != selectedTab);
			SetNPCMessage(selectedTab);
			switch (selectedTab)
			{
			case UI.BTN_TAB_NORMAL:
				SetDeliveryList(normalDeliveryInfo);
				break;
			case UI.BTN_TAB_DAILY:
				SetDeliveryList(dailyDeliveryInfo);
				break;
			case UI.BTN_TAB_WEEKLY:
				SetDeliveryList(weeklyDeliveryInfo);
				break;
			}
		}
		else if (showMode == SHOW_MODE.ORDER)
		{
			if (questItemAry == null && MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() > 0)
			{
				List<QuestItemInfo> list = new List<QuestItemInfo>();
				MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory(delegate(QuestItemInfo item)
				{
					if (item.infoData.questData.num > 0)
					{
						list.Add(item);
					}
				});
				questItemAry = list.ToArray();
				GetCtrl(UI.GRD_ORDER_QUEST).DestroyChildren();
			}
			if (questItemAry == null || questItemAry.Length == 0)
			{
				SetActive(UI.BTN_SORT, is_visible: false);
				SetActive(UI.GRD_ORDER_QUEST, is_visible: false);
				SetActive(UI.STR_ORDER_NON_LIST, is_visible: true);
				return;
			}
			questSortData = sortSettings.CreateSortAry<QuestItemInfo, QuestSortData>(questItemAry);
			SetActive(UI.GRD_ORDER_QUEST, is_visible: true);
			SetActive(UI.STR_ORDER_NON_LIST, is_visible: false);
			SetActive(UI.BTN_SORT, is_visible: true);
			SetLabelText(UI.LBL_SORT, sortSettings.GetSortLabel());
			SetToggle(UI.TGL_ICON_ASC, sortSettings.orderTypeAsc);
			SetDynamicList(UI.GRD_ORDER_QUEST, "QuestListOrderItem", questSortData.Length, reset: false, null, null, delegate(int i, Transform t, bool is_recycle)
			{
				SetActive(t, is_visible: true);
				SetEvent(t, "SELECT_ORDER", i);
				QuestInfoData info2 = this.questSortData[i].itemData.infoData;
				int num9 = (int)(info2.questData.tableData.difficulty + 1);
				int l = 0;
				for (int num10 = difficult.Length; l < num10; l++)
				{
					SetActive(t, difficult[l], l < num9);
				}
				if (!is_recycle)
				{
					ResetTween(t, UI.TWN_DIFFICULT_STAR);
					PlayTween(t, UI.TWN_DIFFICULT_STAR, forward: true, null, is_input_block: false);
				}
				EnemyTable.EnemyData enemyData2 = Singleton<EnemyTable>.I.GetEnemyData((uint)info2.questData.tableData.GetMainEnemyID());
				QuestSortData questSortData = this.questSortData[i];
				ItemIcon.Create(questSortData.GetIconType(), questSortData.GetIconID(), questSortData.GetRarity(), FindCtrl(t, UI.OBJ_ENEMY), questSortData.GetIconElement()).SetEnableCollider(is_enable: false);
				SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData2.element != ELEMENT_TYPE.MAX);
				SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData2.element);
				SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData2.weakElement);
				SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData2.weakElement == ELEMENT_TYPE.MAX);
				SetLabelText(t, UI.LBL_QUEST_NAME, info2.questData.tableData.questText);
				int num11 = 1;
				ClearStatusQuest clearStatusQuest2 = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => info2.questData.tableData.questID == data.questId);
				if (clearStatusQuest2 != null)
				{
					num11 = clearStatusQuest2.questStatus;
				}
				int value2 = i + 100;
				SetToggleGroup(t, UI.OBJ_ICON_NEW, value2);
				SetToggleGroup(t, UI.OBJ_ICON_CLEARED, value2);
				SetToggleGroup(t, UI.OBJ_ICON_COMPLETE, value2);
				switch (num11)
				{
				default:
					SetToggle(t, UI.OBJ_ICON_NEW, value: false);
					SetToggle(t, UI.OBJ_ICON_CLEARED, value: false);
					SetToggle(t, UI.OBJ_ICON_COMPLETE, value: false);
					break;
				case 1:
					SetToggle(t, UI.OBJ_ICON_NEW, value: true);
					SetVisibleWidgetEffect(UI.SCR_ORDER_QUEST, t, UI.SPR_ICON_NEW, "ef_ui_questselect_new");
					break;
				case 3:
					SetToggle(t, UI.OBJ_ICON_CLEARED, value: true);
					break;
				case 4:
					SetToggle(t, UI.OBJ_ICON_COMPLETE, value: true);
					SetVisibleWidgetEffect(UI.SCR_ORDER_QUEST, t, UI.SPR_ICON_COMPLETE, "ef_ui_questselect_complete");
					break;
				}
				SetLabelText(t, UI.LBL_ORDER_NUM, info2.questData.num.ToString());
				SetActive(t, UI.LBL_REMAIN, is_visible: false);
			});
		}
		else if (showMode == SHOW_MODE.QUEST)
		{
			if (questInfo == null || questInfo.Length == 0)
			{
				SetActive(UI.GRD_QUEST, is_visible: false);
				SetActive(UI.STR_QUEST_NON_LIST, is_visible: true);
			}
			else
			{
				SetActive(UI.STR_QUEST_NON_LIST, is_visible: false);
				SetActive(UI.GRD_QUEST, is_visible: true);
				SetDynamicList(UI.GRD_QUEST, "QuestListItem", questInfo.Length, reset: false, null, null, delegate(int i, Transform t, bool is_recycle)
				{
					SetEvent(t, "SELECT_QUEST", i);
					QuestInfoData info = questInfo[i];
					int num5 = (int)(info.questData.tableData.difficulty + 1);
					int j = 0;
					for (int num6 = difficult.Length; j < num6; j++)
					{
						SetActive(t, difficult[j], j < num5);
					}
					if (!is_recycle)
					{
						ResetTween(t, UI.TWN_DIFFICULT_STAR);
						PlayTween(t, UI.TWN_DIFFICULT_STAR, forward: true, null, is_input_block: false);
					}
					EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)info.questData.tableData.GetMainEnemyID());
					if (enemyData != null)
					{
						SetActive(t, UI.OBJ_ENEMY, is_visible: true);
						int iconId = enemyData.iconId;
						RARITY_TYPE? rarity = (info.questData.tableData.questType == QUEST_TYPE.ORDER) ? new RARITY_TYPE?(info.questData.tableData.rarity) : null;
						ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, rarity, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(is_enable: false);
						SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
						SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
						SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
						SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
					}
					else
					{
						SetActive(t, UI.OBJ_ENEMY, is_visible: false);
						SetElementSprite(t, UI.SPR_WEAK_ELEMENT, 6);
						SetActive(t, UI.STR_NON_WEAK_ELEMENT, is_visible: true);
					}
					SetLabelText(t, UI.LBL_QUEST_NUM, string.Empty);
					SetLabelText(t, UI.LBL_QUEST_NAME, info.questData.tableData.questText);
					if (!info.isExistMission)
					{
						SetActive(t, UI.OBJ_MISSION_INFO_ROOT, is_visible: false);
					}
					else
					{
						SetActive(t, UI.OBJ_MISSION_INFO_ROOT, is_visible: true);
						int k = 0;
						for (int num7 = info.missionData.Length; k < num7; k++)
						{
							SetActive(t, ui_top_crown[k], info.missionData[k] != null);
							if (info.missionData[k] != null)
							{
								SetActive(t, ui_crown[k], info.missionData[k].state >= CLEAR_STATUS.CLEAR);
							}
						}
					}
					int num8 = 1;
					ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => info.questData.tableData.questID == data.questId);
					if (clearStatusQuest != null)
					{
						num8 = clearStatusQuest.questStatus;
					}
					int value = i + 100;
					SetToggleGroup(t, UI.OBJ_ICON_NEW, value);
					SetToggleGroup(t, UI.OBJ_ICON_CLEARED, value);
					SetToggleGroup(t, UI.OBJ_ICON_COMPLETE, value);
					switch (num8)
					{
					default:
						SetToggle(t, UI.OBJ_ICON_NEW, value: false);
						SetToggle(t, UI.OBJ_ICON_CLEARED, value: false);
						SetToggle(t, UI.OBJ_ICON_COMPLETE, value: false);
						break;
					case 1:
						SetToggle(t, UI.OBJ_ICON_NEW, value: true);
						SetVisibleWidgetEffect(UI.SCR_NORMAL_QUEST, t, UI.SPR_ICON_NEW, "ef_ui_questselect_new");
						break;
					case 3:
						SetToggle(t, UI.OBJ_ICON_CLEARED, value: true);
						break;
					case 4:
						SetToggle(t, UI.OBJ_ICON_COMPLETE, value: true);
						SetVisibleWidgetEffect(UI.SCR_NORMAL_QUEST, t, UI.SPR_ICON_COMPLETE, "ef_ui_questselect_complete");
						break;
					}
				});
			}
		}
	}

	protected virtual void SetDeliveryList(Delivery[] list)
	{
		if (selectedTab == UI.BTN_TAB_NORMAL && openRegionIds.Length >= 2)
		{
			SetActive(UI.OBJ_DELIVERY_BAR, is_visible: false);
			SetActive(UI.GRD_DELIVERY, is_visible: false);
			SetActive(UI.LBL_DELIVERY_NON_LIST, is_visible: false);
			SetActive(UI.GRD_AREA, is_visible: true);
			SetActive(UI.OBJ_AREA_BAR, is_visible: true);
			RegionTable.Data data = null;
			List<AreaQuestInfo> areaInfos = new List<AreaQuestInfo>();
			for (int j = 0; j < validRegionIds.Length; j++)
			{
				bool flag = MonoBehaviourSingleton<WorldMapManager>.I.IsOpenRegion(validRegionIds[j]);
				data = Singleton<RegionTable>.I.GetData(validRegionIds[j]);
				if (!flag)
				{
					areaInfos.Add(new AreaQuestInfo(data, areaBanners[j], cleared: false, flag));
					continue;
				}
				bool cleared = IsCleardRegion(data);
				areaInfos.Add(new AreaQuestInfo(data, areaBanners[j], cleared, flag));
			}
			SetDynamicList(UI.GRD_AREA, "QuestAreaListItem", areaInfos.Count, reset: true, null, null, delegate(int i, Transform t, bool is_recycle)
			{
				AreaQuestInfo areaQuestInfo = areaInfos[i];
				Texture2D bannerTex = areaQuestInfo.bannerTex;
				if (bannerTex != null)
				{
					Transform t2 = FindCtrl(t, UI.TEX_AREA_BANNER);
					SetActive(t2, is_visible: true);
					SetTexture(t2, bannerTex);
				}
				RegionTable.Data regionData = areaQuestInfo.regionData;
				if (areaQuestInfo.opened)
				{
					SetEvent(t, "SELECT_AREA", (int)regionData.regionId);
				}
				else
				{
					SetEvent(t, "SELECT_CLOSE_AREA", (int)regionData.regionId);
				}
				int completableRegionDeliveryNum = MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableRegionDeliveryNum((int)regionData.regionId, regionData.groupId);
				SetBadge(FindCtrl(t, UI.TEX_AREA_BANNER), completableRegionDeliveryNum, SpriteAlignment.TopRight, -16, -3);
				SetActive(t, UI.SPR_CLEARED_AREA, areaQuestInfo.cleared);
				bool is_visible = false;
				if (areaQuestInfo.opened)
				{
					EventNormalListData eventNormalListData = null;
					RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(regionData.groupId, REGION_DIFFICULTY_TYPE.HARD);
					eventNormalListData = ((data2 == null || !MonoBehaviourSingleton<WorldMapManager>.I.IsOpenRegion(data2.regionId)) ? MonoBehaviourSingleton<DeliveryManager>.I.GetEventNormalListData((int)regionData.regionId) : MonoBehaviourSingleton<DeliveryManager>.I.GetEventNormalListData((int)data2.regionId));
					if (eventNormalListData != null)
					{
						if (eventNormalListData.numerator >= eventNormalListData.denominator)
						{
							SetLabelText(t, UI.LBL_DELIVERY_NUM, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 43u)));
						}
						else
						{
							SetLabelText(t, UI.LBL_DELIVERY_NUM, $"{eventNormalListData.denominator - eventNormalListData.numerator} Missions Left");
						}
						is_visible = true;
					}
				}
				SetActive(t, UI.LBL_DELIVERY_NUM, is_visible);
			});
			isDeliveryGridReset = false;
			RepositionAreaGrid();
		}
		else if (list != null)
		{
			SetActive(UI.OBJ_AREA_BAR, is_visible: false);
			SetActive(UI.GRD_AREA, is_visible: false);
			SetActive(UI.GRD_DELIVERY, list.Length != 0);
			SetActive(UI.LBL_DELIVERY_NON_LIST, list.Length == 0);
			SetActive(UI.OBJ_DELIVERY_BAR, is_visible: true);
			SetDynamicList(UI.GRD_DELIVERY, "QuestRequestItem", list.Length, isDeliveryGridReset, null, null, delegate(int i, Transform t, bool is_recycle)
			{
				SetActive(t, is_visible: true);
				int num = Array.FindIndex(deliveryInfo, (Delivery d) => d.uId == list[i].uId);
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[num].dId);
				if (deliveryTableData.subType == DELIVERY_SUB_TYPE.READ_STORY)
				{
					SetEvent(t, "READ_STORY", num);
				}
				else
				{
					SetEvent(t, "SELECT_DELIVERY", num);
				}
				SetupDeliveryListItem(t, deliveryTableData);
			});
			isDeliveryGridReset = false;
			ShowNonDeliveryList();
		}
	}

	private void RepositionAreaGrid()
	{
		UIScrollView component = GetCtrl(UI.SCR_AREA).GetComponent<UIScrollView>();
		UIPanel component2 = GetCtrl(UI.SCR_AREA).GetComponent<UIPanel>();
		uint num = openRegionIds[openRegionIds.Length - 1];
		int num2 = -1;
		int i = 0;
		for (int num3 = validRegionIds.Length; i < num3; i++)
		{
			if (num == validRegionIds[i])
			{
				num2 = i;
				break;
			}
		}
		if (num2 > -1 && num2 > 3)
		{
			int num4 = validRegionIds.Length;
			float num5 = (float)num2 / (float)num4;
			if (num5 >= 0.6f)
			{
				num2 += 2;
				num5 = (float)num2 / (float)num4;
			}
			component.SetDragAmount(0.5f, num5, updateScrollbars: true);
			Vector2 clipOffset = component2.clipOffset;
			component.transform.localPosition = -clipOffset;
		}
	}

	private bool IsCleardRegion(RegionTable.Data data)
	{
		int i = 0;
		for (int num = normalDeliveryInfo.Length; i < num; i++)
		{
			Delivery delivery = normalDeliveryInfo[i];
			if (delivery.dId != 0)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery.dId);
				if (deliveryTableData.regionId == data.regionId)
				{
					return false;
				}
				if (data.groupId > 0 && deliveryTableData.regionId == data.groupId)
				{
					return false;
				}
			}
		}
		return true;
	}

	private void ShowNonDeliveryList()
	{
		switch (selectedTab)
		{
		case UI.BTN_TAB_NORMAL:
			if (normalDeliveryInfo != null && normalDeliveryInfo.Length == 0)
			{
				SetLabelText(UI.LBL_DELIVERY_NON_LIST, StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 100u));
			}
			break;
		case UI.BTN_TAB_DAILY:
			if (dailyDeliveryInfo != null && dailyDeliveryInfo.Length == 0)
			{
				TimeSpan span2 = TimeSpan.FromSeconds(MonoBehaviourSingleton<DeliveryManager>.I.dailyUpdateRemainTime);
				SetLabelText(UI.LBL_DELIVERY_NON_LIST, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 101u), GetRemainTimeText(span2)));
			}
			break;
		case UI.BTN_TAB_WEEKLY:
			if (weeklyDeliveryInfo != null && weeklyDeliveryInfo.Length == 0)
			{
				TimeSpan span = TimeSpan.FromSeconds(MonoBehaviourSingleton<DeliveryManager>.I.weeklyUpdateRemainTime);
				SetLabelText(UI.LBL_DELIVERY_NON_LIST, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 102u), GetRemainTimeText(span)));
			}
			break;
		}
	}

	public static string GetRemainTimeText(TimeSpan span)
	{
		string text = "";
		if (span.Seconds > 0)
		{
			span = span.Add(TimeSpan.FromMinutes(1.0));
		}
		if (span.Days > 0)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 0u), span.Days);
		}
		if (span.Hours > 0)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 1u), span.Hours);
		}
		if (span.Minutes > 0)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2u), span.Minutes);
		}
		if (text == "")
		{
			return string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2u), 0);
		}
		return text;
	}

	protected void OnQuery_TAB_DELIVERY()
	{
		if (showMode != 0)
		{
			SetDirty(UI.GRD_DELIVERY);
			SetDirty(UI.GRD_AREA);
			ChangeToggle(SHOW_MODE.DELIVERY);
			ResetTween(UI.BTN_DELIVERY);
			PlayTween(UI.BTN_DELIVERY, forward: true, null, is_input_block: false);
		}
	}

	protected void OnQuery_TAB_ORDER()
	{
		if (showMode != SHOW_MODE.ORDER)
		{
			SetDirty(UI.GRD_ORDER_QUEST);
			ChangeToggle(SHOW_MODE.ORDER);
			ResetTween(UI.BTN_ORDER);
			PlayTween(UI.BTN_ORDER, forward: true, null, is_input_block: false);
		}
	}

	protected void OnQuery_TAB_QUEST()
	{
		if (showMode != SHOW_MODE.QUEST)
		{
			SetDirty(UI.GRD_QUEST);
			ChangeToggle(SHOW_MODE.QUEST);
			ResetTween(UI.BTN_QUEST);
			PlayTween(UI.BTN_QUEST, forward: true, null, is_input_block: false);
			if (GameSection.GetEventData() != null)
			{
				MonoBehaviourSingleton<QuestManager>.I.currentDeliveryId = (uint)GameSection.GetEventData();
			}
		}
	}

	public void ChangeToggle(SHOW_MODE show_mode)
	{
		showMode = show_mode;
		ShowSelectUI();
	}

	protected override void OnOpen()
	{
		SetNPCMessage(selectedTab);
		RemoveRecommend();
		changeToDeliveryClearEvent = false;
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(0u);
		MonoBehaviourSingleton<QuestManager>.I.currentDeliveryId = 0u;
		base.OnOpen();
	}

	protected override void OnClose()
	{
		RemoveNew(selectedTab);
		base.OnClose();
	}

	private void SetNPCMessage(UI tab)
	{
		if (isInGameScene)
		{
			return;
		}
		string str = "";
		switch (tab)
		{
		case UI.BTN_TAB_NORMAL:
			str = ((!MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(TAB_TYPES[0])) ? "_COMPLETE" : "");
			break;
		case UI.BTN_TAB_DAILY:
			str = ((!MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(TAB_TYPES[1])) ? "_COMPLETE" : "_DAILY");
			break;
		case UI.BTN_TAB_WEEKLY:
			str = ((!MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(TAB_TYPES[2])) ? "_COMPLETE" : "_WEEKLY");
			break;
		}
		NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(base.sectionData.sectionName + str + "_TEXT");
		if (section != null)
		{
			NPCMessageTable.Message nPCMessage = section.GetNPCMessage();
			if (nPCMessage != null)
			{
				npcText = nPCMessage.GetReplaceText();
			}
		}
		else
		{
			npcText = base.sectionData.GetText("NPC_MESSAGE_" + UnityEngine.Random.Range(0, 3));
		}
		SetLabelText(UI.LBL_NPC_MESSAGE, npcText);
	}

	protected virtual void RemoveRecommend()
	{
		if (GameSaveData.instance.IsRecommendedDeliveryCheck())
		{
			GameSaveData.instance.recommendedDeliveryCheck = 0;
			GameSaveData.Save();
		}
	}

	public override void StartSection()
	{
		UI[] array = new UI[3]
		{
			UI.GRD_DELIVERY,
			UI.GRD_ORDER_QUEST,
			UI.GRD_QUEST
		};
		UITweenAddToChildrenCtrl component = GetComponent<UITweenAddToChildrenCtrl>(array[(int)showMode]);
		if (component != null)
		{
			component.InitTween();
		}
		OpenTutorial();
		base.StartSection();
	}

	public void OnQuery_SELECT_ORDER()
	{
		int num = (int)GameSection.GetEventData();
		if (num < 0 || num >= questSortData.Length)
		{
			GameSection.StopEvent();
			return;
		}
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questSortData[num].GetTableID());
		GameSection.SetEventData(questSortData[num].itemData.infoData);
	}

	public void OnQuery_SELECT_DELIVERY()
	{
		int index = (int)GameSection.GetEventData();
		bool is_enough_material = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryInfo[index].dId);
		int delivery_id = deliveryInfo[index].dId;
		bool is_happen_quest = false;
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery_id);
		if (deliveryTableData != null)
		{
			QuestTable.QuestTableData questData = deliveryTableData.GetQuestData();
			if (questData != null && questData.questType == QUEST_TYPE.HAPPEN)
			{
				is_happen_quest = true;
			}
		}
		if (is_enough_material)
		{
			if (!TutorialStep.HasFirstDeliveryCompleted() && isInGameScene)
			{
				GameSection.ChangeEvent("DELIVERY_ITEM_COMPLETE");
			}
			else if (isInGameScene)
			{
				GameSection.StayEvent();
				MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop(delegate(bool b)
				{
					if (b)
					{
						SendDeliveryComplete(index, delivery_id, is_enough_material, is_happen_quest);
						if (Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint)delivery_id) != null)
						{
							MonoBehaviourSingleton<DeliveryManager>.I.CheckAnnouncePortalOpen();
						}
					}
				});
			}
			else
			{
				GameSection.StayEvent();
				SendDeliveryComplete(index, delivery_id, is_enough_material, is_happen_quest);
			}
		}
		else if (is_happen_quest)
		{
			GameSection.ChangeEvent("SELECT_DELIVERY_HAPPEN", new object[2]
			{
				delivery_id,
				null
			});
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				delivery_id,
				null
			});
		}
	}

	public void OnQuery_READ_STORY()
	{
		int num = (int)GameSection.GetEventData();
		bool flag = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryInfo[num].dId);
		int dId = deliveryInfo[num].dId;
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)dId);
		if (flag)
		{
			GameSection.StayEvent();
			SendDeliveryComplete(num, dId, flag, is_happen_quest: false);
		}
		else
		{
			string goingHomeEvent = GameSection.GetGoingHomeEvent();
			EventData[] array = new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("COMPLETE_READ_STORY", (int)deliveryTableData.readScriptId)
			};
			GameSection.SetEventData(new object[4]
			{
				(int)deliveryTableData.readScriptId,
				"",
				"",
				array
			});
		}
	}

	protected void SendDeliveryComplete(int index, int delivery_id, bool is_enough_material, bool is_happen_quest)
	{
		DeliveryTable.DeliveryData table = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[index].dId);
		changeToDeliveryClearEvent = true;
		bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
		bool enable_clear_event = table.clearEventID != 0;
		MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryInfo[index].uId, enable_clear_event, delegate(bool is_success, DeliveryRewardList recv_reward)
		{
			if (is_success)
			{
				List<FieldMapTable.PortalTableData> deliveryRelationPortalData = Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint)delivery_id);
				for (int i = 0; i < deliveryRelationPortalData.Count; i++)
				{
					GameSaveData.instance.newReleasePortals.Add(deliveryRelationPortalData[i].portalID);
				}
				if (is_tutorial)
				{
					TutorialStep.isSendFirstRewardComplete = true;
				}
				if (!enable_clear_event)
				{
					MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
					if (is_happen_quest)
					{
						GameSection.ChangeStayEvent("DELIVERY_REWARD_HAPPEN", new object[2]
						{
							delivery_id,
							recv_reward
						});
					}
					else
					{
						GameSection.ChangeStayEvent("DELIVERY_REWARD", new object[2]
						{
							delivery_id,
							recv_reward
						});
					}
				}
				else
				{
					GameSection.ChangeStayEvent("CLEAR_EVENT", new object[3]
					{
						(int)table.clearEventID,
						delivery_id,
						recv_reward
					});
					if (isInGameScene)
					{
						is_success = false;
						List<int> list = new List<int>(MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame);
						for (int j = 0; j < list.Count; j++)
						{
							int item = list[j];
							if (!MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.Contains(item))
							{
								MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtHomeScene.Add(item);
							}
						}
						EventData[] requestEventData = new EventData[2]
						{
							new EventData("STORY_DELIVERY_REWARD", new object[2]
							{
								delivery_id,
								recv_reward
							}),
							new EventData("PORTAL_RELEASE", GameSaveData.instance.newReleasePortals)
						};
						GameSaveData.instance.newReleasePortals = new List<uint>();
						MonoBehaviourSingleton<InGameProgress>.I.FieldReadStory((int)table.clearEventID, isSend: true, requestEventData);
						MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame.Clear();
					}
				}
			}
			else
			{
				changeToDeliveryClearEvent = false;
			}
			GameSection.ResumeEvent(is_success);
		});
	}

	public void OnQuery_SELECT_QUEST()
	{
		int num = (int)GameSection.GetEventData();
		if (num < 0 || num >= questInfo.Length)
		{
			GameSection.StopEvent();
			return;
		}
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questInfo[num].questData.tableData.questID);
		GameSection.SetEventData(questInfo[num]);
	}

	private void OnQuery_QuestAcceptDeliveryItemComplete_YES()
	{
		BackHome();
	}

	private void OnQuery_InGameQuestAcceptDeliveryItemComplete_YES()
	{
		BackHome();
	}

	private void OnQuery_InGameQuestBackHome_YES()
	{
		BackHome();
	}

	private void BackHome()
	{
		if (isInGameScene && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
		}
	}

	private void OnQuery_SORT()
	{
		GameSection.SetEventData(sortSettings.Clone());
	}

	protected void _OnCloseDialogSort()
	{
		SortSettings sortSettings = (SortSettings)GameSection.GetEventData();
		if (sortSettings != null)
		{
			this.sortSettings = sortSettings;
			SetDirty(UI.GRD_ORDER_QUEST);
			RefreshUI();
		}
	}

	private void OnCloseDialog_QuestSort()
	{
		_OnCloseDialogSort();
	}

	protected virtual bool IsVisibleDelivery(Delivery delivery, DeliveryTable.DeliveryData tableData)
	{
		return !tableData.IsEvent();
	}

	protected virtual void GetDeliveryList()
	{
		deliveryInfo = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList();
		List<Delivery> list = new List<Delivery>();
		int i = 0;
		for (int num = deliveryInfo.Length; i < num; i++)
		{
			Delivery delivery = deliveryInfo[i];
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery.dId);
			if (deliveryTableData == null)
			{
				Log.Warning("DeliveryTable Not Found : dId " + delivery.dId);
			}
			else if (IsVisibleDelivery(delivery, deliveryTableData))
			{
				if (Singleton<NPCTable>.I.GetNPCData((int)deliveryTableData.npcID) == null)
				{
					Log.Error("DeliveryTable NPC ID Found  : dId " + delivery.dId + " : npcID " + deliveryTableData.npcID);
				}
				else
				{
					list.Add(deliveryInfo[i]);
				}
			}
		}
		deliveryInfo = list.ToArray();
		List<Delivery> list2 = new List<Delivery>();
		List<Delivery> list3 = new List<Delivery>();
		List<Delivery> list4 = new List<Delivery>();
		Delivery[] array = deliveryInfo;
		foreach (Delivery delivery2 in array)
		{
			switch (delivery2.type)
			{
			case 0:
				list3.Add(delivery2);
				break;
			case 10:
				list4.Add(delivery2);
				break;
			default:
				list2.Add(delivery2);
				break;
			}
		}
		normalDeliveryInfo = list2.ToArray();
		dailyDeliveryInfo = list3.ToArray();
		weeklyDeliveryInfo = list4.ToArray();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		bool flag = false;
		if ((flags & NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			SetDirty(UI.GRD_ORDER_QUEST);
			questItemAry = null;
		}
		if ((flags & NOTIFY_FLAG.UPDATE_DELIVERY_OVER) != (NOTIFY_FLAG)0L)
		{
			flag = true;
			MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryUpdate(delegate
			{
				GetDeliveryList();
				SetDirty(UI.GRD_AREA);
				SetDirty(UI.GRD_DELIVERY);
				base.OnNotify(flags);
			});
		}
		if ((flags & NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE) != (NOTIFY_FLAG)0L)
		{
			GetDeliveryList();
			SetDirty(UI.GRD_AREA);
			SetDirty(UI.GRD_DELIVERY);
		}
		if (!flag)
		{
			base.OnNotify(flags);
		}
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE | NOTIFY_FLAG.UPDATE_DELIVERY_OVER;
	}

	public override EventData CheckAutoEvent(string event_name, object event_data)
	{
		if (event_name == "SELECT_ORDER")
		{
			int num = -1;
			uint table_id = (uint)event_data;
			num = Array.FindIndex(questSortData, (QuestSortData data) => (int)data.GetTableID() == table_id);
			if (num != -1)
			{
				return new EventData(event_name, num);
			}
			return new EventData(event_name, -1);
		}
		if (event_name == "SELECT_QUEST")
		{
			int num2 = -1;
			uint table_id2 = (uint)event_data;
			num2 = Array.FindIndex(questInfo, (QuestInfoData data) => (int)data.questData.tableData.questID == table_id2);
			if (num2 != -1)
			{
				return new EventData(event_name, num2);
			}
			return new EventData(event_name, -1);
		}
		if (event_name == "SELECT_DELIVERY")
		{
			int num3 = -1;
			uint table_id3 = (uint)event_data;
			num3 = Array.FindIndex(deliveryInfo, (Delivery data) => data.dId == table_id3);
			if (num3 != -1)
			{
				return new EventData(event_name, num3);
			}
			return new EventData(event_name, -1);
		}
		return base.CheckAutoEvent(event_name, event_data);
	}

	private void OnQuery_SELECT_NORMAL()
	{
		SelectTab(UI.BTN_TAB_NORMAL);
		isDeliveryGridReset = true;
		RefreshUI();
	}

	private void OnQuery_SELECT_DAILY()
	{
		SelectTab(UI.BTN_TAB_DAILY);
		isDeliveryGridReset = true;
		RefreshUI();
	}

	private void OnQuery_SELECT_WEEKLY()
	{
		SelectTab(UI.BTN_TAB_WEEKLY);
		isDeliveryGridReset = true;
		RefreshUI();
	}

	private void SelectTab(UI tab)
	{
		if (selectedTab != tab)
		{
			RemoveNew(selectedTab);
		}
		selectedTab = tab;
	}

	private void RemoveNew(UI old_select)
	{
		switch (old_select)
		{
		case UI.BTN_TAB_NORMAL:
			break;
		case UI.BTN_TAB_DAILY:
			SetNew(UI.BTN_TAB_DAILY, is_visible: false);
			GameSaveData.instance.recommendedDailyDeliveryCheck = 0;
			GameSaveData.Save();
			break;
		case UI.BTN_TAB_WEEKLY:
			SetNew(UI.BTN_TAB_WEEKLY, is_visible: false);
			GameSaveData.instance.recommendedWeeklyDeliveryCheck = 0;
			GameSaveData.Save();
			break;
		}
	}

	private void OnQuery_SELECT_CLOSE_AREA()
	{
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.WORLD_MAP))
		{
			GameSection.ChangeEvent("WORLD_MAP_TUTORIAL");
			return;
		}
		releaseRegionId = (int)GameSection.GetEventData();
		GameSection.StayEvent();
		MonoBehaviourSingleton<WorldMapManager>.I.SendRegionCrystalNum(releaseRegionId, delegate(bool isSuccess, string campainText)
		{
			GameSection.ResumeEvent(isSuccess, new object[2]
			{
				MonoBehaviourSingleton<WorldMapManager>.I.releaseCrystalNum.ToString(),
				campainText
			});
		});
	}

	private void OnQuery_QuestAcceptReleaseRegionDialog_YES()
	{
		if (releaseRegionId >= 0)
		{
			string goingHomeEvent = GameSection.GetGoingHomeEvent();
			EventData[] auto_event_data = new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("QUEST", null)
			};
			GameSection.StayEvent();
			MonoBehaviourSingleton<WorldMapManager>.I.SendRegionOpen(releaseRegionId, delegate(bool isSuccess)
			{
				GameSection.ResumeEvent(isSuccess);
				if (isSuccess)
				{
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(auto_event_data);
					MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard = releaseRegionId;
				}
			});
		}
	}
}
