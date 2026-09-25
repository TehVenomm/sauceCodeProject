// Decompiled with JetBrains decompiler
// Type: Goal_Guard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Goal_Guard : Goal
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.GUARD;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    brain.weaponCtrl.GuardOn();
  }

  protected override Goal.STATUS Process(Brain brain) => this.status;

  protected override void Terminate(Brain brain) => brain.weaponCtrl.GuardOff();

  public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
  {
    base.HandleEvent(brain, ev, param);
    if (ev != BRAIN_EVENT.END_ACTION || (int) param != 19)
      return;
    this.SetStatus(Goal.STATUS.COMPLETED);
  }
}
