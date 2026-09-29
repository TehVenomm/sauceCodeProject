// Decompiled with JetBrains decompiler
// Type: TargetController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TargetController
{
  private Brain brain;
  private StageObject allyTarget;

  public TargetController(Brain brain) => this.brain = brain;

  public StageObject GetAllyTarget() => this.allyTarget;

  public void SetAllyTarget(StageObject ally) => this.allyTarget = ally;

  public bool IsTargetingOfAlly()
  {
    return Object.op_Inequality((Object) this.GetAllyTarget(), (Object) null);
  }

  public bool IsAliveTargetOfAlly() => AIUtility.IsAlive(this.GetAllyTarget());

  public bool CanRescueOfTargetAlly()
  {
    StageObject allyTarget = this.GetAllyTarget();
    if (Object.op_Equality((Object) allyTarget, (Object) null))
      return false;
    switch (allyTarget)
    {
      case Player _:
        Player player = allyTarget as Player;
        if (player.isDead && (double) player.rescueTime > 0.0)
          return true;
        return player.IsStone() && (double) player.stoneRescueTime > 0.0;
      case Character _:
        Character character = allyTarget as Character;
        return character.isDead || character.IsStone();
      default:
        return false;
    }
  }

  public bool IsOtherPlayerReviveOfTarget()
  {
    Player allyTarget = this.GetAllyTarget() as Player;
    if (Object.op_Inequality((Object) allyTarget, (Object) null))
    {
      for (int index = 0; index < allyTarget.prayerIds.Count; ++index)
      {
        if (allyTarget.prayerIds[index] != this.brain.owner.id && !(MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(allyTarget.prayerIds[index]) as Player).isNpc)
          return true;
      }
    }
    return false;
  }

  public StageObject GetCurrentTarget()
  {
    if (Object.op_Equality((Object) this.brain.owner.actionTarget, (Object) null))
    {
      Self owner = this.brain.owner as Self;
      if (Object.op_Inequality((Object) owner, (Object) null) && owner.isAutoMode)
        return this.GetTargetObjectOfNearest();
    }
    return this.brain.owner.actionTarget;
  }

  public void MissCurrentTarget() => this.brain.owner.SetActionTarget((StageObject) null, false);

  public void SetCurrentTarget(StageObject target_obj)
  {
    StageObject currentTarget = this.GetCurrentTarget();
    this.brain.owner.SetActionTarget(target_obj);
    this.brain.opponentMem.OnTargetOpponent(target_obj, currentTarget);
  }

  public bool IsTargeting()
  {
    return Object.op_Inequality((Object) this.GetCurrentTarget(), (Object) null);
  }

  public void UpdateTarget()
  {
    StageObject target_obj = this.GetCurrentTarget();
    if (this.brain.opponentMem.haveHateControl)
    {
      if (this.IsTargetInterestLoseOfHate())
        target_obj = this.GetTargetObjectOfHate();
    }
    else if (!this.IsTargeting())
      target_obj = this.GetTargetObjectOfNearest();
    this.SetCurrentTarget(target_obj);
  }

  public StageObject GetTargetObjectOfNearest()
  {
    StageObject obj = (StageObject) null;
    double len = double.MaxValue;
    this.brain.opponentMem.GetListOfSensedOpponent().ForEach((Action<OpponentMemory.OpponentRecord>) (t =>
    {
      if ((double) t.record.distance >= len)
        return;
      obj = t.obj;
      len = (double) t.record.distance;
    }));
    return obj;
  }

  public StageObject GetTargetObjectOfScountingParam()
  {
    BrainParam.ScountingParam scoutParam = this.brain.param.scoutParam;
    if (scoutParam == null)
      return (StageObject) null;
    List<StageObject> targetObjectList = this.brain.GetTargetObjectList();
    for (int index = 0; index < targetObjectList.Count; ++index)
    {
      if (scoutParam.IsScouted(this.brain.owner._transform, targetObjectList[index]._transform))
        return targetObjectList[index];
    }
    return (StageObject) null;
  }

  public StageObject GetTargetObjectOfHate()
  {
    if (this.brain.opponentMem.IsHateCycleLastTurn())
    {
      OpponentMemory.OpponentRecord targetInHateCycle = this.brain.opponentMem.GetOpponentWithNotTargetInHateCycle();
      if (targetInHateCycle != null)
        return targetInHateCycle.obj;
    }
    OpponentMemory.OpponentRecord opponentWithHigherHate = this.brain.opponentMem.GetOpponentWithHigherHate();
    if (opponentWithHigherHate != null)
      return opponentWithHigherHate.obj;
    List<OpponentMemory.OpponentRecord> ofSensedOpponent = this.brain.opponentMem.GetListOfSensedOpponent();
    if (ofSensedOpponent.Count <= 0)
      return (StageObject) null;
    int index = Utility.Random(ofSensedOpponent.Count);
    return ofSensedOpponent[index].obj;
  }

  public OpponentMemory.OpponentRecord GetOpponent()
  {
    return this.brain.opponentMem.FindOrEmpty(this.GetCurrentTarget());
  }

  public bool IsAliveTarget() => AIUtility.IsAlive(this.GetCurrentTarget());

  public bool IsNearTarget() => this.GetOpponent().record.isNearPlace;

  public bool IsPlaceTarget(PLACE place)
  {
    StageObject currentTarget = this.GetCurrentTarget();
    return !Object.op_Equality((Object) currentTarget, (Object) null) && this.brain.opponentMem.IsPlaceOpponent(currentTarget, place);
  }

  public bool CanAttackTarget()
  {
    return this.brain.targetCtrl.IsSpecialAttackableTarget() || this.brain.targetCtrl.IsAttackableTarget() || this.brain.targetCtrl.IsArrivalAttackPosition() || this.brain.targetCtrl.IsAvoidAttackableTarget();
  }

  public bool IsAttackableTarget()
  {
    StageObject currentTarget = this.GetCurrentTarget();
    if (Object.op_Equality((Object) currentTarget, (Object) null))
      return false;
    if (this.brain.owner is Player)
    {
      Player owner = this.brain.owner as Player;
      if (owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && owner.actionID == Character.ACTION_ID.ATTACK && owner.enableInputCombo)
        return true;
    }
    return this.brain.opponentMem.IsAttackableOpponent(currentTarget);
  }

  public bool IsSpecialAttackableTarget()
  {
    StageObject currentTarget = this.GetCurrentTarget();
    return !Object.op_Equality((Object) currentTarget, (Object) null) && this.brain.opponentMem.IsSpecialAttackableOpponent(currentTarget);
  }

  public bool IsAvoidAttackableTarget()
  {
    StageObject currentTarget = this.GetCurrentTarget();
    if (Object.op_Equality((Object) currentTarget, (Object) null) || !(this.brain.owner is Player))
      return false;
    Player owner = this.brain.owner as Player;
    if (!owner.CheckAttackMode(Player.ATTACK_MODE.TWO_HAND_SWORD) || owner.CheckSpAttackType(SP_ATTACK_TYPE.SOUL))
      return false;
    if (this.brain.canAvoidAttack)
      return true;
    if (!this.brain.canCheckAvoidAttack || !owner.playerParameter.twoHandSwordActionInfo.avoidAttackEnable || !this.brain.opponentMem.IsAvoidAttackableOpponent(currentTarget))
      return false;
    this.brain.canCheckAvoidAttack = false;
    if (Utility.Dice100(80 /*0x50*/))
    {
      this.brain.canAvoidAttack = true;
      return true;
    }
    this.brain.canAvoidAttack = false;
    return false;
  }

  public bool IsArrivalTarget()
  {
    StageObject currentTarget = this.GetCurrentTarget();
    return !Object.op_Equality((Object) currentTarget, (Object) null) && this.brain.opponentMem.IsArrivalPosition(currentTarget);
  }

  public bool IsArrivalAttackPosition()
  {
    StageObject currentTarget = this.GetCurrentTarget();
    return !Object.op_Equality((Object) currentTarget, (Object) null) && this.brain.opponentMem.IsArrivalAttackPosition(currentTarget);
  }

  public float GetDistance() => this.GetOpponent().record.distance;

  public Vector3 GetTargetPosition() => this.GetOpponent().record.pos;

  public Vector3 GetAttackPosition() => this.GetOpponent().record.attackPos;

  public float GetLengthWithAttackPos(Vector3 check_pos)
  {
    StageObject currentTarget = this.GetCurrentTarget();
    return Object.op_Equality((Object) currentTarget, (Object) null) ? 0.0f : this.brain.opponentMem.GetLengthWithAttackPos(currentTarget, check_pos);
  }

  public bool IsTargetInterestLoseOfHate()
  {
    StageObject currentTarget = this.GetCurrentTarget();
    return Object.op_Equality((Object) currentTarget, (Object) null) || this.brain.opponentMem.IsOpponentInterestLoseOfHate(currentTarget);
  }
}
