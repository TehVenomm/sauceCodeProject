// Decompiled with JetBrains decompiler
// Type: State_NonActive
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class State_NonActive : State
{
  public override void Enter(StateMachine fsm, Brain brain)
  {
    fsm.subFsm.ChangeState(STATE_TYPE.EXPLORE);
  }

  public override void Process(StateMachine fsm, Brain brain)
  {
    StageObject ofScountingParam = brain.targetCtrl.GetTargetObjectOfScountingParam();
    if (Object.op_Inequality((Object) ofScountingParam, (Object) null))
    {
      if (brain.opponentMem != null)
        brain.opponentMem.AddHate(ofScountingParam, 100, Hate.TYPE.Damage);
      this.ChangeActiveState(fsm, brain);
    }
    else
      fsm.processSpan.SetTempSpan(0.1f);
  }

  public override void Exit(StateMachine fsm, Brain brain)
  {
    fsm.subFsm.ChangeState(STATE_TYPE.NONE);
  }

  private void ChangeActiveState(StateMachine fsm, Brain brain)
  {
    if (Object.op_Inequality((Object) brain.owner, (Object) null))
      brain.owner.SafeActIdle();
    fsm.ChangeState(STATE_TYPE.ACTIVE);
  }

  public override void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
  {
    if (ev != BRAIN_EVENT.ATTACKED_HIT)
      return;
    this.ChangeActiveState(fsm, brain);
    Vector3 position = brain.owner._position;
    if (brain.param.scoutParam == null)
      return;
    List<StageObject> allyObjectList = brain.GetAllyObjectList();
    for (int index = 0; index < allyObjectList.Count; ++index)
    {
      if (!Object.op_Equality((Object) allyObjectList[index].controller, (Object) null))
      {
        Brain brain1 = allyObjectList[index].controller.brain;
        if (!Object.op_Equality((Object) brain1, (Object) null))
        {
          Vector3 vector3 = Vector3.op_Subtraction(allyObjectList[index]._position, position);
          if ((double) ((Vector3) ref vector3).sqrMagnitude < (double) brain.param.scoutParam.scoutingAudibilitySqr)
          {
            if (Object.op_Inequality((Object) brain1.owner, (Object) null))
              brain1.owner.SafeActIdle();
            if (brain1.fsm != null)
              brain1.fsm.ChangeState(STATE_TYPE.ACTIVE);
          }
        }
      }
    }
  }
}
