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
		"RankingNumber_0",
		"RankingNumber_1",
		"RankingNumber_2",
		"RankingNumber_3",
		"RankingNumber_4",
		"RankingNumber_5",
		"RankingNumber_6",
		"RankingNumber_7",
		"RankingNumber_8",
		"RankingNumber_9"
	};

	private readonly string[] SPR_PASS_NUMBER = new string[10]
	{
		"RankingNumber2_0",
		"RankingNumber2_1",
		"RankingNumber2_2",
		"RankingNumber2_3",
		"RankingNumber2_4",
		"RankingNumber2_5",
		"RankingNumber2_6",
		"RankingNumber2_7",
		"RankingNumber2_8",
		"RankingNumber2_9"
	};

	private List<GameObject> rankingNumbers;

	private List<GameObject> passPositionNumbers;

	private int beforeDigits;

	public override void Initialize()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		SetPointEventData();
		SetActive((Enum)UI.OBJ_REWARD_ROOT, false);
		SetActive((Enum)UI.OBJ_RANKING_ROOT, false);
		SetActive((Enum)UI.OBJ_BONUS_ROOT, false);
		bannerCtrl = GetCtrl(UI.TXT_BANNER);
		this.StartCoroutine(DoInitalize());
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
				Debug.LogError((object)"CarnivalResultDataが存在しません!!!");
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
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		switch (pointResultState)
		{
		case State.START:
		{
			Animation componentInChildren = bannerCtrl.GetComponentInChildren<Animation>(true);
			if (!(componentInChildren == null) && !componentInChildren.get_isPlaying())
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
				Color color = bannerMeshRenderers[i].get_material().get_color();
				color.a = Mathf.Max(0f, color.a - 5f * Time.get_deltaTime());
				bannerMeshRenderers[i].get_material().set_color(color);
				Color color2 = bannerMeshRenderers[i].get_material().get_color();
				if (color2.a <= 0f)
				{
					num++;
				}
				if (i == bannerMeshRenderers.Length - 1)
				{
					if (num == bannerMeshRenderers.Length)
					{
						bannerCtrl.set_position(GetCtrl(UI.OBJ_REWARD_POS).get_position());
						bannerCtrl.set_localScale(GetCtrl(UI.OBJ_REWARD_POS).get_localScale());
						this.StartCoroutine(WaitTiming(2f));
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
					Color color3 = bannerMeshRenderers[j].get_material().get_color();
					color3.a = Mathf.Min(1f, color3.a + 5f * Time.get_deltaTime());
					bannerMeshRenderers[j].get_material().set_color(color3);
					Color color4 = bannerMeshRenderers[j].get_material().get_color();
					if (color4.a >= 1f)
					{
						num2++;
					}
					if (j == bannerMeshRenderers.Length - 1)
					{
						if (num2 == bannerMeshRenderers.Length)
						{
							SetActive((Enum)UI.OBJ_REWARD_ROOT, true);
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
				SetActive((Enum)UI.OBJ_REWARD_ROOT, false);
				SetActive((Enum)UI.OBJ_BONUS_ROOT, true);
				SetActive((Enum)UI.TXT_BANNER, false);
				SetBonusUI();
				stateInitialized = true;
			}
			break;
		case State.RANKING:
			if (!stateInitialized)
			{
				SetActive((Enum)UI.OBJ_REWARD_ROOT, false);
				SetActive((Enum)UI.OBJ_BONUS_ROOT, false);
				SetActive((Enum)UI.OBJ_RANKING_ROOT, true);
				SetActive((Enum)UI.TXT_BANNER, false);
				SetRankingUI();
				stateInitialized = true;
			}
			break;
		}
	}

	private unsafe void SetRewardUI()
	{
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		SetFullScreenButton((Enum)UI.BTN_SKIP_FULL_SCREEN);
		SetActive((Enum)UI.BTN_OK, false);
		InitTween((Enum)UI.OBJ_GET_REWARD_ROOT);
		PointEventCurrentData.PointResultData data = currentData.pointRankingData;
		_003CSetRewardUI_003Ec__AnonStorey420 _003CSetRewardUI_003Ec__AnonStorey;
		SetGrid(UI.GRD_POINT_DETAIL, "CarnivalResultPointDetailItem", data.bonusPoint.Count, true, new Action<int, Transform, bool>((object)_003CSetRewardUI_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti = false;
		SetLabelText((Enum)UI.LBL_GET_POINT, data.getPoint.ToString("N0"));
		SetLabelText((Enum)UI.LBL_TOTAL_POINT, (data.userPoint + data.getPoint).ToString("N0"));
		if (data.nextReward != null)
		{
			List<PointEventCurrentData.Reward> reward = data.nextReward.reward;
			SetAllRewardItem(UI.GRD_NEXT_ITEM_ROOT, reward);
			SetPoint(UI.OBJ_NEXT_REWARD, data.nextReward.point - (data.userPoint + data.getPoint));
		}
		else
		{
			SetActive((Enum)UI.STR_POINT_NEXT, false);
			SetFontStyle(GetCtrl(UI.OBJ_NEXT_REWARD), UI.LBL_POINT, 2);
			SetLabelText(GetCtrl(UI.OBJ_NEXT_REWARD), UI.LBL_POINT, "なし");
		}
		List<PointEventCurrentData.Reward> list = new List<PointEventCurrentData.Reward>();
		foreach (PointEventCurrentData.PointRewardData item in data.getReward)
		{
			list.AddRange(item.reward);
		}
		SetAllRewardItem(UI.GRD_ITEM_ROOT, list);
		this.StartCoroutine(GetPointAnimation());
	}

	private IEnumerator GetPointAnimation()
	{
		int getPoint = currentData.pointRankingData.getPoint;
		int userPoint = currentData.pointRankingData.userPoint;
		int totalPoint = userPoint + getPoint;
		PlayAudio(AUDIO.CATEGORY);
		bool wait2 = true;
		SetLabelText((Enum)UI.LBL_GET_POINT, "0");
		PlayTween((Enum)UI.OBJ_CARNIVAL_POINT, true, (EventDelegate.Callback)delegate
		{
			((_003CGetPointAnimation_003Ec__Iterator136)/*Error near IL_00b8: stateMachine*/)._003Cwait_003E__3 = false;
		}, true, 0);
		while (wait2)
		{
			if (skipRequest)
			{
				SkipTween((Enum)UI.OBJ_CARNIVAL_POINT, true, 0);
				wait2 = false;
			}
			yield return (object)0;
		}
		yield return (object)this.StartCoroutine(CountUpAnimation(0f, getPoint - boostPoint, UI.LBL_GET_POINT));
		yield return (object)this.StartCoroutine(CountUpAnimation((float)userPoint, totalPoint, UI.LBL_TOTAL_POINT));
		if (currentData.pointRankingData.getReward.Count > 0)
		{
			PlayAudio(AUDIO.POINTREWARD);
			wait2 = true;
			PlayTween((Enum)UI.OBJ_GET_REWARD_ROOT, true, (EventDelegate.Callback)delegate
			{
				((_003CGetPointAnimation_003Ec__Iterator136)/*Error near IL_01d8: stateMachine*/)._003Cwait_003E__3 = false;
			}, true, 0);
			while (wait2)
			{
				if (skipRequest)
				{
					SkipTween((Enum)UI.OBJ_GET_REWARD_ROOT, true, 0);
					wait2 = false;
				}
				yield return (object)0;
			}
		}
		SetActive((Enum)UI.BTN_OK, true);
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, false);
	}

	private unsafe void SetNextItemIcon(List<PointEventCurrentData.Reward> reward)
	{
		_003CSetNextItemIcon_003Ec__AnonStorey421 _003CSetNextItemIcon_003Ec__AnonStorey;
		SetDynamicList((Enum)UI.OBJ_NEXT_REWARD_ITEM_ICON_ROOT, "ItemIcon", reward.Count, true, null, null, new Action<int, Transform, bool>((object)_003CSetNextItemIcon_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
			float addingPoint = Mathf.Max(((float)targetPoint - currentPoint) * CountDownCube(Time.get_deltaTime() * 4f), 1f);
			currentPoint += addingPoint;
			currentPoint = Mathf.Min(currentPoint, (float)targetPoint);
			if (before < Mathf.FloorToInt(currentPoint))
			{
				PlayAudio(AUDIO.POINTUP);
			}
			SetLabelText((Enum)targetUI, Mathf.FloorToInt(currentPoint).ToString("N0"));
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
			float addingPoint = Mathf.Min(((float)targetPoint - currentPoint) * CountDownCube(Time.get_deltaTime() * 4f), -1f);
			currentPoint += addingPoint;
			currentPoint = Mathf.Max(currentPoint, (float)targetPoint);
			if (before > Mathf.FloorToInt(currentPoint))
			{
				PlayAudio(AUDIO.POINTUP);
			}
			SetLabelText((Enum)targetUI, Mathf.CeilToInt(currentPoint).ToString("N0"));
		}
	}

	private unsafe void SetAllRewardItem(UI targetGrid, List<PointEventCurrentData.Reward> rewardList)
	{
		_003CSetAllRewardItem_003Ec__AnonStorey422 _003CSetAllRewardItem_003Ec__AnonStorey;
		SetGrid(targetGrid, "ItemIconReward", rewardList.Count, true, new Action<int, Transform, bool>((object)_003CSetAllRewardItem_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void SetPoint(UI parent, int point)
	{
		SetFontStyle(GetCtrl(parent), UI.LBL_POINT, 2);
		SetLabelText(GetCtrl(parent), UI.LBL_POINT, point.ToString("N0") + "pt");
	}

	private void SetBonusUI()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		InitTween((Enum)UI.OBJ_BONUS_ANIM_ROOT);
		this.StartCoroutine(StartBonusAnimation());
	}

	private IEnumerator StartBonusAnimation()
	{
		SoundManager.PlayOneshotJingle(40000268, null, null);
		PlayTween((Enum)UI.OBJ_BONUS_ANIM_ROOT, true, (EventDelegate.Callback)null, true, 0);
		yield return (object)this.StartCoroutine(WaitTiming(2.8f));
		ChangeToRankingState();
	}

	private void SetRankingUI()
	{
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, true);
		PointEventCurrentData.PointResultData pointRankingData = currentData.pointRankingData;
		SetLabelText((Enum)UI.LBL_RANKING_TOTAL_POINT, (pointRankingData.userPoint + pointRankingData.getPoint).ToString("N0"));
		SetLabelText((Enum)UI.LBL_PASS_NUM, Mathf.Max(0, pointRankingData.beforeRank - pointRankingData.afterRank).ToString("N0") + "人抜き");
		int num = Mathf.Min(999999, pointRankingData.beforeRank);
		int num2 = Mathf.Min(999999, pointRankingData.afterRank);
		int num3 = 0;
		num3 = ((num <= num2) ? num2.ToString().Length : num.ToString().Length);
		rankingNumbers = new List<GameObject>(6);
		Transform ctrl = GetCtrl(UI.GRD_COUNT_NUMBERS);
		for (int i = 0; i < 6; i++)
		{
			Transform val = Utility.FindChild(ctrl, "Number" + i);
			if (!(val == null))
			{
				if (i >= num3)
				{
					val.set_parent(null);
					Object.Destroy(val.get_gameObject());
				}
				else
				{
					rankingNumbers.Add(val.get_gameObject());
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
		InitTween((Enum)UI.OBJ_RANKING_ANIM_ROOT);
		this.StartCoroutine(StartRankingAnimation());
	}

	private IEnumerator StartRankingAnimation()
	{
		PlayAudio(AUDIO.CATEGORY);
		bool wait2 = true;
		PlayTween((Enum)UI.OBJ_RANKING_ANIM_ROOT, true, (EventDelegate.Callback)delegate
		{
			((_003CStartRankingAnimation_003Ec__Iterator13A)/*Error near IL_0056: stateMachine*/)._003Cwait_003E__0 = false;
		}, true, 0);
		while (wait2)
		{
			if (skipRequest)
			{
				SkipTween((Enum)UI.OBJ_RANKING_ROOT, true, 0);
				wait2 = false;
			}
			yield return (object)0;
		}
		if (!skipRequest)
		{
			yield return (object)this.StartCoroutine(WaitTiming(1f));
		}
		int currentRank = currentData.pointRankingData.beforeRank;
		int targetRank = currentData.pointRankingData.afterRank;
		if (currentRank > targetRank)
		{
			yield return (object)this.StartCoroutine(CountSpriteAnimation((float)currentRank, targetRank));
			int passRank = currentRank - targetRank;
			Transform originPosNumber = GetCtrl(UI.SPR_POSITION_PASS);
			passPositionNumbers = new List<GameObject>();
			passPositionNumbers.Add(originPosNumber.get_gameObject());
			for (int i = 0; i < passRank.ToString().Length - 1; i++)
			{
				GameObject posObj = Object.Instantiate<GameObject>(originPosNumber.get_gameObject());
				posObj.get_transform().set_parent(originPosNumber.get_parent());
				posObj.get_transform().set_localPosition(originPosNumber.get_localPosition());
				posObj.get_transform().set_localScale(originPosNumber.get_localScale());
				passPositionNumbers.Add(posObj);
			}
			SetSpritePassNumber(passRank, passPositionNumbers);
			GetCtrl(UI.GRD_POSITION_PASS).GetComponent<UIGrid>().Reposition();
			Transform passText = GetCtrl(UI.SPR_PASS_TEXT);
			Vector3 lastPassNumberPos = passPositionNumbers[passPositionNumbers.Count - 1].get_transform().get_localPosition();
			passText.GetComponent<TweenPosition>().to.x = lastPassNumberPos.x + 41f;
			InitTween((Enum)UI.OBJ_PASS_ANIM_ROOT);
			if (!skipRequest)
			{
				yield return (object)this.StartCoroutine(WaitTiming(0.6f));
			}
			PlayAudio(AUDIO.RESULT);
			wait2 = true;
			PlayTween((Enum)UI.OBJ_PASS_ANIM_ROOT, true, (EventDelegate.Callback)delegate
			{
				((_003CStartRankingAnimation_003Ec__Iterator13A)/*Error near IL_039b: stateMachine*/)._003Cwait_003E__0 = false;
			}, true, 0);
			while (wait2)
			{
				if (skipRequest)
				{
					SkipTween((Enum)UI.OBJ_PASS_ANIM_ROOT, true, 0);
					wait2 = false;
				}
				yield return (object)0;
			}
		}
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, false);
		SetActive((Enum)UI.BTN_END_OK, true);
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
			float addingRank = Mathf.Min(((float)targetRank - currentRank) * CountDownCube(Time.get_deltaTime() * 4f), -1f);
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
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		string text = value.ToString();
		if (beforeDigits == 0)
		{
			beforeDigits = text.Length;
		}
		if (beforeDigits > text.Length)
		{
			Object.Destroy(rankingNumbers[0].get_gameObject());
			rankingNumbers.RemoveAt(0);
		}
		for (int i = 0; i < text.Length; i++)
		{
			int num = int.Parse(text[i].ToString());
			if (!(rankingNumbers[i] == null))
			{
				SetSprite(rankingNumbers[i].get_transform(), SPR_RANKING_NUMBER[num]);
			}
		}
		beforeDigits = text.Length;
	}

	private void SetSpritePassNumber(int value, List<GameObject> numbers)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		string text = value.ToString();
		for (int i = 0; i < text.Length; i++)
		{
			int num = int.Parse(text[i].ToString());
			SetSprite(numbers[i].get_transform(), SPR_PASS_NUMBER[num]);
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
