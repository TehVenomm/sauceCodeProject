// Decompiled with JetBrains decompiler
// Type: HelpshiftSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[CreateAssetMenu(fileName = "HelpshiftSettings", menuName = "ScriptableObject/Helpshift Settings")]
public class HelpshiftSettings : ScriptableObject
{
  private static HelpshiftSettings instance;
  private const string helpshiftAssetName = "HelpshiftSettings";
  private const string helpshiftPath = "Helpshift/Resources";
  public string apiKey;
  public string domainName;
  public string androidAppId;
  public string iosAppID;
  [Space]
  [Space]
  [Header("Install Configuration")]
  public bool InAppNotification;
  public bool DefaultFallbackLanguage;
  public bool EntryExitAnimations;
  public bool InboxPolling;
  public bool Logging;
  public bool AutomaticThemeSwitching;
  public HelpshiftSettings.ORIENTATION orientation;
  [Space]
  [Space]
  [Header("API Configuration")]
  public HelpshiftSettings.CONTACT_US ContactUs;
  public bool GotoConversationAfterContactUs;
  public bool RequireEmail;
  public bool HideNameAndEmail;
  public bool FullPrivacy;
  public bool ShowSearchOnNewConversation;
  public bool ShowConversationResolutionQuestion;
  public bool TypingIndicator;
  public bool ShowConversationInfoScreen;

  public static HelpshiftSettings Instance
  {
    get
    {
      HelpshiftSettings.instance = Resources.Load(nameof (HelpshiftSettings)) as HelpshiftSettings;
      return HelpshiftSettings.instance;
    }
  }

  [Serializable]
  public enum ORIENTATION
  {
    SCREEN_ORIENTATION_UNSPECIFIED = -1, // 0xFFFFFFFF
    SCREEN_ORIENTATION_LANDSCAPE = 0,
    SCREEN_ORIENTATION_PORTRAIT = 1,
  }

  [Serializable]
  public enum CONTACT_US
  {
    CONTACT_US_ALWAYS,
    CONTACT_US_NEVER,
    CONTACT_US_AFTER_VIEWING_FAQS,
    CONTACT_US_AFTER_MARKING_ANSWER_UNHELPFUL,
  }
}
