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

	private const string TOKEN_KEY = "robpt";

	private const string EMPTY_RECORD_RESULT = "\"result\":[]";

	private const string EMPTY_RECORD_JSON = "{\"error\":0,\"result\":[]}";

	public List<WWWInfo> wwwInfos = new List<WWWInfo>();

	public string tokenTemp;

	private bool isPreload;

	public static string APP_HOST => "http://app-aprod.dragonproject.gogame.net/";

	public static string IMG_HOST => "http://cdnprd.dragonproject.gogame.net/resources/";

	public static string TABLE_HOST => "http://cdnprd.dragonproject.gogame.net/resources/tables/";

	public static string CLAN_HOST => "http://clan-aprod.dragonproject.gogame.net/";

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
		yield return (object)StartCoroutine(this.RequestFormCoroutine<T>(path, (WWWForm)null, call_back, get_param, token));
	}

	public void Request<T1, T2>(string path, T1 postData, Action<T2> call_back, string get_param = "", string token = "") where T2 : BaseModel, new()
	{
		StartCoroutine(RequestCoroutine(path, postData, call_back, get_param, token));
	}

	public IEnumerator RequestCoroutine<T1, T2>(string path, T1 postData, Action<T2> call_back, string get_param = "", string token = "") where T2 : BaseModel, new()
	{
		WWWForm form = new WWWForm();
		string data = JSONSerializer.Serialize<T1>(postData);
		AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
		string key = (!string.IsNullOrEmpty(account.userHash)) ? account.userHash : "ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ,";
		string encrypt_data = Cipher.EncryptRJ128(key, "yCNBH$$rCNGvC+#f", data);
		string data_to_send = string.IsNullOrEmpty(encrypt_data) ? string.Empty : encrypt_data;
		form.AddField("data", data_to_send);
		yield return (object)StartCoroutine(this.RequestFormCoroutine<T2>(path, form, call_back, get_param, token));
	}

	public void RequestForm<T>(string path, WWWForm form, Action<T> call_back, string get_param = "", string token = "") where T : BaseModel, new()
	{
		StartCoroutine(RequestFormCoroutine(path, form, call_back, get_param, token));
	}

	public IEnumerator RequestFormCoroutine<T>(string path, WWWForm form, Action<T> call_back, string get_param = "", string token = "") where T : BaseModel, new()
	{
		yield return (object)StartCoroutine(this.Request_Impl<T>(path, form, call_back, (Action)delegate
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
		return dictionary;
	}

	private IEnumerator Request_Impl<T>(string path, WWWForm form, Action<T> call_back, Action call_fatal, string get_param = "", string token = "") where T : BaseModel, new()
	{
		SetPreload(false);
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
		CrashlyticsReporter.SetAPIRequestStatus(true);
		byte[] data = form.data;
		Dictionary<string, string> headers = GetHeader(form);
		string msg = GenerateErrorMsg(Error.Unknown);
		AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
		lastRequestTime = Time.time;
		using (WWW www = new WWW(url, data, headers))
		{
			WWWInfo wwwinfo = new WWWInfo(www, false, false);
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
				if (!string.IsNullOrEmpty(www.error))
				{
					string txt = www.error;
					int httpError = 0;
					msg = ((!int.TryParse(txt.Substring(0, 3), out httpError)) ? GenerateErrorMsg(Error.DetectHttpError) : GenerateErrorMsg((Error)(200000 + httpError)));
					break;
				}
				if (www.isDone)
				{
					string text = www.text;
					string signature = null;
					foreach (KeyValuePair<string, string> responseHeader in www.responseHeaders)
					{
						string headerName = responseHeader.Key.ToLower();
						if (string.IsNullOrEmpty(tokenTemp) && headerName == "set-cookie")
						{
							List<string> values = new List<string>((IEnumerable<string>)responseHeader.Value.Split(';'));
							foreach (string item in values)
							{
								if (item.Contains("robpt"))
								{
									tokenTemp = item;
								}
							}
						}
						else if (headerName == "x-compress-encrypt")
						{
							if (!(responseHeader.Value.Trim() == "cipher"))
							{
								continue;
							}
						}
						else if (headerName == "x-signature")
						{
							signature = responseHeader.Value.Trim();
						}
					}
					bool isDecryptSuccess2 = true;
					byte[] gzippedResponse = null;
					try
					{
						string key = (!string.IsNullOrEmpty(account.userHash)) ? account.userHash : "ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ,";
						gzippedResponse = Cipher.DecryptRJ128Byte(key, "yCNBH$$rCNGvC+#f", www.text);
					}
					catch (Exception exc4)
					{
						Debug.LogException(exc4);
						isDecryptSuccess2 = false;
					}
					if (!isDecryptSuccess2)
					{
						if (string.IsNullOrEmpty(account.userHash))
						{
							msg = GenerateErrorMsg(Error.DecryptResponceIsNull);
							Log.Error(LOG.NETWORK, "Decrypt failed!!!");
							break;
						}
						isDecryptSuccess2 = true;
						try
						{
							gzippedResponse = Cipher.DecryptRJ128Byte("ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ,", "yCNBH$$rCNGvC+#f", www.text);
						}
						catch (Exception exc3)
						{
							Log.Exception(exc3);
							isDecryptSuccess2 = false;
						}
						if (!isDecryptSuccess2)
						{
							msg = GenerateErrorMsg(Error.DecryptFailed);
							Log.Error(LOG.NETWORK, "Decrypt failed");
							break;
						}
					}
					if (gzippedResponse == null)
					{
						msg = GenerateErrorMsg(Error.DecryptResponceIsNull);
						Log.Error(LOG.NETWORK, "Decrypt responce is null");
					}
					else
					{
						bool isUncompressSuccess = true;
						try
						{
							msg = GzUncompress(gzippedResponse);
						}
						catch (Exception ex)
						{
							Exception exc2 = ex;
							Log.Exception(exc2);
							isUncompressSuccess = false;
						}
						if (!isUncompressSuccess)
						{
							msg = GenerateErrorMsg(Error.UncompressFailed);
						}
						else if (signature == null)
						{
							msg = GenerateErrorMsg(Error.SignatureIsNull);
							Log.Error(LOG.NETWORK, "Signature is null");
						}
						else
						{
							bool isVerifySignatureSuccess = true;
							bool isValidSignature = true;
							try
							{
								isValidSignature = Cipher.verify(msg, signature);
							}
							catch (Exception ex2)
							{
								Exception exc = ex2;
								Log.Exception(exc);
								isVerifySignatureSuccess = false;
							}
							if (!isVerifySignatureSuccess)
							{
								msg = GenerateErrorMsg(Error.VerifySignatureFailed);
							}
							else if (!isValidSignature)
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
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		try
		{
			if (call_back != null)
			{
				new T();
				try
				{
					JSONSerializer.Deserialize<T>(msg);
				}
				catch (Exception ex3)
				{
					Exception exp2 = ex3;
					string decode_failed_msg = GenerateErrorMsg(Error.DecodeFailed);
					JSONSerializer.Deserialize<T>(decode_failed_msg);
					Debug.LogException(exp2);
				}
				finally
				{
					((_003CRequest_Impl_003Ec__Iterator21D<T>)/*Error near IL_0853: stateMachine*/)._003C_003E__Finally0();
				}
			}
		}
		catch (Exception exp)
		{
			Debug.LogException(exp);
			CrashlyticsReporter.SetAPIRequestStatus(false);
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
