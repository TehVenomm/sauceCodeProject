// Decompiled with JetBrains decompiler
// Type: FieldGimmickCandyWoodObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FieldGimmickCandyWoodObject : FieldGimmickObject
{
  private const string ANIM_STATE_LOW = "CMN_candy01_low";
  private const string ANIM_STATE_MIDDLE = "CMN_candy01_middle";
  private const string ANIM_STATE_HIGH = "CMN_candy01_high";
  private readonly int LOW_ANIM_HASH = Animator.StringToHash("CMN_candy01_low");
  private readonly int MIDDLE_ANIM_HASH = Animator.StringToHash("CMN_candy01_middle");
  private readonly int HIGH_ANIM_HASH = Animator.StringToHash("CMN_candy01_high");
  private FieldGimmickCandyWoodObject.STATE state;
  public Transform transSwitching;
  private Animator anim;

  public override string GetObjectName() => "CandyWood";

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.transSwitching = Utility.FindChild(this.modelTrans, "switching");
    this.anim = ((Component) this.transSwitching).GetComponent<Animator>();
    this.SyncWoodStateAndView();
  }

  public override void OnNotify(object value)
  {
    if (!(value is GrowthGatherPointObject.GrowthInfo growthInfo))
      return;
    FieldGimmickCandyWoodObject.STATE state = growthInfo.current != 0 ? (growthInfo.current != growthInfo.max ? FieldGimmickCandyWoodObject.STATE.Middle : FieldGimmickCandyWoodObject.STATE.High) : FieldGimmickCandyWoodObject.STATE.Low;
    if (state == this.state)
      return;
    this.state = state;
    this.SyncWoodStateAndView();
  }

  private void SyncWoodStateAndView()
  {
    int num = 0;
    switch (this.state)
    {
      case FieldGimmickCandyWoodObject.STATE.None:
      case FieldGimmickCandyWoodObject.STATE.Low:
        num = this.LOW_ANIM_HASH;
        break;
      case FieldGimmickCandyWoodObject.STATE.Middle:
        num = this.MIDDLE_ANIM_HASH;
        break;
      case FieldGimmickCandyWoodObject.STATE.High:
        num = this.HIGH_ANIM_HASH;
        break;
    }
    if (!this.anim.HasState(0, num))
      return;
    this.anim.Play(num);
  }

  public enum STATE
  {
    None,
    Low,
    Middle,
    High,
  }
}
