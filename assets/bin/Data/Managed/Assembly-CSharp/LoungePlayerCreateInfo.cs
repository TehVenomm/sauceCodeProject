// Decompiled with JetBrains decompiler
// Type: LoungePlayerCreateInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class LoungePlayerCreateInfo : MonoBehaviour
{
  public Vector3 lookPoint;
  public SphereCollider createArea;

  public Vector3 GetCreatePosition()
  {
    if (Object.op_Equality((Object) this.createArea, (Object) null))
      return Vector3.zero;
    Transform transform = ((Component) this.createArea).transform;
    Quaternion quaternion = Quaternion.Euler(0.0f, (float) Random.Range(0, 360), 0.0f);
    float num = Random.Range(0.0f, this.createArea.radius);
    return Vector3.op_Addition(transform.position, Vector3.op_Multiply(Quaternion.op_Multiply(quaternion, Vector3.forward), num));
  }
}
