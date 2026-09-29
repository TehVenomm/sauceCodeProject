// Decompiled with JetBrains decompiler
// Type: ProfileTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ProfileTop : GameSection
{
  private PlayerLoader playerLoader;
  private UIRenderTexture renderTexture;
  private UITexture uiTexture;
  private Transform playerShadow;
  private DegreePlate degree;
  private UIEventListener eventListener;

  protected void OnEnable()
  {
    if (!Object.op_Inequality((Object) this.eventListener, (Object) null))
      return;
    this.eventListener.onDrag += new UIEventListener.VectorDelegate(this.OnDrag);
  }

  protected void OnDisable()
  {
    this.eventListener.onDrag -= new UIEventListener.VectorDelegate(this.OnDrag);
  }

  private void OnDrag(InputManager.TouchInfo touch_info)
  {
    if (Object.op_Equality((Object) this.playerLoader, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable() || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != nameof (ProfileTop))
      return;
    ((Component) this.playerLoader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
  }

  private void OnDrag(GameObject obj, Vector2 move)
  {
    if (Object.op_Equality((Object) this.playerLoader, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable() || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != nameof (ProfileTop))
      return;
    ((Component) this.playerLoader).transform.Rotate(GameDefine.GetCharaRotateVector(move));
  }

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    bool wait = true;
    this.degree = ((Component) this.GetCtrl((Enum) ProfileTop.UI.OBJ_DEGREE_ROOT)).GetComponent<DegreePlate>();
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    EquipSetInfo equipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(userStatus.eSetNo);
    uint armor_uniq_id = uint.Parse(userStatus.armorUniqId);
    uint helm_uniq_id = uint.Parse(userStatus.helmUniqId);
    uint arm_uniq_id = uint.Parse(userStatus.armUniqId);
    uint leg_uniq_id = uint.Parse(userStatus.legUniqId);
    bool flag = MonoBehaviourSingleton<StatusManager>.I.GetEquippingShowHelm(userStatus.eSetNo) == 1;
    PlayerLoadInfo info = new PlayerLoadInfo();
    info.SetupLoadInfo(equipSet, 0UL, (ulong) armor_uniq_id, (ulong) helm_uniq_id, (ulong) arm_uniq_id, (ulong) leg_uniq_id, flag);
    OutGameSettingsManager.ProfileScene profileScene = MonoBehaviourSingleton<OutGameSettingsManager>.I.profileScene;
    UIRenderTexture uiRenderTexture = this.InitRenderTexture((Enum) ProfileTop.UI.TEX_MODEL, profileScene.cameraFieldOfView);
    if (uiRenderTexture != null)
      uiRenderTexture.nearClipPlane = profileScene.nearClip;
    this.EnableRenderTexture((Enum) ProfileTop.UI.TEX_MODEL);
    this.SetRenderPlayerModel((Enum) ProfileTop.UI.TEX_MODEL, info, PLAYER_ANIM_TYPE.GetStatus(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex), profileScene.playerPos, new Vector3(0.0f, profileScene.playerRot, 0.0f), flag, (Action<PlayerLoader>) (x =>
    {
      this.playerLoader = x;
      wait = false;
    }));
    if (Object.op_Equality((Object) this.eventListener, (Object) null))
    {
      this.eventListener = ((Component) this.GetCtrl((Enum) ProfileTop.UI.OBJ_PROFILE_BG)).GetComponent<UIEventListener>();
      this.eventListener.onDrag += new UIEventListener.VectorDelegate(this.OnDrag);
    }
    while (wait)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetSupportEncoding(this._transform, (Enum) ProfileTop.UI.LBL_NAME, true);
    this.SetLabelText((Enum) ProfileTop.UI.LBL_NAME, Utility.GetNameWithColoredClanTag(string.Empty, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, true, true));
    this.SetLabelText((Enum) ProfileTop.UI.LBL_USER_ID, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code);
    this.SetLabelText((Enum) ProfileTop.UI.LBL_COMMENT, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.comment);
    this.SetLabelText((Enum) ProfileTop.UI.LBL_LEVEL, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level.ToString());
    this._UpdateFB();
    this.SetActive((Enum) ProfileTop.UI.BTN_DEGREE, GameDefine.ACTIVE_DEGREE);
    if (!GameDefine.ACTIVE_DEGREE)
      return;
    this.degree.Initialize(MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds, false, (Action<DegreePlate>) (x =>
    {
      ((Component) this.degree).gameObject.SetActive(false);
      ((Component) this.degree).gameObject.SetActive(true);
    }));
  }

  private void _UpdateFB()
  {
    this.SetActive((Enum) ProfileTop.UI.BTN_LOGIN, !MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook);
    this.SetActive((Enum) ProfileTop.UI.BTN_DISCONNECT, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserFacebook);
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    base.OnNotify(flags);
    if ((GameSection.NOTIFY_FLAG.FACEBOOK_LOGIN & flags) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.RefreshUI();
  }

  protected override void OnDestroy()
  {
    if (Object.op_Inequality((Object) this.uiTexture, (Object) null))
      Object.Destroy((Object) ((Component) this.uiTexture).gameObject);
    if (Object.op_Inequality((Object) this.playerShadow, (Object) null))
      Object.Destroy((Object) ((Component) this.playerShadow).gameObject);
    if (Object.op_Inequality((Object) this.renderTexture, (Object) null))
      this.renderTexture.Disable();
    base.OnDestroy();
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_INFO | GameSection.NOTIFY_FLAG.UPDATE_DEGREE_FRAME;
  }

  private void OnQuery_CHARA_MAKE()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userInfo,
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus,
      (object) 2
    });
  }

  private void OnQuery_NAMECHANGE()
  {
    GameSection.SetEventData((object) new object[3]
    {
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userInfo,
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus,
      (object) 1
    });
  }

  private void OnQuery_SECTION_BACK() => this.Close();

  private void OnQuery_LOGIN_FB()
  {
    GameSection.StayEvent();
    if (MonoBehaviourSingleton<FBManager>.I.isLoggedIn)
      this._SendRegistLinkFacebook();
    else
      MonoBehaviourSingleton<FBManager>.I.LoginWithReadPermission((Action<bool, string>) ((success, s) =>
      {
        if (success)
          this._SendRegistLinkFacebook();
        else
          GameSection.ResumeEvent(success);
      }));
  }

  private void _SendRegistLinkFacebook()
  {
    MonoBehaviourSingleton<AccountManager>.I.SendRegistLinkFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, (Action<bool, RegistLinkFacebookModel>) ((success, ret) =>
    {
      if (success)
      {
        MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, (Action<bool>) (is_success =>
        {
          if (success)
          {
            MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.FACEBOOK_LOGIN);
            GameSection.ChangeStayEvent("ACCOUNT_LOGIN");
          }
          GameSection.ResumeEvent(success);
        }));
      }
      else
      {
        if (ret.Error == Error.WRN_REGISTER_FACEBOOK_ACCOUNT_LINKED)
        {
          GameSection.ChangeStayEvent("ACCOUNT_CONFLICT", (object) ret.existInfo);
          success = true;
        }
        GameSection.ResumeEvent(success);
      }
    }));
  }

  private void OnQuery_DISCONNECT_FB() => GameSection.ChangeEvent("ACCOUNT_UNBIND_CONFIRM");

  private void OnQuery_COPY_CLIPBOARD()
  {
    ClipBoard.SetClipBoard(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code);
    GameSection.SetEventData((object) new object[1]
    {
      (object) MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code
    });
  }

  private void OnQuery_ProfileAccountUnbindConfirm_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FBManager>.I.Logout((Action<bool, string>) ((fb_success, s) =>
    {
      if (fb_success)
        MonoBehaviourSingleton<AccountManager>.I.SendRegistUnlinkFacebook((Action<bool>) (success =>
        {
          if (success)
            MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.FACEBOOK_LOGIN);
          GameSection.ResumeEvent(success);
        }));
      else
        GameSection.ResumeEvent(fb_success);
    }));
  }

  private enum UI
  {
    LBL_USER_ID,
    LBL_COMMENT,
    LBL_NAME,
    LBL_LEVEL,
    BTN_LOGIN,
    BTN_DISCONNECT,
    BTN_MESSAGE,
    TEX_MODEL,
    OBJ_PROFILE_BG,
    BTN_DEGREE,
    OBJ_DEGREE_ROOT,
  }
}
