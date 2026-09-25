// Decompiled with JetBrains decompiler
// Type: Goal_Attack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Goal_Attack : Goal
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.ATTACK;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    brain.weaponCtrl.AttackOn();
  }

  protected override Goal.STATUS Process(Brain brain) => this.status;

  protected override void Terminate(Brain brain) => brain.weaponCtrl.AttackOff();

  public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
  {
    base.HandleEvent(brain, ev, param);
    switch (ev)
    {
      case BRAIN_EVENT.PLAY_MOTION:
        int num = (int) param;
        if (num < 15 || num > 114)
          break;
        this.SetStatus(Goal.STATUS.COMPLETED);
        break;
      case BRAIN_EVENT.END_ACTION:
        if ((int) param != 6)
          break;
        this.SetStatus(Goal.STATUS.COMPLETED);
        break;
    }
  }
}
