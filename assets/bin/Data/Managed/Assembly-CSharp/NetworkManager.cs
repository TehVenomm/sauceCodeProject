// Decompiled with JetBrains decompiler
// Type: NetworkManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Ionic.Zlib;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
public class NetworkManager : MonoBehaviourSingleton<NetworkManager>
{
  public static string OLD_SERVER_URL = "http://appprd.dragonproject.gogame.net/";
  public List<NetworkManager.WWWInfo> wwwInfos = new List<NetworkManager.WWWInfo>();
  public string tokenTemp;
  private const string TOKEN_KEY = "robpt";
  private const string EMPTY_RECORD_RESULT = "\"result\":[]";
  private const string EMPTY_RECORD_JSON = "{\"error\":0,\"result\":[]}";
  private bool isPreload;

  public static string APP_HOST
  {
    get
    {
      return GameSaveData.instance == null || GameSaveData.instance.currentServer == null ? "" : GameSaveData.instance.currentServer.url;
    }
    set
    {
    }
  }

  public static string IMG_HOST => "http://cdnprd2.dragonproject.gogame.net/resources/";

  public static string TABLE_HOST => "http://cdnprd2.dragonproject.gogame.net/resources/tables/";

  public static string CLAN_HOST => "http://prdclan.dragonproject.gogame.net/";

  public float lastRequestTime { get; private set; }

  public void SetPreload(bool enable) => this.isPreload = enable;

  public bool IsNotPreload() => !this.isPreload;

  protected override void Awake() => base.Awake();

  public void Request<T>(string path, Action<T> call_back, string get_param = "", string token = "") where T : BaseModel, new()
  {
    this.StartCoroutine(this.RequestCoroutine<T>(path, call_back, get_param, token));
  }

  public IEnumerator RequestCoroutine<T>(
    string path,
    Action<T> call_back,
    string get_param = "",
    string token = "")
    where T : BaseModel, new()
  {
    yield return (object) this.StartCoroutine(this.RequestFormCoroutine<T>(path, (WWWForm) null, call_back, get_param, token));
  }

  public void Request<T1, T2>(
    string path,
    T1 postData,
    Action<T2> call_back,
    string get_param = "",
    string token = "")
    where T2 : BaseModel, new()
  {
    this.StartCoroutine(this.RequestCoroutine<T1, T2>(path, postData, call_back, get_param, token));
  }

  public IEnumerator RequestCoroutine<T1, T2>(
    string path,
    T1 postData,
    Action<T2> call_back,
    string get_param = "",
    string token = "")
    where T2 : BaseModel, new()
  {
    WWWForm form = new WWWForm();
    string prm_text_to_encrypt = JSONSerializer.Serialize<T1>(postData);
    AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
    string str1 = Cipher.EncryptRJ128(string.IsNullOrEmpty(account.userHash) ? "ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ," : account.userHash, "yCNBH$$rCNGvC+#f", prm_text_to_encrypt);
    string str2 = !string.IsNullOrEmpty(str1) ? str1 : "";
    form.AddField("data", str2);
    yield return (object) this.StartCoroutine(this.RequestFormCoroutine<T2>(path, form, call_back, get_param, token));
  }

  public void RequestForm<T>(
    string path,
    WWWForm form,
    Action<T> call_back,
    string get_param = "",
    string token = "")
    where T : BaseModel, new()
  {
    this.StartCoroutine(this.RequestFormCoroutine<T>(path, form, call_back, get_param, token));
  }

  public IEnumerator RequestFormCoroutine<T>(
    string path,
    WWWForm form,
    Action<T> call_back,
    string get_param = "",
    string token = "")
    where T : BaseModel, new()
  {
    yield return (object) this.StartCoroutine(this.Request_Impl<T>(path, form, call_back, (System.Action) (() => { }), get_param, token));
  }

  private string GetUrl(string path, string get_param)
  {
    return path.StartsWith("clan") ? NetworkManager.CLAN_HOST + path + get_param : NetworkManager.APP_HOST + path + get_param;
  }

  private Dictionary<string, string> GetHeader(WWWForm form)
  {
    Dictionary<string, string> header = new Dictionary<string, string>();
    foreach (string key in form.headers.Keys)
      header.Add(key, form.headers[key].ToString());
    AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
    header["Cookie"] = account.token ?? "";
    header["apv"] = NetworkNative.getNativeVersionName();
    header["amv"] = "";
    if (MonoBehaviourSingleton<ResourceManager>.IsValid())
    {
      int manifestVersion = MonoBehaviourSingleton<ResourceManager>.I.manifestVersion;
      header["amv"] = manifestVersion.ToString();
    }
    header["aidx"] = "";
    if (MonoBehaviourSingleton<ResourceManager>.IsValid())
    {
      int assetIndex = MonoBehaviourSingleton<ResourceManager>.I.assetIndex;
      header["aidx"] = assetIndex.ToString();
    }
    header["tidx"] = "";
    if (MonoBehaviourSingleton<ResourceManager>.IsValid())
    {
      int tableIndex = MonoBehaviourSingleton<ResourceManager>.I.tableIndex;
      header["tidx"] = tableIndex.ToString();
    }
    header["tmv"] = "";
    if (MonoBehaviourSingleton<DataTableManager>.IsValid())
    {
      int manifestVersion = MonoBehaviourSingleton<DataTableManager>.I.manifestVersion;
      header["tmv"] = manifestVersion.ToString();
    }
    header[ServerConstDefine.CDV_KEY] = "";
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
      header[ServerConstDefine.CDV_KEY] = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.cdv.ToString();
    header["User-Agent"] = NetworkNative.getDefaultUserAgent();
    header["dm"] = SystemInfo.deviceModel;
    return header;
  }

  private IEnumerator Request_Impl<T>(
    string path,
    WWWForm form,
    Action<T> call_back,
    System.Action call_fatal,
    string get_param = "",
    string token = "")
    where T : BaseModel, new()
  {
    this.SetPreload(false);
    if (form == null)
      form = new WWWForm();
    form.AddField("app", "rob");
    if (!string.IsNullOrEmpty(token))
      form.AddField("rcToken", token);
    string url = this.GetUrl(path, get_param);
    CrashlyticsReporter.SetAPIRequest(url);
    CrashlyticsReporter.SetAPIRequestStatus(true);
    byte[] data = form.data;
    Dictionary<string, string> header = this.GetHeader(form);
    string msg = this.GenerateErrorMsg(Error.Unknown);
    AccountManager.Account account = MonoBehaviourSingleton<AccountManager>.I.account;
    this.lastRequestTime = Time.time;
    UnityWebRequest.ClearCookieCache();
    using (UnityWebRequest www = UnityWebRequest.Post(url, form))
    {
      foreach (KeyValuePair<string, string> keyValuePair in header)
        www.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
      www.SendWebRequest();
      NetworkManager.WWWInfo wwwinfo = new NetworkManager.WWWInfo((WWW) null, false, false);
      this.wwwInfos.Add(wwwinfo);
      DateTime timeBegin = DateTime.Now;
      do
      {
        yield return (object) new WaitForEndOfFrame();
        if ((DateTime.Now - timeBegin).TotalSeconds > 15.0)
        {
          msg = this.GenerateErrorMsg(Error.TimeOut);
          goto label_63;
        }
        if (!string.IsNullOrEmpty(www.error))
        {
          string error = www.error;
          int result = 0;
          msg = !int.TryParse(error.Substring(0, 3), out result) ? this.GenerateErrorMsg(Error.DetectHttpError) : this.GenerateErrorMsg((Error) (200000 + result));
          goto label_63;
        }
      }
      while (!www.isDone);
      string text = www.downloadHandler.text;
      string base64Signature = (string) null;
      foreach (KeyValuePair<string, string> responseHeader in www.GetResponseHeaders())
      {
        string lower = responseHeader.Key.ToLower();
        if (string.IsNullOrEmpty(this.tokenTemp) && lower == "set-cookie")
        {
          foreach (string str in new List<string>((IEnumerable<string>) responseHeader.Value.Split(';')))
          {
            if (str.Contains("robpt"))
              this.tokenTemp = str;
          }
        }
        else
        {
          switch (lower)
          {
            case "x-compress-encrypt":
              if (!(responseHeader.Value.Trim() == "cipher"))
                continue;
              continue;
            case "x-signature":
              base64Signature = responseHeader.Value.Trim();
              continue;
            default:
              continue;
          }
        }
      }
      bool flag1 = true;
      byte[] gzEncrypted = (byte[]) null;
      try
      {
        gzEncrypted = Cipher.DecryptRJ128Byte(string.IsNullOrEmpty(account.userHash) ? "ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ," : account.userHash, "yCNBH$$rCNGvC+#f", www.downloadHandler.text);
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
        flag1 = false;
      }
      if (!flag1)
      {
        if (!string.IsNullOrEmpty(account.userHash))
        {
          bool flag2 = true;
          try
          {
            gzEncrypted = Cipher.DecryptRJ128Byte("ELqdT/y.pM#8+J##x7|3/tLb7jZhmqJ,", "yCNBH$$rCNGvC+#f", www.downloadHandler.text);
          }
          catch (Exception ex)
          {
            Log.Exception(ex);
            flag2 = false;
          }
          if (!flag2)
          {
            msg = this.GenerateErrorMsg(Error.DecryptFailed);
            Log.Error(LOG.NETWORK, "Decrypt failed");
            goto label_63;
          }
        }
        else
        {
          msg = this.GenerateErrorMsg(Error.DecryptResponceIsNull);
          Log.Error(LOG.NETWORK, "Decrypt failed!!!");
          goto label_63;
        }
      }
      if (gzEncrypted == null)
      {
        msg = this.GenerateErrorMsg(Error.DecryptResponceIsNull);
        Log.Error(LOG.NETWORK, "Decrypt responce is null");
      }
      else
      {
        bool flag3 = true;
        try
        {
          msg = NetworkManager.GzUncompress(gzEncrypted);
        }
        catch (Exception ex)
        {
          Log.Exception(ex);
          flag3 = false;
        }
        if (!flag3)
          msg = this.GenerateErrorMsg(Error.UncompressFailed);
        else if (base64Signature == null)
        {
          msg = this.GenerateErrorMsg(Error.SignatureIsNull);
          Log.Error(LOG.NETWORK, "Signature is null");
        }
        else
        {
          bool flag4 = true;
          bool flag5 = true;
          try
          {
            flag5 = Cipher.verify(msg, base64Signature);
          }
          catch (Exception ex)
          {
            Log.Exception(ex);
            flag4 = false;
          }
          if (!flag4)
            msg = this.GenerateErrorMsg(Error.VerifySignatureFailed);
          else if (!flag5)
          {
            msg = this.GenerateErrorMsg(Error.InvalidSignature);
          }
          else
          {
            msg = Regex.Unescape(msg);
            if (msg == "{\"error\":0,\"result\":[]}")
              msg = this.GenerateErrorMsg(Error.EmptyRecord);
            if (msg.Contains("\"result\":[]"))
              msg = msg.Replace("\"result\":[]", "\"dummy\":[]");
          }
        }
      }
label_63:
      this.wwwInfos.Remove(wwwinfo);
      wwwinfo = (NetworkManager.WWWInfo) null;
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
        catch (Exception ex)
        {
          obj = JSONSerializer.Deserialize<T>(this.GenerateErrorMsg(Error.DecodeFailed));
          Debug.LogException(ex);
        }
        finally
        {
          int error = (int) obj.Error;
          obj.Apply();
          CrashlyticsReporter.SetAPIRequestStatus(false);
          call_back(obj);
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogException(ex);
      CrashlyticsReporter.SetAPIRequestStatus(false);
      call_fatal();
    }
  }

  public string GenerateErrorMsg(Error error) => $"{{\"error\":{(object) (int) error}}}";

  public bool IsConnecting()
  {
    this.wwwInfos.RemoveAll((Predicate<NetworkManager.WWWInfo>) (i => i.www == null));
    return this.wwwInfos.FindAll((Predicate<NetworkManager.WWWInfo>) (i => !i.isCached)).Count > 0;
  }

  public static string GzUncompress(byte[] gzEncrypted)
  {
    return Encoding.UTF8.GetString(ZlibStream.UncompressBuffer(gzEncrypted));
  }

  public static byte[] GzUncompressByte(byte[] gzEncrypted)
  {
    return ZlibStream.UncompressBuffer(gzEncrypted);
  }

  public static byte[] GzCompress(string decrypted) => GZipStream.CompressString(decrypted);

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
      this.www = _www;
      this.isAssetData = _isAssetData;
      this.isCached = _isCached;
    }
  }
}
