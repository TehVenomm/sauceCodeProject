// Decompiled with JetBrains decompiler
// Type: Goal_ActSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Goal_ActSkill : Goal
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.ACT_SKILL;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    (brain as AutoBrain).skillCtr.IsAct = true;
  }

  protected override Goal.STATUS Process(Brain brain) => this.status;

  protected override void Terminate(Brain brain) => (brain as AutoBrain).skillCtr.IsAct = false;

  public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
  {
    base.HandleEvent(brain, ev, param);
    if (ev != BRAIN_EVENT.END_ACTION || (int) param != 22)
      return;
    this.SetStatus(Goal.STATUS.COMPLETED);
  }
}
