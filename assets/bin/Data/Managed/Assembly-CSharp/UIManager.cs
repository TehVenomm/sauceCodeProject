using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
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
		GACHA_RENDER_LIGHT
	}

	[Flags]
	public enum DISABLE_FACTOR
	{
		INITIALIZE = 0x1,
		RESET = 0x2,
		SCENE_CHANGE = 0x4,
		SCENE_CHANGE_RESERVE = 0x8,
		AUTO_EVENT = 0x10,
		TRANSITION = 0x20,
		HOME_CONTROLLER = 0x40,
		PROTOCOL = 0x80,
		MANUAL_NETWORK = 0x100,
		UITWEEN_SMALL = 0x200,
		CAMERA_ACTION = 0x400,
		DIRECTION = 0x800,
		NOTIFY = 0x1000,
		LOADING = 0x2000,
		MOMENT = 0x4000,
		FREE_CAMERA = 0x40000000,
		DEBUG = int.MinValue
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
			orgSpriteList = new List<UISprite>();
		}
	}

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

	private List<AtlasEntry> atlases = new List<AtlasEntry>();

	public bool isShowingGGTutorialMessage;

	private float showGGTutorialMessageTime;

	public bool isLoading
	{
		get;
		private set;
	}

	public Camera uiCamera
	{
		get;
		private set;
	}

	public Camera[] cameras
	{
		get;
		private set;
	}

	public UICamera nguiCamera
	{
		get;
		private set;
	}

	public UIRoot uiRoot
	{
		get;
		private set;
	}

	public UIPanel uiRootPanel
	{
		get;
		private set;
	}

	public Transform uiRootTransform
	{
		get;
		private set;
	}

	public UIPanel faderPanel
	{
		get;
		private set;
	}

	public Transform buttonEffectTop
	{
		get;
		private set;
	}

	public Transform atlasTop
	{
		get;
		private set;
	}

	public UIBehaviour system
	{
		get;
		private set;
	}

	public LoadingUI loading
	{
		get;
		private set;
	}

	public MainMenu mainMenu
	{
		get;
		private set;
	}

	public MainStatus mainStatus
	{
		get;
		private set;
	}

	public UI_Common common
	{
		get;
		private set;
	}

	public UILevelUpAnnounce levelUp
	{
		get;
		private set;
	}

	public UIKnockDownRaidBossAnnounce knockDownRaidBoss
	{
		get;
		private set;
	}

	public UIClanCreateAnnounce clanCreate
	{
		get;
		private set;
	}

	public NPCMessage npcMessage
	{
		get;
		private set;
	}

	public MainChat mainChat
	{
		get;
		private set;
	}

	public EventBannerView bannerView
	{
		get;
		private set;
	}

	public QuestInvitationButton invitationButton
	{
		get;
		private set;
	}

	public QuestInvitationInGameButton invitationInGameButton
	{
		get;
		private set;
	}

	public TaskClearAnnounce taskClearAnnouce
	{
		get;
		private set;
	}

	public LoungeAnnounce loungeAnnounce
	{
		get;
		private set;
	}

	public ClanAnnounce clanAnnounce
	{
		get;
		private set;
	}

	public BlackMarketButton blackMarkeButton
	{
		get;
		private set;
	}

	public FortuneWheelButton fortuneWheelButton
	{
		get;
		private set;
	}

	public TutorialMessage tutorialMessage
	{
		get;
		private set;
	}

	public DISABLE_FACTOR disableFlags
	{
		get;
		private set;
	}

	public bool enableShadow
	{
		get
		{
			return _enableShadow;
		}
		set
		{
			_enableShadow = value;
			system.GetCtrl(SYSTEM.RENDER_LIGHT).gameObject.SetActive(_enableShadow);
			QualitySettings.SetQualityLevel(_enableShadow ? 1 : 0);
		}
	}

	public bool enableGachaLight
	{
		get
		{
			return _enableGachaLight;
		}
		set
		{
			_enableGachaLight = value;
			system.GetCtrl(SYSTEM.GACHA_RENDER_LIGHT).gameObject.SetActive(_enableGachaLight);
			QualitySettings.SetQualityLevel(_enableGachaLight ? 1 : 0);
		}
	}

	public static UIBehaviour CreatePrefabUI(UnityEngine.Object prefab, GameObject inactive_inctance, Type add_component_type, bool initVisible, Transform parent, int depth, GameSceneTables.SectionData section_data)
	{
		if (parent == null && MonoBehaviourSingleton<UIManager>.IsValid())
		{
			parent = MonoBehaviourSingleton<UIManager>.I._transform;
		}
		string text = prefab.name;
		if (text.StartsWith("internal__"))
		{
			text = text.Substring(text.LastIndexOf("__") + 2);
		}
		Transform transform = Utility.CreateGameObject(text, parent, 5);
		transform.gameObject.AddComponent<UIPanel>();
		Transform transform2 = null;
		transform2 = ((!(inactive_inctance != null)) ? ResourceUtility.Realizes(prefab, transform, 5) : InstantiateManager.Realizes(ref inactive_inctance, transform, 5));
		if (add_component_type == null)
		{
			add_component_type = Type.GetType(transform2.name);
		}
		UIBehaviour uIBehaviour = (!(add_component_type != null)) ? transform.gameObject.AddComponent<UIBehaviour>() : (transform.gameObject.AddComponent(add_component_type) as UIBehaviour);
		uIBehaviour.collectUI = transform2;
		uIBehaviour.sectionData = section_data;
		TestTransitionAnim(transform2, section_data);
		if (MonoBehaviourSingleton<UIManager>.I.common != null)
		{
			int num = 0;
			string text2 = uIBehaviour.GetCaptionText();
			if (section_data != null)
			{
				if (text2 == null)
				{
					text2 = section_data.GetText("CAPTION");
				}
				num = section_data.backButtonIndex;
			}
			if (num > 0)
			{
				MonoBehaviourSingleton<UIManager>.I.common.AttachBackButton(uIBehaviour, num - 1);
			}
			MonoBehaviourSingleton<UIManager>.I.common.AttachCaption(uIBehaviour, num, text2);
		}
		uIBehaviour.InitUI();
		uIBehaviour.baseDepth = depth;
		if (!initVisible)
		{
			transform2.GetComponentsInChildren(Temporary.uiWidgetList);
			int i = 0;
			for (int count = Temporary.uiWidgetList.Count; i < count; i++)
			{
				Temporary.uiWidgetList[i]._Update();
			}
			Temporary.uiWidgetList.Clear();
		}
		uIBehaviour.uiVisible = initVisible;
		return uIBehaviour;
	}

	private static void TestTransitionAnim(Transform ui, GameSceneTables.SectionData section_data)
	{
		if (!(section_data == null) && section_data.type != 0 && !(section_data.sectionName == "InGameMain"))
		{
			SetDefaultTransitionAnim(ui, section_data.type != GAME_SECTION_TYPE.SCREEN);
		}
	}

	private static void SetDefaultTransitionAnim(Transform ui, bool need_scale)
	{
		if (!(ui.gameObject.GetComponentInChildren<UITransition>() != null))
		{
			UITransition uITransition = ui.gameObject.AddComponent<UITransition>();
			float duration = MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() ? MonoBehaviourSingleton<GlobalSettingsManager>.I.defaultUITransitionAnimTime : 0.25f;
			int num = (!need_scale) ? 1 : 2;
			uITransition.openTweens = new UITweener[num];
			TweenAlpha tweenAlpha = (TweenAlpha)(uITransition.openTweens[0] = ui.gameObject.AddComponent<TweenAlpha>());
			tweenAlpha.value = 0f;
			tweenAlpha.SetStartToCurrentValue();
			tweenAlpha.to = 1f;
			tweenAlpha.duration = duration;
			tweenAlpha.animationCurve = Curves.easeLinear;
			tweenAlpha.ignoreTimeScale = false;
			if (need_scale)
			{
				TweenScale tweenScale = (TweenScale)(uITransition.openTweens[1] = ui.gameObject.AddComponent<TweenScale>());
				tweenScale.value = new Vector3(1.05f, 1.05f, 1f);
				tweenScale.SetStartToCurrentValue();
				tweenScale.to = Vector3.one;
				tweenScale.duration = duration;
				tweenScale.animationCurve = Curves.easeIn;
				tweenScale.ignoreTimeScale = false;
			}
			uITransition.closeTweens = new UITweener[num];
			tweenAlpha = (TweenAlpha)(uITransition.closeTweens[0] = ui.gameObject.AddComponent<TweenAlpha>());
			tweenAlpha.from = 1f;
			tweenAlpha.to = 0f;
			tweenAlpha.duration = duration;
			tweenAlpha.animationCurve = Curves.easeLinear;
			tweenAlpha.ignoreTimeScale = false;
			if (need_scale)
			{
				TweenScale tweenScale = (TweenScale)(uITransition.closeTweens[1] = ui.gameObject.AddComponent<TweenScale>());
				tweenScale.from = Vector3.one;
				tweenScale.to = new Vector3(1.05f, 1.05f, 1f);
				tweenScale.duration = duration;
				tweenScale.animationCurve = Curves.easeIn;
				tweenScale.ignoreTimeScale = false;
			}
			uITransition.InitTweens();
		}
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

	public bool IsEnableTutorialMessage()
	{
		if (tutorialMessage != null)
		{
			return tutorialMessage.IsEnableMessage();
		}
		return false;
	}

	public bool IsTutorialErrorResend()
	{
		if (tutorialMessage != null && tutorialMessage.isErrorResend)
		{
			return true;
		}
		string currentSceneName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName();
		string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
		if (!string.IsNullOrEmpty(currentSceneName) && !string.IsNullOrEmpty(currentSectionName))
		{
			bool flag = false;
			if (currentSceneName != "InGameScene")
			{
				flag = (currentSceneName == "ShopScene" || currentSceneName == "GachaScene" || currentSectionName.Contains("QuestAccept"));
			}
			if ((tutorialMessage != null && tutorialMessage.isErrorResendQuestGacha) & flag)
			{
				return true;
			}
		}
		return false;
	}

	protected override void Awake()
	{
		base.Awake();
		uiRoot = GetComponent<UIRoot>();
		UIVirtualScreen.InitUIRoot(uiRoot);
		uiCamera = uiRoot.GetComponentInChildren<Camera>();
		cameras = new Camera[1]
		{
			uiCamera
		};
		nguiCamera = uiCamera.GetComponent<UICamera>();
		uiRootPanel = uiRoot.GetComponent<UIPanel>();
		uiRootTransform = uiRoot.transform;
		initUseMouse = nguiCamera.useMouse;
		initUseTouch = nguiCamera.useTouch;
		system = CreatePrefabUI(Resources.Load("UI/SystemUI"), null, null, initVisible: true, base._transform, 0, null);
		system.CreateCtrlsArray(typeof(SYSTEM));
		updateBlockerSize();
		Transform ctrl = system.GetCtrl(SYSTEM.FADER);
		faderPanel = ctrl.parent.GetComponent<UIPanel>();
		faderPanel.depth = 4000;
		Vector3 position = ctrl.position;
		position.z = -1f;
		ctrl.position = position;
		system.GetCtrl(SYSTEM.BLOCKER).gameObject.SetActive(value: false);
		Transform ctrl2 = system.GetCtrl(SYSTEM.DIALOG_BLOCKER);
		dialogBlockerAlpha = ctrl2.GetComponent<UIRect>().alpha;
		dialogBlockerTween = TweenAlpha.Begin(ctrl2.gameObject, 0.2f, dialogBlockerAlpha);
		dialogBlockerTween.value = 0f;
		dialogBlockerTween.from = 0f;
		dialogBlockerTween.enabled = false;
		ctrl2.gameObject.SetActive(value: false);
		string path = "InternalUI/UI_Common/LoadingUI";
		SetLoadingUI(Resources.Load(path));
		internalUI = true;
		GameObject gameObject = new GameObject("ButtonEffectTop");
		gameObject.AddComponent<UIPanel>().depth = 10000;
		buttonEffectTop = gameObject.transform;
		buttonEffectTop.SetParent(uiRootTransform);
		buttonEffectTop.localPosition = Vector3.zero;
		buttonEffectTop.localRotation = Quaternion.identity;
		buttonEffectTop.localScale = Vector3.one;
		gameObject.layer = uiRoot.gameObject.layer;
		GameObject gameObject2 = new GameObject("AtlasTop");
		atlasTop = gameObject2.transform;
		atlasTop.SetParent(buttonEffectTop);
		gameObject2.SetActive(value: false);
		UIButtonEffect.CacheShaderPropertyId();
		enableShadow = false;
		enableGachaLight = false;
	}

	public void SetLoadingUI(UnityEngine.Object prefab)
	{
		internalUI = false;
		if (loading != null)
		{
			UnityEngine.Object.Destroy(loading.gameObject);
			loading = null;
		}
		loading = (CreatePrefabUI(prefab, null, null, initVisible: true, base._transform, 9100, null) as LoadingUI);
		updateBlockerSize();
		loading.UpdateUI();
	}

	private void OnEnable()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
	}

	protected override void OnDisable()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
		base.OnDisable();
	}

	public void LoadUI(bool need_common, bool need_outgame, bool need_tutorial, bool skipChat = false)
	{
		if (!internalUI && !isLoading && (need_common || need_outgame || need_tutorial))
		{
			StartCoroutine(DoLoadUI(need_common, need_outgame, need_tutorial, skipChat));
		}
	}

	private IEnumerator DoLoadUI(bool need_common, bool need_outgame, bool need_tutorial, bool skipChat = false)
	{
		isLoading = true;
		LoadingQueue loadingQueue = new LoadingQueue(this);
		bool flag = true;
		bool flag2 = need_outgame;
		LoadObject lo_common = (common == null && need_common) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "UI_Common") : null;
		LoadObject lo_main_menu = (mainMenu == null && need_outgame) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "MainMenu") : null;
		LoadObject lo_main_status = (mainStatus == null && need_outgame) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "MainStatus") : null;
		LoadObject lo_npc_msg = (npcMessage == null && need_outgame) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "NPCMessage") : null;
		LoadObject lo_main_chat = (mainChat == null && flag) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "MainChat") : null;
		LoadObject lo_banner_view = (bannerView == null && flag2) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "EventBannerView") : null;
		LoadObject lo_invitation = (invitationButton == null && need_outgame) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "QuestInvitationButton") : null;
		LoadObject lo_invitation_ingame = (invitationInGameButton == null && need_outgame) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "QuestInvitationInGameButton") : null;
		LoadObject lo_tutorial = (tutorialMessage == null && need_tutorial) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "TutorialMessage") : null;
		LoadObject lo_taskAnnounce = (taskClearAnnouce == null) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "TaskClearAnnounce") : null;
		LoadObject lo_loungeAnnoucne = (loungeAnnounce == null) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "LoungeAnnounce") : null;
		LoadObject lo_clanAnnoucne = (clanAnnounce == null) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "LoungeAnnounce") : null;
		LoadObject lo_black_market = (blackMarkeButton == null && need_outgame) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "BlackMarketButton") : null;
		LoadObject lo_fortune_wheel = (fortuneWheelButton == null && need_outgame) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "FortuneWheelButton") : null;
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		if (lo_main_menu != null)
		{
			mainMenu = (CreatePrefabUI(lo_main_menu.loadedObject, null, null, initVisible: false, base._transform, 3000, null) as MainMenu);
		}
		if (lo_main_status != null)
		{
			mainStatus = (CreatePrefabUI(lo_main_status.loadedObject, null, null, initVisible: false, base._transform, 3000, null) as MainStatus);
		}
		if (lo_common != null)
		{
			common = (CreatePrefabUI(lo_common.loadedObject, null, null, initVisible: false, base._transform, 3000, null) as UI_Common);
			common.Open();
			levelUp = common.gameObject.GetComponentInChildren<UILevelUpAnnounce>();
			knockDownRaidBoss = common.gameObject.GetComponentInChildren<UIKnockDownRaidBossAnnounce>();
			clanCreate = common.gameObject.GetComponentInChildren<UIClanCreateAnnounce>();
		}
		if (lo_npc_msg != null)
		{
			npcMessage = (CreatePrefabUI(lo_npc_msg.loadedObject, null, null, initVisible: false, base._transform, 100, null) as NPCMessage);
		}
		if (lo_main_chat != null && !skipChat)
		{
			mainChat = (CreatePrefabUI(lo_main_chat.loadedObject, null, null, initVisible: false, base._transform, 6000, null) as MainChat);
		}
		if (lo_banner_view != null)
		{
			bannerView = (CreatePrefabUI(lo_banner_view.loadedObject, null, null, initVisible: false, base._transform, 2000, null) as EventBannerView);
		}
		if (lo_invitation != null)
		{
			invitationButton = (CreatePrefabUI(lo_invitation.loadedObject, null, null, initVisible: false, base._transform, 3000, null) as QuestInvitationButton);
		}
		if (lo_invitation_ingame != null)
		{
			invitationInGameButton = (CreatePrefabUI(lo_invitation_ingame.loadedObject, null, null, initVisible: false, base._transform, 1000, null) as QuestInvitationInGameButton);
		}
		if (lo_tutorial != null)
		{
			tutorialMessage = (CreatePrefabUI(lo_tutorial.loadedObject, null, null, initVisible: false, base._transform, 6500, null) as TutorialMessage);
		}
		if (lo_taskAnnounce != null)
		{
			taskClearAnnouce = (CreatePrefabUI(lo_taskAnnounce.loadedObject, null, typeof(TaskClearAnnounce), initVisible: true, base._transform, 9050, null) as TaskClearAnnounce);
		}
		if (lo_loungeAnnoucne != null)
		{
			loungeAnnounce = (CreatePrefabUI(lo_loungeAnnoucne.loadedObject, null, typeof(LoungeAnnounce), initVisible: true, base._transform, 1100, null) as LoungeAnnounce);
		}
		if (lo_clanAnnoucne != null)
		{
			clanAnnounce = (CreatePrefabUI(lo_clanAnnoucne.loadedObject, null, typeof(ClanAnnounce), initVisible: true, base._transform, 1100, null) as ClanAnnounce);
		}
		if (lo_black_market != null)
		{
			blackMarkeButton = (CreatePrefabUI(lo_black_market.loadedObject, null, null, initVisible: false, base._transform, 3000, null) as BlackMarketButton);
		}
		if (lo_fortune_wheel != null)
		{
			fortuneWheelButton = (CreatePrefabUI(lo_fortune_wheel.loadedObject, null, null, initVisible: false, base._transform, 3000, null) as FortuneWheelButton);
		}
		isLoading = false;
	}

	public void DeleteUI()
	{
		if (mainMenu != null)
		{
			UnityEngine.Object.DestroyImmediate(mainMenu.gameObject);
			mainMenu = null;
		}
		if (bannerView != null)
		{
			UnityEngine.Object.DestroyImmediate(bannerView.gameObject);
			bannerView = null;
		}
	}

	public void ResetUI()
	{
		if (mainMenu != null)
		{
			mainMenu.gameObject.SetActive(value: false);
			mainMenu.gameObject.SetActive(value: true);
		}
		if (mainStatus != null)
		{
			mainStatus.gameObject.SetActive(value: false);
			mainStatus.gameObject.SetActive(value: true);
		}
		if (npcMessage != null)
		{
			npcMessage.gameObject.SetActive(value: false);
			npcMessage.gameObject.SetActive(value: true);
		}
		if (bannerView != null)
		{
			bannerView.gameObject.SetActive(value: false);
			bannerView.gameObject.SetActive(value: true);
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		UIVirtualScreen.InitUIRoot(uiRoot);
		uiList.ForEach(delegate(UIBehaviour o)
		{
			UIVirtualScreen componentInChildren = o.gameObject.GetComponentInChildren<UIVirtualScreen>();
			if (componentInChildren != null)
			{
				componentInChildren.InitWidget();
			}
		});
	}

	public void SetDisable(DISABLE_FACTOR factor, bool is_disable)
	{
		if (is_disable)
		{
			disableFlags |= factor;
		}
		else
		{
			disableFlags &= ~factor;
		}
		updateBlockerSize();
		system.GetCtrl(SYSTEM.BLOCKER).gameObject.SetActive(disableFlags != (DISABLE_FACTOR)0);
		loading.UpdateUIDisableFactor(disableFlags);
	}

	public void SetDisableMoment()
	{
		SetDisable(DISABLE_FACTOR.MOMENT, is_disable: true);
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			SetDisable(DISABLE_FACTOR.MOMENT, is_disable: false);
		});
	}

	public bool IsDisable()
	{
		return disableFlags != (DISABLE_FACTOR)0;
	}

	public void SetEnableUIInput(bool is_enable)
	{
		if (enableUIInput == is_enable)
		{
			return;
		}
		enableUIInput = is_enable;
		if (is_enable)
		{
			nguiCamera.useTouch = initUseTouch;
			nguiCamera.useMouse = initUseMouse;
			return;
		}
		nguiCamera.useTouch = false;
		nguiCamera.useMouse = false;
		if (nguiCamera.allowMultiTouch || UICamera.CountInputSources() != 1)
		{
			return;
		}
		UICamera.MouseOrTouch touch = UICamera.GetTouch(1);
		if (touch != null && touch.pressed != null)
		{
			UIButton component = touch.pressed.GetComponent<UIButton>();
			if (component != null)
			{
				component.SetState(UIButtonColor.State.Normal, immediate: true);
			}
			UIScrollView componentInParent = touch.pressed.GetComponentInParent<UIScrollView>();
			if (componentInParent != null)
			{
				componentInParent.Press(pressed: false);
			}
			touch.pressed = null;
		}
		if (UICamera.currentTouch != null)
		{
			nguiCamera.ProcessTouch(pressed: false, released: true);
		}
	}

	public bool IsEnableUIInput()
	{
		return enableUIInput;
	}

	public Transform Find(string name)
	{
		UIBehaviour uIBehaviour = uiList.FindLast((UIBehaviour o) => o.name == name);
		if (uIBehaviour != null)
		{
			return uIBehaviour._transform;
		}
		return Utility.Find(base._transform, name);
	}

	public bool IsTransitioning()
	{
		if (uiList.FindLast((UIBehaviour o) => IsTransitioning(o)) != null)
		{
			return true;
		}
		return false;
	}

	public bool IsTransitioningMainUI()
	{
		if (IsTransitioning(mainMenu))
		{
			return true;
		}
		if (IsTransitioning(mainStatus))
		{
			return true;
		}
		if (IsTransitioning(mainChat))
		{
			return true;
		}
		if (IsTransitioning(bannerView))
		{
			return true;
		}
		return false;
	}

	private static bool IsTransitioning(UIBehaviour ui)
	{
		if (ui == null)
		{
			return false;
		}
		return ui.IsTransitioning();
	}

	public void AttachScene(GameObject obj, int index = 0)
	{
		Utility.Attach((uiCamera != null) ? uiCamera.transform : base._transform, obj.transform);
	}

	public void UpdateDialogBlocker(GameSectionHierarchy hierarchy, GameSceneTables.SectionData new_section_data)
	{
		if (new_section_data != null && MonoBehaviourSingleton<GoGameSettingsManager>.IsValid() && MonoBehaviourSingleton<GoGameSettingsManager>.I.PreventUpdateDialogBlocker(new_section_data.sectionName))
		{
			return;
		}
		updateBlockerSize();
		GameObject blocker = system.GetCtrl(SYSTEM.DIALOG_BLOCKER).gameObject;
		int dialogDialogBlockerDepth = hierarchy.GetDialogDialogBlockerDepth(new_section_data);
		int num = 3000;
		if (dialogDialogBlockerDepth > -1)
		{
			blocker.SetActive(value: true);
			dialogBlockerTween.SetOnFinished((EventDelegate.Callback)null);
			dialogBlockerTween.PlayForward();
			blocker.GetComponent<UIPanel>().depth = dialogDialogBlockerDepth;
		}
		else
		{
			dialogBlockerTween.SetOnFinished(delegate
			{
				blocker.SetActive(value: false);
			});
			dialogBlockerTween.PlayReverse();
			if (blocker.activeSelf)
			{
				num = -1;
			}
		}
		if (num > -1)
		{
			if (mainMenu != null)
			{
				mainMenu.baseDepth = num;
			}
			if (mainStatus != null)
			{
				mainStatus.baseDepth = num;
			}
		}
	}

	public void OnNotify(GameSection.NOTIFY_FLAG notify_flags)
	{
		int i = 0;
		for (int count = uiList.Count; i < count; i++)
		{
			UIBehaviour uIBehaviour = uiList[i];
			if (!(uIBehaviour is GameSection))
			{
				uIBehaviour.OnNotify(notify_flags);
			}
		}
	}

	public void UpdateMainUI()
	{
		UpdateMainUI(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName(), MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
	}

	public void UpdateMainUI(string scene_name, string section_name)
	{
		bool flag = GameSceneGlobalSettings.IsDisplayMainUI(scene_name, section_name);
		if (mainMenu != null)
		{
			if (flag)
			{
				mainMenu.Open();
			}
			else
			{
				mainMenu.Close();
			}
		}
		bool flag2 = GameSceneGlobalSettings.IsDisplayMainStatusUI(scene_name, section_name);
		if (mainStatus != null)
		{
			if (flag2)
			{
				mainStatus.Open();
			}
			else
			{
				mainStatus.Close();
			}
		}
		GCAtlas();
	}

	private void Update()
	{
		if (!Input.GetKeyUp(KeyCode.Escape))
		{
			return;
		}
		if ((!string.IsNullOrEmpty(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialBit) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM)) || !TutorialStep.HasAllTutorialCompleted())
		{
			if (!MonoBehaviourSingleton<GameSceneManager>.IsValid())
			{
				return;
			}
			string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
			if (!string.IsNullOrEmpty(currentSectionName))
			{
				if (currentSectionName == "Opening" || currentSectionName == "AccountTop" || currentSectionName == "AccountLoginMail" || currentSectionName == "TitleClearCacheConfirm" || currentSectionName == "AccountContact" || currentSectionName == "OpeningSkipConfirm" || currentSectionName == "CommonDialogError" || currentSectionName == "TermsInfo")
				{
					ProcessBackKey();
				}
				else if (MonoBehaviourSingleton<ToastManager>.IsValid() && !MonoBehaviourSingleton<ToastManager>.I.IsShowingDialog())
				{
					ToastManager.PushOpen((MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "TitleScene") ? "You are unable to go back to Town Scene during this Tutorial Mission" : StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 36u));
				}
			}
		}
		else if (IsEnableTutorialMessage())
		{
			if (tutorialMessage.IsOnlyShowImage() && TutorialMessage.GetCursor() == null)
			{
				tutorialMessage.TutorialClose();
			}
		}
		else if ((!(tutorialMessage != null) || !(TutorialMessage.GetCursor() != null)) && (!MenuReset.needClearCache || !MenuReset.needPredownload))
		{
			ProcessBackKey();
		}
	}

	private void ProcessBackKey()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.IsValid() || !MonoBehaviourSingleton<GameSceneManager>.I.IsBackKeyEventExecutionPossible())
		{
			return;
		}
		ChatState_PersonalMsgView chatState_PersonalMsgView = null;
		if (mainChat != null)
		{
			if (mainChat.StateMachine != null && mainChat.StateMachine.CurrentState != null)
			{
				chatState_PersonalMsgView = (mainChat.StateMachine.CurrentState as ChatState_PersonalMsgView);
				if (chatState_PersonalMsgView != null && chatState_PersonalMsgView.M_friendMsg != null)
				{
					FriendMessageUIController m_friendMsg = chatState_PersonalMsgView.M_friendMsg;
					if (m_friendMsg != null && m_friendMsg.gameObject.activeSelf)
					{
						m_friendMsg.OnClickCloseButton();
						return;
					}
				}
			}
			if ((bool)mainChat && mainChat.IsOpeningWindow())
			{
				mainChat.OnPressBackKey();
				return;
			}
		}
		GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
		if (!(currentSection != null) || !(currentSection.collectUI != null))
		{
			return;
		}
		if (currentSection.useOnPressBackKey)
		{
			currentSection.OnPressBackKey();
			return;
		}
		string b = "[BACK]";
		if (!string.IsNullOrEmpty(currentSection.overrideBackKeyEvent))
		{
			b = currentSection.overrideBackKeyEvent;
		}
		UIGameSceneEventSender[] componentsInChildren = currentSection.collectUI.GetComponentsInChildren<UIGameSceneEventSender>();
		int num = 0;
		int num2 = componentsInChildren.Length;
		while (true)
		{
			if (num < num2)
			{
				if (componentsInChildren[num].eventName == b)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		componentsInChildren[num]._SendEvent();
	}

	public AtlasEntry ReplaceAtlas(UISprite sprite, string shader)
	{
		if (null == sprite || null == sprite.atlas)
		{
			return null;
		}
		AtlasEntry atlasEntry = atlases.Find((AtlasEntry o) => sprite.atlas.Equals(o.orgAtlas) ? true : false);
		if (atlasEntry != null && (null == atlasEntry.copyAtlas || null == atlasEntry.orgAtlas))
		{
			atlases.Remove(atlasEntry);
			atlasEntry = null;
		}
		if (atlasEntry == null)
		{
			UIAtlas uIAtlas = (!(sprite.atlas.replacement != null)) ? ResourceUtility.Instantiate(sprite.atlas) : ResourceUtility.Instantiate(sprite.atlas.replacement);
			if (!(uIAtlas == null))
			{
				_ = (uIAtlas.spriteMaterial == null);
			}
			uIAtlas.spriteMaterial = new Material(uIAtlas.spriteMaterial);
			uIAtlas.spriteMaterial.shader = ResourceUtility.FindShader(shader);
			atlasEntry = new AtlasEntry(sprite.atlas, uIAtlas);
			atlases.Add(atlasEntry);
			uIAtlas.name = "_" + sprite.atlas.name;
		}
		atlasEntry.orgSpriteList.Add(sprite);
		sprite.atlas = atlasEntry.copyAtlas;
		return atlasEntry;
	}

	public void ReleaseAtlas(UISprite sprite)
	{
		if (null == sprite || null == sprite.atlas)
		{
			return;
		}
		AtlasEntry atlasEntry = atlases.Find((AtlasEntry o) => sprite.atlas.Equals(o.orgAtlas) ? true : false);
		if (atlasEntry == null)
		{
			return;
		}
		atlasEntry.orgSpriteList.Remove(sprite);
		if (0 >= atlasEntry.orgSpriteList.Count)
		{
			if (null != atlasEntry.copyAtlas)
			{
				UnityEngine.Object.Destroy(atlasEntry.copyAtlas.spriteMaterial);
				UnityEngine.Object.Destroy(atlasEntry.copyAtlas.gameObject);
			}
			atlases.Remove(atlasEntry);
		}
	}

	public void GCAtlas()
	{
		int count = atlases.Count;
		for (int i = 0; i < count; i++)
		{
			AtlasEntry atlasEntry = atlases[i];
			atlasEntry.orgSpriteList.RemoveAll((UISprite o) => (null == o) ? true : false);
			if (0 >= atlasEntry.orgSpriteList.Count)
			{
				if (null != atlasEntry.copyAtlas)
				{
					UnityEngine.Object.Destroy(atlasEntry.copyAtlas.spriteMaterial);
					UnityEngine.Object.Destroy(atlasEntry.copyAtlas.gameObject);
				}
				atlasEntry.copyAtlas = null;
			}
		}
		atlases.RemoveAll((AtlasEntry o) => (null == o.copyAtlas) ? true : false);
	}

	public void LoadTutorialMessage(Action callback)
	{
		StartCoroutine(_LoadTutorialMessage(callback));
	}

	private IEnumerator _LoadTutorialMessage(Action callback)
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_tutorial = (tutorialMessage == null) ? loadingQueue.Load(RESOURCE_CATEGORY.UI, "TutorialMessage") : null;
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		if (lo_tutorial != null)
		{
			tutorialMessage = (CreatePrefabUI(lo_tutorial.loadedObject, null, null, initVisible: false, base._transform, 6500, null) as TutorialMessage);
		}
		callback();
	}

	private void updateBlockerSize()
	{
		if (system != null)
		{
			UIVirtualScreen componentInChildren = system.GetComponentInChildren<UIVirtualScreen>();
			if (componentInChildren != null)
			{
				componentInChildren.IsOverSafeArea = true;
				componentInChildren.InitWidget();
			}
		}
		if (loading != null)
		{
			UIVirtualScreen componentInChildren2 = loading.GetComponentInChildren<UIVirtualScreen>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.IsOverSafeArea = true;
				componentInChildren2.InitWidget();
			}
		}
	}

	public bool canHideGGTutorialMessage(float waitTIme)
	{
		if (Time.time - showGGTutorialMessageTime > waitTIme)
		{
			return true;
		}
		return false;
	}

	public void ShowGGTutorialMessage()
	{
		isShowingGGTutorialMessage = true;
		StartCoroutine("ShowGGTutorialMessage_");
	}

	public void HideGGTutorialMessage()
	{
		isShowingGGTutorialMessage = false;
		StopCoroutine("ShowGGTutorialMessage_");
		loading.HideTutorialMsg();
		showGGTutorialMessageTime = 0f;
	}

	private IEnumerator ShowGGTutorialMessage_()
	{
		uint j = 0u;
		uint i = 0u;
		uint tutLen = (uint)StringTable.GetAllInCategory(STRING_CATEGORY.TUTORIAL_LOADING_MSG).Length;
		while (true)
		{
			string msg = StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, j);
			string endTxt;
			switch (i)
			{
			case 0u:
				endTxt = "[000000]...[-]";
				break;
			case 1u:
				endTxt = ".[000000]..[-]";
				break;
			case 2u:
				endTxt = "..[000000].[-]";
				break;
			default:
				endTxt = "...";
				break;
			}
			if (!(system != null))
			{
				continue;
			}
			loading.ShowTutorialMsg(msg, endTxt);
			UIVirtualScreen vscreen = system.GetComponentInChildren<UIVirtualScreen>();
			yield return new WaitForSeconds(2.2f);
			if (vscreen != null)
			{
				j++;
				i++;
				vscreen.IsOverSafeArea = true;
				if (j >= tutLen - 1)
				{
					j = tutLen - 1;
				}
				vscreen.InitWidget();
				if (i > 3)
				{
					i = 0u;
				}
			}
		}
	}

	public void ShowEndGGTutorialMessage()
	{
		StartCoroutine("ShowEndGGTutorialMessage_");
	}

	public void HideEndGGTutorialMessage()
	{
		StopCoroutine("ShowEndGGTutorialMessage_");
		loading.HideTutorialMsg();
		showGGTutorialMessageTime = 0f;
	}

	private IEnumerator ShowEndGGTutorialMessage_()
	{
		UIVirtualScreen vscreen = loading.GetComponentInChildren<UIVirtualScreen>();
		showGGTutorialMessageTime = Time.time;
		yield return new WaitForSeconds(0.2f);
		if (!(vscreen != null))
		{
			yield break;
		}
		loading.ShowTutorialMsg(StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, 3u), string.Empty);
		vscreen.IsOverSafeArea = true;
		yield return new WaitForSeconds(1f);
		vscreen.InitWidget();
		loading.ShowTutorialMsg(StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, 4u), string.Empty);
		yield return new WaitForSeconds(1f);
		string text = StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, 5u);
		loading.ShowTutorialMsg(text, "[000000]...[-]");
		int i = 0;
		while (true)
		{
			yield return new WaitForSeconds(1f);
			i++;
			if (i > 3)
			{
				i = 0;
			}
			switch (i)
			{
			case 0:
				loading.ShowTutorialMsg(text, "[000000]...[-]");
				break;
			case 1:
				loading.ShowTutorialMsg(text, ".[000000]..[-]");
				break;
			case 2:
				loading.ShowTutorialMsg(text, "..[000000].[-]");
				break;
			default:
				loading.ShowTutorialMsg(text, "...");
				break;
			}
		}
	}
}
