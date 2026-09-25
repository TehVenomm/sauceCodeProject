// Decompiled with JetBrains decompiler
// Type: UIManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIManager : MonoBehaviourSingleton<UIManager>
{
  private const float FADER_GLOBAL_Z = -1f;
  public List<UIBehaviour> uiList = new List<UIBehaviour>();
  private bool internalUI;
  private bool initUseMouse;
  private bool initUseTouch;
  private bool enableUIInput = true;
  private float dialogBlockerAlpha;
  private TweenAlpha dialogBlockerTween;
  private bool _enableShadow;
  private bool _enableGachaLight;
  private List<UIManager.AtlasEntry> atlases = new List<UIManager.AtlasEntry>();
  public bool isShowingGGTutorialMessage;
  private float showGGTutorialMessageTime;

  public static UIBehaviour CreatePrefabUI(
    Object prefab,
    GameObject inactive_inctance,
    System.Type add_component_type,
    bool initVisible,
    Transform parent,
    int depth,
    GameSceneTables.SectionData section_data)
  {
    if (Object.op_Equality((Object) parent, (Object) null) && MonoBehaviourSingleton<UIManager>.IsValid())
      parent = MonoBehaviourSingleton<UIManager>.I._transform;
    string name = prefab.name;
    if (name.StartsWith("internal__"))
      name = name.Substring(name.LastIndexOf("__") + 2);
    Transform gameObject = Utility.CreateGameObject(name, parent, 5);
    ((Component) gameObject).gameObject.AddComponent<UIPanel>();
    Transform ui = !Object.op_Inequality((Object) inactive_inctance, (Object) null) ? ResourceUtility.Realizes(prefab, gameObject, 5) : InstantiateManager.Realizes(ref inactive_inctance, gameObject, 5);
    if (add_component_type == (System.Type) null)
      add_component_type = System.Type.GetType(((Object) ui).name);
    UIBehaviour target_ui = !(add_component_type != (System.Type) null) ? ((Component) gameObject).gameObject.AddComponent<UIBehaviour>() : ((Component) gameObject).gameObject.AddComponent(add_component_type) as UIBehaviour;
    target_ui.collectUI = ui;
    target_ui.sectionData = section_data;
    UIManager.TestTransitionAnim(ui, section_data);
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.common, (Object) null))
    {
      int button_index = 0;
      string caption = target_ui.GetCaptionText();
      if (section_data != (GameSceneTables.SectionData) null)
      {
        if (caption == null)
          caption = section_data.GetText("CAPTION");
        button_index = section_data.backButtonIndex;
      }
      if (button_index > 0)
        MonoBehaviourSingleton<UIManager>.I.common.AttachBackButton(target_ui, button_index - 1);
      MonoBehaviourSingleton<UIManager>.I.common.AttachCaption(target_ui, button_index, caption);
    }
    target_ui.InitUI();
    target_ui.baseDepth = depth;
    if (!initVisible)
    {
      ((Component) ui).GetComponentsInChildren<UIWidget>(Temporary.uiWidgetList);
      int index = 0;
      for (int count = Temporary.uiWidgetList.Count; index < count; ++index)
        Temporary.uiWidgetList[index]._Update();
      Temporary.uiWidgetList.Clear();
    }
    target_ui.uiVisible = initVisible;
    return target_ui;
  }

  private static void TestTransitionAnim(Transform ui, GameSceneTables.SectionData section_data)
  {
    if (section_data == (GameSceneTables.SectionData) null || section_data.type == GAME_SECTION_TYPE.SCENE || section_data.sectionName == "InGameMain")
      return;
    UIManager.SetDefaultTransitionAnim(ui, section_data.type != GAME_SECTION_TYPE.SCREEN);
  }

  private static void SetDefaultTransitionAnim(Transform ui, bool need_scale)
  {
    if (Object.op_Inequality((Object) ((Component) ui).gameObject.GetComponentInChildren<UITransition>(), (Object) null))
      return;
    UITransition uiTransition = ((Component) ui).gameObject.AddComponent<UITransition>();
    float num = MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() ? MonoBehaviourSingleton<GlobalSettingsManager>.I.defaultUITransitionAnimTime : 0.25f;
    int length = need_scale ? 2 : 1;
    uiTransition.openTweens = new UITweener[length];
    TweenAlpha tweenAlpha1;
    uiTransition.openTweens[0] = (UITweener) (tweenAlpha1 = ((Component) ui).gameObject.AddComponent<TweenAlpha>());
    tweenAlpha1.value = 0.0f;
    tweenAlpha1.SetStartToCurrentValue();
    tweenAlpha1.to = 1f;
    tweenAlpha1.duration = num;
    tweenAlpha1.animationCurve = Curves.easeLinear;
    tweenAlpha1.ignoreTimeScale = false;
    if (need_scale)
    {
      TweenScale tweenScale;
      uiTransition.openTweens[1] = (UITweener) (tweenScale = ((Component) ui).gameObject.AddComponent<TweenScale>());
      tweenScale.value = new Vector3(1.05f, 1.05f, 1f);
      tweenScale.SetStartToCurrentValue();
      tweenScale.to = Vector3.one;
      tweenScale.duration = num;
      tweenScale.animationCurve = Curves.easeIn;
      tweenScale.ignoreTimeScale = false;
    }
    uiTransition.closeTweens = new UITweener[length];
    TweenAlpha tweenAlpha2;
    uiTransition.closeTweens[0] = (UITweener) (tweenAlpha2 = ((Component) ui).gameObject.AddComponent<TweenAlpha>());
    tweenAlpha2.from = 1f;
    tweenAlpha2.to = 0.0f;
    tweenAlpha2.duration = num;
    tweenAlpha2.animationCurve = Curves.easeLinear;
    tweenAlpha2.ignoreTimeScale = false;
    if (need_scale)
    {
      TweenScale tweenScale;
      uiTransition.closeTweens[1] = (UITweener) (tweenScale = ((Component) ui).gameObject.AddComponent<TweenScale>());
      tweenScale.from = Vector3.one;
      tweenScale.to = new Vector3(1.05f, 1.05f, 1f);
      tweenScale.duration = num;
      tweenScale.animationCurve = Curves.easeIn;
      tweenScale.ignoreTimeScale = false;
    }
    uiTransition.InitTweens();
  }

  public static bool ProcessingStringForUILabel(ref string text)
  {
    if (!string.IsNullOrEmpty(text) && text[0] == '{' && text[text.Length - 1] == '}')
    {
      int num = text.IndexOf(',');
      if (num != -1)
      {
        string str = text.Substring(1, num - 1);
        string s = text.Substring(num + 1, text.Length - num - 2);
        text = StringTable.Get(StringCategory.FromString(str), uint.Parse(s));
        return true;
      }
    }
    return false;
  }

  public bool isLoading { get; private set; }

  public Camera uiCamera { get; private set; }

  public Camera[] cameras { get; private set; }

  public UICamera nguiCamera { get; private set; }

  public UIRoot uiRoot { get; private set; }

  public UIPanel uiRootPanel { get; private set; }

  public Transform uiRootTransform { get; private set; }

  public UIPanel faderPanel { get; private set; }

  public Transform buttonEffectTop { get; private set; }

  public Transform atlasTop { get; private set; }

  public UIBehaviour system { get; private set; }

  public LoadingUI loading { get; private set; }

  public MainMenu mainMenu { get; private set; }

  public MainStatus mainStatus { get; private set; }

  public UI_Common common { get; private set; }

  public UILevelUpAnnounce levelUp { get; private set; }

  public UIKnockDownRaidBossAnnounce knockDownRaidBoss { get; private set; }

  public UIClanCreateAnnounce clanCreate { get; private set; }

  public NPCMessage npcMessage { get; private set; }

  public MainChat mainChat { get; private set; }

  public EventBannerView bannerView { get; private set; }

  public QuestInvitationButton invitationButton { get; private set; }

  public QuestInvitationInGameButton invitationInGameButton { get; private set; }

  public TaskClearAnnounce taskClearAnnouce { get; private set; }

  public LoungeAnnounce loungeAnnounce { get; private set; }

  public ClanAnnounce clanAnnounce { get; private set; }

  public BlackMarketButton blackMarkeButton { get; private set; }

  public FortuneWheelButton fortuneWheelButton { get; private set; }

  public TutorialMessage tutorialMessage { get; private set; }

  public bool IsEnableTutorialMessage()
  {
    return Object.op_Inequality((Object) this.tutorialMessage, (Object) null) && this.tutorialMessage.IsEnableMessage();
  }

  public bool IsTutorialErrorResend()
  {
    if (Object.op_Inequality((Object) this.tutorialMessage, (Object) null) && this.tutorialMessage.isErrorResend)
      return true;
    string currentSceneName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName();
    string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
    if (!string.IsNullOrEmpty(currentSceneName) && !string.IsNullOrEmpty(currentSectionName))
    {
      bool flag = false;
      if (currentSceneName != "InGameScene")
        flag = currentSceneName == "ShopScene" || currentSceneName == "GachaScene" || currentSectionName.Contains("QuestAccept");
      if (((!Object.op_Inequality((Object) this.tutorialMessage, (Object) null) ? 0 : (this.tutorialMessage.isErrorResendQuestGacha ? 1 : 0)) & (flag ? 1 : 0)) != 0)
        return true;
    }
    return false;
  }

  public UIManager.DISABLE_FACTOR disableFlags { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    this.uiRoot = ((Component) this).GetComponent<UIRoot>();
    UIVirtualScreen.InitUIRoot(this.uiRoot);
    this.uiCamera = ((Component) this.uiRoot).GetComponentInChildren<Camera>();
    this.cameras = new Camera[1]{ this.uiCamera };
    this.nguiCamera = ((Component) this.uiCamera).GetComponent<UICamera>();
    this.uiRootPanel = ((Component) this.uiRoot).GetComponent<UIPanel>();
    this.uiRootTransform = ((Component) this.uiRoot).transform;
    this.initUseMouse = this.nguiCamera.useMouse;
    this.initUseTouch = this.nguiCamera.useTouch;
    this.system = UIManager.CreatePrefabUI(Resources.Load("UI/SystemUI"), (GameObject) null, (System.Type) null, true, this._transform, 0, (GameSceneTables.SectionData) null);
    this.system.CreateCtrlsArray(typeof (UIManager.SYSTEM));
    this.updateBlockerSize();
    Transform ctrl1 = this.system.GetCtrl((Enum) UIManager.SYSTEM.FADER);
    this.faderPanel = ((Component) ctrl1.parent).GetComponent<UIPanel>();
    this.faderPanel.depth = 4000;
    Vector3 position = ctrl1.position;
    position.z = -1f;
    ctrl1.position = position;
    ((Component) this.system.GetCtrl((Enum) UIManager.SYSTEM.BLOCKER)).gameObject.SetActive(false);
    Transform ctrl2 = this.system.GetCtrl((Enum) UIManager.SYSTEM.DIALOG_BLOCKER);
    this.dialogBlockerAlpha = ((Component) ctrl2).GetComponent<UIRect>().alpha;
    this.dialogBlockerTween = TweenAlpha.Begin(((Component) ctrl2).gameObject, 0.2f, this.dialogBlockerAlpha);
    this.dialogBlockerTween.value = 0.0f;
    this.dialogBlockerTween.from = 0.0f;
    ((Behaviour) this.dialogBlockerTween).enabled = false;
    ((Component) ctrl2).gameObject.SetActive(false);
    this.SetLoadingUI(Resources.Load("InternalUI/UI_Common/LoadingUI"));
    this.internalUI = true;
    GameObject gameObject1 = new GameObject("ButtonEffectTop");
    gameObject1.AddComponent<UIPanel>().depth = 10000;
    this.buttonEffectTop = gameObject1.transform;
    this.buttonEffectTop.SetParent(this.uiRootTransform);
    this.buttonEffectTop.localPosition = Vector3.zero;
    this.buttonEffectTop.localRotation = Quaternion.identity;
    this.buttonEffectTop.localScale = Vector3.one;
    gameObject1.layer = ((Component) this.uiRoot).gameObject.layer;
    GameObject gameObject2 = new GameObject("AtlasTop");
    this.atlasTop = gameObject2.transform;
    this.atlasTop.SetParent(this.buttonEffectTop);
    gameObject2.SetActive(false);
    UIButtonEffect.CacheShaderPropertyId();
    this.enableShadow = false;
    this.enableGachaLight = false;
  }

  public void SetLoadingUI(Object prefab)
  {
    this.internalUI = false;
    if (Object.op_Inequality((Object) this.loading, (Object) null))
    {
      Object.Destroy((Object) ((Component) this.loading).gameObject);
      this.loading = (LoadingUI) null;
    }
    this.loading = UIManager.CreatePrefabUI(prefab, (GameObject) null, (System.Type) null, true, this._transform, 9100, (GameSceneTables.SectionData) null) as LoadingUI;
    this.updateBlockerSize();
    this.loading.UpdateUI();
  }

  private void OnEnable()
  {
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  protected override void OnDisable()
  {
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    base.OnDisable();
  }

  public void LoadUI(bool need_common, bool need_outgame, bool need_tutorial, bool skipChat = false)
  {
    if (this.internalUI || this.isLoading || !need_common && !need_outgame && !need_tutorial)
      return;
    this.StartCoroutine(this.DoLoadUI(need_common, need_outgame, need_tutorial, skipChat));
  }

  private IEnumerator DoLoadUI(
    bool need_common,
    bool need_outgame,
    bool need_tutorial,
    bool skipChat = false)
  {
    this.isLoading = true;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    bool flag1 = true;
    bool flag2 = need_outgame;
    LoadObject lo_common = Object.op_Equality((Object) this.common, (Object) null) & need_common ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_Common") : (LoadObject) null;
    LoadObject lo_main_menu = Object.op_Equality((Object) this.mainMenu, (Object) null) & need_outgame ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "MainMenu") : (LoadObject) null;
    LoadObject lo_main_status = Object.op_Equality((Object) this.mainStatus, (Object) null) & need_outgame ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "MainStatus") : (LoadObject) null;
    LoadObject lo_npc_msg = Object.op_Equality((Object) this.npcMessage, (Object) null) & need_outgame ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "NPCMessage") : (LoadObject) null;
    LoadObject lo_main_chat = Object.op_Equality((Object) this.mainChat, (Object) null) & flag1 ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "MainChat") : (LoadObject) null;
    LoadObject lo_banner_view = Object.op_Equality((Object) this.bannerView, (Object) null) & flag2 ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "EventBannerView") : (LoadObject) null;
    LoadObject lo_invitation = Object.op_Equality((Object) this.invitationButton, (Object) null) & need_outgame ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "QuestInvitationButton") : (LoadObject) null;
    LoadObject lo_invitation_ingame = Object.op_Equality((Object) this.invitationInGameButton, (Object) null) & need_outgame ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "QuestInvitationInGameButton") : (LoadObject) null;
    LoadObject lo_tutorial = Object.op_Equality((Object) this.tutorialMessage, (Object) null) & need_tutorial ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "TutorialMessage") : (LoadObject) null;
    LoadObject lo_taskAnnounce = Object.op_Equality((Object) this.taskClearAnnouce, (Object) null) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "TaskClearAnnounce") : (LoadObject) null;
    LoadObject lo_loungeAnnoucne = Object.op_Equality((Object) this.loungeAnnounce, (Object) null) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "LoungeAnnounce") : (LoadObject) null;
    LoadObject lo_clanAnnoucne = Object.op_Equality((Object) this.clanAnnounce, (Object) null) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "LoungeAnnounce") : (LoadObject) null;
    LoadObject lo_black_market = Object.op_Equality((Object) this.blackMarkeButton, (Object) null) & need_outgame ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "BlackMarketButton") : (LoadObject) null;
    LoadObject lo_fortune_wheel = Object.op_Equality((Object) this.fortuneWheelButton, (Object) null) & need_outgame ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "FortuneWheelButton") : (LoadObject) null;
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    if (lo_main_menu != null)
      this.mainMenu = UIManager.CreatePrefabUI(lo_main_menu.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 3000, (GameSceneTables.SectionData) null) as MainMenu;
    if (lo_main_status != null)
      this.mainStatus = UIManager.CreatePrefabUI(lo_main_status.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 3000, (GameSceneTables.SectionData) null) as MainStatus;
    if (lo_common != null)
    {
      this.common = UIManager.CreatePrefabUI(lo_common.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 3000, (GameSceneTables.SectionData) null) as UI_Common;
      this.common.Open();
      this.levelUp = ((Component) this.common).gameObject.GetComponentInChildren<UILevelUpAnnounce>();
      this.knockDownRaidBoss = ((Component) this.common).gameObject.GetComponentInChildren<UIKnockDownRaidBossAnnounce>();
      this.clanCreate = ((Component) this.common).gameObject.GetComponentInChildren<UIClanCreateAnnounce>();
    }
    if (lo_npc_msg != null)
      this.npcMessage = UIManager.CreatePrefabUI(lo_npc_msg.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 100, (GameSceneTables.SectionData) null) as NPCMessage;
    if (lo_main_chat != null && !skipChat)
      this.mainChat = UIManager.CreatePrefabUI(lo_main_chat.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 6000, (GameSceneTables.SectionData) null) as MainChat;
    if (lo_banner_view != null)
      this.bannerView = UIManager.CreatePrefabUI(lo_banner_view.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 2000, (GameSceneTables.SectionData) null) as EventBannerView;
    if (lo_invitation != null)
      this.invitationButton = UIManager.CreatePrefabUI(lo_invitation.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 3000, (GameSceneTables.SectionData) null) as QuestInvitationButton;
    if (lo_invitation_ingame != null)
      this.invitationInGameButton = UIManager.CreatePrefabUI(lo_invitation_ingame.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 1000, (GameSceneTables.SectionData) null) as QuestInvitationInGameButton;
    if (lo_tutorial != null)
      this.tutorialMessage = UIManager.CreatePrefabUI(lo_tutorial.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 6500, (GameSceneTables.SectionData) null) as TutorialMessage;
    if (lo_taskAnnounce != null)
      this.taskClearAnnouce = UIManager.CreatePrefabUI(lo_taskAnnounce.loadedObject, (GameObject) null, typeof (TaskClearAnnounce), true, this._transform, 9050, (GameSceneTables.SectionData) null) as TaskClearAnnounce;
    if (lo_loungeAnnoucne != null)
      this.loungeAnnounce = UIManager.CreatePrefabUI(lo_loungeAnnoucne.loadedObject, (GameObject) null, typeof (LoungeAnnounce), true, this._transform, 1100, (GameSceneTables.SectionData) null) as LoungeAnnounce;
    if (lo_clanAnnoucne != null)
      this.clanAnnounce = UIManager.CreatePrefabUI(lo_clanAnnoucne.loadedObject, (GameObject) null, typeof (ClanAnnounce), true, this._transform, 1100, (GameSceneTables.SectionData) null) as ClanAnnounce;
    if (lo_black_market != null)
      this.blackMarkeButton = UIManager.CreatePrefabUI(lo_black_market.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 3000, (GameSceneTables.SectionData) null) as BlackMarketButton;
    if (lo_fortune_wheel != null)
      this.fortuneWheelButton = UIManager.CreatePrefabUI(lo_fortune_wheel.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 3000, (GameSceneTables.SectionData) null) as FortuneWheelButton;
    this.isLoading = false;
  }

  public void DeleteUI()
  {
    if (Object.op_Inequality((Object) this.mainMenu, (Object) null))
    {
      Object.DestroyImmediate((Object) ((Component) this.mainMenu).gameObject);
      this.mainMenu = (MainMenu) null;
    }
    if (!Object.op_Inequality((Object) this.bannerView, (Object) null))
      return;
    Object.DestroyImmediate((Object) ((Component) this.bannerView).gameObject);
    this.bannerView = (EventBannerView) null;
  }

  public void ResetUI()
  {
    if (Object.op_Inequality((Object) this.mainMenu, (Object) null))
    {
      ((Component) this.mainMenu).gameObject.SetActive(false);
      ((Component) this.mainMenu).gameObject.SetActive(true);
    }
    if (Object.op_Inequality((Object) this.mainStatus, (Object) null))
    {
      ((Component) this.mainStatus).gameObject.SetActive(false);
      ((Component) this.mainStatus).gameObject.SetActive(true);
    }
    if (Object.op_Inequality((Object) this.npcMessage, (Object) null))
    {
      ((Component) this.npcMessage).gameObject.SetActive(false);
      ((Component) this.npcMessage).gameObject.SetActive(true);
    }
    if (!Object.op_Inequality((Object) this.bannerView, (Object) null))
      return;
    ((Component) this.bannerView).gameObject.SetActive(false);
    ((Component) this.bannerView).gameObject.SetActive(true);
  }

  public bool enableShadow
  {
    get => this._enableShadow;
    set
    {
      this._enableShadow = value;
      ((Component) this.system.GetCtrl((Enum) UIManager.SYSTEM.RENDER_LIGHT)).gameObject.SetActive(this._enableShadow);
      QualitySettings.SetQualityLevel(this._enableShadow ? 1 : 0);
    }
  }

  public bool enableGachaLight
  {
    get => this._enableGachaLight;
    set
    {
      this._enableGachaLight = value;
      ((Component) this.system.GetCtrl((Enum) UIManager.SYSTEM.GACHA_RENDER_LIGHT)).gameObject.SetActive(this._enableGachaLight);
      QualitySettings.SetQualityLevel(this._enableGachaLight ? 1 : 0);
    }
  }

  private void OnScreenRotate(bool is_portrait)
  {
    UIVirtualScreen.InitUIRoot(this.uiRoot);
    this.uiList.ForEach((Action<UIBehaviour>) (o =>
    {
      UIVirtualScreen componentInChildren = ((Component) o).gameObject.GetComponentInChildren<UIVirtualScreen>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.InitWidget();
    }));
  }

  public void SetDisable(UIManager.DISABLE_FACTOR factor, bool is_disable)
  {
    if (is_disable)
      this.disableFlags |= factor;
    else
      this.disableFlags &= ~factor;
    this.updateBlockerSize();
    ((Component) this.system.GetCtrl((Enum) UIManager.SYSTEM.BLOCKER)).gameObject.SetActive(this.disableFlags != 0);
    this.loading.UpdateUIDisableFactor(this.disableFlags);
  }

  public void SetDisableMoment()
  {
    this.SetDisable(UIManager.DISABLE_FACTOR.MOMENT, true);
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.SetDisable(UIManager.DISABLE_FACTOR.MOMENT, false));
  }

  public bool IsDisable() => this.disableFlags != 0;

  public void SetEnableUIInput(bool is_enable)
  {
    if (this.enableUIInput == is_enable)
      return;
    this.enableUIInput = is_enable;
    if (is_enable)
    {
      this.nguiCamera.useTouch = this.initUseTouch;
      this.nguiCamera.useMouse = this.initUseMouse;
    }
    else
    {
      this.nguiCamera.useTouch = false;
      this.nguiCamera.useMouse = false;
      if (this.nguiCamera.allowMultiTouch || UICamera.CountInputSources() != 1)
        return;
      UICamera.MouseOrTouch touch = UICamera.GetTouch(1);
      if (touch != null && Object.op_Inequality((Object) touch.pressed, (Object) null))
      {
        UIButton component = touch.pressed.GetComponent<UIButton>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.SetState(UIButtonColor.State.Normal, true);
        UIScrollView componentInParent = touch.pressed.GetComponentInParent<UIScrollView>();
        if (Object.op_Inequality((Object) componentInParent, (Object) null))
          componentInParent.Press(false);
        touch.pressed = (GameObject) null;
      }
      if (UICamera.currentTouch == null)
        return;
      this.nguiCamera.ProcessTouch(false, true);
    }
  }

  public bool IsEnableUIInput() => this.enableUIInput;

  public Transform Find(string name)
  {
    UIBehaviour last = this.uiList.FindLast((Predicate<UIBehaviour>) (o => ((Object) o).name == name));
    return Object.op_Inequality((Object) last, (Object) null) ? last._transform : Utility.Find(this._transform, name);
  }

  public bool IsTransitioning()
  {
    return Object.op_Inequality((Object) this.uiList.FindLast((Predicate<UIBehaviour>) (o => UIManager.IsTransitioning(o))), (Object) null);
  }

  public bool IsTransitioningMainUI()
  {
    return UIManager.IsTransitioning((UIBehaviour) this.mainMenu) || UIManager.IsTransitioning((UIBehaviour) this.mainStatus) || UIManager.IsTransitioning((UIBehaviour) this.mainChat) || UIManager.IsTransitioning((UIBehaviour) this.bannerView);
  }

  private static bool IsTransitioning(UIBehaviour ui)
  {
    return !Object.op_Equality((Object) ui, (Object) null) && ui.IsTransitioning();
  }

  public void AttachScene(GameObject obj, int index = 0)
  {
    Utility.Attach(Object.op_Inequality((Object) this.uiCamera, (Object) null) ? ((Component) this.uiCamera).transform : this._transform, obj.transform);
  }

  public void UpdateDialogBlocker(
    GameSectionHierarchy hierarchy,
    GameSceneTables.SectionData new_section_data)
  {
    if (new_section_data != (GameSceneTables.SectionData) null && MonoBehaviourSingleton<GoGameSettingsManager>.IsValid() && MonoBehaviourSingleton<GoGameSettingsManager>.I.PreventUpdateDialogBlocker(new_section_data.sectionName))
      return;
    this.updateBlockerSize();
    GameObject blocker = ((Component) this.system.GetCtrl((Enum) UIManager.SYSTEM.DIALOG_BLOCKER)).gameObject;
    int dialogBlockerDepth = hierarchy.GetDialogDialogBlockerDepth(new_section_data);
    int num = 3000;
    if (dialogBlockerDepth > -1)
    {
      blocker.SetActive(true);
      this.dialogBlockerTween.SetOnFinished((EventDelegate.Callback) null);
      this.dialogBlockerTween.PlayForward();
      blocker.GetComponent<UIPanel>().depth = dialogBlockerDepth;
    }
    else
    {
      this.dialogBlockerTween.SetOnFinished((EventDelegate.Callback) (() => blocker.SetActive(false)));
      this.dialogBlockerTween.PlayReverse();
      if (blocker.activeSelf)
        num = -1;
    }
    if (num <= -1)
      return;
    if (Object.op_Inequality((Object) this.mainMenu, (Object) null))
      this.mainMenu.baseDepth = num;
    if (!Object.op_Inequality((Object) this.mainStatus, (Object) null))
      return;
    this.mainStatus.baseDepth = num;
  }

  public void OnNotify(GameSection.NOTIFY_FLAG notify_flags)
  {
    int index = 0;
    for (int count = this.uiList.Count; index < count; ++index)
    {
      UIBehaviour ui = this.uiList[index];
      if (!(ui is GameSection))
        ui.OnNotify(notify_flags);
    }
  }

  public void UpdateMainUI()
  {
    this.UpdateMainUI(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName(), MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
  }

  public void UpdateMainUI(string scene_name, string section_name)
  {
    bool flag1 = GameSceneGlobalSettings.IsDisplayMainUI(scene_name, section_name);
    if (Object.op_Inequality((Object) this.mainMenu, (Object) null))
    {
      if (flag1)
        this.mainMenu.Open();
      else
        this.mainMenu.Close();
    }
    bool flag2 = GameSceneGlobalSettings.IsDisplayMainStatusUI(scene_name, section_name);
    if (Object.op_Inequality((Object) this.mainStatus, (Object) null))
    {
      if (flag2)
        this.mainStatus.Open();
      else
        this.mainStatus.Close();
    }
    this.GCAtlas();
  }

  private void Update()
  {
    if (!Input.GetKeyUp((KeyCode) 27))
      return;
    if (!string.IsNullOrEmpty(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialBit) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM) || !TutorialStep.HasAllTutorialCompleted())
    {
      if (!MonoBehaviourSingleton<GameSceneManager>.IsValid())
        return;
      string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
      if (string.IsNullOrEmpty(currentSectionName))
        return;
      if (currentSectionName == "Opening" || currentSectionName == "AccountTop" || currentSectionName == "AccountLoginMail" || currentSectionName == "TitleClearCacheConfirm" || currentSectionName == "AccountContact" || currentSectionName == "OpeningSkipConfirm" || currentSectionName == "CommonDialogError" || currentSectionName == "TermsInfo" || currentSectionName == "ConfigServer")
      {
        this.ProcessBackKey();
      }
      else
      {
        if (!MonoBehaviourSingleton<ToastManager>.IsValid() || MonoBehaviourSingleton<ToastManager>.I.IsShowingDialog())
          return;
        ToastManager.PushOpen(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "TitleScene" ? "You are unable to go back to Town Scene during this Tutorial Mission" : StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 36U));
      }
    }
    else if (this.IsEnableTutorialMessage())
    {
      if (!this.tutorialMessage.IsOnlyShowImage() || !Object.op_Equality((Object) TutorialMessage.GetCursor(), (Object) null))
        return;
      this.tutorialMessage.TutorialClose();
    }
    else
    {
      if (Object.op_Inequality((Object) this.tutorialMessage, (Object) null) && Object.op_Inequality((Object) TutorialMessage.GetCursor(), (Object) null) || MenuReset.needClearCache && MenuReset.needPredownload)
        return;
      this.ProcessBackKey();
    }
  }

  private void ProcessBackKey()
  {
    if (!MonoBehaviourSingleton<GameSceneManager>.IsValid() || !MonoBehaviourSingleton<GameSceneManager>.I.IsBackKeyEventExecutionPossible())
      return;
    if (Object.op_Inequality((Object) this.mainChat, (Object) null))
    {
      if (this.mainChat.StateMachine != null && this.mainChat.StateMachine.CurrentState != null && this.mainChat.StateMachine.CurrentState is ChatState_PersonalMsgView currentState && Object.op_Inequality((Object) currentState.M_friendMsg, (Object) null))
      {
        FriendMessageUIController mFriendMsg = currentState.M_friendMsg;
        if (Object.op_Inequality((Object) mFriendMsg, (Object) null) && ((Component) mFriendMsg).gameObject.activeSelf)
        {
          mFriendMsg.OnClickCloseButton();
          return;
        }
      }
      if (Object.op_Implicit((Object) this.mainChat) && this.mainChat.IsOpeningWindow())
      {
        this.mainChat.OnPressBackKey();
        return;
      }
    }
    GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
    if (!Object.op_Inequality((Object) currentSection, (Object) null) || !Object.op_Inequality((Object) currentSection.collectUI, (Object) null))
      return;
    if (currentSection.useOnPressBackKey)
    {
      currentSection.OnPressBackKey();
    }
    else
    {
      string str = "[BACK]";
      if (!string.IsNullOrEmpty(currentSection.overrideBackKeyEvent))
        str = currentSection.overrideBackKeyEvent;
      UIGameSceneEventSender[] componentsInChildren = ((Component) currentSection.collectUI).GetComponentsInChildren<UIGameSceneEventSender>();
      int index = 0;
      for (int length = componentsInChildren.Length; index < length; ++index)
      {
        if (componentsInChildren[index].eventName == str)
        {
          componentsInChildren[index]._SendEvent();
          break;
        }
      }
    }
  }

  public UIManager.AtlasEntry ReplaceAtlas(UISprite sprite, string shader)
  {
    if (Object.op_Equality((Object) null, (Object) sprite) || Object.op_Equality((Object) null, (Object) sprite.atlas))
      return (UIManager.AtlasEntry) null;
    UIManager.AtlasEntry atlasEntry = this.atlases.Find((Predicate<UIManager.AtlasEntry>) (o => ((object) sprite.atlas).Equals((object) o.orgAtlas)));
    if (atlasEntry != null && (Object.op_Equality((Object) null, (Object) atlasEntry.copyAtlas) || Object.op_Equality((Object) null, (Object) atlasEntry.orgAtlas)))
    {
      this.atlases.Remove(atlasEntry);
      atlasEntry = (UIManager.AtlasEntry) null;
    }
    if (atlasEntry == null)
    {
      UIAtlas copyAtlas = !Object.op_Inequality((Object) sprite.atlas.replacement, (Object) null) ? ResourceUtility.Instantiate<UIAtlas>(sprite.atlas) : ResourceUtility.Instantiate<UIAtlas>(sprite.atlas.replacement);
      if (!Object.op_Equality((Object) copyAtlas, (Object) null))
        Object.op_Equality((Object) copyAtlas.spriteMaterial, (Object) null);
      copyAtlas.spriteMaterial = new Material(copyAtlas.spriteMaterial);
      copyAtlas.spriteMaterial.shader = ResourceUtility.FindShader(shader);
      atlasEntry = new UIManager.AtlasEntry(sprite.atlas, copyAtlas);
      this.atlases.Add(atlasEntry);
      ((Object) copyAtlas).name = "_" + ((Object) sprite.atlas).name;
    }
    atlasEntry.orgSpriteList.Add(sprite);
    sprite.atlas = atlasEntry.copyAtlas;
    return atlasEntry;
  }

  public void ReleaseAtlas(UISprite sprite)
  {
    if (Object.op_Equality((Object) null, (Object) sprite) || Object.op_Equality((Object) null, (Object) sprite.atlas))
      return;
    UIManager.AtlasEntry atlasEntry = this.atlases.Find((Predicate<UIManager.AtlasEntry>) (o => ((object) sprite.atlas).Equals((object) o.orgAtlas)));
    if (atlasEntry == null)
      return;
    atlasEntry.orgSpriteList.Remove(sprite);
    if (0 < atlasEntry.orgSpriteList.Count)
      return;
    if (Object.op_Inequality((Object) null, (Object) atlasEntry.copyAtlas))
    {
      Object.Destroy((Object) atlasEntry.copyAtlas.spriteMaterial);
      Object.Destroy((Object) ((Component) atlasEntry.copyAtlas).gameObject);
    }
    this.atlases.Remove(atlasEntry);
  }

  public void GCAtlas()
  {
    int count = this.atlases.Count;
    for (int index = 0; index < count; ++index)
    {
      UIManager.AtlasEntry atlase = this.atlases[index];
      atlase.orgSpriteList.RemoveAll((Predicate<UISprite>) (o => Object.op_Equality((Object) null, (Object) o)));
      if (0 >= atlase.orgSpriteList.Count)
      {
        if (Object.op_Inequality((Object) null, (Object) atlase.copyAtlas))
        {
          Object.Destroy((Object) atlase.copyAtlas.spriteMaterial);
          Object.Destroy((Object) ((Component) atlase.copyAtlas).gameObject);
        }
        atlase.copyAtlas = (UIAtlas) null;
      }
    }
    this.atlases.RemoveAll((Predicate<UIManager.AtlasEntry>) (o => Object.op_Equality((Object) null, (Object) o.copyAtlas)));
  }

  public void LoadTutorialMessage(System.Action callback)
  {
    this.StartCoroutine(this._LoadTutorialMessage(callback));
  }

  private IEnumerator _LoadTutorialMessage(System.Action callback)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_tutorial = Object.op_Equality((Object) this.tutorialMessage, (Object) null) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "TutorialMessage") : (LoadObject) null;
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    if (lo_tutorial != null)
      this.tutorialMessage = UIManager.CreatePrefabUI(lo_tutorial.loadedObject, (GameObject) null, (System.Type) null, false, this._transform, 6500, (GameSceneTables.SectionData) null) as TutorialMessage;
    callback();
  }

  private void updateBlockerSize()
  {
    if (Object.op_Inequality((Object) this.system, (Object) null))
    {
      UIVirtualScreen componentInChildren = ((Component) this.system).GetComponentInChildren<UIVirtualScreen>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
      {
        componentInChildren.IsOverSafeArea = true;
        componentInChildren.InitWidget();
      }
    }
    if (!Object.op_Inequality((Object) this.loading, (Object) null))
      return;
    UIVirtualScreen componentInChildren1 = ((Component) this.loading).GetComponentInChildren<UIVirtualScreen>();
    if (!Object.op_Inequality((Object) componentInChildren1, (Object) null))
      return;
    componentInChildren1.IsOverSafeArea = true;
    componentInChildren1.InitWidget();
  }

  public bool canHideGGTutorialMessage(float waitTIme)
  {
    return (double) Time.time - (double) this.showGGTutorialMessageTime > (double) waitTIme;
  }

  public void ShowGGTutorialMessage()
  {
    this.isShowingGGTutorialMessage = true;
    this.StartCoroutine("ShowGGTutorialMessage_");
  }

  public void HideGGTutorialMessage()
  {
    this.isShowingGGTutorialMessage = false;
    this.StopCoroutine("ShowGGTutorialMessage_");
    this.loading.HideTutorialMsg();
    this.showGGTutorialMessageTime = 0.0f;
  }

  private IEnumerator ShowGGTutorialMessage_()
  {
    uint i = 0;
    uint j = 0;
    uint tutLen = (uint) StringTable.GetAllInCategory(STRING_CATEGORY.TUTORIAL_LOADING_MSG).Length;
    while (true)
    {
      string msg;
      string endTxt;
      do
      {
        msg = StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, i);
        endTxt = j != 0U ? (j != 1U ? (j != 2U ? "..." : "..[000000].[-]") : ".[000000]..[-]") : "[000000]...[-]";
      }
      while (!Object.op_Inequality((Object) this.system, (Object) null));
      this.loading.ShowTutorialMsg(msg, endTxt);
      UIVirtualScreen vscreen = ((Component) this.system).GetComponentInChildren<UIVirtualScreen>();
      yield return (object) new WaitForSeconds(2.2f);
      if (Object.op_Inequality((Object) vscreen, (Object) null))
      {
        ++i;
        ++j;
        vscreen.IsOverSafeArea = true;
        if (i >= tutLen - 1U)
          i = tutLen - 1U;
        vscreen.InitWidget();
        if (j > 3U)
          j = 0U;
      }
      vscreen = (UIVirtualScreen) null;
    }
  }

  public void ShowEndGGTutorialMessage() => this.StartCoroutine("ShowEndGGTutorialMessage_");

  public void HideEndGGTutorialMessage()
  {
    this.StopCoroutine("ShowEndGGTutorialMessage_");
    this.loading.HideTutorialMsg();
    this.showGGTutorialMessageTime = 0.0f;
  }

  private IEnumerator ShowEndGGTutorialMessage_()
  {
    UIVirtualScreen vscreen = ((Component) this.loading).GetComponentInChildren<UIVirtualScreen>();
    this.showGGTutorialMessageTime = Time.time;
    yield return (object) new WaitForSeconds(0.2f);
    if (Object.op_Inequality((Object) vscreen, (Object) null))
    {
      this.loading.ShowTutorialMsg(StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, 3U), string.Empty);
      vscreen.IsOverSafeArea = true;
      yield return (object) new WaitForSeconds(1f);
      vscreen.InitWidget();
      this.loading.ShowTutorialMsg(StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, 4U), string.Empty);
      yield return (object) new WaitForSeconds(1f);
      string text = StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, 5U);
      this.loading.ShowTutorialMsg(text, "[000000]...[-]");
      int i = 0;
      while (true)
      {
        yield return (object) new WaitForSeconds(1f);
        ++i;
        if (i > 3)
          i = 0;
        switch (i)
        {
          case 0:
            this.loading.ShowTutorialMsg(text, "[000000]...[-]");
            continue;
          case 1:
            this.loading.ShowTutorialMsg(text, ".[000000]..[-]");
            continue;
          case 2:
            this.loading.ShowTutorialMsg(text, "..[000000].[-]");
            continue;
          default:
            this.loading.ShowTutorialMsg(text, "...");
            continue;
        }
      }
    }
  }

  public static class DEPTH
  {
    public const int SYSTEM = 0;
    public const int BACK_GROUND = 3;
    public const int NPC = 100;
    public const int SECTION_BASE = 1000;
    public const int LOUNGE_ANNOUNCE = 1100;
    public const int BANNER = 2000;
    public const int MAIN_MENU = 3000;
    public const int FADER = 4000;
    public const int SECTION_DIALOG = 5000;
    public const int MAIN_CHAT = 6000;
    public const int MAIN_CHAT_DIALOG = 6200;
    public const int UI_TUTORIAL = 6500;
    public const int FADER_HIGH = 7000;
    public const int SYSTEM_PANEL = 9000;
    public const int TASK_ANNOUNCE = 9050;
    public const int LOADING = 9100;
    public const int ERROR_DIALOG = 9500;
    public const int IMPORTANT = 9999;
  }

  public enum SYSTEM
  {
    FADER,
    BLOCKER,
    DIALOG_BLOCKER,
    RENDER_LIGHT,
    Texture,
    GACHA_RENDER_LIGHT,
  }

  [Flags]
  public enum DISABLE_FACTOR
  {
    INITIALIZE = 1,
    RESET = 2,
    SCENE_CHANGE = 4,
    SCENE_CHANGE_RESERVE = 8,
    AUTO_EVENT = 16, // 0x00000010
    TRANSITION = 32, // 0x00000020
    HOME_CONTROLLER = 64, // 0x00000040
    PROTOCOL = 128, // 0x00000080
    MANUAL_NETWORK = 256, // 0x00000100
    UITWEEN_SMALL = 512, // 0x00000200
    CAMERA_ACTION = 1024, // 0x00000400
    DIRECTION = 2048, // 0x00000800
    NOTIFY = 4096, // 0x00001000
    LOADING = 8192, // 0x00002000
    MOMENT = 16384, // 0x00004000
    FREE_CAMERA = 1073741824, // 0x40000000
    DEBUG = -2147483648, // 0x80000000
  }

  public class AtlasEntry
  {
    public UIAtlas orgAtlas;
    public UIAtlas copyAtlas;
    public List<UISprite> orgSpriteList;

    public AtlasEntry(UIAtlas orgAtlas, UIAtlas copyAtlas)
    {
      this.orgAtlas = orgAtlas;
      this.copyAtlas = copyAtlas;
      this.orgSpriteList = new List<UISprite>();
    }
  }
}
