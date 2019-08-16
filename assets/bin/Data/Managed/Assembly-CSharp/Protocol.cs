using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

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
			if (busy > 0)
			{
				return true;
			}
			return MonoBehaviourSingleton<ProtocolManager>.IsValid() && MonoBehaviourSingleton<ProtocolManager>.I.isReserved;
		}
	}

	public static void Initialize()
	{
		busy = 0;
		isTry = false;
	}

	public static bool Resend(Action send_callback)
	{
		return _Try(send_callback, resend: true);
	}

	public static bool Try(Action send_callback)
	{
		return _Try(send_callback, resend: false);
	}

	private static bool _Try(Action send_callback, bool resend)
	{
		if ((resend && busy > 0) || (!resend && isBusy) || MonoBehaviourSingleton<GameSceneManager>.I.isOpenCommonDialog)
		{
			return false;
		}
		isTry = true;
		if (!IsEnabledSend())
		{
			isTry = false;
			return false;
		}
		send_callback();
		isTry = false;
		return true;
	}

	public static void Force(Action send_callback)
	{
		isForce = true;
		send_callback();
		isForce = false;
	}

	public static void SendAsync<T>(string url, Action<T> call_back, string get_param = "") where T : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, call_back, get_param, token, isAsync: true);
	}

	public static void Send<T>(string url, Action<T> call_back, string get_param = "") where T : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, call_back, get_param, token, isAsync: false);
	}

	private static void Send<T>(string url, Action<T> callBack, string getParam, string token, bool isAsync) where T : BaseModel, new()
	{
		Action send = delegate
		{
			Send(url, callBack, getParam, token, isAsync);
		};
		if (Begin(url, send, isAsync))
		{
			MonoBehaviourSingleton<NetworkManager>.I.Request(CheckURL(url), delegate(T ret)
			{
				if (End(CheckURL(url), ret, callBack, send, isAsync))
				{
					callBack(ret);
				}
			}, getParam, token);
		}
	}

	public static void SendAsync<T1, T2>(string url, T1 postData, Action<T2> callBack, string getParam = "") where T2 : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, postData, callBack, getParam, token, isAsync: true);
	}

	public static void Send<T1, T2>(string url, T1 postData, Action<T2> callBack, string getParam = "") where T2 : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, postData, callBack, getParam, token, isAsync: false);
	}

	private static void Send<T1, T2>(string url, T1 postData, Action<T2> callBack, string getParam, string token, bool isAsync) where T2 : BaseModel, new()
	{
		Action send = delegate
		{
			Send(url, postData, callBack, getParam, token, isAsync);
		};
		if (Begin(url, send, isAsync))
		{
			MonoBehaviourSingleton<NetworkManager>.I.Request(CheckURL(url), postData, delegate(T2 ret)
			{
				if (End(CheckURL(url), ret, callBack, send, isAsync))
				{
					callBack(ret);
				}
			}, getParam, token);
		}
	}

	public static void SendAsync<T>(string url, WWWForm form, Action<T> callBack, string getParam = "") where T : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, form, callBack, getParam, token, isAsync: true);
	}

	public static void Send<T>(string url, WWWForm form, Action<T> call_back, string get_param = "") where T : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, form, call_back, get_param, token, isAsync: false);
	}

	private static void Send<T>(string url, WWWForm form, Action<T> call_back, string get_param, string token, bool isAsync) where T : BaseModel, new()
	{
		Action send = delegate
		{
			Send(url, form, call_back, get_param, token, isAsync);
		};
		if (Begin(url, send, isAsync))
		{
			MonoBehaviourSingleton<NetworkManager>.I.RequestForm(CheckURL(url), form, delegate(T ret)
			{
				if (End(CheckURL(url), ret, call_back, send, isAsync))
				{
					call_back(ret);
				}
			}, get_param, token);
		}
	}

	private static string CheckURL(string url)
	{
		return url;
	}

	private static bool IsEnabledSend()
	{
		if (!AppMain.isInitialized)
		{
			return true;
		}
		if (!MonoBehaviourSingleton<UIManager>.I.IsDisable())
		{
			if (!isTry && !GameSceneEvent.IsStay())
			{
				return false;
			}
		}
		else if (!GameSceneEvent.IsStay() && !MonoBehaviourSingleton<GameSceneManager>.I.isWaiting)
		{
			return false;
		}
		return true;
	}

	private static bool Begin(string url, Action send, bool isAsync)
	{
		if (!isForce)
		{
			if (isBusy || MonoBehaviourSingleton<GameSceneManager>.I.isOpenCommonDialog)
			{
				if (MonoBehaviourSingleton<ProtocolManager>.IsValid())
				{
					MonoBehaviourSingleton<ProtocolManager>.I.Reserve(send);
				}
				return false;
			}
			if (strict && !IsEnabledSend())
			{
				Log.Error(LOG.NETWORK, "Protocol : Send Error : {0}", url);
				return false;
			}
		}
		if (!notBusyURLs.Contains(url) && !isAsync)
		{
			SetBusy(1);
		}
		return true;
	}

	private static void SetBusy(int v)
	{
		busy += v;
		bool flag = busy != 0;
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (strict || !flag))
		{
			MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.PROTOCOL, flag);
		}
	}

	private static bool End<T>(string url, T ret, Action<T> call_back, Action retry_action, bool isAsync) where T : BaseModel
	{
		if (!notBusyURLs.Contains(url) && !isAsync)
		{
			SetBusy(-1);
		}
		int code = ret.error;
		bool flag = MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.IsTutorialErrorResend();
		if (code == 0)
		{
			if (flag)
			{
				GameSceneEvent.PopStay();
			}
			if (Utility.IsExist(ret.diff) && MonoBehaviourSingleton<ProtocolManager>.IsValid())
			{
				MonoBehaviourSingleton<ProtocolManager>.I.OnDiff(ret.diff[0]);
			}
			return true;
		}
		bool flag2 = false;
		if (code == 31007)
		{
			flag2 = true;
		}
		if (flag2)
		{
			string errorMessage = StringTable.GetErrorMessage((uint)code);
			GameSceneEvent.PushStay();
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage), delegate
			{
				GameSceneEvent.PopStay();
				if (code == 1003)
				{
					Native.launchMyselfMarket();
				}
				MonoBehaviourSingleton<AppMain>.I.Reset();
			}, error: true, code);
			return false;
		}
		bool flag3;
		if (code < 100000)
		{
			flag3 = false;
			if (code != 1002 && code != 1003)
			{
				switch (code)
				{
				case 1020:
				case 1023:
					goto IL_018a;
				}
				if (code != 2001)
				{
					goto IL_0192;
				}
			}
			goto IL_018a;
		}
		uint id = 1001u;
		if (code == 200000)
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("error_200000", "Functionality");
		}
		else
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("error_code_" + code, "Error");
		}
		string text = StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, id, code);
		GameSceneEvent.PushStay();
		if (code == 129903)
		{
			text = StringTable.Format(STRING_CATEGORY.ERROR_DIALOG, 72003u, code);
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, text), delegate
			{
				GameSceneEvent.PopStay();
				call_back(ret);
			}, error: true, code);
		}
		else if (code > 500000 && code < 600000)
		{
			text = StringTable.Format(STRING_CATEGORY.ERROR_DIALOG, (uint)code, code);
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, text), delegate
			{
				GameSceneEvent.PopStay();
				call_back(ret);
			}, error: true, code);
		}
		else if (code > 600000 && code < 700000)
		{
			text = StringTable.Format(STRING_CATEGORY.ERROR_DIALOG, (uint)code, code);
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, text, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111u)), delegate(string btn)
			{
				GameSceneEvent.PopStay();
				if (btn == "YES")
				{
					retry_action();
				}
				else
				{
					MonoBehaviourSingleton<AppMain>.I.Reset();
				}
			}, error: true, code);
		}
		else
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, text, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111u)), delegate(string btn)
			{
				GameSceneEvent.PopStay();
				if (btn == "YES")
				{
					retry_action();
				}
				else
				{
					MonoBehaviourSingleton<AppMain>.I.Reset();
				}
			}, error: true, code);
		}
		return false;
		IL_0192:
		if ((!flag3 || ret is CheckRegisterModel) && GameSceneGlobalSettings.IsNonPopupError(ret))
		{
			if (code == 1002)
			{
				OpenMaintenancePopup(ret);
			}
			return true;
		}
		string errorMessage2 = StringTable.GetErrorMessage((uint)code);
		MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("error_code_" + code, "Error");
		if (flag3 && code != 1002)
		{
			GameSceneEvent.PushStay();
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, errorMessage2), delegate
			{
				GameSceneEvent.PopStay();
				if (code == 1003)
				{
					Native.launchMyselfMarket();
				}
				MonoBehaviourSingleton<AppMain>.I.Reset();
			}, error: true, code);
		}
		else if (flag3 && code == 1002)
		{
			OpenMaintenancePopup(ret);
		}
		else if (!MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !flag)
		{
			if (code == 42002)
			{
				if (MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.IsRegistered() && ClanMatchingManager.IsValidInClan())
				{
					MonoBehaviourSingleton<ClanMatchingManager>.I.Kick(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
				}
				return false;
			}
			GameSceneEvent.PushStay();
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage2), delegate
			{
				GameSceneEvent.PopStay();
				call_back(ret);
				if (code == 74001 || code == 74002)
				{
					Debug.Log((object)"kciked");
					MonoBehaviourSingleton<AppMain>.I.ChangeScene(string.Empty, "HomeTop", delegate
					{
						MonoBehaviourSingleton<GuildManager>.I.UpdateGuild(null);
					});
				}
			}, error: true, code);
		}
		else
		{
			if (flag && GameSceneEvent.IsStay())
			{
				GameSceneEvent.PushStay();
			}
			if (code == 74001 || code == 74002)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage2), delegate
				{
					GameSceneEvent.PopStay();
					call_back(ret);
					if (code == 74001 || code == 74002)
					{
						MonoBehaviourSingleton<AppMain>.I.Reset();
					}
				}, error: true, code);
			}
			else
			{
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, errorMessage2, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111u)), delegate(string btn)
				{
					if (btn == "YES")
					{
						retry_action();
					}
					else
					{
						MonoBehaviourSingleton<AppMain>.I.Reset();
					}
				}, error: true, code);
			}
		}
		return false;
		IL_018a:
		flag3 = true;
		goto IL_0192;
	}

	private static void OpenMaintenancePopup<T>(T ret) where T : BaseModel
	{
		GameSceneEvent.PushStay();
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage(1002u), StringTable.Get(STRING_CATEGORY.ERROR_DIALOG, 100202u), StringTable.Get(STRING_CATEGORY.ERROR_DIALOG, 100201u), null, ret.infoError), delegate(string btn)
		{
			if (btn == "YES")
			{
				Native.OpenURL("https://www.facebook.com/DragonProject/");
			}
			GameSceneEvent.PopStay();
			MonoBehaviourSingleton<AppMain>.I.Reset();
			Native.applicationQuit();
		}, error: true, ret.error);
	}

	public static string GenerateToken()
	{
		string str = DateTime.Now.ToString("yyyyMMddhhmmssfff");
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			string text = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				text += str;
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				byte[] source = MD5.Create().ComputeHash(bytes);
				return string.Concat((from i in source
				select i.ToString("x2")).ToArray());
			}
		}
		return Guid.NewGuid().ToString("N");
	}
}
