// Decompiled with JetBrains decompiler
// Type: EnemyDataCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.IO;
using UnityEngine;

#nullable disable
public class EnemyDataCache : DataCache
{
  public EnemyDataCache(string cachePath = null)
  {
    string cachePath1 = cachePath;
    if (string.IsNullOrEmpty(cachePath1))
      cachePath1 = Path.Combine(Application.temporaryCachePath, "assets/enemy");
    this.SetCachePath(cachePath1);
  }
}
