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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private unsafe IEnumerator DoInitialize()
	{
		bool is_recv_quest = false;
		LoadingQueue load_queue = new LoadingQueue(this);
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_new");
		load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_complete");
		InitSearchParam();
		QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam sendParam = new QuestAcceptChallengeRoomCondition.ChallengeSearchRequestParam();
		int userLevel = sendParam.enemyLevel = GetEnemyLevelFromUserLevel();
		MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeList(sendParam, new Action<bool, Error>((object)/*Error near IL_00a4: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), false);
		while (!is_recv_quest)
		{
			yield return (object)null;
		}
		this.StartCoroutine(CheckLimitQuestItem());
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
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

	protected unsafe void ShowChallenge()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		List<QuestData> challengeList = MonoBehaviourSingleton<QuestManager>.I.challengeList;
		if (MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount != null)
		{
			SetActive((Enum)UI.STR_CHALLENGE_BONUS_MESSAGE, true);
			SetActive((Enum)UI.BTN_DETAIL, true);
			UIPanel component = GetCtrl(UI.SCR_ORDER_QUEST).GetComponent<UIPanel>();
			component.baseClipRegion = new Vector4(0f, -110f, 440f, 549f);
			SetLabelText((Enum)UI.STR_CHALLENGE_BONUS_MESSAGE, StringTable.Format(STRING_CATEGORY.SHADOW_COUNT, 3u, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.oldShadowCount.num));
			base.GetComponent<UILabel>((Enum)UI.STR_CHALLENGE_BONUS_MESSAGE).supportEncoding = true;
		}
		else
		{
			SetActive((Enum)UI.STR_CHALLENGE_BONUS_MESSAGE, false);
			SetActive((Enum)UI.BTN_DETAIL, false);
		}
		SetLabelText((Enum)UI.STR_CHALLENGE_MESSAGE, MonoBehaviourSingleton<PartyManager>.I.challengeInfo.message);
		SetSupportEncoding(UI.STR_CHALLENGE_MESSAGE, true);
		if (challengeList == null || challengeList.Count == 0)
		{
			SetActive((Enum)UI.GRD_ORDER_QUEST, false);
			SetActive((Enum)UI.STR_ORDER_NON_LIST, true);
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, false);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, true);
			SetLabelText((Enum)UI.LBL_MAX, "0");
			SetLabelText((Enum)UI.LBL_NOW, "0");
			UIScrollView component2 = GetCtrl(UI.SCR_ORDER_QUEST).GetComponent<UIScrollView>();
			if (component2 != null)
			{
				component2.set_enabled(false);
				component2.verticalScrollBar.alpha = 0f;
			}
		}
		else
		{
			SetActive((Enum)UI.GRD_ORDER_QUEST, true);
			SetActive((Enum)UI.STR_ORDER_NON_LIST, false);
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
			bool isGuildRequest = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "GuildRequestChallengeCounter";
			UIScrollView scrollView = GetCtrl(UI.SCR_ORDER_QUEST).GetComponent<UIScrollView>();
			_003CShowChallenge_003Ec__AnonStorey355 _003CShowChallenge_003Ec__AnonStorey;
			SetGrid(UI.GRD_ORDER_QUEST, "QuestListChallengeItem", challengeData.Length, isResetUI, new Func<int, Transform, Transform>((object)_003CShowChallenge_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CShowChallenge_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void TryScrollViewToReady()
	{
		if (base.isOpen && isTransitionFinished && onScrollViewReady != null)
		{
			isScrollViewReady = true;
			onScrollViewReady.Invoke();
			onScrollViewReady = null;
		}
	}

	private unsafe void PanelToStatic(ItemIcon icon, UIPanel uiPanel)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		_003CPanelToStatic_003Ec__AnonStorey358 _003CPanelToStatic_003Ec__AnonStorey;
		if (icon.isIconLoaded)
		{
			uiPanel.widgetsAreStatic = false;
			AppMain i = MonoBehaviourSingleton<AppMain>.I;
			i.onDelayCall = Delegate.Combine((Delegate)i.onDelayCall, (Delegate)new Action((object)_003CPanelToStatic_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
		else
		{
			icon.onIconLoaded = new Action((object)_003CPanelToStatic_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
	}

	public virtual void OnQuery_SELECT_ORDER()
	{
		int num = (int)GameSection.GetEventData();
		if (num < 0 || num >= challengeData.Length)
		{
			GameSection.StopEvent();
		}
		else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog((uint)challengeData[num].questId, true))
		{
			GameSection.StopEvent();
		}
		else
		{
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)challengeData[num].questId, true);
			QuestInfoData questChallengeInfoData = MonoBehaviourSingleton<QuestManager>.I.GetQuestChallengeInfoData((uint)challengeData[num].questId);
			GameSection.SetEventData(questChallengeInfoData);
			isScrollViewReady = false;
		}
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

	private unsafe void Update()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		if (!isScrollViewReady)
		{
			TryScrollViewToReady();
		}
		if (isQuestItemDirty)
		{
			RefreshUI();
			Protocol.Force(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			isQuestItemDirty = false;
		}
	}

	private IEnumerator CheckLimitQuestItem()
	{
		if (!isQuestItemDirty)
		{
			List<QuestData> challengeList = MonoBehaviourSingleton<QuestManager>.I.challengeList;
			if (challengeList != null && challengeList.Count > 0)
			{
				float minRemainingSec = 3.40282347E+38f;
				QuestData questData = null;
				challengeList.ForEach(delegate(QuestData challengeQuest)
				{
					for (int i = 0; i < challengeQuest.remainTimes.Count; i++)
					{
						((_003CCheckLimitQuestItem_003Ec__Iterator83)/*Error near IL_007f: stateMachine*/)._003CparseRemainingSec_003E__3 = challengeQuest.remainTimes[i];
						if (!(((_003CCheckLimitQuestItem_003Ec__Iterator83)/*Error near IL_007f: stateMachine*/)._003CparseRemainingSec_003E__3 <= 0f) && ((_003CCheckLimitQuestItem_003Ec__Iterator83)/*Error near IL_007f: stateMachine*/)._003CminRemainingSec_003E__1 > ((_003CCheckLimitQuestItem_003Ec__Iterator83)/*Error near IL_007f: stateMachine*/)._003CparseRemainingSec_003E__3)
						{
							((_003CCheckLimitQuestItem_003Ec__Iterator83)/*Error near IL_007f: stateMachine*/)._003CminRemainingSec_003E__1 = ((_003CCheckLimitQuestItem_003Ec__Iterator83)/*Error near IL_007f: stateMachine*/)._003CparseRemainingSec_003E__3;
							((_003CCheckLimitQuestItem_003Ec__Iterator83)/*Error near IL_007f: stateMachine*/)._003CquestData_003E__2 = challengeQuest;
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
