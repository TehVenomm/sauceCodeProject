using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChallengeSelect : GameSection
{
	protected enum UI
	{
		SPR_BG_BTN_CLOSE,
		BTN_INPUT_CLOSE,
		BTN_INPUT_CLOSE_BG,
		BG,
		OBJ_ORDER_ROOT,
		STR_ORDER_NON_LIST,
		GRD_ORDER_QUEST,
		OBJ_ICON_ROOT,
		SCR_ORDER_QUEST,
		OBJ_FRAME,
		LBL_QUEST_NAME,
		OBJ_ENEMY,
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
		BTN_PARTY,
		BTN_GUILD_REQUEST,
		TXT_NEED_POINT,
		OBJ_ICON_NEW,
		SPR_ICON_NEW,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		LBL_MAX,
		LBL_NOW,
		OBJ_CHALLENGE_MSG_ROOT,
		STR_CHALLENGE_MESSAGE,
		STR_CHALLENGE_BONUS_MESSAGE,
		BTN_DETAIL
	}

	protected QuestData[] challengeData;

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

	protected bool isScrollViewReady;

	private Action onScrollViewReady;

	private bool isTransitionFinished;

	private Action onOpen;

	private bool isQuestItemDirty;

	private bool isResetUI;

	private int nowPage = 1;

	private int pageMax = 1;

	private QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam param = new QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam();

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool is_recv_quest = false;
		LoadingQueue load_queue = new LoadingQueue(this);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_new");
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_complete");
		InitSearchParam();
		QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam sendParam = new QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam();
		int userLevel = sendParam.enemyLevel = GetEnemyLevelFromUserLevel();
		MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeList(sendParam, delegate
		{
			is_recv_quest = true;
		}, isSave: false);
		while (!is_recv_quest)
		{
			yield return null;
		}
		this.StartCoroutine(CheckLimitQuestItem());
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
		}
		base.Initialize();
	}

	protected override void OnOpen()
	{
		GameSaveData.instance.recommendedChallengeCheck = 0;
		GameSaveData.Save();
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		ShowChallenge();
		isResetUI = false;
	}

	protected void ShowChallenge()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		List<QuestData> challengeList = MonoBehaviourSingleton<QuestManager>.I.challengeList;
		if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount != null)
		{
			SetActive((Enum)UI.STR_CHALLENGE_BONUS_MESSAGE, is_visible: true);
			SetActive((Enum)UI.BTN_DETAIL, is_visible: true);
			UIPanel component = GetCtrl(UI.SCR_ORDER_QUEST).GetComponent<UIPanel>();
			component.baseClipRegion = new Vector4(0f, -110f, 440f, 549f);
			SetLabelText((Enum)UI.STR_CHALLENGE_BONUS_MESSAGE, StringTable.Format(STRING_CATEGORY.SHADOW_COUNT, 3u, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount.num));
			base.GetComponent<UILabel>((Enum)UI.STR_CHALLENGE_BONUS_MESSAGE).supportEncoding = true;
		}
		else
		{
			SetActive((Enum)UI.STR_CHALLENGE_BONUS_MESSAGE, is_visible: false);
			SetActive((Enum)UI.BTN_DETAIL, is_visible: false);
		}
		SetLabelText((Enum)UI.STR_CHALLENGE_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
		SetSupportEncoding(UI.STR_CHALLENGE_MESSAGE, isEnable: true);
		if (challengeList == null || challengeList.Count == 0)
		{
			SetActive((Enum)UI.GRD_ORDER_QUEST, is_visible: false);
			SetActive((Enum)UI.STR_ORDER_NON_LIST, is_visible: true);
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, is_visible: false);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, is_visible: true);
			SetLabelText((Enum)UI.LBL_MAX, "0");
			SetLabelText((Enum)UI.LBL_NOW, "0");
			UIScrollView component2 = GetCtrl(UI.SCR_ORDER_QUEST).GetComponent<UIScrollView>();
			if (component2 != null)
			{
				component2.set_enabled(false);
				component2.verticalScrollBar.alpha = 0f;
			}
			return;
		}
		SetActive((Enum)UI.GRD_ORDER_QUEST, is_visible: true);
		SetActive((Enum)UI.STR_ORDER_NON_LIST, is_visible: false);
		pageMax = 1 + (challengeList.Count - 1) / 10;
		bool flag = pageMax > 1;
		SetActive((Enum)UI.OBJ_ACTIVE_ROOT, flag);
		SetActive((Enum)UI.OBJ_INACTIVE_ROOT, !flag);
		SetLabelText((Enum)UI.LBL_MAX, pageMax.ToString());
		SetLabelText((Enum)UI.LBL_NOW, nowPage.ToString());
		UITweener[] transitions = GetCtrl(UI.OBJ_FRAME).GetComponents<UITweener>();
		int finishCount = 0;
		UITweener[] array = transitions;
		foreach (UITweener uITweener in array)
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
		int num = 10 * (nowPage - 1);
		int num2 = (nowPage != pageMax) ? 10 : (challengeList.Count - num);
		challengeData = new QuestData[num2];
		Array.Copy(challengeList.ToArray(), num, challengeData, 0, num2);
		bool isGuildRequest = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName().Contains("GuildRequest");
		UIScrollView scrollView = GetCtrl(UI.SCR_ORDER_QUEST).GetComponent<UIScrollView>();
		SetGrid(UI.GRD_ORDER_QUEST, "QuestListChallengeItem", challengeData.Length, isResetUI, (int i, Transform t) => Realizes("QuestListChallengeItem", t), delegate(int i, Transform t, bool is_recycle)
		{
			SetActive(t, is_visible: true);
			SetEvent(t, "SELECT_ORDER", i);
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)challengeData[i].questId);
			if (isGuildRequest)
			{
				SetActive(t, UI.TWN_DIFFICULT_STAR, is_visible: false);
				SetActive(t, UI.TXT_NEED_POINT, is_visible: true);
				string text = string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 6u), MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(questData.rarity), MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(questData.rarity));
				SetLabelText(t, UI.TXT_NEED_POINT, text);
			}
			else
			{
				SetActive(t, UI.TWN_DIFFICULT_STAR, is_visible: false);
				SetActive(t, UI.TXT_NEED_POINT, is_visible: false);
				Debug.Log((object)"2");
			}
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
			ITEM_ICON_TYPE itemIconType = ItemIcon.GetItemIconType(questData.questType);
			ItemIcon icon = ItemIcon.Create(itemIconType, enemyData.iconId, questData.rarity, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element);
			icon.SetEnableCollider(is_enable: false);
			SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
			SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
			SetLabelText(t, UI.LBL_QUEST_NAME, questData.questText);
			int num3 = 1;
			ClearStatusQuestEnemySpecies clearStatusQuestEnemySpecies = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestEnemySpecies(questData.questID);
			if (clearStatusQuestEnemySpecies != null)
			{
				num3 = clearStatusQuestEnemySpecies.questStatus;
			}
			int value = i + 100;
			SetToggleGroup(t, UI.OBJ_ICON_NEW, value);
			CLEAR_STATUS cLEAR_STATUS = (CLEAR_STATUS)num3;
			if (cLEAR_STATUS != CLEAR_STATUS.NEW)
			{
				SetToggle(t, UI.OBJ_ICON_NEW, value: false);
				SetActive(t, UI.OBJ_ICON_ROOT, is_visible: false);
			}
			else
			{
				SetActive(t, UI.OBJ_ICON_ROOT, is_visible: true);
				SetToggle(t, UI.OBJ_ICON_NEW, value: true);
				SetVisibleWidgetEffect(UI.SCR_ORDER_QUEST, t, UI.SPR_ICON_NEW, "ef_ui_questselect_new");
			}
			Transform val = FindCtrl(t, UI.OBJ_FRAME);
			if (val != null)
			{
				UIPanel uiPanel = val.get_gameObject().GetComponent<UIPanel>();
				if (uiPanel == null)
				{
					uiPanel = val.get_gameObject().AddComponent<UIPanel>();
					uiPanel.depth = scrollView.panel.depth + 1;
				}
				uiPanel.widgetsAreStatic = false;
				if (isScrollViewReady)
				{
					PanelToStatic(icon, uiPanel);
				}
				else
				{
					QuestChallengeSelect questChallengeSelect = this;
					questChallengeSelect.onScrollViewReady = (Action)Delegate.Combine(questChallengeSelect.onScrollViewReady, (Action)delegate
					{
						PanelToStatic(icon, uiPanel);
					});
				}
			}
		});
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
		if (num < 0 || num >= challengeData.Length)
		{
			GameSection.StopEvent();
			return;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog((uint)challengeData[num].questId))
		{
			GameSection.StopEvent();
			return;
		}
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)challengeData[num].questId);
		QuestInfoData questChallengeInfoData = MonoBehaviourSingleton<QuestManager>.I.GetQuestChallengeInfoData((uint)challengeData[num].questId);
		GameSection.SetEventData(questChallengeInfoData);
		isScrollViewReady = false;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		bool flag = false;
		if ((flags & NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			SetDirty(UI.GRD_ORDER_QUEST);
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
				MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeList(param, delegate
				{
					this.StartCoroutine(CheckLimitQuestItem());
				}, isSave: true);
			});
			isQuestItemDirty = false;
		}
	}

	private IEnumerator CheckLimitQuestItem()
	{
		if (isQuestItemDirty)
		{
			yield break;
		}
		List<QuestData> challengeList = MonoBehaviourSingleton<QuestManager>.I.challengeList;
		if (challengeList != null && challengeList.Count > 0)
		{
			float minRemainingSec = float.MaxValue;
			QuestData questData = null;
			float parseRemainingSec;
			challengeList.ForEach(delegate(QuestData challengeQuest)
			{
				for (int i = 0; i < challengeQuest.remainTimes.Count; i++)
				{
					parseRemainingSec = challengeQuest.remainTimes[i];
					if (!(parseRemainingSec <= 0f) && minRemainingSec > parseRemainingSec)
					{
						minRemainingSec = parseRemainingSec;
						questData = challengeQuest;
					}
				}
			});
			yield return (object)new WaitForSeconds(minRemainingSec);
			if (questData != null)
			{
				isQuestItemDirty = true;
			}
		}
	}

	private void OnQuery_PAGE_PREV()
	{
		isResetUI = true;
		nowPage = ((nowPage <= 1) ? pageMax : (nowPage - 1));
		ShowChallenge();
	}

	private void OnQuery_PAGE_NEXT()
	{
		isResetUI = true;
		nowPage = ((nowPage >= pageMax) ? 1 : (nowPage + 1));
		ShowChallenge();
	}

	private void InitSearchParam()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetChallengeSearchRequestFromPrefs(GetEnemyLevelFromUserLevel(), param);
	}

	private void OnQuery_CONDITION()
	{
		GameSection.SetEventData(param);
	}

	protected void OnCloseDialog_QuestAcceptChallengeRoomCondition()
	{
		QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam challengeSearchRequestParam = GameSection.GetEventData() as QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam;
		if (challengeSearchRequestParam != null && challengeSearchRequestParam.order == 1)
		{
			param = challengeSearchRequestParam;
			nowPage = 1;
			isResetUI = true;
			RefreshUI();
		}
	}

	private int GetEnemyLevelFromUserLevel()
	{
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		return GetEnemyLevelFromUserLevel(userStatus.level);
	}

	private int GetEnemyLevelFromUserLevel(int userLevel)
	{
		int qUEST_ITEM_LEVEL_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_ITEM_LEVEL_MAX;
		int num = Mathf.Clamp(userLevel, 10, qUEST_ITEM_LEVEL_MAX);
		return (num + 9) / 10 * 10;
	}
}
