using Ionic.Zlib;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

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

	public List<WWWInfo> wwwInfos = new List<WWWInfo>();

	public string tokenTemp;

	private const string TOKEN_KEY = "robpt";

	private const string EMPTY_RECORD_RESULT = "\"result\":[]";

	private const string EMPTY_RECORD_JSON = "{\"error\":0,\"result\":[]}";

	private bool isPreload;

	public static string APP_HOST => "http://appprd.dragonproject.gogame.net/";

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
		this.StartCoroutine(RequestCoroutine(path, call_back, get_param, token));
	}

	public IEnumerator RequestCoroutine<T>(string path, Action<T> call_back, string get_param = "", string token = "") where T : BaseModel, new()
	{
		yield return this.StartCoroutine(RequestFormCoroutine(path, null, call_back, get_param, token));
	}

	public void Request<T1, T2>(string path, T1 postData, Action<T2> call_back, string get_param = "", string token = "") where T2 : BaseModel, new()
	{
		this.StartCoroutine(RequestCoroutine(path, postData, call_back, get_param, token));
	}

	public IEnumerator RequestCoroutine<T1, T2>(string path, T1 postData, Action<T2> call_back, string get_param = "", string token = "") where T2 : BaseModel, new()
	{
		WWWForm form = new WWWForm();
		string data = JSONSerializer.Serialize(postData);
		AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
		string key = (!string.IsNullOrEmpty(account.userHash)) ? account.userHash : "ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ,";
		string encrypt_data = Cipher.EncryptRJ128(key, "yCNBH$$rCNGvC+#f", data);
		string data_to_send = string.IsNullOrEmpty(encrypt_data) ? string.Empty : encrypt_data;
		form.AddField("data", data_to_send);
		yield return this.StartCoroutine(RequestFormCoroutine(path, form, call_back, get_param, token));
	}

	public void RequestForm<T>(string path, WWWForm form, Action<T> call_back, string get_param = "", string token = "") where T : BaseModel, new()
	{
		this.StartCoroutine(RequestFormCoroutine(path, form, call_back, get_param, token));
	}

	public IEnumerator RequestFormCoroutine<T>(string path, WWWForm form, Action<T> call_back, string get_param = "", string token = "") where T : BaseModel, new()
	{
		yield return this.StartCoroutine(Request_Impl(path, form, call_back, delegate
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
		foreach (string key in form.get_headers().Keys)
		{
			dictionary.Add(key, form.get_headers()[key].ToString());
		}
		AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
		dictionary["Cookie"] = (account.token ?? string.Empty);
		dictionary["apv"] = NetworkNative.getNativeVersionName();
		dictionary["amv"] = string.Empty;
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			dictionary["amv"] = MonoBehaviourSingleton<ResourceManager>.I.manifestVersion.ToString();
		}
		dictionary["aidx"] = string.Empty;
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			dictionary["aidx"] = MonoBehaviourSingleton<ResourceManager>.I.assetIndex.ToString();
		}
		dictionary["tidx"] = string.Empty;
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			dictionary["tidx"] = MonoBehaviourSingleton<ResourceManager>.I.tableIndex.ToString();
		}
		dictionary["tmv"] = string.Empty;
		if (MonoBehaviourSingleton<DataTableManager>.IsValid())
		{
			dictionary["tmv"] = MonoBehaviourSingleton<DataTableManager>.I.manifestVersion.ToString();
		}
		dictionary[ServerConstDefine.CDV_KEY] = string.Empty;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
		{
			dictionary[ServerConstDefine.CDV_KEY] = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.cdv.ToString();
		}
		dictionary["User-Agent"] = NetworkNative.getDefaultUserAgent();
		dictionary["dm"] = SystemInfo.get_deviceModel();
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
		byte[] data = form.get_data();
		Dictionary<string, string> headers = GetHeader(form);
		string msg = GenerateErrorMsg(Error.Unknown);
		AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
		lastRequestTime = Time.get_time();
		WWW www = new WWW(url, data, headers);
		try
		{
			WWWInfo wwwinfo = new WWWInfo(www, _isAssetData: false, _isCached: false);
			wwwInfos.Add(wwwinfo);
			DateTime timeBegin = DateTime.Now;
			while (true)
			{
				yield return (object)new WaitForEndOfFrame();
				if ((DateTime.Now - timeBegin).TotalSeconds > 15.0)
				{
					msg = GenerateErrorMsg(Error.TimeOut);
					break;
				}
				if (!string.IsNullOrEmpty(www.get_error()))
				{
					string txt = www.get_error();
					int result = 0;
					msg = ((!int.TryParse(txt.Substring(0, 3), out result)) ? GenerateErrorMsg(Error.DetectHttpError) : GenerateErrorMsg((Error)(200000 + result)));
					break;
				}
				if (www.get_isDone())
				{
					www.get_text();
					string text = null;
					foreach (KeyValuePair<string, string> responseHeader in www.get_responseHeaders())
					{
						string a = responseHeader.Key.ToLower();
						if (string.IsNullOrEmpty(tokenTemp) && a == "set-cookie")
						{
							List<string> list = new List<string>(responseHeader.Value.Split(';'));
							foreach (string item in list)
							{
								if (item.Contains("robpt"))
								{
									tokenTemp = item;
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
						string prm_key = (!string.IsNullOrEmpty(account.userHash)) ? account.userHash : "ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ,";
						array = Cipher.DecryptRJ128Byte(prm_key, "yCNBH$$rCNGvC+#f", www.get_text());
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
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
							array = Cipher.DecryptRJ128Byte("ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ,", "yCNBH$$rCNGvC+#f", www.get_text());
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
						else if (text == null)
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
					break;
				}
			}
			wwwInfos.Remove(wwwinfo);
		}
		finally
		{
			base._003C_003E__Finally0();
		}
		try
		{
			if (call_back != null)
			{
				T obj = new T();
				try
				{
					obj = JSONSerializer.Deserialize<T>(msg);
				}
				catch (Exception ex2)
				{
					string message = GenerateErrorMsg(Error.DecodeFailed);
					obj = JSONSerializer.Deserialize<T>(message);
					Debug.LogException(ex2);
				}
				finally
				{
					if (obj.Error != 0)
					{
					}
					obj.Apply();
					CrashlyticsReporter.SetAPIRequestStatus(requesting: false);
					call_back(obj);
				}
			}
		}
		catch (Exception ex3)
		{
			Debug.LogException(ex3);
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
		List<WWWInfo> list = wwwInfos.FindAll((WWWInfo i) => !i.isCached);
		return list.Count > 0;
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
