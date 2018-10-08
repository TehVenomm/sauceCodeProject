using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestOrderSelect : GameSection
{
	protected enum UI
	{
		TEX_NPCMODEL,
		OBJ_NPC_MESSAGE,
		LBL_NPC_MESSAGE,
		BTN_SEARCH,
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
		STR_ORDER_NON_LIST,
		GRD_ORDER_QUEST,
		SCR_ORDER_QUEST2,
		OBJ_ICON_ROOT,
		OBJ_BUTTON_ROOT,
		SCR_ORDER_QUEST,
		SPR_ORDER_RARITY_FRAME,
		LBL_ORDER_NUM,
		OBJ_FRAME,
		LBL_QUEST_NAME,
		LBL_QUEST_NUM,
		LBL_REMAIN,
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
		TXT_NEED_POINT,
		OBJ_ICON,
		OBJ_ICON_NEW,
		OBJ_ICON_CLEARED,
		OBJ_ICON_COMPLETE,
		SPR_ICON_NEW,
		SPR_ICON_CLEARED,
		SPR_ICON_COMPLETE,
		OBJ_BANNER_ROOT,
		SPR_BANNER,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		LBL_MAX,
		LBL_NOW,
		OBJ_CHALLENGE_ON,
		OBJ_CHALLENGE_OFF,
		LBL_CHALLENGE_ON_MESSAGE,
		LBL_CHALLENGE_OFF_MESSAGE,
		BTN_SHADOW_COUNT
	}

	public class QuestGridData
	{
		public enum ORDER_QUEST_TYPE
		{
			Quest,
			Challenge
		}

		public ORDER_QUEST_TYPE orderQuestType;

		public QuestSortData questSortData;

		public QuestGridData(ORDER_QUEST_TYPE type = ORDER_QUEST_TYPE.Quest, QuestSortData data = null)
		{
			orderQuestType = type;
			questSortData = data;
		}
	}

	private SortSettings sortSettings;

	private QuestItemInfo[] questItemAry;

	protected QuestGridData[] questGridDatas;

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

	private string npcText;

	protected bool isScrollViewReady;

	private Action onScrollViewReady;

	private bool isTransitionFinished;

	private Action onOpen;

	private bool isQuestItemDirty;

	private float remainingTime;

	private float SHOW_QUEST_REMAIN_LIMIT_SECOND;

	private bool isResetUI;

	protected int nowPage = 1;

	private int pageMax = 1;

	protected QuestSearchRoomCondition.SearchRequestParam param = new QuestSearchRoomCondition.SearchRequestParam();

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool is_recv_quest = false;
		LoadingQueue load_queue = new LoadingQueue(this);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_new");
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_complete");
		if (MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList)
		{
			MonoBehaviourSingleton<QuestManager>.I.SendGetQuestList(delegate
			{
				MonoBehaviourSingleton<QuestManager>.I.needRequestOrderQuestList = false;
				((_003CDoInitialize_003Ec__Iterator7F)/*Error near IL_0075: stateMachine*/)._003Cis_recv_quest_003E__0 = true;
			});
			while (!is_recv_quest)
			{
				yield return (object)null;
			}
		}
		StartCoroutine(CheckLimitQuestItem());
		sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.QUEST, SortSettings.SETTINGS_TYPE.ORDER_QUEST);
		SHOW_QUEST_REMAIN_LIMIT_SECOND = (float)TimeSpan.FromDays(5.0).TotalSeconds;
		sortSettings.indivComparison = Compare;
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
		}
		base.Initialize();
	}

	protected override void OnOpen()
	{
		GameSaveData.instance.recommendedOrderCheck = 0;
		GameSaveData.Save();
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		npcText = Singleton<NPCMessageTable>.I.GetNPCMessageBySectionData(base.sectionData);
		SetRenderNPCModel(UI.TEX_NPCMODEL, 2, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.orderCenterNPCFOV, null);
		SetLabelText(UI.LBL_NPC_MESSAGE, npcText);
		if (MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.challengeInfo != null && MonoBehaviourSingleton<PartyManager>.I.challengeInfo.currentShadowCount != null)
		{
			SetActive(UI.BTN_SHADOW_COUNT, true);
		}
		else
		{
			SetActive(UI.BTN_SHADOW_COUNT, false);
		}
		ShowOrder();
		isResetUI = false;
	}

	protected void ShowOrder()
	{
		if (MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() > 0)
		{
			List<QuestItemInfo> list = new List<QuestItemInfo>();
			MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory(delegate(QuestItemInfo item)
			{
				if (IsSetQuestItemInfoByOrderQuest(item))
				{
					if (isQuestItemDirty)
					{
						GetRemainingTimeAndExpiredNum(item, out float remainTime, out int expiredNum);
						if (remainTime > 0f)
						{
							item.infoData.questData.num -= expiredNum;
							if (item.infoData.questData.num > 0)
							{
								list.Add(item);
							}
						}
					}
					else if (item.infoData.questData.num > 0)
					{
						list.Add(item);
					}
				}
			});
			Search(ref list);
			questItemAry = list.ToArray();
		}
		List<QuestGridData> list2 = new List<QuestGridData>();
		if (MonoBehaviourSingleton<PartyManager>.IsValid() && MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsEnable())
		{
			list2.Add(new QuestGridData(QuestGridData.ORDER_QUEST_TYPE.Challenge, null));
		}
		if (questItemAry != null && questItemAry.Length != 0)
		{
			QuestSortData[] array = sortSettings.CreateSortAry<QuestItemInfo, QuestSortData>(questItemAry);
			if (array != null)
			{
				int i = 0;
				for (int num = array.Length; i < num; i++)
				{
					QuestSortData questSortData = array[i];
					QuestInfoData info = questSortData.itemData.infoData;
					int num2 = info.questData.num;
					int num3 = 0;
					if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
					{
						num3 = (from g in MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList
						where g.questId == (int)info.questData.tableData.questID
						select g).Count();
					}
					int num4 = num2 - num3;
					if (num4 > 0)
					{
						list2.Add(new QuestGridData(QuestGridData.ORDER_QUEST_TYPE.Quest, array[i]));
					}
				}
			}
		}
		questGridDatas = list2.ToArray();
		if (questGridDatas == null || questGridDatas.Length == 0)
		{
			SetActive(UI.GRD_ORDER_QUEST, false);
			SetActive(UI.STR_ORDER_NON_LIST, true);
			SetActive(UI.OBJ_ACTIVE_ROOT, false);
			SetActive(UI.OBJ_INACTIVE_ROOT, true);
			SetLabelText(UI.LBL_MAX, "0");
			SetLabelText(UI.LBL_NOW, "0");
			UIScrollView component = GetCtrl(UI.SCR_ORDER_QUEST).GetComponent<UIScrollView>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				component.enabled = false;
				component.verticalScrollBar.alpha = 0f;
			}
		}
		else
		{
			if (questGridDatas.Length == 1 && questGridDatas[0].orderQuestType == QuestGridData.ORDER_QUEST_TYPE.Challenge)
			{
				SetActive(UI.STR_ORDER_NON_LIST, true);
			}
			else
			{
				SetActive(UI.STR_ORDER_NON_LIST, false);
			}
			SetActive(UI.GRD_ORDER_QUEST, true);
			SetLabelText(UI.LBL_SORT, sortSettings.GetSortLabel());
			SetToggle(UI.TGL_ICON_ASC, sortSettings.orderTypeAsc);
			pageMax = 1 + (questGridDatas.Length - 1) / 10;
			bool flag = pageMax > 1;
			SetActive(UI.OBJ_ACTIVE_ROOT, flag);
			SetActive(UI.OBJ_INACTIVE_ROOT, !flag);
			SetLabelText(UI.LBL_MAX, pageMax.ToString());
			SetLabelText(UI.LBL_NOW, nowPage.ToString());
			UITweener[] transitions = GetCtrl(UI.OBJ_FRAME).GetComponents<UITweener>();
			int finishCount = 0;
			UITweener[] array2 = transitions;
			foreach (UITweener uITweener in array2)
			{
				uITweener.AddOnFinished(delegate
				{
					finishCount++;
					if (finishCount >= transitions.Length)
					{
						isTransitionFinished = true;
					}
				});
			}
			int num5 = 10 * (nowPage - 1);
			int num6 = (nowPage != pageMax) ? 10 : (questGridDatas.Length - num5);
			QuestGridData[] destinationArray = new QuestGridData[num6];
			Array.Copy(questGridDatas, num5, destinationArray, 0, num6);
			questGridDatas = destinationArray;
			SetGrid(UI.GRD_ORDER_QUEST, string.Empty, 0, true, null, null);
			SetGrid(UI.GRD_ORDER_QUEST, "QuestListOrderItem", questGridDatas.Length, true, CreateGridListItem, UpdateGridListItem);
		}
	}

	private Transform CreateGridListItem(int index, Transform t)
	{
		if (index < 0 || questGridDatas.Length <= index)
		{
			return null;
		}
		string prefab_name = "QuestListOrderItem";
		if (questGridDatas[index].orderQuestType == QuestGridData.ORDER_QUEST_TYPE.Challenge)
		{
			prefab_name = "QuestListChallengeGotoItem";
		}
		return Realizes(prefab_name, t, true);
	}

	private void UpdateGridListItem(int i, Transform t, bool is_recycle)
	{
		if (i >= 0 && questGridDatas.Length > i)
		{
			switch (questGridDatas[i].orderQuestType)
			{
			case QuestGridData.ORDER_QUEST_TYPE.Quest:
				UpdateGirdListItemQuest(i, t, is_recycle);
				break;
			case QuestGridData.ORDER_QUEST_TYPE.Challenge:
				UpdateGridListItemChallenge(i, t, is_recycle);
				break;
			}
		}
	}

	private void UpdateGirdListItemQuest(int i, Transform t, bool is_recycle)
	{
		List<GameSectionHistory.HistoryData> historyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList();
		bool flag = historyList.Any((GameSectionHistory.HistoryData h) => h.sectionName.StartsWith("GuildRequest"));
		SetActive(t, true);
		SetEvent(t, "SELECT_ORDER", i);
		QuestSortData questSortData = questGridDatas[i].questSortData;
		UIScrollView component = GetCtrl(UI.SCR_ORDER_QUEST).GetComponent<UIScrollView>();
		QuestInfoData info = questSortData.itemData.infoData;
		if (flag)
		{
			SetActive(t, UI.TWN_DIFFICULT_STAR, false);
			SetActive(t, UI.TXT_NEED_POINT, true);
			string text = string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 6u), MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(info.questData.tableData.rarity), MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(info.questData.tableData.rarity));
			SetLabelText(t, UI.TXT_NEED_POINT, text);
		}
		else
		{
			SetActive(t, UI.TWN_DIFFICULT_STAR, false);
			SetActive(t, UI.TXT_NEED_POINT, false);
		}
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)info.questData.tableData.GetMainEnemyID());
		QuestSortData questSortData2 = questSortData;
		ItemIcon icon = ItemIcon.Create(questSortData2.GetIconType(), questSortData2.GetIconID(), questSortData2.GetRarity(), FindCtrl(t, UI.OBJ_ENEMY), questSortData2.GetIconElement(), null, -1, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		icon.SetEnableCollider(false);
		SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
		SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
		SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
		SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
		SetLabelText(t, UI.LBL_QUEST_NAME, info.questData.tableData.questText);
		int num = 1;
		ClearStatusQuestEnemySpecies clearStatusQuestEnemySpecies = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestEnemySpecies(info.questData.tableData.questID);
		if (clearStatusQuestEnemySpecies != null)
		{
			num = clearStatusQuestEnemySpecies.questStatus;
		}
		int value = i + 100;
		SetToggleGroup(t, UI.OBJ_ICON_NEW, value);
		SetToggleGroup(t, UI.OBJ_ICON_CLEARED, value);
		SetToggleGroup(t, UI.OBJ_ICON_COMPLETE, value);
		CLEAR_STATUS cLEAR_STATUS = (CLEAR_STATUS)num;
		if (cLEAR_STATUS != CLEAR_STATUS.NEW)
		{
			SetToggle(t, UI.OBJ_ICON_NEW, false);
			SetToggle(t, UI.OBJ_ICON_CLEARED, false);
			SetToggle(t, UI.OBJ_ICON_COMPLETE, false);
			SetActive(t, UI.OBJ_ICON_ROOT, false);
			SetVisibleWidgetEffect(UI.SCR_ORDER_QUEST, t, UI.SPR_ICON_NEW, null);
			SetVisibleWidgetEffect(UI.SCR_ORDER_QUEST, t, UI.SPR_ICON_COMPLETE, null);
		}
		else
		{
			SetActive(t, UI.OBJ_ICON_ROOT, true);
			SetToggle(t, UI.OBJ_ICON_NEW, true);
			SetVisibleWidgetEffect(UI.SCR_ORDER_QUEST, t, UI.SPR_ICON_COMPLETE, null);
			SetVisibleWidgetEffect(UI.SCR_ORDER_QUEST, t, UI.SPR_ICON_NEW, "ef_ui_questselect_new");
		}
		int num2 = info.questData.num;
		int num3 = 0;
		if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			num3 = (from g in MonoBehaviourSingleton<GuildRequestManager>.I.guildRequestData.guildRequestItemList
			where g.questId == (int)info.questData.tableData.questID
			select g).Count();
		}
		int num4 = num2 - num3;
		SetLabelText(t, UI.LBL_ORDER_NUM, num4.ToString());
		if (num4 <= 0)
		{
			t.GetComponent<UIButton>().isEnabled = false;
		}
		Transform transform = FindCtrl(t, UI.OBJ_FRAME);
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			UIPanel uiPanel = transform.gameObject.GetComponent<UIPanel>();
			if ((UnityEngine.Object)uiPanel == (UnityEngine.Object)null)
			{
				uiPanel = transform.gameObject.AddComponent<UIPanel>();
				uiPanel.depth = component.panel.depth + 1;
			}
			uiPanel.widgetsAreStatic = false;
			if (isScrollViewReady)
			{
				PanelToStatic(icon, uiPanel);
			}
			else
			{
				onScrollViewReady = (Action)Delegate.Combine(onScrollViewReady, (Action)delegate
				{
					PanelToStatic(icon, uiPanel);
				});
			}
		}
		QuestItemInfo itemData = questSortData.itemData;
		bool is_visible = false;
		foreach (float remainTime in itemData.remainTimes)
		{
			float num5 = remainTime;
			if (num5 < SHOW_QUEST_REMAIN_LIMIT_SECOND)
			{
				is_visible = true;
				break;
			}
		}
		SetLabelText(t, UI.LBL_REMAIN, StringTable.Get(STRING_CATEGORY.GATE_QUEST_NAME, 1u));
		SetActive(t, UI.LBL_REMAIN, is_visible);
	}

	private void UpdateGridListItemChallenge(int i, Transform t, bool is_recycle)
	{
		SetActive(t, true);
		SetEvent(t, "SELECT_CHALLENGE", i);
		SetActive(t, UI.OBJ_CHALLENGE_ON, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy());
		SetActive(t, UI.OBJ_CHALLENGE_OFF, !MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy());
		SetLabelText(t, UI.LBL_CHALLENGE_ON_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
		SetLabelText(t, UI.LBL_CHALLENGE_OFF_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
		SetSupportEncoding(UI.LBL_CHALLENGE_ON_MESSAGE, true);
		SetSupportEncoding(UI.LBL_CHALLENGE_OFF_MESSAGE, true);
	}

	private void TryScrollViewToReady()
	{
		if (base.isOpen && isTransitionFinished && onScrollViewReady != null)
		{
			isScrollViewReady = true;
			onScrollViewReady();
			onScrollViewReady = null;
		}
	}

	private void PanelToStatic(ItemIcon icon, UIPanel uiPanel)
	{
		if (icon.isIconLoaded)
		{
			uiPanel.widgetsAreStatic = false;
			AppMain i = MonoBehaviourSingleton<AppMain>.I;
			i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
			{
				uiPanel.widgetsAreStatic = true;
			});
		}
		else
		{
			icon.onIconLoaded = delegate
			{
				uiPanel.widgetsAreStatic = false;
				AppMain i2 = MonoBehaviourSingleton<AppMain>.I;
				i2.onDelayCall = (Action)Delegate.Combine(i2.onDelayCall, (Action)delegate
				{
					uiPanel.widgetsAreStatic = true;
				});
			};
		}
	}

	public virtual void OnQuery_SELECT_ORDER()
	{
		int num = (int)GameSection.GetEventData();
		if (num < 0 || num >= questGridDatas.Length)
		{
			GameSection.StopEvent();
		}
		else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(questGridDatas[num].questSortData.GetTableID(), true))
		{
			GameSection.StopEvent();
		}
		else
		{
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questGridDatas[num].questSortData.GetTableID(), true);
			GameSection.SetEventData(questGridDatas[num].questSortData.itemData.infoData);
			isScrollViewReady = false;
		}
	}

	public void OnQuery_SELECT_ORDER_FROM_ITEM_DETAIL()
	{
		uint num = (uint)GameSection.GetEventData();
		if (num == 0)
		{
			GameSection.StopEvent();
		}
		else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(num, true))
		{
			GameSection.StopEvent();
		}
		else
		{
			QuestInfoData questInfoData = MonoBehaviourSingleton<QuestManager>.I.GetQuestInfoData(num);
			if (questInfoData == null)
			{
				GameSection.StopEvent();
			}
			else
			{
				MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(num, true);
				GameSection.SetEventData(questInfoData);
				isScrollViewReady = false;
			}
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

	private void OnQuery_CAUTION()
	{
		GameSection.SetEventData(WebViewManager.GachaQuestList);
	}

	public virtual void OnQuery_SELECT_CHALLENGE()
	{
		if (MonoBehaviourSingleton<PartyManager>.IsValid())
		{
			if (!MonoBehaviourSingleton<PartyManager>.I.challengeInfo.IsSatisfy())
			{
				GameSection.ChangeEvent("NO_SATISFY", null);
			}
			else if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.num == 0)
			{
				GameSection.ChangeEvent("NUM_ZERO", null);
			}
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		bool flag = false;
		if ((flags & NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			SetDirty(UI.GRD_ORDER_QUEST);
			questItemAry = null;
		}
		if (!flag)
		{
			base.OnNotify(flags);
		}
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY;
	}

	public override EventData CheckAutoEvent(string event_name, object event_data)
	{
		if (event_name == "SELECT_ORDER")
		{
			int num = -1;
			uint table_id = (uint)event_data;
			num = Array.FindIndex(questGridDatas, (QuestGridData data) => data.questSortData != null && (int)data.questSortData.GetTableID() == table_id);
			if (num != -1)
			{
				return new EventData(event_name, num);
			}
			return new EventData(event_name, -1);
		}
		return base.CheckAutoEvent(event_name, event_data);
	}

	private void Update()
	{
		if (!isScrollViewReady)
		{
			TryScrollViewToReady();
		}
		if (isQuestItemDirty)
		{
			RefreshUI();
			Protocol.Force(delegate
			{
				MonoBehaviourSingleton<QuestManager>.I.SendGetQuestList(delegate
				{
					StartCoroutine(CheckLimitQuestItem());
				});
			});
			isQuestItemDirty = false;
		}
	}

	private bool IsSetQuestItemInfoByOrderQuest(QuestItemInfo questItemInfo)
	{
		bool isMatch = false;
		List<QuestData> orderQuestList = MonoBehaviourSingleton<QuestManager>.I.orderQuestList;
		if (orderQuestList == null || orderQuestList.Count <= 0)
		{
			return false;
		}
		orderQuestList.ForEach(delegate(QuestData orderQuest)
		{
			if (orderQuest.questId == questItemInfo.infoData.questData.tableData.questID)
			{
				isMatch = true;
				questItemInfo.remainTimes = orderQuest.remainTimes;
				questItemInfo.infoData.questData.num = orderQuest.order.num;
			}
		});
		return isMatch;
	}

	private void GetRemainingTimeAndExpiredNum(QuestItemInfo questItemInfo, out float remainTime, out int expiredNum)
	{
		remainTime = 0f;
		expiredNum = 0;
		for (int i = 0; i < questItemInfo.remainTimes.Count; i++)
		{
			float num = questItemInfo.remainTimes[i] - remainingTime;
			if (remainTime < num)
			{
				remainTime = num;
			}
			if (num <= 0f)
			{
				expiredNum++;
			}
		}
	}

	private IEnumerator CheckLimitQuestItem()
	{
		if (!isQuestItemDirty)
		{
			List<QuestData> orderQuestList = MonoBehaviourSingleton<QuestManager>.I.orderQuestList;
			if (orderQuestList != null && orderQuestList.Count > 0)
			{
				float minRemainingSec = 3.40282347E+38f;
				QuestData questData = null;
				orderQuestList.ForEach(delegate(QuestData orderQuest)
				{
					for (int i = 0; i < orderQuest.remainTimes.Count; i++)
					{
						((_003CCheckLimitQuestItem_003Ec__Iterator80)/*Error near IL_007f: stateMachine*/)._003CparseRemainingSec_003E__3 = orderQuest.remainTimes[i];
						if (!(((_003CCheckLimitQuestItem_003Ec__Iterator80)/*Error near IL_007f: stateMachine*/)._003CparseRemainingSec_003E__3 <= 0f) && ((_003CCheckLimitQuestItem_003Ec__Iterator80)/*Error near IL_007f: stateMachine*/)._003CminRemainingSec_003E__1 > ((_003CCheckLimitQuestItem_003Ec__Iterator80)/*Error near IL_007f: stateMachine*/)._003CparseRemainingSec_003E__3)
						{
							((_003CCheckLimitQuestItem_003Ec__Iterator80)/*Error near IL_007f: stateMachine*/)._003CminRemainingSec_003E__1 = ((_003CCheckLimitQuestItem_003Ec__Iterator80)/*Error near IL_007f: stateMachine*/)._003CparseRemainingSec_003E__3;
							((_003CCheckLimitQuestItem_003Ec__Iterator80)/*Error near IL_007f: stateMachine*/)._003CquestData_003E__2 = orderQuest;
						}
					}
				});
				remainingTime = minRemainingSec;
				yield return (object)new WaitForSeconds(minRemainingSec);
				if (questData != null)
				{
					isQuestItemDirty = true;
				}
			}
		}
	}

	private void OnQuery_PAGE_PREV()
	{
		isResetUI = true;
		nowPage = ((nowPage <= 1) ? pageMax : (nowPage - 1));
		ShowOrder();
	}

	private void OnQuery_PAGE_NEXT()
	{
		isResetUI = true;
		nowPage = ((nowPage >= pageMax) ? 1 : (nowPage + 1));
		ShowOrder();
	}

	private void OnQuery_CONDITION()
	{
		GameSection.SetEventData(param);
	}

	private void Search(ref List<QuestItemInfo> list)
	{
		if (param != null && param.order != 0)
		{
			list = (from item in list
			where param.IsMatchRarity(item)
			where param.IsMatchLevel(item)
			where param.IsMatchElement(item)
			where param.IsMatchEnemySpecies(item)
			select item).ToList();
		}
	}

	private int Compare(SortCompareData lp, SortCompareData rp)
	{
		QuestSortData questSortData = lp as QuestSortData;
		QuestSortData questSortData2 = rp as QuestSortData;
		if (questSortData == null || questSortData2 == null)
		{
			return 0;
		}
		float num = questSortData.itemData.remainTimes.Min();
		float num2 = questSortData2.itemData.remainTimes.Min();
		bool flag = num < SHOW_QUEST_REMAIN_LIMIT_SECOND;
		bool flag2 = num2 < SHOW_QUEST_REMAIN_LIMIT_SECOND;
		if (lp.IsAbsFirst() != rp.IsAbsFirst())
		{
			return (!lp.IsAbsFirst()) ? 1 : (-1);
		}
		if (flag != flag2)
		{
			return (!flag) ? 1 : (-1);
		}
		int num3 = questSortData2.GetRarity() - questSortData.GetRarity();
		if (num3 == 0)
		{
			return (int)(questSortData2.GetUniqID() - questSortData.GetUniqID());
		}
		return num3;
	}
}
