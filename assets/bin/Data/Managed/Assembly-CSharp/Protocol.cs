// Decompiled with JetBrains decompiler
// Type: Protocol
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

#nullable disable
public class Protocol
{
  public static bool strict = true;
  private static int busy;
  private static bool isTry;
  private static bool isForce;
  private static string[] notBusyURLs = new string[1]
  {
    "ajax/clan-message/messageupdate"
  };

  public static bool isBusy
  {
    get
    {
      if (Protocol.busy > 0)
        return true;
      return MonoBehaviourSingleton<ProtocolManager>.IsValid() && MonoBehaviourSingleton<ProtocolManager>.I.isReserved;
    }
  }

  public static void Initialize()
  {
    Protocol.busy = 0;
    Protocol.isTry = false;
  }

  public static bool Resend(System.Action send_callback) => Protocol._Try(send_callback, true);

  public static bool Try(System.Action send_callback) => Protocol._Try(send_callback, false);

  private static bool _Try(System.Action send_callback, bool resend)
  {
    if (resend && Protocol.busy > 0 || !resend && Protocol.isBusy || MonoBehaviourSingleton<GameSceneManager>.I.isOpenCommonDialog)
      return false;
    Protocol.isTry = true;
    if (!Protocol.IsEnabledSend())
    {
      Protocol.isTry = false;
      return false;
    }
    send_callback();
    Protocol.isTry = false;
    return true;
  }

  public static void Force(System.Action send_callback)
  {
    Protocol.isForce = true;
    send_callback();
    Protocol.isForce = false;
  }

  public static void SendAsync<T>(string url, Action<T> call_back, string get_param = "") where T : BaseModel, new()
  {
    string token = Protocol.GenerateToken();
    Protocol.Send<T>(url, call_back, get_param, token, true);
  }

  public static void Send<T>(string url, Action<T> call_back, string get_param = "") where T : BaseModel, new()
  {
    string token = Protocol.GenerateToken();
    Protocol.Send<T>(url, call_back, get_param, token, false);
  }

  private static void Send<T>(
    string url,
    Action<T> callBack,
    string getParam,
    string token,
    bool isAsync)
    where T : BaseModel, new()
  {
    System.Action send = (System.Action) (() => Protocol.Send<T>(url, callBack, getParam, token, isAsync));
    if (!Protocol.Begin(url, send, isAsync))
      return;
    MonoBehaviourSingleton<NetworkManager>.I.Request<T>(Protocol.CheckURL(url), (Action<T>) (ret =>
    {
      if (!Protocol.End<T>(Protocol.CheckURL(url), ret, callBack, send, isAsync))
        return;
      callBack(ret);
    }), getParam, token);
  }

  public static void SendAsync<T1, T2>(
    string url,
    T1 postData,
    Action<T2> callBack,
    string getParam = "")
    where T2 : BaseModel, new()
  {
    string token = Protocol.GenerateToken();
    Protocol.Send<T1, T2>(url, postData, callBack, getParam, token, true);
  }

  public static void Send<T1, T2>(string url, T1 postData, Action<T2> callBack, string getParam = "") where T2 : BaseModel, new()
  {
    string token = Protocol.GenerateToken();
    Protocol.Send<T1, T2>(url, postData, callBack, getParam, token, false);
  }

  private static void Send<T1, T2>(
    string url,
    T1 postData,
    Action<T2> callBack,
    string getParam,
    string token,
    bool isAsync)
    where T2 : BaseModel, new()
  {
    System.Action send = (System.Action) (() => Protocol.Send<T1, T2>(url, postData, callBack, getParam, token, isAsync));
    if (!Protocol.Begin(url, send, isAsync))
      return;
    MonoBehaviourSingleton<NetworkManager>.I.Request<T1, T2>(Protocol.CheckURL(url), postData, (Action<T2>) (ret =>
    {
      if (!Protocol.End<T2>(Protocol.CheckURL(url), ret, callBack, send, isAsync))
        return;
      callBack(ret);
    }), getParam, token);
  }

  public static void SendAsync<T>(string url, WWWForm form, Action<T> callBack, string getParam = "") where T : BaseModel, new()
  {
    string token = Protocol.GenerateToken();
    Protocol.Send<T>(url, form, callBack, getParam, token, true);
  }

  public static void Send<T>(string url, WWWForm form, Action<T> call_back, string get_param = "") where T : BaseModel, new()
  {
    string token = Protocol.GenerateToken();
    Protocol.Send<T>(url, form, call_back, get_param, token, false);
  }

  private static void Send<T>(
    string url,
    WWWForm form,
    Action<T> call_back,
    string get_param,
    string token,
    bool isAsync)
    where T : BaseModel, new()
  {
    System.Action send = (System.Action) (() => Protocol.Send<T>(url, form, call_back, get_param, token, isAsync));
    if (!Protocol.Begin(url, send, isAsync))
      return;
    MonoBehaviourSingleton<NetworkManager>.I.RequestForm<T>(Protocol.CheckURL(url), form, (Action<T>) (ret =>
    {
      if (!Protocol.End<T>(Protocol.CheckURL(url), ret, call_back, send, isAsync))
        return;
      call_back(ret);
    }), get_param, token);
  }

  private static string CheckURL(string url) => url;

  private static bool IsEnabledSend()
  {
    if (!AppMain.isInitialized)
      return true;
    if (!MonoBehaviourSingleton<UIManager>.I.IsDisable())
    {
      if (!Protocol.isTry && !GameSceneEvent.IsStay())
        return false;
    }
    else if (!GameSceneEvent.IsStay() && !MonoBehaviourSingleton<GameSceneManager>.I.isWaiting)
      return false;
    return true;
  }

  private static bool Begin(string url, System.Action send, bool isAsync)
  {
    if (!Protocol.isForce)
    {
      if (Protocol.isBusy || MonoBehaviourSingleton<GameSceneManager>.I.isOpenCommonDialog)
      {
        if (MonoBehaviourSingleton<ProtocolManager>.IsValid())
          MonoBehaviourSingleton<ProtocolManager>.I.Reserve(send);
        return false;
      }
      if (Protocol.strict && !Protocol.IsEnabledSend())
      {
        Log.Error(LOG.NETWORK, "Protocol : Send Error : {0}", (object) url);
        return false;
      }
    }
    if (!((IEnumerable<string>) Protocol.notBusyURLs).Contains<string>(url) && !isAsync)
      Protocol.SetBusy(1);
    return true;
  }

  private static void SetBusy(int v)
  {
    Protocol.busy += v;
    bool is_disable = Protocol.busy != 0;
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Protocol.strict && is_disable)
      return;
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.PROTOCOL, is_disable);
  }

  private static bool End<T>(
    string url,
    T ret,
    Action<T> call_back,
    System.Action retry_action,
    bool isAsync)
    where T : BaseModel
  {
    if (!((IEnumerable<string>) Protocol.notBusyURLs).Contains<string>(url) && !isAsync)
      Protocol.SetBusy(-1);
    int code = ret.error;
    bool flag1 = MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.IsTutorialErrorResend();
    if (code == 0)
    {
      if (flag1)
        GameSceneEvent.PopStay();
      if (Utility.IsExist((ICollection) ret.diff) && MonoBehaviourSingleton<ProtocolManager>.IsValid())
        MonoBehaviourSingleton<ProtocolManager>.I.OnDiff(ret.diff[0]);
      return true;
    }
    bool flag2 = false;
    if (code == 31007)
      flag2 = true;
    if (flag2)
    {
      string errorMessage = StringTable.GetErrorMessage((uint) code);
      GameSceneEvent.PushStay();
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage), (Action<string>) (btn =>
      {
        GameSceneEvent.PopStay();
        if (code == 1003)
          Native.launchMyselfMarket();
        MonoBehaviourSingleton<AppMain>.I.Reset();
      }), true, code);
      return false;
    }
    if (code < 100000)
    {
      bool flag3 = false;
      switch (code)
      {
        case 1002:
        case 1003:
        case 1020:
        case 1023 /*0x03FF*/:
        case 2001:
        case 66003:
          flag3 = true;
          break;
      }
      if ((!flag3 || (object) ret is CheckRegisterModel) && GameSceneGlobalSettings.IsNonPopupError((BaseModel) ret))
      {
        if (code == 1002)
          Protocol.OpenMaintenancePopup<T>(ret);
        return true;
      }
      string errorMessage = StringTable.GetErrorMessage((uint) code);
      MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("error_code_" + (object) code, "Error");
      if (flag3 && code == 66003)
      {
        GameSceneEvent.PushStay();
        MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage), (Action<string>) (btn =>
        {
          GameSceneEvent.PopStay();
          MonoBehaviourSingleton<AppMain>.I.Reset();
        }), true, code);
      }
      else if (flag3 && code != 1002)
      {
        GameSceneEvent.PushStay();
        MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, errorMessage), (Action<string>) (btn =>
        {
          GameSceneEvent.PopStay();
          if (code == 1003)
            Native.launchMyselfMarket();
          MonoBehaviourSingleton<AppMain>.I.Reset();
        }), true, code);
      }
      else if (flag3 && code == 1002)
        Protocol.OpenMaintenancePopup<T>(ret);
      else if (!MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !flag1)
      {
        if (code == 42002)
        {
          if (MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.IsRegistered() && ClanMatchingManager.IsValidInClan())
            MonoBehaviourSingleton<ClanMatchingManager>.I.Kick(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
          return false;
        }
        GameSceneEvent.PushStay();
        MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage), (Action<string>) (btn =>
        {
          GameSceneEvent.PopStay();
          call_back(ret);
          if (code != 74001 && code != 74002)
            return;
          Debug.Log((object) "kciked");
          MonoBehaviourSingleton<AppMain>.I.ChangeScene("", "HomeTop", (System.Action) (() => MonoBehaviourSingleton<GuildManager>.I.UpdateGuild((GuildModel.Guild) null)));
        }), true, code);
      }
      else
      {
        if (flag1 && GameSceneEvent.IsStay())
          GameSceneEvent.PushStay();
        if (code == 74001 || code == 74002)
          MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage), (Action<string>) (btn =>
          {
            GameSceneEvent.PopStay();
            call_back(ret);
            if (code != 74001 && code != 74002)
              return;
            MonoBehaviourSingleton<AppMain>.I.Reset();
          }), true, code);
        else
          MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, errorMessage, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110U), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111U)), (Action<string>) (btn =>
          {
            if (btn == "YES")
              retry_action();
            else
              MonoBehaviourSingleton<AppMain>.I.Reset();
          }), true, code);
      }
      return false;
    }
    uint id = 1001;
    if (code == 200000)
      MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("error_200000", "Functionality");
    else
      MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("error_code_" + (object) code, "Error");
    string _text = StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, id, (object) code);
    GameSceneEvent.PushStay();
    if (code == 129903)
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Format(STRING_CATEGORY.ERROR_DIALOG, 72003U, (object) code)), (Action<string>) (btn =>
      {
        GameSceneEvent.PopStay();
        call_back(ret);
      }), true, code);
    else if (code > 500000 && code < 600000)
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Format(STRING_CATEGORY.ERROR_DIALOG, (uint) code, (object) code)), (Action<string>) (btn =>
      {
        GameSceneEvent.PopStay();
        call_back(ret);
      }), true, code);
    else if (code > 600000 && code < 700000)
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.Format(STRING_CATEGORY.ERROR_DIALOG, (uint) code, (object) code), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110U), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111U)), (Action<string>) (btn =>
      {
        GameSceneEvent.PopStay();
        if (btn == "YES")
          retry_action();
        else
          MonoBehaviourSingleton<AppMain>.I.Reset();
      }), true, code);
    else
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, _text, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110U), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111U)), (Action<string>) (btn =>
      {
        GameSceneEvent.PopStay();
        if (btn == "YES")
          retry_action();
        else
          MonoBehaviourSingleton<AppMain>.I.Reset();
      }), true, code);
    return false;
  }

  private static void OpenMaintenancePopup<T>(T ret) where T : BaseModel
  {
    GameSceneEvent.PushStay();
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage(1002U), StringTable.Get(STRING_CATEGORY.ERROR_DIALOG, 100202U), StringTable.Get(STRING_CATEGORY.ERROR_DIALOG, 100201U), data: (object) ret.infoError), (Action<string>) (btn =>
    {
      if (btn == "YES")
        Native.OpenURL("https://www.facebook.com/DragonProject/");
      GameSceneEvent.PopStay();
      MonoBehaviourSingleton<AppMain>.I.Reset();
      Native.applicationQuit();
    }), true, ret.error);
  }

  public static string GenerateToken()
  {
    string str1 = DateTime.Now.ToString("yyyyMMddhhmmssfff");
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
    {
      string str2 = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString();
      if (!string.IsNullOrEmpty(str2))
      {
        byte[] bytes = Encoding.UTF8.GetBytes(str2 + str1);
        return string.Concat(((IEnumerable<byte>) MD5.Create().ComputeHash(bytes)).Select<byte, string>((Func<byte, string>) (i => i.ToString("x2"))).ToArray<string>());
      }
    }
    return Guid.NewGuid().ToString("N");
  }
}
