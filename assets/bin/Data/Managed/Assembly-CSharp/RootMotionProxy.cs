// Decompiled with JetBrains decompiler
// Type: RootMotionProxy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RootMotionProxy : MonoBehaviour
{
  private Animator animator;
  private Transform parentTransfom;
  private Rigidbody parentRigidbody;

  private void Start()
  {
    this.animator = ((Component) this).gameObject.GetComponent<Animator>();
    this.parentTransfom = ((Component) this).transform.parent;
    if (!Object.op_Inequality((Object) this.parentTransfom, (Object) null))
      return;
    this.parentRigidbody = ((Component) this.parentTransfom).gameObject.GetComponent<Rigidbody>();
  }

  private void OnAnimatorMove()
  {
    if (Object.op_Equality((Object) this.parentTransfom, (Object) null) || Object.op_Equality((Object) this.animator, (Object) null))
      return;
    if (!this.animator.applyRootMotion)
    {
      if (!Object.op_Inequality((Object) this.parentRigidbody, (Object) null))
        return;
      this.parentRigidbody.velocity = Vector3.zero;
    }
    else if (Object.op_Inequality((Object) this.parentRigidbody, (Object) null) && !this.parentRigidbody.isKinematic)
    {
      if ((double) Time.deltaTime > 0.0)
        this.parentRigidbody.velocity = Vector3.op_Division(this.animator.deltaPosition, Time.deltaTime);
      else
        this.parentRigidbody.velocity = Vector3.zero;
    }
    else
    {
      this.parentTransfom.localPosition = Vector3.op_Addition(this.parentTransfom.localPosition, this.animator.deltaPosition);
      this.parentTransfom.localRotation = Quaternion.op_Multiply(this.parentTransfom.localRotation, this.animator.deltaRotation);
    }
  }
}
