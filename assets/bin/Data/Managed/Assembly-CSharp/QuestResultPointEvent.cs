using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultPointEvent : GameSection
{
	public enum UI
	{
		TXT_BANNER,
		OBJ_GET_EXP_ROOT,
		OBJ_TOTAL_EXP_ROOT,
		OBJ_NEXT_REWARD_ROOT,
		OBJ_GET_REWARD_ROOT,
		LBL_GET_POINT,
		LBL_TOTAL_POINT,
		LBL_NEXT_POINT,
		OBJ_NEXT_REWARD_ITEM_ICON_ROOT,
		GRD_ANIM_ITEM_ROOT,
		LBL_NEXT_ITEM_NUM,
		BTN_SKIP_FULL_SCREEN,
		BTN_OK,
		SPR_LOGO,
		SPR_LOGO_BG,
		OBJ_REWARD_POS,
		OBJ_REWARD_ROOT,
		OBJ_RANKING_ROOT,
		OBJ_ENEMY,
		LBL_QUEST_NAME,
		LBL_MONSTER_POINT,
		OBJ_BONUS1_ROOT,
		LBL_BONUS1_NAME,
		LBL_BONUS1_POINT,
		OBJ_BONUS2_ROOT,
		LBL_BONUS2_NAME,
		LBL_BONUS2_POINT,
		OBJ_BONUS3_ROOT,
		LBL_BONUS3_NAME,
		LBL_BONUS3_POINT,
		OBJ_LINE1,
		OBJ_LINE2,
		OBJ_LINE3,
		OBJ_LINE4,
		OBJ_ITEM_ROOT,
		OBJ_RANKING_ANIM_ROOT,
		OBJ_PASS_ANIM_ROOT,
		LBL_RANKING_TOTAL_POINT,
		LBL_PASS_NUM,
		BTN_END_OK,
		GRD_POSITION_PASS,
		SPR_POSITION_PASS,
		SPR_PASS_TEXT,
		GRD_COUNT_NUMBERS,
		OBJ_BONUS_TIME_ROOT,
		LBL_BONUS_RATE,
		OBJ_BONUS_ROOT,
		OBJ_BONUS_ANIM_ROOT,
		OBJ_POINT_DETAIL
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

	private State pointResultState;

	private bool stateInitialized;

	private PointEventCurrentData currentData;

	private const string BOOST_BONUS_NAME = "ブ\u30fcストポイント";

	private int boostPoint;

	private float boostRate;

	private UI[] bonusNames = new UI[3]
	{
		UI.LBL_BONUS1_NAME,
		UI.LBL_BONUS2_NAME,
		UI.LBL_BONUS3_NAME
	};

	private UI[] bonusPoints = new UI[3]
	{
		UI.LBL_BONUS1_POINT,
		UI.LBL_BONUS2_POINT,
		UI.LBL_BONUS3_POINT
	};

	private UI[] bonusRoots = new UI[3]
	{
		UI.OBJ_BONUS1_ROOT,
		UI.OBJ_BONUS2_ROOT,
		UI.OBJ_BONUS3_ROOT
	};

	private UI[] bonusLines = new UI[3]
	{
		UI.OBJ_LINE2,
		UI.OBJ_LINE3,
		UI.OBJ_LINE4
	};

	private const string TITLE_LOGO_NAME_FORMAT = "ef_ui_pointresult";

	private const float COUNT_ANIM_SPEED = 4f;

	private List<PointEventCurrentData.Reward> rewardList = new List<PointEventCurrentData.Reward>();

	private List<GameObject> rewardObjects = new List<GameObject>();

	private bool skipRequest;

	private Transform bannerCtrl;

	private MeshRenderer[] bannerMeshRenderers;

	private const float FADE_SPEED = 5f;

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
		currentData = (GameSection.GetEventData() as PointEventCurrentData);
		SetActive((Enum)UI.OBJ_REWARD_ROOT, is_visible: false);
		SetActive((Enum)UI.OBJ_RANKING_ROOT, is_visible: false);
		SetActive((Enum)UI.OBJ_BONUS_ROOT, is_visible: false);
		bannerCtrl = GetCtrl(UI.TXT_BANNER);
		this.StartCoroutine(DoInitalize());
	}

	private IEnumerator DoInitalize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_pointresult");
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		SetVisibleWidgetEffect(UI.TXT_BANNER, "ef_ui_pointresult");
		UIVisibleWidgetEffect visibleWidget = bannerCtrl.GetComponent<UIVisibleWidgetEffect>();
		visibleWidget.SetRendererQueue(4000);
		PlayAudio(AUDIO.TITLE_LOGO);
		int eventId = -1;
		if (currentData != null)
		{
			eventId = currentData.eventId;
			string questEventBannerResult = ResourceName.GetQuestEventBannerResult(eventId);
			if (MonoBehaviourSingleton<ResourceManager>.I.event_manifest == null)
			{
				eventId = 10017100;
			}
			else
			{
				Hash128 assetBundleHash = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.EVENT_BANNER_RESULT.ToAssetBundleName(questEventBannerResult));
				if (!assetBundleHash.get_isValid())
				{
					eventId = 10017100;
				}
			}
		}
		ResourceLoad.LoadEventBannerResultTexture(GetCtrl(UI.SPR_LOGO).GetComponent<UITexture>(), (uint)eventId);
		ResourceLoad.LoadEventBannerResultBGTexture(GetCtrl(UI.SPR_LOGO_BG).GetComponent<UITexture>(), (uint)eventId);
		TweenAlpha logoBGAlpha = GetCtrl(UI.SPR_LOGO_BG).GetComponent<TweenAlpha>();
		if (logoBGAlpha != null)
		{
			logoBGAlpha.ResetToBeginning();
			logoBGAlpha.PlayForward();
		}
		base.Initialize();
	}

	private void Update()
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
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
						SetActive((Enum)UI.SPR_LOGO_BG, is_visible: false);
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
		{
			if (stateInitialized)
			{
				break;
			}
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
						SetActive((Enum)UI.OBJ_REWARD_ROOT, is_visible: true);
						stateInitialized = true;
						SetRewardUI();
					}
					else
					{
						num2 = 0;
					}
				}
			}
			break;
		}
		case State.BONUS:
			if (!stateInitialized)
			{
				SetActive((Enum)UI.OBJ_REWARD_ROOT, is_visible: false);
				SetActive((Enum)UI.OBJ_BONUS_ROOT, is_visible: true);
				SetActive((Enum)UI.TXT_BANNER, is_visible: false);
				SetBonusUI();
				stateInitialized = true;
			}
			break;
		case State.RANKING:
			if (!stateInitialized)
			{
				SetActive((Enum)UI.OBJ_REWARD_ROOT, is_visible: false);
				SetActive((Enum)UI.OBJ_BONUS_ROOT, is_visible: false);
				SetActive((Enum)UI.OBJ_RANKING_ROOT, is_visible: true);
				SetActive((Enum)UI.TXT_BANNER, is_visible: false);
				SetRankingUI();
				stateInitialized = true;
			}
			break;
		}
	}

	private void SetRewardUI()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		SetFullScreenButton((Enum)UI.BTN_SKIP_FULL_SCREEN);
		SetActive((Enum)UI.BTN_OK, is_visible: false);
		InitTween((Enum)UI.OBJ_GET_EXP_ROOT);
		InitTween((Enum)UI.OBJ_TOTAL_EXP_ROOT);
		InitTween((Enum)UI.OBJ_NEXT_REWARD_ROOT);
		InitTween((Enum)UI.OBJ_GET_REWARD_ROOT);
		InitTween((Enum)UI.OBJ_BONUS_TIME_ROOT);
		PointEventCurrentData.PointResultData pointRankingData = currentData.pointRankingData;
		if (MonoBehaviourSingleton<GuildRequestManager>.IsValid() && MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti)
		{
			Transform ctrl = GetCtrl(UI.OBJ_POINT_DETAIL);
			IEnumerator enumerator = ctrl.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform val = enumerator.Current;
					val.get_gameObject().SetActive(false);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		else
		{
			SetLabelText((Enum)UI.LBL_QUEST_NAME, MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestName());
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID(0));
			ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, null, GetCtrl(UI.OBJ_ENEMY));
		}
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < pointRankingData.bonusPoint.Count; i++)
		{
			num += pointRankingData.bonusPoint[i].point;
			if (!MonoBehaviourSingleton<GuildRequestManager>.IsValid() || !MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti)
			{
				if (pointRankingData.bonusPoint[i].name != "ブ\u30fcストポイント" && pointRankingData.bonusPoint[i].point > 0)
				{
					SetBonusPoint(num2, pointRankingData.bonusPoint[i].name, pointRankingData.bonusPoint[i].point);
					num2++;
				}
				else
				{
					boostPoint = pointRankingData.bonusPoint[i].point;
					boostRate = pointRankingData.bonusPoint[i].boostRate;
				}
			}
		}
		MonoBehaviourSingleton<GuildRequestManager>.I.isCompleteMulti = false;
		SetLabelText((Enum)UI.LBL_MONSTER_POINT, "+" + (pointRankingData.getPoint - num).ToString("N0"));
		SetLabelText((Enum)UI.LBL_GET_POINT, pointRankingData.getPoint.ToString("N0"));
		SetLabelText((Enum)UI.LBL_TOTAL_POINT, (pointRankingData.userPoint + pointRankingData.getPoint).ToString("N0"));
		if (pointRankingData.nextReward != null)
		{
			SetLabelText((Enum)UI.LBL_NEXT_POINT, pointRankingData.nextReward.point.ToString());
			SetLabelText((Enum)UI.LBL_NEXT_ITEM_NUM, pointRankingData.nextReward.reward[0].num.ToString("N0"));
		}
		this.StartCoroutine(GetPointAnimation());
	}

	private IEnumerator GetPointAnimation()
	{
		int getPoint = currentData.pointRankingData.getPoint;
		int userPoint = currentData.pointRankingData.userPoint;
		int totalPoint = userPoint + getPoint;
		bool wait = true;
		PlayAudio(AUDIO.CATEGORY);
		wait = true;
		SetLabelText((Enum)UI.LBL_GET_POINT, "0");
		PlayTween((Enum)UI.OBJ_GET_EXP_ROOT, forward: true, (EventDelegate.Callback)delegate
		{
			wait = false;
		}, is_input_block: true, 0);
		while (wait)
		{
			if (skipRequest)
			{
				SkipTween((Enum)UI.OBJ_GET_EXP_ROOT, forward: true, 0);
				wait = false;
			}
			yield return 0;
		}
		yield return this.StartCoroutine(CountUpAnimation(0f, getPoint - boostPoint, UI.LBL_GET_POINT));
		if (boostPoint > 0)
		{
			SetLabelText((Enum)UI.LBL_BONUS_RATE, "x" + boostRate.ToString());
			wait = true;
			PlayTween((Enum)UI.OBJ_BONUS_TIME_ROOT, forward: true, (EventDelegate.Callback)delegate
			{
				wait = false;
			}, is_input_block: true, 0);
			while (wait)
			{
				if (skipRequest)
				{
					SkipTween((Enum)UI.OBJ_BONUS_TIME_ROOT, forward: true, 0);
					wait = false;
				}
				yield return 0;
			}
			PlayAudio(AUDIO.RESULT);
			if (!skipRequest)
			{
				yield return this.StartCoroutine(WaitTiming(1.2f));
			}
			yield return this.StartCoroutine(CountUpAnimation(getPoint - boostPoint, getPoint, UI.LBL_GET_POINT));
		}
		else
		{
			SetActive((Enum)UI.OBJ_BONUS_TIME_ROOT, is_visible: false);
		}
		PlayAudio(AUDIO.CATEGORY);
		wait = true;
		SetLabelText((Enum)UI.LBL_TOTAL_POINT, userPoint.ToString("N0"));
		PlayTween((Enum)UI.OBJ_TOTAL_EXP_ROOT, forward: true, (EventDelegate.Callback)delegate
		{
			wait = false;
		}, is_input_block: true, 0);
		while (wait)
		{
			if (skipRequest)
			{
				SkipTween((Enum)UI.OBJ_TOTAL_EXP_ROOT, forward: true, 0);
				wait = false;
			}
			yield return 0;
		}
		yield return this.StartCoroutine(CountUpAnimation(userPoint, totalPoint, UI.LBL_TOTAL_POINT));
		int lastGetRewardsPoint = 0;
		if (currentData.pointRankingData.getReward.Count > 0)
		{
			for (int j = 0; j < currentData.pointRankingData.getReward.Count; j++)
			{
				PointEventCurrentData.PointRewardData currentNextData = currentData.pointRankingData.getReward[j];
				for (int k = 0; k < currentNextData.reward.Count; k++)
				{
					rewardList.Add(currentNextData.reward[k]);
				}
			}
			if (rewardList.Count > 0)
			{
				SetAllRewardItem(rewardList);
				for (int l = 0; l < rewardObjects.Count; l++)
				{
					rewardObjects[l].SetActive(false);
				}
			}
			for (int i = 0; i < currentData.pointRankingData.getReward.Count; i++)
			{
				PointEventCurrentData.PointRewardData currentNextData = currentData.pointRankingData.getReward[i];
				SetNextItemIcon(currentNextData.reward);
				wait = true;
				ResetTween((Enum)UI.OBJ_NEXT_REWARD_ROOT, 0);
				int currentPoint2 = currentNextData.point - lastGetRewardsPoint;
				SetLabelText((Enum)UI.LBL_NEXT_POINT, currentPoint2.ToString("N0"));
				PlayTween((Enum)UI.OBJ_NEXT_REWARD_ROOT, forward: true, (EventDelegate.Callback)delegate
				{
					wait = false;
				}, is_input_block: true, 0);
				if (!skipRequest)
				{
					PlayAudio(AUDIO.CATEGORY);
				}
				while (wait)
				{
					if (skipRequest)
					{
						SkipTween((Enum)UI.OBJ_NEXT_REWARD_ROOT, forward: true, 0);
						wait = false;
					}
					yield return 0;
				}
				yield return this.StartCoroutine(CountDownAnimation(currentPoint2, 0, UI.LBL_NEXT_POINT));
				if (!skipRequest)
				{
					yield return this.StartCoroutine(WaitTiming(0.5f));
				}
				lastGetRewardsPoint = currentNextData.point;
			}
		}
		if (currentData.pointRankingData.nextReward != null)
		{
			SetNextItemIcon(currentData.pointRankingData.nextReward.reward);
			wait = true;
			ResetTween((Enum)UI.OBJ_NEXT_REWARD_ROOT, 0);
			int currentPoint = currentData.pointRankingData.nextReward.point - lastGetRewardsPoint;
			SetLabelText((Enum)UI.LBL_NEXT_POINT, currentPoint.ToString("N0"));
			PlayTween((Enum)UI.OBJ_NEXT_REWARD_ROOT, forward: true, (EventDelegate.Callback)delegate
			{
				wait = false;
			}, is_input_block: true, 0);
			PlayAudio(AUDIO.CATEGORY);
			while (wait)
			{
				if (skipRequest)
				{
					SkipTween((Enum)UI.OBJ_NEXT_REWARD_ROOT, forward: true, 0);
					wait = false;
				}
				yield return 0;
			}
			yield return this.StartCoroutine(CountDownAnimation(targetPoint: currentData.pointRankingData.nextReward.point - totalPoint, currentPoint: currentPoint, targetUI: UI.LBL_NEXT_POINT));
		}
		else
		{
			SetActive((Enum)UI.OBJ_NEXT_REWARD_ROOT, is_visible: false);
		}
		if (rewardObjects.Count > 0)
		{
			wait = true;
			ResetTween((Enum)UI.OBJ_GET_REWARD_ROOT, 0);
			PlayTween((Enum)UI.OBJ_GET_REWARD_ROOT, forward: true, (EventDelegate.Callback)delegate
			{
				wait = false;
			}, is_input_block: true, 0);
			PlayAudio(AUDIO.POINTREWARD);
			while (wait)
			{
				if (skipRequest)
				{
					SkipTween((Enum)UI.OBJ_GET_REWARD_ROOT, forward: true, 0);
					wait = false;
				}
				yield return 0;
			}
			for (int m = 0; m < rewardObjects.Count; m++)
			{
				rewardObjects[m].SetActive(true);
				TweenAlpha component = rewardObjects[m].GetComponent<TweenAlpha>();
				if (!(component == null))
				{
					component.ResetToBeginning();
					component.PlayForward();
				}
			}
		}
		SetActive((Enum)UI.BTN_OK, is_visible: true);
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, is_visible: false);
	}

	private void SetNextItemIcon(List<PointEventCurrentData.Reward> reward)
	{
		SetDynamicList((Enum)UI.OBJ_NEXT_REWARD_ITEM_ICON_ROOT, "ItemIcon", reward.Count, reset: true, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
		{
			PointEventCurrentData.Reward reward2 = reward[i];
			ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward2.type, (uint)reward2.itemId, t, reward2.num);
		});
	}

	private void SetAllRewardItem(List<PointEventCurrentData.Reward> rewardList)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.OBJ_ITEM_ROOT);
		for (int j = 0; j < rewardList.Count; j++)
		{
			if (j == 0)
			{
				Transform val = Utility.FindChild(ctrl, "itemNum");
				val.GetComponent<UILabel>().text = "×" + rewardList[j].num;
				rewardObjects.Add(ctrl.get_gameObject());
				continue;
			}
			GameObject val2 = Object.Instantiate<GameObject>(ctrl.get_gameObject());
			val2.get_transform().set_parent(ctrl.get_parent());
			val2.get_transform().set_localPosition(ctrl.get_localPosition());
			val2.get_transform().set_localScale(ctrl.get_localScale());
			Transform val3 = Utility.FindChild(val2.get_transform(), "itemNum");
			val3.GetComponent<UILabel>().text = "×" + rewardList[j].num;
			rewardObjects.Add(val2);
		}
		SetDynamicList((Enum)UI.GRD_ANIM_ITEM_ROOT, "ItemIcon", rewardList.Count, reset: true, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
		{
			PointEventCurrentData.Reward reward = rewardList[i];
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward.type, (uint)reward.itemId, t.get_parent(), reward.num);
			if (itemIcon != null)
			{
				itemIcon.SetEnableCollider(is_enable: false);
			}
		});
	}

	private void SetBonusPoint(int bonusIndex, string bonusName, int point)
	{
		if (bonusIndex <= 2)
		{
			SetLabelText((Enum)bonusNames[bonusIndex], bonusName);
			SetLabelText((Enum)bonusPoints[bonusIndex], "+" + point.ToString("N0"));
			SetActive((Enum)bonusRoots[bonusIndex], is_visible: true);
			SetActive((Enum)bonusLines[bonusIndex], is_visible: true);
		}
	}

	private IEnumerator CountUpAnimation(float currentPoint, int targetPoint, UI targetUI)
	{
		while (currentPoint < (float)targetPoint)
		{
			yield return 0;
			if (skipRequest)
			{
				currentPoint = targetPoint;
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
			yield return 0;
			if (skipRequest)
			{
				currentPoint = targetPoint;
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

	private void SetBonusUI()
	{
		InitTween((Enum)UI.OBJ_BONUS_ANIM_ROOT);
		this.StartCoroutine(StartBonusAnimation());
	}

	private IEnumerator StartBonusAnimation()
	{
		SoundManager.PlayOneshotJingle(40000268);
		PlayTween((Enum)UI.OBJ_BONUS_ANIM_ROOT, forward: true, (EventDelegate.Callback)null, is_input_block: true, 0);
		yield return this.StartCoroutine(WaitTiming(2.8f));
		ChangeToRankingState();
	}

	private void SetRankingUI()
	{
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, is_visible: true);
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
		bool wait = true;
		PlayTween((Enum)UI.OBJ_RANKING_ANIM_ROOT, forward: true, (EventDelegate.Callback)delegate
		{
			wait = false;
		}, is_input_block: true, 0);
		while (wait)
		{
			if (skipRequest)
			{
				SkipTween((Enum)UI.OBJ_RANKING_ROOT, forward: true, 0);
				wait = false;
			}
			yield return 0;
		}
		if (!skipRequest)
		{
			yield return this.StartCoroutine(WaitTiming(1f));
		}
		int currentRank = currentData.pointRankingData.beforeRank;
		int targetRank = currentData.pointRankingData.afterRank;
		if (currentRank > targetRank)
		{
			yield return this.StartCoroutine(CountSpriteAnimation(currentRank, targetRank));
			int passRank = currentRank - targetRank;
			Transform ctrl = GetCtrl(UI.SPR_POSITION_PASS);
			passPositionNumbers = new List<GameObject>();
			passPositionNumbers.Add(ctrl.get_gameObject());
			for (int i = 0; i < passRank.ToString().Length - 1; i++)
			{
				GameObject val = Object.Instantiate<GameObject>(ctrl.get_gameObject());
				val.get_transform().set_parent(ctrl.get_parent());
				val.get_transform().set_localPosition(ctrl.get_localPosition());
				val.get_transform().set_localScale(ctrl.get_localScale());
				passPositionNumbers.Add(val);
			}
			SetSpritePassNumber(passRank, passPositionNumbers);
			GetCtrl(UI.GRD_POSITION_PASS).GetComponent<UIGrid>().Reposition();
			Transform ctrl2 = GetCtrl(UI.SPR_PASS_TEXT);
			Vector3 localPosition = passPositionNumbers[passPositionNumbers.Count - 1].get_transform().get_localPosition();
			ctrl2.GetComponent<TweenPosition>().to.x = localPosition.x + 41f;
			InitTween((Enum)UI.OBJ_PASS_ANIM_ROOT);
			if (!skipRequest)
			{
				yield return this.StartCoroutine(WaitTiming(0.6f));
			}
			PlayAudio(AUDIO.RESULT);
			wait = true;
			PlayTween((Enum)UI.OBJ_PASS_ANIM_ROOT, forward: true, (EventDelegate.Callback)delegate
			{
				wait = false;
			}, is_input_block: true, 0);
			while (wait)
			{
				if (skipRequest)
				{
					SkipTween((Enum)UI.OBJ_PASS_ANIM_ROOT, forward: true, 0);
					wait = false;
				}
				yield return 0;
			}
		}
		SetActive((Enum)UI.BTN_SKIP_FULL_SCREEN, is_visible: false);
		SetActive((Enum)UI.BTN_END_OK, is_visible: true);
	}

	private IEnumerator CountSpriteAnimation(float currentRank, int targetRank)
	{
		while (currentRank > (float)targetRank)
		{
			yield return 0;
			if (skipRequest)
			{
				currentRank = targetRank;
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
			GameSection.BackSection();
			return;
		}
		stateInitialized = false;
		skipRequest = false;
		pointResultState = State.RANKING;
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
		GameSection.BackSection();
	}
}
