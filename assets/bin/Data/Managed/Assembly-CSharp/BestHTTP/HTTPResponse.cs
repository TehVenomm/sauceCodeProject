// Decompiled with JetBrains decompiler
// Type: BestHTTP.HTTPResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using BestHTTP.Caching;
using BestHTTP.Decompression.Zlib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

#nullable disable
namespace BestHTTP;

public class HTTPResponse : IDisposable
{
  internal const byte CR = 13;
  internal const byte LF = 10;
  internal const int BufferSize = 4096 /*0x1000*/;
  protected string dataAsText;
  protected Texture2D texture;
  protected HTTPRequest baseRequest;
  protected Stream Stream;
  protected List<byte[]> streamedFragments;
  protected object SyncRoot = new object();
  protected byte[] fragmentBuffer;
  protected int fragmentBufferDataLength;
  protected Stream cacheStream;
  protected int allFragmentSize;

  public int VersionMajor { get; protected set; }

  public int VersionMinor { get; protected set; }

  public int StatusCode { get; protected set; }

  public string Message { get; protected set; }

  public bool IsStreamed { get; protected set; }

  public bool IsStreamingFinished { get; internal set; }

  public bool IsFromCache { get; protected set; }

  public Dictionary<string, List<string>> Headers { get; protected set; }

  public byte[] Data { get; internal set; }

  public bool IsUpgraded { get; protected set; }

  public string DataAsText
  {
    get
    {
      if (this.Data == null)
        return string.Empty;
      return !string.IsNullOrEmpty(this.dataAsText) ? this.dataAsText : (this.dataAsText = Encoding.UTF8.GetString(this.Data, 0, this.Data.Length));
    }
  }

  public Texture2D DataAsTexture2D
  {
    get
    {
      if (this.Data == null)
        return (Texture2D) null;
      if (Object.op_Inequality((Object) this.texture, (Object) null))
        return this.texture;
      this.texture = new Texture2D(0, 0);
      this.texture.LoadRawTextureData(this.Data);
      return this.texture;
    }
  }

  internal HTTPResponse(HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache)
  {
    this.baseRequest = request;
    this.Stream = stream;
    this.IsStreamed = isStreamed;
    this.IsFromCache = isFromCache;
  }

  internal virtual bool Receive(int forceReadRawContentLength = -1)
  {
    string empty = string.Empty;
    string str;
    try
    {
      str = this.ReadTo(this.Stream, (byte) 32 /*0x20*/);
    }
    catch (Exception ex)
    {
      if (!this.baseRequest.DisableRetry)
        return false;
      throw ex;
    }
    if (!this.baseRequest.DisableRetry && string.IsNullOrEmpty(str))
      return false;
    string[] strArray = str.Split('/', '.');
    this.VersionMajor = int.Parse(strArray[1]);
    this.VersionMinor = int.Parse(strArray[2]);
    string s = this.ReadTo(this.Stream, (byte) 32 /*0x20*/);
    int result;
    if (this.baseRequest.DisableRetry)
      result = int.Parse(s);
    else if (!int.TryParse(s, out result))
      return false;
    this.StatusCode = result;
    this.Message = this.ReadTo(this.Stream, (byte) 10);
    this.ReadHeaders(this.Stream);
    this.IsUpgraded = this.StatusCode == 101 && this.HasHeaderWithValue("connection", "upgrade");
    if (forceReadRawContentLength != -1)
    {
      this.IsFromCache = true;
      this.ReadRaw(this.Stream, forceReadRawContentLength);
      return true;
    }
    if (this.StatusCode >= 100 && this.StatusCode < 200 || this.StatusCode == 204 || this.StatusCode == 304 || this.baseRequest.MethodType == HTTPMethods.Head)
      return true;
    if (this.HasHeaderWithValue("transfer-encoding", "chunked"))
    {
      this.ReadChunked(this.Stream);
    }
    else
    {
      List<string> headerValues1 = this.GetHeaderValues("content-length");
      List<string> headerValues2 = this.GetHeaderValues("content-range");
      if (headerValues1 != null && headerValues2 == null)
        this.ReadRaw(this.Stream, int.Parse(headerValues1[0]));
      else if (headerValues2 != null)
      {
        HTTPRange range = this.GetRange();
        this.ReadRaw(this.Stream, range.LastBytePos - range.FirstBytePos + 1);
      }
    }
    return true;
  }

  protected void ReadHeaders(Stream stream)
  {
    for (string name = this.ReadTo(stream, (byte) 58, (byte) 10).Trim(); name != string.Empty; name = this.ReadTo(stream, (byte) 58, (byte) 10))
    {
      string str = this.ReadTo(stream, (byte) 10);
      this.AddHeader(name, str);
    }
  }

  protected void AddHeader(string name, string value)
  {
    name = name.ToLower();
    if (this.Headers == null)
      this.Headers = new Dictionary<string, List<string>>();
    List<string> stringList;
    if (!this.Headers.TryGetValue(name, out stringList))
      this.Headers.Add(name, stringList = new List<string>(1));
    stringList.Add(value);
  }

  public List<string> GetHeaderValues(string name)
  {
    name = name.ToLower();
    List<string> stringList;
    return !this.Headers.TryGetValue(name, out stringList) || stringList.Count == 0 ? (List<string>) null : stringList;
  }

  public string GetFirstHeaderValue(string name)
  {
    name = name.ToLower();
    List<string> stringList;
    return !this.Headers.TryGetValue(name, out stringList) || stringList.Count == 0 ? (string) null : stringList[0];
  }

  public bool HasHeaderWithValue(string headerName, string value)
  {
    List<string> headerValues = this.GetHeaderValues(headerName);
    if (headerValues == null)
      return false;
    for (int index = 0; index < headerValues.Count; ++index)
    {
      if (string.Compare(headerValues[index], value, StringComparison.InvariantCultureIgnoreCase) == 0)
        return true;
    }
    return false;
  }

  public bool HasHeader(string headerName) => this.GetHeaderValues(headerName) != null;

  public HTTPRange GetRange()
  {
    string[] strArray = (this.GetHeaderValues("content-range") ?? throw null)[0].Split(new char[3]
    {
      ' ',
      '-',
      '/'
    }, StringSplitOptions.RemoveEmptyEntries);
    return strArray[1] == "*" ? new HTTPRange(int.Parse(strArray[2])) : new HTTPRange(int.Parse(strArray[1]), int.Parse(strArray[2]), strArray[3] != "*" ? int.Parse(strArray[3]) : -1);
  }

  protected string ReadTo(Stream stream, byte blocker)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      for (int index = stream.ReadByte(); index != (int) blocker && index != -1; index = stream.ReadByte())
        memoryStream.WriteByte((byte) index);
      return memoryStream.ToArray().AsciiToString().Trim();
    }
  }

  protected string ReadTo(Stream stream, byte blocker1, byte blocker2)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      for (int index = stream.ReadByte(); index != (int) blocker1 && index != (int) blocker2 && index != -1; index = stream.ReadByte())
        memoryStream.WriteByte((byte) index);
      return memoryStream.ToArray().AsciiToString().Trim();
    }
  }

  protected int ReadChunkLength(Stream stream)
  {
    return int.Parse(this.ReadTo(stream, (byte) 10).Split(';')[0], NumberStyles.AllowHexSpecifier);
  }

  protected void ReadChunked(Stream stream)
  {
    this.BeginReceiveStreamFragments();
    using (MemoryStream streamToDecode = new MemoryStream())
    {
      int newSize = this.ReadChunkLength(stream);
      byte[] array = new byte[newSize];
      int num1 = 0;
      for (; newSize != 0; newSize = this.ReadChunkLength(stream))
      {
        if (array.Length < newSize)
          Array.Resize<byte>(ref array, newSize);
        int num2 = 0;
        this.WaitWhileHasFragments();
        do
        {
          num2 += stream.Read(array, num2, newSize - num2);
        }
        while (num2 < newSize);
        if (this.baseRequest.UseStreaming)
          this.FeedStreamFragment(array, 0, num2);
        else
          streamToDecode.Write(array, 0, num2);
        this.ReadTo(stream, (byte) 10);
        num1 += num2;
      }
      if (this.baseRequest.UseStreaming)
        this.FlushRemainingFragmentBuffer();
      this.ReadHeaders(stream);
      if (this.baseRequest.UseStreaming)
        return;
      this.Data = this.DecodeStream((Stream) streamToDecode);
    }
  }

  internal void ReadRaw(Stream stream, int contentLength)
  {
    this.BeginReceiveStreamFragments();
    using (MemoryStream streamToDecode = new MemoryStream(this.baseRequest.UseStreaming ? 0 : contentLength))
    {
      byte[] buffer = new byte[Math.Min(this.baseRequest.StreamFragmentSize, 4096 /*0x1000*/)];
      while (contentLength > 0)
      {
        int num1 = 0;
        this.WaitWhileHasFragments();
        do
        {
          int num2 = stream.Read(buffer, num1, Math.Min(contentLength, buffer.Length - num1));
          num1 += num2;
          contentLength -= num2;
        }
        while (num1 < buffer.Length && contentLength > 0);
        if (this.baseRequest.UseStreaming)
          this.FeedStreamFragment(buffer, 0, num1);
        else
          streamToDecode.Write(buffer, 0, num1);
      }
      if (this.baseRequest.UseStreaming)
        this.FlushRemainingFragmentBuffer();
      if (this.baseRequest.UseStreaming)
        return;
      this.Data = this.DecodeStream((Stream) streamToDecode);
    }
  }

  protected byte[] DecodeStream(Stream streamToDecode)
  {
    streamToDecode.Seek(0L, SeekOrigin.Begin);
    List<string> headerValues = this.IsFromCache ? (List<string>) null : this.GetHeaderValues("content-encoding");
    Stream stream;
    if (headerValues == null)
    {
      stream = streamToDecode;
    }
    else
    {
      switch (headerValues[0])
      {
        case "identity":
          stream = streamToDecode;
          break;
        case "gzip":
          stream = (Stream) new GZipStream(streamToDecode, CompressionMode.Decompress);
          break;
        case "deflate":
          stream = (Stream) new DeflateStream(streamToDecode, CompressionMode.Decompress);
          break;
        default:
          throw new NotSupportedException("Not supported encoding found: " + headerValues[0]);
      }
    }
    using (MemoryStream memoryStream = new MemoryStream((int) streamToDecode.Length))
    {
      byte[] buffer = new byte[1024 /*0x0400*/];
      int count;
      while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
        memoryStream.Write(buffer, 0, count);
      return memoryStream.ToArray();
    }
  }

  protected void BeginReceiveStreamFragments()
  {
    if (!this.baseRequest.DisableCache && this.baseRequest.UseStreaming && !this.IsFromCache && HTTPCacheService.IsCacheble(this.baseRequest.CurrentUri, this.baseRequest.MethodType, this))
      this.cacheStream = HTTPCacheService.PrepareStreamed(this.baseRequest.CurrentUri, this);
    this.allFragmentSize = 0;
  }

  protected void FeedStreamFragment(byte[] buffer, int pos, int length)
  {
    if (this.fragmentBuffer == null)
    {
      this.fragmentBuffer = new byte[this.baseRequest.StreamFragmentSize];
      this.fragmentBufferDataLength = 0;
    }
    if (this.fragmentBufferDataLength + length <= this.baseRequest.StreamFragmentSize)
    {
      Array.Copy((Array) buffer, pos, (Array) this.fragmentBuffer, this.fragmentBufferDataLength, length);
      this.fragmentBufferDataLength += length;
      if (this.fragmentBufferDataLength != this.baseRequest.StreamFragmentSize)
        return;
      this.AddStreamedFragment(this.fragmentBuffer);
      this.fragmentBuffer = (byte[]) null;
      this.fragmentBufferDataLength = 0;
    }
    else
    {
      int length1 = this.baseRequest.StreamFragmentSize - this.fragmentBufferDataLength;
      this.FeedStreamFragment(buffer, pos, length1);
      this.FeedStreamFragment(buffer, pos + length1, length - length1);
    }
  }

  protected void FlushRemainingFragmentBuffer()
  {
    if (this.fragmentBuffer != null)
    {
      Array.Resize<byte>(ref this.fragmentBuffer, this.fragmentBufferDataLength);
      this.AddStreamedFragment(this.fragmentBuffer);
      this.fragmentBuffer = (byte[]) null;
      this.fragmentBufferDataLength = 0;
    }
    if (this.cacheStream == null)
      return;
    this.cacheStream.Close();
    this.cacheStream = (Stream) null;
    HTTPCacheService.SetBodyLength(this.baseRequest.CurrentUri, this.allFragmentSize);
  }

  protected void AddStreamedFragment(byte[] buffer)
  {
    lock (this.SyncRoot)
    {
      if (this.streamedFragments == null)
        this.streamedFragments = new List<byte[]>();
      this.streamedFragments.Add(buffer);
      if (this.cacheStream == null)
        return;
      this.cacheStream.Write(buffer, 0, buffer.Length);
      this.allFragmentSize += buffer.Length;
    }
  }

  protected void WaitWhileHasFragments()
  {
  }

  public List<byte[]> GetStreamedFragments()
  {
    lock (this.SyncRoot)
    {
      if (this.streamedFragments == null || this.streamedFragments.Count == 0)
        return (List<byte[]>) null;
      List<byte[]> streamedFragments = new List<byte[]>((IEnumerable<byte[]>) this.streamedFragments);
      this.streamedFragments.Clear();
      return streamedFragments;
    }
  }

  internal bool HasStreamedFragments()
  {
    lock (this.SyncRoot)
      return this.streamedFragments != null && this.streamedFragments.Count > 0;
  }

  internal void FinishStreaming()
  {
    this.IsStreamingFinished = true;
    this.Dispose();
  }

  public void Dispose()
  {
    if (this.cacheStream == null)
      return;
    this.cacheStream.Close();
    this.cacheStream = (Stream) null;
  }
}
