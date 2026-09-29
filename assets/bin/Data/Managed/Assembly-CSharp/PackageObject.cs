// Decompiled with JetBrains decompiler
// Type: PackageObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using UnityEngine;

#nullable disable
public class PackageObject
{
  public int refCount;
  public string name;
  public object obj;
  public BetterList<PackageObject> linkPackages;
  public UIAtlas hostAtlas;
  public bool unloadAllLoadedObjects;

  public static void ClearPoolObjects() => rymTPool<PackageObject>.Clear();

  public static PackageObject Get() => rymTPool<PackageObject>.Get();

  public static PackageObject Get(string name, object obj)
  {
    PackageObject packageObject = rymTPool<PackageObject>.Get();
    packageObject.name = name;
    packageObject.obj = obj;
    packageObject.refCount = 0;
    return packageObject;
  }

  public static void Release(ref PackageObject obj)
  {
    AssetBundle assetBundle = obj.obj as AssetBundle;
    if (Object.op_Inequality((Object) assetBundle, (Object) null))
      assetBundle.Unload(false);
    obj.Reset();
    rymTPool<PackageObject>.Release(ref obj);
  }

  public PackageObject()
  {
    this.refCount = 0;
    this.linkPackages = new BetterList<PackageObject>();
    this.obj = (object) null;
  }

  public void Reset()
  {
    this.refCount = 0;
    this.name = (string) null;
    this.obj = (object) null;
    this.linkPackages.Clear();
  }

  private class Pool_PackageObject : rymTPool<PackageObject>
  {
  }
}
