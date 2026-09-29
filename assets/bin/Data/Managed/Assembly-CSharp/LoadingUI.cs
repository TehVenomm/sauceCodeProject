// Decompiled with JetBrains decompiler
// Type: LoadingUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class LoadingUI : UIBehaviour
{
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
    set => this._downloadGaugeVisible = value;
    get => this._downloadGaugeVisible;
  }

  private void OnEnable()
  {
    if (MonoBehaviourSingleton<ResourceManager>.IsValid())
      MonoBehaviourSingleton<ResourceManager>.I.onAddRequest += new System.Action(this.OnAddLoadRequest);
    if (this.currentProgress == null || !this.polling)
      return;
    this.StartCoroutine(this.DoUpdate());
  }

  private void OnDisable()
  {
    if (MonoBehaviourSingleton<ResourceManager>.IsValid())
      MonoBehaviourSingleton<ResourceManager>.I.onAddRequest -= new System.Action(this.OnAddLoadRequest);
    PlayerPrefs.SetInt("Tut_Weapon_Type", -1);
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) LoadingUI.UI.SPR_TIPS, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_ICON, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_TEXT, false);
    this.SetActive((Enum) LoadingUI.UI.SPR_DL, false);
    this.SetActive((Enum) LoadingUI.UI.LBL_SYSTEM_MESSAGE, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_TITLE, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_ANNOUNCE_TIME_BONUS, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_ANNOUNCE_ELAPSED_TIME, false);
    this.SetActive((Enum) LoadingUI.UI.SPR_DRAGON_UI, false);
    this.downloadGauge = this.GetComponent<UISlider>((Enum) LoadingUI.UI.SPR_DL_GAUGE);
    this.percentLabel = this.GetComponent<UILabel>((Enum) LoadingUI.UI.LBL_PERCENT);
    this.percentRefLabel = this.GetComponent<UILabel>((Enum) LoadingUI.UI.LBL_PERCENT_REFLECT);
    if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.NeedLoadingUIIndicatorsAnchor)
      return;
    DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
    Transform ctrl = this.GetCtrl((Enum) LoadingUI.UI.Indicators);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    UIWidget component = ((Component) ctrl).GetComponent<UIWidget>();
    UIVirtualScreen componentInChildren = ((Component) this).GetComponentInChildren<UIVirtualScreen>();
    if ((double) componentInChildren.ScreenWidthFull <= (double) componentInChildren.ScreenHeightFull)
      return;
    component.leftAnchor.absolute = specialDeviceInfo.LoadingUIIndicatorsAnchor.left;
    component.rightAnchor.absolute = specialDeviceInfo.LoadingUIIndicatorsAnchor.right;
    component.topAnchor.absolute = specialDeviceInfo.LoadingUIIndicatorsAnchor.top;
    component.bottomAnchor.absolute = specialDeviceInfo.LoadingUIIndicatorsAnchor.bottom;
    component.UpdateAnchors();
  }

  private void Update()
  {
    bool flag = Protocol.strict && CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected() && !MonoBehaviourSingleton<KtbWebSocket>.I.IsCompleteSendAll();
    if (this.visibleConnectingByKtbWebSocket != flag)
    {
      this.visibleConnectingByKtbWebSocket = flag;
      this.UpdateConnecting();
    }
    this.TouchScreen();
    if (!Input.GetKeyUp((KeyCode) 27) || !this.SupportEscape())
      return;
    Native.applicationQuit();
  }

  private bool SupportEscape()
  {
    return ((Component) this.GetCtrl((Enum) LoadingUI.UI.OBJ_CHANGE_PERMISSION_MASSAGE)).gameObject.activeSelf || ((Component) this.GetCtrl((Enum) LoadingUI.UI.OBJ_DELLY_MASSAGE)).gameObject.activeSelf || ((Component) this.GetCtrl((Enum) LoadingUI.UI.OBJ_FIRSTLOAD)).gameObject.activeSelf || ((Component) this.GetCtrl((Enum) LoadingUI.UI.OBJ_WELLCOME_MASSAGE)).gameObject.activeSelf;
  }

  private void TouchScreen()
  {
    int touchCount = Input.touchCount;
    for (int index = 0; index < touchCount; ++index)
    {
      UnityEngine.Touch touch = Input.GetTouch(index);
      this.Touch(((UnityEngine.Touch) ref touch).fingerId, ((UnityEngine.Touch) ref touch).phase, ((UnityEngine.Touch) ref touch).position);
    }
  }

  private void Touch(int id, TouchPhase phase, Vector2 pos)
  {
    if (phase != null)
      return;
    this.ResetTips();
  }

  public void UpdateUIDisableFactor(UIManager.DISABLE_FACTOR flags)
  {
    if (!Protocol.strict)
      flags &= ~UIManager.DISABLE_FACTOR.PROTOCOL;
    bool flag = (flags & (UIManager.DISABLE_FACTOR.PROTOCOL | UIManager.DISABLE_FACTOR.MANUAL_NETWORK)) != 0;
    bool is_active = (flags & (UIManager.DISABLE_FACTOR.SCENE_CHANGE | UIManager.DISABLE_FACTOR.TRANSITION)) == (UIManager.DISABLE_FACTOR.SCENE_CHANGE | UIManager.DISABLE_FACTOR.TRANSITION) || (flags & (UIManager.DISABLE_FACTOR.INITIALIZE | UIManager.DISABLE_FACTOR.LOADING)) != 0;
    if (GameSceneManager.isAutoEventSkip && (flags & UIManager.DISABLE_FACTOR.AUTO_EVENT) != (UIManager.DISABLE_FACTOR) 0)
      is_active = true;
    UIUtility.SetActiveAndAlphaFade(((Component) this.GetCtrl((Enum) LoadingUI.UI.OBJ_ICON)).gameObject, is_active);
    this.visibleConnectingByUIDisable = flag;
    this.UpdateConnecting();
  }

  public void ShowRushUI(bool is_show)
  {
    if (((!MonoBehaviourSingleton<InGameManager>.IsValid() ? 0 : (MonoBehaviourSingleton<InGameManager>.I.isResultedRush ? 1 : 0)) & (is_show ? 1 : 0)) != 0)
      return;
    this.SetSpriteAnimation(this.IsRush());
    UIUtility.SetActiveAndAlphaFade(((Component) this.GetCtrl((Enum) LoadingUI.UI.OBJ_TITLE)).gameObject, this.IsRush() & is_show);
    this.UpdateWave();
    this.SetActive((Enum) LoadingUI.UI.OBJ_ANNOUNCE_TIME_BONUS, this.HasRushTimeBonus());
    this.ShowRushTimeBonus(is_show);
  }

  private bool HasRushTimeBonus()
  {
    return this.IsRush() && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.rushTimeBonus != null && MonoBehaviourSingleton<InGameProgress>.I.rushTimeBonus.Count > 0;
  }

  private void ShowRushTimeBonus(bool forward)
  {
    if (!this.HasRushTimeBonus())
      return;
    this.ResetTween((Enum) LoadingUI.UI.OBJ_REMAIN_TIME);
    this.ResetTween((Enum) LoadingUI.UI.OBJ_TIME_BONUS);
    Transform itemRoot = this.GetCtrl((Enum) LoadingUI.UI.OBJ_TIME_BONUS_ITEM);
    QuestRushProgressData.RushTimeBonus[] bonus = MonoBehaviourSingleton<InGameProgress>.I.rushTimeBonus.ToArray();
    int plusSec = 0;
    List<Transform> t_timeBonusItem = new List<Transform>();
    this.SetGrid((Enum) LoadingUI.UI.GRD_TIME_BONUS_ROOT, (string) null, bonus.Length, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      Transform transform = t.Find("bonus");
      if (Object.op_Equality((Object) transform, (Object) null))
      {
        transform = ResourceUtility.Realizes((Object) ((Component) itemRoot).gameObject);
        transform.parent = t;
        transform.localPosition = Vector3.one;
        transform.localScale = itemRoot.localScale;
        ((Object) transform).name = "bonus";
      }
      this.SetActive(transform, true);
      UILabel component = ((Component) this.FindCtrl(transform, (Enum) LoadingUI.UI.LBL_TIME_BONUS)).GetComponent<UILabel>();
      component.alpha = 1f;
      component.text = string.Format(StringTable.Get(STRING_CATEGORY.RUSH_TIME_BONUS, 1U), (object) bonus[i].bonusName, (object) bonus[i].plusSec);
      component.fontStyle = (FontStyle) 2;
      t_timeBonusItem.Add(transform);
      this.ResetTween(transform);
      plusSec += bonus[i].plusSec;
    }));
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      MonoBehaviourSingleton<InGameProgress>.I.PlayTimeBonusSE();
    int num = Mathf.CeilToInt(MonoBehaviourSingleton<InGameProgress>.I.remaindTime);
    this.SetLabelText((Enum) LoadingUI.UI.LBL_REMAIN_TIME, InGameProgress.GetTimeToString(num));
    this.cnt_timeBonus = t_timeBonusItem.Count;
    this.PlayTween((Enum) LoadingUI.UI.OBJ_REMAIN_TIME, forward, (EventDelegate.Callback) (() => this.PlayTween((Enum) LoadingUI.UI.OBJ_TIME_BONUS, callback: (EventDelegate.Callback) (() =>
    {
      foreach (Transform t in t_timeBonusItem)
        this.PlayTween(t, forward, (EventDelegate.Callback) (() => --this.cnt_timeBonus));
    }), is_input_block: false)), false);
    int targetPoint = num + plusSec;
    this.StartCoroutine(this.CountUpAnimation((float) num, targetPoint, LoadingUI.UI.LBL_REMAIN_TIME));
  }

  public void ShowArenaUI(bool isShow)
  {
    if (!this.IsArena())
      return;
    this.SetSpriteAnimation(false);
    if (MonoBehaviourSingleton<InGameManager>.I.IsArenaTimeAttack())
    {
      this.SetActive((Enum) LoadingUI.UI.OBJ_ANNOUNCE_ELAPSED_TIME, isShow);
      this.ShowArenaElapsedTime(isShow);
    }
    else
    {
      this.SetActive((Enum) LoadingUI.UI.OBJ_ANNOUNCE_TIME_BONUS, this.HasArenaTimeBonus());
      this.ShowArenaTimeBonus(isShow);
    }
  }

  private bool HasArenaTimeBonus()
  {
    return MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() && MonoBehaviourSingleton<InGameProgress>.I.arenaTimeBonus != null && MonoBehaviourSingleton<InGameProgress>.I.arenaTimeBonus.Count > 0;
  }

  private void ShowArenaTimeBonus(bool forward)
  {
    if (!this.HasArenaTimeBonus())
      return;
    this.ResetTween((Enum) LoadingUI.UI.OBJ_REMAIN_TIME);
    this.ResetTween((Enum) LoadingUI.UI.OBJ_TIME_BONUS);
    Transform itemRoot = this.GetCtrl((Enum) LoadingUI.UI.OBJ_TIME_BONUS_ITEM);
    QuestArenaProgressData.ArenaTimeBonus[] bonus = MonoBehaviourSingleton<InGameProgress>.I.arenaTimeBonus.ToArray();
    int plusSec = 0;
    List<Transform> timeBonusItemTransList = new List<Transform>();
    this.SetGrid((Enum) LoadingUI.UI.GRD_TIME_BONUS_ROOT, (string) null, bonus.Length, true, (Action<int, Transform, bool>) ((i, t, isRecycle) =>
    {
      Transform transform = t.Find("bonus");
      if (Object.op_Equality((Object) transform, (Object) null))
      {
        transform = ResourceUtility.Realizes((Object) ((Component) itemRoot).gameObject);
        transform.parent = t;
        transform.localPosition = Vector3.one;
        transform.localScale = itemRoot.localScale;
        ((Object) transform).name = "bonus";
      }
      this.SetActive(transform, true);
      UILabel component = ((Component) this.FindCtrl(transform, (Enum) LoadingUI.UI.LBL_TIME_BONUS)).GetComponent<UILabel>();
      component.alpha = 1f;
      component.text = string.Format(StringTable.Get(STRING_CATEGORY.RUSH_TIME_BONUS, 1U), (object) bonus[i].bonusName, (object) bonus[i].plusSec);
      component.fontStyle = (FontStyle) 2;
      timeBonusItemTransList.Add(transform);
      this.ResetTween(transform);
      plusSec += bonus[i].plusSec;
    }));
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      MonoBehaviourSingleton<InGameProgress>.I.PlayTimeBonusSE();
    int num = Mathf.CeilToInt(MonoBehaviourSingleton<InGameProgress>.I.remaindTime);
    this.SetLabelText((Enum) LoadingUI.UI.LBL_REMAIN_TIME, InGameProgress.GetTimeToString(num));
    this.cnt_timeBonus = timeBonusItemTransList.Count;
    this.PlayTween((Enum) LoadingUI.UI.OBJ_REMAIN_TIME, forward, (EventDelegate.Callback) (() => this.PlayTween((Enum) LoadingUI.UI.OBJ_TIME_BONUS, callback: (EventDelegate.Callback) (() =>
    {
      for (int index = 0; index < timeBonusItemTransList.Count; ++index)
        this.PlayTween(timeBonusItemTransList[index], forward, (EventDelegate.Callback) (() => --this.cnt_timeBonus));
    }), is_input_block: false)), false);
    int targetPoint = num + plusSec;
    this.StartCoroutine(this.CountUpAnimation((float) num, targetPoint, LoadingUI.UI.LBL_REMAIN_TIME));
  }

  private void ShowArenaElapsedTime(bool forward)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameManager>.IsValid() || (double) MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedTime() <= 0.0)
      return;
    this.ResetTween((Enum) LoadingUI.UI.OBJ_ELAPSED_TIME);
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      MonoBehaviourSingleton<InGameProgress>.I.PlayTimeBonusSE();
    this.SetLabelText((Enum) LoadingUI.UI.LBL_ELAPSED_TIME, InGameProgress.GetTimeWithMilliSecToString(MonoBehaviourSingleton<InGameProgress>.I.GetArenaElapsedTime()));
    this.PlayTween((Enum) LoadingUI.UI.OBJ_ELAPSED_TIME, forward, is_input_block: false);
  }

  private IEnumerator CountUpAnimation(float currentPoint, int targetPoint, LoadingUI.UI targetUI)
  {
    float timer = 0.0f;
    while (this.cnt_timeBonus > 0 && (double) timer < 2.0)
    {
      timer += Time.deltaTime;
      yield return (object) null;
    }
    yield return (object) new WaitForSeconds(0.2f);
    while ((double) currentPoint < (double) targetPoint)
    {
      yield return (object) 0;
      currentPoint += Mathf.Max(((float) targetPoint - currentPoint) * LoadingUI.CountDownCube(Time.deltaTime * 0.5f), 1f);
      currentPoint = Mathf.Min(currentPoint, (float) targetPoint);
      this.SetLabelText((Enum) targetUI, InGameProgress.GetTimeToString(Mathf.FloorToInt(currentPoint)));
    }
  }

  private static float CountDownCube(float currentValue) => currentValue * (2f - currentValue);

  private void SetSpriteAnimation(bool is_rush)
  {
    this.SetActive((Enum) LoadingUI.UI.SPR_PAMERA, is_rush);
    this.SetActive((Enum) LoadingUI.UI.SPR_DRAGON, !is_rush);
  }

  private bool IsRush()
  {
    return MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsRush();
  }

  private bool IsArena()
  {
    return MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo();
  }

  private void UpdateWave()
  {
    if (!this.IsRush())
      return;
    int currentWaveNum = MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum();
    string str1 = currentWaveNum.ToString("D4");
    string str2 = str1[3].ToString();
    string str3 = str1[2].ToString();
    string str4 = str1[1].ToString();
    string str5 = str1[0].ToString();
    ((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_WAVE_001)).GetComponent<UISprite>().spriteName = "Load_txt_" + str2;
    ((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_WAVE_010)).GetComponent<UISprite>().spriteName = "Load_txt_" + str3;
    ((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_WAVE_100)).GetComponent<UISprite>().spriteName = currentWaveNum >= 100 ? "Load_txt_" + str4 : "";
    ((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_WAVE_1000)).GetComponent<UISprite>().spriteName = currentWaveNum >= 1000 ? "Load_txt_" + str5 : "";
  }

  private void UpdateConnecting()
  {
    bool is_active = this.visibleConnectingByUIDisable || this.visibleConnectingByKtbWebSocket;
    UIUtility.SetActiveAndAlphaFade(((Component) this.GetCtrl((Enum) LoadingUI.UI.OBJ_TEXT)).gameObject, is_active);
  }

  public void ShowTips(bool is_show)
  {
    if (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
      is_show = false;
    if (this.coroutineTips != null)
    {
      this.StopCoroutine(this.coroutineTips);
      this.coroutineTips = (IEnumerator) null;
    }
    if (is_show)
      this.StartCoroutine(this.coroutineTips = this.DoShowTips());
    else
      this.StartCoroutine(this.coroutineTips = this.DoHideTips());
  }

  private IEnumerator DoShowTips()
  {
    this.prevTipsIdx = 0;
    yield return (object) this.LoadAndSetTips();
    UIUtility.SetActiveAndAlphaFade(((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_TIPS)).gameObject, true);
    ((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_TIPS)).GetComponent<TweenAlpha>().value = 0.0f;
    this.coroutineTips = (IEnumerator) null;
  }

  public void ResetTips()
  {
    if (this.coroutineTips != null || !((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_TIPS)).gameObject.activeSelf)
      return;
    this.StartCoroutine(this.coroutineTips = this.DoResetTips());
  }

  private IEnumerator DoResetTips()
  {
    yield return (object) this.LoadAndSetTips();
    this.coroutineTips = (IEnumerator) null;
  }

  public void SetShowTipsList(uint quest_id)
  {
    this.tipsIdxList = Singleton<DeliveryTable>.I.GetTipsList(quest_id);
  }

  private void ResetShowTipsList() => this.tipsIdxList.Clear();

  private IEnumerator LoadAndSetTips()
  {
    string tips = (string) null;
    STRING_CATEGORY category = this.IsRush() ? STRING_CATEGORY.RUSH_TIPS : STRING_CATEGORY.TIPS;
    int length1 = StringTable.GetAllInCategory(category).Length;
    List<int> intList = new List<int>((IEnumerable<int>) this.tipsIdxList);
    int id;
    do
    {
      int typeFromTutorial = Utility.GetTipTypeFromTutorial();
      id = typeFromTutorial == -1 ? Random.Range(1, length1 + 1) : typeFromTutorial;
      if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name == "/colopl_rob")
        id = 1;
      if (typeFromTutorial != -1)
        id = typeFromTutorial;
      tips = StringTable.Get(category, (uint) id);
    }
    while ((string.IsNullOrEmpty(tips) || tips.Length <= 1) && id != 1);
    this.prevTipsIdx = id;
    LoadObject lo_image = (LoadObject) null;
    if (!ResourceManager.internalMode)
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      ResourceManager.enableCache = false;
      lo_image = !this.IsRush() ? loadingQueue.Load(true, RESOURCE_CATEGORY.TIPS_IMAGE, ResourceName.GetTipsImage(id)) : loadingQueue.Load(RESOURCE_CATEGORY.RUSH_TIPS_IMAGE, ResourceName.GetRushTipsImage(id));
      ResourceManager.enableCache = true;
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
    }
    if (lo_image != null && Object.op_Inequality(lo_image.loadedObject, (Object) null))
    {
      this.RemoveTexImage();
      this.SetTexture((Enum) LoadingUI.UI.TEX_IMAGE, lo_image.loadedObject as Texture);
    }
    lo_image = (LoadObject) null;
    string text1 = string.Empty;
    string text2 = string.Empty;
    int length2 = tips.IndexOf('\n');
    if (length2 >= 0)
    {
      text1 = tips.Substring(0, length2);
      text2 = tips.Substring(length2 + 1);
    }
    this.SetLabelText((Enum) LoadingUI.UI.LBL_TIPS_TITLE, text1);
    this.SetLabelText((Enum) LoadingUI.UI.LBL_TIPS_TITLE_REFLECT, text1);
    this.SetLabelText((Enum) LoadingUI.UI.LBL_TIPS, text2);
  }

  private IEnumerator DoHideTips()
  {
    UIUtility.SetActiveAndAlphaFade(((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_TIPS)).gameObject, false);
    while (((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_TIPS)).gameObject.activeSelf)
      yield return (object) null;
    this.RemoveTexImage();
    this.ResetShowTipsList();
    this.coroutineTips = (IEnumerator) null;
  }

  private void RemoveTexImage()
  {
    UITexture component = ((Component) this.GetCtrl((Enum) LoadingUI.UI.TEX_IMAGE)).GetComponent<UITexture>();
    Texture mainTexture = component.mainTexture;
    component.mainTexture = (Texture) null;
    if (!Object.op_Inequality((Object) mainTexture, (Object) null))
      return;
    Resources.UnloadAsset((Object) mainTexture);
  }

  public void ShowSystemMessage(string msg)
  {
    if (!string.IsNullOrEmpty(msg))
    {
      this.SetActive((Enum) LoadingUI.UI.LBL_SYSTEM_MESSAGE, true);
      this.SetLabelText((Enum) LoadingUI.UI.LBL_SYSTEM_MESSAGE, msg);
    }
    else
      this.SetActive((Enum) LoadingUI.UI.LBL_SYSTEM_MESSAGE, false);
  }

  private void OnAddLoadRequest()
  {
    if (this.polling || !this._downloadGaugeVisible || !ResourceManager.isDownloadAssets || this.currentProgress != null)
      return;
    this.SetProgress((IProgress) new ResourceManagerProgress());
  }

  public void SetProgress(IProgress progress)
  {
    this.currentProgress = progress;
    this.polling = true;
    this.StartCoroutine(this.DoUpdate());
  }

  public void SetActiveDragon(bool active)
  {
    this.SetActive((Enum) LoadingUI.UI.SPR_DRAGON_UI, active);
  }

  private IEnumerator DoUpdate()
  {
    bool gauge_fadein = false;
    float reverbe_time = 0.5f;
    while (this.polling && this.currentProgress != null)
    {
      yield return (object) null;
      bool flag = this._downloadGaugeVisible && this.currentProgress != null && this.currentProgress.IsVisible();
      if (this.currentProgress == null || this.currentProgress.IsCompleted())
      {
        this.UpdateGauge();
        flag = false;
        gauge_fadein = true;
        reverbe_time = 0.0f;
        this.polling = false;
        this.currentProgress = (IProgress) null;
      }
      if (flag)
      {
        if (!gauge_fadein)
        {
          gauge_fadein = true;
          UIUtility.SetActiveAndAlphaFade(((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_DL)).gameObject, true);
        }
        this.UpdateGauge();
      }
      else if ((double) reverbe_time > 0.0)
      {
        this.UpdateGauge();
        reverbe_time -= Time.deltaTime;
      }
      else if (gauge_fadein)
      {
        gauge_fadein = false;
        UIUtility.SetActiveAndAlphaFade(((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_DL)).gameObject, false);
      }
    }
    this.currentProgress = (IProgress) null;
    this.polling = false;
    UIUtility.SetActiveAndAlphaFade(((Component) this.GetCtrl((Enum) LoadingUI.UI.SPR_DL)).gameObject, false);
  }

  private void UpdateGauge()
  {
    if (this.currentProgress == null)
      return;
    float progress = this.currentProgress.GetProgress();
    this.downloadGauge.value = Mathf.Clamp01(progress);
    if (!Object.op_Inequality((Object) this.percentLabel, (Object) null))
      return;
    string str = $"{Mathf.Clamp((int) ((double) progress * 100.0 + 9.9999997473787516E-06), 0, 100),3}%";
    this.percentLabel.text = str;
    this.percentRefLabel.text = str;
  }

  public void HideAllPermissionMsg()
  {
    this.SetActive((Enum) LoadingUI.UI.OBJ_EMPTY_MASSAGE, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_WELLCOME_MASSAGE, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_DELLY_MASSAGE, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_CHANGE_PERMISSION_MASSAGE, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_FIRSTLOAD, false);
  }

  public void ShowWellcomeMsg(bool isShow)
  {
    this.SetActive((Enum) LoadingUI.UI.OBJ_WELLCOME_MASSAGE, isShow);
  }

  public void ShowDellyMsg(bool isShow)
  {
    this.SetActive((Enum) LoadingUI.UI.OBJ_DELLY_MASSAGE, isShow);
    this.SetSupportEncoding(this.GetCtrl((Enum) LoadingUI.UI.OBJ_DELLY_MASSAGE), (Enum) LoadingUI.UI.LBL_FIRST_MASSAGE, true);
  }

  public void ShowChangePermissionMsg(bool isShow)
  {
    this.SetActive((Enum) LoadingUI.UI.OBJ_CHANGE_PERMISSION_MASSAGE, isShow);
    this.SetSupportEncoding(this.GetCtrl((Enum) LoadingUI.UI.OBJ_CHANGE_PERMISSION_MASSAGE), (Enum) LoadingUI.UI.LBL_FIRST_MASSAGE, true);
  }

  public void ShowFirstLoad(bool isShow)
  {
    this.SetActive((Enum) LoadingUI.UI.OBJ_FIRSTLOAD, isShow);
  }

  public void HideAllTextMsg()
  {
    this.SetActive((Enum) LoadingUI.UI.OBJ_WELLCOME_MASSAGE, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_DELLY_MASSAGE, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_CHANGE_PERMISSION_MASSAGE, false);
    this.SetActive((Enum) LoadingUI.UI.OBJ_FIRSTLOAD, false);
  }

  public void ShowEmptyFirstLoad(bool isShow)
  {
    this.SetActive((Enum) LoadingUI.UI.OBJ_EMPTY_MASSAGE, isShow);
  }

  public void ShowTutorialMsg(string msg, string endTxt)
  {
    if (Object.op_Equality((Object) this.tutorialTweenCtrl, (Object) null))
      this.tutorialTweenCtrl = this.GetComponent<UITweenCtrl>((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE);
    this.SetActive((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE, true);
    this.SetSupportEncoding((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE, true);
    this.SetFontStyle((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE, (FontStyle) 2);
    this.SetLabelText((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE, msg);
    this.SetActive((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE_END, true);
    this.SetSupportEncoding((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE_END, true);
    this.SetFontStyle((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE_END, (FontStyle) 2);
    this.SetLabelText((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE_END, endTxt);
    this.tutorialTweenCtrl.Reset();
    this.tutorialTweenCtrl.Play();
  }

  public void HideTutorialMsg()
  {
    this.SetActive((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE_END, false);
    this.SetActive((Enum) LoadingUI.UI.LBL_FIRST_TUTORIAL_MESSAGE, false);
  }

  public void ShowTutorialBg(bool isShow)
  {
    this.SetActive((Enum) LoadingUI.UI.SPR_BG_TUTORIAL, isShow);
  }

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
    SPR_BG_TUTORIAL,
  }
}
