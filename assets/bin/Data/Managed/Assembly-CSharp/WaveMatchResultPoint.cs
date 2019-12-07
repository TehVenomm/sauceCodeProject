using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMatchResultPoint : GameSection
{
	private enum UI
	{
		OBJ_TITLE,
		LBL_QUEST_NAME,
		SPR_TITLE,
		OBJ_WAVEMATCH_POINT,
		OBJ_GET_POINT,
		LBL_GET_POINT,
		GRD_POINT_DETAIL,
		OBJ_POINT_DETAIL_ITEM,
		LBL_POINT,
		LBL_POINT_NAME,
		STR_POINT_NEXT,
		OBJ_TOTAL_POINT,
		LBL_TOTAL_POINT,
		OBJ_NEXT_REWARD,
		GRD_NEXT_ITEM_ROOT,
		OBJ_NEXT_ITEM_ROOT,
		BTN_NEXT,
		BTN_SKIP_FULL_SCREEN,
		BTN_SKIP_IN_SCROLL,
		OBJ_GET_REWARD_ROOT,
		GRD_ITEM_ROOT,
		OBJ_ITEM_ROOT
	}

	private enum AUDIO
	{
		CATEGORY = 40000228,
		COUNTUP = 40000012,
		POINTREWARD = 40000230
	}

	private enum RESULT_ANIM_STATE
	{
		IDLE,
		POINT,
		COUNT_UP,
		NEXT_REWARD,
		REWARD,
		END
	}

	private const float COUNT_ANIM_SPEED = 4f;

	private bool is_skip;

	private PointEventCurrentData pointEvents;

	private RESULT_ANIM_STATE animState;

	public override void Initialize()
	{
		StartCoroutine(LoadSE());
	}

	private IEnumerator LoadSE()
	{
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(active: true);
		yield return new WaitForEndOfFrame();
		LoadingQueue loadingQueue = new LoadingQueue(this);
		int[] array = (int[])Enum.GetValues(typeof(AUDIO));
		foreach (int se_id in array)
		{
			loadingQueue.CacheSE(se_id);
		}
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		GC.Collect();
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(active: false);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (MonoBehaviourSingleton<QuestManager>.I.compData != null)
		{
			pointEvents = MonoBehaviourSingleton<QuestManager>.I.compData.waveMatchPoint;
		}
		else
		{
			pointEvents = MonoBehaviourSingleton<QuestManager>.I.retireData.waveMatchPoint;
		}
		PointEventCurrentData.PointResultData d = pointEvents.pointRankingData;
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
		SetLabelText(UI.LBL_QUEST_NAME, questData.questText);
		SetFontStyle(UI.LBL_GET_POINT, FontStyle.Italic);
		SetLabelText(UI.LBL_GET_POINT, "0pt");
		SetFontStyle(UI.LBL_TOTAL_POINT, FontStyle.Italic);
		SetLabelText(UI.LBL_TOTAL_POINT, d.userPoint.ToString("N0") + "pt");
		SetGrid(UI.GRD_POINT_DETAIL, "WaveMatchResultPointDetailItem", d.bonusPoint.Count, reset: true, delegate(int i, Transform t, bool is_recycle)
		{
			UILabel component = FindCtrl(t, UI.LBL_POINT).GetComponent<UILabel>();
			component.alpha = 1f;
			component.text = d.bonusPoint[i].point.ToString("N0");
			component.fontStyle = FontStyle.Italic;
			UILabel component2 = FindCtrl(t, UI.LBL_POINT_NAME).GetComponent<UILabel>();
			component2.alpha = 1f;
			component2.text = d.bonusPoint[i].name;
			component2.fontStyle = FontStyle.Italic;
		});
		if (d.nextReward != null)
		{
			List<PointEventCurrentData.Reward> reward = d.nextReward.reward;
			SetAllRewardItem(UI.GRD_NEXT_ITEM_ROOT, reward);
			SetPoint(UI.OBJ_NEXT_REWARD, d.nextReward.point - (d.userPoint + d.getPoint));
		}
		else
		{
			SetActive(UI.STR_POINT_NEXT, is_visible: false);
			SetFontStyle(GetCtrl(UI.OBJ_NEXT_REWARD), UI.LBL_POINT, FontStyle.Italic);
			SetLabelText(GetCtrl(UI.OBJ_NEXT_REWARD), UI.LBL_POINT, "None");
		}
		List<PointEventCurrentData.Reward> list = new List<PointEventCurrentData.Reward>();
		foreach (PointEventCurrentData.PointRewardData item in d.getReward)
		{
			list.AddRange(item.reward);
		}
		SetAllRewardItem(UI.GRD_ITEM_ROOT, list);
		StartCoroutine(PlayAnimation());
	}

	private void SetAllRewardItem(UI targetGrid, List<PointEventCurrentData.Reward> rewardList)
	{
		SetGrid(targetGrid, "ItemIconReward", rewardList.Count, reset: true, delegate(int i, Transform t, bool is_recycle)
		{
			PointEventCurrentData.Reward reward = rewardList[i];
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward.type, (uint)reward.itemId, t, reward.num);
			if (itemIcon != null)
			{
				itemIcon.SetEnableCollider(is_enable: false);
			}
			t.Find("itemNum").GetComponent<UILabel>().text = "×" + rewardList[i].num;
			if (targetGrid == UI.GRD_NEXT_ITEM_ROOT)
			{
				t.localScale = new Vector3(0.7f, 0.7f, 1f);
				if (i > 2)
				{
					itemIcon.VisibleIcon(is_visible: false);
				}
			}
		});
	}

	private void SetPoint(UI parent, int point)
	{
		SetFontStyle(GetCtrl(parent), UI.LBL_POINT, FontStyle.Italic);
		SetLabelText(GetCtrl(parent), UI.LBL_POINT, point.ToString("N0") + "pt");
	}

	private void AddPointEventData(PointEventCurrentData add_data)
	{
		if (add_data != null && Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).eventId == add_data.eventId)
		{
			pointEvents.pointRankingData.getPoint += add_data.pointRankingData.getPoint;
			foreach (PointEventCurrentData.BonusPointData add_bonus in add_data.pointRankingData.bonusPoint)
			{
				PointEventCurrentData.BonusPointData bonusPointData = pointEvents.pointRankingData.bonusPoint.Find((PointEventCurrentData.BonusPointData bp) => bp.name == add_bonus.name);
				if (bonusPointData == null)
				{
					pointEvents.pointRankingData.bonusPoint.Add(add_bonus);
				}
				else
				{
					bonusPointData.point += add_bonus.point;
				}
			}
			pointEvents.pointRankingData.nextReward = add_data.pointRankingData.nextReward;
			pointEvents.pointRankingData.getReward.AddRange(add_data.pointRankingData.getReward);
		}
	}

	private IEnumerator PlayAnimation()
	{
		is_skip = false;
		PlayTween(UI.OBJ_TITLE);
		SkipTween(UI.OBJ_TITLE);
		animState = RESULT_ANIM_STATE.POINT;
		PlayTween(UI.OBJ_WAVEMATCH_POINT, forward: true, delegate
		{
			SoundManager.PlayOneShotUISE(40000228);
			animState = RESULT_ANIM_STATE.IDLE;
		}, is_input_block: false);
		while (animState != 0 && !is_skip)
		{
			yield return null;
		}
		animState = RESULT_ANIM_STATE.COUNT_UP;
		StartCoroutine(GetPointAnimation(delegate
		{
			animState = RESULT_ANIM_STATE.IDLE;
		}));
		while (animState != 0 && !is_skip)
		{
			yield return null;
		}
		animState = RESULT_ANIM_STATE.NEXT_REWARD;
		PlayTween(UI.OBJ_NEXT_REWARD, forward: true, delegate
		{
			animState = RESULT_ANIM_STATE.IDLE;
		}, is_input_block: false);
		SoundManager.PlayOneShotUISE(40000228);
		if (pointEvents.pointRankingData.getReward.Count > 0)
		{
			animState = RESULT_ANIM_STATE.REWARD;
			PlayTween(UI.OBJ_GET_REWARD_ROOT, forward: true, delegate
			{
				animState = RESULT_ANIM_STATE.IDLE;
			}, is_input_block: false);
		}
		animState = RESULT_ANIM_STATE.END;
		VisibleEndButton();
	}

	private IEnumerator GetPointAnimation(Action callback)
	{
		int getPoint = pointEvents.pointRankingData.getPoint;
		int userPoint = pointEvents.pointRankingData.userPoint;
		int totalPoint = userPoint + getPoint;
		SetFontStyle(UI.LBL_GET_POINT, FontStyle.Italic);
		yield return StartCoroutine(CountUpAnimation(0f, getPoint, UI.LBL_GET_POINT));
		SetFontStyle(UI.LBL_TOTAL_POINT, FontStyle.Italic);
		yield return StartCoroutine(CountUpAnimation(userPoint, totalPoint, UI.LBL_TOTAL_POINT));
		callback();
	}

	private IEnumerator CountUpAnimation(float currentPoint, int targetPoint, UI targetUI)
	{
		while (currentPoint < (float)targetPoint)
		{
			yield return 0;
			if (is_skip)
			{
				currentPoint = targetPoint;
			}
			int num = Mathf.FloorToInt(currentPoint);
			float num2 = Mathf.Max(((float)targetPoint - currentPoint) * CountDownCube(Time.deltaTime * 4f), 1f);
			currentPoint += num2;
			currentPoint = Mathf.Min(currentPoint, targetPoint);
			if (num < Mathf.FloorToInt(currentPoint))
			{
				SoundManager.PlayOneShotUISE(40000012);
			}
			SetLabelText(targetUI, Mathf.FloorToInt(currentPoint).ToString("N0") + "pt");
		}
	}

	private static float CountDownCube(float currentValue)
	{
		return currentValue * (2f - currentValue);
	}

	private void VisibleEndButton()
	{
		SetActive(UI.BTN_NEXT, animState == RESULT_ANIM_STATE.END);
		SetActive(UI.BTN_SKIP_FULL_SCREEN, animState != RESULT_ANIM_STATE.END);
		SetActive(UI.BTN_SKIP_IN_SCROLL, animState != RESULT_ANIM_STATE.END);
	}

	private void OnQuery_SKIP()
	{
		is_skip = true;
		switch (animState)
		{
		case RESULT_ANIM_STATE.POINT:
		case RESULT_ANIM_STATE.COUNT_UP:
		case RESULT_ANIM_STATE.NEXT_REWARD:
			SkipTween(UI.OBJ_WAVEMATCH_POINT);
			SkipTween(UI.OBJ_NEXT_REWARD);
			break;
		case RESULT_ANIM_STATE.REWARD:
			SkipTween(UI.OBJ_GET_REWARD_ROOT);
			break;
		}
	}

	private void OnQuery_NEXT()
	{
		if (MonoBehaviourSingleton<QuestManager>.I.compData == null)
		{
			GameSection.ChangeEvent("FAILED");
		}
	}
}
