using UnityEngine;

public class PlayerAttackFunnelBit : AttackFunnelBit
{
	public override void Initialize(StageObject attacker, AttackInfo atkInfo, StageObject targetObj, Transform launchTrans, Vector3 offsetPos, Quaternion offsetRot)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(attacker, atkInfo, targetObj, launchTrans, offsetPos, offsetRot);
		Player player = attacker as Player;
		if (player != null)
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
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Enemy enemy = base.TargetObject as Enemy;
		return enemy == null || enemy.isDead || !enemy.get_enabled() || !enemy.get_gameObject().get_activeInHierarchy();
	}

	protected override StageObject SearchNearestTarget(Vector2 bulletPos, float searchRadius)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
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
		if (enemy != null)
		{
			num += enemy.bodyRadius;
		}
		return num;
	}

	protected override float GetFloatingHeight()
	{
		float num = base.GetFloatingHeight();
		Enemy enemy = base.TargetObject as Enemy;
		if (enemy != null)
		{
			num += enemy.bodyRadius;
		}
		return num;
	}
}
