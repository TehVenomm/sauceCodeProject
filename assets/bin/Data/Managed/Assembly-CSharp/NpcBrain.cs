// Decompiled with JetBrains decompiler
// Type: NpcBrain
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class NpcBrain : Brain
{
  protected Player player;
  public float choiceGoalSpan = 3f;

  protected override void Awake()
  {
    base.Awake();
    this.player = this.owner as Player;
  }

  protected override void OnInitialize()
  {
    base.OnInitialize();
    this.fsm = new StateMachine((Brain) this);
    this.fsm.SetCurrentState(STATE_TYPE.BATTLE_START);
    this.think = Goal.Alloc<Goal_Think>();
    this.think.SetChoiceGoalSpanTimer(this.choiceGoalSpan);
    this.dangerRader = DangerRader.Create((Brain) this, 10f);
  }

  public override List<StageObject> GetTargetObjectList()
  {
    return !MonoBehaviourSingleton<StageObjectManager>.IsValid() ? new List<StageObject>() : MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
  }
}
