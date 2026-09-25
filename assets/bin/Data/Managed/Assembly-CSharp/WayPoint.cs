// Decompiled with JetBrains decompiler
// Type: WayPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class WayPoint : MonoBehaviour
{
  public WayPoint[] links;
  public string waitAnimStateName;

  public Vector3 GetPosInCollider()
  {
    Transform transform = ((Component) this).transform;
    Collider component = ((Component) this).GetComponent<Collider>();
    switch (component)
    {
      case SphereCollider _:
        return Vector3.op_Addition(transform.position, Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.Euler(0.0f, (float) Random.Range(0, 360), 0.0f), Vector3.forward), Random.Range(0.0f, (component as SphereCollider).radius)));
      case BoxCollider _:
        Vector3 size = (component as BoxCollider).size;
        size.x = Utility.SymmetryRandom(size.x * 0.5f);
        size.y = 0.0f;
        size.z = Utility.SymmetryRandom(size.z * 0.5f);
        return transform.TransformPoint(size);
      default:
        return transform.position;
    }
  }
}
