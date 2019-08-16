using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

	[CompilerGenerated]
	private static Predicate<UIBehaviour> _003C_003Ef__mg_0024cache0;

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
			system.GetCtrl(SYSTEM.RENDER_LIGHT).get_gameObject().SetActive(_enableShadow);
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
			system.GetCtrl(SYSTEM.GACHA_RENDER_LIGHT).get_gameObject().SetActive(_enableGachaLight);
			QualitySettings.SetQualityLevel(_enableGachaLight ? 1 : 0);
		}
	}

	public static UIBehaviour CreatePrefabUI(Object prefab, GameObject inactive_inctance, Type add_component_type, bool initVisible, Transform parent, int depth, GameSceneTables.SectionData section_data)
	{
		if (parent == null && MonoBehaviourSingleton<UIManager>.IsValid())
		{
			parent = MonoBehaviourSingleton<UIManager>.I._transform;
		}
		string text = prefab.get_name();
		if (text.StartsWith("internal__"))
		{
			text = text.Substring(text.LastIndexOf("__") + 2);
		}
		Transform val = Utility.CreateGameObject(text, parent, 5);
		val.get_gameObject().AddComponent<UIPanel>();
		Transform val2 = null;
		val2 = ((!(inactive_inctance != null)) ? ResourceUtility.Realizes(prefab, val, 5) : InstantiateManager.Realizes(ref inactive_inctance, val, 5));
		if (add_component_type == null)
		{
			add_component_type = Type.GetType(val2.get_name());
		}
		UIBehaviour uIBehaviour = (add_component_type == null) ? val.get_gameObject().AddComponent<UIBehaviour>() : (val.get_gameObject().AddComponent(add_component_type) as UIBehaviour);
		uIBehaviour.collectUI = val2;
		uIBehaviour.sectionData = section_data;
		TestTransitionAnim(val2, section_data);
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
			val2.GetComponentsInChildren<UIWidget>(Temporary.uiWidgetList);
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
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		if (!(ui.get_gameObject().GetComponentInChildren<UITransition>() != null))
		{
			UITransition uITransition = ui.get_gameObject().AddComponent<UITransition>();
			float duration = (!MonoBehaviourSingleton<GlobalSettingsManager>.IsValid()) ? 0.25f : MonoBehaviourSingleton<GlobalSettingsManager>.I.defaultUITransitionAnimTime;
			int num = (!need_scale) ? 1 : 2;
			uITransition.openTweens = new UITweener[num];
			TweenAlpha tweenAlpha = (TweenAlpha)(uITransition.openTweens[0] = ui.get_gameObject().AddComponent<TweenAlpha>());
			tweenAlpha.value = 0f;
			tweenAlpha.SetStartToCurrentValue();
			tweenAlpha.to = 1f;
			tweenAlpha.duration = duration;
			tweenAlpha.animationCurve = Curves.easeLinear;
			tweenAlpha.ignoreTimeScale = false;
			if (need_scale)
			{
				TweenScale tweenScale = (TweenScale)(uITransition.openTweens[1] = ui.get_gameObject().AddComponent<TweenScale>());
				tweenScale.value = new Vector3(1.05f, 1.05f, 1f);
				tweenScale.SetStartToCurrentValue();
				tweenScale.to = Vector3.get_one();
				tweenScale.duration = duration;
				tweenScale.animationCurve = Curves.easeIn;
				tweenScale.ignoreTimeScale = false;
			}
			uITransition.closeTweens = new UITweener[num];
			tweenAlpha = (TweenAlpha)(uITransition.closeTweens[0] = ui.get_gameObject().AddComponent<TweenAlpha>());
			tweenAlpha.from = 1f;
			tweenAlpha.to = 0f;
			tweenAlpha.duration = duration;
			tweenAlpha.animationCurve = Curves.easeLinear;
			tweenAlpha.ignoreTimeScale = false;
			if (need_scale)
			{
				TweenScale tweenScale = (TweenScale)(uITransition.closeTweens[1] = ui.get_gameObject().AddComponent<TweenScale>());
				tweenScale.from = Vector3.get_one();
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
		return tutorialMessage != null && tutorialMessage.IsEnableMessage();
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
			if (tutorialMessage != null && tutorialMessage.isErrorResendQuestGacha && flag)
			{
				return true;
			}
		}
		return false;
	}

	protected override void Awake()
	{
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Expected O, but got Unknown
		base.Awake();
		uiRoot = this.GetComponent<UIRoot>();
		UIVirtualScreen.InitUIRoot(uiRoot);
		uiCamera = uiRoot.GetComponentInChildren<Camera>();
		cameras = (Camera[])new Camera[1]
		{
			uiCamera
		};
		nguiCamera = uiCamera.GetComponent<UICamera>();
		uiRootPanel = uiRoot.GetComponent<UIPanel>();
		uiRootTransform = uiRoot.get_transform();
		initUseMouse = nguiCamera.useMouse;
		initUseTouch = nguiCamera.useTouch;
		system = CreatePrefabUI(Resources.Load("UI/SystemUI"), null, null, initVisible: true, base._transform, 0, null);
		system.CreateCtrlsArray(typeof(SYSTEM));
		updateBlockerSize();
		Transform ctrl = system.GetCtrl(SYSTEM.FADER);
		faderPanel = ctrl.get_parent().GetComponent<UIPanel>();
		faderPanel.depth = 4000;
		Vector3 position = ctrl.get_position();
		position.z = -1f;
		ctrl.set_position(position);
		Transform ctrl2 = system.GetCtrl(SYSTEM.BLOCKER);
		ctrl2.get_gameObject().SetActive(false);
		Transform ctrl3 = system.GetCtrl(SYSTEM.DIALOG_BLOCKER);
		dialogBlockerAlpha = ctrl3.GetComponent<UIRect>().alpha;
		dialogBlockerTween = TweenAlpha.Begin(ctrl3.get_gameObject(), 0.2f, dialogBlockerAlpha);
		dialogBlockerTween.value = 0f;
		dialogBlockerTween.from = 0f;
		dialogBlockerTween.set_enabled(false);
		ctrl3.get_gameObject().SetActive(false);
		string text = "InternalUI/UI_Common/LoadingUI";
		SetLoadingUI(Resources.Load(text));
		internalUI = true;
		GameObject val = new GameObject("ButtonEffectTop");
		UIPanel uIPanel = val.AddComponent<UIPanel>();
		uIPanel.depth = 10000;
		buttonEffectTop = val.get_transform();
		buttonEffectTop.SetParent(uiRootTransform);
		buttonEffectTop.set_localPosition(Vector3.get_zero());
		buttonEffectTop.set_localRotation(Quaternion.get_identity());
		buttonEffectTop.set_localScale(Vector3.get_one());
		val.set_layer(uiRoot.get_gameObject().get_layer());
		GameObject val2 = new GameObject("AtlasTop");
		atlasTop = val2.get_transform();
		atlasTop.SetParent(buttonEffectTop);
		val2.SetActive(false);
		UIButtonEffect.CacheShaderPropertyId();
		enableShadow = false;
		enableGachaLight = false;
	}

	public void SetLoadingUI(Object prefab)
	{
		internalUI = false;
		if (loading != null)
		{
			Object.Destroy(loading.get_gameObject());
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
			this.StartCoroutine(DoLoadUI(need_common, need_outgame, need_tutorial, skipChat));
		}
	}

	private IEnumerator DoLoadUI(bool need_common, bool need_outgame, bool need_tutorial, bool skipChat = false)
	{
		isLoading = true;
		LoadingQueue load_queue = new LoadingQueue(this);
		bool need_main_chat = true;
		bool need_banner_view = need_outgame;
		LoadObject lo_common = (!(common == null) || !need_common) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "UI_Common");
		LoadObject lo_main_menu = (!(mainMenu == null) || !need_outgame) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "MainMenu");
		LoadObject lo_main_status = (!(mainStatus == null) || !need_outgame) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "MainStatus");
		LoadObject lo_npc_msg = (!(npcMessage == null) || !need_outgame) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "NPCMessage");
		LoadObject lo_main_chat = (!(mainChat == null) || !need_main_chat) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "MainChat");
		LoadObject lo_banner_view = (!(bannerView == null) || !need_banner_view) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "EventBannerView");
		LoadObject lo_invitation = (!(invitationButton == null) || !need_outgame) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "QuestInvitationButton");
		LoadObject lo_invitation_ingame = (!(invitationInGameButton == null) || !need_outgame) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "QuestInvitationInGameButton");
		LoadObject lo_tutorial = (!(tutorialMessage == null) || !need_tutorial) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "TutorialMessage");
		LoadObject lo_taskAnnounce = (!(taskClearAnnouce == null)) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "TaskClearAnnounce");
		LoadObject lo_loungeAnnoucne = (!(loungeAnnounce == null)) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "LoungeAnnounce");
		LoadObject lo_clanAnnoucne = (!(clanAnnounce == null)) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "LoungeAnnounce");
		LoadObject lo_black_market = (!(blackMarkeButton == null) || !need_outgame) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "BlackMarketButton");
		LoadObject lo_fortune_wheel = (!(fortuneWheelButton == null) || !need_outgame) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "FortuneWheelButton");
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
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
			levelUp = common.get_gameObject().GetComponentInChildren<UILevelUpAnnounce>();
			knockDownRaidBoss = common.get_gameObject().GetComponentInChildren<UIKnockDownRaidBossAnnounce>();
			clanCreate = common.get_gameObject().GetComponentInChildren<UIClanCreateAnnounce>();
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
			Object.DestroyImmediate(mainMenu.get_gameObject());
			mainMenu = null;
		}
		if (bannerView != null)
		{
			Object.DestroyImmediate(bannerView.get_gameObject());
			bannerView = null;
		}
	}

	public void ResetUI()
	{
		if (mainMenu != null)
		{
			mainMenu.get_gameObject().SetActive(false);
			mainMenu.get_gameObject().SetActive(true);
		}
		if (mainStatus != null)
		{
			mainStatus.get_gameObject().SetActive(false);
			mainStatus.get_gameObject().SetActive(true);
		}
		if (npcMessage != null)
		{
			npcMessage.get_gameObject().SetActive(false);
			npcMessage.get_gameObject().SetActive(true);
		}
		if (bannerView != null)
		{
			bannerView.get_gameObject().SetActive(false);
			bannerView.get_gameObject().SetActive(true);
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		UIVirtualScreen.InitUIRoot(uiRoot);
		uiList.ForEach(delegate(UIBehaviour o)
		{
			UIVirtualScreen componentInChildren = o.get_gameObject().GetComponentInChildren<UIVirtualScreen>();
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
		system.GetCtrl(SYSTEM.BLOCKER).get_gameObject().SetActive(disableFlags != (DISABLE_FACTOR)0);
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
		UIBehaviour uIBehaviour = uiList.FindLast((UIBehaviour o) => o.get_name() == name);
		if (uIBehaviour != null)
		{
			return uIBehaviour._transform;
		}
		return Utility.Find(base._transform, name);
	}

	public bool IsTransitioning()
	{
		UIBehaviour uIBehaviour = uiList.FindLast(IsTransitioning);
		if (uIBehaviour != null)
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
		Utility.Attach((!(uiCamera != null)) ? base._transform : uiCamera.get_transform(), obj.get_transform());
	}

	public void UpdateDialogBlocker(GameSectionHierarchy hierarchy, GameSceneTables.SectionData new_section_data)
	{
		updateBlockerSize();
		GameObject blocker = system.GetCtrl(SYSTEM.DIALOG_BLOCKER).get_gameObject();
		int dialogDialogBlockerDepth = hierarchy.GetDialogDialogBlockerDepth(new_section_data);
		int num = 3000;
		if (dialogDialogBlockerDepth > -1)
		{
			blocker.SetActive(true);
			dialogBlockerTween.SetOnFinished((EventDelegate.Callback)null);
			dialogBlockerTween.PlayForward();
			blocker.GetComponent<UIPanel>().depth = dialogDialogBlockerDepth;
		}
		else
		{
			dialogBlockerTween.SetOnFinished(delegate
			{
				blocker.SetActive(false);
			});
			dialogBlockerTween.PlayReverse();
			if (blocker.get_activeSelf())
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
		if (!Input.GetKeyUp(27))
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
					string text = (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "TitleScene")) ? StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 36u) : "You are unable to go back to Town Scene during this Tutorial Mission";
					ToastManager.PushOpen(text);
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
					if (m_friendMsg != null && m_friendMsg.get_gameObject().get_activeSelf())
					{
						m_friendMsg.OnClickCloseButton();
						return;
					}
				}
			}
			if (Object.op_Implicit(mainChat) && mainChat.IsOpeningWindow())
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
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		if (null == sprite || null == sprite.atlas)
		{
			return null;
		}
		AtlasEntry atlasEntry = atlases.Find(delegate(AtlasEntry o)
		{
			if (((object)sprite.atlas).Equals((object)o.orgAtlas))
			{
				return true;
			}
			return false;
		});
		if (atlasEntry != null && (null == atlasEntry.copyAtlas || null == atlasEntry.orgAtlas))
		{
			atlases.Remove(atlasEntry);
			atlasEntry = null;
		}
		if (atlasEntry == null)
		{
			UIAtlas uIAtlas = (!(sprite.atlas.replacement != null)) ? ResourceUtility.Instantiate<UIAtlas>(sprite.atlas) : ResourceUtility.Instantiate<UIAtlas>(sprite.atlas.replacement);
			if (uIAtlas == null || uIAtlas.spriteMaterial == null)
			{
			}
			uIAtlas.spriteMaterial = new Material(uIAtlas.spriteMaterial);
			uIAtlas.spriteMaterial.set_shader(ResourceUtility.FindShader(shader));
			atlasEntry = new AtlasEntry(sprite.atlas, uIAtlas);
			atlases.Add(atlasEntry);
			uIAtlas.set_name("_" + sprite.atlas.get_name());
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
		AtlasEntry atlasEntry = atlases.Find(delegate(AtlasEntry o)
		{
			if (((object)sprite.atlas).Equals((object)o.orgAtlas))
			{
				return true;
			}
			return false;
		});
		if (atlasEntry == null)
		{
			return;
		}
		atlasEntry.orgSpriteList.Remove(sprite);
		if (0 >= atlasEntry.orgSpriteList.Count)
		{
			if (null != atlasEntry.copyAtlas)
			{
				Object.Destroy(atlasEntry.copyAtlas.spriteMaterial);
				Object.Destroy(atlasEntry.copyAtlas.get_gameObject());
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
			atlasEntry.orgSpriteList.RemoveAll(delegate(UISprite o)
			{
				if (null == o)
				{
					return true;
				}
				return false;
			});
			if (0 >= atlasEntry.orgSpriteList.Count)
			{
				if (null != atlasEntry.copyAtlas)
				{
					Object.Destroy(atlasEntry.copyAtlas.spriteMaterial);
					Object.Destroy(atlasEntry.copyAtlas.get_gameObject());
				}
				atlasEntry.copyAtlas = null;
			}
		}
		atlases.RemoveAll(delegate(AtlasEntry o)
		{
			if (null == o.copyAtlas)
			{
				return true;
			}
			return false;
		});
	}

	public void LoadTutorialMessage(Action callback)
	{
		this.StartCoroutine(_LoadTutorialMessage(callback));
	}

	private IEnumerator _LoadTutorialMessage(Action callback)
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_tutorial = (!(tutorialMessage == null)) ? null : load_queue.Load(RESOURCE_CATEGORY.UI, "TutorialMessage");
		if (load_queue.IsLoading())
		{
			yield return load_queue.Wait();
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
		if (Time.get_time() - showGGTutorialMessageTime > waitTIme)
		{
			return true;
		}
		return false;
	}

	public void ShowGGTutorialMessage()
	{
		isShowingGGTutorialMessage = true;
		this.StartCoroutine("ShowGGTutorialMessage_");
	}

	public void HideGGTutorialMessage()
	{
		isShowingGGTutorialMessage = false;
		this.StopCoroutine("ShowGGTutorialMessage_");
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
			string text3 = StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, j);
			string text2;
			switch (i)
			{
			case 0u:
				text2 = "[000000]...[-]";
				break;
			case 1u:
				text2 = ".[000000]..[-]";
				break;
			case 2u:
				text2 = "..[000000].[-]";
				break;
			default:
				text2 = "...";
				break;
			}
			if (!(system != null))
			{
				continue;
			}
			loading.ShowTutorialMsg(text3, text2);
			UIVirtualScreen vscreen = system.GetComponentInChildren<UIVirtualScreen>();
			yield return (object)new WaitForSeconds(2.2f);
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
		this.StartCoroutine("ShowEndGGTutorialMessage_");
	}

	public void HideEndGGTutorialMessage()
	{
		this.StopCoroutine("ShowEndGGTutorialMessage_");
		loading.HideTutorialMsg();
		showGGTutorialMessageTime = 0f;
	}

	private IEnumerator ShowEndGGTutorialMessage_()
	{
		UIVirtualScreen vscreen = loading.GetComponentInChildren<UIVirtualScreen>();
		showGGTutorialMessageTime = Time.get_time();
		yield return (object)new WaitForSeconds(0.2f);
		if (!(vscreen != null))
		{
			yield break;
		}
		loading.ShowTutorialMsg(StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, 3u), string.Empty);
		vscreen.IsOverSafeArea = true;
		yield return (object)new WaitForSeconds(1f);
		vscreen.InitWidget();
		loading.ShowTutorialMsg(StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, 4u), string.Empty);
		yield return (object)new WaitForSeconds(1f);
		string text = StringTable.Get(STRING_CATEGORY.TUTORIAL_LOADING_MSG, 5u);
		loading.ShowTutorialMsg(text, "[000000]...[-]");
		int i = 0;
		while (true)
		{
			yield return (object)new WaitForSeconds(1f);
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
