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

	private RESULT_TYPE resultType;

	private State pointResultState;

	private bool stateInitialized;

	private PointEventCurrentData currentData;

	private const string BOOST_BONUS_NAME = "ブ\u30fcストポイント";

	private int boostPoint;

	private float boostRate;

	private const string TITLE_LOGO_NAME_FORMAT = "ef_ui_pointresult";

	private const float COUNT_ANIM_SPEED = 4f;

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
		SetPointEventData();
		SetActive((Enum)UI.OBJ_REWARD_ROOT, is_visible: false);
		SetActive((Enum)UI.OBJ_RANKING_ROOT, is_visible: false);
		SetActive((Enum)UI.OBJ_BONUS_ROOT, is_visible: false);
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
					pointEventCurrentData.eventId = pointEventCurrentData2.eventId;
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
			foreach (PointEventCurrentData.BonusPointData add_bonus in addData.pointRankingData.bonusPoint)
			{
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
			allPointEvents.pointRankingData.nextReward = addData.pointRankingData.nextReward;
			allPointEvents.pointRankingData.getReward.AddRange(addData.pointRankingData.getReward);
		}
	}

	private IEnumerator DoInitalize()
	{
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(active: true);
		yield return (object)new WaitForEndOfFrame();
		LoadingQueue loadQueue = new LoadingQueue(this);
		loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_pointresult");
		LoadingQueue load_queue = new LoadingQueue(this);
		int[] ids = (int[])Enum.GetValues(typeof(AUDIO));
		int[] array = ids;
		foreach (int se_id in array)
		{
			load_queue.CacheSE(se_id);
		}
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
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		GC.Collect();
		MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(active: false);
		SetVisibleWidgetEffect(UI.TXT_BANNER, "ef_ui_pointresult");
		UIVisibleWidgetEffect visibleWidget = bannerCtrl.GetComponent<UIVisibleWidgetEffect>();
		visibleWidget.SetRendererQueue(4000);
		PlayAudio(AUDIO.TITLE_LOGO);
		base.Initialize();
	}

	private void Update()
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
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
		SetFullScreenButton((Enum)UI.BTN_SKIP_FULL_SCREEN);
		SetActive((Enum)UI.BTN_OK, is_visible: false);
		InitTween((Enum)UI.OBJ_GET_REWARD_ROOT);
		PointEventCurrentData.PointResultData data = currentData.pointRankingData;
		SetGrid(UI.GRD_POINT_DETAIL, "CarnivalResultPointDetailItem", data.bonusPoint.Count, reset: true, delegate(int i, Transform t, bool is_recycle)
		{
			UILabel component = FindCtrl(t, UI.LBL_POINT).GetComponent<UILabel>();
			component.alpha = 1f;
			component.text = data.bonusPoint[i].point.ToString("N0");
			component.fontStyle = 2;
			UILabel component2 = FindCtrl(t, UI.LBL_POINT_NAME).GetComponent<UILabel>();
			component2.alpha = 1f;
			component2.text = data.bonusPoint[i].name;
			component2.fontStyle = 2;
		});
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
			SetActive((Enum)UI.STR_POINT_NEXT, is_visible: false);
			SetFontStyle(GetCtrl(UI.OBJ_NEXT_REWARD), UI.LBL_POINT, 2);
			SetLabelText(GetCtrl(UI.OBJ_NEXT_REWARD), UI.LBL_POINT, "None");
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
		bool wait = true;
		PlayAudio(AUDIO.CATEGORY);
		wait = true;
		SetLabelText((Enum)UI.LBL_GET_POINT, "0");
		PlayTween((Enum)UI.OBJ_CARNIVAL_POINT, forward: true, (EventDelegate.Callback)delegate
		{
			wait = false;
		}, is_input_block: true, 0);
		while (wait)
		{
			if (skipRequest)
			{
				SkipTween((Enum)UI.OBJ_CARNIVAL_POINT, forward: true, 0);
				wait = false;
			}
			yield return 0;
		}
		yield return this.StartCoroutine(CountUpAnimation(0f, getPoint - boostPoint, UI.LBL_GET_POINT));
		yield return this.StartCoroutine(CountUpAnimation(userPoint, totalPoint, UI.LBL_TOTAL_POINT));
		if (currentData.pointRankingData.getReward.Count > 0)
		{
			PlayAudio(AUDIO.POINTREWARD);
			wait = true;
			PlayTween((Enum)UI.OBJ_GET_REWARD_ROOT, forward: true, (EventDelegate.Callback)delegate
			{
				wait = false;
			}, is_input_block: true, 0);
			while (wait)
			{
				if (skipRequest)
				{
					SkipTween((Enum)UI.OBJ_GET_REWARD_ROOT, forward: true, 0);
					wait = false;
				}
				yield return 0;
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

	private void SetAllRewardItem(UI targetGrid, List<PointEventCurrentData.Reward> rewardList)
	{
		SetGrid(targetGrid, "ItemIconReward", rewardList.Count, reset: true, delegate(int i, Transform t, bool is_recycle)
		{
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			PointEventCurrentData.Reward reward = rewardList[i];
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward.type, (uint)reward.itemId, t, reward.num);
			if (itemIcon != null)
			{
				itemIcon.SetEnableCollider(is_enable: false);
			}
			t.Find("itemNum").GetComponent<UILabel>().text = "×" + rewardList[i].num;
			if (targetGrid == UI.GRD_NEXT_ITEM_ROOT)
			{
				t.set_localScale(new Vector3(0.7f, 0.7f, 1f));
				if (i > 2)
				{
					itemIcon.VisibleIcon(is_visible: false);
				}
			}
		});
	}

	private void SetPoint(UI parent, int point)
	{
		SetFontStyle(GetCtrl(parent), UI.LBL_POINT, 2);
		SetLabelText(GetCtrl(parent), UI.LBL_POINT, point.ToString("N0") + "pt");
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
			NextSection();
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
				GameSection.ChangeEvent("FAILED");
			}
			else
			{
				GameSection.ChangeEvent("NEXT");
			}
			break;
		case RESULT_TYPE.WAVE:
			if (MonoBehaviourSingleton<QuestManager>.I.compData == null)
			{
				GameSection.ChangeEvent("FAILED");
			}
			else
			{
				GameSection.ChangeEvent("NEXT");
			}
			break;
		default:
			GameSection.BackSection();
			break;
		}
	}
}
