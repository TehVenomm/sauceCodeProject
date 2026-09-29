// Decompiled with JetBrains decompiler
// Type: AccountManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AccountManager : MonoBehaviourSingleton<AccountManager>
{
  public bool sendAsset;
  public bool termsCheck;
  public string termsUpdateDay;
  public bool usageLimitMode;
  public bool appClose;
  public string closedNotice = "";
  public bool openRefundForm;

  public static void ResetAccount()
  {
    GameSaveData.Delete();
    if (MonoBehaviourSingleton<AccountManager>.IsValid())
      MonoBehaviourSingleton<AccountManager>.I.ClearAccount();
    FieldRewardPool.DeleteSave();
    PlayerPrefs.SetInt("LastNewsID", -1);
    new DataTableCache().RemoveAll();
    TutorialReadData.DeleteSave();
    if (Singleton<TutorialMessageTable>.IsValid() && Singleton<TutorialMessageTable>.I.ReadData != null)
    {
      Singleton<TutorialMessageTable>.I.ReadData.LoadSaveData();
      Singleton<TutorialMessageTable>.I.ReadData.Save();
    }
    Native.ResetPackagePreferences();
  }

  public static void SaveAsEmptyData()
  {
    SaveData.SetData<AccountManager.Account>(SaveData.Key.Account, new AccountManager.Account());
    SaveData.Save();
  }

  public AccountManager.Account account { get; protected set; }

  protected override void Awake()
  {
    base.Awake();
    this.LoadSaveData();
  }

  private void LoadSaveData()
  {
    bool flag = false;
    if (!SaveData.HasKey(SaveData.Key.Account))
    {
      flag = true;
      this.account = new AccountManager.Account();
      SaveData.SetData<AccountManager.Account>(SaveData.Key.Account, this.account);
    }
    this.account = SaveData.GetData<AccountManager.Account>(SaveData.Key.Account);
    AccountManager.Account accountOnServer = ServerAccountSaveData.instance.GetAccountOnServer(NetworkManager.APP_HOST);
    if (accountOnServer != null)
      this.account = accountOnServer;
    if (flag)
    {
      SaveData.Save();
    }
    else
    {
      NetworkNative.setCookieToken(this.account.token);
      NetworkNative.setSidToken(this.account.token);
    }
  }

  public void GetLastLoginAccountOnServer()
  {
    AccountManager.Account accountOnServer = ServerAccountSaveData.instance.GetAccountOnServer(NetworkManager.APP_HOST);
    if (accountOnServer == null)
      return;
    this.account = accountOnServer;
    this.SaveAccount(this.account.userHash, this.account.token);
  }

  public void SaveAccount(string user_hash, string token = null)
  {
    this.account.userHash = user_hash;
    if (token != null)
    {
      this.account.token = token;
      if (!string.IsNullOrEmpty(token))
      {
        NetworkNative.setCookieToken(this.account.token);
        NetworkNative.setSidToken(this.account.token);
        Native.getList();
      }
    }
    else if (!string.IsNullOrEmpty(MonoBehaviourSingleton<NetworkManager>.I.tokenTemp))
    {
      this.account.token = MonoBehaviourSingleton<NetworkManager>.I.tokenTemp;
      NetworkNative.setCookieToken(this.account.token);
      NetworkNative.setSidToken(this.account.token);
      Native.getList();
    }
    if (!string.IsNullOrEmpty(this.account.userHash))
      ServerAccountSaveData.instance.UpdateAccount(NetworkManager.APP_HOST, this.account);
    SaveData.SetData<AccountManager.Account>(SaveData.Key.Account, this.account);
    SaveData.Save();
  }

  public void ClearAccount()
  {
    string empty = string.Empty;
    this.SaveAccount(empty, empty);
    MonoBehaviourSingleton<NetworkManager>.I.tokenTemp = empty;
  }

  public void SendCheckRegister(string ntc_data, Action<bool> call_back)
  {
    Protocol.Send<CheckRegisterModel.RequestSendForm, CheckRegisterModel>(CheckRegisterModel.URL, new CheckRegisterModel.RequestSendForm()
    {
      data = ntc_data,
      d = NetworkNative.getUniqueDeviceId()
    }, (Action<CheckRegisterModel>) (ret =>
    {
      switch (ret.Error)
      {
        case Error.None:
          MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo, ret.result.tutorialStep);
          MonoBehaviourSingleton<UserInfoManager>.I.SetNewsID(ret.result.newsId);
          MonoBehaviourSingleton<UserInfoManager>.I.userIdHash = ret.result.userIdHash;
          this.sendAsset = ret.result.sendAsset;
          this.termsCheck = ret.result.termsCheck;
          this.termsUpdateDay = ret.result.termsUpdateDay;
          if (ret.result.recommendUpdate)
          {
            this.RecommendUpdateCheck((System.Action) (() => call_back(true)));
            break;
          }
          if (call_back == null)
            break;
          call_back(true);
          break;
        case Error.WRN_MAINTENANCE:
          this.OpenMessageDialog(ret.Error, (Action<string>) (sel => MonoBehaviourSingleton<GameSceneManager>.I.OpenInfoDialog((Action<string>) (o => this.SendCheckRegister(ntc_data, call_back)), true)));
          break;
        case Error.WRN_UPDATE_FORCE:
          this.OpenMessageDialog(ret.Error, (Action<string>) (sel =>
          {
            Native.launchMyselfMarket();
            this.SendCheckRegister(ntc_data, call_back);
          }));
          break;
        case Error.ERR_AUTH_FAILED:
          if (string.IsNullOrEmpty(this.account.token))
          {
            if (ret.result.recommendUpdate)
            {
              this.RecommendUpdateCheck((System.Action) (() => call_back(false)));
              break;
            }
            if (call_back == null)
              break;
            call_back(false);
            break;
          }
          MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage((uint) ret.Error), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101U), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102U)), (Action<string>) (btn =>
          {
            if (btn == "YES")
            {
              ServerAccountSaveData.instance.RemoveAccount(NetworkManager.APP_HOST);
              GameSceneEvent.PopStay();
              AccountManager.ResetAccount();
              TutorialReadData.SaveAsEmptyData();
              AccountManager.SaveAsEmptyData();
              MonoBehaviourSingleton<AppMain>.I.Reset();
            }
            else
              this.SendCheckRegister(ntc_data, call_back);
          }), true, (int) ret.Error);
          break;
        case Error.WRN_BAN_USER:
          this.OpenMessageDialog(ret.Error, (Action<string>) (sel => this.SendCheckRegister(ntc_data, call_back)));
          break;
        case Error.WRN_RESIGNED_USER:
          this.OpenMessageDialog(ret.Error, (Action<string>) (sel => this.SendCheckRegister(ntc_data, call_back)));
          break;
        default:
          this.OpenYesNoDialog(ret.Error, (Action<string>) (sel =>
          {
            if ("YES" == sel)
              this.SendCheckRegister(ntc_data, call_back);
            else
              Application.Quit();
          }));
          break;
      }
    }));
  }

  public void SendRegistCreate(Action<bool> call_back)
  {
    RegistCreateSendParam postData = new RegistCreateSendParam();
    postData.d = NetworkNative.getUniqueDeviceId();
    postData.SetAttribute(Native.GetInstallReferrer());
    bool is_success = false;
    Protocol.Send<RegistCreateSendParam, RegistCreateModel>(RegistCreateModel.URL, postData, (Action<RegistCreateModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo);
        MonoBehaviourSingleton<UserInfoManager>.I.userIdHash = ret.result.userIdHash;
        this.SaveAccount(ret.result.uh);
        if (ret.result.guestUser)
        {
          MonoBehaviourSingleton<AppMain>.I.Reset();
        }
        else
        {
          MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Account_Register", "Account", new Dictionary<string, object>()
          {
            {
              "fb_login",
              (object) "no"
            },
            {
              "colopl_login",
              (object) "no"
            },
            {
              "guest_login",
              (object) "yes"
            }
          });
          MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_1_login_screen, "Tutorial");
          Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_1_login_screen.ToString()));
          MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_1_login_screen, "Tutorial");
        }
        call_back(is_success);
      }
      else
        this.OpenYesNoDialog(ret.Error, (Action<string>) (sel =>
        {
          if ("YES" == sel)
          {
            MonoBehaviourSingleton<NetworkManager>.I.tokenTemp = (string) null;
            this.SendRegistCreate(call_back);
          }
          else
            Application.Quit();
        }));
    }));
  }

  private void RecommendUpdateCheck(System.Action call_back)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.Get(STRING_CATEGORY.COMMON, 11U), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101U), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102U)), (Action<string>) (sel =>
    {
      if ("YES" == sel)
        Native.launchMyselfMarket();
      call_back();
    }), true);
  }

  public List<LoginBonus> logInBonus { get; private set; }

  public int logInBonusLimitedCount { get; private set; }

  public void SendLogInBonus(Action<bool> callback)
  {
    this.logInBonus = (List<LoginBonus>) null;
    this.logInBonusLimitedCount = 0;
    GameSaveData.instance.showIAPAdsPop = string.Empty;
    Protocol.SendAsync<LoginBonusModel>(LoginBonusModel.URL, (Action<LoginBonusModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.logInBonus = ret.result;
        GameSaveData.instance.logInBonus = ret.result;
        GameSaveData.Save();
        int index = 0;
        for (int count = this.logInBonus.Count; index < count; ++index)
        {
          if (this.logInBonus[index].priority > 0)
            ++this.logInBonusLimitedCount;
        }
        this.IsRecvLogInBonus = true;
      }
      if (callback == null)
        return;
      callback(flag);
    }));
  }

  public bool IsRecvLogInBonus { get; private set; }

  public void DisplayLogInBonusSection() => this.IsRecvLogInBonus = false;

  public void SendRegistCreateRobAccount(
    string email,
    string password,
    string confirmPassword,
    int secretQuestionType,
    string secretQuestionAnswer,
    Action<bool> call_back)
  {
    RegistCreateRobAccountModel.RequestSendForm postData = new RegistCreateRobAccountModel.RequestSendForm();
    postData.email = email;
    postData.password = password;
    postData.confirmPassword = confirmPassword;
    postData.secretQuestionType = secretQuestionType;
    postData.secretQuestionAnswer = secretQuestionAnswer;
    bool is_success = false;
    Protocol.Send<RegistCreateRobAccountModel.RequestSendForm, RegistCreateRobAccountModel>(RegistCreateRobAccountModel.URL, postData, (Action<RegistCreateRobAccountModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result);
        MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Account_Register", "Account", new Dictionary<string, object>()
        {
          {
            "fb_login",
            (object) "no"
          },
          {
            "colopl_login",
            (object) "yes"
          },
          {
            "guest_login",
            (object) "no"
          }
        });
      }
      call_back(is_success);
    }));
  }

  public void SendRegistAuthRob(string email, string password, Action<bool> call_back, string uid = "")
  {
    MonoBehaviourSingleton<NetworkManager>.I.tokenTemp = (string) null;
    RegistAuthRobModel.RequestSendForm postData = new RegistAuthRobModel.RequestSendForm();
    postData.email = email;
    if (!string.IsNullOrEmpty(password))
      postData.password = password;
    postData.uid = uid;
    postData.d = NetworkNative.getUniqueDeviceId();
    bool is_success = false;
    Protocol.Send<RegistAuthRobModel.RequestSendForm, RegistAuthRobModel>(RegistAuthRobModel.URL, postData, (Action<RegistAuthRobModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo);
        MonoBehaviourSingleton<UserInfoManager>.I.SetNewsID(ret.result.newsId);
        MonoBehaviourSingleton<UserInfoManager>.I.userIdHash = ret.result.userIdHash;
        this.SaveAccount(ret.result.uh);
      }
      call_back(is_success);
    }));
  }

  public void SendRegistChangePasswordRob(
    string currentPassword,
    string newPassword,
    string newPasswordConfirm,
    Action<bool> call_back)
  {
    RegistChangePasswordRobModel.RequestSendForm postData = new RegistChangePasswordRobModel.RequestSendForm();
    postData.currentPassword = currentPassword;
    postData.newPassword = newPassword;
    postData.newPasswordConfirm = newPasswordConfirm;
    bool is_success = false;
    Protocol.Send<RegistChangePasswordRobModel.RequestSendForm, RegistChangePasswordRobModel>(RegistChangePasswordRobModel.URL, postData, (Action<RegistChangePasswordRobModel>) (ret =>
    {
      if (ret.Error == Error.None)
        is_success = true;
      call_back(is_success);
    }));
  }

  public void SendRegistChangeSecretQuestion(
    string password,
    int secretQuestionType,
    string secretQuestionAnswer,
    Action<bool> call_back)
  {
    RegistChangeSecretQuestionModel.RequestSendForm postData = new RegistChangeSecretQuestionModel.RequestSendForm();
    postData.password = password;
    postData.secretQuestionType = secretQuestionType;
    postData.secretQuestionAnswer = secretQuestionAnswer;
    bool is_success = false;
    Protocol.Send<RegistChangeSecretQuestionModel.RequestSendForm, RegistChangeSecretQuestionModel>(RegistChangeSecretQuestionModel.URL, postData, (Action<RegistChangeSecretQuestionModel>) (ret =>
    {
      if (ret.Error == Error.None)
        is_success = true;
      call_back(is_success);
    }));
  }

  public void SendRegistCreateGoogleAccount(
    string account,
    string key,
    string password,
    string confirmPassword,
    Action<bool> call_back)
  {
    RegistCreateGoogleAccountModel.RequestSendForm postData = new RegistCreateGoogleAccountModel.RequestSendForm();
    postData.account = account;
    postData.accountKey = key;
    postData.password = password;
    postData.confirmPassword = confirmPassword;
    bool is_success = false;
    Protocol.Send<RegistCreateGoogleAccountModel.RequestSendForm, RegistCreateGoogleAccountModel>(RegistCreateGoogleAccountModel.URL, postData, (Action<RegistCreateGoogleAccountModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result);
      }
      call_back(is_success);
    }));
  }

  public void SendRegistAuthGoogle(
    string account,
    string key,
    string password,
    Action<bool> call_back)
  {
    MonoBehaviourSingleton<NetworkManager>.I.tokenTemp = (string) null;
    RegistAuthGoogleModel.RequestSendForm postData = new RegistAuthGoogleModel.RequestSendForm();
    postData.account = account;
    postData.accountKey = key;
    postData.password = password;
    postData.d = NetworkNative.getUniqueDeviceId();
    postData.purchasetype = 0;
    bool is_success = false;
    Protocol.Send<RegistAuthGoogleModel.RequestSendForm, RegistAuthGoogleModel>(RegistAuthGoogleModel.URL, postData, (Action<RegistAuthGoogleModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo);
        MonoBehaviourSingleton<UserInfoManager>.I.SetNewsID(ret.result.newsId);
        MonoBehaviourSingleton<UserInfoManager>.I.userIdHash = ret.result.userIdHash;
        this.SaveAccount(ret.result.uh);
      }
      call_back(is_success);
    }));
  }

  public void SendRegistChangePasswordGoogle(
    string currentPassword,
    string newPassword,
    string newPasswordConfirm,
    Action<bool> call_back)
  {
    RegistChangePasswordGoogleModel.RequestSendForm postData = new RegistChangePasswordGoogleModel.RequestSendForm();
    postData.currentPassword = currentPassword;
    postData.newPassword = newPassword;
    postData.newPasswordConfirm = newPasswordConfirm;
    bool is_success = false;
    Protocol.Send<RegistChangePasswordGoogleModel.RequestSendForm, RegistChangePasswordGoogleModel>(RegistChangePasswordGoogleModel.URL, postData, (Action<RegistChangePasswordGoogleModel>) (ret =>
    {
      if (ret.Error == Error.None)
        is_success = true;
      call_back(is_success);
    }));
  }

  public void SendRegistAuthFacebook(string access_token, Action<bool> call_back, string uid = null)
  {
    RegistAuthFacebookModel.RequestSendForm postData = new RegistAuthFacebookModel.RequestSendForm();
    postData.accessToken = access_token;
    if (!string.IsNullOrEmpty(uid))
      postData.uid = uid;
    postData.d = NetworkNative.getUniqueDeviceId();
    bool is_success = false;
    Protocol.Send<RegistAuthFacebookModel.RequestSendForm, RegistAuthFacebookModel>(RegistAuthFacebookModel.URL, postData, (Action<RegistAuthFacebookModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo);
        MonoBehaviourSingleton<UserInfoManager>.I.SetNewsID(ret.result.newsId);
        this.SaveAccount(ret.result.uh);
        MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Account_Register", "Account", new Dictionary<string, object>()
        {
          {
            "fb_login",
            (object) "yes"
          },
          {
            "colopl_login",
            (object) "no"
          },
          {
            "guest_login",
            (object) "no"
          }
        });
      }
      call_back(is_success);
    }));
  }

  public void SendRegistLinkFacebook(
    string access_token,
    Action<bool, RegistLinkFacebookModel> call_back)
  {
    RegistLinkFacebookModel.RequestSendForm postData = new RegistLinkFacebookModel.RequestSendForm();
    postData.accessToken = access_token;
    bool is_success = false;
    Protocol.Send<RegistLinkFacebookModel.RequestSendForm, RegistLinkFacebookModel>(RegistLinkFacebookModel.URL, postData, (Action<RegistLinkFacebookModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result);
      }
      call_back(is_success, ret);
    }));
  }

  public void SendRegistOverrideFacebook(string access_token, Action<bool> call_back)
  {
    RegistLinkFacebookModel.RequestOverrideSendForm postData = new RegistLinkFacebookModel.RequestOverrideSendForm();
    postData.accessToken = access_token;
    postData.overwriteOldData = 1;
    bool is_success = false;
    Protocol.Send<RegistLinkFacebookModel.RequestOverrideSendForm, RegistLinkFacebookModel>(RegistLinkFacebookModel.URL, postData, (Action<RegistLinkFacebookModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result);
      }
      call_back(is_success);
    }));
  }

  public void SendRegistUnlinkFacebook(Action<bool> call_back)
  {
    bool is_success = false;
    Protocol.Send<RegistUnlinkFacebookModel>(RegistUnlinkFacebookModel.URL, (Action<RegistUnlinkFacebookModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result);
      }
      call_back(is_success);
    }));
  }

  public void SendTrackInviteFacebook(
    string access_token,
    List<string> list,
    Action<bool> call_back)
  {
    TrackFriendInviteModel.SendForm postData = new TrackFriendInviteModel.SendForm();
    postData.listFriend = list;
    bool is_success = false;
    Protocol.Send<TrackFriendInviteModel.SendForm, TrackFriendInviteModel>(TrackFriendInviteModel.URL, postData, (Action<TrackFriendInviteModel>) (ret =>
    {
      if (ret.Error == Error.None)
        is_success = true;
      call_back(is_success);
    }));
  }

  public void SendRegistLogout(Action<bool, RegistLogoutModel> call_back)
  {
    bool is_success = false;
    Protocol.Send<RegistLogoutModel>(RegistLogoutModel.URL, (Action<RegistLogoutModel>) (ret =>
    {
      if (ret.Error == Error.None)
        is_success = true;
      call_back(is_success, ret);
    }));
  }

  public void SendLinkRob(string email, string password, Action<bool, LinkRobModel> call_back)
  {
    LinkRobModel.RequestSendForm postData = new LinkRobModel.RequestSendForm();
    postData.email = email;
    postData.password = password;
    bool is_success = false;
    Protocol.Send<LinkRobModel.RequestSendForm, LinkRobModel>(LinkRobModel.URL, postData, (Action<LinkRobModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result);
      }
      call_back(is_success, ret);
    }));
  }

  public void SendRegistIgnoreCloudData(string email, Action<bool> call_back)
  {
    RegistLinkRobIgnoreCloudModel.RequestSendForm postData = new RegistLinkRobIgnoreCloudModel.RequestSendForm();
    postData.email = email;
    bool is_success = false;
    Protocol.Send<RegistLinkRobIgnoreCloudModel.RequestSendForm, RegistLinkRobIgnoreCloudModel>(RegistLinkRobIgnoreCloudModel.URL, postData, (Action<RegistLinkRobIgnoreCloudModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result);
      }
      call_back(is_success);
    }));
  }

  public void SendLinkRobWithCloudData(string email, Action<bool> call_back)
  {
    RegistLinkRobUseCloudDataModel.RequestSendForm postData = new RegistLinkRobUseCloudDataModel.RequestSendForm();
    postData.email = email;
    postData.d = NetworkNative.getUniqueDeviceId();
    bool is_success = false;
    Protocol.Send<RegistLinkRobUseCloudDataModel.RequestSendForm, RegistLinkRobUseCloudDataModel>(RegistLinkRobUseCloudDataModel.URL, postData, (Action<RegistLinkRobUseCloudDataModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        is_success = true;
        MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo);
        MonoBehaviourSingleton<UserInfoManager>.I.SetNewsID(ret.result.newsId);
        this.SaveAccount(ret.result.uh);
      }
      call_back(is_success);
    }));
  }

  private void OpenMessageDialog(Error msg_id, Action<string> callback)
  {
    GameSceneEvent.PushStay();
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.GetErrorMessage((uint) msg_id)), (Action<string>) (ret =>
    {
      GameSceneEvent.PopStay();
      callback(ret);
    }), true);
  }

  private void OpenYesNoDialog(Error msg_id, Action<string> callback)
  {
    GameSceneEvent.PushStay();
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage((uint) msg_id), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110U), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 112U /*0x70*/)), (Action<string>) (ret =>
    {
      GameSceneEvent.PopStay();
      callback(ret);
    }), true);
  }

  public void SetLoginBonusFromCache(List<LoginBonus> cacheLogInBonus)
  {
    this.logInBonusLimitedCount = 0;
    this.logInBonus = cacheLogInBonus;
    int index = 0;
    for (int count = this.logInBonus.Count; index < count; ++index)
    {
      if (this.logInBonus[index].priority > 0)
        ++this.logInBonusLimitedCount;
    }
    this.IsRecvLogInBonus = true;
  }

  public bool ChangingServer() => !string.IsNullOrEmpty(MonoBehaviourSingleton<AppMain>.I.uid);

  public void SendChangeServerInfo(Action<bool> call_back)
  {
    if (!string.IsNullOrEmpty(MonoBehaviourSingleton<AppMain>.I.email))
      this.SendRegistAuthRob(MonoBehaviourSingleton<AppMain>.I.email, (string) null, call_back, MonoBehaviourSingleton<AppMain>.I.uid);
    else if (MonoBehaviourSingleton<FBManager>.I.isLoggedIn)
      this.SendRegistAuthFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, call_back, MonoBehaviourSingleton<AppMain>.I.uid);
    else
      MonoBehaviourSingleton<FBManager>.I.LoginWithReadPermission((Action<bool, string>) ((success, b) =>
      {
        if (success)
          this.SendRegistAuthFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, call_back, MonoBehaviourSingleton<AppMain>.I.uid);
        else
          call_back(false);
      }));
  }

  [Serializable]
  public class Account
  {
    public string token;
    public string userHash;

    public bool IsRegist() => !string.IsNullOrEmpty(this.token);

    public override string ToString() => $"token={this.token},userHash={this.userHash}";
  }
}
