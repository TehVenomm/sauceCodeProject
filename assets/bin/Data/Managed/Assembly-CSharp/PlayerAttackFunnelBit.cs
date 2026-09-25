// Decompiled with JetBrains decompiler
// Type: PlayerAttackFunnelBit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class PlayerAttackFunnelBit : AttackFunnelBit
{
  public override void Initialize(
    StageObject attacker,
    AttackInfo atkInfo,
    StageObject targetObj,
    Transform launchTrans,
    Vector3 offsetPos,
    Quaternion offsetRot)
  {
    base.Initialize(attacker, atkInfo, targetObj, launchTrans, offsetPos, offsetRot);
    Player player = attacker as Player;
    if (!Object.op_Inequality((Object) player, (Object) null))
      return;
    AtkAttribute atk = new AtkAttribute();
    attacker.GetAtk(atkInfo as AttackHitInfo, ref atk);
    this.SetAttackMode(player.attackMode);
    this.SetExAtk(atk);
    this.SetSkillParam(player.skillInfo.actSkillParam);
  }

  protected override bool CheckTargetDead()
  {
    Enemy targetObject = this.TargetObject as Enemy;
    return Object.op_Equality((Object) targetObject, (Object) null) || targetObject.isDead || !((Behaviour) targetObject).enabled || !((Component) targetObject).gameObject.activeInHierarchy;
  }

  protected override StageObject SearchNearestTarget(Vector2 bulletPos, float searchRadius)
  {
    float num1 = float.MaxValue;
    StageObject stageObject = (StageObject) null;
    int count = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList.Count;
    for (int index = 0; index < count; ++index)
    {
      Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList[index];
      if (!enemy.isDead)
      {
        float num2 = Vector2.Distance(enemy.positionXZ, bulletPos);
        if ((double) num2 <= (double) enemy.bodyRadius + (double) searchRadius && (double) num2 <= (double) num1)
        {
          num1 = num2;
          stageObject = (StageObject) enemy;
        }
      }
    }
    return stageObject;
  }

  protected override float GetAttackStartRange()
  {
    float attackStartRange = base.GetAttackStartRange();
    Enemy targetObject = this.TargetObject as Enemy;
    if (Object.op_Inequality((Object) targetObject, (Object) null))
      attackStartRange += targetObject.bodyRadius;
    return attackStartRange;
  }

  protected override float GetFloatingHeight()
  {
    float floatingHeight = base.GetFloatingHeight();
    Enemy targetObject = this.TargetObject as Enemy;
    if (Object.op_Inequality((Object) targetObject, (Object) null))
      floatingHeight += targetObject.bodyRadius;
    return floatingHeight;
  }
}
