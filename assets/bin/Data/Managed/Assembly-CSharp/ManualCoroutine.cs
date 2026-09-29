// Decompiled with JetBrains decompiler
// Type: ManualCoroutine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class ManualCoroutine
{
  public int id;
  public object data;
  private MonoBehaviour mono;
  private IEnumerator instance;
  private IEnumerator updateInstance;

  public static ManualCoroutine current { get; private set; }

  public bool active { get; set; }

  public bool isEnabled => this.instance != null;

  public System.Action callback { get; set; }

  public ManualCoroutine() => this.active = true;

  public ManualCoroutine(
    int _id,
    MonoBehaviour _mono,
    IEnumerator co,
    bool _active = true,
    System.Action _callback = null,
    object _data = null)
  {
    this.id = _id;
    this.Set(_mono, co);
    this.active = _active;
    this.callback = _callback;
    this.data = _data;
  }

  public void Set(MonoBehaviour _mono, IEnumerator co)
  {
    this.Clear();
    this.mono = _mono;
    this.instance = co;
    this.updateInstance = this.DoManualCoroutineUpdate();
    this.mono.StartCoroutine(this.updateInstance);
  }

  public void Clear()
  {
    if (Object.op_Inequality((Object) this.mono, (Object) null) && this.updateInstance != null)
      this.mono.StopCoroutine(this.updateInstance);
    this.instance = (IEnumerator) null;
    this.updateInstance = (IEnumerator) null;
    this.mono = (MonoBehaviour) null;
    this.active = true;
  }

  private IEnumerator DoManualCoroutineUpdate()
  {
    while (true)
    {
      while (this.instance == null || !this.active || !((Behaviour) this.mono).enabled)
        yield return (object) null;
      ManualCoroutine.current = this;
      if (this.instance.MoveNext())
      {
        ManualCoroutine.current = (ManualCoroutine) null;
        yield return this.instance.Current;
      }
      else
        break;
    }
    if (this.callback != null)
      this.callback();
    ManualCoroutine.current = (ManualCoroutine) null;
    this.Clear();
  }
}
