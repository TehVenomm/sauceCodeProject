// Decompiled with JetBrains decompiler
// Type: ResourceLink
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ResourceLink : MonoBehaviour
{
  public Object[] objects;

  public T Get<T>(string name) where T : Object
  {
    if (this.objects != null && !string.IsNullOrEmpty(name))
    {
      int index = 0;
      for (int length = this.objects.Length; index < length; ++index)
      {
        Object @object = this.objects[index];
        if (Object.op_Inequality(@object, (Object) null) && @object is T && @object.name == name)
          return @object as T;
      }
    }
    return default (T);
  }

  public T GetFirstObject<T>(string filter) where T : Object
  {
    if (this.objects == null)
      return default (T);
    for (int index = 0; index < this.objects.Length; ++index)
    {
      Object firstObject = this.objects[index];
      if (Object.op_Inequality(firstObject, (Object) null) && (string.IsNullOrEmpty(filter) || firstObject.name.Contains(filter)) && firstObject is T)
        return firstObject as T;
    }
    return default (T);
  }
}
