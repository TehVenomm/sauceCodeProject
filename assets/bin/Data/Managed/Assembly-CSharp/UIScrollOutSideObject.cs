// Decompiled with JetBrains decompiler
// Type: UIScrollOutSideObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIScrollOutSideObject : MonoBehaviour
{
  private Transform target;
  private Transform _transform;

  public bool enaleUpdate { get; private set; }

  public void SetActive(bool is_active)
  {
    this.enaleUpdate = is_active;
    ((Component) this).gameObject.SetActive(is_active);
  }

  public void SetTargetTransform(Transform _t)
  {
    this.target = _t;
    this.enaleUpdate = true;
  }

  public void OnDestroy()
  {
    this._transform = (Transform) null;
    this.target = (Transform) null;
  }

  private void Start() => this._transform = ((Component) this).transform;

  private void LateUpdate()
  {
    if (!this.enaleUpdate || !Object.op_Inequality((Object) this._transform, (Object) null) || !Object.op_Inequality((Object) this.target, (Object) null))
      return;
    ((Component) this).transform.position = this.target.position;
  }
}
