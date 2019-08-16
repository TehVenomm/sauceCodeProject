using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUI : UIBehaviour
{
	private enum UI
	{
		SPR_TIPS,
		TEX_IMAGE,
		LBL_TIPS_TITLE,
		LBL_TIPS_TITLE_REFLECT,
		LBL_TIPS,
		OBJ_ICON,
		OBJ_TEXT,
		SPR_DL,
		SPR_DL_GAUGE,
		LBL_SYSTEM_MESSAGE,
		LBL_PERCENT,
		LBL_PERCENT_REFLECT,
		OBJ_TITLE,
		SPR_WAVE_001,
		SPR_WAVE_010,
		SPR_WAVE_100,
		SPR_WAVE_1000,
		SPR_DRAGON,
		SPR_PAMERA,
		SPR_DRAGON_UI,
		OBJ_ANNOUNCE_TIME_BONUS,
		OBJ_TIME_BONUS,
		OBJ_REMAIN_TIME,
		GRD_TIME_BONUS_ROOT,
		OBJ_TIME_BONUS_ITEM,
		LBL_TIME_BONUS,
		LBL_REMAIN_TIME,
		OBJ_ANNOUNCE_ELAPSED_TIME,
		OBJ_ELAPSED_TIME,
		LBL_ELAPSED_TIME,
		SPR_BG,
		Indicators,
		OBJ_FIRSTLOAD,
		OBJ_EMPTY_MASSAGE,
		OBJ_WELLCOME_MASSAGE,
		OBJ_DELLY_MASSAGE,
		OBJ_CHANGE_PERMISSION_MASSAGE,
		LBL_FIRST_MASSAGE,
		LBL_FIRST_TUTORIAL_MESSAGE,
		LBL_FIRST_TUTORIAL_MESSAGE_END,
		SPR_BG_TUTORIAL
	}

	private bool _downloadGaugeVisible = true;

	private UISlider downloadGauge;

	private UILabel percentLabel;

	private UILabel percentRefLabel;

	private IEnumerator coroutineTips;

	private bool polling;

	private bool visibleConnectingByUIDisable;

	private bool visibleConnectingByKtbWebSocket;

	private int prevTipsIdx;

	private List<int> tipsIdxList = new List<int>();

	private const float COUNT_ANIM_SPEED = 0.5f;

	private int cnt_timeBonus;

	private const string WAVE_SPRITE_NAME_BASE = "Load_txt_";

	private IProgress currentProgress;

	private UITweenCtrl tutorialTweenCtrl;

	public bool downloadGaugeVisible
	{
		get
		{
			return _downloadGaugeVisible;
		}
		set
		{
			_downloadGaugeVisible = value;
		}
	}

	private void OnEnable()
	{
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			ResourceManager i = MonoBehaviourSingleton<ResourceManager>.I;
			i.onAddRequest = (Action)Delegate.Combine(i.onAddRequest, new Action(OnAddLoadRequest));
		}
		if (currentProgress != null && polling)
		{
			this.StartCoroutine(DoUpdate());
		}
	}

	private void OnDisable()
	{
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			ResourceManager i = MonoBehaviourSingleton<ResourceManager>.I;
			i.onAddRequest = (Action)Delegate.Remove(i.onAddRequest, new Action(OnAddLoadRequest));
		}
		PlayerPrefs.SetInt("Tut_Weapon_Type", -1);
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.SPR_TIPS, is_visible: false);
		SetActive((Enum)UI.OBJ_ICON, is_visible: false);
		SetActive((Enum)UI.OBJ_TEXT, is_visible: false);
		SetActive((Enum)UI.SPR_DL, is_visible: false);
		SetActive((Enum)UI.LBL_SYSTEM_MESSAGE, is_visible: false);
		SetActive((Enum)UI.OBJ_TITLE, is_visible: false);
		SetActive((Enum)UI.OBJ_ANNOUNCE_TIME_BONUS, is_visible: false);
		SetActive((Enum)UI.OBJ_ANNOUNCE_ELAPSED_TIME, is_visible: false);
		SetActive((Enum)UI.SPR_DRAGON_UI, is_visible: false);
		downloadGauge = base.GetComponent<UISlider>((Enum)UI.SPR_DL_GAUGE);
		percentLabel = base.GetComponent<UILabel>((Enum)UI.LBL_PERCENT);
		percentRefLabel = base.GetComponent<UILabel>((Enum)UI.LBL_PERCENT_REFLECT);
		if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.NeedLoadingUIIndicatorsAnchor)
		{
			return;
		}
		DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
		Transform ctrl = GetCtrl(UI.Indicators);
		if (!(ctrl == null))
		{
			UIWidget component = ctrl.GetComponent<UIWidget>();
			UIVirtualScreen componentInChildren = this.GetComponentInChildren<UIVirtualScreen>();
			if (componentInChildren.ScreenWidthFull > componentInChildren.ScreenHeightFull)
			{
				component.leftAnchor.absolute = specialDeviceInfo.LoadingUIIndicatorsAnchor.left;
				component.rightAnchor.absolute = specialDeviceInfo.LoadingUIIndicatorsAnchor.right;
				component.topAnchor.absolute = specialDeviceInfo.LoadingUIIndicatorsAnchor.top;
				component.bottomAnchor.absolute = specialDeviceInfo.LoadingUIIndicatorsAnchor.bottom;
				component.UpdateAnchors();
			}
		}
	}

	private void Update()
	{
		bool flag = Protocol.strict && CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && !MonoBehaviourSingleton<KtbWebSocket>.I.IsCompleteSendAll();
		if (visibleConnectingByKtbWebSocket != flag)
		{
			visibleConnectingByKtbWebSocket = flag;
			UpdateConnecting();
		}
		TouchScreen();
		if (Input.GetKeyUp(27) && SupportEscape())
		{
			Native.applicationQuit();
		}
	}

	private bool SupportEscape()
	{
		return GetCtrl(UI.OBJ_CHANGE_PERMISSION_MASSAGE).get_gameObject().get_activeSelf() || GetCtrl(UI.OBJ_DELLY_MASSAGE).get_gameObject().get_activeSelf() || GetCtrl(UI.OBJ_FIRSTLOAD).get_gameObject().get_activeSelf() || GetCtrl(UI.OBJ_WELLCOME_MASSAGE).get_gameObject().get_activeSelf();
	}

	private void TouchScreen()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		int touchCount = Input.get_touchCount();
		for (int i = 0; i < touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			Touch(touch.get_fingerId(), touch.get_phase(), touch.get_position());
		}
	}

	private void Touch(int id, TouchPhase phase, Vector2 pos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if ((int)phase == 0)
		{
			ResetTips();
		}
	}

	public void UpdateUIDisableFactor(UIManager.DISABLE_FACTOR flags)
	{
		if (!Protocol.strict)
		{
			flags &= ~UIManager.DISABLE_FACTOR.PROTOCOL;
		}
		bool flag = (flags & (UIManager.DISABLE_FACTOR.PROTOCOL | UIManager.DISABLE_FACTOR.MANUAL_NETWORK)) != (UIManager.DISABLE_FACTOR)0;
		bool is_active = (flags & (UIManager.DISABLE_FACTOR.SCENE_CHANGE | UIManager.DISABLE_FACTOR.TRANSITION)) == (UIManager.DISABLE_FACTOR.SCENE_CHANGE | UIManager.DISABLE_FACTOR.TRANSITION) || (flags & (UIManager.DISABLE_FACTOR.INITIALIZE | UIManager.DISABLE_FACTOR.LOADING)) != (UIManager.DISABLE_FACTOR)0;
		if (GameSceneManager.isAutoEventSkip && (flags & UIManager.DISABLE_FACTOR.AUTO_EVENT) != 0)
		{
			is_active = true;
		}
		UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.OBJ_ICON).get_gameObject(), is_active);
		visibleConnectingByUIDisable = flag;
		UpdateConnecting();
	}

	public void ShowRushUI(bool is_show)
	{
		if (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.isResultedRush || !is_show)
		{
			SetSpriteAnimation(IsRush());
			UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.OBJ_TITLE).get_gameObject(), IsRush() && is_show);
			UpdateWave();
			SetActive((Enum)UI.OBJ_ANNOUNCE_TIME_BONUS, HasRushTimeBonus());
			ShowRushTimeBonus(is_show);
		}
	}

	private bool HasRushTimeBonus()
	{
		if (!IsRush())
		{
			return false;
		}
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.rushTimeBonus == null)
		{
			return false;
		}
		return MonoBehaviourSingleton<InGameProgress>.I.rushTimeBonus.Count > 0;
	}

	private void ShowRushTimeBonus(bool forward)
	{
		if (HasRushTimeBonus())
		{
			ResetTween((Enum)UI.OBJ_REMAIN_TIME, 0);
			ResetTween((Enum)UI.OBJ_TIME_BONUS, 0);
			Transform itemRoot = GetCtrl(UI.OBJ_TIME_BONUS_ITEM);
			QuestRushProgressData.RushTimeBonus[] bonus = MonoBehaviourSingleton<InGameProgress>.I.rushTimeBonus.ToArray();
			int plusSec = 0;
			List<Transform> t_timeBonusItem = new List<Transform>();
			SetGrid(UI.GRD_TIME_BONUS_ROOT, null, bonus.Length, reset: true, delegate(int i, Transform t, bool is_recycle)
			{
				//IL_0032: Unknown result type (might be due to invalid IL or missing references)
				//IL_0043: Unknown result type (might be due to invalid IL or missing references)
				Transform val = t.Find("bonus");
				if (val == null)
				{
					val = ResourceUtility.Realizes(itemRoot.get_gameObject());
					val.set_parent(t);
					val.set_localPosition(Vector3.get_one());
					val.set_localScale(itemRoot.get_localScale());
					val.set_name("bonus");
				}
				SetActive(val, is_visible: true);
				UILabel component = FindCtrl(val, UI.LBL_TIME_BONUS).GetComponent<UILabel>();
				component.alpha = 1f;
				component.text = string.Format(StringTable.Get(STRING_CATEGORY.RUSH_TIME_BONUS, 1u), bonus[i].bonusName, bonus[i].plusSec);
				component.fontStyle = 2;
				t_timeBonusItem.Add(val);
				ResetTween(val);
				plusSec += bonus[i].plusSec;
			});
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.PlayTimeBonusSE();
			}
			int num = Mathf.CeilToInt(MonoBehaviourSingleton<InGameProgress>.I.remaindTime);
			SetLabelText((Enum)UI.LBL_REMAIN_TIME, InGameProgress.GetTimeToString(num));
			cnt_timeBonus = t_timeBonusItem.Count;
			PlayTween((Enum)UI.OBJ_REMAIN_TIME, forward, (EventDelegate.Callback)delegate
			{
				PlayTween((Enum)UI.OBJ_TIME_BONUS, forward: true, (EventDelegate.Callback)delegate
				{
					foreach (Transform item in t_timeBonusItem)
					{
						PlayTween(item, forward, delegate
						{
							cnt_timeBonus--;
						});
					}
				}, is_input_block: false, 0);
			}, is_input_block: false, 0);
			int targetPoint = num + plusSec;
			this.StartCoroutine(CountUpAnimation(num, targetPoint, UI.LBL_REMAIN_TIME));
		}
	}

	public void ShowArenaUI(bool isShow)
	{
		if (IsArena())
		{
			SetSpriteAnimation(is_rush: false);
			if (MonoBehaviourSingleton<InGameManager>.I.IsArenaTimeAttack())
			{
				SetActive((Enum)UI.OBJ_ANNOUNCE_ELAPSED_TIME, isShow);
				ShowArenaElapsedTime(isShow);
			}
			else
			{
				SetActive((Enum)UI.OBJ_ANNOUNCE_TIME_BONUS, HasArenaTimeBonus());
				ShowArenaTimeBonus(isShow);
			}
		}
	}

	private bool HasArenaTimeBonus()
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return false;
		}
		if (!MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			return false;
		}
		if (!MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.arenaTimeBonus == null)
		{
			return false;
		}
		return MonoBehaviourSingleton<InGameProgress>.I.arenaTimeBonus.Count > 0;
	}

	private void ShowArenaTimeBonus(bool forward)
	{
		if (HasArenaTimeBonus())
		{
			ResetTween((Enum)UI.OBJ_REMAIN_TIME, 0);
			ResetTween((Enum)UI.OBJ_TIME_BONUS, 0);
			Transform itemRoot = GetCtrl(UI.OBJ_TIME_BONUS_ITEM);
			QuestArenaProgressData.ArenaTimeBonus[] bonus = MonoBehaviourSingleton<InGameProgress>.I.arenaTimeBonus.ToArray();
			int plusSec = 0;
			List<Transform> timeBonusItemTransList = new List<Transform>();
			SetGrid(UI.GRD_TIME_BONUS_ROOT, null, bonus.Length, reset: true, delegate(int i, Transform t, bool isRecycle)
			{
				//IL_0032: Unknown result type (might be due to invalid IL or missing references)
				//IL_0043: Unknown result type (might be due to invalid IL or missing references)
				Transform val = t.Find("bonus");
				if (val == null)
				{
					val = ResourceUtility.Realizes(itemRoot.get_gameObject());
					val.set_parent(t);
					val.set_localPosition(Vector3.get_one());
					val.set_localScale(itemRoot.get_localScale());
					val.set_name("bonus");
				}
				SetActive(val, is_visible: true);
				UILabel component = FindCtrl(val, UI.LBL_TIME_BONUS).GetComponent<UILabel>();
				component.alpha = 1f;
				component.text = string.Format(StringTable.Get(STRING_CATEGORY.RUSH_TIME_BONUS, 1u), bonus[i].bonusName, bonus[i].plusSec);
				component.fontStyle = 2;
				timeBonusItemTransList.Add(val);
				ResetTween(val);
				plusSec += bonus[i].plusSec;
			});
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.PlayTimeBonusSE();
			}
			int num = Mathf.CeilToInt(MonoBehaviourSingleton<InGameProgress>.I.remaindTime);
			SetLabelText((Enum)UI.LBL_REMAIN_TIME, InGameProgress.GetTimeToString(num));
			cnt_timeBonus = timeBonusItemTransList.Count;
			PlayTween((Enum)UI.OBJ_REMAIN_TIME, forward, (EventDelegate.Callback)delegate
			{
				PlayTween((Enum)UI.OBJ_TIME_BONUS, forward: true, (EventDelegate.Callback)delegate
				{
					for (int j = 0; j < timeBonusItemTransList.Count; j++)
					{
						PlayTween(timeBonusItemTransList[j], forward, delegate
						{
							cnt_timeBonus--;
						});
					}
				}, is_input_block: false, 0);
			}, is_input_block: false, 0);
			int targetPoint = num + plusSec;
			this.StartCoroutine(CountUpAnimation(num, targetPoint, UI.LBL_REMAIN_TIME));
		}
	}

	private void ShowArenaElapsedTime(bool forward)
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameManager>.IsValid() && !(MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedTime() <= 0f))
		{
			ResetTween((Enum)UI.OBJ_ELAPSED_TIME, 0);
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.PlayTimeBonusSE();
			}
			SetLabelText((Enum)UI.LBL_ELAPSED_TIME, InGameProgress.GetTimeWithMilliSecToString(MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedTime()));
			PlayTween((Enum)UI.OBJ_ELAPSED_TIME, forward, (EventDelegate.Callback)null, is_input_block: false, 0);
		}
	}

	private IEnumerator CountUpAnimation(float currentPoint, int targetPoint, UI targetUI)
	{
		float timer = 0f;
		while (cnt_timeBonus > 0 && timer < 2f)
		{
			timer += Time.get_deltaTime();
			yield return null;
		}
		yield return (object)new WaitForSeconds(0.2f);
		while (currentPoint < (float)targetPoint)
		{
			yield return 0;
			float addingPoint = Mathf.Max(((float)targetPoint - currentPoint) * CountDownCube(Time.get_deltaTime() * 0.5f), 1f);
			currentPoint += addingPoint;
			currentPoint = Mathf.Min(currentPoint, (float)targetPoint);
			SetLabelText((Enum)targetUI, InGameProgress.GetTimeToString(Mathf.FloorToInt(currentPoint)));
		}
	}

	private static float CountDownCube(float currentValue)
	{
		return currentValue * (2f - currentValue);
	}

	private void SetSpriteAnimation(bool is_rush)
	{
		SetActive((Enum)UI.SPR_PAMERA, is_rush);
		SetActive((Enum)UI.SPR_DRAGON, !is_rush);
	}

	private bool IsRush()
	{
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			return MonoBehaviourSingleton<InGameManager>.I.IsRush();
		}
		return false;
	}

	private bool IsArena()
	{
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			return MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo();
		}
		return false;
	}

	private void UpdateWave()
	{
		if (IsRush())
		{
			int currentWaveNum = MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum();
			string text = currentWaveNum.ToString("D4");
			string str = text[3].ToString();
			string str2 = text[2].ToString();
			string str3 = text[1].ToString();
			string str4 = text[0].ToString();
			GetCtrl(UI.SPR_WAVE_001).GetComponent<UISprite>().spriteName = "Load_txt_" + str;
			GetCtrl(UI.SPR_WAVE_010).GetComponent<UISprite>().spriteName = "Load_txt_" + str2;
			GetCtrl(UI.SPR_WAVE_100).GetComponent<UISprite>().spriteName = ((currentWaveNum < 100) ? string.Empty : ("Load_txt_" + str3));
			GetCtrl(UI.SPR_WAVE_1000).GetComponent<UISprite>().spriteName = ((currentWaveNum < 1000) ? string.Empty : ("Load_txt_" + str4));
		}
	}

	private void UpdateConnecting()
	{
		bool is_active = visibleConnectingByUIDisable || visibleConnectingByKtbWebSocket;
		UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.OBJ_TEXT).get_gameObject(), is_active);
	}

	public void ShowTips(bool is_show)
	{
		if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			is_show = false;
		}
		if (coroutineTips != null)
		{
			this.StopCoroutine(coroutineTips);
			coroutineTips = null;
		}
		if (is_show)
		{
			this.StartCoroutine(coroutineTips = DoShowTips());
		}
		else
		{
			this.StartCoroutine(coroutineTips = DoHideTips());
		}
	}

	private IEnumerator DoShowTips()
	{
		prevTipsIdx = 0;
		yield return LoadAndSetTips();
		UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.SPR_TIPS).get_gameObject(), is_active: true);
		GetCtrl(UI.SPR_TIPS).GetComponent<TweenAlpha>().value = 0f;
		coroutineTips = null;
	}

	public void ResetTips()
	{
		if (coroutineTips == null && GetCtrl(UI.SPR_TIPS).get_gameObject().get_activeSelf())
		{
			this.StartCoroutine(coroutineTips = DoResetTips());
		}
	}

	private IEnumerator DoResetTips()
	{
		yield return LoadAndSetTips();
		coroutineTips = null;
	}

	public void SetShowTipsList(uint quest_id)
	{
		tipsIdxList = Singleton<DeliveryTable>.I.GetTipsList(quest_id);
	}

	private void ResetShowTipsList()
	{
		tipsIdxList.Clear();
	}

	private IEnumerator LoadAndSetTips()
	{
		STRING_CATEGORY category = (!IsRush()) ? STRING_CATEGORY.TIPS : STRING_CATEGORY.RUSH_TIPS;
		int num = StringTable.GetAllInCategory(category).Length;
		new List<int>(tipsIdxList);
		int tips_index;
		string tips;
		do
		{
			int tipTypeFromTutorial = Utility.GetTipTypeFromTutorial();
			tips_index = ((tipTypeFromTutorial == -1) ? Random.Range(1, num + 1) : tipTypeFromTutorial);
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name == "/colopl_rob")
			{
				tips_index = 1;
			}
			if (tipTypeFromTutorial != -1)
			{
				tips_index = tipTypeFromTutorial;
			}
			tips = StringTable.Get(category, (uint)tips_index);
		}
		while ((string.IsNullOrEmpty(tips) || tips.Length <= 1) && tips_index != 1);
		prevTipsIdx = tips_index;
		LoadObject lo_image = null;
		if (!ResourceManager.internalMode)
		{
			LoadingQueue load_queue = new LoadingQueue(this);
			ResourceManager.enableCache = false;
			lo_image = ((!IsRush()) ? load_queue.Load(isEventAsset: true, RESOURCE_CATEGORY.TIPS_IMAGE, ResourceName.GetTipsImage(tips_index)) : load_queue.Load(RESOURCE_CATEGORY.RUSH_TIPS_IMAGE, ResourceName.GetRushTipsImage(tips_index)));
			ResourceManager.enableCache = true;
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
		}
		if (lo_image != null && lo_image.loadedObject != null)
		{
			RemoveTexImage();
			SetTexture((Enum)UI.TEX_IMAGE, lo_image.loadedObject as Texture);
		}
		string text = string.Empty;
		string text2 = string.Empty;
		int num2 = tips.IndexOf('\n');
		if (num2 >= 0)
		{
			text = tips.Substring(0, num2);
			text2 = tips.Substring(num2 + 1);
		}
		SetLabelText((Enum)UI.LBL_TIPS_TITLE, text);
		SetLabelText((Enum)UI.LBL_TIPS_TITLE_REFLECT, text);
		SetLabelText((Enum)UI.LBL_TIPS, text2);
	}

	private IEnumerator DoHideTips()
	{
		UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.SPR_TIPS).get_gameObject(), is_active: false);
		while (GetCtrl(UI.SPR_TIPS).get_gameObject().get_activeSelf())
		{
			yield return null;
		}
		RemoveTexImage();
		ResetShowTipsList();
		coroutineTips = null;
	}

	private void RemoveTexImage()
	{
		UITexture component = GetCtrl(UI.TEX_IMAGE).GetComponent<UITexture>();
		Texture mainTexture = component.mainTexture;
		component.mainTexture = null;
		if (mainTexture != null)
		{
			Resources.UnloadAsset(mainTexture);
			mainTexture = null;
		}
	}

	public void ShowSystemMessage(string msg)
	{
		if (!string.IsNullOrEmpty(msg))
		{
			SetActive((Enum)UI.LBL_SYSTEM_MESSAGE, is_visible: true);
			SetLabelText((Enum)UI.LBL_SYSTEM_MESSAGE, msg);
		}
		else
		{
			SetActive((Enum)UI.LBL_SYSTEM_MESSAGE, is_visible: false);
		}
	}

	private void OnAddLoadRequest()
	{
		if (!polling && _downloadGaugeVisible && ResourceManager.isDownloadAssets && currentProgress == null)
		{
			SetProgress(new ResourceManagerProgress());
		}
	}

	public void SetProgress(IProgress progress)
	{
		currentProgress = progress;
		polling = true;
		this.StartCoroutine(DoUpdate());
	}

	public void SetActiveDragon(bool active)
	{
		SetActive((Enum)UI.SPR_DRAGON_UI, active);
	}

	private IEnumerator DoUpdate()
	{
		bool gauge_fadein = false;
		float reverbe_time = 0.5f;
		while (polling && currentProgress != null)
		{
			yield return null;
			bool gauge_visible = _downloadGaugeVisible && currentProgress != null && currentProgress.IsVisible();
			if (currentProgress == null || currentProgress.IsCompleted())
			{
				UpdateGauge();
				gauge_visible = false;
				gauge_fadein = true;
				reverbe_time = 0f;
				polling = false;
				currentProgress = null;
			}
			if (gauge_visible)
			{
				if (!gauge_fadein)
				{
					gauge_fadein = true;
					UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.SPR_DL).get_gameObject(), is_active: true);
				}
				UpdateGauge();
			}
			else if (reverbe_time > 0f)
			{
				UpdateGauge();
				reverbe_time -= Time.get_deltaTime();
			}
			else if (gauge_fadein)
			{
				gauge_fadein = false;
				UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.SPR_DL).get_gameObject(), is_active: false);
			}
		}
		currentProgress = null;
		polling = false;
		UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.SPR_DL).get_gameObject(), is_active: false);
	}

	private void UpdateGauge()
	{
		if (currentProgress != null)
		{
			float progress = currentProgress.GetProgress();
			downloadGauge.value = Mathf.Clamp01(progress);
			if (percentLabel != null)
			{
				int num = Mathf.Clamp((int)(progress * 100f + 1E-05f), 0, 100);
				string text = string.Format("{0,3}%", num);
				percentLabel.text = text;
				percentRefLabel.text = text;
			}
		}
	}

	public void HideAllPermissionMsg()
	{
		SetActive((Enum)UI.OBJ_EMPTY_MASSAGE, is_visible: false);
		SetActive((Enum)UI.OBJ_WELLCOME_MASSAGE, is_visible: false);
		SetActive((Enum)UI.OBJ_DELLY_MASSAGE, is_visible: false);
		SetActive((Enum)UI.OBJ_CHANGE_PERMISSION_MASSAGE, is_visible: false);
		SetActive((Enum)UI.OBJ_FIRSTLOAD, is_visible: false);
	}

	public void ShowWellcomeMsg(bool isShow)
	{
		SetActive((Enum)UI.OBJ_WELLCOME_MASSAGE, isShow);
	}

	public void ShowDellyMsg(bool isShow)
	{
		SetActive((Enum)UI.OBJ_DELLY_MASSAGE, isShow);
		Transform ctrl = GetCtrl(UI.OBJ_DELLY_MASSAGE);
		SetSupportEncoding(ctrl, UI.LBL_FIRST_MASSAGE, isEnable: true);
	}

	public void ShowChangePermissionMsg(bool isShow)
	{
		SetActive((Enum)UI.OBJ_CHANGE_PERMISSION_MASSAGE, isShow);
		Transform ctrl = GetCtrl(UI.OBJ_CHANGE_PERMISSION_MASSAGE);
		SetSupportEncoding(ctrl, UI.LBL_FIRST_MASSAGE, isEnable: true);
	}

	public void ShowFirstLoad(bool isShow)
	{
		SetActive((Enum)UI.OBJ_FIRSTLOAD, isShow);
	}

	public void HideAllTextMsg()
	{
		SetActive((Enum)UI.OBJ_WELLCOME_MASSAGE, is_visible: false);
		SetActive((Enum)UI.OBJ_DELLY_MASSAGE, is_visible: false);
		SetActive((Enum)UI.OBJ_CHANGE_PERMISSION_MASSAGE, is_visible: false);
		SetActive((Enum)UI.OBJ_FIRSTLOAD, is_visible: false);
	}

	public void ShowEmptyFirstLoad(bool isShow)
	{
		SetActive((Enum)UI.OBJ_EMPTY_MASSAGE, isShow);
	}

	public void ShowTutorialMsg(string msg, string endTxt)
	{
		if (tutorialTweenCtrl == null)
		{
			tutorialTweenCtrl = base.GetComponent<UITweenCtrl>((Enum)UI.LBL_FIRST_TUTORIAL_MESSAGE);
		}
		SetActive((Enum)UI.LBL_FIRST_TUTORIAL_MESSAGE, is_visible: true);
		SetSupportEncoding(UI.LBL_FIRST_TUTORIAL_MESSAGE, isEnable: true);
		SetFontStyle((Enum)UI.LBL_FIRST_TUTORIAL_MESSAGE, 2);
		SetLabelText((Enum)UI.LBL_FIRST_TUTORIAL_MESSAGE, msg);
		SetActive((Enum)UI.LBL_FIRST_TUTORIAL_MESSAGE_END, is_visible: true);
		SetSupportEncoding(UI.LBL_FIRST_TUTORIAL_MESSAGE_END, isEnable: true);
		SetFontStyle((Enum)UI.LBL_FIRST_TUTORIAL_MESSAGE_END, 2);
		SetLabelText((Enum)UI.LBL_FIRST_TUTORIAL_MESSAGE_END, endTxt);
		tutorialTweenCtrl.Reset();
		tutorialTweenCtrl.Play();
	}

	public void HideTutorialMsg()
	{
		SetActive((Enum)UI.LBL_FIRST_TUTORIAL_MESSAGE_END, is_visible: false);
		SetActive((Enum)UI.LBL_FIRST_TUTORIAL_MESSAGE, is_visible: false);
	}

	public void ShowTutorialBg(bool isShow)
	{
		SetActive((Enum)UI.SPR_BG_TUTORIAL, isShow);
	}
}
