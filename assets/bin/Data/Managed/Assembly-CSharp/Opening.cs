// Decompiled with JetBrains decompiler
// Type: Opening
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Opening : GameSection
{
  private GameObject cutSceneObjectRoot;
  private GameObject cutOP;
  private Animation cutSceneAnimation;
  private GameObject titleObjectRoot;
  private Animation titleAnimation;
  private Material whiteFadeMaterial;
  private float downloadGaugeDisplayTimer;
  private bool isAnimationStarted;
  private bool isRegisted;
  private bool isCacheClear;
  private bool isDownloading = true;
  private bool hasSkipped;
  private bool isCheckRegister;
  private bool endCutScene;
  private Opening.VoiceSequenceData[] voice_sequence = new Opening.VoiceSequenceData[10]
  {
    new Opening.VoiceSequenceData(4f, Opening.VOICE.V00),
    new Opening.VoiceSequenceData(5f, Opening.VOICE.V01),
    new Opening.VoiceSequenceData(6.6f, Opening.VOICE.V02),
    new Opening.VoiceSequenceData(6.8f, Opening.VOICE.V03),
    new Opening.VoiceSequenceData(4f, Opening.VOICE.V04),
    new Opening.VoiceSequenceData(7f, Opening.VOICE.V05),
    new Opening.VoiceSequenceData(7f, Opening.VOICE.V06),
    new Opening.VoiceSequenceData(4.8f, Opening.VOICE.V07),
    new Opening.VoiceSequenceData(7.4f, Opening.VOICE.V08),
    new Opening.VoiceSequenceData(5.2f, Opening.VOICE.V09)
  };
  private Opening.AudioSequeceData[] audio_sequence = new Opening.AudioSequeceData[5]
  {
    new Opening.AudioSequeceData(4f, Opening.AUDIO.SE_CUT_01),
    new Opening.AudioSequeceData(10f, Opening.AUDIO.SE_CUT_02),
    new Opening.AudioSequeceData(10f, Opening.AUDIO.SE_CUT_03),
    new Opening.AudioSequeceData(14f, Opening.AUDIO.SE_CUT_04),
    new Opening.AudioSequeceData(12f, Opening.AUDIO.SE_CUT_05)
  };

  public override bool useOnPressBackKey => true;

  public override void OnPressBackKey()
  {
    if (this.isAnimationStarted)
      return;
    Native.applicationQuit();
  }

  public override void Initialize() => this.StartCoroutine(this.DoInitialzie());

  private IEnumerator DoInitialzie()
  {
    bool isBGLoaded = false;
    this.isCheckRegister = false;
    Transform bgRoot = ((Component) MonoBehaviourSingleton<AppMain>.I).transform.Find("BG_CutObject");
    if (Object.op_Inequality((Object) bgRoot, (Object) null))
      isBGLoaded = true;
    LoadObject loadedCutSceneObj = (LoadObject) null;
    LoadObject loadedTitleObj = (LoadObject) null;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    if (!isBGLoaded)
    {
      loadedCutSceneObj = loadingQueue.Load(RESOURCE_CATEGORY.CUTSCENE, nameof (Opening));
      loadedTitleObj = loadingQueue.Load(RESOURCE_CATEGORY.CUTSCENE, "Title");
    }
    foreach (int se_id in (int[]) Enum.GetValues(typeof (Opening.AUDIO)))
      loadingQueue.CacheSE(se_id);
    foreach (int voice_id in (int[]) Enum.GetValues(typeof (Opening.VOICE)))
      loadingQueue.CacheVoice(voice_id);
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<AppMain>.I.mainCamera, (Object) null))
      ((Behaviour) MonoBehaviourSingleton<AppMain>.I.mainCamera).enabled = false;
    if (isBGLoaded)
    {
      this.cutSceneObjectRoot = ((Component) bgRoot.GetChild(0)).gameObject;
      this.titleObjectRoot = ((Component) bgRoot.GetChild(1)).gameObject;
    }
    else
    {
      this.cutSceneObjectRoot = ResourceUtility.Instantiate<Object>(loadedCutSceneObj.loadedObject) as GameObject;
      this.cutSceneObjectRoot.transform.parent = ((Component) MonoBehaviourSingleton<AppMain>.I).transform;
      this.titleObjectRoot = ResourceUtility.Instantiate<Object>(loadedTitleObj.loadedObject) as GameObject;
      this.titleObjectRoot.transform.parent = ((Component) MonoBehaviourSingleton<AppMain>.I).transform;
    }
    Transform transform1 = this.cutSceneObjectRoot.transform.Find("CUT_op");
    if (Object.op_Inequality((Object) transform1, (Object) null))
    {
      this.cutOP = ((Component) transform1).gameObject;
      this.cutSceneAnimation = this.cutOP.GetComponent<Animation>();
      this.cutOP.SetActive(false);
      if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyOpening)
        this.cutOP.transform.localScale = SpecialDeviceManager.SpecialDeviceInfo.OpeningCutScale;
    }
    Transform transform2 = this.cutSceneObjectRoot.transform.Find("Main Camera/Plane");
    if (Object.op_Inequality((Object) transform2, (Object) null))
      this.whiteFadeMaterial = ((Renderer) ((Component) transform2).GetComponent<MeshRenderer>()).material;
    this.titleAnimation = this.titleObjectRoot.GetComponent<Animation>();
    this.cutSceneAnimation.Stop();
    MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(10000101U);
    MonoBehaviourSingleton<UIManager>.I.loading.HideAllPermissionMsg();
    base.Initialize();
    PredownloadManager.openingMode = true;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<PredownloadManager>();
    MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, true);
    DataTableManager dataTableManager = MonoBehaviourSingleton<DataTableManager>.I;
    bool updatedTableIndex = false;
    Protocol.Send<CheckRegisterModel>(CheckRegisterModel.URL, (Action<CheckRegisterModel>) (ret => updatedTableIndex = true));
    yield return (object) new WaitUntil((Func<bool>) (() => updatedTableIndex));
    this.isDownloading = true;
    dataTableManager.InitializeForDownload();
    dataTableManager.UpdateManifest((System.Action) (() => dataTableManager.LoadInitialTable((System.Action) (() => MonoBehaviourSingleton<UIManager>.I.loading.SetProgress((IProgress) new FirstOpeningProgress(dataTableManager.LoadAllTable((System.Action) (() =>
    {
      PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_TUTORIAL, false);
      this.isDownloading = false;
    }), true)))), true)));
    TitleTop.isFirstBoot = false;
  }

  public override void StartSection()
  {
    base.StartSection();
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook)
      this.DispatchEvent("START");
    this.RefreshUI();
  }

  protected override void OnClose()
  {
    SoundManager.StopVoice();
    SoundManager.StopSEAll();
    if (Object.op_Inequality((Object) this.cutSceneObjectRoot, (Object) null))
    {
      Object.Destroy((Object) this.cutSceneObjectRoot);
      this.cutSceneObjectRoot = (GameObject) null;
    }
    if (Object.op_Inequality((Object) this.titleObjectRoot, (Object) null))
    {
      Object.Destroy((Object) this.titleObjectRoot);
      this.titleObjectRoot = (GameObject) null;
    }
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<AppMain>.I.mainCamera, (Object) null))
      ((Behaviour) MonoBehaviourSingleton<AppMain>.I.mainCamera).enabled = true;
    if (MonoBehaviourSingleton<PredownloadManager>.IsValid() && (MonoBehaviourSingleton<PredownloadManager>.I.tutorialCount == 0 || MonoBehaviourSingleton<PredownloadManager>.I.loadedCount < MonoBehaviourSingleton<PredownloadManager>.I.tutorialCount))
      Object.Destroy((Object) MonoBehaviourSingleton<PredownloadManager>.I);
    MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
    base.OnClose();
  }

  private void Update()
  {
    if (this.endCutScene)
      return;
    if (MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible)
    {
      this.downloadGaugeDisplayTimer -= Time.deltaTime;
      if ((double) this.downloadGaugeDisplayTimer <= 0.0)
      {
        this.downloadGaugeDisplayTimer = 0.0f;
        MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = false;
      }
    }
    if (!this.isAnimationStarted)
      return;
    GameSceneManager i = MonoBehaviourSingleton<GameSceneManager>.I;
    if (i.isChangeing || !this.isRegisted || !this.isCheckRegister)
      return;
    bool flag = i.GetCurrentSectionName() == nameof (Opening);
    if (Object.op_Equality((Object) this.cutSceneAnimation, (Object) null))
    {
      if (!(i.IsEventExecutionPossible() & flag))
        return;
      this.GoEventTutorial();
    }
    else if (!this.cutSceneAnimation.isPlaying)
    {
      if (!(i.IsEventExecutionPossible() & flag))
        return;
      this.GoEventTutorial();
    }
    else
    {
      if (!MonoBehaviourSingleton<InputManager>.I.IsTouch() || !Object.op_Equality((Object) MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection(), (Object) this))
        return;
      if (MonoBehaviourSingleton<PredownloadManager>.IsValid() && !MonoBehaviourSingleton<PredownloadManager>.I.isLoadingInOpening)
      {
        MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "OpeningSkipConfirm");
      }
      else
      {
        if (MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible)
          return;
        MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
        this.downloadGaugeDisplayTimer = 1f;
      }
    }
  }

  private void GoEventTutorial()
  {
    MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_loading_start, "Tutorial");
    this.endCutScene = true;
    this.Fade(Color.black, 0.0f, 1f, 1f, (System.Action) (() =>
    {
      if (MonoBehaviourSingleton<PredownloadManager>.I.isLoadingInOpening || this.isDownloading)
      {
        this.StartCoroutine("WaitForDownload");
      }
      else
      {
        this.DispatchEvent("ENTER_TUTORIAL");
        ResourceManager.internalMode = false;
        MonoBehaviourSingleton<UIManager>.I.ShowGGTutorialMessage();
      }
    }));
  }

  private IEnumerator WaitForDownload()
  {
    MonoBehaviourSingleton<UIManager>.I.loading.downloadGaugeVisible = true;
    while (MonoBehaviourSingleton<PredownloadManager>.I.isLoadingInOpening || this.isDownloading)
      yield return (object) null;
    ResourceManager.internalMode = false;
    this.DispatchEvent("ENTER_TUTORIAL");
    MonoBehaviourSingleton<UIManager>.I.ShowGGTutorialMessage();
  }

  private void OnQuery_START()
  {
    if (MonoBehaviourSingleton<AccountManager>.I.usageLimitMode)
    {
      GameSection.ChangeEvent("SERVICE_LIMIT");
    }
    else
    {
      this.isRegisted = MonoBehaviourSingleton<AccountManager>.I.account.IsRegist();
      if (!this.isRegisted)
      {
        GameSection.StayEvent();
        MonoBehaviourSingleton<AccountManager>.I.SendRegistCreate((Action<bool>) (is_success =>
        {
          if (is_success)
          {
            this.StartOpening();
            MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_login, "Tutorial", new Dictionary<string, object>()
            {
              {
                "login_type",
                (object) 1
              }
            });
          }
          else
            MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_start_game, "Tutorial");
          this.isRegisted = is_success;
          GameSection.ResumeEvent(is_success);
          this.isCheckRegister = true;
        }));
      }
      else
      {
        this.isCheckRegister = true;
        this.StartOpening();
        MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_start_game, "Tutorial");
      }
    }
  }

  private void OnQuery_FB_LOGIN()
  {
    GameSection.StayEvent();
    if (MonoBehaviourSingleton<FBManager>.I.isLoggedIn)
      this._SendRegistAuthFacebook();
    else
      MonoBehaviourSingleton<FBManager>.I.LoginWithReadPermission((Action<bool, string>) ((success, b) =>
      {
        if (success)
          this._SendRegistAuthFacebook();
        else
          GameSection.ResumeEvent(success);
      }));
  }

  private void _SendRegistAuthFacebook()
  {
    MonoBehaviourSingleton<AccountManager>.I.SendRegistAuthFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, (Action<bool>) (success =>
    {
      if (success)
      {
        MenuReset.needClearCache = true;
        MenuReset.needPredownload = true;
        if (!MonoBehaviourSingleton<UserInfoManager>.I.userInfo.IsModiedName)
        {
          MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_start_game, "Tutorial");
          MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_login, "Tutorial", new Dictionary<string, object>()
          {
            {
              "login_type",
              (object) 0
            }
          });
        }
        else
          MonoBehaviourSingleton<NativeGameService>.I.SetOldUserLogin();
      }
      GameSection.ResumeEvent(success);
    }));
  }

  private void StartOpening()
  {
    this.SetActive((Enum) Opening.UI.LBL_CURRENT_SERVER, false);
    this.SetActive((Enum) Opening.UI.BTN_CHANGE_SERVER, false);
    this.SetActive((Enum) Opening.UI.LBL_APP_VERSION, false);
    this.SetActive((Enum) Opening.UI.BTN_START, false);
    this.SetActive((Enum) Opening.UI.BTN_ADVANCED_LOGIN, false);
    this.SetActive((Enum) Opening.UI.BTN_CLEARCACHE, false);
    this.SetActive((Enum) Opening.UI.BTN_FB_LOGIN, false);
    MonoBehaviourSingleton<SoundManager>.I.TransitionTo(nameof (Opening));
    this.titleAnimation.Play("Tap");
    this.Fade(Color.white, 0.0f, 1f, 1f, (System.Action) (() =>
    {
      this.titleObjectRoot.SetActive(false);
      this.cutOP.SetActive(true);
      this.cutSceneAnimation.Play();
      this.StartCoroutine(this.DoSEPlay());
      this.StartCoroutine(this.DoVOICEPlay());
      this.isAnimationStarted = true;
      this.Fade(Color.white, 1f, 0.0f, 1f, (System.Action) (() => { }));
    }));
    MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_story, "Tutorial");
  }

  private IEnumerator DoVOICEPlay()
  {
    Opening.VoiceSequenceData[] voiceSequenceDataArray = this.voice_sequence;
    for (int index = 0; index < voiceSequenceDataArray.Length; ++index)
    {
      Opening.VoiceSequenceData seq = voiceSequenceDataArray[index];
      yield return (object) new WaitForSeconds(seq.delay);
      if (!this.hasSkipped)
        SoundManager.PlayVoice(seq.id);
      seq = (Opening.VoiceSequenceData) null;
    }
    voiceSequenceDataArray = (Opening.VoiceSequenceData[]) null;
  }

  private IEnumerator DoSEPlay()
  {
    Opening.AudioSequeceData[] audioSequeceDataArray = this.audio_sequence;
    for (int index = 0; index < audioSequeceDataArray.Length; ++index)
    {
      Opening.AudioSequeceData seq = audioSequeceDataArray[index];
      yield return (object) new WaitForSeconds(seq.delay);
      if (!this.hasSkipped)
        SoundManager.PlayOneShotUISE(seq.id);
      seq = (Opening.AudioSequeceData) null;
    }
    audioSequeceDataArray = (Opening.AudioSequeceData[]) null;
  }

  private void Fade(Color baseColor, float _from, float _to, float duration, System.Action onComplete)
  {
    this.StartCoroutine(this.DoFade(baseColor, _from, _to, duration, onComplete));
  }

  private IEnumerator DoFade(
    Color baseColor,
    float _from,
    float _to,
    float duration,
    System.Action onComplete)
  {
    float timer = 0.0f;
    this.whiteFadeMaterial.shader = !Color.op_Inequality(baseColor, Color.white) ? Shader.Find("mobile/Custom/Effect/effect_add") : Shader.Find("mobile/Custom/Effect/effect_alpha");
    while ((double) timer < (double) duration)
    {
      timer += Time.deltaTime;
      float num = Mathf.Lerp(_from, _to, timer / duration);
      this.whiteFadeMaterial.SetColor("_Color", new Color(baseColor.r, baseColor.g, baseColor.b, num));
      yield return (object) null;
    }
    if (onComplete != null)
      onComplete();
  }

  public override void UpdateUI()
  {
    this.SetApplicationVersionText((Enum) Opening.UI.LBL_APP_VERSION);
    this.SetLabelText((Enum) Opening.UI.LBL_CURRENT_SERVER, "Server: " + GameSaveData.instance.currentServer.name);
    if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && MonoBehaviourSingleton<GlobalSettingsManager>.I.submissionVersion)
      this.SetActive((Enum) Opening.UI.BTN_ADVANCED_LOGIN, false);
    else
      this.SetActive((Enum) Opening.UI.BTN_ADVANCED_LOGIN, !MonoBehaviourSingleton<AccountManager>.I.account.IsRegist());
    this.SetActive((Enum) Opening.UI.BTN_FB_LOGIN, !MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook);
    this.SetActive((Enum) Opening.UI.BTN_START, !MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook);
  }

  public override void Exit()
  {
    base.Exit();
    if (this.isCacheClear || MonoBehaviourSingleton<LoadingProcess>.IsValid())
      return;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<LoadingProcess>();
  }

  private void OnQuery_OpeningSkipConfirm_YES()
  {
    MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_loading_start, "Tutorial");
    ResourceManager.internalMode = false;
    this.hasSkipped = true;
    MonoBehaviourSingleton<UIManager>.I.ShowGGTutorialMessage();
  }

  private void OnQuery_TitleClearCacheConfirm_YES()
  {
    this.isCacheClear = true;
    MenuReset.needClearCache = true;
    MenuReset.needPredownload = false;
  }

  private enum UI
  {
    LBL_APP_VERSION,
    LBL_CURRENT_SERVER,
    BTN_START,
    BTN_FB_LOGIN,
    BTN_ADVANCED_LOGIN,
    BTN_CLEARCACHE,
    BTN_CHANGE_SERVER,
  }

  private enum AUDIO
  {
    SE_CUT_01 = 40000095, // 0x02625A5F
    SE_CUT_02 = 40000096, // 0x02625A60
    SE_CUT_03 = 40000097, // 0x02625A61
    SE_CUT_04 = 40000098, // 0x02625A62
    SE_CUT_05 = 40000099, // 0x02625A63
  }

  private enum VOICE
  {
    V00 = 401, // 0x00000191
    V01 = 402, // 0x00000192
    V02 = 403, // 0x00000193
    V03 = 404, // 0x00000194
    V04 = 405, // 0x00000195
    V05 = 406, // 0x00000196
    V06 = 407, // 0x00000197
    V07 = 408, // 0x00000198
    V08 = 409, // 0x00000199
    V09 = 410, // 0x0000019A
  }

  private class VoiceSequenceData
  {
    public int id;
    public float delay;

    public VoiceSequenceData(float _delay, Opening.VOICE _voice)
    {
      this.delay = _delay;
      this.id = (int) _voice;
    }
  }

  private class AudioSequeceData
  {
    public float delay;
    public Opening.AUDIO SEType;

    public int id => (int) this.SEType;

    public AudioSequeceData(float _delay, Opening.AUDIO _audio)
    {
      this.delay = _delay;
      this.SEType = _audio;
    }
  }
}
