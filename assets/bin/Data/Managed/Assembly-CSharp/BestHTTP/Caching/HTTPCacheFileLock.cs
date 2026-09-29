// Decompiled with JetBrains decompiler
// Type: BestHTTP.Caching.HTTPCacheFileLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace BestHTTP.Caching;

internal sealed class HTTPCacheFileLock
{
  private static Dictionary<Uri, object> FileLocks = new Dictionary<Uri, object>();
  private static object SyncRoot = new object();

  internal static object Acquire(Uri uri)
  {
    lock (HTTPCacheFileLock.SyncRoot)
    {
      object obj;
      if (!HTTPCacheFileLock.FileLocks.TryGetValue(uri, out obj))
        HTTPCacheFileLock.FileLocks.Add(uri, obj = new object());
      return obj;
    }
  }

  internal static void Remove(Uri uri)
  {
    lock (HTTPCacheFileLock.SyncRoot)
    {
      if (!HTTPCacheFileLock.FileLocks.ContainsKey(uri))
        return;
      HTTPCacheFileLock.FileLocks.Remove(uri);
    }
  }

  internal static void Clear()
  {
    lock (HTTPCacheFileLock.SyncRoot)
      HTTPCacheFileLock.FileLocks.Clear();
  }
}
