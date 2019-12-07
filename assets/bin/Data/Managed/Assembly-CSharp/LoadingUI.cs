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
			StartCoroutine(DoUpdate());
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
		SetActive(UI.SPR_TIPS, is_visible: false);
		SetActive(UI.OBJ_ICON, is_visible: false);
		SetActive(UI.OBJ_TEXT, is_visible: false);
		SetActive(UI.SPR_DL, is_visible: false);
		SetActive(UI.LBL_SYSTEM_MESSAGE, is_visible: false);
		SetActive(UI.OBJ_TITLE, is_visible: false);
		SetActive(UI.OBJ_ANNOUNCE_TIME_BONUS, is_visible: false);
		SetActive(UI.OBJ_ANNOUNCE_ELAPSED_TIME, is_visible: false);
		SetActive(UI.SPR_DRAGON_UI, is_visible: false);
		downloadGauge = GetComponent<UISlider>(UI.SPR_DL_GAUGE);
		percentLabel = GetComponent<UILabel>(UI.LBL_PERCENT);
		percentRefLabel = GetComponent<UILabel>(UI.LBL_PERCENT_REFLECT);
		if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.NeedLoadingUIIndicatorsAnchor)
		{
			return;
		}
		DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
		Transform ctrl = GetCtrl(UI.Indicators);
		if (!(ctrl == null))
		{
			UIWidget component = ctrl.GetComponent<UIWidget>();
			UIVirtualScreen componentInChildren = GetComponentInChildren<UIVirtualScreen>();
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
		if (Input.GetKeyUp(KeyCode.Escape) && SupportEscape())
		{
			Native.applicationQuit();
		}
	}

	private bool SupportEscape()
	{
		if (!GetCtrl(UI.OBJ_CHANGE_PERMISSION_MASSAGE).gameObject.activeSelf && !GetCtrl(UI.OBJ_DELLY_MASSAGE).gameObject.activeSelf && !GetCtrl(UI.OBJ_FIRSTLOAD).gameObject.activeSelf)
		{
			return GetCtrl(UI.OBJ_WELLCOME_MASSAGE).gameObject.activeSelf;
		}
		return true;
	}

	private void TouchScreen()
	{
		int touchCount = Input.touchCount;
		for (int i = 0; i < touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			Touch(touch.fingerId, touch.phase, touch.position);
		}
	}

	private void Touch(int id, TouchPhase phase, Vector2 pos)
	{
		if (phase == TouchPhase.Began)
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
		bool flag = (flags & (UIManager.DISABLE_FACTOR.PROTOCOL | UIManager.DISABLE_FACTOR.MANUAL_NETWORK)) != 0;
		bool is_active = (flags & (UIManager.DISABLE_FACTOR.SCENE_CHANGE | UIManager.DISABLE_FACTOR.TRANSITION)) == (UIManager.DISABLE_FACTOR.SCENE_CHANGE | UIManager.DISABLE_FACTOR.TRANSITION) || (flags & (UIManager.DISABLE_FACTOR.INITIALIZE | UIManager.DISABLE_FACTOR.LOADING)) != 0;
		if (GameSceneManager.isAutoEventSkip && (flags & UIManager.DISABLE_FACTOR.AUTO_EVENT) != 0)
		{
			is_active = true;
		}
		UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.OBJ_ICON).gameObject, is_active);
		visibleConnectingByUIDisable = flag;
		UpdateConnecting();
	}

	public void ShowRushUI(bool is_show)
	{
		if (!((MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isResultedRush) & is_show))
		{
			SetSpriteAnimation(IsRush());
			UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.OBJ_TITLE).gameObject, IsRush() && is_show);
			UpdateWave();
			SetActive(UI.OBJ_ANNOUNCE_TIME_BONUS, HasRushTimeBonus());
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
			ResetTween(UI.OBJ_REMAIN_TIME);
			ResetTween(UI.OBJ_TIME_BONUS);
			Transform itemRoot = GetCtrl(UI.OBJ_TIME_BONUS_ITEM);
			QuestRushProgressData.RushTimeBonus[] bonus = MonoBehaviourSingleton<InGameProgress>.I.rushTimeBonus.ToArray();
			int plusSec = 0;
			List<Transform> t_timeBonusItem = new List<Transform>();
			SetGrid(UI.GRD_TIME_BONUS_ROOT, null, bonus.Length, reset: true, delegate(int i, Transform t, bool is_recycle)
			{
				Transform transform = t.Find("bonus");
				if (transform == null)
				{
					transform = ResourceUtility.Realizes(itemRoot.gameObject);
					transform.parent = t;
					transform.localPosition = Vector3.one;
					transform.localScale = itemRoot.localScale;
					transform.name = "bonus";
				}
				SetActive(transform, is_visible: true);
				UILabel component = FindCtrl(transform, UI.LBL_TIME_BONUS).GetComponent<UILabel>();
				component.alpha = 1f;
				component.text = string.Format(StringTable.Get(STRING_CATEGORY.RUSH_TIME_BONUS, 1u), bonus[i].bonusName, bonus[i].plusSec);
				component.fontStyle = FontStyle.Italic;
				t_timeBonusItem.Add(transform);
				ResetTween(transform);
				plusSec += bonus[i].plusSec;
			});
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.PlayTimeBonusSE();
			}
			int num = Mathf.CeilToInt(MonoBehaviourSingleton<InGameProgress>.I.remaindTime);
			SetLabelText(UI.LBL_REMAIN_TIME, InGameProgress.GetTimeToString(num));
			cnt_timeBonus = t_timeBonusItem.Count;
			PlayTween(UI.OBJ_REMAIN_TIME, forward, delegate
			{
				PlayTween(UI.OBJ_TIME_BONUS, forward: true, delegate
				{
					foreach (Transform item in t_timeBonusItem)
					{
						PlayTween(item, forward, delegate
						{
							cnt_timeBonus--;
						});
					}
				}, is_input_block: false);
			}, is_input_block: false);
			int targetPoint = num + plusSec;
			StartCoroutine(CountUpAnimation(num, targetPoint, UI.LBL_REMAIN_TIME));
		}
	}

	public void ShowArenaUI(bool isShow)
	{
		if (IsArena())
		{
			SetSpriteAnimation(is_rush: false);
			if (MonoBehaviourSingleton<InGameManager>.I.IsArenaTimeAttack())
			{
				SetActive(UI.OBJ_ANNOUNCE_ELAPSED_TIME, isShow);
				ShowArenaElapsedTime(isShow);
			}
			else
			{
				SetActive(UI.OBJ_ANNOUNCE_TIME_BONUS, HasArenaTimeBonus());
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
			ResetTween(UI.OBJ_REMAIN_TIME);
			ResetTween(UI.OBJ_TIME_BONUS);
			Transform itemRoot = GetCtrl(UI.OBJ_TIME_BONUS_ITEM);
			QuestArenaProgressData.ArenaTimeBonus[] bonus = MonoBehaviourSingleton<InGameProgress>.I.arenaTimeBonus.ToArray();
			int plusSec = 0;
			List<Transform> timeBonusItemTransList = new List<Transform>();
			SetGrid(UI.GRD_TIME_BONUS_ROOT, null, bonus.Length, reset: true, delegate(int i, Transform t, bool isRecycle)
			{
				Transform transform = t.Find("bonus");
				if (transform == null)
				{
					transform = ResourceUtility.Realizes(itemRoot.gameObject);
					transform.parent = t;
					transform.localPosition = Vector3.one;
					transform.localScale = itemRoot.localScale;
					transform.name = "bonus";
				}
				SetActive(transform, is_visible: true);
				UILabel component = FindCtrl(transform, UI.LBL_TIME_BONUS).GetComponent<UILabel>();
				component.alpha = 1f;
				component.text = string.Format(StringTable.Get(STRING_CATEGORY.RUSH_TIME_BONUS, 1u), bonus[i].bonusName, bonus[i].plusSec);
				component.fontStyle = FontStyle.Italic;
				timeBonusItemTransList.Add(transform);
				ResetTween(transform);
				plusSec += bonus[i].plusSec;
			});
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.PlayTimeBonusSE();
			}
			int num = Mathf.CeilToInt(MonoBehaviourSingleton<InGameProgress>.I.remaindTime);
			SetLabelText(UI.LBL_REMAIN_TIME, InGameProgress.GetTimeToString(num));
			cnt_timeBonus = timeBonusItemTransList.Count;
			PlayTween(UI.OBJ_REMAIN_TIME, forward, delegate
			{
				PlayTween(UI.OBJ_TIME_BONUS, forward: true, delegate
				{
					for (int j = 0; j < timeBonusItemTransList.Count; j++)
					{
						PlayTween(timeBonusItemTransList[j], forward, delegate
						{
							cnt_timeBonus--;
						});
					}
				}, is_input_block: false);
			}, is_input_block: false);
			int targetPoint = num + plusSec;
			StartCoroutine(CountUpAnimation(num, targetPoint, UI.LBL_REMAIN_TIME));
		}
	}

	private void ShowArenaElapsedTime(bool forward)
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameManager>.IsValid() && !(MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedTime() <= 0f))
		{
			ResetTween(UI.OBJ_ELAPSED_TIME);
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.PlayTimeBonusSE();
			}
			SetLabelText(UI.LBL_ELAPSED_TIME, InGameProgress.GetTimeWithMilliSecToString(MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedTime()));
			PlayTween(UI.OBJ_ELAPSED_TIME, forward, null, is_input_block: false);
		}
	}

	private IEnumerator CountUpAnimation(float currentPoint, int targetPoint, UI targetUI)
	{
		float timer = 0f;
		while (cnt_timeBonus > 0 && timer < 2f)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds(0.2f);
		while (currentPoint < (float)targetPoint)
		{
			yield return 0;
			float num = Mathf.Max(((float)targetPoint - currentPoint) * CountDownCube(Time.deltaTime * 0.5f), 1f);
			currentPoint += num;
			currentPoint = Mathf.Min(currentPoint, targetPoint);
			SetLabelText(targetUI, InGameProgress.GetTimeToString(Mathf.FloorToInt(currentPoint)));
		}
	}

	private static float CountDownCube(float currentValue)
	{
		return currentValue * (2f - currentValue);
	}

	private void SetSpriteAnimation(bool is_rush)
	{
		SetActive(UI.SPR_PAMERA, is_rush);
		SetActive(UI.SPR_DRAGON, !is_rush);
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
			GetCtrl(UI.SPR_WAVE_100).GetComponent<UISprite>().spriteName = ((currentWaveNum >= 100) ? ("Load_txt_" + str3) : "");
			GetCtrl(UI.SPR_WAVE_1000).GetComponent<UISprite>().spriteName = ((currentWaveNum >= 1000) ? ("Load_txt_" + str4) : "");
		}
	}

	private void UpdateConnecting()
	{
		bool is_active = visibleConnectingByUIDisable || visibleConnectingByKtbWebSocket;
		UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.OBJ_TEXT).gameObject, is_active);
	}

	public void ShowTips(bool is_show)
	{
		if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			is_show = false;
		}
		if (coroutineTips != null)
		{
			StopCoroutine(coroutineTips);
			coroutineTips = null;
		}
		if (is_show)
		{
			StartCoroutine(coroutineTips = DoShowTips());
		}
		else
		{
			StartCoroutine(coroutineTips = DoHideTips());
		}
	}

	private IEnumerator DoShowTips()
	{
		prevTipsIdx = 0;
		yield return LoadAndSetTips();
		UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.SPR_TIPS).gameObject, is_active: true);
		GetCtrl(UI.SPR_TIPS).GetComponent<TweenAlpha>().value = 0f;
		coroutineTips = null;
	}

	public void ResetTips()
	{
		if (coroutineTips == null && GetCtrl(UI.SPR_TIPS).gameObject.activeSelf)
		{
			StartCoroutine(coroutineTips = DoResetTips());
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
		STRING_CATEGORY category = IsRush() ? STRING_CATEGORY.RUSH_TIPS : STRING_CATEGORY.TIPS;
		int num = StringTable.GetAllInCategory(category).Length;
		new List<int>(tipsIdxList);
		int num2;
		string tips;
		do
		{
			int tipTypeFromTutorial = Utility.GetTipTypeFromTutorial();
			num2 = ((tipTypeFromTutorial == -1) ? UnityEngine.Random.Range(1, num + 1) : tipTypeFromTutorial);
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name == "/colopl_rob")
			{
				num2 = 1;
			}
			if (tipTypeFromTutorial != -1)
			{
				num2 = tipTypeFromTutorial;
			}
			tips = StringTable.Get(category, (uint)num2);
		}
		while ((string.IsNullOrEmpty(tips) || tips.Length <= 1) && num2 != 1);
		prevTipsIdx = num2;
		LoadObject lo_image = null;
		if (!ResourceManager.internalMode)
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			ResourceManager.enableCache = false;
			lo_image = ((!IsRush()) ? loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.TIPS_IMAGE, ResourceName.GetTipsImage(num2)) : loadingQueue.Load(RESOURCE_CATEGORY.RUSH_TIPS_IMAGE, ResourceName.GetRushTipsImage(num2)));
			ResourceManager.enableCache = true;
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
		}
		if (lo_image != null && lo_image.loadedObject != null)
		{
			RemoveTexImage();
			SetTexture(UI.TEX_IMAGE, lo_image.loadedObject as Texture);
		}
		string text = string.Empty;
		string text2 = string.Empty;
		int num3 = tips.IndexOf('\n');
		if (num3 >= 0)
		{
			text = tips.Substring(0, num3);
			text2 = tips.Substring(num3 + 1);
		}
		SetLabelText(UI.LBL_TIPS_TITLE, text);
		SetLabelText(UI.LBL_TIPS_TITLE_REFLECT, text);
		SetLabelText(UI.LBL_TIPS, text2);
	}

	private IEnumerator DoHideTips()
	{
		UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.SPR_TIPS).gameObject, is_active: false);
		while (GetCtrl(UI.SPR_TIPS).gameObject.activeSelf)
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
			SetActive(UI.LBL_SYSTEM_MESSAGE, is_visible: true);
			SetLabelText(UI.LBL_SYSTEM_MESSAGE, msg);
		}
		else
		{
			SetActive(UI.LBL_SYSTEM_MESSAGE, is_visible: false);
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
		StartCoroutine(DoUpdate());
	}

	public void SetActiveDragon(bool active)
	{
		SetActive(UI.SPR_DRAGON_UI, active);
	}

	private IEnumerator DoUpdate()
	{
		bool gauge_fadein = false;
		float reverbe_time = 0.5f;
		while (polling && currentProgress != null)
		{
			yield return null;
			bool flag = _downloadGaugeVisible && currentProgress != null && currentProgress.IsVisible();
			if (currentProgress == null || currentProgress.IsCompleted())
			{
				UpdateGauge();
				flag = false;
				gauge_fadein = true;
				reverbe_time = 0f;
				polling = false;
				currentProgress = null;
			}
			if (flag)
			{
				if (!gauge_fadein)
				{
					gauge_fadein = true;
					UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.SPR_DL).gameObject, is_active: true);
				}
				UpdateGauge();
			}
			else if (reverbe_time > 0f)
			{
				UpdateGauge();
				reverbe_time -= Time.deltaTime;
			}
			else if (gauge_fadein)
			{
				gauge_fadein = false;
				UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.SPR_DL).gameObject, is_active: false);
			}
		}
		currentProgress = null;
		polling = false;
		UIUtility.SetActiveAndAlphaFade(GetCtrl(UI.SPR_DL).gameObject, is_active: false);
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
		SetActive(UI.OBJ_EMPTY_MASSAGE, is_visible: false);
		SetActive(UI.OBJ_WELLCOME_MASSAGE, is_visible: false);
		SetActive(UI.OBJ_DELLY_MASSAGE, is_visible: false);
		SetActive(UI.OBJ_CHANGE_PERMISSION_MASSAGE, is_visible: false);
		SetActive(UI.OBJ_FIRSTLOAD, is_visible: false);
	}

	public void ShowWellcomeMsg(bool isShow)
	{
		SetActive(UI.OBJ_WELLCOME_MASSAGE, isShow);
	}

	public void ShowDellyMsg(bool isShow)
	{
		SetActive(UI.OBJ_DELLY_MASSAGE, isShow);
		Transform ctrl = GetCtrl(UI.OBJ_DELLY_MASSAGE);
		SetSupportEncoding(ctrl, UI.LBL_FIRST_MASSAGE, isEnable: true);
	}

	public void ShowChangePermissionMsg(bool isShow)
	{
		SetActive(UI.OBJ_CHANGE_PERMISSION_MASSAGE, isShow);
		Transform ctrl = GetCtrl(UI.OBJ_CHANGE_PERMISSION_MASSAGE);
		SetSupportEncoding(ctrl, UI.LBL_FIRST_MASSAGE, isEnable: true);
	}

	public void ShowFirstLoad(bool isShow)
	{
		SetActive(UI.OBJ_FIRSTLOAD, isShow);
	}

	public void HideAllTextMsg()
	{
		SetActive(UI.OBJ_WELLCOME_MASSAGE, is_visible: false);
		SetActive(UI.OBJ_DELLY_MASSAGE, is_visible: false);
		SetActive(UI.OBJ_CHANGE_PERMISSION_MASSAGE, is_visible: false);
		SetActive(UI.OBJ_FIRSTLOAD, is_visible: false);
	}

	public void ShowEmptyFirstLoad(bool isShow)
	{
		SetActive(UI.OBJ_EMPTY_MASSAGE, isShow);
	}

	public void ShowTutorialMsg(string msg, string endTxt)
	{
		if (tutorialTweenCtrl == null)
		{
			tutorialTweenCtrl = GetComponent<UITweenCtrl>(UI.LBL_FIRST_TUTORIAL_MESSAGE);
		}
		SetActive(UI.LBL_FIRST_TUTORIAL_MESSAGE, is_visible: true);
		SetSupportEncoding(UI.LBL_FIRST_TUTORIAL_MESSAGE, isEnable: true);
		SetFontStyle(UI.LBL_FIRST_TUTORIAL_MESSAGE, FontStyle.Italic);
		SetLabelText(UI.LBL_FIRST_TUTORIAL_MESSAGE, msg);
		SetActive(UI.LBL_FIRST_TUTORIAL_MESSAGE_END, is_visible: true);
		SetSupportEncoding(UI.LBL_FIRST_TUTORIAL_MESSAGE_END, isEnable: true);
		SetFontStyle(UI.LBL_FIRST_TUTORIAL_MESSAGE_END, FontStyle.Italic);
		SetLabelText(UI.LBL_FIRST_TUTORIAL_MESSAGE_END, endTxt);
		tutorialTweenCtrl.Reset();
		tutorialTweenCtrl.Play();
	}

	public void HideTutorialMsg()
	{
		SetActive(UI.LBL_FIRST_TUTORIAL_MESSAGE_END, is_visible: false);
		SetActive(UI.LBL_FIRST_TUTORIAL_MESSAGE, is_visible: false);
	}

	public void ShowTutorialBg(bool isShow)
	{
		SetActive(UI.SPR_BG_TUTORIAL, isShow);
	}
}
