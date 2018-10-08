using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaResultTop : QuestResultTop
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
		BTN_RETRY,
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
		ACHIEVEMENT = 40000028,
		ADVENT = 40000026,
		ARRIVAL = 40000269,
		CATEGORY = 40000228,
		COUNTUP = 40000012,
		POINTREWARD = 40000230
	}

	private new enum RESULT_ANIM_STATE
	{
		IDLE,
		TITLE,
		DROP,
		REMAIN_TIME,
		CLEAR_TIME_COUNT_UP,
		CLEAR_EFFECT,
		BEST_SCORE,
		EVENT,
		END
	}

	private const float COUNT_ANIM_SPEED = 4f;

	private bool is_skip;

	private ResultReward[] resultRewards;

	private PointEventCurrentData allPointEvents;

	private ARENA_RANK m_rank;

	private ARENA_GROUP m_group;

	private bool m_isTimeAttack;

	private new RESULT_ANIM_STATE animState;

	public override void Initialize()
	{
		m_rank = MonoBehaviourSingleton<InGameManager>.I.GetCurrentArenaRank();
		m_group = MonoBehaviourSingleton<InGameManager>.I.GetCurrentArenaGroup();
		m_isTimeAttack = MonoBehaviourSingleton<InGameManager>.I.IsArenaTimeAttack();
		base.Initialize();
		UITexture component = GetCtrl(UI.TEX_RANK_PRE).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(m_rank));
		UITexture component2 = GetCtrl(UI.TEX_RANK_NEW).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component2, RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(m_rank + 1));
		if (MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
	}

	protected override void InitReward()
	{
		List<ResultReward> list = new List<ResultReward>();
		dropItemNum = 0;
		dropLineNum = 0;
		eventRewardTitles = new List<string>();
		if (MonoBehaviourSingleton<InGameManager>.I.arenaRewards.Count > 0)
		{
			foreach (QuestCompleteRewardList arenaReward in MonoBehaviourSingleton<InGameManager>.I.arenaRewards)
			{
				ResultReward resultReward = new ResultReward();
				DevideRewardDropAndEvent(resultReward, arenaReward.drop);
				List<SortCompareData> list2 = new List<SortCompareData>();
				int start_ary_index = 0;
				start_ary_index = ResultUtility.SetDropData(list2, start_ary_index, resultReward.dropReward.item, REWARD_CATEGORY.DROP);
				start_ary_index = ResultUtility.SetDropData(list2, start_ary_index, resultReward.dropReward.equipItem, REWARD_CATEGORY.DROP);
				start_ary_index = ResultUtility.SetDropData(list2, start_ary_index, resultReward.dropReward.skillItem, REWARD_CATEGORY.DROP);
				start_ary_index = ResultUtility.SetDropData(list2, start_ary_index, resultReward.dropReward.questItem, REWARD_CATEGORY.DROP);
				list2.Sort((SortCompareData l, SortCompareData r) => r.GetSortValueQuestResult() - l.GetSortValueQuestResult());
				resultReward.dropItemIconData = list2.ToArray();
				dropItemNum += resultReward.dropItemIconData.Length;
				list.Add(resultReward);
			}
		}
		pointShopResultData = (MonoBehaviourSingleton<InGameManager>.I.arenaPointShops ?? new List<PointShopResultData>());
		resultRewards = list.ToArray();
	}

	protected override void OnClose()
	{
		try
		{
			if (MonoBehaviourSingleton<InGameManager>.IsValid() && !MonoBehaviourSingleton<InGameManager>.I.isRetry)
			{
				MonoBehaviourSingleton<InGameManager>.I.ClearArenaInfo();
			}
			base.OnClose();
		}
		catch (Exception ex)
		{
			Log.Warning(LOG.UI, "ArenaResultTop OnClose\n{0}\n{1}", ex.Message, ex.StackTrace);
		}
	}

	public override void UpdateUI()
	{
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		allPointEvents = new PointEventCurrentData();
		allPointEvents.pointRankingData = new PointEventCurrentData.PointResultData();
		isVictory = (MonoBehaviourSingleton<QuestManager>.I.arenaCompData != null);
		SetFullScreenButton((Enum)UI.BTN_SKIP_FULL_SCREEN);
		SetActive((Enum)UI.BTN_NEXT, false);
		SetActive((Enum)UI.BTN_RETRY, false);
		SetActive((Enum)UI.OBJ_TIME, false);
		SetActive((Enum)UI.OBJ_CLEAR_EFFECT_ROOT, false);
		SetActive((Enum)UI.OBJ_CLEAR_EFFECT, false);
		SetActive((Enum)UI.OBJ_RANK_UP_ROOT, false);
		SetActive((Enum)UI.OBJ_CONGRATULATIONS_ROOT, false);
		if (m_isTimeAttack)
		{
			SetActive((Enum)UI.OBJ_REMAIN_TIME, false);
			if (isVictory)
			{
				SetActive((Enum)UI.OBJ_TIME, true);
			}
		}
		string arg = string.Format(StringTable.Get(STRING_CATEGORY.ARENA, 1u), m_rank.ToString());
		string arg2 = string.Format(StringTable.Get(STRING_CATEGORY.ARENA, 0u), m_group.ToString());
		SetLabelText((Enum)UI.LBL_QUEST_NAME, $"{arg} {arg2}");
		List<QuestCompleteRewardList> arenaRewards = MonoBehaviourSingleton<InGameManager>.I.arenaRewards;
		int num = 0;
		int num2 = 0;
		for (int j = 0; j < arenaRewards.Count; j++)
		{
			QuestCompleteRewardList questCompleteRewardList = arenaRewards[j];
			QuestCompleteReward drop = questCompleteRewardList.drop;
			QuestCompleteReward breakReward = questCompleteRewardList.breakReward;
			QuestCompleteReward order = questCompleteRewardList.order;
			num += drop.exp + breakReward.exp + order.exp;
			num2 += drop.money + breakReward.money + order.money;
		}
		int my_user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		InGameRecorder.PlayerRecord playerRecord = MonoBehaviourSingleton<InGameRecorder>.I.players.Find((InGameRecorder.PlayerRecord data) => data.charaInfo.userId == my_user_id);
		if (playerRecord.beforeLevel >= Singleton<UserLevelTable>.I.GetMaxLevel())
		{
			num = 0;
		}
		SetLabelText((Enum)UI.LBL_EXP, num.ToString("N0"));
		SetLabelText((Enum)UI.LBL_REWARD_GOLD, num2.ToString("N0"));
		SetLabelText((Enum)UI.LBL_TIME, MonoBehaviourSingleton<InGameProgress>.I.GetArenaRemainTimeToString());
		SetLabelText((Enum)UI.LBL_CLEAR_TIME, InGameProgress.GetTimeWithMilliSecToString(0f));
		SetActive((Enum)UI.SPR_BESTSCORE, false);
		if (isVictory)
		{
			SetLabelText((Enum)UI.LBL_BEFORE_TIME, InGameProgress.GetTimeWithMilliSecToString((float)(int)MonoBehaviourSingleton<QuestManager>.I.arenaCompData.previousClearMilliSec * 0.001f));
		}
		bool flag = pointShopResultData.Count > 0;
		SetActive((Enum)UI.OBJ_POINT_SHOP_RESULT_ROOT, flag);
		if (flag)
		{
			SetGrid(UI.OBJ_POINT_SHOP_RESULT_ROOT, "QuestResultPointShop", pointShopResultData.Count, true, delegate(int i, Transform t, bool b)
			{
				ResetTween(t, 0);
				PointShopResultData pointShopResultData = base.pointShopResultData[i];
				SetActive(t, UI.OBJ_NORMAL_POINT_SHOP_ROOT, !pointShopResultData.isEvent);
				if (!pointShopResultData.isEvent)
				{
					SetLabelText(t, UI.LBL_NORMAL_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.getPoint));
					SetLabelText(t, UI.LBL_NORMAL_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.totalPoint));
					UITexture component = FindCtrl(t, UI.TEX_NORMAL_POINT_SHOP_ICON).GetComponent<UITexture>();
					ResourceLoad.LoadPointIconImageTexture(component, (uint)pointShopResultData.pointShopId);
				}
				SetActive(t, UI.OBJ_EVENT_POINT_SHOP_ROOT, pointShopResultData.isEvent);
				if (pointShopResultData.isEvent)
				{
					SetLabelText(t, UI.LBL_EVENT_GET_POINT_SHOP, string.Format("+" + StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.getPoint));
					SetLabelText(t, UI.LBL_EVENT_TOTAL_POINT_SHOP, string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), pointShopResultData.totalPoint));
					UITexture component2 = FindCtrl(t, UI.TEX_EVENT_POINT_SHOP_ICON).GetComponent<UITexture>();
					ResourceLoad.LoadPointIconImageTexture(component2, (uint)pointShopResultData.pointShopId);
				}
			});
		}
		this.StartCoroutine(PlayAnimation());
	}

	private void PlayAudio(AUDIO type)
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			SoundManager.PlayOneShotUISE((int)type);
		}
	}

	private IEnumerator PlayAnimation()
	{
		is_skip = false;
		animState = RESULT_ANIM_STATE.TITLE;
		PlayAudio(AUDIO.ADVENT);
		PlayTween((Enum)UI.OBJ_TITLE, true, (EventDelegate.Callback)delegate
		{
			((_003CPlayAnimation_003Ec__Iterator11)/*Error near IL_007b: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
		}, false, 0);
		while (animState != 0 && !is_skip)
		{
			yield return (object)null;
		}
		animState = RESULT_ANIM_STATE.DROP;
		PlayAudio(AUDIO.ACHIEVEMENT);
		if (pointShopResultData.Count > 0)
		{
			foreach (Transform item in GetCtrl(UI.OBJ_POINT_SHOP_RESULT_ROOT).get_transform())
			{
				Transform t = item;
				PlayTween(t, true, null, true, 0);
			}
		}
		PlayTween((Enum)UI.OBJ_EXP, true, (EventDelegate.Callback)null, true, 0);
		PlayTween((Enum)UI.OBJ_MONEY, true, (EventDelegate.Callback)delegate
		{
			((_003CPlayAnimation_003Ec__Iterator11)/*Error near IL_019a: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
		}, false, 0);
		while (animState != 0 && !is_skip)
		{
			yield return (object)null;
		}
		if (!m_isTimeAttack)
		{
			animState = RESULT_ANIM_STATE.REMAIN_TIME;
			PlayTween((Enum)UI.OBJ_REMAIN_TIME, true, (EventDelegate.Callback)delegate
			{
				SoundManager.PlayOneShotUISE(40000228);
				((_003CPlayAnimation_003Ec__Iterator11)/*Error near IL_020f: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
			}, false, 0);
			while (animState != 0 && !is_skip)
			{
				yield return (object)null;
			}
			if (isVictory)
			{
				if (m_rank == ARENA_RANK.SS)
				{
					SetActive((Enum)UI.OBJ_CONGRATULATIONS_ROOT, true);
					ResetTween((Enum)UI.OBJ_CONGRATULATIONS, 0);
					animState = RESULT_ANIM_STATE.CLEAR_EFFECT;
					ParticleSystem particle2 = GetCtrl(UI.OBJ_CONGRATULATIONS_PARTICLE).GetComponent<ParticleSystem>();
					particle2.GetComponent<ParticleSystemRenderer>().get_sharedMaterial().set_renderQueue(4000);
					yield return (object)null;
					PlayAudio(AUDIO.ARRIVAL);
					PlayTween((Enum)UI.OBJ_CONGRATULATIONS, true, (EventDelegate.Callback)delegate
					{
						((_003CPlayAnimation_003Ec__Iterator11)/*Error near IL_0315: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
					}, true, 0);
					while (animState != 0 && !is_skip)
					{
						yield return (object)null;
					}
				}
				else
				{
					SetActive((Enum)UI.OBJ_RANK_UP_ROOT, true);
					ResetTween((Enum)UI.OBJ_RANK_UP, 0);
					animState = RESULT_ANIM_STATE.CLEAR_EFFECT;
					ParticleSystem particle = GetCtrl(UI.OBJ_PARTICLE).GetComponent<ParticleSystem>();
					particle.GetComponent<ParticleSystemRenderer>().get_sharedMaterial().set_renderQueue(4000);
					yield return (object)null;
					PlayAudio(AUDIO.ARRIVAL);
					PlayTween((Enum)UI.OBJ_RANK_UP, true, (EventDelegate.Callback)delegate
					{
						((_003CPlayAnimation_003Ec__Iterator11)/*Error near IL_03ff: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
					}, true, 0);
					while (animState != 0 && !is_skip)
					{
						yield return (object)null;
					}
				}
			}
		}
		if (m_isTimeAttack)
		{
			animState = RESULT_ANIM_STATE.CLEAR_TIME_COUNT_UP;
			this.StartCoroutine(PlayCountUpClearTimeAnim(MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedTime(), delegate
			{
				((_003CPlayAnimation_003Ec__Iterator11)/*Error near IL_047c: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
			}));
			while (animState != 0 && !is_skip)
			{
				yield return (object)null;
			}
			if (IsBreakRecord())
			{
				animState = RESULT_ANIM_STATE.BEST_SCORE;
				PlayAudio(AUDIO.ARRIVAL);
				SetActive((Enum)UI.SPR_BESTSCORE, true);
				PlayTween((Enum)UI.SPR_BESTSCORE, true, (EventDelegate.Callback)delegate
				{
					((_003CPlayAnimation_003Ec__Iterator11)/*Error near IL_0518: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
				}, true, 0);
				while (animState != 0 && !is_skip)
				{
					yield return (object)null;
				}
			}
		}
		animState = RESULT_ANIM_STATE.EVENT;
		OpenAllEventRewardDialog(delegate
		{
			((_003CPlayAnimation_003Ec__Iterator11)/*Error near IL_0576: stateMachine*/)._003C_003Ef__this.animState = RESULT_ANIM_STATE.IDLE;
		});
		while (animState != 0 && !is_skip)
		{
			yield return (object)null;
		}
		animState = RESULT_ANIM_STATE.END;
		VisibleEndButton();
	}

	private IEnumerator GetPointAnimation(Action callback)
	{
		int getPoint = allPointEvents.pointRankingData.getPoint;
		int userPoint = allPointEvents.pointRankingData.userPoint;
		yield return (object)null;
		callback();
	}

	private IEnumerator PlayCountUpClearTimeAnim(float targetTime, Action callBack)
	{
		float currentShowTime2 = 0f;
		while (currentShowTime2 < targetTime)
		{
			yield return (object)null;
			if (is_skip)
			{
				currentShowTime2 = targetTime;
			}
			int before = Mathf.FloorToInt(currentShowTime2);
			float addingTime = Mathf.Max((targetTime - currentShowTime2) * CountDownCube(Time.get_deltaTime() * 4f), 1f);
			currentShowTime2 += addingTime;
			currentShowTime2 = Mathf.Min(currentShowTime2, targetTime);
			if (before < Mathf.FloorToInt(currentShowTime2))
			{
				SoundManager.PlayOneShotUISE(40000012);
			}
			SetLabelText((Enum)UI.LBL_CLEAR_TIME, InGameProgress.GetTimeWithMilliSecToString(currentShowTime2));
		}
		callBack?.Invoke();
	}

	private float CountDownCube(float currentValue)
	{
		return currentValue * (2f - currentValue);
	}

	protected override void VisibleEndButton()
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		SetActive((Enum)UI.BTN_NEXT, animState == RESULT_ANIM_STATE.END);
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, animState != RESULT_ANIM_STATE.END);
		if (m_isTimeAttack || MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_RETIRE)
		{
			SetActive((Enum)UI.BTN_RETRY, animState == RESULT_ANIM_STATE.END);
		}
		else
		{
			SetActive((Enum)UI.BTN_RETRY, false);
			Vector3 localPosition = GetCtrl(UI.BTN_NEXT).get_localPosition();
			GetCtrl(UI.BTN_NEXT).set_localPosition(new Vector3(0f, localPosition.y, localPosition.x));
		}
	}

	private bool IsBreakRecord()
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return false;
		}
		if (!MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			return false;
		}
		if (MonoBehaviourSingleton<QuestManager>.I.arenaCompData == null)
		{
			return false;
		}
		int num = Mathf.FloorToInt(MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedTime() * 1000f);
		int num2 = MonoBehaviourSingleton<QuestManager>.I.arenaCompData.previousClearMilliSec;
		if (num < num2)
		{
			return true;
		}
		return false;
	}

	private void DevideRewardDropAndEvent(ResultReward resultReward, QuestCompleteReward reward)
	{
		resultReward.dropReward = new QuestCompleteReward();
		resultReward.eventReward = new QuestCompleteReward();
		List<string> list = new List<string>();
		resultReward.dropReward.exp = reward.exp;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < reward.eventPrice.Count; i++)
		{
			num += reward.eventPrice[i].gold;
			num2 += reward.eventPrice[i].gold;
			resultReward.eventReward.eventPrice.Add(reward.eventPrice[i]);
			list.Add(reward.eventPrice[i].rewardTitle);
		}
		resultReward.dropReward.money = Mathf.Max(0, reward.money - num);
		resultReward.dropReward.crystal = Mathf.Max(0, reward.crystal - num2);
		for (int j = 0; j < reward.item.Count; j++)
		{
			if (string.IsNullOrEmpty(reward.item[j].rewardTitle))
			{
				resultReward.dropReward.item.Add(reward.item[j]);
			}
			else
			{
				resultReward.eventReward.item.Add(reward.item[j]);
				list.Add(reward.item[j].rewardTitle);
			}
		}
		for (int k = 0; k < reward.skillItem.Count; k++)
		{
			if (string.IsNullOrEmpty(reward.skillItem[k].rewardTitle))
			{
				resultReward.dropReward.skillItem.Add(reward.skillItem[k]);
			}
			else
			{
				resultReward.eventReward.skillItem.Add(reward.skillItem[k]);
				list.Add(reward.skillItem[k].rewardTitle);
			}
		}
		for (int l = 0; l < reward.equipItem.Count; l++)
		{
			if (string.IsNullOrEmpty(reward.equipItem[l].rewardTitle))
			{
				resultReward.dropReward.equipItem.Add(reward.equipItem[l]);
			}
			else
			{
				resultReward.eventReward.equipItem.Add(reward.equipItem[l]);
				list.Add(reward.equipItem[l].rewardTitle);
			}
		}
		for (int m = 0; m < reward.questItem.Count; m++)
		{
			if (string.IsNullOrEmpty(reward.questItem[m].rewardTitle))
			{
				resultReward.dropReward.questItem.Add(reward.questItem[m]);
			}
			else
			{
				resultReward.eventReward.questItem.Add(reward.questItem[m]);
				list.Add(reward.questItem[m].rewardTitle);
			}
		}
		for (int n = 0; n < list.Count; n++)
		{
			if (!eventRewardTitles.Contains(list[n]))
			{
				eventRewardTitles.Add(list[n]);
			}
		}
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
		ResultReward[] array = resultRewards;
		foreach (ResultReward resultReward in array)
		{
			QuestCompleteReward eventReward = resultReward.eventReward;
			for (int k = 0; k < eventReward.eventPrice.Count; k++)
			{
				for (int l = 0; l < eventRewardTitles.Count; l++)
				{
					if (eventRewardTitles[l] == eventReward.eventPrice[k].rewardTitle)
					{
						eventRewardList[l].eventPrice.Add(eventReward.eventPrice[k]);
					}
				}
			}
			for (int m = 0; m < eventReward.item.Count; m++)
			{
				for (int n = 0; n < eventRewardTitles.Count; n++)
				{
					if (eventRewardTitles[n] == eventReward.item[m].rewardTitle)
					{
						eventRewardList[n].item.Add(eventReward.item[m]);
					}
				}
			}
			for (int num = 0; num < eventReward.skillItem.Count; num++)
			{
				for (int num2 = 0; num2 < eventRewardTitles.Count; num2++)
				{
					if (eventRewardTitles[num2] == eventReward.skillItem[num].rewardTitle)
					{
						eventRewardList[num2].skillItem.Add(eventReward.skillItem[num]);
					}
				}
			}
			for (int num3 = 0; num3 < eventReward.equipItem.Count; num3++)
			{
				for (int num4 = 0; num4 < eventRewardTitles.Count; num4++)
				{
					if (eventRewardTitles[num4] == eventReward.equipItem[num3].rewardTitle)
					{
						eventRewardList[num4].equipItem.Add(eventReward.equipItem[num3]);
					}
				}
			}
			for (int num5 = 0; num5 < eventReward.questItem.Count; num5++)
			{
				for (int num6 = 0; num6 < eventRewardTitles.Count; num6++)
				{
					if (eventRewardTitles[num6] == eventReward.questItem[num5].rewardTitle)
					{
						eventRewardList[num6].questItem.Add(eventReward.questItem[num5]);
					}
				}
			}
		}
		if (eventRewardList.Count == 0)
		{
			endCallback?.Invoke();
		}
		else
		{
			OpenEventRewardDialog(eventRewardList[eventRewardIndex], eventRewardTitles[eventRewardIndex], endCallback);
			eventRewardIndex++;
		}
	}

	private void OnQuery_SKIP()
	{
		switch (animState)
		{
		case RESULT_ANIM_STATE.TITLE:
		case RESULT_ANIM_STATE.DROP:
		case RESULT_ANIM_STATE.REMAIN_TIME:
			SkipTween((Enum)UI.OBJ_TITLE, true, 0);
			SkipTween((Enum)UI.OBJ_POINT_SHOP_RESULT_ROOT, true, 0);
			SkipTween((Enum)UI.OBJ_EXP, true, 0);
			SkipTween((Enum)UI.OBJ_MONEY, true, 0);
			SkipTween((Enum)UI.OBJ_REMAIN_TIME, true, 0);
			break;
		case RESULT_ANIM_STATE.BEST_SCORE:
			SkipTween((Enum)UI.SPR_BESTSCORE, true, 0);
			break;
		case RESULT_ANIM_STATE.CLEAR_EFFECT:
			SkipTween((Enum)UI.OBJ_RANK_UP, true, 0);
			break;
		}
		is_skip = true;
		GameSection.StopEvent();
	}

	private void OnQuery_NEXT()
	{
		if (animState == RESULT_ANIM_STATE.IDLE)
		{
			GoArenaList();
		}
		else if (animState != RESULT_ANIM_STATE.END)
		{
			OnQuery_SKIP();
		}
		else
		{
			GoArenaList();
		}
	}

	private void GoArenaList()
	{
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.ClearArenaInfo();
		}
		string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
		EventData[] autoEvents = new EventData[2]
		{
			new EventData(name),
			new EventData("ARENA_LIST")
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private void OnQuery_RETRY()
	{
		if (animState == RESULT_ANIM_STATE.IDLE)
		{
			ReloadScene();
		}
		else if (animState != RESULT_ANIM_STATE.END)
		{
			OnQuery_SKIP();
		}
		else
		{
			ReloadScene();
		}
	}

	private void ReloadScene()
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.Clear();
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.isRetry = true;
		}
		MonoBehaviourSingleton<GameSceneManager>.I.ReloadScene(UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}

	protected override string GetSceneName()
	{
		return "ArenaResultTop";
	}
}
