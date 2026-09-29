// Decompiled with JetBrains decompiler
// Type: CircleShadow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CircleShadow : MonoBehaviour
{
  private Transform _transform;
  private Transform animTransform;

  private void Awake() => this._transform = ((Component) this).transform;

  private void LateUpdate()
  {
    Vector3 position = this._transform.position;
    if (Object.op_Inequality((Object) this.animTransform, (Object) null))
      position = this.animTransform.position;
    position.y = 0.005f;
    this._transform.position = position;
  }

  public void setAnimTransform(Transform target) => this.animTransform = target;
}
