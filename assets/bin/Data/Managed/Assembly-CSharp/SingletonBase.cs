// Decompiled with JetBrains decompiler
// Type: SingletonBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public abstract class SingletonBase
{
  public static List<object> instanceList;

  public static void AddInstance(object obj)
  {
    if (SingletonBase.instanceList == null)
      SingletonBase.instanceList = new List<object>();
    SingletonBase.instanceList.Add(obj);
  }

  public static void RemoveAllInstance()
  {
    if (SingletonBase.instanceList == null)
      return;
    while (SingletonBase.instanceList.Count > 0)
    {
      if (SingletonBase.instanceList[0] is SingletonBase)
        (SingletonBase.instanceList[0] as SingletonBase).Remove();
      else
        SingletonBase.instanceList.RemoveAt(0);
    }
    SingletonBase.instanceList = (List<object>) null;
  }

  public virtual void Remove()
  {
  }
}
