// Decompiled with JetBrains decompiler
// Type: ColliderWeightCtl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ColliderWeightCtl : MonoBehaviour
{
  public Collider collider;
  public ColliderWeightCtl.COLLIDER_TYPE colliderType;
  public int layerIndex = 1;
  public Vector3 startCenter;
  public Vector3 endCenter;
  public float startRadius;
  public float endRadius;
  public float startHeight;
  public float endHeight;
  public Vector3 startSize;
  public Vector3 endSize;
  private float currentWeight;
  private Vector3 diffCenter;
  private float diffRadius;
  private float diffHeight;
  private Vector3 diffSize;
  private Animator animator;

  private void Awake()
  {
    this.diffCenter = Vector3.op_Subtraction(this.endCenter, this.startCenter);
    this.diffRadius = this.endRadius - this.startRadius;
    this.diffHeight = this.endHeight - this.startHeight;
    this.diffSize = Vector3.op_Subtraction(this.endSize, this.startSize);
    this.currentWeight = 0.0f;
  }

  public void SetAnimator(Animator _animator)
  {
    this.animator = _animator;
    this.currentWeight = this.animator.GetLayerWeight(this.layerIndex);
    if (!Object.op_Inequality((Object) this.collider, (Object) null))
      return;
    this.calc();
  }

  private float GetAnimatorLayerWeight()
  {
    return Object.op_Equality((Object) this.animator, (Object) null) ? 0.0f : this.animator.GetLayerWeight(this.layerIndex);
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.collider, (Object) null) || Object.op_Equality((Object) this.animator, (Object) null))
      return;
    float layerWeight = this.animator.GetLayerWeight(this.layerIndex);
    if ((double) this.currentWeight == (double) layerWeight)
      return;
    this.currentWeight = layerWeight;
    this.calc();
  }

  private void calc()
  {
    Vector3 vector3_1 = Vector3.op_Addition(Vector3.op_Multiply(this.diffCenter, this.currentWeight), this.startCenter);
    switch (this.colliderType)
    {
      case ColliderWeightCtl.COLLIDER_TYPE.CAPSULE:
        float num1 = this.diffRadius * this.currentWeight + this.startRadius;
        float num2 = this.diffHeight * this.currentWeight + this.startHeight;
        CapsuleCollider collider1 = this.collider as CapsuleCollider;
        collider1.center = vector3_1;
        collider1.radius = num1;
        collider1.height = num2;
        break;
      case ColliderWeightCtl.COLLIDER_TYPE.SPHERE:
        float num3 = this.diffRadius * this.currentWeight + this.startRadius;
        SphereCollider collider2 = this.collider as SphereCollider;
        collider2.center = vector3_1;
        collider2.radius = num3;
        break;
      case ColliderWeightCtl.COLLIDER_TYPE.BOX:
        Vector3 vector3_2 = Vector3.op_Addition(Vector3.op_Multiply(this.diffSize, this.currentWeight), this.startSize);
        BoxCollider collider3 = this.collider as BoxCollider;
        collider3.center = vector3_1;
        collider3.size = vector3_2;
        break;
    }
  }

  public enum COLLIDER_TYPE
  {
    CAPSULE,
    SPHERE,
    BOX,
  }
}
