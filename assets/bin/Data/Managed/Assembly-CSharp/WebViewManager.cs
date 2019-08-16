using System;
using System.Collections.Generic;
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
		Debug.Log((object)url);
		onClose = _onClose;
		webViewObject = this.get_gameObject().AddComponent<WebViewObject>();
		webViewObject.Init(string.Empty, string.Empty, string.Empty);
		webViewObject.EvaluateJS("var appVersion='" + NetworkNative.getNativeVersionName() + "';");
		webViewObject.SetCookie(NetworkManager.APP_HOST, "apv", NetworkNative.getNativeVersionName());
		if (MonoBehaviourSingleton<AccountManager>.I.account.token != string.Empty)
		{
			string[] array = MonoBehaviourSingleton<AccountManager>.I.account.token.Split('=');
			webViewObject.SetCookie(NetworkManager.APP_HOST, array[0], array[1]);
		}
		webViewObject.SetCookie(NetworkManager.APP_HOST, "dm", SystemInfo.get_deviceModel());
		webViewObject.LoadURL(url);
		webViewObject.SetVisibility(v: true);
		int num = Screen.get_width();
		int num2 = Screen.get_height();
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			num = MonoBehaviourSingleton<AppMain>.I.defaultScreenWidth;
			num2 = MonoBehaviourSingleton<AppMain>.I.defaultScreenHeight;
		}
		int left = (int)((float)num * m_Margine.get_xMin());
		int top = (int)((float)num2 * m_Margine.get_yMin());
		int right = (int)((float)num * m_Margine.get_width());
		int bottom = (int)((float)num2 * m_Margine.get_height());
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
					Transform val = Utility.FindChild(this.get_gameObject().get_transform(), "Window");
					if (val != null)
					{
						UIWidget component = val.GetComponent<UIWidget>();
						if (component != null)
						{
							component.leftAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.left;
							component.rightAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.right;
							component.bottomAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.bottom;
							component.topAnchor.absolute = specialDeviceInfo.WebViewInfoAnchorLandscape.top;
							Utility.UpdateAllAnchors(this.get_gameObject());
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
			Debug.Log((object)("[mailto]:" + msg));
			Application.OpenURL(msg);
		}
		if (msg.StartsWith("browser:"))
		{
			string text = msg.Replace("browser:", string.Empty);
			Debug.Log((object)("[browser]:" + text));
			Application.OpenURL(text);
		}
		if (msg.StartsWith("checkPurchase:"))
		{
			bool flag = (int.Parse(msg.Replace("checkPurchase:", string.Empty)) == 1) ? true : false;
			Debug.Log((object)("[checkPurchase]:" + flag));
			Native.RestorePurchasedItem(flag);
		}
		if (msg.StartsWith("openOpinionBox:"))
		{
			Debug.Log((object)"[openOpinionBox]:");
			Close(msg);
			MonoBehaviourSingleton<GameSceneManager>.I.OpinionBox();
		}
		if (msg.StartsWith("close:"))
		{
			Debug.Log((object)"[close]:");
			Close(msg);
		}
		if (msg.StartsWith("movie:"))
		{
			string text2 = msg.Replace("movie:", string.Empty);
			Debug.Log((object)("[movie]:" + text2));
			switch (text2)
			{
			case "tutorial":
				webViewObject.onDestroy = delegate
				{
					Utility.PlayFullScreenMovie("tutorial_move.mp4");
				};
				break;
			case "skill":
				webViewObject.onDestroy = delegate
				{
					Utility.PlayFullScreenMovie("tutorial_skill.mp4");
				};
				break;
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
		string text = msg.Replace("goto:", string.Empty);
		string[] array = text.Split('/');
		string text2 = array[0];
		string text3 = (array.Length <= 1) ? null : array[1];
		Debug.Log((object)("[goto]:" + text2 + "/" + text3));
		string goingHomeEvent = GameSection.GetGoingHomeEvent();
		EventData[] array2 = null;
		if (text2 != null)
		{
			if (_003C_003Ef__switch_0024mapB == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(13);
				dictionary.Add("gacha", 0);
				dictionary.Add("magi_gacha", 1);
				dictionary.Add("inn", 2);
				dictionary.Add("quest", 3);
				dictionary.Add("event_quest", 4);
				dictionary.Add("gacha_quest", 5);
				dictionary.Add("explore_quest", 6);
				dictionary.Add("point_shop", 7);
				dictionary.Add("bingo", 8);
				dictionary.Add("arena", 9);
				dictionary.Add("wave_quest", 10);
				dictionary.Add("promotion", 11);
				dictionary.Add("event_trial", 12);
				_003C_003Ef__switch_0024mapB = dictionary;
			}
			if (_003C_003Ef__switch_0024mapB.TryGetValue(text2, out int value))
			{
				switch (value)
				{
				case 0:
					array2 = new EventData[1]
					{
						new EventData("MAIN_MENU_SHOP", null)
					};
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				case 1:
					array2 = new EventData[2]
					{
						new EventData("MAIN_MENU_SHOP", null),
						new EventData("MAGI_GACHA", null)
					};
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				case 2:
					array2 = new EventData[1]
					{
						new EventData("MAIN_MENU_STUDIO", null)
					};
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				case 3:
					array2 = new EventData[2]
					{
						new EventData(goingHomeEvent, null),
						new EventData("QUEST_COUNTER", null)
					};
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				case 4:
				{
					int result4 = 0;
					array2 = ((!int.TryParse(text3, out result4)) ? new EventData[2]
					{
						new EventData(goingHomeEvent, null),
						new EventData("TO_EVENT", null)
					} : new EventData[3]
					{
						new EventData(goingHomeEvent, null),
						new EventData("TO_EVENT", null),
						new EventData("SELECT", result4)
					});
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				}
				case 5:
					array2 = new EventData[3]
					{
						new EventData(goingHomeEvent, null),
						new EventData("GACHA_QUEST_COUNTER", null),
						new EventData("TO_GACHA_QUEST_COUNTER", null)
					};
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				case 6:
				{
					int result2 = 0;
					array2 = ((!int.TryParse(text3, out result2)) ? new EventData[2]
					{
						new EventData(goingHomeEvent, null),
						new EventData("EXPLORE", null)
					} : new EventData[3]
					{
						new EventData(goingHomeEvent, null),
						new EventData("EXPLORE", null),
						new EventData("SELECT_EXPLORE", result2)
					});
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				}
				case 7:
					array2 = new EventData[2]
					{
						new EventData(goingHomeEvent, null),
						new EventData("POINT_SHOP_FROM_BUTTON", null)
					};
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				case 8:
					array2 = new EventData[2]
					{
						new EventData(goingHomeEvent, null),
						new EventData("BINGO", true)
					};
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				case 9:
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
				case 10:
				{
					int result3 = 0;
					array2 = ((!int.TryParse(text3, out result3)) ? new EventData[2]
					{
						new EventData(goingHomeEvent, null),
						new EventData("TO_EVENT", null)
					} : new EventData[3]
					{
						new EventData(goingHomeEvent, null),
						new EventData("TO_EVENT", null),
						new EventData("SELECT_WAVE", result3)
					});
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				}
				case 11:
					array2 = new EventData[2]
					{
						new EventData(goingHomeEvent, null),
						new EventData("FRIEND_PROMOTION", null)
					};
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				case 12:
				{
					int result = 0;
					array2 = ((!int.TryParse(text3, out result)) ? new EventData[2]
					{
						new EventData(goingHomeEvent, null),
						new EventData("TO_EVENT", null)
					} : new EventData[3]
					{
						new EventData(goingHomeEvent, null),
						new EventData("TO_EVENT", null),
						new EventData("SELECT_TRIAL", result)
					});
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array2);
					return;
				}
				}
			}
		}
		if (text2.StartsWith("login_bonus:"))
		{
			string s = text2.Replace("login_bonus:", string.Empty);
			int.TryParse(s, out int result5);
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
		else if (text2.StartsWith("gacha_equip:"))
		{
			string text4 = text2.Replace("gacha_equip:", string.Empty);
			int[] array3 = new int[3]
			{
				-1,
				-1,
				-1
			};
			string[] array4 = text4.Split(':');
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
		Object.Destroy(webViewObject);
		webViewObject = null;
		try
		{
			if (onClose != null)
			{
				onClose(result);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		finally
		{
			onClose = null;
		}
	}
}
