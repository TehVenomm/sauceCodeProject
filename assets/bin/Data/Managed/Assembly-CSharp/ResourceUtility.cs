// Decompiled with JetBrains decompiler
// Type: ResourceUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public static class ResourceUtility
{
  public static Shader FindShader(string shader_name)
  {
    Shader shader = Shader.Find(shader_name);
    if (Object.op_Equality((Object) shader, (Object) null) && MonoBehaviourSingleton<ResourceManager>.IsValid())
      shader = MonoBehaviourSingleton<ResourceManager>.I.cache.shaderCaches.Get(shader_name);
    return shader;
  }

  public static T Instantiate<T>(T obj) where T : Object
  {
    return (object) Object.Instantiate<T>(obj) as T;
  }

  public static Transform Realizes(Object obj, int layer = -1)
  {
    GameObject gameObject1 = obj as GameObject;
    if (Object.op_Equality((Object) gameObject1, (Object) null))
      return (Transform) null;
    GameObject gameObject2 = ResourceUtility.Instantiate<GameObject>(gameObject1);
    ((Object) gameObject2).name = ResourceName.Normalize(((Object) gameObject2).name).Replace("(Clone)", "");
    Transform transform = gameObject2.transform;
    if (layer != -1)
      Utility.SetLayerWithChildren(transform, layer);
    return transform;
  }

  public static Transform Realizes(Object obj, Transform parent, int layer = -1)
  {
    if (Object.op_Equality(obj, (Object) null))
    {
      Log.Error("ResourceUtility.Realizes : obj == null");
      return (Transform) null;
    }
    Transform child = ResourceUtility.Realizes(obj, layer);
    if (Object.op_Equality((Object) child, (Object) null))
      return (Transform) null;
    Utility.Attach(parent, child);
    return child;
  }
}
