// Decompiled with JetBrains decompiler
// Type: SubstituteEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SubstituteEffect
{
  private int index;
  private Player owner;
  private SubstituteEffect lerpTarget;
  private Transform effectTransform;
  private InGameSettingsManager.BuffParamInfo info;

  public bool IsEnable() => Object.op_Inequality((Object) this.effectTransform, (Object) null);

  public Transform GetEffectTransform() => this.effectTransform;

  public void Initialize(
    Transform parentTrans,
    int i,
    Player p,
    SubstituteEffect se,
    InGameSettingsManager.BuffParamInfo bpi)
  {
    this.index = i;
    this.owner = p;
    this.lerpTarget = se;
    this.info = bpi;
    this.Create(parentTrans);
  }

  public void Create(Transform parentTrans)
  {
    if (Object.op_Inequality((Object) this.effectTransform, (Object) null))
      return;
    this.effectTransform = EffectManager.GetEffect("ef_btl_sk_magi_shikigami_01_02", parentTrans);
    this.effectTransform.position = this.GetTargetPosition(false);
  }

  public void End()
  {
    if (Object.op_Equality((Object) this.effectTransform, (Object) null))
      return;
    EffectManager.ReleaseEffect(ref this.effectTransform);
  }

  public void Update(bool isLerp = true)
  {
    if (Object.op_Equality((Object) this.effectTransform, (Object) null))
      return;
    this.effectTransform.position = this.GetTargetPosition(isLerp);
  }

  private Vector3 GetTargetPosition(bool isLerp)
  {
    if (this.info == null)
      return Vector3.zero;
    Vector3 vector3 = this.lerpTarget != null ? Vector3.op_Subtraction(this.lerpTarget.effectTransform.position, Vector3.op_Multiply(this.owner._forward, this.info.substituteOffset2)) : Vector3.op_Subtraction(this.owner._transform.position, Vector3.op_Multiply(this.owner._forward, this.info.substituteOffset1));
    vector3.y = this.info.substituteHeight;
    return isLerp ? Vector3.Lerp(this.effectTransform.position, vector3, this.info.substituteLerpSpeed * Time.deltaTime) : vector3;
  }
}
