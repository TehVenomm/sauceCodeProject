// Decompiled with JetBrains decompiler
// Type: AutoBrain
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AutoBrain : Brain
{
  public Self self;
  public float choiceGoalSpan = 3f;
  public SkillController skillCtr;

  protected override void Awake()
  {
    base.Awake();
    this.self = this.owner as Self;
  }

  protected override void OnInitialize()
  {
    base.OnInitialize();
    this.fsm = new StateMachine((Brain) this);
    this.fsm.SetCurrentState(STATE_TYPE.BATTLE_START);
    this.think = Goal.Alloc<Goal_Think>();
    this.think.SetChoiceGoalSpanTimer(this.choiceGoalSpan);
    this.dangerRader = DangerRader.Create((Brain) this, 10f);
    this.skillCtr = new SkillController((Brain) this);
    this.targetUpdateSpanTimer = new SpanTimer(1f);
  }

  public int CanActiSkill()
  {
    this.moveCtrl.ChangeStopRange(5f);
    if (!this.targetCtrl.IsArrivalAttackPosition())
      return 0;
    return Object.op_Inequality((Object) this.self.targetingPoint, (Object) null) && Object.op_Inequality((Object) this.self.targetingPoint.owner, (Object) null) ? 1 : 2;
  }

  public override List<StageObject> GetTargetObjectList()
  {
    return !MonoBehaviourSingleton<StageObjectManager>.IsValid() ? new List<StageObject>() : MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
  }
}
