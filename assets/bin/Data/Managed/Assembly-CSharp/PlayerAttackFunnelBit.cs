using UnityEngine;

public class PlayerAttackFunnelBit : AttackFunnelBit
{
	public override void Initialize(StageObject attacker, AttackInfo atkInfo, StageObject targetObj, Transform launchTrans, Vector3 offsetPos, Quaternion offsetRot)
	{
		base.Initialize(attacker, atkInfo, targetObj, launchTrans, offsetPos, offsetRot);
		Player player = attacker as Player;
		if ((Object)player != (Object)null)
		{
			AtkAttribute atk = new AtkAttribute();
			attacker.GetAtk(atkInfo as AttackHitInfo, ref atk);
			SetAttackMode(player.attackMode);
			SetExAtk(atk);
			SetSkillParam(player.skillInfo.actSkillParam);
		}
	}

	protected override bool CheckTargetDead()
	{
		Enemy enemy = base.TargetObject as Enemy;
		return (Object)enemy == (Object)null || enemy.isDead || !enemy.enabled || !enemy.gameObject.activeInHierarchy;
	}

	protected override StageObject SearchNearestTarget(Vector2 bulletPos, float searchRadius)
	{
		float num = 3.40282347E+38f;
		StageObject result = null;
		int count = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList.Count;
		for (int i = 0; i < count; i++)
		{
			Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList[i];
			if (!enemy.isDead)
			{
				float num2 = Vector2.Distance(enemy.positionXZ, bulletPos);
				if (num2 <= enemy.bodyRadius + searchRadius && num2 <= num)
				{
					num = num2;
					result = enemy;
				}
			}
		}
		return result;
	}

	protected override float GetAttackStartRange()
	{
		float num = base.GetAttackStartRange();
		Enemy enemy = base.TargetObject as Enemy;
		if ((Object)enemy != (Object)null)
		{
			num += enemy.bodyRadius;
		}
		return num;
	}

	protected override float GetFloatingHeight()
	{
		float num = base.GetFloatingHeight();
		Enemy enemy = base.TargetObject as Enemy;
		if ((Object)enemy != (Object)null)
		{
			num += enemy.bodyRadius;
		}
		return num;
	}
}
