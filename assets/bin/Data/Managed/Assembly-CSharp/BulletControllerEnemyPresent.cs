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
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, skillParam, pos, rot);
		bulletData = bullet;
		this.get_gameObject().set_name("EnemyPresentBullet");
		this.get_gameObject().set_layer(31);
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
		cachedCollider = this.get_gameObject().AddComponent<SphereCollider>();
		cachedCollider.set_radius(bullet.data.radius);
		cachedCollider.set_center(bullet.data.hitOffset);
		cachedCollider.set_isTrigger(true);
		cachedCollider.set_enabled(true);
		isPicked = false;
	}

	public override bool IsHit(Collider collider)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (isPicked)
		{
			return false;
		}
		int layer = collider.get_gameObject().get_layer();
		if (((1 << layer) & ignoreLayerMask) > 0)
		{
			return false;
		}
		if (layer == 8 && collider.get_gameObject().GetComponent<DangerRader>() != null)
		{
			return false;
		}
		Self component = collider.get_gameObject().GetComponent<Self>();
		if (component == null)
		{
			return false;
		}
		return true;
	}

	public override void OnHit(Collider collider)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		isPicked = true;
		if (cachedCollider != null)
		{
			cachedCollider.set_enabled(false);
		}
		Self component = collider.get_gameObject().GetComponent<Self>();
		if (component != null)
		{
			_ExecHeal(component);
			_ExecBuff(component);
		}
		if (bulletObject != null)
		{
			bulletObject.NotifyBroken(false);
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
