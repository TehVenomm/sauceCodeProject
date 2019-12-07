using Ionic.Zlib;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviourSingleton<NetworkManager>
{
	[Serializable]
	public class WWWInfo
	{
		public WWW www;

		public bool isAssetData;

		public bool isCached;

		public WWWInfo()
		{
		}

		public WWWInfo(WWW _www, bool _isAssetData, bool _isCached)
		{
			www = _www;
			isAssetData = _isAssetData;
			isCached = _isCached;
		}
	}

	public static string OLD_SERVER_URL = "http://appprd.dragonproject.gogame.net/";

	public List<WWWInfo> wwwInfos = new List<WWWInfo>();

	public string tokenTemp;

	private const string TOKEN_KEY = "robpt";

	private const string EMPTY_RECORD_RESULT = "\"result\":[]";

	private const string EMPTY_RECORD_JSON = "{\"error\":0,\"result\":[]}";

	private bool isPreload;

	public static string APP_HOST
	{
		get
		{
			if (GameSaveData.instance == null)
			{
				return "";
			}
			if (GameSaveData.instance.currentServer == null)
			{
				return "";
			}
			return GameSaveData.instance.currentServer.url;
		}
		set
		{
		}
	}

	public static string IMG_HOST => "http://cdnprd.dragonproject.gogame.net/resources/";

	public static string TABLE_HOST => "http://cdnprd.dragonproject.gogame.net/resources/tables/";

	public static string CLAN_HOST => "http://prdclan.dragonproject.gogame.net/";

	public float lastRequestTime
	{
		get;
		private set;
	}

	public void SetPreload(bool enable)
	{
		isPreload = enable;
	}

	public bool IsNotPreload()
	{
		return !isPreload;
	}

	protected override void Awake()
	{
		base.Awake();
	}

	public void Request<T>(string path, Action<T> call_back, string get_param = "", string token = "") where T : BaseModel, new()
	{
		StartCoroutine(RequestCoroutine(path, call_back, get_param, token));
	}

	public IEnumerator RequestCoroutine<T>(string path, Action<T> call_back, string get_param = "", string token = "") where T : BaseModel, new()
	{
		yield return StartCoroutine(RequestFormCoroutine(path, null, call_back, get_param, token));
	}

	public void Request<T1, T2>(string path, T1 postData, Action<T2> call_back, string get_param = "", string token = "") where T2 : BaseModel, new()
	{
		StartCoroutine(RequestCoroutine(path, postData, call_back, get_param, token));
	}

	public IEnumerator RequestCoroutine<T1, T2>(string path, T1 postData, Action<T2> call_back, string get_param = "", string token = "") where T2 : BaseModel, new()
	{
		WWWForm wWWForm = new WWWForm();
		string prm_text_to_encrypt = JSONSerializer.Serialize(postData);
		AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
		string text = Cipher.EncryptRJ128(string.IsNullOrEmpty(account.userHash) ? "ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ," : account.userHash, "yCNBH$$rCNGvC+#f", prm_text_to_encrypt);
		string value = (!string.IsNullOrEmpty(text)) ? text : "";
		wWWForm.AddField("data", value);
		yield return StartCoroutine(RequestFormCoroutine(path, wWWForm, call_back, get_param, token));
	}

	public void RequestForm<T>(string path, WWWForm form, Action<T> call_back, string get_param = "", string token = "") where T : BaseModel, new()
	{
		StartCoroutine(RequestFormCoroutine(path, form, call_back, get_param, token));
	}

	public IEnumerator RequestFormCoroutine<T>(string path, WWWForm form, Action<T> call_back, string get_param = "", string token = "") where T : BaseModel, new()
	{
		yield return StartCoroutine(Request_Impl(path, form, call_back, delegate
		{
		}, get_param, token));
	}

	private string GetUrl(string path, string get_param)
	{
		if (path.StartsWith("clan"))
		{
			return CLAN_HOST + path + get_param;
		}
		return APP_HOST + path + get_param;
	}

	private Dictionary<string, string> GetHeader(WWWForm form)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (string key in form.headers.Keys)
		{
			dictionary.Add(key, form.headers[key].ToString());
		}
		AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
		dictionary["Cookie"] = (account.token ?? "");
		dictionary["apv"] = NetworkNative.getNativeVersionName();
		dictionary["amv"] = "";
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			dictionary["amv"] = MonoBehaviourSingleton<ResourceManager>.I.manifestVersion.ToString();
		}
		dictionary["aidx"] = "";
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			dictionary["aidx"] = MonoBehaviourSingleton<ResourceManager>.I.assetIndex.ToString();
		}
		dictionary["tidx"] = "";
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			dictionary["tidx"] = MonoBehaviourSingleton<ResourceManager>.I.tableIndex.ToString();
		}
		dictionary["tmv"] = "";
		if (MonoBehaviourSingleton<DataTableManager>.IsValid())
		{
			dictionary["tmv"] = MonoBehaviourSingleton<DataTableManager>.I.manifestVersion.ToString();
		}
		dictionary[ServerConstDefine.CDV_KEY] = "";
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
		{
			dictionary[ServerConstDefine.CDV_KEY] = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.cdv.ToString();
		}
		dictionary["User-Agent"] = NetworkNative.getDefaultUserAgent();
		dictionary["dm"] = SystemInfo.deviceModel;
		return dictionary;
	}

	private IEnumerator Request_Impl<T>(string path, WWWForm form, Action<T> call_back, Action call_fatal, string get_param = "", string token = "") where T : BaseModel, new()
	{
		SetPreload(enable: false);
		if (form == null)
		{
			form = new WWWForm();
		}
		form.AddField("app", "rob");
		if (!string.IsNullOrEmpty(token))
		{
			form.AddField("rcToken", token);
		}
		string url = GetUrl(path, get_param);
		CrashlyticsReporter.SetAPIRequest(url);
		CrashlyticsReporter.SetAPIRequestStatus(requesting: true);
		_ = form.data;
		Dictionary<string, string> header = GetHeader(form);
		string msg = GenerateErrorMsg(Error.Unknown);
		AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
		lastRequestTime = Time.time;
		UnityWebRequest.ClearCookieCache();
		using (UnityWebRequest www = UnityWebRequest.Post(url, form))
		{
			foreach (KeyValuePair<string, string> item in header)
			{
				www.SetRequestHeader(item.Key, item.Value);
			}
			www.SendWebRequest();
			WWWInfo wwwinfo = new WWWInfo(null, _isAssetData: false, _isCached: false);
			wwwInfos.Add(wwwinfo);
			DateTime timeBegin = DateTime.Now;
			while (true)
			{
				yield return new WaitForEndOfFrame();
				if ((DateTime.Now - timeBegin).TotalSeconds > 15.0)
				{
					msg = GenerateErrorMsg(Error.TimeOut);
					break;
				}
				if (!string.IsNullOrEmpty(www.error))
				{
					string error = www.error;
					int result = 0;
					msg = ((!int.TryParse(error.Substring(0, 3), out result)) ? GenerateErrorMsg(Error.DetectHttpError) : GenerateErrorMsg((Error)(200000 + result)));
					break;
				}
				if (www.isDone)
				{
					_ = www.downloadHandler.text;
					string text = null;
					foreach (KeyValuePair<string, string> responseHeader in www.GetResponseHeaders())
					{
						string a = responseHeader.Key.ToLower();
						if (string.IsNullOrEmpty(tokenTemp) && a == "set-cookie")
						{
							foreach (string item2 in new List<string>(responseHeader.Value.Split(';')))
							{
								if (item2.Contains("robpt"))
								{
									tokenTemp = item2;
								}
							}
						}
						else if (a == "x-compress-encrypt")
						{
							if (!(responseHeader.Value.Trim() == "cipher"))
							{
							}
						}
						else if (a == "x-signature")
						{
							text = responseHeader.Value.Trim();
						}
					}
					bool flag = true;
					byte[] array = null;
					try
					{
						array = Cipher.DecryptRJ128Byte(string.IsNullOrEmpty(account.userHash) ? "ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ," : account.userHash, "yCNBH$$rCNGvC+#f", www.downloadHandler.text);
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						flag = false;
					}
					if (!flag)
					{
						if (string.IsNullOrEmpty(account.userHash))
						{
							msg = GenerateErrorMsg(Error.DecryptResponceIsNull);
							Log.Error(LOG.NETWORK, "Decrypt failed!!!");
							break;
						}
						flag = true;
						try
						{
							array = Cipher.DecryptRJ128Byte("ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ,", "yCNBH$$rCNGvC+#f", www.downloadHandler.text);
						}
						catch (Exception exc)
						{
							Log.Exception(exc);
							flag = false;
						}
						if (!flag)
						{
							msg = GenerateErrorMsg(Error.DecryptFailed);
							Log.Error(LOG.NETWORK, "Decrypt failed");
							break;
						}
					}
					if (array == null)
					{
						msg = GenerateErrorMsg(Error.DecryptResponceIsNull);
						Log.Error(LOG.NETWORK, "Decrypt responce is null");
					}
					else
					{
						bool flag2 = true;
						try
						{
							msg = GzUncompress(array);
						}
						catch (Exception exc2)
						{
							Log.Exception(exc2);
							flag2 = false;
						}
						if (!flag2)
						{
							msg = GenerateErrorMsg(Error.UncompressFailed);
						}
						else
						{
							_ = msg;
							if (text == null)
							{
								msg = GenerateErrorMsg(Error.SignatureIsNull);
								Log.Error(LOG.NETWORK, "Signature is null");
							}
							else
							{
								bool flag3 = true;
								bool flag4 = true;
								try
								{
									flag4 = Cipher.verify(msg, text);
								}
								catch (Exception exc3)
								{
									Log.Exception(exc3);
									flag3 = false;
								}
								if (!flag3)
								{
									msg = GenerateErrorMsg(Error.VerifySignatureFailed);
								}
								else if (!flag4)
								{
									msg = GenerateErrorMsg(Error.InvalidSignature);
								}
								else
								{
									msg = Regex.Unescape(msg);
									if (msg == "{\"error\":0,\"result\":[]}")
									{
										msg = GenerateErrorMsg(Error.EmptyRecord);
									}
									if (msg.Contains("\"result\":[]"))
									{
										msg = msg.Replace("\"result\":[]", "\"dummy\":[]");
									}
								}
							}
						}
					}
					break;
				}
			}
			wwwInfos.Remove(wwwinfo);
		}
		try
		{
			if (call_back != null)
			{
				T val = new T();
				try
				{
					val = JSONSerializer.Deserialize<T>(msg);
				}
				catch (Exception exception2)
				{
					val = JSONSerializer.Deserialize<T>(GenerateErrorMsg(Error.DecodeFailed));
					Debug.LogException(exception2);
				}
				finally
				{
					_ = val.Error;
					val.Apply();
					CrashlyticsReporter.SetAPIRequestStatus(requesting: false);
					call_back(val);
				}
			}
		}
		catch (Exception exception3)
		{
			Debug.LogException(exception3);
			CrashlyticsReporter.SetAPIRequestStatus(requesting: false);
			call_fatal();
		}
	}

	public string GenerateErrorMsg(Error error)
	{
		return "{\"error\":" + (int)error + "}";
	}

	public bool IsConnecting()
	{
		wwwInfos.RemoveAll((WWWInfo i) => i.www == null);
		return wwwInfos.FindAll((WWWInfo i) => !i.isCached).Count > 0;
	}

	public static string GzUncompress(byte[] gzEncrypted)
	{
		byte[] bytes = ZlibStream.UncompressBuffer(gzEncrypted);
		return Encoding.UTF8.GetString(bytes);
	}

	public static byte[] GzUncompressByte(byte[] gzEncrypted)
	{
		return ZlibStream.UncompressBuffer(gzEncrypted);
	}

	public static byte[] GzCompress(string decrypted)
	{
		return GZipStream.CompressString(decrypted);
	}
}
