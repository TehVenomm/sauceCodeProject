// Decompiled with JetBrains decompiler
// Type: BestHTTP.HTTPRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using BestHTTP.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

#nullable disable
namespace BestHTTP;

public sealed class HTTPRequest
{
  internal static readonly byte[] EOL = new byte[2]
  {
    (byte) 13,
    (byte) 10
  };
  public Action<HTTPRequest, HTTPResponse> OnUpgraded;
  private bool isKeepAlive;
  private bool disableCache;
  private int streamFragmentSize;
  private bool useStreaming;
  private Action<HTTPRequest, HTTPResponse> callback;

  public Uri Uri { get; private set; }

  public HTTPMethods MethodType { get; private set; }

  public byte[] RawData { get; set; }

  public bool IsKeepAlive
  {
    get => this.isKeepAlive;
    set
    {
      this.isKeepAlive = !this.isKeepAlive ? value : throw new NotSupportedException("Changing the IsKeepAlive property while processing the request is not supported.");
    }
  }

  public bool DisableCache
  {
    get => this.disableCache;
    set
    {
      if (this.Processing)
        throw new NotSupportedException("Changing the DisableCache property while processing the request is not supported.");
      this.disableCache = value;
    }
  }

  public bool UseStreaming
  {
    get => this.useStreaming;
    set
    {
      if (this.Processing)
        throw new NotSupportedException("Changing the UseStreaming property while processing the request is not supported.");
      this.useStreaming = value;
    }
  }

  public int StreamFragmentSize
  {
    get => this.streamFragmentSize;
    set
    {
      if (this.Processing)
        throw new NotSupportedException("Changing the StreamFragmentSize property while processing the request is not supported.");
      this.streamFragmentSize = value >= 1 ? value : throw new ArgumentException("StreamFragmentSize must be at least 1.");
    }
  }

  public Action<HTTPRequest, HTTPResponse> Callback
  {
    get => this.callback;
    set
    {
      if (this.Processing)
        throw new NotSupportedException("Changing the StreamFragmentSize property while processing the request is not supported.");
      this.callback = value;
    }
  }

  public bool DisableRetry { get; set; }

  public bool IsRedirected { get; internal set; }

  public Uri RedirectUri { get; internal set; }

  public Uri CurrentUri => !this.IsRedirected ? this.Uri : this.RedirectUri;

  public HTTPResponse Response { get; internal set; }

  public Exception Exception { get; internal set; }

  public object Tag { get; set; }

  public Credentials Credentials { get; set; }

  public int MaxRedirects { get; set; }

  public bool UseAlternateSSL { get; set; }

  internal bool Processing { get; set; }

  internal int RedirectCount { get; set; }

  private Dictionary<string, List<string>> Headers { get; set; }

  private WWWForm FieldsImpl { get; set; }

  public HTTPRequest(Uri uri)
    : this(uri, HTTPMethods.Get, HTTPManager.KeepAliveDefaultValue, HTTPManager.IsCachingDisabled, (Action<HTTPRequest, HTTPResponse>) null)
  {
  }

  public HTTPRequest(Uri uri, Action<HTTPRequest, HTTPResponse> callback)
    : this(uri, HTTPMethods.Get, HTTPManager.KeepAliveDefaultValue, HTTPManager.IsCachingDisabled, callback)
  {
  }

  public HTTPRequest(Uri uri, bool isKeepAlive, Action<HTTPRequest, HTTPResponse> callback)
    : this(uri, HTTPMethods.Get, isKeepAlive, HTTPManager.IsCachingDisabled, callback)
  {
  }

  public HTTPRequest(
    Uri uri,
    bool isKeepAlive,
    bool disableCache,
    Action<HTTPRequest, HTTPResponse> callback)
    : this(uri, HTTPMethods.Get, isKeepAlive, disableCache, callback)
  {
  }

  public HTTPRequest(Uri uri, HTTPMethods methodType, Action<HTTPRequest, HTTPResponse> callback)
    : this(uri, methodType, HTTPManager.KeepAliveDefaultValue, HTTPManager.IsCachingDisabled, callback)
  {
  }

  public HTTPRequest(
    Uri uri,
    HTTPMethods methodType,
    bool isKeepAlive,
    Action<HTTPRequest, HTTPResponse> callback)
    : this(uri, methodType, isKeepAlive, HTTPManager.IsCachingDisabled, callback)
  {
  }

  public HTTPRequest(
    Uri uri,
    HTTPMethods methodType,
    bool isKeepAlive,
    bool disableCache,
    Action<HTTPRequest, HTTPResponse> callback)
  {
    this.Uri = uri;
    this.MethodType = methodType;
    this.IsKeepAlive = isKeepAlive;
    this.DisableCache = disableCache;
    this.Callback = callback;
    this.StreamFragmentSize = 4096 /*0x1000*/;
    this.DisableRetry = methodType == HTTPMethods.Post;
    this.MaxRedirects = int.MaxValue;
    this.RedirectCount = 0;
  }

  public void AddField(string fieldName, string value)
  {
    if (this.FieldsImpl == null)
      this.FieldsImpl = new WWWForm();
    this.FieldsImpl.AddField(fieldName, value);
  }

  public void AddBinaryData(string fieldName, byte[] contents)
  {
    if (this.FieldsImpl == null)
      this.FieldsImpl = new WWWForm();
    this.FieldsImpl.AddBinaryData(fieldName, contents);
  }

  public void SetFields(WWWForm wwwForm) => this.FieldsImpl = wwwForm;

  public void AddHeader(string name, string value)
  {
    if (this.Headers == null)
      this.Headers = new Dictionary<string, List<string>>();
    List<string> stringList;
    if (!this.Headers.TryGetValue(name, out stringList))
      this.Headers.Add(name, stringList = new List<string>(1));
    stringList.Add(value);
  }

  public void SetHeader(string name, string value)
  {
    if (this.Headers == null)
      this.Headers = new Dictionary<string, List<string>>();
    List<string> stringList;
    if (!this.Headers.TryGetValue(name, out stringList))
      this.Headers.Add(name, stringList = new List<string>(1));
    stringList.Clear();
    stringList.Add(value);
  }

  public bool HasHeader(string name) => this.Headers != null && this.Headers.ContainsKey(name);

  public string GetFirstHeaderValue(string name)
  {
    if (this.Headers == null)
      return (string) null;
    List<string> stringList = (List<string>) null;
    return this.Headers.TryGetValue(name, out stringList) && stringList.Count > 0 ? stringList[0] : (string) null;
  }

  public void SetRangeHeader(int firstBytePos) => this.SetHeader("Range", $"bytes={firstBytePos}-");

  public void SetRangeHeader(int firstBytePos, int lastBytePos)
  {
    this.SetHeader("Range", $"bytes={firstBytePos}-{lastBytePos}");
  }

  private void SendHeaders(BinaryWriter stream)
  {
    this.SetHeader("Host", this.CurrentUri.Host);
    if (this.IsRedirected && !this.HasHeader("Referer"))
      this.AddHeader("Referer", this.Uri.ToString());
    if (!this.HasHeader("Accept-Encoding"))
      this.AddHeader("Accept-Encoding", "gzip, deflate, identity");
    if (!this.HasHeader("Connection"))
      this.AddHeader("Connection", this.IsKeepAlive ? "Keep-Alive, TE" : "Close, TE");
    if (!this.HasHeader("TE"))
      this.AddHeader("TE", "chunked, identity");
    byte[] entityBody = this.GetEntityBody();
    int length = entityBody != null ? entityBody.Length : 0;
    if (this.RawData == null)
    {
      byte[] data = this.FieldsImpl != null ? this.FieldsImpl.data : (byte[]) null;
      if (data != null && data.Length != 0 && !this.HasHeader("Content-Type"))
        this.AddHeader("Content-Type", "application/x-www-form-urlencoded");
    }
    if (!this.HasHeader("Content-Length") && length != 0)
      this.AddHeader("Content-Length", length.ToString());
    if (this.Credentials != null)
    {
      switch (this.Credentials.Type)
      {
        case AuthenticationTypes.Unknown:
        case AuthenticationTypes.Digest:
          Digest digest = DigestStore.Get(this.CurrentUri);
          if (digest != null)
          {
            string responseHeader = digest.GenerateResponseHeader(this);
            if (!string.IsNullOrEmpty(responseHeader))
            {
              this.SetHeader("Authorization", responseHeader);
              break;
            }
            break;
          }
          break;
        case AuthenticationTypes.Basic:
          this.SetHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{this.Credentials.UserName}:{this.Credentials.Password}")));
          break;
      }
    }
    foreach (KeyValuePair<string, List<string>> header in this.Headers)
    {
      byte[] asciiBytes = (header.Key + ": ").GetASCIIBytes();
      for (int index = 0; index < header.Value.Count; ++index)
      {
        stream.Write(asciiBytes);
        stream.Write(header.Value[index].GetASCIIBytes());
        stream.Write(HTTPRequest.EOL);
      }
    }
  }

  public string DumpHeaders()
  {
    using (MemoryStream output = new MemoryStream())
    {
      using (BinaryWriter stream = new BinaryWriter((Stream) output))
      {
        this.SendHeaders(stream);
        return output.ToArray().AsciiToString();
      }
    }
  }

  internal byte[] GetEntityBody()
  {
    if (this.RawData != null)
      return this.RawData;
    return this.FieldsImpl == null ? (byte[]) null : this.FieldsImpl.data;
  }

  internal bool SendOutTo(Stream stream)
  {
    bool flag = false;
    try
    {
      BinaryWriter stream1 = new BinaryWriter(stream);
      stream1.Write($"{this.MethodType.ToString().ToUpper()} {this.CurrentUri.PathAndQuery} HTTP/1.1".GetASCIIBytes());
      stream1.Write(HTTPRequest.EOL);
      this.SendHeaders(stream1);
      stream1.Write(HTTPRequest.EOL);
      byte[] buffer = this.RawData != null ? this.RawData : (this.FieldsImpl != null ? this.FieldsImpl.data : (byte[]) null);
      if (buffer != null && buffer.Length != 0)
        stream1.Write(buffer, 0, buffer.Length);
      flag = true;
    }
    catch
    {
    }
    return flag;
  }

  internal void UpgradeCallback()
  {
    if (this.Response == null)
      return;
    if (!this.Response.IsUpgraded)
      return;
    try
    {
      if (this.OnUpgraded == null)
        return;
      this.OnUpgraded(this, this.Response);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) $"{ex.Message}: {ex.StackTrace}");
    }
  }

  internal void CallCallback()
  {
    try
    {
      if (this.Callback == null)
        return;
      this.Callback(this, this.Response);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) $"{ex.Message}: {ex.StackTrace}");
    }
  }

  internal void FinishStreaming()
  {
    if (this.Response == null || !this.UseStreaming)
      return;
    this.Response.FinishStreaming();
  }

  public void Send() => HTTPManager.SendRequest(this);
}
