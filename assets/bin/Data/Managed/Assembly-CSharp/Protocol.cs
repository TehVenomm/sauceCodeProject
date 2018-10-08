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
		return _Try(send_callback, true);
	}

	public static bool Try(Action send_callback)
	{
		return _Try(send_callback, false);
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
		send_callback.Invoke();
		isTry = false;
		return true;
	}

	public static void Force(Action send_callback)
	{
		isForce = true;
		send_callback.Invoke();
		isForce = false;
	}

	public static void SendAsync<T>(string url, Action<T> call_back, string get_param = "") where T : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, call_back, get_param, token, true);
	}

	public static void Send<T>(string url, Action<T> call_back, string get_param = "") where T : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, call_back, get_param, token, false);
	}

	private unsafe static void Send<T>(string url, Action<T> callBack, string getParam, string token, bool isAsync) where T : BaseModel, new()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		_003CSend_003Ec__AnonStorey703<T> _003CSend_003Ec__AnonStorey;
		Action send = new Action((object)_003CSend_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		if (Begin(url, send, isAsync))
		{
			MonoBehaviourSingleton<NetworkManager>.I.Request(CheckURL(url), delegate(T ret)
			{
				if (Protocol.End<T>(ret, callBack, send, isAsync))
				{
					callBack(ret);
				}
			}, getParam, token);
		}
	}

	public static void SendAsync<T1, T2>(string url, T1 postData, Action<T2> callBack, string getParam = "") where T2 : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, postData, callBack, getParam, token, true);
	}

	public static void Send<T1, T2>(string url, T1 postData, Action<T2> callBack, string getParam = "") where T2 : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, postData, callBack, getParam, token, false);
	}

	private unsafe static void Send<T1, T2>(string url, T1 postData, Action<T2> callBack, string getParam, string token, bool isAsync) where T2 : BaseModel, new()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		_003CSend_003Ec__AnonStorey704<T1, T2> _003CSend_003Ec__AnonStorey;
		Action send = new Action((object)_003CSend_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		if (Begin(url, send, isAsync))
		{
			MonoBehaviourSingleton<NetworkManager>.I.Request(CheckURL(url), postData, delegate(T2 ret)
			{
				if (Protocol.End<T2>(ret, callBack, send, isAsync))
				{
					callBack(ret);
				}
			}, getParam, token);
		}
	}

	public static void SendAsync<T>(string url, WWWForm form, Action<T> callBack, string getParam = "") where T : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, form, callBack, getParam, token, true);
	}

	public static void Send<T>(string url, WWWForm form, Action<T> call_back, string get_param = "") where T : BaseModel, new()
	{
		string token = GenerateToken();
		Send(url, form, call_back, get_param, token, false);
	}

	private unsafe static void Send<T>(string url, WWWForm form, Action<T> call_back, string get_param, string token, bool isAsync) where T : BaseModel, new()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		_003CSend_003Ec__AnonStorey705<T> _003CSend_003Ec__AnonStorey;
		Action send = new Action((object)_003CSend_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		if (Begin(url, send, isAsync))
		{
			MonoBehaviourSingleton<NetworkManager>.I.RequestForm(CheckURL(url), form, delegate(T ret)
			{
				if (Protocol.End<T>(ret, call_back, send, isAsync))
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
		if (!isAsync)
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

	private unsafe static bool End<T>(T ret, Action<T> call_back, Action retry_action, bool isAsync) where T : BaseModel
	{
		if (!isAsync)
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
		int num = code;
		if (num == 31007)
		{
			flag2 = true;
		}
		if (flag2)
		{
			string errorMessage = StringTable.GetErrorMessage((uint)code);
			GameSceneEvent.PushStay();
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage, null, null, null, null), delegate
			{
				GameSceneEvent.PopStay();
				if (code == 1003)
				{
					Native.launchMyselfMarket();
				}
				MonoBehaviourSingleton<AppMain>.I.Reset();
			}, true, code);
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
			case 1023:
			case 2001:
				flag3 = true;
				break;
			}
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
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, errorMessage2, null, null, null, null), delegate
				{
					GameSceneEvent.PopStay();
					if (code == 1003)
					{
						Native.launchMyselfMarket();
					}
					MonoBehaviourSingleton<AppMain>.I.Reset();
				}, true, code);
			}
			else if (flag3 && code == 1002)
			{
				OpenMaintenancePopup(ret);
			}
			else if (!MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !flag)
			{
				GameSceneEvent.PushStay();
				MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage2, null, null, null, null), delegate
				{
					//IL_005d: Unknown result type (might be due to invalid IL or missing references)
					//IL_0062: Expected O, but got Unknown
					GameSceneEvent.PopStay();
					call_back(ret);
					if (code == 74001 || code == 74002)
					{
						Debug.Log((object)"kciked");
						AppMain i = MonoBehaviourSingleton<AppMain>.I;
						string empty = string.Empty;
						if (_003CEnd_003Ec__AnonStorey706<T>._003C_003Ef__am_0024cache4 == null)
						{
							_003CEnd_003Ec__AnonStorey706<T>._003C_003Ef__am_0024cache4 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
						}
						i.ChangeScene(empty, "HomeTop", _003CEnd_003Ec__AnonStorey706<T>._003C_003Ef__am_0024cache4);
					}
				}, true, code);
			}
			else
			{
				if (flag && GameSceneEvent.IsStay())
				{
					GameSceneEvent.PushStay();
				}
				if (code == 74001 || code == 74002)
				{
					MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage2, null, null, null, null), delegate
					{
						GameSceneEvent.PopStay();
						call_back(ret);
						if (code == 74001 || code == 74002)
						{
							MonoBehaviourSingleton<AppMain>.I.Reset();
						}
					}, true, code);
				}
				else
				{
					MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, errorMessage2, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111u), null, null), delegate(string btn)
					{
						if (btn == "YES")
						{
							retry_action.Invoke();
						}
						else
						{
							MonoBehaviourSingleton<AppMain>.I.Reset();
						}
					}, true, code);
				}
			}
			return false;
		}
		if (code == 200000)
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("error_200000", "Functionality");
		}
		else
		{
			MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("error_code_" + code, "Error");
		}
		string text = StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1001u, code);
		GameSceneEvent.PushStay();
		if (code == 129903)
		{
			text = StringTable.Format(STRING_CATEGORY.ERROR_DIALOG, 72003u, code);
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, text, null, null, null, null), delegate
			{
				GameSceneEvent.PopStay();
				call_back(ret);
			}, true, code);
		}
		else if (code > 500000 && code < 600000)
		{
			text = StringTable.Format(STRING_CATEGORY.ERROR_DIALOG, (uint)code, code);
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, text, null, null, null, null), delegate
			{
				GameSceneEvent.PopStay();
				call_back(ret);
			}, true, code);
		}
		else if (code > 600000 && code < 700000)
		{
			text = StringTable.Format(STRING_CATEGORY.ERROR_DIALOG, (uint)code, code);
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, text, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111u), null, null), delegate(string btn)
			{
				GameSceneEvent.PopStay();
				if (btn == "YES")
				{
					retry_action.Invoke();
				}
				else
				{
					MonoBehaviourSingleton<AppMain>.I.Reset();
				}
			}, true, code);
		}
		else
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, text, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111u), null, null), delegate(string btn)
			{
				GameSceneEvent.PopStay();
				if (btn == "YES")
				{
					retry_action.Invoke();
				}
				else
				{
					MonoBehaviourSingleton<AppMain>.I.Reset();
				}
			}, true, code);
		}
		return false;
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
		}, true, ret.error);
	}

	public unsafe static string GenerateToken()
	{
		string str = DateTime.Now.ToString("yyyyMMddhhmmssfff");
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			string text = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				text += str;
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				byte[] array = MD5.Create().ComputeHash(bytes);
				byte[] source = array;
				if (_003C_003Ef__am_0024cache4 == null)
				{
					_003C_003Ef__am_0024cache4 = new Func<byte, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				return string.Concat(source.Select<byte, string>(_003C_003Ef__am_0024cache4).ToArray());
			}
		}
		return Guid.NewGuid().ToString("N");
	}
}
