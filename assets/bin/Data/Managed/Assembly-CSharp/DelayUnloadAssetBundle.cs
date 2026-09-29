// Decompiled with JetBrains decompiler
// Type: DelayUnloadAssetBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using UnityEngine;

#nullable disable
public class DelayUnloadAssetBundle
{
  public string name;
  public AssetBundle assetBundle;

  public static void ClearPoolObjects() => rymTPool<DelayUnloadAssetBundle>.Clear();

  public static DelayUnloadAssetBundle Get(string name, AssetBundle asset_bundle)
  {
    DelayUnloadAssetBundle unloadAssetBundle = rymTPool<DelayUnloadAssetBundle>.Get();
    unloadAssetBundle.name = name;
    unloadAssetBundle.assetBundle = asset_bundle;
    return unloadAssetBundle;
  }

  public static void Release(ref DelayUnloadAssetBundle obj)
  {
    if (Object.op_Inequality((Object) obj.assetBundle, (Object) null))
      obj.assetBundle.Unload(false);
    obj.Reset();
    rymTPool<DelayUnloadAssetBundle>.Release(ref obj);
  }

  public void Reset()
  {
    this.name = (string) null;
    this.assetBundle = (AssetBundle) null;
  }

  private class Pool_DelayUnloadAssetBundle : rymTPool<DelayUnloadAssetBundle>
  {
  }
}
