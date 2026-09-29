// Decompiled with JetBrains decompiler
// Type: WebViewManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class WebViewManager : MonoBehaviourSingleton<WebViewManager>
{
  public WebViewManager.UrlType urlType;
  private const string COOKIE_KEY_VERSION = "apv";
  private WebViewObject webViewObject;
  private Action<string> onClose;
  [SerializeField]
  private Rect m_Margine;

  public static string Url_News => NetworkManager.APP_HOST + WebViewManager.News;

  public static string Url_Help => NetworkManager.APP_HOST + WebViewManager.Help;

  public static string News => "news";

  public static string Help => "help";

  public static string Terms => "tos/terms";

  public static string Present => "tos/present";

  public static string Currency => "tos/currency";

  public static string Commercial => "tos/commercial";

  public static string Found => "tos/found";

  public static string GachaQuestList => "questitem/list";

  public static string PointShop => "pointshop/top";

  public static string GachaTicket => "tos/gachaticket";

  public static string lounge => "lounge/top";

  public static string BingoRule => "bingo/rule";

  public static string BingoReward => "bingo/reward";

  public static string Explore => "explore/help";

  public static string GuildRequest => "guild-request/top";

  public static string eula => "tos/eula";

  public static string GuildHint => "goclan";

  public static string FortuneWheel => "dragon-vault";

  public static string Wave => "wave/help";

  public static string Trial => "trial/help";

  public static string Arena => "arena/help";

  public static string Rush => "rush/help";

  public static string Carnival => "carnival/help";

  public static string ClanReward => "clan/reward";

  public static string Clan => "clan/help";

  public static string SeriesArena => "seriesarena/help";

  public static string NewsWithLinkParamFormat => "news/show?link={0}";

  public static string NewsWithLinkParamFormatFromInGame => "news/show?link={0}&at=1";

  public static string TradingPost => "tradingpost/help";

  public static string CreateNewsWithLinkParamUrl(string link_param)
  {
    return string.Format(WebViewManager.NewsWithLinkParamFormat, (object) link_param);
  }

  public void Open(string url, Action<string> _onClose = null)
  {
    Debug.Log((object) url);
    this.onClose = _onClose;
    this.webViewObject = ((Component) this).gameObject.AddComponent<WebViewObject>();
    this.webViewObject.Init();
    this.webViewObject.EvaluateJS($"var appVersion='{NetworkNative.getNativeVersionName()}';");
    this.webViewObject.SetCookie(NetworkManager.APP_HOST, "apv", NetworkNative.getNativeVersionName());
    if (MonoBehaviourSingleton<AccountManager>.I.account.token != "")
    {
      string[] strArray = MonoBehaviourSingleton<AccountManager>.I.account.token.Split('=');
      this.webViewObject.SetCookie(NetworkManager.APP_HOST, strArray[0], strArray[1]);
    }
    this.webViewObject.SetCookie(NetworkManager.APP_HOST, "dm", SystemInfo.deviceModel);
    this.webViewObject.LoadURL(url);
    this.webViewObject.SetVisibility(true);
    int num1 = Screen.width;
    int num2 = Screen.height;
    if (MonoBehaviourSingleton<AppMain>.IsValid())
    {
      num1 = MonoBehaviourSingleton<AppMain>.I.defaultScreenWidth;
      num2 = MonoBehaviourSingleton<AppMain>.I.defaultScreenHeight;
    }
    int left = (int) ((double) num1 * (double) ((Rect) ref this.m_Margine).xMin);
    int top = (int) ((double) num2 * (double) ((Rect) ref this.m_Margine).yMin);
    int right = (int) ((double) num1 * (double) ((Rect) ref this.m_Margine).width);
    int bottom = (int) ((double) num2 * (double) ((Rect) ref this.m_Margine).height);
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyWebView)
    {
      DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
      if (specialDeviceInfo != null)
      {
        switch (this.urlType)
        {
          case WebViewManager.UrlType.NEWS:
            if (SpecialDeviceManager.IsPortrait)
            {
              left = specialDeviceInfo.WebViewInfoPortrait.left;
              top = specialDeviceInfo.WebViewInfoPortrait.top;
              right = specialDeviceInfo.WebViewInfoPortrait.right;
              bottom = specialDeviceInfo.WebViewInfoPortrait.bottom;
              break;
            }
            left = specialDeviceInfo.WebViewInfoLandscape.left;
            top = specialDeviceInfo.WebViewInfoLandscape.top;
            right = specialDeviceInfo.WebViewInfoLandscape.right;
            bottom = specialDeviceInfo.WebViewInfoLandscape.bottom;
            Transform child = Utility.FindChild(((Component) this).gameObject.transform, "Window");
            if (Object.op_Inequality((Object) child, (Object) null))
            {
              UIWidget component = ((Component) child).GetComponent<UIWidget>();
              if (Object.op_Inequality((Object) component, (Object) null))
              {
                component.leftAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.left;
                component.rightAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.right;
                component.bottomAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.bottom;
                component.topAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.top;
                Utility.UpdateAllAnchors(((Component) this).gameObject);
                break;
              }
              break;
            }
            break;
          case WebViewManager.UrlType.HELP:
            left = specialDeviceInfo.WebViewHelpPortrait.left;
            top = specialDeviceInfo.WebViewHelpPortrait.top;
            right = specialDeviceInfo.WebViewHelpPortrait.right;
            bottom = specialDeviceInfo.WebViewHelpPortrait.bottom;
            break;
        }
      }
    }
    this.webViewObject.SetMargins(left, top, right, bottom);
  }

  public void OnWebViewEvent(string msg)
  {
    if (string.IsNullOrEmpty(msg))
      return;
    if (msg.StartsWith("mailto:"))
    {
      Debug.Log((object) ("[mailto]:" + msg));
      Application.OpenURL(msg);
    }
    if (msg.StartsWith("browser:"))
    {
      string str = msg.Replace("browser:", "");
      Debug.Log((object) ("[browser]:" + str));
      Application.OpenURL(str);
    }
    if (msg.StartsWith("checkPurchase:"))
    {
      bool showErrorDialog = int.Parse(msg.Replace("checkPurchase:", "")) == 1;
      Debug.Log((object) ("[checkPurchase]:" + showErrorDialog.ToString()));
      Native.RestorePurchasedItem(showErrorDialog);
    }
    if (msg.StartsWith("openOpinionBox:"))
    {
      Debug.Log((object) "[openOpinionBox]:");
      this.Close(msg);
      MonoBehaviourSingleton<GameSceneManager>.I.OpinionBox();
    }
    if (msg.StartsWith("close:"))
    {
      Debug.Log((object) "[close]:");
      this.Close(msg);
    }
    if (msg.StartsWith("movie:"))
    {
      string str = msg.Replace("movie:", "");
      Debug.Log((object) ("[movie]:" + str));
      switch (str)
      {
        case "tutorial":
          this.webViewObject.onDestroy = (System.Action) (() => Utility.PlayFullScreenMovie("tutorial_move.mp4"));
          break;
        case "skill":
          this.webViewObject.onDestroy = (System.Action) (() => Utility.PlayFullScreenMovie("tutorial_skill.mp4"));
          break;
      }
      this.Close(msg);
    }
    if (!msg.StartsWith("goto:"))
      return;
    this.Close(msg);
    WebViewManager.ProcessGotoEvent(msg);
  }

  public static void ProcessGotoEvent(string msg)
  {
    string[] strArray1 = msg.Replace("goto:", "").Split('/');
    string str1 = strArray1[0];
    string s = strArray1.Length > 1 ? strArray1[1] : (string) null;
    Debug.Log((object) $"[goto]:{str1}/{s}");
    string goingHomeEvent = GameSection.GetGoingHomeEvent();
    switch (str1)
    {
      case "arena":
        EventData eventData = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50 ? new EventData("SELECT_ARENA", (object) null) : new EventData("SELECT_DISABLE_ARENA", (object) null);
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
        {
          new EventData(goingHomeEvent, (object) null),
          new EventData("TO_EVENT", (object) null),
          eventData
        });
        break;
      case "bingo":
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
        {
          new EventData(goingHomeEvent, (object) null),
          new EventData("BINGO", (object) true)
        });
        break;
      case "event_quest":
        int result1 = 0;
        EventData[] event_datas1;
        if (int.TryParse(s, out result1))
          event_datas1 = new EventData[3]
          {
            new EventData(goingHomeEvent, (object) null),
            new EventData("TO_EVENT", (object) null),
            new EventData("SELECT", (object) result1)
          };
        else
          event_datas1 = new EventData[2]
          {
            new EventData(goingHomeEvent, (object) null),
            new EventData("TO_EVENT", (object) null)
          };
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas1);
        break;
      case "event_trial":
        int result2 = 0;
        EventData[] event_datas2;
        if (int.TryParse(s, out result2))
          event_datas2 = new EventData[3]
          {
            new EventData(goingHomeEvent, (object) null),
            new EventData("TO_EVENT", (object) null),
            new EventData("SELECT_TRIAL", (object) result2)
          };
        else
          event_datas2 = new EventData[2]
          {
            new EventData(goingHomeEvent, (object) null),
            new EventData("TO_EVENT", (object) null)
          };
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas2);
        break;
      case "explore_quest":
        int result3 = 0;
        EventData[] event_datas3;
        if (int.TryParse(s, out result3))
          event_datas3 = new EventData[3]
          {
            new EventData(goingHomeEvent, (object) null),
            new EventData("EXPLORE", (object) null),
            new EventData("SELECT_EXPLORE", (object) result3)
          };
        else
          event_datas3 = new EventData[2]
          {
            new EventData(goingHomeEvent, (object) null),
            new EventData("EXPLORE", (object) null)
          };
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas3);
        break;
      case "gacha":
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
        {
          new EventData("MAIN_MENU_SHOP", (object) null)
        });
        break;
      case "gacha_quest":
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
        {
          new EventData(goingHomeEvent, (object) null),
          new EventData("GACHA_QUEST_COUNTER", (object) null),
          new EventData("TO_GACHA_QUEST_COUNTER", (object) null)
        });
        break;
      case "inn":
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
        {
          new EventData("MAIN_MENU_STUDIO", (object) null)
        });
        break;
      case "magi_gacha":
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
        {
          new EventData("MAIN_MENU_SHOP", (object) null),
          new EventData("MAGI_GACHA", (object) null)
        });
        break;
      case "point_shop":
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
        {
          new EventData(goingHomeEvent, (object) null),
          new EventData("POINT_SHOP_FROM_BUTTON", (object) null)
        });
        break;
      case "promotion":
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
        {
          new EventData(goingHomeEvent, (object) null),
          new EventData("FRIEND_PROMOTION", (object) null)
        });
        break;
      case "quest":
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
        {
          new EventData(goingHomeEvent, (object) null),
          new EventData("QUEST_COUNTER", (object) null)
        });
        break;
      case "wave_quest":
        int result4 = 0;
        EventData[] event_datas4;
        if (int.TryParse(s, out result4))
          event_datas4 = new EventData[3]
          {
            new EventData(goingHomeEvent, (object) null),
            new EventData("TO_EVENT", (object) null),
            new EventData("SELECT_WAVE", (object) result4)
          };
        else
          event_datas4 = new EventData[2]
          {
            new EventData(goingHomeEvent, (object) null),
            new EventData("TO_EVENT", (object) null)
          };
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas4);
        break;
      default:
        if (str1.StartsWith("login_bonus:"))
        {
          int result5;
          int.TryParse(str1.Replace("login_bonus:", ""), out result5);
          if (result5 == 0)
            break;
          MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
          {
            new EventData(goingHomeEvent, (object) null),
            new EventData("LIMITED_LOGIN_BONUS_VIEW", (object) result5)
          });
          break;
        }
        if (!str1.StartsWith("gacha_equip:"))
          break;
        string str2 = str1.Replace("gacha_equip:", "");
        int[] numArray = new int[3]{ -1, -1, -1 };
        string[] strArray2 = str2.Split(':');
        int index = 0;
        for (int length = strArray2.Length; index < length; ++index)
          int.TryParse(strArray2[index], out numArray[index]);
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
        {
          new EventData("MAIN_MENU_SHOP", (object) null),
          new EventData("GACHA_EQUIP_LIST_FROM_NEWS", (object) new object[3]
          {
            (object) numArray[0],
            (object) numArray[1],
            (object) numArray[2]
          })
        });
        break;
    }
  }

  public void Close(string result)
  {
    Object.Destroy((Object) this.webViewObject);
    this.webViewObject = (WebViewObject) null;
    try
    {
      if (this.onClose == null)
        return;
      this.onClose(result);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    finally
    {
      this.onClose = (Action<string>) null;
    }
  }

  public enum UrlType
  {
    NEWS,
    HELP,
    TERMS,
  }
}
