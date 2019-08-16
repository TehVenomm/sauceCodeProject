using System.Collections.Generic;
using UnityEngine;

public class HealAttackZoneObject : HealAttackObject
{
	private class TargetInfo
	{
		private Dictionary<string, bool> collInfo = new Dictionary<string, bool>();

		public float sec;

		public float lastTime;

		public void Enter(string name)
		{
			if (collInfo.ContainsKey(name))
			{
				collInfo[name] = true;
			}
			else
			{
				collInfo.Add(name, value: true);
			}
		}

		public void Stay(string name)
		{
			if (collInfo.ContainsKey(name))
			{
				collInfo[name] = true;
			}
			else
			{
				collInfo.Add(name, value: true);
			}
			if (lastTime != Time.get_time())
			{
				sec += Time.get_deltaTime();
				lastTime = Time.get_time();
			}
		}

		public void Exit(string name)
		{
			if (collInfo.ContainsKey(name))
			{
				collInfo[name] = false;
			}
			if (!_CheckValid())
			{
				sec = 0f;
				lastTime = 0f;
			}
		}

		private bool _CheckValid()
		{
			foreach (bool value in collInfo.Values)
			{
				if (value)
				{
					return true;
				}
			}
			return false;
		}
	}

	private Dictionary<int, TargetInfo> targetCollection = new Dictionary<int, TargetInfo>();

	private float intervalTime;

	protected override bool isDuplicateAttackInfo => true;

	protected override string GetAttackInfoName()
	{
		return "sk_heal_atk_zone";
	}

	public void Setup(Player owner, Transform trans, BulletData bullet, SkillInfo.SkillParam skill)
	{
		Initialize(owner, trans, skill.supportValue[0], bullet.data.radius);
		intervalTime = bullet.dataZone.intervalTime;
		AttackHitInfo attackHitInfo = m_attackInfo as AttackHitInfo;
		if (attackHitInfo != null)
		{
			attackHitInfo.atkRate /= Mathf.FloorToInt(skill.supportTime[0] / intervalTime);
		}
		targetCollection.Clear();
	}

	protected override void Update()
	{
		m_timeCount += Time.get_deltaTime();
	}

	protected override void OnTriggerEnter(Collider collider)
	{
		Enemy enemy = _GetValidEnemy(collider);
		if (!object.ReferenceEquals(enemy, null))
		{
			if (targetCollection.ContainsKey(enemy.id))
			{
				targetCollection[enemy.id].Enter(collider.get_name());
				return;
			}
			TargetInfo targetInfo = new TargetInfo();
			targetInfo.Enter(collider.get_name());
			targetCollection.Add(enemy.id, targetInfo);
		}
	}

	protected override void OnTriggerStay(Collider collider)
	{
		Enemy enemy = _GetValidEnemy(collider);
		if (!object.ReferenceEquals(enemy, null))
		{
			if (!targetCollection.ContainsKey(enemy.id))
			{
				targetCollection.Add(enemy.id, new TargetInfo());
			}
			targetCollection[enemy.id].Stay(collider.get_name());
			if (targetCollection[enemy.id].sec >= intervalTime)
			{
				targetCollection[enemy.id].sec -= intervalTime;
				m_attackHitChecker.ClearTarget(m_attackInfo, enemy);
				m_colliderProcessor.OnTriggerEnter(collider);
			}
		}
	}

	protected override void OnTriggerExit(Collider collider)
	{
		Enemy enemy = _GetValidEnemy(collider);
		if (!object.ReferenceEquals(enemy, null) && targetCollection.ContainsKey(enemy.id))
		{
			targetCollection[enemy.id].Exit(collider.get_name());
		}
	}

	private Enemy _GetValidEnemy(Collider collider)
	{
		Enemy componentInParent = collider.get_gameObject().GetComponentInParent<Enemy>();
		if (object.ReferenceEquals(componentInParent, null))
		{
			return null;
		}
		if (componentInParent.healDamageRate <= 0f)
		{
			return null;
		}
		return componentInParent;
	}
}
