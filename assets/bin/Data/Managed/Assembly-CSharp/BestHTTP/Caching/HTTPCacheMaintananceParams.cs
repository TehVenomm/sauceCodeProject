// Decompiled with JetBrains decompiler
// Type: BestHTTP.Caching.HTTPCacheMaintananceParams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace BestHTTP.Caching;

public sealed class HTTPCacheMaintananceParams
{
  public TimeSpan DeleteOlder { get; private set; }

  public ulong MaxCacheSize { get; private set; }

  public HTTPCacheMaintananceParams(TimeSpan deleteOlder, ulong maxCacheSize)
  {
    this.DeleteOlder = deleteOlder;
    this.MaxCacheSize = maxCacheSize;
  }
}
