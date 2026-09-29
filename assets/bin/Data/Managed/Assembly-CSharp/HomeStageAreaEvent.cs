// Decompiled with JetBrains decompiler
// Type: HomeStageAreaEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class HomeStageAreaEvent : HomeStageEventBase
{
  public const int EVENT_LAYER = 2;
  public const int EVENT_LAYER_MASK = 4;
  public float noticeRange = 2f;
  public float noticeViewHeight = 3f;
  public string noticeButtonName;

  public Transform _transform { get; private set; }

  public SphereCollider _collider { get; private set; }

  public float defaultRadius { get; private set; }

  protected override int GetLayer() => 2;

  protected override void Awake()
  {
    base.Awake();
    this._transform = ((Component) this).transform;
    this._collider = ((Component) this).GetComponent<SphereCollider>();
    if (Object.op_Equality((Object) this._collider, (Object) null))
      return;
    this.defaultRadius = this._collider.radius;
    this._collider.radius += this.noticeRange;
  }
}
