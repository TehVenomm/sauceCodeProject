// Decompiled with JetBrains decompiler
// Type: AssetBundleInfoCollection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AssetBundleInfoCollection : ScriptableObject
{
  public List<AssetBundleInfoCollection.Info> assetBundles = new List<AssetBundleInfoCollection.Info>();

  [Serializable]
  public class Info
  {
    public string assetBundleName;
    public List<AssetBundleInfoCollection.AssetInfo> assetsInfo;
    public uint crc;
    public string hash;
    public long size;
  }

  [Serializable]
  public class AssetInfo
  {
    public string assetName;
    public List<string> subAssetNames;
  }
}
