// Decompiled with JetBrains decompiler
// Type: ControlObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ControlObject : DisableNotifyMonoBehaviour
{
  public virtual Vector3 _position
  {
    get => this._transform.position;
    set => this._transform.position = value;
  }

  public virtual Quaternion _rotation
  {
    get => this._transform.rotation;
    set => this._transform.rotation = value;
  }

  public virtual Vector3 _forward
  {
    get => this._transform.forward;
    set => this._transform.forward = value;
  }

  public virtual Vector3 _right
  {
    get => this._transform.right;
    set => this._transform.right = value;
  }

  public virtual Vector3 _up
  {
    get => this._transform.up;
    set => this._transform.up = value;
  }

  public virtual void _LookAt(Vector3 pos) => this._transform.LookAt(pos);
}
