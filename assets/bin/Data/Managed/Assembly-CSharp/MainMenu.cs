using Network;
using System;
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
		_SPR_LOUNGE_ACTIVE,
		_SPR_LOUNGE_INACTIVE,
		SPR_LOUNGE_ACTIVE_DECORATION,
		BTN_LOUNGE,
		BTN_CLAN,
		SCR_MENU,
		SPR_NEW_MAP
	}

	private const string HomeButtonName = "BtnHome";

	private string updateSceneButton;

	private EventDelegate _delegate;

	private GachaDeco gachaDecoInfo;

	private Transform homeButton;

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
		SetActive((Enum)UI.SPR_NEW_MAP, false);
		mapCheckSpan = new SpanTimer(2f);
	}

	private void POP_MENU()
	{
		if (!IsTransitioning())
		{
			if (!isPopMenu)
			{
				ResetTween((Enum)UI.TWN_POP_MENU, 0);
			}
			isPopMenu = !isPopMenu;
			if (TutorialStep.HasAllTutorialCompleted())
			{
				PlayerPrefs.SetInt("IS_POP_FOOTER_MENU", isPopMenu ? 1 : 0);
			}
			PlayTween((Enum)UI.TWN_POP_MENU, isPopMenu, (EventDelegate.Callback)null, true, 0);
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
		string currentSceneName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName();
		MAIN_SCENE mAIN_SCENE = GameDefine.SceneNameToEnum(currentSceneName);
		if ((mAIN_SCENE != nowScene && mAIN_SCENE != MAIN_SCENE.MAX) || (nowScene == MAIN_SCENE.HOME && currentSectionName == "StoryMain"))
		{
			if (mAIN_SCENE == MAIN_SCENE.HOME || mAIN_SCENE == MAIN_SCENE.LOUNGE)
			{
				if (!TutorialStep.HasAllTutorialCompleted() || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP))
				{
					ResetTween((Enum)UI.TWN_POP_MENU, 0);
					isPopMenu = false;
					SkipTween((Enum)UI.TWN_POP_MENU, false, 0);
				}
				else
				{
					ResetTween((Enum)UI.TWN_POP_MENU, 0);
					isPopMenu = (PlayerPrefs.GetInt("IS_POP_FOOTER_MENU", 0) == 1);
					SkipTween((Enum)UI.TWN_POP_MENU, isPopMenu, 0);
				}
			}
			else
			{
				ResetTween((Enum)UI.TWN_POP_MENU, 0);
				isPopMenu = true;
				SkipTween((Enum)UI.TWN_POP_MENU, true, 0);
			}
			if (_delegate == null)
			{
				_delegate = new EventDelegate(POP_MENU);
				UIButton component = base.GetComponent<UIButton>((Enum)UI.BTN_POP_MENU);
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
			UpdateNewMapUI();
		}
	}

	private void UpdateNewMapUI()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		bool flag = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop";
		if (isPopMenu && flag)
		{
			if (MonoBehaviourSingleton<WorldMapManager>.I.releasedRegionIds == null)
			{
				return;
			}
			Transform ctrl = GetCtrl(UI.SPR_NEW_MAP);
			bool activeSelf = ctrl.get_gameObject().get_activeSelf();
			bool is_visible = MonoBehaviourSingleton<WorldMapManager>.I.ExistRegionDirection();
			SetActive((Enum)UI.SPR_NEW_MAP, is_visible);
			if (!activeSelf)
			{
				TweenAlpha component = ctrl.GetComponent<TweenAlpha>();
				component.ResetToBeginning();
				component.PlayForward();
			}
		}
		else
		{
			SetActive((Enum)UI.SPR_NEW_MAP, false);
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
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		if (base.uiFirstUpdate)
		{
			SetActive((Enum)UI.OBJ_GACHA_DECO_ROOT, false);
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.needShowOneTimesOfferSS)
		{
			SetActive((Enum)UI.SPR_SPECIAL_OFFER, true);
		}
		else
		{
			SetActive((Enum)UI.SPR_SPECIAL_OFFER, false);
		}
		if (homeButton != null)
		{
			homeButton.get_gameObject().SetActive(!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge());
		}
		SetActive((Enum)UI.BTN_LOUNGE, MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge());
		SetActive((Enum)UI.BTN_CLAN, false);
		SetToggle((Enum)UI.TGL_POP_MENU, isPopMenu);
		SetColor((Enum)UI.OBJ_ANCHOR_MENU, Color.get_clear());
		SetColor((Enum)UI.OBJ_ANCHOR_POP_MENU, Color.get_white());
		UpdateSceneButtons(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName());
		int badgeTotalNum = MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum();
		if (isPopMenu)
		{
			SetActive(base.collectUI, false);
			SetActive(base.collectUI, true);
			SetBadge((Enum)UI._SPR_STUDIO_ACTIVE, badgeTotalNum, 1, 8, -8, true);
			SetBadge((Enum)UI._SPR_STUDIO_INACTIVE, badgeTotalNum, 1, 8, -8, true);
			SetBadge((Enum)UI.SPR_POP_MENU_ACTIVE, 0, 1, 5, 5, false);
		}
		else
		{
			SetBadge((Enum)UI._SPR_STUDIO_ACTIVE, 0, 1, 5, 5, false);
			SetBadge((Enum)UI._SPR_STUDIO_INACTIVE, 0, 1, 5, 5, false);
			SetBadge((Enum)UI.SPR_POP_MENU_ACTIVE, badgeTotalNum, 1, 5, 5, true);
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
		SetActive((Enum)active_ui, flag);
		SetActive((Enum)active_decoration, flag);
		SetActive((Enum)inactive_ui, !flag);
		if (flag)
		{
			activeSceneButton = GetCtrl(active_ui);
		}
	}

	private void Update()
	{
		if (updateSceneButton != null)
		{
			MAIN_SCENE mAIN_SCENE = GameDefine.SceneNameToEnum(updateSceneButton);
			updateSceneButton = null;
			activeSceneButton = null;
			UpdateSceneButton(mAIN_SCENE, MAIN_SCENE.HOME, UI._SPR_HOME_ACTIVE, UI.SPR_HOME_ACTIVE_DECORATION, UI._SPR_HOME_INACTIVE);
			UpdateSceneButton(mAIN_SCENE, MAIN_SCENE.LOUNGE, UI._SPR_LOUNGE_ACTIVE, UI.SPR_LOUNGE_ACTIVE_DECORATION, UI._SPR_LOUNGE_INACTIVE);
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
		SetButtonEnabled((Enum)UI.BTN_POP_MENU, is_enable);
	}

	public void UpdateGachaDeco(GachaDeco data)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		bool flag = gachaDecoInfo == null && data != null;
		gachaDecoInfo = data;
		if (flag)
		{
			this.StartCoroutine(DoGachaDeco());
		}
	}

	private IEnumerator DoGachaDeco()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		GachaDeco visible_info = null;
		while (true)
		{
			if (visible_info == gachaDecoInfo)
			{
				yield return (object)null;
			}
			else
			{
				if (visible_info != null)
				{
					bool wait2 = true;
					PlayTween((Enum)UI.OBJ_GACHA_DECO_ROOT, false, (EventDelegate.Callback)delegate
					{
						((_003CDoGachaDeco_003Ec__Iterator2C)/*Error near IL_009a: stateMachine*/)._003Cwait_003E__2 = false;
					}, false, 0);
					while (wait2)
					{
						yield return (object)null;
					}
					SetActive((Enum)UI.OBJ_GACHA_DECO_ROOT, false);
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
				LoadObject lo_tex = load_queue.Load(RESOURCE_CATEGORY.HOME_GACHA_DECO_IMAGE, ResourceName.GetGachaDecoImage(visible_info.decoId), false);
				if (!string.IsNullOrEmpty(icon_effect_name))
				{
					load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, icon_effect_name);
				}
				if (load_queue.IsLoading())
				{
					yield return (object)load_queue.Wait();
				}
				Texture2D tex = lo_tex.loadedObject as Texture2D;
				if (tex != null)
				{
					SetActive((Enum)UI.OBJ_GACHA_DECO_ROOT, true);
					for (int i = 0; i < icon_type_count; i++)
					{
						Transform t = GetGachaDecoIcon(i);
						if (t != null)
						{
							t.get_gameObject().SetActive(icon_type == i);
						}
					}
					SetTexture((Enum)UI.TEX_GACHA_DECO_IMAGE, tex);
					SetWidth((Enum)UI.TEX_GACHA_DECO_IMAGE, tex.get_width());
					SetHeight((Enum)UI.TEX_GACHA_DECO_IMAGE, tex.get_height());
					bool wait2 = true;
					PlayTween((Enum)UI.OBJ_GACHA_DECO_ROOT, true, (EventDelegate.Callback)delegate
					{
						((_003CDoGachaDeco_003Ec__Iterator2C)/*Error near IL_034d: stateMachine*/)._003Cwait_003E__2 = false;
					}, false, 0);
					while (wait2)
					{
						yield return (object)null;
					}
					if (!string.IsNullOrEmpty(icon_effect_name) && icon_type > 0)
					{
						SetVisibleWidgetEffect(null, GetGachaDecoIcon(icon_type), icon_effect_name);
					}
				}
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
