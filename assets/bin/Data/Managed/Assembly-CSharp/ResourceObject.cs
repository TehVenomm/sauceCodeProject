// Decompiled with JetBrains decompiler
// Type: ResourceObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ResourceObject
{
  private int _refCount;
  public PackageObject package;
  public RESOURCE_CATEGORY category;
  public Object obj;
  public string name;
  private Object[] willReleaseObjs;
  private static List<Object> willReleaseList = new List<Object>(2);

  public static void ClearPoolObjects() => rymTPool<ResourceObject>.Clear();

  public static ResourceObject Get(RESOURCE_CATEGORY category, string name, Object obj)
  {
    ResourceObject resourceObject = rymTPool<ResourceObject>.Get();
    resourceObject._refCount = 0;
    resourceObject.category = category;
    resourceObject.obj = obj;
    resourceObject.name = name;
    switch (resourceObject.category)
    {
      case RESOURCE_CATEGORY.EFFECT_TEX:
      case RESOURCE_CATEGORY.PLAYER_HIGH_RESO_TEX:
      case RESOURCE_CATEGORY.SOUND_VOICE:
        ResourceObject.willReleaseList.Add(resourceObject.obj);
        break;
      case RESOURCE_CATEGORY.PLAYER_ARM:
      case RESOURCE_CATEGORY.PLAYER_BDY:
      case RESOURCE_CATEGORY.PLAYER_FACE:
      case RESOURCE_CATEGORY.PLAYER_HEAD:
      case RESOURCE_CATEGORY.PLAYER_LEG:
      case RESOURCE_CATEGORY.PLAYER_WEAPON:
        GameObject gameObject = resourceObject.obj as GameObject;
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          Renderer componentInChildren = gameObject.GetComponentInChildren<Renderer>();
          ResourceObject.willReleaseList.Add((Object) componentInChildren.sharedMaterial.mainTexture);
          if (componentInChildren is MeshRenderer)
          {
            MeshFilter component = ((Component) componentInChildren).GetComponent<MeshFilter>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              ResourceObject.willReleaseList.Add((Object) component.sharedMesh);
              break;
            }
            break;
          }
          if (componentInChildren is SkinnedMeshRenderer)
          {
            SkinnedMeshRenderer skinnedMeshRenderer = componentInChildren as SkinnedMeshRenderer;
            ResourceObject.willReleaseList.Add((Object) skinnedMeshRenderer.sharedMesh);
            break;
          }
          break;
        }
        break;
    }
    if (ResourceObject.willReleaseList.Count > 0)
    {
      resourceObject.willReleaseObjs = ResourceObject.willReleaseList.ToArray();
      ResourceObject.willReleaseList.Clear();
    }
    return resourceObject;
  }

  public static void Release(ref ResourceObject resobj)
  {
    if (resobj.willReleaseObjs != null && ResourceCache.CanUseCustomUnloder())
    {
      for (int index = 0; index < resobj.willReleaseObjs.Length; ++index)
      {
        Object.DestroyImmediate(resobj.willReleaseObjs[index], true);
        resobj.willReleaseObjs[index] = (Object) null;
      }
    }
    resobj.Reset();
    rymTPool<ResourceObject>.Release(ref resobj);
  }

  public int refCount
  {
    get => this._refCount;
    set => this._refCount = value;
  }

  public ResourceObject()
  {
    this._refCount = 0;
    this.package = (PackageObject) null;
    this.obj = (Object) null;
    this.name = (string) null;
    this.willReleaseObjs = (Object[]) null;
  }

  public void Reset()
  {
    this._refCount = 0;
    this.package = (PackageObject) null;
    this.obj = (Object) null;
    this.name = (string) null;
    this.willReleaseObjs = (Object[]) null;
  }

  private class Pool_ResourceObject : rymTPool<ResourceObject>
  {
  }
}
