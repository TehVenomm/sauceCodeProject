// Decompiled with JetBrains decompiler
// Type: BestHTTP.Caching.HTTPCacheFileInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace BestHTTP.Caching;

internal class HTTPCacheFileInfo : IComparable<HTTPCacheFileInfo>
{
  internal Uri Uri { get; set; }

  internal DateTime LastAccess { get; set; }

  internal int BodyLength { get; set; }

  private string ETag { get; set; }

  private string LastModified { get; set; }

  private DateTime Expires { get; set; }

  private long Age { get; set; }

  private long MaxAge { get; set; }

  private DateTime Date { get; set; }

  private bool MustRevalidate { get; set; }

  private DateTime Received { get; set; }

  internal HTTPCacheFileInfo(Uri uri)
    : this(uri, DateTime.UtcNow, -1)
  {
  }

  internal HTTPCacheFileInfo(Uri uri, DateTime lastAcces, int bodyLength)
  {
    this.Uri = uri;
    this.LastAccess = lastAcces;
    this.BodyLength = bodyLength;
    this.MaxAge = -1L;
  }

  internal HTTPCacheFileInfo(Uri uri, BinaryReader reader, int version)
  {
    this.Uri = uri;
    this.LastAccess = DateTime.FromBinary(reader.ReadInt64());
    this.BodyLength = reader.ReadInt32();
    if (version != 1)
      return;
    this.ETag = reader.ReadString();
    this.LastModified = reader.ReadString();
    this.Expires = DateTime.FromBinary(reader.ReadInt64());
    this.Age = reader.ReadInt64();
    this.MaxAge = reader.ReadInt64();
    this.Date = DateTime.FromBinary(reader.ReadInt64());
    this.MustRevalidate = reader.ReadBoolean();
    this.Received = DateTime.FromBinary(reader.ReadInt64());
  }

  internal void SaveTo(BinaryWriter writer)
  {
    writer.Write(this.LastAccess.ToBinary());
    writer.Write(this.BodyLength);
    writer.Write(this.ETag);
    writer.Write(this.LastModified);
    writer.Write(this.Expires.ToBinary());
    writer.Write(this.Age);
    writer.Write(this.MaxAge);
    writer.Write(this.Date.ToBinary());
    writer.Write(this.MustRevalidate);
    writer.Write(this.Received.ToBinary());
  }

  internal string GetFileName()
  {
    return Convert.ToBase64String(this.Uri.ToString().GetASCIIBytes()).Replace('/', '-');
  }

  internal string GetPath() => Path.Combine(HTTPCacheService.CacheFolder, this.GetFileName());

  internal bool IsExists() => File.Exists(this.GetPath());

  internal void Delete()
  {
    string path = this.GetPath();
    try
    {
      File.Delete(path);
    }
    catch
    {
    }
    finally
    {
      this.Reset();
    }
  }

  private void Reset()
  {
    this.BodyLength = -1;
    this.ETag = string.Empty;
    this.Expires = DateTime.FromBinary(0L);
    this.LastModified = string.Empty;
    this.Age = 0L;
    this.MaxAge = -1L;
    this.Date = DateTime.FromBinary(0L);
    this.MustRevalidate = false;
    this.Received = DateTime.FromBinary(0L);
  }

  private void SetUpCachingValues(HTTPResponse response)
  {
    this.ETag = response.GetFirstHeaderValue("ETag").ToStrOrEmpty();
    this.Expires = response.GetFirstHeaderValue("Expires").ToDateTime(DateTime.FromBinary(0L));
    this.LastModified = response.GetFirstHeaderValue("Last-Modified").ToStrOrEmpty();
    this.Age = response.GetFirstHeaderValue("Age").ToInt64();
    this.Date = response.GetFirstHeaderValue("Date").ToDateTime(DateTime.FromBinary(0L));
    string firstHeaderValue = response.GetFirstHeaderValue("cache-control");
    if (!string.IsNullOrEmpty(firstHeaderValue))
    {
      string[] option = firstHeaderValue.FindOption("Max-Age");
      double result;
      if (option != null && double.TryParse(option[1], out result))
        this.MaxAge = (long) (int) result;
      this.MustRevalidate = firstHeaderValue.ToLower().Contains("must-revalidate");
    }
    this.Received = DateTime.UtcNow;
  }

  internal bool WillExpireInTheFuture()
  {
    if (!this.IsExists() || this.MustRevalidate)
      return false;
    return this.MaxAge != -1L ? Math.Max(Math.Max(0L, (long) (this.Received - this.Date).TotalSeconds), this.Age) + (long) (DateTime.UtcNow - this.Date).TotalSeconds < this.MaxAge : this.Expires > DateTime.UtcNow;
  }

  internal void SetUpRevalidationHeaders(HTTPRequest request)
  {
    if (!this.IsExists())
      return;
    if (!string.IsNullOrEmpty(this.ETag))
      request.AddHeader("If-None-Match", this.ETag);
    if (string.IsNullOrEmpty(this.LastModified))
      return;
    request.AddHeader("If-Modified-Since", this.LastModified);
  }

  internal Stream GetBodyStream(out int length)
  {
    if (!this.IsExists())
    {
      length = 0;
      return (Stream) null;
    }
    length = this.BodyLength;
    this.LastAccess = DateTime.UtcNow;
    FileStream bodyStream = new FileStream(this.GetPath(), FileMode.Open);
    bodyStream.Seek((long) -length, SeekOrigin.End);
    return (Stream) bodyStream;
  }

  internal HTTPResponse ReadResponseTo(HTTPRequest request)
  {
    if (!this.IsExists())
      return (HTTPResponse) null;
    this.LastAccess = DateTime.UtcNow;
    using (FileStream fileStream = new FileStream(this.GetPath(), FileMode.Open))
    {
      HTTPResponse httpResponse = new HTTPResponse(request, (Stream) fileStream, request.UseStreaming, true);
      httpResponse.Receive(this.BodyLength);
      return httpResponse;
    }
  }

  internal void Store(HTTPResponse response)
  {
    if (this.GetPath().Length > HTTPManager.MaxPathLength)
      return;
    this.Delete();
    using (FileStream fs = new FileStream(this.GetPath(), FileMode.Create))
    {
      fs.WriteLine("HTTP/1.1 {0} {1}", (object) response.StatusCode, (object) response.Message);
      foreach (KeyValuePair<string, List<string>> header in response.Headers)
      {
        for (int index = 0; index < header.Value.Count; ++index)
          fs.WriteLine("{0}: {1}", (object) header.Key, (object) header.Value[index]);
      }
      fs.WriteLine();
      fs.Write(response.Data, 0, response.Data.Length);
    }
    this.BodyLength = response.Data.Length;
    this.LastAccess = DateTime.UtcNow;
    this.SetUpCachingValues(response);
  }

  internal Stream GetSaveStream(HTTPResponse response)
  {
    this.LastAccess = DateTime.UtcNow;
    this.Delete();
    if (this.GetPath().Length > HTTPManager.MaxPathLength)
      return (Stream) null;
    using (FileStream fs = new FileStream(this.GetPath(), FileMode.Create))
    {
      fs.WriteLine("HTTP/1.1 {0} {1}", (object) response.StatusCode, (object) response.Message);
      foreach (KeyValuePair<string, List<string>> header in response.Headers)
      {
        for (int index = 0; index < header.Value.Count; ++index)
          fs.WriteLine("{0}: {1}", (object) header.Key, (object) header.Value[index]);
      }
      fs.WriteLine();
    }
    if (response.IsFromCache && !response.Headers.ContainsKey("content-length"))
      response.Headers.Add("content-length", new List<string>()
      {
        this.BodyLength.ToString()
      });
    this.SetUpCachingValues(response);
    return (Stream) new FileStream(this.GetPath(), FileMode.Append);
  }

  public int CompareTo(HTTPCacheFileInfo other) => this.LastAccess.CompareTo((object) other);
}
