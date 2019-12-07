using Network;
using System.Collections;
using UnityEngine;

public class MainMenu : UIBehaviour
{
	private enum UI
	{
		BTN_SHOP,
		OBJ_ANCHOR_MENU,
		OBJ_ANCHOR_POP_MENU,
		SPR_POP_MENU_ACTIVE,
		_SPR_HOME_ACTIVE,
		_SPR_HOME_INACTIVE,
		_SPR_QUEST_ACTIVE,
		_SPR_QUEST_INACTIVE,
		_SPR_SHOP_ACTIVE,
		_SPR_SHOP_INACTIVE,
		_SPR_STUDIO_ACTIVE,
		_SPR_STUDIO_INACTIVE,
		BTN_POP_MENU,
		TWN_POP_MENU,
		TGL_POP_MENU,
		SPR_HOME_ACTIVE_DECORATION,
		SPR_QUEST_ACTIVE_DECORATION,
		SPR_SHOP_ACTIVE_DECORATION,
		SPR_STUDIO_ACTIVE_DECORATION,
		SPR_GATHER_ACTIVE_DECORATION,
		OBJ_GACHA_DECO_ROOT,
		TEX_GACHA_DECO_IMAGE,
		SPR_GACHA_DECO_NEW,
		SPR_SPECIAL_OFFER,
		SPR_SPECIAL_OFFER_LEFT,
		_SPR_LOUNGE_ACTIVE,
		_SPR_LOUNGE_INACTIVE,
		SPR_LOUNGE_ACTIVE_DECORATION,
		BTN_LOUNGE,
		_SPR_CLAN_ACTIVE,
		_SPR_CLAN_INACTIVE,
		SPR_CLAN_ACTIVE_DECORATION,
		BTN_CLAN,
		SCR_MENU,
		SPR_NEW_MAP
	}

	private string updateSceneButton;

	private EventDelegate _delegate;

	private GachaDeco gachaDecoInfo;

	private Transform homeButton;

	private const string HomeButtonName = "BtnHome";

	private bool isPopMenu = true;

	private MAIN_SCENE nowScene = MAIN_SCENE.MAX;

	private SpanTimer mapCheckSpan;

	public Transform activeSceneButton
	{
		get;
		private set;
	}

	private void Start()
	{
		Transform ctrl = GetCtrl(UI.SCR_MENU);
		homeButton = Utility.FindChild(ctrl, "BtnHome");
		SetActive(UI.SPR_NEW_MAP, is_visible: false);
		mapCheckSpan = new SpanTimer(2f);
	}

	private void POP_MENU()
	{
		if (!IsTransitioning())
		{
			if (!isPopMenu)
			{
				ResetTween(UI.TWN_POP_MENU);
			}
			isPopMenu = !isPopMenu;
			if (TutorialStep.HasAllTutorialCompleted())
			{
				PlayerPrefs.SetInt("IS_POP_FOOTER_MENU", isPopMenu ? 1 : 0);
			}
			PlayTween(UI.TWN_POP_MENU, isPopMenu);
			RefreshUI();
		}
	}

	public override void OnNotify(GameSection.NOTIFY_FLAG flags)
	{
		if (TutorialStep.HasFirstDeliveryCompleted())
		{
			if ((flags & GameSection.NOTIFY_FLAG.CHANGED_SCENE) != (GameSection.NOTIFY_FLAG)0L)
			{
				UpdateMainMenu();
			}
			base.OnNotify(flags);
		}
	}

	public void UpdateMainMenu()
	{
		string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
		MAIN_SCENE mAIN_SCENE = GameDefine.SceneNameToEnum(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName());
		if ((mAIN_SCENE != nowScene && mAIN_SCENE != MAIN_SCENE.MAX) || (nowScene == MAIN_SCENE.HOME && currentSectionName == "StoryMain"))
		{
			if (mAIN_SCENE == MAIN_SCENE.HOME || mAIN_SCENE == MAIN_SCENE.LOUNGE || mAIN_SCENE == MAIN_SCENE.CLAN)
			{
				if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
				{
					ResetTween(UI.TWN_POP_MENU);
					isPopMenu = true;
					SkipTween(UI.TWN_POP_MENU);
				}
				else if (!TutorialStep.HasAllTutorialCompleted() || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
				{
					ResetTween(UI.TWN_POP_MENU);
					isPopMenu = false;
					SkipTween(UI.TWN_POP_MENU, forward: false);
				}
				else
				{
					ResetTween(UI.TWN_POP_MENU);
					isPopMenu = (PlayerPrefs.GetInt("IS_POP_FOOTER_MENU", 0) == 1);
					SkipTween(UI.TWN_POP_MENU, isPopMenu);
				}
			}
			else
			{
				ResetTween(UI.TWN_POP_MENU);
				isPopMenu = true;
				SkipTween(UI.TWN_POP_MENU);
			}
			if (_delegate == null)
			{
				_delegate = new EventDelegate(POP_MENU);
				UIButton component = GetComponent<UIButton>(UI.BTN_POP_MENU);
				if (!component.onClick.Contains(_delegate))
				{
					component.onClick.Add(_delegate);
				}
			}
			UpdateUI();
			nowScene = mAIN_SCENE;
		}
		else
		{
			ResetTween(UI.TWN_POP_MENU);
			isPopMenu = true;
			SkipTween(UI.TWN_POP_MENU);
			UpdateNewMapUI();
		}
	}

	private void UpdateNewMapUI()
	{
		bool flag = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "ClanTop";
		if (isPopMenu && flag)
		{
			if (MonoBehaviourSingleton<WorldMapManager>.I.releasedRegionIds == null)
			{
				return;
			}
			Transform ctrl = GetCtrl(UI.SPR_NEW_MAP);
			bool activeSelf = ctrl.gameObject.activeSelf;
			MonoBehaviourSingleton<WorldMapManager>.I.ExistRegionDirection();
			if (!activeSelf)
			{
				TweenAlpha component = ctrl.GetComponent<TweenAlpha>();
				component.ResetToBeginning();
				component.PlayForward();
			}
		}
		else
		{
			SetActive(UI.SPR_NEW_MAP, is_visible: false);
		}
		if (mapCheckSpan != null)
		{
			mapCheckSpan.ResetNextTime();
		}
	}

	protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SMITH_BADGE;
	}

	public override void UpdateUI()
	{
		if (base.uiFirstUpdate)
		{
			SetActive(UI.OBJ_GACHA_DECO_ROOT, is_visible: false);
		}
		if (homeButton != null)
		{
			homeButton.gameObject.SetActive(value: false);
		}
		SetActive(UI.BTN_LOUNGE, is_visible: false);
		SetActive(UI.BTN_CLAN, is_visible: false);
		if (LoungeMatchingManager.IsValidInLounge())
		{
			SetActive(UI.BTN_LOUNGE, is_visible: true);
		}
		else if (ClanMatchingManager.IsValidInClan())
		{
			SetActive(UI.BTN_CLAN, is_visible: true);
		}
		else if (homeButton != null)
		{
			homeButton.gameObject.SetActive(value: true);
		}
		SetToggle(UI.TGL_POP_MENU, isPopMenu);
		SetColor(UI.OBJ_ANCHOR_MENU, Color.clear);
		SetColor(UI.OBJ_ANCHOR_POP_MENU, Color.white);
		UpdateSceneButtons(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName());
		int badgeTotalNum = MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum();
		if (isPopMenu)
		{
			SetActive(base.collectUI, is_visible: false);
			SetActive(base.collectUI, is_visible: true);
			SetBadge(UI._SPR_STUDIO_ACTIVE, badgeTotalNum, SpriteAlignment.TopLeft, 8, -8, is_scale_normalize: true);
			SetBadge(UI._SPR_STUDIO_INACTIVE, badgeTotalNum, SpriteAlignment.TopLeft, 8, -8, is_scale_normalize: true);
			SetBadge(UI.SPR_POP_MENU_ACTIVE, 0, SpriteAlignment.TopLeft);
			if (MonoBehaviourSingleton<UserInfoManager>.I.needShowOneTimesOfferSS)
			{
				SetActive(UI.SPR_SPECIAL_OFFER, is_visible: true);
				SetActive(UI.SPR_SPECIAL_OFFER_LEFT, is_visible: false);
			}
			else
			{
				SetActive(UI.SPR_SPECIAL_OFFER, is_visible: false);
				SetActive(UI.SPR_SPECIAL_OFFER_LEFT, is_visible: false);
			}
		}
		else
		{
			SetBadge(UI._SPR_STUDIO_ACTIVE, 0, SpriteAlignment.TopLeft);
			SetBadge(UI._SPR_STUDIO_INACTIVE, 0, SpriteAlignment.TopLeft);
			SetBadge(UI.SPR_POP_MENU_ACTIVE, badgeTotalNum, SpriteAlignment.TopLeft, 5, 5, is_scale_normalize: true);
			if (MonoBehaviourSingleton<UserInfoManager>.I.needShowOneTimesOfferSS)
			{
				SetActive(UI.SPR_SPECIAL_OFFER, is_visible: false);
				SetActive(UI.SPR_SPECIAL_OFFER_LEFT, is_visible: true);
			}
			else
			{
				SetActive(UI.SPR_SPECIAL_OFFER, is_visible: false);
				SetActive(UI.SPR_SPECIAL_OFFER_LEFT, is_visible: false);
			}
		}
		UpdateNewMapUI();
	}

	public void UpdateSceneButtons(string scene_name)
	{
		updateSceneButton = scene_name;
	}

	private void UpdateSceneButton(MAIN_SCENE now, MAIN_SCENE check, UI active_ui, UI active_decoration, UI inactive_ui)
	{
		bool flag = now == check;
		SetActive(active_ui, flag);
		SetActive(active_decoration, flag);
		SetActive(inactive_ui, !flag);
		if (flag)
		{
			activeSceneButton = GetCtrl(active_ui);
		}
	}

	private void Update()
	{
		if (updateSceneButton == null)
		{
			return;
		}
		MAIN_SCENE mAIN_SCENE = GameDefine.SceneNameToEnum(updateSceneButton);
		updateSceneButton = null;
		activeSceneButton = null;
		UpdateSceneButton(mAIN_SCENE, MAIN_SCENE.HOME, UI._SPR_HOME_ACTIVE, UI.SPR_HOME_ACTIVE_DECORATION, UI._SPR_HOME_INACTIVE);
		UpdateSceneButton(mAIN_SCENE, MAIN_SCENE.LOUNGE, UI._SPR_LOUNGE_ACTIVE, UI.SPR_LOUNGE_ACTIVE_DECORATION, UI._SPR_LOUNGE_INACTIVE);
		UpdateSceneButton(mAIN_SCENE, MAIN_SCENE.CLAN, UI._SPR_CLAN_ACTIVE, UI.SPR_CLAN_ACTIVE_DECORATION, UI._SPR_CLAN_INACTIVE);
		UpdateSceneButton(mAIN_SCENE, MAIN_SCENE.QUEST, UI._SPR_QUEST_ACTIVE, UI.SPR_QUEST_ACTIVE_DECORATION, UI._SPR_QUEST_INACTIVE);
		UpdateSceneButton(mAIN_SCENE, MAIN_SCENE.STUDIO, UI._SPR_STUDIO_ACTIVE, UI.SPR_STUDIO_ACTIVE_DECORATION, UI._SPR_STUDIO_INACTIVE);
		UpdateSceneButton(mAIN_SCENE, MAIN_SCENE.SHOP, UI._SPR_SHOP_ACTIVE, UI.SPR_SHOP_ACTIVE_DECORATION, UI._SPR_SHOP_INACTIVE);
		if (!isPopMenu)
		{
			activeSceneButton = null;
		}
		if (MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
		{
			if (activeSceneButton != null)
			{
				MonoBehaviourSingleton<OutGameEffectManager>.I.UpdateSceneButtonEffect(mAIN_SCENE, activeSceneButton);
			}
			else
			{
				MonoBehaviourSingleton<OutGameEffectManager>.I.ReleaseSceneButtonEffect();
			}
		}
	}

	private void LateUpdate()
	{
		if (mapCheckSpan != null && mapCheckSpan.IsReady())
		{
			UpdateNewMapUI();
		}
		if (MonoBehaviourSingleton<FilterManager>.IsValid())
		{
			if (base.uiPanels[1].depth != 0 && MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
			{
				base.uiPanels[1].depth = 0;
			}
			else if (base.uiPanels[1].depth == 0 && !MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
			{
				base.uiPanels[1].depth = base.baseDepth + base.uiPanelDepths[1] + 1;
			}
		}
	}

	public void SetMenuButtonEnable(bool is_enable)
	{
		SetButtonEnabled(UI.BTN_POP_MENU, is_enable);
	}

	public void UpdateGachaDeco(GachaDeco data)
	{
		bool num = gachaDecoInfo == null && data != null;
		gachaDecoInfo = data;
		if (num)
		{
			StartCoroutine(DoGachaDeco());
		}
	}

	private IEnumerator DoGachaDeco()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		GachaDeco visible_info = null;
		bool wait = false;
		while (true)
		{
			if (visible_info == gachaDecoInfo)
			{
				yield return null;
				continue;
			}
			if (visible_info != null)
			{
				wait = true;
				PlayTween(UI.OBJ_GACHA_DECO_ROOT, forward: false, delegate
				{
					wait = false;
				}, is_input_block: false);
				while (wait)
				{
					yield return null;
				}
				SetActive(UI.OBJ_GACHA_DECO_ROOT, is_visible: false);
			}
			if (gachaDecoInfo == null)
			{
				break;
			}
			visible_info = gachaDecoInfo;
			int icon_type = visible_info.appendixType;
			int icon_type_count = 0;
			string icon_effect_name = null;
			if (MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.gachaDecoIconEffectNames != null)
			{
				icon_type_count = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.gachaDecoIconEffectNames.Length;
				icon_effect_name = ((icon_type >= icon_type_count) ? null : MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.gachaDecoIconEffectNames[icon_type]);
			}
			if (icon_type_count <= 0)
			{
				if (icon_type > 0)
				{
					icon_type = 1;
				}
				icon_type_count = 2;
				icon_effect_name = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.gachaDecoNewEffectName;
			}
			LoadObject lo_tex = load_queue.Load(isEventAsset: true, RESOURCE_CATEGORY.HOME_GACHA_DECO_IMAGE, ResourceName.GetGachaDecoImage(visible_info.decoId));
			if (!string.IsNullOrEmpty(icon_effect_name))
			{
				load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, icon_effect_name);
			}
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			Texture2D texture2D = lo_tex.loadedObject as Texture2D;
			if (!(texture2D != null))
			{
				continue;
			}
			SetActive(UI.OBJ_GACHA_DECO_ROOT, is_visible: true);
			for (int i = 0; i < icon_type_count; i++)
			{
				Transform gachaDecoIcon = GetGachaDecoIcon(i);
				if (gachaDecoIcon != null)
				{
					gachaDecoIcon.gameObject.SetActive(icon_type == i);
				}
			}
			SetTexture(UI.TEX_GACHA_DECO_IMAGE, texture2D);
			SetWidth(UI.TEX_GACHA_DECO_IMAGE, texture2D.width);
			SetHeight(UI.TEX_GACHA_DECO_IMAGE, texture2D.height);
			wait = true;
			PlayTween(UI.OBJ_GACHA_DECO_ROOT, forward: true, delegate
			{
				wait = false;
			}, is_input_block: false);
			while (wait)
			{
				yield return null;
			}
			if (!string.IsNullOrEmpty(icon_effect_name) && icon_type > 0)
			{
				SetVisibleWidgetEffect(null, GetGachaDecoIcon(icon_type), icon_effect_name);
			}
		}
	}

	private Transform GetGachaDecoIcon(int id)
	{
		if (id <= 0)
		{
			return null;
		}
		if (id == 1)
		{
			Transform ctrl = GetCtrl(UI.SPR_GACHA_DECO_NEW);
			if (ctrl != null)
			{
				return ctrl;
			}
		}
		return Utility.FindChild(GetCtrl(UI.OBJ_GACHA_DECO_ROOT), $"SPR_GACHA_DECO_ICON_{id}");
	}
}
