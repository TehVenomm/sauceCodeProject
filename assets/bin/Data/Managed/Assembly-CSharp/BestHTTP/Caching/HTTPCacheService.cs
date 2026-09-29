// Decompiled with JetBrains decompiler
// Type: BestHTTP.Caching.HTTPCacheService
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

#nullable disable
namespace BestHTTP.Caching;

public static class HTTPCacheService
{
  private const int LibraryVersion = 1;
  private static Dictionary<Uri, HTTPCacheFileInfo> library;
  private static bool InClearThread;
  private static bool InMaintainenceThread;

  private static Dictionary<Uri, HTTPCacheFileInfo> Library
  {
    get
    {
      HTTPCacheService.LoadLibrary();
      return HTTPCacheService.library;
    }
  }

  internal static string CacheFolder { get; set; }

  private static string LibraryPath { get; set; }

  private static string GetFileNameFromUri(Uri uri)
  {
    return Convert.ToBase64String(uri.ToString().GetASCIIBytes()).Replace('/', '-');
  }

  private static Uri GetUriFromFileName(string fileName)
  {
    return new Uri(Convert.FromBase64String(fileName.Replace('-', '/')).AsciiToString());
  }

  private static void CheckSetup()
  {
    try
    {
      HTTPCacheService.SetupCacheFolder();
      HTTPCacheService.LoadLibrary();
    }
    catch
    {
    }
  }

  internal static void SetupCacheFolder()
  {
    if (!string.IsNullOrEmpty(HTTPCacheService.CacheFolder))
      return;
    HTTPCacheService.CacheFolder = Path.Combine(Application.persistentDataPath, "HTTPCache");
    if (!Directory.Exists(HTTPCacheService.CacheFolder))
      Directory.CreateDirectory(HTTPCacheService.CacheFolder);
    HTTPCacheService.LibraryPath = Path.Combine(Application.persistentDataPath, "Library");
  }

  internal static bool HasEntity(Uri uri)
  {
    lock (HTTPCacheService.Library)
      return HTTPCacheService.Library.ContainsKey(uri);
  }

  internal static void DeleteEntity(Uri uri)
  {
    object obj = HTTPCacheFileLock.Acquire(uri);
    if (!Monitor.TryEnter(obj, TimeSpan.FromSeconds(0.5)))
      return;
    try
    {
      lock (HTTPCacheService.Library)
      {
        HTTPCacheFileInfo httpCacheFileInfo;
        int num = HTTPCacheService.Library.TryGetValue(uri, out httpCacheFileInfo) ? 1 : 0;
        if (num != 0)
          httpCacheFileInfo.Delete();
        if (num == 0)
          return;
        HTTPCacheService.Library.Remove(uri);
      }
    }
    finally
    {
      Monitor.Exit(obj);
    }
  }

  internal static bool IsCachedEntityExpiresInTheFuture(HTTPRequest request)
  {
    lock (HTTPCacheService.Library)
    {
      HTTPCacheFileInfo httpCacheFileInfo;
      if (HTTPCacheService.Library.TryGetValue(request.CurrentUri, out httpCacheFileInfo))
        return httpCacheFileInfo.WillExpireInTheFuture();
    }
    return false;
  }

  internal static void SetHeaders(HTTPRequest request)
  {
    lock (HTTPCacheService.Library)
    {
      HTTPCacheFileInfo httpCacheFileInfo;
      if (!HTTPCacheService.Library.TryGetValue(request.CurrentUri, out httpCacheFileInfo))
        return;
      httpCacheFileInfo.SetUpRevalidationHeaders(request);
    }
  }

  internal static Stream GetBody(Uri uri, out int length)
  {
    length = 0;
    lock (HTTPCacheService.Library)
    {
      HTTPCacheFileInfo httpCacheFileInfo;
      if (HTTPCacheService.Library.TryGetValue(uri, out httpCacheFileInfo))
        return httpCacheFileInfo.GetBodyStream(out length);
    }
    return (Stream) null;
  }

  internal static HTTPResponse GetFullResponse(HTTPRequest request)
  {
    lock (HTTPCacheService.Library)
    {
      HTTPCacheFileInfo httpCacheFileInfo;
      if (HTTPCacheService.Library.TryGetValue(request.CurrentUri, out httpCacheFileInfo))
        return httpCacheFileInfo.ReadResponseTo(request);
    }
    return (HTTPResponse) null;
  }

  internal static bool IsCacheble(Uri uri, HTTPMethods method, HTTPResponse response)
  {
    if (method != HTTPMethods.Get || response == null || response.StatusCode == 304 || response.StatusCode < 200 || response.StatusCode >= 400)
      return false;
    List<string> headerValues1 = response.GetHeaderValues("cache-control");
    if (headerValues1 != null && headerValues1[0].ToLower().Contains("no-store"))
      return false;
    List<string> headerValues2 = response.GetHeaderValues("pragma");
    return (headerValues2 == null || !headerValues2[0].ToLower().Contains("no-cache")) && response.GetHeaderValues("content-range") == null;
  }

  internal static void Store(Uri uri, HTTPMethods method, HTTPResponse response)
  {
    if (response == null || response.Data == null || response.Data.Length == 0)
      return;
    lock (HTTPCacheService.Library)
    {
      HTTPCacheFileInfo httpCacheFileInfo;
      if (!HTTPCacheService.Library.TryGetValue(uri, out httpCacheFileInfo))
        HTTPCacheService.Library.Add(uri, httpCacheFileInfo = new HTTPCacheFileInfo(uri));
      try
      {
        httpCacheFileInfo.Store(response);
      }
      catch (Exception ex)
      {
        HTTPCacheService.DeleteEntity(uri);
        throw ex;
      }
    }
  }

  internal static Stream PrepareStreamed(Uri uri, HTTPResponse response)
  {
    lock (HTTPCacheService.Library)
    {
      HTTPCacheFileInfo httpCacheFileInfo;
      if (!HTTPCacheService.Library.TryGetValue(uri, out httpCacheFileInfo))
        HTTPCacheService.Library.Add(uri, httpCacheFileInfo = new HTTPCacheFileInfo(uri));
      try
      {
        return httpCacheFileInfo.GetSaveStream(response);
      }
      catch (Exception ex)
      {
        HTTPCacheService.DeleteEntity(uri);
        throw ex;
      }
    }
  }

  public static void BeginClear()
  {
    if (HTTPCacheService.InClearThread)
      return;
    HTTPCacheService.InClearThread = true;
    HTTPCacheService.SetupCacheFolder();
    ThreadPool.QueueUserWorkItem((WaitCallback) (param =>
    {
      try
      {
        foreach (string file in Directory.GetFiles(HTTPCacheService.CacheFolder))
        {
          try
          {
            HTTPCacheService.DeleteEntity(HTTPCacheService.GetUriFromFileName(Path.GetFileName(file)));
          }
          catch
          {
          }
        }
      }
      finally
      {
        HTTPCacheService.SaveLibrary();
        HTTPCacheService.InClearThread = false;
      }
    }));
  }

  public static void BeginMaintainence(HTTPCacheMaintananceParams maintananceParams)
  {
    if (maintananceParams == null)
      throw new ArgumentNullException("maintananceParams == null");
    if (HTTPCacheService.InMaintainenceThread)
      return;
    HTTPCacheService.InMaintainenceThread = true;
    HTTPCacheService.SetupCacheFolder();
    ThreadPool.QueueUserWorkItem((WaitCallback) (maintananceParam =>
    {
      HTTPCacheMaintananceParams maintananceParams1 = maintananceParam as HTTPCacheMaintananceParams;
      try
      {
        lock (HTTPCacheService.Library)
        {
          DateTime dateTime = DateTime.UtcNow - maintananceParams1.DeleteOlder;
          foreach (KeyValuePair<Uri, HTTPCacheFileInfo> keyValuePair in HTTPCacheService.Library)
          {
            if (keyValuePair.Value.LastAccess < dateTime)
              HTTPCacheService.DeleteEntity(keyValuePair.Key);
          }
          ulong cacheSize = HTTPCacheService.GetCacheSize();
          if (cacheSize <= maintananceParams1.MaxCacheSize)
            return;
          List<HTTPCacheFileInfo> httpCacheFileInfoList = new List<HTTPCacheFileInfo>(HTTPCacheService.library.Count);
          foreach (KeyValuePair<Uri, HTTPCacheFileInfo> keyValuePair in HTTPCacheService.library)
            httpCacheFileInfoList.Add(keyValuePair.Value);
          httpCacheFileInfoList.Sort();
          int index = 0;
          while (cacheSize >= maintananceParams1.MaxCacheSize)
          {
            if (index >= httpCacheFileInfoList.Count)
              break;
            try
            {
              HTTPCacheFileInfo httpCacheFileInfo = httpCacheFileInfoList[index];
              ulong bodyLength = (ulong) httpCacheFileInfo.BodyLength;
              HTTPCacheService.DeleteEntity(httpCacheFileInfo.Uri);
              cacheSize -= bodyLength;
            }
            catch
            {
            }
            finally
            {
              ++index;
            }
          }
        }
      }
      finally
      {
        HTTPCacheService.SaveLibrary();
        HTTPCacheService.InMaintainenceThread = false;
      }
    }), (object) maintananceParams);
  }

  public static int GetCacheEntityCount()
  {
    HTTPCacheService.CheckSetup();
    lock (HTTPCacheService.Library)
      return HTTPCacheService.Library.Count;
  }

  public static ulong GetCacheSize()
  {
    HTTPCacheService.CheckSetup();
    ulong cacheSize = 0;
    lock (HTTPCacheService.Library)
    {
      foreach (KeyValuePair<Uri, HTTPCacheFileInfo> keyValuePair in HTTPCacheService.Library)
      {
        if (keyValuePair.Value.BodyLength > 0)
          cacheSize += (ulong) keyValuePair.Value.BodyLength;
      }
    }
    return cacheSize;
  }

  private static void LoadLibrary()
  {
    if (HTTPCacheService.library != null)
      return;
    HTTPCacheService.library = new Dictionary<Uri, HTTPCacheFileInfo>();
    if (!File.Exists(HTTPCacheService.LibraryPath))
    {
      HTTPCacheService.DeleteUnusedFiles();
    }
    else
    {
      try
      {
        lock (HTTPCacheService.Library)
        {
          using (FileStream input = new FileStream(HTTPCacheService.LibraryPath, FileMode.Open))
          {
            using (BinaryReader reader = new BinaryReader((Stream) input))
            {
              int version = reader.ReadInt32();
              int num = reader.ReadInt32();
              for (int index = 0; index < num; ++index)
              {
                Uri uri = new Uri(reader.ReadString());
                if (File.Exists(Path.Combine(HTTPCacheService.CacheFolder, HTTPCacheService.GetFileNameFromUri(uri))))
                  HTTPCacheService.Library.Add(uri, new HTTPCacheFileInfo(uri, reader, version));
              }
            }
          }
        }
        HTTPCacheService.DeleteUnusedFiles();
      }
      catch
      {
      }
    }
  }

  internal static void SaveLibrary()
  {
    try
    {
      lock (HTTPCacheService.Library)
      {
        using (FileStream output = new FileStream(HTTPCacheService.LibraryPath, FileMode.Create))
        {
          using (BinaryWriter writer = new BinaryWriter((Stream) output))
          {
            writer.Write(1);
            writer.Write(HTTPCacheService.Library.Count);
            foreach (KeyValuePair<Uri, HTTPCacheFileInfo> keyValuePair in HTTPCacheService.Library)
            {
              writer.Write(keyValuePair.Key.ToString());
              keyValuePair.Value.SaveTo(writer);
            }
          }
        }
      }
    }
    catch
    {
    }
  }

  internal static void SetBodyLength(Uri uri, int bodyLength)
  {
    lock (HTTPCacheService.Library)
    {
      HTTPCacheFileInfo httpCacheFileInfo1;
      if (HTTPCacheService.Library.TryGetValue(uri, out httpCacheFileInfo1))
      {
        httpCacheFileInfo1.BodyLength = bodyLength;
      }
      else
      {
        HTTPCacheFileInfo httpCacheFileInfo2;
        HTTPCacheService.Library.Add(uri, httpCacheFileInfo2 = new HTTPCacheFileInfo(uri, DateTime.UtcNow, bodyLength));
      }
    }
  }

  private static void DeleteUnusedFiles()
  {
    HTTPCacheService.CheckSetup();
    foreach (string file in Directory.GetFiles(HTTPCacheService.CacheFolder))
    {
      try
      {
        Uri uriFromFileName = HTTPCacheService.GetUriFromFileName(Path.GetFileName(file));
        lock (HTTPCacheService.Library)
        {
          if (!HTTPCacheService.Library.ContainsKey(uriFromFileName))
            HTTPCacheService.DeleteEntity(uriFromFileName);
        }
      }
      catch
      {
      }
    }
  }
}
