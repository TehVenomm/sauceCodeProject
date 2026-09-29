// Decompiled with JetBrains decompiler
// Type: HelpshiftWrapper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Helpshift;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class HelpshiftWrapper
{
  private HelpshiftSdk _support;
  private HelpshiftSettings settings;
  private int countMess;

  public HelpshiftSdk Support => this._support;

  public HelpshiftWrapper(string ObjectRecvCallBack, HelpshiftUser user)
  {
    this._support = HelpshiftSdk.getInstance();
    this._support.logout();
    this.settings = HelpshiftSettings.Instance;
    this._support.login(user);
    if (!Object.op_Inequality((Object) this.settings, (Object) null))
      return;
    this._support.install(this.settings.apiKey, this.settings.domainName, this.settings.androidAppId, this.getInstallConfig(ObjectRecvCallBack));
  }

  public void Start()
  {
    this._support.requestUnreadMessagesCount(true);
    this._support.checkIfConversationActive();
    this._support.setTheme("Helpshift.Theme.DayNight.Light");
  }

  public void ShowCustomContactUs(
    string account_id,
    string player_name,
    string dpro_id,
    string server,
    bool is_vip)
  {
    Dictionary<string, object> configMap = new Dictionary<string, object>();
    configMap.Add("type", (object) "conversationFlow");
    configMap.Add("title", (object) "Converse");
    configMap.Add("config", (object) new Dictionary<string, object>()
    {
      {
        "requireEmail",
        (object) this.settings.RequireEmail
      },
      {
        "hideNameAndEmail",
        (object) this.settings.HideNameAndEmail
      },
      {
        "showSearchOnNewConversation",
        (object) this.settings.ShowSearchOnNewConversation
      }
    });
    configMap.Add("hs-custom-issue-field", (object) new Dictionary<string, string[]>()
    {
      {
        nameof (account_id),
        new string[2]{ "sl", account_id }
      },
      {
        nameof (player_name),
        new string[2]{ "sl", player_name }
      },
      {
        nameof (dpro_id),
        new string[2]{ "sl", dpro_id }
      },
      {
        nameof (server),
        new string[2]{ "dd", server }
      },
      {
        "vip_status",
        new string[2]{ "b", is_vip ? "true" : "false" }
      }
    });
    this._support.clearAnonymousUser();
    if (this.countMess == 0)
      this._support.showFAQs(configMap);
    else
      this._support.showConversation(configMap);
  }

  public void ShowConversationClick()
  {
    Debug.Log((object) "Show Conversation clicked !!");
    this._support.showConversation(this.getApiConfig());
  }

  protected Dictionary<string, object>[] getDynamicFlows()
  {
    return new Dictionary<string, object>[2]
    {
      new Dictionary<string, object>()
      {
        {
          "type",
          (object) "conversationFlow"
        },
        {
          "title",
          (object) "Converse"
        },
        {
          "config",
          (object) new Dictionary<string, object>()
          {
            {
              "conversationPrefillText",
              (object) "This is from dynamic"
            },
            {
              "hideNameAndEmail",
              (object) "no"
            },
            {
              "showSearchOnNewConversation",
              (object) "yes"
            }
          }
        }
      },
      new Dictionary<string, object>()
      {
        {
          "type",
          (object) "faqsFlow"
        },
        {
          "title",
          (object) "FAQs"
        }
      }
    };
  }

  private Dictionary<string, object> getApiConfig()
  {
    Dictionary<string, object> apiConfig = new Dictionary<string, object>();
    switch (this.settings.ContactUs)
    {
      case HelpshiftSettings.CONTACT_US.CONTACT_US_ALWAYS:
        apiConfig.Add("enableContactUs", (object) "always");
        break;
      case HelpshiftSettings.CONTACT_US.CONTACT_US_NEVER:
        apiConfig.Add("enableContactUs", (object) "never");
        break;
      case HelpshiftSettings.CONTACT_US.CONTACT_US_AFTER_VIEWING_FAQS:
        apiConfig.Add("enableContactUs", (object) "after_viewing_faqs");
        break;
      case HelpshiftSettings.CONTACT_US.CONTACT_US_AFTER_MARKING_ANSWER_UNHELPFUL:
        apiConfig.Add("enableContactUs", (object) "after_viewing_faqs");
        break;
    }
    apiConfig.Add("gotoConversationAfterContactUs", this.settings.GotoConversationAfterContactUs ? (object) "yes" : (object) "no");
    apiConfig.Add("requireEmail", this.settings.RequireEmail ? (object) "yes" : (object) "no");
    apiConfig.Add("hideNameAndEmail", this.settings.HideNameAndEmail ? (object) "yes" : (object) "no");
    apiConfig.Add("enableFullPrivacy", this.settings.FullPrivacy ? (object) "yes" : (object) "no");
    apiConfig.Add("showSearchOnNewConversation", this.settings.ShowSearchOnNewConversation ? (object) "yes" : (object) "no");
    apiConfig.Add("showConversationResolutionQuestion", this.settings.ShowConversationResolutionQuestion ? (object) "yes" : (object) "no");
    apiConfig.Add("enableTypingIndicator", this.settings.TypingIndicator ? (object) "yes" : (object) "no");
    apiConfig.Add("showConversationInfoScreen", this.settings.ShowConversationInfoScreen ? (object) "yes" : (object) "no");
    return apiConfig;
  }

  private Dictionary<string, object> getInstallConfig(string unityObj)
  {
    return new Dictionary<string, object>()
    {
      {
        "unityGameObject",
        (object) unityObj
      },
      {
        "enableInAppNotification",
        this.settings.InAppNotification ? (object) "yes" : (object) "no"
      },
      {
        "enableDefaultFallbackLanguage",
        this.settings.DefaultFallbackLanguage ? (object) "yes" : (object) "no"
      },
      {
        "disableEntryExitAnimations",
        this.settings.EntryExitAnimations ? (object) "no" : (object) "yes"
      },
      {
        "enableInboxPolling",
        this.settings.InboxPolling ? (object) "yes" : (object) "no"
      },
      {
        "enableLogging",
        this.settings.Logging ? (object) "yes" : (object) "no"
      },
      {
        "enableAutomaticThemeSwitching",
        this.settings.AutomaticThemeSwitching ? (object) "yes" : (object) "no"
      },
      {
        "screenOrientation",
        (object) (int) this.settings.orientation
      }
    };
  }

  public void SetUnReadMessCount(string _count)
  {
    this.countMess = _count.ToInt32OrDefault();
    if (this.countMess >= 0)
      return;
    this.countMess = 0;
  }

  public void Logout() => this._support.logout();
}
