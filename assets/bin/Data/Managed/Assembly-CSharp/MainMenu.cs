// Decompiled with JetBrains decompiler
// Type: MainMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class MainMenu : UIBehaviour
{
  private string updateSceneButton;
  private EventDelegate _delegate;
  private GachaDeco gachaDecoInfo;
  private Transform homeButton;
  private const string HomeButtonName = "BtnHome";
  private bool isPopMenu = true;
  private MAIN_SCENE nowScene = MAIN_SCENE.MAX;
  private SpanTimer mapCheckSpan;

  public Transform activeSceneButton { get; private set; }

  private void Start()
  {
    this.homeButton = Utility.FindChild(this.GetCtrl((Enum) MainMenu.UI.SCR_MENU), "BtnHome");
    this.SetActive((Enum) MainMenu.UI.SPR_NEW_MAP, false);
    this.mapCheckSpan = new SpanTimer(2f);
  }

  private void POP_MENU()
  {
    if (this.IsTransitioning())
      return;
    if (!this.isPopMenu)
      this.ResetTween((Enum) MainMenu.UI.TWN_POP_MENU);
    this.isPopMenu = !this.isPopMenu;
    if (TutorialStep.HasAllTutorialCompleted())
      PlayerPrefs.SetInt("IS_POP_FOOTER_MENU", this.isPopMenu ? 1 : 0);
    this.PlayTween((Enum) MainMenu.UI.TWN_POP_MENU, this.isPopMenu);
    this.RefreshUI();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if (!TutorialStep.HasFirstDeliveryCompleted())
      return;
    if ((flags & GameSection.NOTIFY_FLAG.CHANGED_SCENE) != (GameSection.NOTIFY_FLAG) 0)
      this.UpdateMainMenu();
    base.OnNotify(flags);
  }

  public void UpdateMainMenu()
  {
    string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
    MAIN_SCENE mainScene = GameDefine.SceneNameToEnum(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName());
    if (mainScene != this.nowScene && mainScene != MAIN_SCENE.MAX || this.nowScene == MAIN_SCENE.HOME && currentSectionName == "StoryMain")
    {
      if (mainScene == MAIN_SCENE.HOME || mainScene == MAIN_SCENE.LOUNGE || mainScene == MAIN_SCENE.CLAN)
      {
        if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1))
        {
          this.ResetTween((Enum) MainMenu.UI.TWN_POP_MENU);
          this.isPopMenu = true;
          this.SkipTween((Enum) MainMenu.UI.TWN_POP_MENU);
        }
        else if (!TutorialStep.HasAllTutorialCompleted() || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA1) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA2) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
        {
          this.ResetTween((Enum) MainMenu.UI.TWN_POP_MENU);
          this.isPopMenu = false;
          this.SkipTween((Enum) MainMenu.UI.TWN_POP_MENU, false);
        }
        else
        {
          this.ResetTween((Enum) MainMenu.UI.TWN_POP_MENU);
          this.isPopMenu = PlayerPrefs.GetInt("IS_POP_FOOTER_MENU", 0) == 1;
          this.SkipTween((Enum) MainMenu.UI.TWN_POP_MENU, this.isPopMenu);
        }
      }
      else
      {
        this.ResetTween((Enum) MainMenu.UI.TWN_POP_MENU);
        this.isPopMenu = true;
        this.SkipTween((Enum) MainMenu.UI.TWN_POP_MENU);
      }
      if (this._delegate == null)
      {
        this._delegate = new EventDelegate(new EventDelegate.Callback(this.POP_MENU));
        UIButton component = this.GetComponent<UIButton>((Enum) MainMenu.UI.BTN_POP_MENU);
        if (!component.onClick.Contains(this._delegate))
          component.onClick.Add(this._delegate);
      }
      this.UpdateUI();
      this.nowScene = mainScene;
    }
    else
    {
      this.ResetTween((Enum) MainMenu.UI.TWN_POP_MENU);
      this.isPopMenu = true;
      this.SkipTween((Enum) MainMenu.UI.TWN_POP_MENU);
      this.UpdateNewMapUI();
    }
  }

  private void UpdateNewMapUI()
  {
    if (this.isPopMenu & (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "ClanTop"))
    {
      if (MonoBehaviourSingleton<WorldMapManager>.I.releasedRegionIds == null)
        return;
      Transform ctrl = this.GetCtrl((Enum) MainMenu.UI.SPR_NEW_MAP);
      int num = ((Component) ctrl).gameObject.activeSelf ? 1 : 0;
      MonoBehaviourSingleton<WorldMapManager>.I.ExistRegionDirection();
      if (num == 0)
      {
        TweenAlpha component = ((Component) ctrl).GetComponent<TweenAlpha>();
        component.ResetToBeginning();
        component.PlayForward();
      }
    }
    else
      this.SetActive((Enum) MainMenu.UI.SPR_NEW_MAP, false);
    if (this.mapCheckSpan == null)
      return;
    this.mapCheckSpan.ResetNextTime();
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS | GameSection.NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | GameSection.NOTIFY_FLAG.UPDATE_SMITH_BADGE;
  }

  public override void UpdateUI()
  {
    if (this.uiFirstUpdate)
      this.SetActive((Enum) MainMenu.UI.OBJ_GACHA_DECO_ROOT, false);
    if (Object.op_Inequality((Object) this.homeButton, (Object) null))
      ((Component) this.homeButton).gameObject.SetActive(false);
    this.SetActive((Enum) MainMenu.UI.BTN_LOUNGE, false);
    this.SetActive((Enum) MainMenu.UI.BTN_CLAN, false);
    if (LoungeMatchingManager.IsValidInLounge())
      this.SetActive((Enum) MainMenu.UI.BTN_LOUNGE, true);
    else if (ClanMatchingManager.IsValidInClan())
      this.SetActive((Enum) MainMenu.UI.BTN_CLAN, true);
    else if (Object.op_Inequality((Object) this.homeButton, (Object) null))
      ((Component) this.homeButton).gameObject.SetActive(true);
    this.SetToggle((Enum) MainMenu.UI.TGL_POP_MENU, this.isPopMenu);
    this.SetColor((Enum) MainMenu.UI.OBJ_ANCHOR_MENU, Color.clear);
    this.SetColor((Enum) MainMenu.UI.OBJ_ANCHOR_POP_MENU, Color.white);
    this.UpdateSceneButtons(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName());
    int badgeTotalNum = MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum();
    if (this.isPopMenu)
    {
      this.SetActive(this.collectUI, false);
      this.SetActive(this.collectUI, true);
      this.SetBadge((Enum) MainMenu.UI._SPR_STUDIO_ACTIVE, badgeTotalNum, (SpriteAlignment) 1, 8, -8, true);
      this.SetBadge((Enum) MainMenu.UI._SPR_STUDIO_INACTIVE, badgeTotalNum, (SpriteAlignment) 1, 8, -8, true);
      this.SetBadge((Enum) MainMenu.UI.SPR_POP_MENU_ACTIVE, 0, (SpriteAlignment) 1);
      if (MonoBehaviourSingleton<UserInfoManager>.I.needShowOneTimesOfferSS)
      {
        this.SetActive((Enum) MainMenu.UI.SPR_SPECIAL_OFFER, true);
        this.SetActive((Enum) MainMenu.UI.SPR_SPECIAL_OFFER_LEFT, false);
      }
      else
      {
        this.SetActive((Enum) MainMenu.UI.SPR_SPECIAL_OFFER, false);
        this.SetActive((Enum) MainMenu.UI.SPR_SPECIAL_OFFER_LEFT, false);
      }
    }
    else
    {
      this.SetBadge((Enum) MainMenu.UI._SPR_STUDIO_ACTIVE, 0, (SpriteAlignment) 1);
      this.SetBadge((Enum) MainMenu.UI._SPR_STUDIO_INACTIVE, 0, (SpriteAlignment) 1);
      this.SetBadge((Enum) MainMenu.UI.SPR_POP_MENU_ACTIVE, badgeTotalNum, (SpriteAlignment) 1, is_scale_normalize: true);
      if (MonoBehaviourSingleton<UserInfoManager>.I.needShowOneTimesOfferSS)
      {
        this.SetActive((Enum) MainMenu.UI.SPR_SPECIAL_OFFER, false);
        this.SetActive((Enum) MainMenu.UI.SPR_SPECIAL_OFFER_LEFT, true);
      }
      else
      {
        this.SetActive((Enum) MainMenu.UI.SPR_SPECIAL_OFFER, false);
        this.SetActive((Enum) MainMenu.UI.SPR_SPECIAL_OFFER_LEFT, false);
      }
    }
    this.UpdateNewMapUI();
  }

  public void UpdateSceneButtons(string scene_name) => this.updateSceneButton = scene_name;

  private void UpdateSceneButton(
    MAIN_SCENE now,
    MAIN_SCENE check,
    MainMenu.UI active_ui,
    MainMenu.UI active_decoration,
    MainMenu.UI inactive_ui)
  {
    bool is_visible = now == check;
    this.SetActive((Enum) active_ui, is_visible);
    this.SetActive((Enum) active_decoration, is_visible);
    this.SetActive((Enum) inactive_ui, !is_visible);
    if (!is_visible)
      return;
    this.activeSceneButton = this.GetCtrl((Enum) active_ui);
  }

  private void Update()
  {
    if (this.updateSceneButton == null)
      return;
    MAIN_SCENE mainScene = GameDefine.SceneNameToEnum(this.updateSceneButton);
    this.updateSceneButton = (string) null;
    this.activeSceneButton = (Transform) null;
    this.UpdateSceneButton(mainScene, MAIN_SCENE.HOME, MainMenu.UI._SPR_HOME_ACTIVE, MainMenu.UI.SPR_HOME_ACTIVE_DECORATION, MainMenu.UI._SPR_HOME_INACTIVE);
    this.UpdateSceneButton(mainScene, MAIN_SCENE.LOUNGE, MainMenu.UI._SPR_LOUNGE_ACTIVE, MainMenu.UI.SPR_LOUNGE_ACTIVE_DECORATION, MainMenu.UI._SPR_LOUNGE_INACTIVE);
    this.UpdateSceneButton(mainScene, MAIN_SCENE.CLAN, MainMenu.UI._SPR_CLAN_ACTIVE, MainMenu.UI.SPR_CLAN_ACTIVE_DECORATION, MainMenu.UI._SPR_CLAN_INACTIVE);
    this.UpdateSceneButton(mainScene, MAIN_SCENE.QUEST, MainMenu.UI._SPR_QUEST_ACTIVE, MainMenu.UI.SPR_QUEST_ACTIVE_DECORATION, MainMenu.UI._SPR_QUEST_INACTIVE);
    this.UpdateSceneButton(mainScene, MAIN_SCENE.STUDIO, MainMenu.UI._SPR_STUDIO_ACTIVE, MainMenu.UI.SPR_STUDIO_ACTIVE_DECORATION, MainMenu.UI._SPR_STUDIO_INACTIVE);
    this.UpdateSceneButton(mainScene, MAIN_SCENE.SHOP, MainMenu.UI._SPR_SHOP_ACTIVE, MainMenu.UI.SPR_SHOP_ACTIVE_DECORATION, MainMenu.UI._SPR_SHOP_INACTIVE);
    if (!this.isPopMenu)
      this.activeSceneButton = (Transform) null;
    if (!MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
      return;
    if (Object.op_Inequality((Object) this.activeSceneButton, (Object) null))
      MonoBehaviourSingleton<OutGameEffectManager>.I.UpdateSceneButtonEffect(mainScene, this.activeSceneButton);
    else
      MonoBehaviourSingleton<OutGameEffectManager>.I.ReleaseSceneButtonEffect();
  }

  private void LateUpdate()
  {
    if (this.mapCheckSpan != null && this.mapCheckSpan.IsReady())
      this.UpdateNewMapUI();
    if (!MonoBehaviourSingleton<FilterManager>.IsValid())
      return;
    if (this.uiPanels[1].depth != 0 && MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
    {
      this.uiPanels[1].depth = 0;
    }
    else
    {
      if (this.uiPanels[1].depth != 0 || MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
        return;
      this.uiPanels[1].depth = this.baseDepth + this.uiPanelDepths[1] + 1;
    }
  }

  public void SetMenuButtonEnable(bool is_enable)
  {
    this.SetButtonEnabled((Enum) MainMenu.UI.BTN_POP_MENU, is_enable);
  }

  public void UpdateGachaDeco(GachaDeco data)
  {
    int num = this.gachaDecoInfo != null ? 0 : (data != null ? 1 : 0);
    this.gachaDecoInfo = data;
    if (num == 0)
      return;
    this.StartCoroutine(this.DoGachaDeco());
  }

  private IEnumerator DoGachaDeco()
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    GachaDeco visible_info = (GachaDeco) null;
    bool wait = false;
    while (true)
    {
      while (visible_info != this.gachaDecoInfo)
      {
        if (visible_info != null)
        {
          wait = true;
          this.PlayTween((Enum) MainMenu.UI.OBJ_GACHA_DECO_ROOT, false, (EventDelegate.Callback) (() => wait = false), false);
          while (wait)
            yield return (object) null;
          this.SetActive((Enum) MainMenu.UI.OBJ_GACHA_DECO_ROOT, false);
        }
        if (this.gachaDecoInfo == null)
          yield break;
        visible_info = this.gachaDecoInfo;
        int icon_type = visible_info.appendixType;
        int icon_type_count = 0;
        string icon_effect_name = (string) null;
        if (MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.gachaDecoIconEffectNames != null)
        {
          icon_type_count = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.gachaDecoIconEffectNames.Length;
          icon_effect_name = icon_type >= icon_type_count ? (string) null : MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.gachaDecoIconEffectNames[icon_type];
        }
        if (icon_type_count <= 0)
        {
          if (icon_type > 0)
            icon_type = 1;
          icon_type_count = 2;
          icon_effect_name = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.gachaDecoNewEffectName;
        }
        LoadObject lo_tex = load_queue.Load(true, RESOURCE_CATEGORY.HOME_GACHA_DECO_IMAGE, ResourceName.GetGachaDecoImage(visible_info.decoId));
        if (!string.IsNullOrEmpty(icon_effect_name))
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, icon_effect_name);
        if (load_queue.IsLoading())
          yield return (object) load_queue.Wait();
        Texture2D loadedObject = lo_tex.loadedObject as Texture2D;
        if (Object.op_Inequality((Object) loadedObject, (Object) null))
        {
          this.SetActive((Enum) MainMenu.UI.OBJ_GACHA_DECO_ROOT, true);
          for (int id = 0; id < icon_type_count; ++id)
          {
            Transform gachaDecoIcon = this.GetGachaDecoIcon(id);
            if (Object.op_Inequality((Object) gachaDecoIcon, (Object) null))
              ((Component) gachaDecoIcon).gameObject.SetActive(icon_type == id);
          }
          this.SetTexture((Enum) MainMenu.UI.TEX_GACHA_DECO_IMAGE, (Texture) loadedObject);
          this.SetWidth((Enum) MainMenu.UI.TEX_GACHA_DECO_IMAGE, ((Texture) loadedObject).width);
          this.SetHeight((Enum) MainMenu.UI.TEX_GACHA_DECO_IMAGE, ((Texture) loadedObject).height);
          wait = true;
          this.PlayTween((Enum) MainMenu.UI.OBJ_GACHA_DECO_ROOT, callback: (EventDelegate.Callback) (() => wait = false), is_input_block: false);
          while (wait)
            yield return (object) null;
          if (!string.IsNullOrEmpty(icon_effect_name) && icon_type > 0)
            this.SetVisibleWidgetEffect((Transform) null, this.GetGachaDecoIcon(icon_type), icon_effect_name);
        }
        icon_effect_name = (string) null;
        lo_tex = (LoadObject) null;
      }
      yield return (object) null;
    }
  }

  private Transform GetGachaDecoIcon(int id)
  {
    if (id <= 0)
      return (Transform) null;
    if (id == 1)
    {
      Transform ctrl = this.GetCtrl((Enum) MainMenu.UI.SPR_GACHA_DECO_NEW);
      if (Object.op_Inequality((Object) ctrl, (Object) null))
        return ctrl;
    }
    return Utility.FindChild(this.GetCtrl((Enum) MainMenu.UI.OBJ_GACHA_DECO_ROOT), $"SPR_GACHA_DECO_ICON_{id}");
  }

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
    SPR_NEW_MAP,
  }
}
