// Decompiled with JetBrains decompiler
// Type: MonoBehaviourSingleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MonoBehaviourSingleton<T> : DisableNotifyMonoBehaviour where T : DisableNotifyMonoBehaviour
{
  private static T instance;

  public static T I
  {
    get
    {
      if (Object.op_Equality((Object) (object) MonoBehaviourSingleton<T>.instance, (Object) null))
      {
        MonoBehaviourSingleton<T>.instance = (T) Object.FindObjectOfType(typeof (T));
        if (Object.op_Equality((Object) (object) MonoBehaviourSingleton<T>.instance, (Object) null))
          Log.Error(LOG.SYSTEM, typeof (T).ToString() + " is nothing");
      }
      return MonoBehaviourSingleton<T>.instance;
    }
  }

  private void OnDestroy()
  {
    if (!AppMain.isApplicationQuit)
      this._OnDestroy();
    if (!Object.op_Equality((Object) (object) MonoBehaviourSingleton<T>.instance, (Object) this))
      return;
    this.OnDestroySingleton();
    MonoBehaviourSingleton<T>.instance = default (T);
  }

  protected virtual void _OnDestroy()
  {
  }

  protected virtual void OnDestroySingleton()
  {
  }

  protected override void Awake()
  {
    base.Awake();
    this.CheckInstance();
  }

  protected bool CheckInstance()
  {
    if (Object.op_Equality((Object) this, (Object) (object) MonoBehaviourSingleton<T>.I))
      return true;
    Object.Destroy((Object) this);
    return false;
  }

  protected void SelfInstance() => MonoBehaviourSingleton<T>.instance = this as T;

  protected void RemoveInstance()
  {
    if (!Object.op_Equality((Object) (object) MonoBehaviourSingleton<T>.instance, (Object) (object) (this as T)))
      return;
    MonoBehaviourSingleton<T>.instance = default (T);
  }

  public static bool IsValid()
  {
    return Object.op_Inequality((Object) (object) MonoBehaviourSingleton<T>.instance, (Object) null);
  }
}
