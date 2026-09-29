// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Singleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace AlmostEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
  public static T m_Instance;
  public bool m_DontDestroyOnLoad = true;

  private void Awake()
  {
    if (Object.op_Equality((Object) (object) Singleton<T>.m_Instance, (Object) null))
    {
      Singleton<T>.m_Instance = this as T;
      if (Object.op_Equality((Object) ((Component) this).transform.parent, (Object) null) && this.m_DontDestroyOnLoad)
        Object.DontDestroyOnLoad((Object) ((Component) this).gameObject);
      this.OnSingletonAwake();
    }
    else
    {
      if (!Object.op_Inequality((Object) this, (Object) (object) Singleton<T>.m_Instance))
        return;
      Object.DestroyImmediate((Object) ((Component) this).gameObject);
    }
  }

  private void OnDestroy()
  {
    if (!Object.op_Equality((Object) (object) Singleton<T>.m_Instance, (Object) this))
      return;
    Singleton<T>.m_Instance = default (T);
  }

  protected virtual void OnSingletonAwake()
  {
  }
}
