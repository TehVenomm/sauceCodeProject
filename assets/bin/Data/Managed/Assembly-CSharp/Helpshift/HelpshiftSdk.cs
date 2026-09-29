// Decompiled with JetBrains decompiler
// Type: Helpshift.HelpshiftSdk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace Helpshift;

public class HelpshiftSdk
{
  public const string HS_RATE_ALERT_CLOSE = "HS_RATE_ALERT_CLOSE";
  public const string HS_RATE_ALERT_FEEDBACK = "HS_RATE_ALERT_FEEDBACK";
  public const string HS_RATE_ALERT_SUCCESS = "HS_RATE_ALERT_SUCCESS";
  public const string HS_RATE_ALERT_FAIL = "HS_RATE_ALERT_FAIL";
  public const string HSTAGSKEY = "hs-tags";
  public const string HSCUSTOMMETADATAKEY = "hs-custom-metadata";
  public const string UNITY_GAME_OBJECT = "unityGameObject";
  public const string ENABLE_IN_APP_NOTIFICATION = "enableInAppNotification";
  public const string ENABLE_DEFAULT_FALLBACK_LANGUAGE = "enableDefaultFallbackLanguage";
  public const string ENABLE_LOGGING = "enableLogging";
  public const string ENABLE_INBOX_POLLING = "enableInboxPolling";
  public const string ENABLE_AUTOMATIC_THEME_SWITCHING = "enableAutomaticThemeSwitching";
  public const string DISABLE_ENTRY_EXIT_ANIMATIONS = "disableEntryExitAnimations";
  public const string HSCUSTOMISSUEFIELDKEY = "hs-custom-issue-field";
  public const string HSTAGSMATCHINGKEY = "withTagsMatching";
  public const string CONTACT_US_ALWAYS = "always";
  public const string CONTACT_US_NEVER = "never";
  public const string CONTACT_US_AFTER_VIEWING_FAQS = "after_viewing_faqs";
  public const string CONTACT_US_AFTER_MARKING_ANSWER_UNHELPFUL = "after_marking_answer_unhelpful";
  public const string HSUserAcceptedTheSolution = "User accepted the solution";
  public const string HSUserRejectedTheSolution = "User rejected the solution";
  public const string HSUserSentScreenShot = "User sent a screenshot";
  public const string HSUserReviewedTheApp = "User reviewed the app";
  public const string HsFlowTypeDefault = "defaultFlow";
  public const string HsFlowTypeConversation = "conversationFlow";
  public const string HsFlowTypeFaqs = "faqsFlow";
  public const string HsFlowTypeFaqSection = "faqSectionFlow";
  public const string HsFlowTypeSingleFaq = "singleFaqFlow";
  public const string HsFlowTypeNested = "dynamicFormFlow";
  public const string HsCustomContactUsFlows = "customContactUsFlows";
  public const string HsFlowType = "type";
  public const string HsFlowConfig = "config";
  public const string HsFlowData = "data";
  public const string HsFlowTitle = "title";
  private static HelpshiftSdk instance;
  private static HelpshiftAndroid nativeSdk;

  private HelpshiftSdk()
  {
  }

  public static HelpshiftSdk getInstance()
  {
    if (HelpshiftSdk.instance == null)
    {
      HelpshiftSdk.instance = new HelpshiftSdk();
      HelpshiftSdk.nativeSdk = new HelpshiftAndroid();
    }
    return HelpshiftSdk.instance;
  }

  public void install(
    string apiKey,
    string domainName,
    string appId,
    Dictionary<string, object> config = null)
  {
    if (config == null)
      config = new Dictionary<string, object>();
    config.Add("sdkType", (object) "unity");
    config.Add("pluginVersion", (object) "5.2.0");
    config.Add("runtimeVersion", (object) Application.unityVersion);
    config.Add("notificationIcon", (object) "push_icon");
    config.Add("largeNotificationIcon", (object) "large_icon");
    HelpshiftSdk.nativeSdk.install(apiKey, domainName, appId, config);
  }

  public void requestUnreadMessagesCount(bool isAsync)
  {
    HelpshiftSdk.nativeSdk.requestUnreadMessagesCount(isAsync);
  }

  [Obsolete]
  public void setNameAndEmail(string userName, string email)
  {
    HelpshiftSdk.nativeSdk.setNameAndEmail(userName, email);
  }

  [Obsolete]
  public void setUserIdentifier(string identifier)
  {
    HelpshiftSdk.nativeSdk.setUserIdentifier(identifier);
  }

  [Obsolete("Use the login(HelpshiftUser user) api instead.")]
  public void login(string identifier, string name, string email)
  {
    HelpshiftSdk.nativeSdk.login(identifier, name, email);
  }

  public void login(HelpshiftUser helpshiftUser) => HelpshiftSdk.nativeSdk.login(helpshiftUser);

  public void clearAnonymousUser() => HelpshiftSdk.nativeSdk.clearAnonymousUser();

  public void logout() => HelpshiftSdk.nativeSdk.logout();

  public void registerDeviceToken(string deviceToken)
  {
    HelpshiftSdk.nativeSdk.registerDeviceToken(deviceToken);
  }

  public void leaveBreadCrumb(string breadCrumb)
  {
    HelpshiftSdk.nativeSdk.leaveBreadCrumb(breadCrumb);
  }

  public void clearBreadCrumbs() => HelpshiftSdk.nativeSdk.clearBreadCrumbs();

  public void showConversation(Dictionary<string, object> configMap = null)
  {
    HelpshiftSdk.nativeSdk.showConversation(configMap);
  }

  public void showFAQSection(string sectionPublishId, Dictionary<string, object> configMap = null)
  {
    HelpshiftSdk.nativeSdk.showFAQSection(sectionPublishId, configMap);
  }

  public void showSingleFAQ(string questionPublishId, Dictionary<string, object> configMap = null)
  {
    HelpshiftSdk.nativeSdk.showSingleFAQ(questionPublishId, configMap);
  }

  public void showFAQs(Dictionary<string, object> configMap = null)
  {
    HelpshiftSdk.nativeSdk.showFAQs(configMap);
  }

  public void updateMetaData(Dictionary<string, object> metaData)
  {
    HelpshiftSdk.nativeSdk.updateMetaData(metaData);
  }

  public void handlePushNotification(Dictionary<string, object> pushNotificationData)
  {
    List<string> stringList = new List<string>();
    foreach (KeyValuePair<string, object> keyValuePair in pushNotificationData)
    {
      if (keyValuePair.Value == null)
        stringList.Add(keyValuePair.Key);
    }
    foreach (string key in stringList)
      pushNotificationData.Remove(key);
    HelpshiftSdk.nativeSdk.handlePushNotification(pushNotificationData);
  }

  public void showAlertToRateAppWithURL(string url)
  {
    HelpshiftSdk.nativeSdk.showAlertToRateAppWithURL(url);
  }

  public void setSDKLanguage(string locale) => HelpshiftSdk.nativeSdk.setSDKLanguage(locale);

  public void setTheme(string themeName) => HelpshiftSdk.nativeSdk.setTheme(themeName);

  public void registerDelegates() => HelpshiftSdk.nativeSdk.registerDelegates();

  public void showDynamicForm(string title, Dictionary<string, object>[] flows)
  {
    HelpshiftSdk.nativeSdk.showDynamicForm(title, flows);
  }

  public void onApplicationQuit() => HelpshiftSdk.nativeSdk.onApplicationQuit();

  public void checkIfConversationActive() => HelpshiftSdk.nativeSdk.checkIfConversationActive();
}
