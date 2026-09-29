// Decompiled with JetBrains decompiler
// Type: GachaDecoPosLink
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class GachaDecoPosLink : MonoBehaviour
{
  public Transform target;
  public float limitX;
  private Transform _transform;

  private void Awake()
  {
    this._transform = ((Component) this).transform;
    if (!Object.op_Equality((Object) this.target, (Object) null))
      return;
    ((Behaviour) this).enabled = false;
  }

  private void LateUpdate()
  {
    this._transform.position = this.target.position;
    Vector3 localPosition = this._transform.localPosition;
    if ((double) localPosition.x <= (double) this.limitX)
      return;
    localPosition.x = this.limitX;
    this._transform.localPosition = localPosition;
  }
}
