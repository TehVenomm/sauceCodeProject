// Decompiled with JetBrains decompiler
// Type: MainStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class MainStatus : UIBehaviour
{
  private StatusBoostAnimator boostAnimator;

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.PRETREAT_SCENE | GameSection.NOTIFY_FLAG.CHANGED_SCENE | GameSection.NOTIFY_FLAG.UPDATE_PRESENT_NUM | GameSection.NOTIFY_FLAG.UPDATE_USER_INFO | GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    base.OnNotify(flags);
    if ((flags & GameSection.NOTIFY_FLAG.PRETREAT_SCENE) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.NoEventReleaseTouchAndRelease((Enum) MainStatus.UI.SPR_BG02);
    this.OnQuery_EXP_NEXT_HIDE();
  }

  public override void UpdateUI()
  {
    Network.UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    this.SetSupportEncoding(this._transform, (Enum) MainStatus.UI.LBL_NAME, true);
    this.SetLabelText((Enum) MainStatus.UI.LBL_NAME, Utility.GetNameWithColoredClanTag(string.Empty, userInfo.name, true, true));
    this.SetSupportEncoding(this._transform, (Enum) MainStatus.UI.LBL_SERVER_NAME, true);
    this.SetLabelText((Enum) MainStatus.UI.LBL_SERVER_NAME, "Server: " + GameSaveData.instance.currentServer.name);
    this.SetLabelText((Enum) MainStatus.UI.LBL_LEVEL, userStatus.level.ToString());
    this.SetLabelText((Enum) MainStatus.UI.LBL_CRYSTAL, userStatus.crystal.ToString("N0"));
    this.SetLabelText((Enum) MainStatus.UI.LBL_MONEY, userStatus.money.ToString("N0"));
    this.SetProgressValue((Enum) MainStatus.UI.PBR_EXP, userStatus.ExpProgress01);
    this.InitDeactive((Enum) MainStatus.UI.SPR_EXP_NEXT);
    if (TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() && Object.op_Equality((Object) TutorialMessage.GetCursor(), (Object) null) && userStatus.IsTutorialBitReady && MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
      this.SetTouchAndRelease((Enum) MainStatus.UI.SPR_BG02, "EXP_NEXT_SHOW", "EXP_NEXT_HIDE");
    int num = MonoBehaviourSingleton<PresentManager>.I.presentNum + MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum + (GameSaveData.instance.IsShowNewsNotification() ? 1 : 0);
    if (MonoBehaviourSingleton<HelpshiftManager>.IsValid())
      num += MonoBehaviourSingleton<HelpshiftManager>.I.countMess;
    this.SetBadge((Enum) MainStatus.UI.BTN_MENU, num, (SpriteAlignment) 3, -15, -8);
    if (Object.op_Equality((Object) this.boostAnimator, (Object) null))
      this.boostAnimator = ((Component) this).GetComponentInChildren<StatusBoostAnimator>();
    this.boostAnimator.SetupUI((Action<BoostStatus>) (update_boost =>
    {
      if (update_boost != null)
        this.UpdateShowBoost(update_boost);
      else
        this.EndShowBoost();
    }), (Action<BoostStatus>) (change_boost =>
    {
      if (change_boost != null)
      {
        this.ChangeShowBoost(change_boost.type);
        this.UpdateShowBoost(change_boost);
      }
      else
        this.EndShowBoost();
    }));
    this.SetFontStyle((Enum) MainStatus.UI.LBL_BOOST_RATE, (FontStyle) 2);
    this.SetFontStyle((Enum) MainStatus.UI.LBL_BOOST_TIME, (FontStyle) 2);
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName().Contains("HomeScene"))
      return;
    this.SetActive((Enum) MainStatus.UI.SPR_TUTORIAL_CURSOR_UP, false);
  }

  public void OnQuery_EXP_NEXT_SHOW()
  {
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    this.SetActive((Enum) MainStatus.UI.SPR_EXP_NEXT, true);
    this.SetLabelText((Enum) MainStatus.UI.STR_NOW_EXP, StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 2U));
    this.SetLabelText((Enum) MainStatus.UI.STR_NEXT_EXP, StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 3U));
    this.SetLabelText((Enum) MainStatus.UI.LBL_NOW_EXP, userStatus.exp.ToString());
    if ((int) userStatus.level >= Singleton<UserLevelTable>.I.GetMaxLevel())
      this.SetLabelText((Enum) MainStatus.UI.LBL_NEXT_EXP, "-");
    else
      this.SetLabelText((Enum) MainStatus.UI.LBL_NEXT_EXP, (userStatus.ExpNext - (int) userStatus.exp).ToString());
  }

  public void OnQuery_EXP_NEXT_HIDE() => this.SetActive((Enum) MainStatus.UI.SPR_EXP_NEXT, false);

  public void OnQuery_SHOW_GEMS_DIALOG()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("CrystalShop");
  }

  public void OnQuery_SHOW_PROFILE_DIALOG()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Profile");
    if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS) || !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM))
      return;
    TutorialMessageTable.SendTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS, (Action<bool>) (b => this.SetTutArrowActive(false)));
  }

  public void SetMenuButtonEnable(bool is_enable)
  {
    this.SetButtonEnabled((Enum) MainStatus.UI.BTN_MENU, is_enable);
  }

  private void EndShowBoost() => this.ChangeShowBoost(0);

  private void ChangeShowBoost(int _show_type)
  {
    USE_ITEM_EFFECT_TYPE useItemEffectType = (USE_ITEM_EFFECT_TYPE) _show_type;
    this.SetActive((Enum) MainStatus.UI.LBL_BOOST_RATE, useItemEffectType != 0);
    this.SetActive((Enum) MainStatus.UI.OBJ_BOOST_DROP_ROOT, useItemEffectType == USE_ITEM_EFFECT_TYPE.DROP_UP);
    this.SetActive((Enum) MainStatus.UI.OBJ_BOOST_EXP_ROOT, useItemEffectType == USE_ITEM_EFFECT_TYPE.EXP_UP);
    this.SetActive((Enum) MainStatus.UI.OBJ_BOOST_GOLD_ROOT, useItemEffectType == USE_ITEM_EFFECT_TYPE.MONEY_UP);
    this.SetActive((Enum) MainStatus.UI.OBJ_BOOST_HGP_ROOT, useItemEffectType == USE_ITEM_EFFECT_TYPE.EVENT_POINT_UP);
    this.SetActive((Enum) MainStatus.UI.OBJ_BOOST_NOVICE_DROP_ROOT, useItemEffectType == USE_ITEM_EFFECT_TYPE.NOVICE_DROP_UP);
    this.SetActive((Enum) MainStatus.UI.OBJ_BOOST_ENCOUNTER_ROOT, useItemEffectType == USE_ITEM_EFFECT_TYPE.HAPPEN_QUEST_UP);
    switch (useItemEffectType)
    {
      case USE_ITEM_EFFECT_TYPE.EXP_UP:
        this.ResetTween((Enum) MainStatus.UI.OBJ_BOOST_EXP_ROOT);
        this.PlayTween((Enum) MainStatus.UI.OBJ_BOOST_EXP_ROOT, is_input_block: false);
        break;
      case USE_ITEM_EFFECT_TYPE.MONEY_UP:
        this.ResetTween((Enum) MainStatus.UI.OBJ_BOOST_GOLD_ROOT);
        this.PlayTween((Enum) MainStatus.UI.OBJ_BOOST_GOLD_ROOT, is_input_block: false);
        break;
      case USE_ITEM_EFFECT_TYPE.DROP_UP:
        this.ResetTween((Enum) MainStatus.UI.OBJ_BOOST_DROP_ROOT);
        this.PlayTween((Enum) MainStatus.UI.OBJ_BOOST_DROP_ROOT, is_input_block: false);
        break;
      case USE_ITEM_EFFECT_TYPE.EVENT_POINT_UP:
        this.ResetTween((Enum) MainStatus.UI.OBJ_BOOST_HGP_ROOT);
        this.PlayTween((Enum) MainStatus.UI.OBJ_BOOST_HGP_ROOT, is_input_block: false);
        break;
      case USE_ITEM_EFFECT_TYPE.NOVICE_DROP_UP:
        this.ResetTween((Enum) MainStatus.UI.OBJ_BOOST_NOVICE_DROP_ROOT);
        this.PlayTween((Enum) MainStatus.UI.OBJ_BOOST_NOVICE_DROP_ROOT, is_input_block: false);
        break;
      case USE_ITEM_EFFECT_TYPE.HAPPEN_QUEST_UP:
        this.ResetTween((Enum) MainStatus.UI.OBJ_BOOST_ENCOUNTER_ROOT);
        this.PlayTween((Enum) MainStatus.UI.OBJ_BOOST_ENCOUNTER_ROOT, is_input_block: false);
        break;
    }
  }

  private void UpdateShowBoost(BoostStatus boost)
  {
    switch ((USE_ITEM_EFFECT_TYPE) boost.type)
    {
      case USE_ITEM_EFFECT_TYPE.EXP_UP:
      case USE_ITEM_EFFECT_TYPE.MONEY_UP:
      case USE_ITEM_EFFECT_TYPE.DROP_UP:
      case USE_ITEM_EFFECT_TYPE.EVENT_POINT_UP:
      case USE_ITEM_EFFECT_TYPE.NOVICE_DROP_UP:
      case USE_ITEM_EFFECT_TYPE.HAPPEN_QUEST_UP:
        this.SetColor((Enum) MainStatus.UI.LBL_BOOST_RATE, this.boostAnimator.GetRateColor(boost.value));
        this.SetLabelText((Enum) MainStatus.UI.LBL_BOOST_RATE, boost.GetBoostRateText());
        this.SetLabelText((Enum) MainStatus.UI.LBL_BOOST_TIME, boost.type == 210 ? "" : boost.GetRemainTime());
        break;
    }
  }

  public void SetTutArrowActive(bool isActive)
  {
    if (isActive)
      this.SetActive((Enum) MainStatus.UI.SPR_TUTORIAL_CURSOR_UP, true);
    else
      this.SetActive((Enum) MainStatus.UI.SPR_TUTORIAL_CURSOR_UP, false);
  }

  protected override void OnOpen()
  {
    base.OnOpen();
    if (!MonoBehaviourSingleton<HelpshiftManager>.IsValid())
      return;
    MonoBehaviourSingleton<HelpshiftManager>.I.RegisterDelegate(new System.Action(((UIBehaviour) this).UpdateUI));
  }

  protected override void OnDestroy()
  {
    if (MonoBehaviourSingleton<HelpshiftManager>.IsValid())
      MonoBehaviourSingleton<HelpshiftManager>.I.UnRegisterDelegate(new System.Action(((UIBehaviour) this).UpdateUI));
    base.OnDestroy();
  }

  private enum UI
  {
    LBL_NAME,
    LBL_LEVEL,
    LBL_CRYSTAL,
    LBL_MONEY,
    LBL_SERVER_NAME,
    PBR_EXP,
    SPR_BG02,
    SPR_EXP_NEXT,
    STR_NEXT_EXP,
    LBL_NEXT_EXP,
    STR_NOW_EXP,
    LBL_NOW_EXP,
    SPR_BADGE,
    BTN_MENU,
    LBL_BOOST_RATE,
    LBL_BOOST_TIME,
    OBJ_BOOST_EXP_ROOT,
    OBJ_BOOST_DROP_ROOT,
    OBJ_BOOST_GOLD_ROOT,
    OBJ_BOOST_HGP_ROOT,
    OBJ_BOOST_NOVICE_DROP_ROOT,
    SPR_TUTORIAL_CURSOR_UP,
    OBJ_BOOST_ENCOUNTER_ROOT,
  }
}
