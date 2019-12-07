using System;
using UnityEngine;

public class WebViewManager : MonoBehaviourSingleton<WebViewManager>
{
	public enum UrlType
	{
		NEWS,
		HELP,
		TERMS
	}

	public UrlType urlType;

	private const string COOKIE_KEY_VERSION = "apv";

	private WebViewObject webViewObject;

	private Action<string> onClose;

	[SerializeField]
	private Rect m_Margine;

	public static string Url_News => NetworkManager.APP_HOST + News;

	public static string Url_Help => NetworkManager.APP_HOST + Help;

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
		return string.Format(NewsWithLinkParamFormat, link_param);
	}

	public void Open(string url, Action<string> _onClose = null)
	{
		Debug.Log(url);
		onClose = _onClose;
		webViewObject = base.gameObject.AddComponent<WebViewObject>();
		webViewObject.Init();
		webViewObject.EvaluateJS("var appVersion='" + NetworkNative.getNativeVersionName() + "';");
		webViewObject.SetCookie(NetworkManager.APP_HOST, "apv", NetworkNative.getNativeVersionName());
		if (MonoBehaviourSingleton<AccountManager>.I.account.token != "")
		{
			string[] array = MonoBehaviourSingleton<AccountManager>.I.account.token.Split('=');
			webViewObject.SetCookie(NetworkManager.APP_HOST, array[0], array[1]);
		}
		webViewObject.SetCookie(NetworkManager.APP_HOST, "dm", SystemInfo.deviceModel);
		webViewObject.LoadURL(url);
		webViewObject.SetVisibility(v: true);
		int num = Screen.width;
		int num2 = Screen.height;
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			num = MonoBehaviourSingleton<AppMain>.I.defaultScreenWidth;
			num2 = MonoBehaviourSingleton<AppMain>.I.defaultScreenHeight;
		}
		int left = (int)((float)num * m_Margine.xMin);
		int top = (int)((float)num2 * m_Margine.yMin);
		int right = (int)((float)num * m_Margine.width);
		int bottom = (int)((float)num2 * m_Margine.height);
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyWebView)
		{
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			if (specialDeviceInfo != null)
			{
				switch (urlType)
				{
				case UrlType.NEWS:
				{
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
					Transform transform = Utility.FindChild(base.gameObject.transform, "Window");
					if (transform != null)
					{
						UIWidget component = transform.GetComponent<UIWidget>();
						if (component != null)
						{
							component.leftAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.left;
							component.rightAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.right;
							component.bottomAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.bottom;
							component.topAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.top;
							Utility.UpdateAllAnchors(base.gameObject);
						}
					}
					break;
				}
				case UrlType.HELP:
					left = specialDeviceInfo.WebViewHelpPortrait.left;
					top = specialDeviceInfo.WebViewHelpPortrait.top;
					right = specialDeviceInfo.WebViewHelpPortrait.right;
					bottom = specialDeviceInfo.WebViewHelpPortrait.bottom;
					break;
				}
			}
		}
		webViewObject.SetMargins(left, top, right, bottom);
	}

	public void OnWebViewEvent(string msg)
	{
		if (string.IsNullOrEmpty(msg))
		{
			return;
		}
		if (msg.StartsWith("mailto:"))
		{
			Debug.Log("[mailto]:" + msg);
			Application.OpenURL(msg);
		}
		if (msg.StartsWith("browser:"))
		{
			string text = msg.Replace("browser:", "");
			Debug.Log("[browser]:" + text);
			Application.OpenURL(text);
		}
		if (msg.StartsWith("checkPurchase:"))
		{
			bool showErrorDialog = (int.Parse(msg.Replace("checkPurchase:", "")) == 1) ? true : false;
			Debug.Log("[checkPurchase]:" + showErrorDialog.ToString());
			Native.RestorePurchasedItem(showErrorDialog);
		}
		if (msg.StartsWith("openOpinionBox:"))
		{
			Debug.Log("[openOpinionBox]:");
			Close(msg);
			MonoBehaviourSingleton<GameSceneManager>.I.OpinionBox();
		}
		if (msg.StartsWith("close:"))
		{
			Debug.Log("[close]:");
			Close(msg);
		}
		if (msg.StartsWith("movie:"))
		{
			string text2 = msg.Replace("movie:", "");
			Debug.Log("[movie]:" + text2);
			if (!(text2 == "tutorial"))
			{
				if (text2 == "skill")
				{
					webViewObject.onDestroy = delegate
					{
						Utility.PlayFullScreenMovie("tutorial_skill.mp4");
					};
				}
			}
			else
			{
				webViewObject.onDestroy = delegate
				{
					Utility.PlayFullScreenMovie("tutorial_move.mp4");
				};
			}
			Close(msg);
		}
		if (msg.StartsWith("goto:"))
		{
			Close(msg);
			ProcessGotoEvent(msg);
		}
	}

	public static void ProcessGotoEvent(string msg)
	{
		string[] array = msg.Replace("goto:", "").Split('/');
		string text = array[0];
		string text2 = (array.Length > 1) ? array[1] : null;
		Debug.Log("[goto]:" + text + "/" + text2);
		string goingHomeEvent = GameSection.GetGoingHomeEvent();
		EventData[] array2 = null;
		switch (text)
		{
		case "gacha":
			array2 = new EventData[1]
			{
				new EventData("MAIN_MENU_SHOP", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		case "magi_gacha":
			array2 = new EventData[2]
			{
				new EventData("MAIN_MENU_SHOP", null),
				new EventData("MAGI_GACHA", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		case "inn":
			array2 = new EventData[1]
			{
				new EventData("MAIN_MENU_STUDIO", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		case "quest":
			array2 = new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("QUEST_COUNTER", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		case "event_quest":
		{
			int result = 0;
			array2 = ((!int.TryParse(text2, out result)) ? new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("TO_EVENT", null)
			} : new EventData[3]
			{
				new EventData(goingHomeEvent, null),
				new EventData("TO_EVENT", null),
				new EventData("SELECT", result)
			});
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		}
		case "gacha_quest":
			array2 = new EventData[3]
			{
				new EventData(goingHomeEvent, null),
				new EventData("GACHA_QUEST_COUNTER", null),
				new EventData("TO_GACHA_QUEST_COUNTER", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		case "explore_quest":
		{
			int result3 = 0;
			array2 = ((!int.TryParse(text2, out result3)) ? new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("EXPLORE", null)
			} : new EventData[3]
			{
				new EventData(goingHomeEvent, null),
				new EventData("EXPLORE", null),
				new EventData("SELECT_EXPLORE", result3)
			});
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		}
		case "point_shop":
			array2 = new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("POINT_SHOP_FROM_BUTTON", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		case "bingo":
			array2 = new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("BINGO", true)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		case "arena":
		{
			EventData eventData = ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50) ? new EventData("SELECT_ARENA", null) : new EventData("SELECT_DISABLE_ARENA", null);
			array2 = new EventData[3]
			{
				new EventData(goingHomeEvent, null),
				new EventData("TO_EVENT", null),
				eventData
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		}
		case "wave_quest":
		{
			int result4 = 0;
			array2 = ((!int.TryParse(text2, out result4)) ? new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("TO_EVENT", null)
			} : new EventData[3]
			{
				new EventData(goingHomeEvent, null),
				new EventData("TO_EVENT", null),
				new EventData("SELECT_WAVE", result4)
			});
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		}
		case "promotion":
			array2 = new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("FRIEND_PROMOTION", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		case "event_trial":
		{
			int result2 = 0;
			array2 = ((!int.TryParse(text2, out result2)) ? new EventData[2]
			{
				new EventData(goingHomeEvent, null),
				new EventData("TO_EVENT", null)
			} : new EventData[3]
			{
				new EventData(goingHomeEvent, null),
				new EventData("TO_EVENT", null),
				new EventData("SELECT_TRIAL", result2)
			});
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			return;
		}
		}
		if (text.StartsWith("login_bonus:"))
		{
			int.TryParse(text.Replace("login_bonus:", ""), out int result5);
			if (result5 != 0)
			{
				array2 = new EventData[2]
				{
					new EventData(goingHomeEvent, null),
					new EventData("LIMITED_LOGIN_BONUS_VIEW", result5)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
			}
		}
		else if (text.StartsWith("gacha_equip:"))
		{
			string text3 = text.Replace("gacha_equip:", "");
			int[] array3 = new int[3]
			{
				-1,
				-1,
				-1
			};
			string[] array4 = text3.Split(':');
			int i = 0;
			for (int num = array4.Length; i < num; i++)
			{
				int.TryParse(array4[i], out array3[i]);
			}
			array2 = new EventData[2]
			{
				new EventData("MAIN_MENU_SHOP", null),
				new EventData("GACHA_EQUIP_LIST_FROM_NEWS", new object[3]
				{
					array3[0],
					array3[1],
					array3[2]
				})
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
		}
	}

	public void Close(string result)
	{
		UnityEngine.Object.Destroy(webViewObject);
		webViewObject = null;
		try
		{
			if (onClose != null)
			{
				onClose(result);
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		finally
		{
			onClose = null;
		}
	}
}
