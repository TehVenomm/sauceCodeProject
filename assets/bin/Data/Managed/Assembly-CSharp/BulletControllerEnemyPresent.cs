using System.Collections.Generic;
using UnityEngine;

public class BulletControllerEnemyPresent : BulletControllerBase
{
	private const string OBJECT_NAME = "EnemyPresentBullet";

	private BulletData bulletData;

	private SphereCollider cachedCollider;

	private int ignoreLayerMask;

	private bool isPicked;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, skillParam, pos, rot);
		bulletData = bullet;
		base.gameObject.name = "EnemyPresentBullet";
		base.gameObject.layer = 31;
		if (!bullet.dataEnemyPresent.isHitEnemyAttack)
		{
			ignoreLayerMask |= 8192;
		}
		if (!bullet.dataEnemyPresent.isHitEnemyMove)
		{
			ignoreLayerMask |= 1024;
		}
		ignoreLayerMask |= 32768;
		ignoreLayerMask |= 20480;
		if (!bullet.data.isObjectHitDelete)
		{
			ignoreLayerMask |= 2490880;
		}
		cachedCollider = base.gameObject.AddComponent<SphereCollider>();
		cachedCollider.radius = bullet.data.radius;
		cachedCollider.center = bullet.data.hitOffset;
		cachedCollider.isTrigger = true;
		cachedCollider.enabled = true;
		isPicked = false;
	}

	public override bool IsHit(Collider collider)
	{
		if (isPicked)
		{
			return false;
		}
		int layer = collider.gameObject.layer;
		if (((1 << layer) & ignoreLayerMask) > 0)
		{
			return false;
		}
		if (layer == 8 && collider.gameObject.GetComponent<DangerRader>() != null)
		{
			return false;
		}
		if (collider.gameObject.GetComponent<Self>() == null)
		{
			return false;
		}
		return true;
	}

	public override void OnHit(Collider collider)
	{
		isPicked = true;
		if (cachedCollider != null)
		{
			cachedCollider.enabled = false;
		}
		Self component = collider.gameObject.GetComponent<Self>();
		if (component != null)
		{
			_ExecHeal(component);
			_ExecBuff(component);
		}
		if (bulletObject != null)
		{
			bulletObject.NotifyBroken(isSendOnlyOriginal: false);
		}
	}

	private void _ExecHeal(Self self)
	{
		Character.HealData healData = new Character.HealData(bulletData.dataEnemyPresent.value, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>
		{
			10
		});
		if (bulletData.dataEnemyPresent.valueType == CALCULATE_TYPE.RATE)
		{
			healData.healHp = (int)((float)bulletData.dataEnemyPresent.value * 0.01f * (float)self.hpMax);
			healData.applyAbilityTypeList.Clear();
		}
		self.OnHealReceive(healData);
	}

	private void _ExecBuff(Self self)
	{
		if (bulletData.dataEnemyPresent.buffIds != null && bulletData.dataEnemyPresent.buffIds.Count > 0)
		{
			int i = 0;
			for (int count = bulletData.dataEnemyPresent.buffIds.Count; i < count; i++)
			{
				self.StartBuffByBuffTableId(bulletData.dataEnemyPresent.buffIds[i], null);
			}
		}
	}
}
