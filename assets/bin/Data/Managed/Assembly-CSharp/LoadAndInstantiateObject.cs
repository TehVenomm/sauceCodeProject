// Decompiled with JetBrains decompiler
// Type: LoadAndInstantiateObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class LoadAndInstantiateObject : LoadObject
{
  private GameObject instantiatedObject;

  public LoadAndInstantiateObject(
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string resource_name)
  {
    this.QuickLoad(mono_behaviour, category, resource_name, false);
    if (!MonoBehaviourSingleton<InstantiateManager>.IsValid() || this.isLoading || !Object.op_Inequality(this.loadedObject, (Object) null))
      return;
    this.isLoading = true;
    InstantiateManager.Request((Object) this.resLoad, this.loadedObject, new Action<InstantiateManager.InstantiateData>(this.OnInstantiate), true);
  }

  protected override void OnLoadComplate(ResourceManager.LoadRequest request, ResourceObject[] objs)
  {
    if (!MonoBehaviourSingleton<InstantiateManager>.IsValid())
      base.OnLoadComplate(request, objs);
    else if (objs != null && objs.Length == 1 && objs[0] != null)
    {
      this.loadedObject = objs[0].obj;
      this.loadedObjects = objs;
      this.resLoad.SetReference(this.loadedObjects);
      InstantiateManager.Request((Object) this.resLoad, objs[0].obj, new Action<InstantiateManager.InstantiateData>(this.OnInstantiate), true);
    }
    else
    {
      this.isLoading = false;
      Log.Warning("LoadAndInstantiateObject : not support.");
    }
  }

  private void OnInstantiate(InstantiateManager.InstantiateData data)
  {
    this.isLoading = false;
    this.instantiatedObject = data.instantiatedObject as GameObject;
  }

  public override Transform Realizes(Transform parent = null, int layer = -1)
  {
    return Object.op_Equality((Object) this.instantiatedObject, (Object) null) ? base.Realizes(parent, layer) : InstantiateManager.Realizes(ref this.instantiatedObject, parent, layer);
  }

  public override GameObject PopInstantiatedGameObject()
  {
    GameObject instantiatedObject = this.instantiatedObject;
    this.instantiatedObject = (GameObject) null;
    return instantiatedObject;
  }

  public override bool HasInstantiatedGameObject()
  {
    return Object.op_Inequality((Object) this.instantiatedObject, (Object) null);
  }
}
