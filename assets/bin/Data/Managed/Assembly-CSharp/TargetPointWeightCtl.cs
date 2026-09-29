// Decompiled with JetBrains decompiler
// Type: TargetPointWeightCtl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TargetPointWeightCtl : MonoBehaviour
{
  public TargetPoint targetPoint;
  public int layerIndex = 1;
  public Vector3 startOffset;
  public Vector3 endOffset;
  public float startMarkerZShift;
  public float endMarkerZShift;
  public Vector3 startBleedOffsetPos = Vector3.zero;
  public Vector3 endBleedOffsetPos = Vector3.zero;
  public Vector3 startBleedOffsetRot = Vector3.zero;
  public Vector3 endBleedOffsetRot = Vector3.zero;
  public float startAimMarkerPointRate = 1f;
  public float endAimMarkerPointRate = 1f;
  public float startWeight;
  public float endWeight;
  public bool isOffsetChange = true;
  public bool isMarkerZShiftChange = true;
  public bool isBleedOffsetPosChange = true;
  public bool isBleedOffsetRotChange = true;
  public bool isAimMarkerPointRateChange = true;
  public bool isWeightChange = true;
  public bool isArrowPosUpdate;
  public bool isArrowRotUpdate;
  public bool isArrowOriginalScele;
  private float currentWeight;
  private Vector3 diffOffset;
  private float diffMarkerZShift;
  private Vector3 diffBleedOffsetPos = Vector3.zero;
  private Vector3 diffBleedOffsetRot = Vector3.zero;
  private float diffAimMarkerPointRate = 1f;
  private float diffWeight;
  private Animator animator;

  private void Awake()
  {
    this.diffOffset = Vector3.op_Subtraction(this.endOffset, this.startOffset);
    this.diffMarkerZShift = this.endMarkerZShift - this.startMarkerZShift;
    this.diffBleedOffsetPos = Vector3.op_Subtraction(this.endBleedOffsetPos, this.startBleedOffsetPos);
    this.diffBleedOffsetRot = Vector3.op_Subtraction(this.endBleedOffsetRot, this.startBleedOffsetRot);
    this.diffAimMarkerPointRate = this.endAimMarkerPointRate - this.startAimMarkerPointRate;
    this.diffWeight = this.endWeight - this.startWeight;
    this.currentWeight = 0.0f;
  }

  public void SetAnimator(Animator _animator)
  {
    this.animator = _animator;
    this.currentWeight = this.animator.GetLayerWeight(this.layerIndex);
    if (!Object.op_Inequality((Object) this.targetPoint, (Object) null))
      return;
    this.calc();
  }

  private float GetAnimatorLayerWeight()
  {
    return Object.op_Equality((Object) this.animator, (Object) null) ? 0.0f : this.animator.GetLayerWeight(this.layerIndex);
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.targetPoint, (Object) null) || Object.op_Equality((Object) this.animator, (Object) null))
      return;
    float layerWeight = this.animator.GetLayerWeight(this.layerIndex);
    if ((double) this.currentWeight == (double) layerWeight)
      return;
    this.currentWeight = layerWeight;
    if (this.isOffsetChange)
      this.targetPoint.offset = Vector3.op_Addition(Vector3.op_Multiply(this.diffOffset, this.currentWeight), this.startOffset);
    if (this.isMarkerZShiftChange)
      this.targetPoint.markerZShift = this.diffMarkerZShift * this.currentWeight + this.startMarkerZShift;
    if (this.isBleedOffsetPosChange)
      this.targetPoint.bleedOffsetPos = Vector3.op_Addition(Vector3.op_Multiply(this.diffBleedOffsetPos, this.currentWeight), this.startBleedOffsetPos);
    if (this.isBleedOffsetRotChange)
      this.targetPoint.bleedOffsetRot = Vector3.op_Addition(Vector3.op_Multiply(this.diffBleedOffsetRot, this.currentWeight), this.startBleedOffsetRot);
    if (this.isAimMarkerPointRateChange)
      this.targetPoint.aimMarkerPointRate = this.diffAimMarkerPointRate * this.currentWeight + this.startAimMarkerPointRate;
    if (this.isWeightChange)
      this.targetPoint.weight = this.diffWeight * this.currentWeight + this.startWeight;
    this.targetPoint.scaledCalc();
    this.calc();
  }

  private void calc()
  {
    if (this.isOffsetChange)
      this.targetPoint.offset = Vector3.op_Addition(Vector3.op_Multiply(this.diffOffset, this.currentWeight), this.startOffset);
    if (this.isMarkerZShiftChange)
      this.targetPoint.markerZShift = this.diffMarkerZShift * this.currentWeight + this.startMarkerZShift;
    if (this.isBleedOffsetPosChange)
      this.targetPoint.bleedOffsetPos = Vector3.op_Addition(Vector3.op_Multiply(this.diffBleedOffsetPos, this.currentWeight), this.startBleedOffsetPos);
    if (this.isBleedOffsetRotChange)
      this.targetPoint.bleedOffsetRot = Vector3.op_Addition(Vector3.op_Multiply(this.diffBleedOffsetRot, this.currentWeight), this.startBleedOffsetRot);
    if (this.isAimMarkerPointRateChange)
      this.targetPoint.aimMarkerPointRate = this.diffAimMarkerPointRate * this.currentWeight + this.startAimMarkerPointRate;
    if (this.isWeightChange)
      this.targetPoint.weight = this.diffWeight * this.currentWeight + this.startWeight;
    this.targetPoint.scaledCalc();
    this.targetPoint.ArrowBleedEffectUpdate(this.isArrowPosUpdate, this.isArrowRotUpdate, this.isArrowOriginalScele);
  }
}
