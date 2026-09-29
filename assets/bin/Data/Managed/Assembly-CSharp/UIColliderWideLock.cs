// Decompiled with JetBrains decompiler
// Type: UIColliderWideLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIColliderWideLock : MonoBehaviour
{
  private BoxCollider attach_collider;
  private float sizex;

  private void Awake()
  {
    this.attach_collider = ((Component) this).GetComponent<BoxCollider>();
    if (Object.op_Inequality((Object) this.attach_collider, (Object) null))
      this.sizex = this.attach_collider.size.x;
    else
      Object.Destroy((Object) this);
  }

  private void LateUpdate()
  {
    if ((double) this.sizex == (double) this.attach_collider.size.x)
      return;
    Vector3 size = this.attach_collider.size;
    size.x = this.sizex;
    this.attach_collider.size = size;
  }
}
