// Decompiled with JetBrains decompiler
// Type: TitleTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using rhyme;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class TitleTop : GameSection
{
  private const string EFFECT01_NAME = "ef_ui_title_01";
  private const string EFFECT04_NAME = "ef_ui_title_04";
  private TutorialBossDirector director;
  private GameObject tapPrefab;
  private Transform tapEffect;
  public static bool isFirstServerSelection = true;
  public static bool isFirstBoot = true;

  public override bool useOnPressBackKey => true;

  public override void OnPressBackKey() => Native.applicationQuit();

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    if (TitleTop.isFirstBoot && TitleTop.CheckTitleSkip())
    {
      bool wait = true;
      MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (is_success => wait = false));
      while (wait)
        yield return (object) null;
      wait = true;
      MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, (Action<UserClanData>) (userClanData =>
      {
        MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
        wait = false;
      }));
      while (wait)
        yield return (object) null;
      wait = true;
      MonoBehaviourSingleton<ClanMatchingManager>.I.SendInfo((Action<bool>) (is_success => wait = false));
      while (wait)
        yield return (object) null;
      this.SetActiveUI(false);
      base.Initialize();
    }
    else
    {
      LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_title_01");
      LoadObject lo_director = load_queue.Load(RESOURCE_CATEGORY.CUTSCENE, "InGameTutorialDirector");
      LoadObject lo_tap = load_queue.Load(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_title_04");
      while (load_queue.IsLoading())
        yield return (object) null;
      Transform transform = ResourceUtility.Realizes(lo_director.loadedObject);
      if (Object.op_Inequality((Object) transform, (Object) null))
      {
        this.director = ((Component) transform).GetComponent<TutorialBossDirector>();
        if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyTitleTop)
        {
          DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
          this.director.logo.camera.orthographicSize = specialDeviceInfo.TitleTopCameraSize;
          this.director.logo.bg.transform.localScale = specialDeviceInfo.TitleTopBGScale;
        }
        this.director.StartLogoAnimation(false, (System.Action) null, (System.Action) (() => this.SetActiveUI(true)));
        ((Behaviour) ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<RenderTargetCacher>()).enabled = false;
      }
      else
        this.SetActiveUI(true);
      this.tapPrefab = lo_tap.loadedObject as GameObject;
      base.Initialize();
    }
  }

  private void SetActiveUI(bool enable)
  {
    ((Component) this.GetCtrl((Enum) TitleTop.UI.Container)).gameObject.SetActive(enable);
  }

  public override void UpdateUI()
  {
    this.SetApplicationVersionText((Enum) TitleTop.UI.LBL_APP_VERSION);
    this.SetVisibleWidgetEffect((Enum) TitleTop.UI.TEX_BG, "ef_ui_title_01");
    if (MonoBehaviourSingleton<GlobalSettingsManager>.I.submissionVersion)
      this.SetActive((Enum) TitleTop.UI.BTN_ADVANCED_LOGIN, false);
    else
      this.SetActive((Enum) TitleTop.UI.BTN_ADVANCED_LOGIN, !MonoBehaviourSingleton<AccountManager>.I.account.IsRegist());
  }

  public override void Exit()
  {
    base.Exit();
    if (!Object.op_Inequality((Object) this.director, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this.director).gameObject);
    ((Behaviour) ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<RenderTargetCacher>()).enabled = true;
  }

  private bool IsAgreement()
  {
    return MonoBehaviourSingleton<AccountManager>.I.account.IsRegist() && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep >= 9 && MonoBehaviourSingleton<AccountManager>.I.termsCheck;
  }

  private void OnQuery_PUSH_START()
  {
    if (Object.op_Inequality((Object) null, (Object) this.tapEffect))
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (is_success =>
    {
      MonoBehaviourSingleton<ClanMatchingManager>.I.RequestUserDetail(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, (Action<UserClanData>) (userClanData =>
      {
        MonoBehaviourSingleton<UserInfoManager>.I.SetUserClan(userClanData);
        MonoBehaviourSingleton<ClanMatchingManager>.I.SendInfo((Action<bool>) (is_success_clan => GameSection.ResumeEvent(is_success & is_success_clan)));
      }));
      GameSection.ResumeEvent(is_success);
    }));
    this.tapEffect = ResourceUtility.Realizes((Object) this.tapPrefab);
    if (Object.op_Inequality((Object) null, (Object) this.tapEffect))
    {
      rymFX component = ((Component) this.tapEffect).GetComponent<rymFX>();
      if (Object.op_Inequality((Object) null, (Object) component) && Object.op_Inequality((Object) null, (Object) this.director))
        component.Cameras = new Camera[1]
        {
          this.director.logoCamera
        };
      this.tapEffect.localPosition = new Vector3(0.0f, 1000f, 0.1f);
      this.tapEffect.localScale = new Vector3(11f, 11f, 1f);
      ((Component) this.tapEffect).gameObject.SetActive(true);
    }
    this.SetActive((Enum) TitleTop.UI.BTN_CLEARCACHE, false);
    this.StartCoroutine(this.DelayStart());
  }

  private IEnumerator DelayStart()
  {
    yield return (object) new WaitForSeconds(0.5f);
    this.SetActive((Enum) TitleTop.UI.BTN_START, false);
    yield return (object) new WaitForSeconds(1.5f);
    this.DispatchEvent("START");
  }

  private void OnQuery_START()
  {
    if (!MonoBehaviourSingleton<AccountManager>.I.account.IsRegist())
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<AccountManager>.I.SendRegistCreate((Action<bool>) (is_success =>
      {
        if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name == "/colopl_rob")
          GameSection.ChangeStayEvent("OPENING");
        GameSection.ResumeEvent(is_success);
      }));
    }
    else if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep > 0 && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep <= 2)
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep != 1)
      {
        int tutorialStep = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
      }
      this.DispatchEvent("MAIN_MENU_HOME");
    }
    if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.END))
      return;
    Protocol.Force((System.Action) (() => MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialStep((Action<bool>) (is_success => { }))));
  }

  private void OnQuery_HOST_SELECT()
  {
  }

  private void OnQuery_TitleClearCacheConfirm_YES()
  {
    MenuReset.needClearCache = true;
    MenuReset.needPredownload = true;
  }

  public override void StartSection()
  {
    if (TitleTop.isFirstBoot && TitleTop.CheckTitleSkip())
      this.DispatchEvent("START");
    else if (MonoBehaviourSingleton<SoundManager>.IsValid())
      SoundManager.RequestBGM(1);
    TitleTop.isFirstBoot = false;
  }

  public static bool CheckTitleSkip()
  {
    return MonoBehaviourSingleton<AccountManager>.I.account.IsRegist() && 1 <= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
  }

  private enum UI
  {
    LBL_APP_VERSION,
    TEX_BG,
    Container,
    BTN_START,
    BTN_ADVANCED_LOGIN,
    BTN_CLEARCACHE,
  }
}
