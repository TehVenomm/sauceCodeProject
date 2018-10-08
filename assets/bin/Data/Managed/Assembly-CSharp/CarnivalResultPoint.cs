using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivalResultPoint : GameSection
{
	public enum UI
	{
		TXT_BANNER,
		OBJ_GET_REWARD_ROOT,
		LBL_GET_POINT,
		LBL_TOTAL_POINT,
		OBJ_NEXT_REWARD_ITEM_ICON_ROOT,
		BTN_SKIP_FULL_SCREEN,
		BTN_OK,
		SPR_LOGO,
		OBJ_REWARD_POS,
		OBJ_REWARD_ROOT,
		OBJ_RANKING_ROOT,
		OBJ_RANKING_ANIM_ROOT,
		OBJ_PASS_ANIM_ROOT,
		LBL_RANKING_TOTAL_POINT,
		LBL_PASS_NUM,
		BTN_END_OK,
		GRD_POSITION_PASS,
		SPR_POSITION_PASS,
		SPR_PASS_TEXT,
		GRD_COUNT_NUMBERS,
		OBJ_BONUS_ROOT,
		OBJ_BONUS_ANIM_ROOT,
		OBJ_CARNIVAL_POINT,
		GRD_POINT_DETAIL,
		LBL_POINT,
		LBL_POINT_NAME,
		GRD_NEXT_ITEM_ROOT,
		OBJ_NEXT_REWARD,
		STR_POINT_NEXT,
		GRD_ITEM_ROOT
	}

	private enum State
	{
		START,
		TO_REWARD,
		REWARD,
		BONUS,
		RANKING
	}

	private enum AUDIO
	{
		RESULT = 40000049,
		TITLE_LOGO = 40000227,
		CATEGORY = 40000228,
		POINTUP = 40000229,
		POINTREWARD = 40000230,
		START_BONUS_TIME = 40000268
	}

	private enum RESULT_TYPE
	{
		NORMAL,
		RUSH,
		WAVE
	}

	private const string BOOST_BONUS_NAME = "ブ\u30fcストポイント";

	private const string TITLE_LOGO_NAME_FORMAT = "ef_ui_pointresult";

	private const float COUNT_ANIM_SPEED = 4f;

	private const float FADE_SPEED = 5f;

	private RESULT_TYPE resultType;

	private State pointResultState;

	private bool stateInitialized;

	private PointEventCurrentData currentData;

	private int boostPoint;

	private float boostRate;

	private bool skipRequest;

	private Transform bannerCtrl;

	private MeshRenderer[] bannerMeshRenderers;

	private readonly string[] SPR_RANKING_NUMBER = new string[10]
	{
		"RankingNumber2_9",
		"RankingNumber2_7",
		"RankingNumber_0",
		"RankingNumber2_6",
		"RankingNumber_3",
		"RankingNumber_1",
		"RankingNumber_2",
		"RankingNumber_4",
		"RankingNumber_5",
		"RankingNumber2_8"
	};

	private readonly string[] SPR_PASS_NUMBER = new string[10]
	{
		"RankingNumber_6",
		"RankingNumber_7",
		"RankingNumber_8",
		"RankingNumber_9",
		"RankingNumber2_0",
		"RankingNumber2_1",
		"RankingNumber2_2",
		"RankingNumber2_3",
		"RankingNumber2_4",
		"RankingNumber2_5"
	};

	private List<GameObject> rankingNumbers;

	private List<GameObject> passPositionNumbers;

	private int beforeDigits;

	public override void Initialize()
	{
		SetPointEventData();
		SetActive(UI.OBJ_REWARD_ROOT, false);
		SetActive(UI.OBJ_RANKING_ROOT, false);
		SetActive(UI.OBJ_BONUS_ROOT, false);
		bannerCtrl = GetCtrl(UI.TXT_BANNER);
		StartCoroutine(DoInitalize());
	}

	private void SetPointEventData()
	{
		PointEventCurrentData pointEventCurrentData = new PointEventCurrentData();
		pointEventCurrentData.pointRankingData = new PointEventCurrentData.PointResultData();
		if (MonoBehaviourSingleton<InGameManager>.I.rushPointEvents != null)
		{
			resultType = RESULT_TYPE.RUSH;
			int i = 0;
			for (int count = MonoBehaviourSingleton<InGameManager>.I.rushPointEvents.Count; i < count; i++)
			{
				PointEventCurrentData pointEventCurrentData2 = MonoBehaviourSingleton<InGameManager>.I.rushPointEvents[i];
				if (i == 0)
				{
					pointEventCurrentData.pointRankingData.userPoint = pointEventCurrentData2.pointRankingData.userPoint;
					pointEventCurrentData.pointRankingData.beforeRank = pointEventCurrentData2.pointRankingData.beforeRank;
				}
				if (i == count - 1)
				{
					pointEventCurrentData.pointRankingData.afterRank = pointEventCurrentData2.pointRankingData.afterRank;
					pointEventCurrentData.pointRankingData.isStartedBoost = pointEventCurrentData2.pointRankingData.isStartedBoost;
				}
				AddPointEventData(pointEventCurrentData2, pointEventCurrentData);
			}
			currentData = pointEventCurrentData;
		}
		else if (MonoBehaviourSingleton<QuestManager>.I.compData != null && MonoBehaviourSingleton<QuestManager>.I.compData.waveMatchPoint != null)
		{
			resultType = RESULT_TYPE.WAVE;
			currentData = MonoBehaviourSingleton<QuestManager>.I.compData.waveMatchPoint;
		}
		else if (MonoBehaviourSingleton<QuestManager>.I.retireData != null && MonoBehaviourSingleton<QuestManager>.I.retireData.waveMatchPoint != null)
		{
			resultType = RESULT_TYPE.WAVE;
			currentData = MonoBehaviourSingleton<QuestManager>.I.retireData.waveMatchPoint;
		}
		else
		{
			currentData = (GameSection.GetEventData() as PointEventCurrentData);
			if (currentData != null)
			{
				resultType = RESULT_TYPE.NORMAL;
			}
			else if (currentData == null)
			{
				Debug.LogError("CarnivalResultDataが存在しません!!!");
				currentData = new PointEventCurrentData();
				currentData.pointRankingData = new PointEventCurrentData.PointResultData();
			}
		}
	}

	private void AddPointEventData(PointEventCurrentData addData, PointEventCurrentData allPointEvents)
	{
		if (addData != null)
		{
			allPointEvents.pointRankingData.getPoint += addData.pointRankingData.getPoint;
			using (List<PointEventCurrentData.BonusPointData>.Enumerator enumerator = addData.pointRankingData.bonusPoint.GetEnumerator())
			{
				PointEventCurrentData.BonusPointData add_bonus;
				while (enumerator.MoveNext())
				{
					add_bonus = enumerator.Current;
					PointEventCurrentData.BonusPointData bonusPointData = allPointEvents.pointRankingData.bonusPoint.Find((PointEventCurrentData.BonusPointData bp) => bp.name == add_bonus.name);
					if (bonusPointData == null)
					{
						allPointEvents.pointRankingData.bonusPoint.Add(add_bonus);
					}
					else
					{
						bonusPointData.point += add_bonus.point;
					}
				}
			}
			allPointEvents.pointRankingData.nextReward = addData.pointRankingData.nextReward;
			allPointEvents.pointRankingData.getReward.AddRange(addData.pointRankingData.getReward);
		}
	}

	private IEnumerator DoInitalize()
	{
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
		yield return (object)new WaitForEndOfFrame();
		LoadingQueue loadQueue = new LoadingQueue(this);
		loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_pointresult");
		LoadingQueue load_queue = new LoadingQueue(this);
		int[] ids = (int[])Enum.GetValues(typeof(AUDIO));
		int[] array = ids;
		foreach (int id in array)
		{
			load_queue.CacheSE(id, null);
		}
		ResourceLoad.LoadEventBannerResultTexture(GetCtrl(UI.SPR_LOGO).GetComponent<UITexture>(), (uint)currentData.eventId);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		GC.Collect();
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
		SetVisibleWidgetEffect(UI.TXT_BANNER, "ef_ui_pointresult");
		UIVisibleWidgetEffect visibleWidget = bannerCtrl.GetComponent<UIVisibleWidgetEffect>();
		visibleWidget.SetRendererQueue(4000);
		PlayAudio(AUDIO.TITLE_LOGO);
		base.Initialize();
	}

	private void Update()
	{
		switch (pointResultState)
		{
		case State.START:
		{
			Animation componentInChildren = bannerCtrl.GetComponentInChildren<Animation>(true);
			if (!((UnityEngine.Object)componentInChildren == (UnityEngine.Object)null) && !componentInChildren.isPlaying)
			{
				pointResultState = State.TO_REWARD;
			}
			break;
		}
		case State.TO_REWARD:
		{
			if (!stateInitialized)
			{
				bannerMeshRenderers = bannerCtrl.GetComponentsInChildren<MeshRenderer>();
				stateInitialized = true;
			}
			int num = 0;
			for (int i = 0; i < bannerMeshRenderers.Length; i++)
			{
				Color color = bannerMeshRenderers[i].material.color;
				color.a = Mathf.Max(0f, color.a - 5f * Time.deltaTime);
				bannerMeshRenderers[i].material.color = color;
				Color color2 = bannerMeshRenderers[i].material.color;
				if (color2.a <= 0f)
				{
					num++;
				}
				if (i == bannerMeshRenderers.Length - 1)
				{
					if (num == bannerMeshRenderers.Length)
					{
						bannerCtrl.position = GetCtrl(UI.OBJ_REWARD_POS).position;
						bannerCtrl.localScale = GetCtrl(UI.OBJ_REWARD_POS).localScale;
						StartCoroutine(WaitTiming(2f));
						pointResultState = State.REWARD;
						stateInitialized = false;
					}
					else
					{
						num = 0;
					}
				}
			}
			break;
		}
		case State.REWARD:
			if (!stateInitialized)
			{
				int num2 = 0;
				for (int j = 0; j < bannerMeshRenderers.Length; j++)
				{
					Color color3 = bannerMeshRenderers[j].material.color;
					color3.a = Mathf.Min(1f, color3.a + 5f * Time.deltaTime);
					bannerMeshRenderers[j].material.color = color3;
					Color color4 = bannerMeshRenderers[j].material.color;
					if (color4.a >= 1f)
					{
						num2++;
					}
					if (j == bannerMeshRenderers.Length - 1)
					{
						if (num2 == bannerMeshRenderers.Length)
						{
							SetActive(UI.OBJ_REWARD_ROOT, true);
							stateInitialized = true;
							SetRewardUI();
						}
						else
						{
							num2 = 0;
						}
					}
				}
			}
			break;
		case State.BONUS:
			if (!stateInitialized)
			{
				SetActive(UI.OBJ_REWARD_ROOT, false);
				SetActive(UI.OBJ_BONUS_ROOT, true);
				SetActive(UI.TXT_BANNER, false);
				SetBonusUI();
				stateInitialized = true;
			}
			break;
		case State.RANKING:
			if (!stateInitialized)
			{
				SetActive(UI.OBJ_REWARD_ROOT, false);
				SetActive(UI.OBJ_BONUS_ROOT, false);
				SetActive(UI.OBJ_RANKING_ROOT, true);
				SetActive(UI.TXT_BANNER, false);
				SetRankingUI();
				stateInitialized = true;
			}
			break;
		}
	}

	private void SetRewardUI()
	{
		SetFullScreenButton(UI.BTN_SKIP_FULL_SCREEN);
		SetActive(UI.BTN_OK, false);
		InitTween(UI.OBJ_GET_REWARD_ROOT);
		PointEventCurrentData.PointResultData data = currentData.pointRankingData;
		SetGrid(UI.GRD_POINT_DETAIL, "CarnivalResultPointDetailItem", data.bonusPoint.Count, true, delegate(int i, Transform t, bool is_recycle)
		{
			UILabel component = FindCtrl(t, UI.LBL_POINT).GetComponent<UILabel>();
			component.alpha = 1f;
			component.text = data.bonusPoint[i].point.ToString("N0");
			component.fontStyle = FontStyle.Italic;
			UILabel component2 = FindCtrl(t, UI.LBL_POINT_NAME).GetComponent<UILabel>();
			component2.alpha = 1f;
			component2.text = data.bonusPoint[i].name;
			component2.fontStyle = FontStyle.Italic;
		});
		MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti = false;
		SetLabelText(UI.LBL_GET_POINT, data.getPoint.ToString("N0"));
		SetLabelText(UI.LBL_TOTAL_POINT, (data.userPoint + data.getPoint).ToString("N0"));
		if (data.nextReward != null)
		{
			List<PointEventCurrentData.Reward> reward = data.nextReward.reward;
			SetAllRewardItem(UI.GRD_NEXT_ITEM_ROOT, reward);
			SetPoint(UI.OBJ_NEXT_REWARD, data.nextReward.point - (data.userPoint + data.getPoint));
		}
		else
		{
			SetActive(UI.STR_POINT_NEXT, false);
			SetFontStyle(GetCtrl(UI.OBJ_NEXT_REWARD), UI.LBL_POINT, FontStyle.Italic);
			SetLabelText(GetCtrl(UI.OBJ_NEXT_REWARD), UI.LBL_POINT, "なし");
		}
		List<PointEventCurrentData.Reward> list = new List<PointEventCurrentData.Reward>();
		foreach (PointEventCurrentData.PointRewardData item in data.getReward)
		{
			list.AddRange(item.reward);
		}
		SetAllRewardItem(UI.GRD_ITEM_ROOT, list);
		StartCoroutine(GetPointAnimation());
	}

	private IEnumerator GetPointAnimation()
	{
		int getPoint = currentData.pointRankingData.getPoint;
		int userPoint = currentData.pointRankingData.userPoint;
		int totalPoint = userPoint + getPoint;
		PlayAudio(AUDIO.CATEGORY);
		bool wait2 = true;
		SetLabelText(UI.LBL_GET_POINT, "0");
		PlayTween(UI.OBJ_CARNIVAL_POINT, true, delegate
		{
			((_003CGetPointAnimation_003Ec__Iterator12E)/*Error near IL_00b8: stateMachine*/)._003Cwait_003E__3 = false;
		}, true, 0);
		while (wait2)
		{
			if (skipRequest)
			{
				SkipTween(UI.OBJ_CARNIVAL_POINT, true, 0);
				wait2 = false;
			}
			yield return (object)0;
		}
		yield return (object)StartCoroutine(CountUpAnimation(0f, getPoint - boostPoint, UI.LBL_GET_POINT));
		yield return (object)StartCoroutine(CountUpAnimation((float)userPoint, totalPoint, UI.LBL_TOTAL_POINT));
		if (currentData.pointRankingData.getReward.Count > 0)
		{
			PlayAudio(AUDIO.POINTREWARD);
			wait2 = true;
			PlayTween(UI.OBJ_GET_REWARD_ROOT, true, delegate
			{
				((_003CGetPointAnimation_003Ec__Iterator12E)/*Error near IL_01d8: stateMachine*/)._003Cwait_003E__3 = false;
			}, true, 0);
			while (wait2)
			{
				if (skipRequest)
				{
					SkipTween(UI.OBJ_GET_REWARD_ROOT, true, 0);
					wait2 = false;
				}
				yield return (object)0;
			}
		}
		SetActive(UI.BTN_OK, true);
		SetActive(UI.BTN_SKIP_FULL_SCREEN, false);
	}

	private void SetNextItemIcon(List<PointEventCurrentData.Reward> reward)
	{
		SetDynamicList(UI.OBJ_NEXT_REWARD_ITEM_ICON_ROOT, "ItemIcon", reward.Count, true, null, null, delegate(int i, Transform t, bool is_recycle)
		{
			PointEventCurrentData.Reward reward2 = reward[i];
			ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward2.type, (uint)reward2.itemId, t, reward2.num, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
		});
	}

	private IEnumerator CountUpAnimation(float currentPoint, int targetPoint, UI targetUI)
	{
		while (currentPoint < (float)targetPoint)
		{
			yield return (object)0;
			if (skipRequest)
			{
				currentPoint = (float)targetPoint;
			}
			int before = Mathf.FloorToInt(currentPoint);
			float addingPoint = Mathf.Max(((float)targetPoint - currentPoint) * CountDownCube(Time.deltaTime * 4f), 1f);
			currentPoint += addingPoint;
			currentPoint = Mathf.Min(currentPoint, (float)targetPoint);
			if (before < Mathf.FloorToInt(currentPoint))
			{
				PlayAudio(AUDIO.POINTUP);
			}
			SetLabelText(targetUI, Mathf.FloorToInt(currentPoint).ToString("N0"));
		}
	}

	private IEnumerator CountDownAnimation(float currentPoint, int targetPoint, UI targetUI)
	{
		while (currentPoint > (float)targetPoint)
		{
			yield return (object)0;
			if (skipRequest)
			{
				currentPoint = (float)targetPoint;
			}
			int before = Mathf.FloorToInt(currentPoint);
			float addingPoint = Mathf.Min(((float)targetPoint - currentPoint) * CountDownCube(Time.deltaTime * 4f), -1f);
			currentPoint += addingPoint;
			currentPoint = Mathf.Max(currentPoint, (float)targetPoint);
			if (before > Mathf.FloorToInt(currentPoint))
			{
				PlayAudio(AUDIO.POINTUP);
			}
			SetLabelText(targetUI, Mathf.CeilToInt(currentPoint).ToString("N0"));
		}
	}

	private void SetAllRewardItem(UI targetGrid, List<PointEventCurrentData.Reward> rewardList)
	{
		SetGrid(targetGrid, "ItemIconReward", rewardList.Count, true, delegate(int i, Transform t, bool is_recycle)
		{
			PointEventCurrentData.Reward reward = rewardList[i];
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward.type, (uint)reward.itemId, t, reward.num, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
			if ((UnityEngine.Object)itemIcon != (UnityEngine.Object)null)
			{
				itemIcon.SetEnableCollider(false);
			}
			t.FindChild("itemNum").GetComponent<UILabel>().text = "×" + rewardList[i].num;
			if (targetGrid == UI.GRD_NEXT_ITEM_ROOT)
			{
				t.localScale = new Vector3(0.7f, 0.7f, 1f);
				if (i > 2)
				{
					itemIcon.VisibleIcon(false, true);
				}
			}
		});
	}

	private void SetPoint(UI parent, int point)
	{
		SetFontStyle(GetCtrl(parent), UI.LBL_POINT, FontStyle.Italic);
		SetLabelText(GetCtrl(parent), UI.LBL_POINT, point.ToString("N0") + "pt");
	}

	private void SetBonusUI()
	{
		InitTween(UI.OBJ_BONUS_ANIM_ROOT);
		StartCoroutine(StartBonusAnimation());
	}

	private IEnumerator StartBonusAnimation()
	{
		SoundManager.PlayOneshotJingle(40000268, null, null);
		PlayTween(UI.OBJ_BONUS_ANIM_ROOT, true, null, true, 0);
		yield return (object)StartCoroutine(WaitTiming(2.8f));
		ChangeToRankingState();
	}

	private void SetRankingUI()
	{
		SetActive(UI.BTN_SKIP_FULL_SCREEN, true);
		PointEventCurrentData.PointResultData pointRankingData = currentData.pointRankingData;
		SetLabelText(UI.LBL_RANKING_TOTAL_POINT, (pointRankingData.userPoint + pointRankingData.getPoint).ToString("N0"));
		SetLabelText(UI.LBL_PASS_NUM, Mathf.Max(0, pointRankingData.beforeRank - pointRankingData.afterRank).ToString("N0") + "人抜き");
		int num = Mathf.Min(999999, pointRankingData.beforeRank);
		int num2 = Mathf.Min(999999, pointRankingData.afterRank);
		int num3 = 0;
		num3 = ((num <= num2) ? num2.ToString().Length : num.ToString().Length);
		rankingNumbers = new List<GameObject>(6);
		Transform ctrl = GetCtrl(UI.GRD_COUNT_NUMBERS);
		for (int i = 0; i < 6; i++)
		{
			Transform transform = Utility.FindChild(ctrl, "Number" + i);
			if (!((UnityEngine.Object)transform == (UnityEngine.Object)null))
			{
				if (i >= num3)
				{
					transform.parent = null;
					UnityEngine.Object.Destroy(transform.gameObject);
				}
				else
				{
					rankingNumbers.Add(transform.gameObject);
				}
			}
		}
		ctrl.GetComponent<UIGrid>().Reposition();
		if (num > num2)
		{
			SetSpriteNumber(num);
		}
		else
		{
			SetSpriteNumber(num2);
		}
		InitTween(UI.OBJ_RANKING_ANIM_ROOT);
		StartCoroutine(StartRankingAnimation());
	}

	private IEnumerator StartRankingAnimation()
	{
		PlayAudio(AUDIO.CATEGORY);
		bool wait2 = true;
		PlayTween(UI.OBJ_RANKING_ANIM_ROOT, true, delegate
		{
			((_003CStartRankingAnimation_003Ec__Iterator132)/*Error near IL_0056: stateMachine*/)._003Cwait_003E__0 = false;
		}, true, 0);
		while (wait2)
		{
			if (skipRequest)
			{
				SkipTween(UI.OBJ_RANKING_ROOT, true, 0);
				wait2 = false;
			}
			yield return (object)0;
		}
		if (!skipRequest)
		{
			yield return (object)StartCoroutine(WaitTiming(1f));
		}
		int currentRank = currentData.pointRankingData.beforeRank;
		int targetRank = currentData.pointRankingData.afterRank;
		if (currentRank > targetRank)
		{
			yield return (object)StartCoroutine(CountSpriteAnimation((float)currentRank, targetRank));
			int passRank = currentRank - targetRank;
			Transform originPosNumber = GetCtrl(UI.SPR_POSITION_PASS);
			passPositionNumbers = new List<GameObject>();
			passPositionNumbers.Add(originPosNumber.gameObject);
			for (int i = 0; i < passRank.ToString().Length - 1; i++)
			{
				GameObject posObj = UnityEngine.Object.Instantiate(originPosNumber.gameObject);
				posObj.transform.parent = originPosNumber.parent;
				posObj.transform.localPosition = originPosNumber.localPosition;
				posObj.transform.localScale = originPosNumber.localScale;
				passPositionNumbers.Add(posObj);
			}
			SetSpritePassNumber(passRank, passPositionNumbers);
			GetCtrl(UI.GRD_POSITION_PASS).GetComponent<UIGrid>().Reposition();
			Transform passText = GetCtrl(UI.SPR_PASS_TEXT);
			Vector3 lastPassNumberPos = passPositionNumbers[passPositionNumbers.Count - 1].transform.localPosition;
			passText.GetComponent<TweenPosition>().to.x = lastPassNumberPos.x + 41f;
			InitTween(UI.OBJ_PASS_ANIM_ROOT);
			if (!skipRequest)
			{
				yield return (object)StartCoroutine(WaitTiming(0.6f));
			}
			PlayAudio(AUDIO.RESULT);
			wait2 = true;
			PlayTween(UI.OBJ_PASS_ANIM_ROOT, true, delegate
			{
				((_003CStartRankingAnimation_003Ec__Iterator132)/*Error near IL_039b: stateMachine*/)._003Cwait_003E__0 = false;
			}, true, 0);
			while (wait2)
			{
				if (skipRequest)
				{
					SkipTween(UI.OBJ_PASS_ANIM_ROOT, true, 0);
					wait2 = false;
				}
				yield return (object)0;
			}
		}
		SetActive(UI.BTN_SKIP_FULL_SCREEN, false);
		SetActive(UI.BTN_END_OK, true);
	}

	private IEnumerator CountSpriteAnimation(float currentRank, int targetRank)
	{
		while (currentRank > (float)targetRank)
		{
			yield return (object)0;
			if (skipRequest)
			{
				currentRank = (float)targetRank;
			}
			int before = Mathf.FloorToInt(currentRank);
			float addingRank = Mathf.Min(((float)targetRank - currentRank) * CountDownCube(Time.deltaTime * 4f), -1f);
			currentRank += addingRank;
			currentRank = Mathf.Max(currentRank, (float)targetRank);
			if (before > Mathf.FloorToInt(currentRank))
			{
				PlayAudio(AUDIO.POINTUP);
			}
			int showPoint = Mathf.Max((int)currentRank, 1);
			SetSpriteNumber(showPoint);
		}
	}

	private void SetSpriteNumber(int value)
	{
		string text = value.ToString();
		if (beforeDigits == 0)
		{
			beforeDigits = text.Length;
		}
		if (beforeDigits > text.Length)
		{
			UnityEngine.Object.Destroy(rankingNumbers[0].gameObject);
			rankingNumbers.RemoveAt(0);
		}
		for (int i = 0; i < text.Length; i++)
		{
			int num = int.Parse(text[i].ToString());
			if (!((UnityEngine.Object)rankingNumbers[i] == (UnityEngine.Object)null))
			{
				SetSprite(rankingNumbers[i].transform, SPR_RANKING_NUMBER[num]);
			}
		}
		beforeDigits = text.Length;
	}

	private void SetSpritePassNumber(int value, List<GameObject> numbers)
	{
		string text = value.ToString();
		for (int i = 0; i < text.Length; i++)
		{
			int num = int.Parse(text[i].ToString());
			SetSprite(numbers[i].transform, SPR_PASS_NUMBER[num]);
		}
	}

	private IEnumerator WaitTiming(float waitTime)
	{
		yield return (object)new WaitForSeconds(waitTime);
	}

	private static float CountDownCube(float currentValue)
	{
		return currentValue * (2f - currentValue);
	}

	private void PlayAudio(AUDIO type)
	{
		SoundManager.PlayOneShotUISE((int)type);
	}

	private void ChangeToRankingState()
	{
		if (currentData.pointRankingData.beforeRank < 0 || currentData.pointRankingData.afterRank < 0)
		{
			NextSection();
		}
		else
		{
			stateInitialized = false;
			skipRequest = false;
			pointResultState = State.RANKING;
		}
	}

	private void OnQuery_SKIP()
	{
		skipRequest = true;
		GameSection.StopEvent();
	}

	private void OnQuery_OK_REWARD()
	{
		if (currentData.pointRankingData.isStartedBoost)
		{
			stateInitialized = false;
			skipRequest = false;
			pointResultState = State.BONUS;
		}
		else
		{
			ChangeToRankingState();
		}
	}

	private void OnQuery_OK_END()
	{
		NextSection();
	}

	private void NextSection()
	{
		switch (resultType)
		{
		case RESULT_TYPE.NORMAL:
			GameSection.BackSection();
			break;
		case RESULT_TYPE.RUSH:
			if (MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameManager>.I.SetResultedRush();
			}
			if (MonoBehaviourSingleton<QuestManager>.I.compData == null)
			{
				GameSection.ChangeEvent("FAILED", null);
			}
			else
			{
				GameSection.ChangeEvent("NEXT", null);
			}
			break;
		case RESULT_TYPE.WAVE:
			if (MonoBehaviourSingleton<QuestManager>.I.compData == null)
			{
				GameSection.ChangeEvent("FAILED", null);
			}
			else
			{
				GameSection.ChangeEvent("NEXT", null);
			}
			break;
		default:
			GameSection.BackSection();
			break;
		}
	}
}
