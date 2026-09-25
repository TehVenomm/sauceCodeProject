// Decompiled with JetBrains decompiler
// Type: RotateObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RotateObject : MonoBehaviour
{
  [SerializeField]
  private Vector3 rotateSpeed = Vector3.zero;
  private Transform _transform;

  private void Awake() => this._transform = ((Component) this).transform;

  private void Update()
  {
    if (Object.op_Equality((Object) this._transform, (Object) null))
      return;
    this._transform.Rotate(Vector3.op_Multiply(this.rotateSpeed, Time.deltaTime));
  }
}
