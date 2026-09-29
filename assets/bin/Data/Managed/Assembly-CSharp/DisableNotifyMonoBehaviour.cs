// Decompiled with JetBrains decompiler
// Type: DisableNotifyMonoBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class DisableNotifyMonoBehaviour : MonoBehaviour
{
  public Transform _transform { get; private set; }

  public DisableNotifyMonoBehaviour notifyMaster { get; private set; }

  public List<DisableNotifyMonoBehaviour> notifyServants { get; private set; }

  protected virtual void Awake() => this._transform = ((Component) this).transform;

  protected virtual void OnDisable()
  {
    if (this.notifyServants != null)
    {
      this.notifyServants.ForEach((Action<DisableNotifyMonoBehaviour>) (o => o.OnDisableMaster()));
      this.notifyServants.Clear();
      this.notifyServants = (List<DisableNotifyMonoBehaviour>) null;
    }
    this.ResetNotifyMaster();
  }

  private void OnApplicationQuit()
  {
    this.notifyMaster = (DisableNotifyMonoBehaviour) null;
    if (this.notifyServants == null)
      return;
    this.notifyServants.Clear();
    this.notifyServants = (List<DisableNotifyMonoBehaviour>) null;
  }

  public virtual void SetNotifyMaster(DisableNotifyMonoBehaviour master)
  {
    this.ResetNotifyMaster();
    this.notifyMaster = master;
    if (master.notifyServants == null)
      master.notifyServants = new List<DisableNotifyMonoBehaviour>();
    master.notifyServants.Add(this);
    master.OnAttachServant(this);
  }

  public void ResetNotifyMaster()
  {
    if (!Object.op_Inequality((Object) this.notifyMaster, (Object) null))
      return;
    if (this.notifyMaster.notifyServants != null)
      this.notifyMaster.notifyServants.Remove(this);
    this.notifyMaster.OnDetachServant(this);
    this.notifyMaster = (DisableNotifyMonoBehaviour) null;
  }

  protected virtual void OnDisableMaster()
  {
  }

  protected virtual void OnAttachServant(DisableNotifyMonoBehaviour servant)
  {
  }

  protected virtual void OnDetachServant(DisableNotifyMonoBehaviour servant)
  {
  }
}
