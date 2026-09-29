// Decompiled with JetBrains decompiler
// Type: Helpshift.HelpshiftAndroid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using HSMiniJSON;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace Helpshift;

public class HelpshiftAndroid
{
  private AndroidJavaClass jc;
  private AndroidJavaObject currentActivity;
  private AndroidJavaObject application;
  private AndroidJavaClass hsHelpshiftClass;

  public HelpshiftAndroid()
  {
    this.jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    this.currentActivity = ((AndroidJavaObject) this.jc).GetStatic<AndroidJavaObject>(nameof (currentActivity));
    this.application = this.currentActivity.Call<AndroidJavaObject>("getApplication", Array.Empty<object>());
    this.hsHelpshiftClass = new AndroidJavaClass("com.helpshift.HelpshiftUnityAPI");
  }

  public void install(
    string apiKey,
    string domain,
    string appId,
    Dictionary<string, object> configMap)
  {
    string str = Json.Serialize((object) configMap);
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (install), new object[5]
    {
      (object) this.application,
      (object) apiKey,
      (object) domain,
      (object) appId,
      (object) str
    });
    HelpshiftInternalLogger.d($"Install called : Domain : {domain}, Config : {str}");
  }

  public void requestUnreadMessagesCount(bool isAsync)
  {
    HelpshiftInternalLogger.d("Call requestUnreadMessagesCount: isAsync : " + isAsync.ToString());
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (requestUnreadMessagesCount), new object[1]
    {
      (object) isAsync
    });
  }

  [Obsolete]
  public void setNameAndEmail(string userName, string email)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (setNameAndEmail), new object[2]
    {
      (object) userName,
      (object) email
    });
  }

  [Obsolete]
  public void setUserIdentifier(string identifier)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (setUserIdentifier), new object[1]
    {
      (object) identifier
    });
  }

  public void registerDeviceToken(string deviceToken)
  {
    HelpshiftInternalLogger.d("Register device token :" + deviceToken);
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (registerDeviceToken), new object[2]
    {
      (object) this.currentActivity,
      (object) deviceToken
    });
  }

  public void leaveBreadCrumb(string breadCrumb)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (leaveBreadCrumb), new object[1]
    {
      (object) breadCrumb
    });
  }

  public void clearBreadCrumbs()
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (clearBreadCrumbs), Array.Empty<object>());
  }

  [Obsolete("Use the login(HelpshiftUser user) api instead.")]
  public void login(string identifier, string userName, string email)
  {
    HelpshiftInternalLogger.d("Login called : " + userName);
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (login), new object[3]
    {
      (object) identifier,
      (object) userName,
      (object) email
    });
  }

  public void login(HelpshiftUser helpshiftUser)
  {
    HelpshiftInternalLogger.d("Login called : " + helpshiftUser.name);
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic("loginHelpshiftUser", new object[1]
    {
      (object) this.jsonifyHelpshiftUser(helpshiftUser)
    });
  }

  public void clearAnonymousUser()
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (clearAnonymousUser), Array.Empty<object>());
  }

  public void logout()
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (logout), Array.Empty<object>());
  }

  private string serializeApiConfig(Dictionary<string, object> configMap)
  {
    return configMap != null ? Json.Serialize((object) this.cleanConfig(configMap)) : (string) null;
  }

  public void showConversation(Dictionary<string, object> configMap)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic("showConversationUnity", new object[2]
    {
      (object) this.currentActivity,
      (object) this.serializeApiConfig(configMap)
    });
  }

  public void showFAQSection(string sectionPublishId, Dictionary<string, object> configMap)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic("showFAQSectionUnity", new object[3]
    {
      (object) this.currentActivity,
      (object) sectionPublishId,
      (object) this.serializeApiConfig(configMap)
    });
  }

  public void showSingleFAQ(string questionPublishId, Dictionary<string, object> configMap)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic("showSingleFAQUnity", new object[3]
    {
      (object) this.currentActivity,
      (object) questionPublishId,
      (object) this.serializeApiConfig(configMap)
    });
  }

  public void showFAQs(Dictionary<string, object> configMap)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic("showFAQsUnity", new object[2]
    {
      (object) this.currentActivity,
      (object) this.serializeApiConfig(configMap)
    });
  }

  public void updateMetaData(Dictionary<string, object> metaData)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic("setMetaData", new object[1]
    {
      (object) Json.Serialize((object) metaData)
    });
  }

  private Dictionary<string, object> cleanConfig(Dictionary<string, object> configMap)
  {
    if (configMap.ContainsKey("customIssueFields"))
    {
      configMap["hs-custom-issue-field"] = configMap["customIssueFields"];
      configMap.Remove("customIssueFields");
    }
    return configMap;
  }

  public void handlePushNotification(Dictionary<string, object> pushNotificationData)
  {
    HelpshiftInternalLogger.d("Handle push notification : data :" + pushNotificationData.ToString());
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic("handlePush", new object[2]
    {
      (object) this.currentActivity,
      (object) Json.Serialize((object) pushNotificationData)
    });
  }

  public void showAlertToRateAppWithURL(string url)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic("showAlertToRateApp", new object[1]
    {
      (object) url
    });
  }

  public void registerDelegates()
  {
    HelpshiftInternalLogger.d("Registering delegates");
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (registerDelegates), Array.Empty<object>());
  }

  public void setSDKLanguage(string locale)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (setSDKLanguage), new object[1]
    {
      (object) locale
    });
  }

  public void setTheme(string themeResourceName)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (setTheme), new object[1]
    {
      (object) themeResourceName
    });
  }

  public void showDynamicForm(string title, Dictionary<string, object>[] flows)
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic("showDynamicFormFromDataJson", new object[3]
    {
      (object) this.currentActivity,
      (object) title,
      (object) Json.Serialize((object) flows)
    });
  }

  public void checkIfConversationActive()
  {
    ((AndroidJavaObject) this.hsHelpshiftClass).CallStatic(nameof (checkIfConversationActive), Array.Empty<object>());
  }

  public void onApplicationQuit() => HelpshiftInternalLogger.d(nameof (onApplicationQuit));

  private string jsonifyHelpshiftUser(HelpshiftUser helpshiftUser)
  {
    return Json.Serialize((object) new Dictionary<string, string>()
    {
      {
        "identifier",
        helpshiftUser.identifier
      },
      {
        "email",
        helpshiftUser.email
      },
      {
        "name",
        helpshiftUser.name
      },
      {
        "authToken",
        helpshiftUser.authToken
      }
    });
  }
}
