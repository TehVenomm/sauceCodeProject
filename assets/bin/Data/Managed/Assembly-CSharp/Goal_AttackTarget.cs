// Decompiled with JetBrains decompiler
// Type: Goal_AttackTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_AttackTarget : GoalComposite
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.ATTACK_TARGET;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    if (!brain.targetCtrl.IsAliveTarget())
      this.SetStatus(Goal.STATUS.COMPLETED);
    else if (!brain.targetCtrl.CanAttackTarget())
    {
      this.SetStatus(Goal.STATUS.COMPLETED);
    }
    else
    {
      Player owner = brain.owner as Player;
      if (Object.op_Inequality((Object) owner, (Object) null) && owner.isLongAttackMode)
      {
        Vector3 targetPosition = brain.targetCtrl.GetTargetPosition();
        float distance = brain.targetCtrl.GetDistance();
        if (!brain.moveCtrl.CanSeekToOpponent(targetPosition, distance))
        {
          float len = 3f;
          PLACE place = Utility.Coin() ? PLACE.RIGHT : PLACE.LEFT;
          RaycastHit seekHit = brain.moveCtrl.seekHit;
          Vector3 position = ((RaycastHit) ref seekHit).transform.position;
          this.AddSubGoal<Goal_MoveToAround>().SetParam(place, position, len);
          return;
        }
      }
      bool flag = false;
      if (brain.targetCtrl.IsAttackableTarget())
      {
        brain.canCheckAvoidAttack = true;
        brain.weaponCtrl.AvoidAttackOff();
        if (brain.weaponCtrl.IsCombo())
          flag = false;
        else if ((double) brain.weaponCtrl.GetSpecialReach() > 0.0 && Utility.Dice100(30))
          flag = !owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL);
        else if (owner.CheckAttackMode(Player.ATTACK_MODE.TWO_HAND_SWORD) && !owner.CheckSpAttackType(SP_ATTACK_TYPE.SOUL) && owner.playerParameter.twoHandSwordActionInfo.avoidAttackEnable && Utility.Dice100(40))
          brain.weaponCtrl.AvoidAttackOn();
      }
      else if (brain.targetCtrl.IsSpecialAttackableTarget())
        flag = true;
      else if (brain.targetCtrl.IsAvoidAttackableTarget())
      {
        brain.canAvoidAttack = false;
        brain.weaponCtrl.AvoidAttackOn();
      }
      if (Object.op_Inequality((Object) owner, (Object) null))
      {
        if (owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT))
          flag = owner.pairSwordsCtrl.IsAbleToAlterSpAction();
        else if (owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
          flag = owner.IsSpActionGaugeFullCharged();
        else if (owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
          flag = true;
      }
      if (flag)
        this.AddSubGoal<Goal_SpecialAttack>();
      else
        this.AddSubGoal<Goal_Attack>();
    }
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    this.SetStatus(this.UpdateSubGoals(brain));
    if (!brain.targetCtrl.IsAliveTarget())
      this.SetStatus(Goal.STATUS.COMPLETED);
    if (!brain.targetCtrl.CanAttackTarget())
      this.SetStatus(Goal.STATUS.COMPLETED);
    return this.status;
  }

  protected override void Terminate(Brain brain)
  {
  }
}
