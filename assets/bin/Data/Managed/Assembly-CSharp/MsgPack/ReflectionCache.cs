// Decompiled with JetBrains decompiler
// Type: MsgPack.ReflectionCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace MsgPack;

public static class ReflectionCache
{
  private static Dictionary<Type, ReflectionCacheEntry> _cache = new Dictionary<Type, ReflectionCacheEntry>();

  public static ReflectionCacheEntry Lookup(Type type)
  {
    lock (ReflectionCache._cache)
    {
      ReflectionCacheEntry reflectionCacheEntry;
      if (ReflectionCache._cache.TryGetValue(type, out reflectionCacheEntry))
        return reflectionCacheEntry;
    }
    ReflectionCacheEntry reflectionCacheEntry1 = new ReflectionCacheEntry(type);
    lock (ReflectionCache._cache)
      ReflectionCache._cache[type] = reflectionCacheEntry1;
    return reflectionCacheEntry1;
  }

  public static void RemoveCache(Type type)
  {
    lock (ReflectionCache._cache)
      ReflectionCache._cache.Remove(type);
  }

  public static void Clear()
  {
    lock (ReflectionCache._cache)
      ReflectionCache._cache.Clear();
  }
}
