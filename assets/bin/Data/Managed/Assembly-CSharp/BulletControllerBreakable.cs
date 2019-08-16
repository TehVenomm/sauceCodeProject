using System.Collections.Generic;
using UnityEngine;

public class BulletControllerBreakable : BulletControllerBase
{
	private class HitTimerInfo
	{
		public string name;

		public float timer;

		public HitTimerInfo(string name, float timer)
		{
			this.name = name;
			this.timer = timer;
		}
	}

	private const float OFFSET_TARGET_HEIGHT = 1f;

	private BulletData.BulletBreakable.MOVE_TYPE moveType;

	private List<HitTimerInfo> hitTimerInfoList = new List<HitTimerInfo>();

	private int breakCount;

	private int hitCounter;

	private int ignoreLayerMask;

	private int ignoreHitCountLayerMask;

	private float homingLimit;

	private float homingChangeStart;

	private float homingChange;

	private bool hightLock;

	private float acceleration;

	private int damageToEndurance;

	private BulletData emissionBulletOnBroken;

	private string emissionBulletAttackInfoName;

	private bool enableEmissionBulletOnBroken => emissionBulletOnBroken != null;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		if (bullet.dataBreakable != null)
		{
			moveType = bullet.dataBreakable.moveType;
			hitCounter = 0;
			breakCount = bullet.dataBreakable.breakCount;
			emissionBulletOnBroken = bullet.dataBreakable.emissionBulletOnBroken;
			emissionBulletAttackInfoName = bullet.dataBreakable.emissionBulletAttackInfoName;
			if (bullet.dataBreakable.isIgnoreHitEnemyAttack)
			{
				ignoreLayerMask |= 40960;
			}
			if (bullet.dataBreakable.isIgnoreHitEnemyBody)
			{
				ignoreLayerMask |= 2048;
			}
			if (bullet.dataBreakable.isIgnoreHitEnemyMove)
			{
				ignoreLayerMask |= 1024;
			}
			if (bullet.dataBreakable.isIgnoreHitPlayerBody)
			{
				ignoreLayerMask |= 256;
			}
			if (bullet.dataBreakable.isIgnoreHitPlayerAttack)
			{
				ignoreLayerMask |= 20480;
			}
			if (bullet.dataBreakable.isIgnoreHitWallAndObject)
			{
				ignoreLayerMask |= 393728;
			}
			ignoreHitCountLayerMask = ignoreLayerMask;
			if (bullet.dataBreakable.isIgnoreHitCountPlayerBody)
			{
				ignoreHitCountLayerMask |= 256;
			}
			homingLimit = bullet.dataToEndurance.limitAngel;
			homingChangeStart = bullet.dataToEndurance.limitChangeStartTime;
			homingChange = bullet.dataToEndurance.limitChangeAngel;
			hightLock = bullet.dataToEndurance.hightLock;
			acceleration = bullet.dataToEndurance.acceleration;
			damageToEndurance = bullet.dataToEndurance.toEnduranceDamage;
		}
	}

	public override void Update()
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		if (moveType == BulletData.BulletBreakable.MOVE_TYPE.NORMAL)
		{
			return;
		}
		base.timeCount += Time.get_deltaTime();
		int i = 0;
		for (int count = hitTimerInfoList.Count; i < count; i++)
		{
			hitTimerInfoList[i].timer -= Time.get_deltaTime();
		}
		hitTimerInfoList.RemoveAll((HitTimerInfo item) => item.timer <= 0f);
		if (targetObject == null)
		{
			return;
		}
		float velocity = base.initialVelocity + acceleration * base.timeCount;
		SetVelocity(velocity);
		float num = homingLimit;
		if (base.timeCount > homingChangeStart)
		{
			num -= homingChange * (base.timeCount - homingChangeStart);
			if (num < 0f)
			{
				num = 0f;
			}
		}
		num *= Time.get_deltaTime();
		Vector3 position = base._transform.get_position();
		Vector3 position2 = targetObject._transform.get_position();
		if (hightLock)
		{
			position2.y = 0f;
			position.y = 0f;
		}
		else
		{
			position2.y += 1f;
		}
		Vector3 val = position2 - position;
		float num2 = Mathf.Abs(Vector3.Angle(base._transform.get_forward(), val));
		if (num2 != 0f)
		{
			float num3 = num / num2;
			if (num3 > 1f)
			{
				num3 = 1f;
			}
			base._transform.set_rotation(Quaternion.Lerp(base._transform.get_rotation(), Quaternion.LookRotation(val), num3));
			Vector3 val2 = Vector3.get_forward();
			val2 = base._transform.get_rotation() * val2;
			val2 *= base.speed;
			if (hightLock)
			{
				val2.y = 0f;
			}
			base._rigidbody.set_velocity(val2);
		}
	}

	public override void OnShot()
	{
		this.get_gameObject().set_layer(31);
	}

	public override bool IsHit(Collider collider)
	{
		if (((1 << collider.get_gameObject().get_layer()) & ignoreLayerMask) > 0)
		{
			return false;
		}
		DangerRader component = collider.get_gameObject().GetComponent<DangerRader>();
		if (component != null)
		{
			return false;
		}
		AnimEventCollider.AtkColliderHiter atkHiter = collider.get_gameObject().GetComponent<AnimEventCollider.AtkColliderHiter>();
		if (atkHiter != null)
		{
			return !hitTimerInfoList.Exists((HitTimerInfo item) => item.name == atkHiter.attackInfo.name);
		}
		return true;
	}

	public override void OnHit(Collider collider)
	{
		if (((1 << collider.get_gameObject().get_layer()) & ignoreHitCountLayerMask) > 0)
		{
			return;
		}
		hitCounter++;
		AnimEventCollider.AtkColliderHiter atkHiter = collider.get_gameObject().GetComponent<AnimEventCollider.AtkColliderHiter>();
		if (atkHiter != null && !hitTimerInfoList.Exists((HitTimerInfo item) => item.name == atkHiter.attackInfo.name))
		{
			AttackHitInfo attackHitInfo = atkHiter.attackInfo as AttackHitInfo;
			if (attackHitInfo != null)
			{
				hitTimerInfoList.Add(new HitTimerInfo(attackHitInfo.name, attackHitInfo.hitIntervalTime));
			}
		}
		if (enableEmissionBulletOnBroken && (collider.get_gameObject().get_layer() == 12 || collider.get_gameObject().get_layer() == 14) && IsBreak(collider))
		{
			CreateEmissionBulletOnBroken();
		}
	}

	private void CreateEmissionBulletOnBroken()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		AttackInfo attackInfo = fromObject.FindAttackInfo(emissionBulletAttackInfoName);
		if (attackInfo == null)
		{
			attackInfo = bulletObject.GetAttackInfo();
		}
		if (attackInfo != null)
		{
			BulletData bulletData = emissionBulletOnBroken;
			if (!(bulletData == null) && !(fromObject == null))
			{
				AnimEventShot.CreateByExternalBulletData(bulletData, fromObject, attackInfo, base._transform.get_position(), base._transform.get_rotation());
			}
		}
	}

	public override bool IsBreak(Collider collider)
	{
		if (breakCount <= 0 || hitCounter >= breakCount)
		{
			return true;
		}
		return false;
	}

	public int GetHitCount()
	{
		return hitCounter;
	}

	public void SetHitCount(int count)
	{
		hitCounter = count;
	}

	public override void OnLandHit()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle() && MonoBehaviourSingleton<InGameProgress>.IsValid() && !(MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEndurance <= 0f))
		{
			MonoBehaviourSingleton<InGameProgress>.I.DamageToEndurance(damageToEndurance);
			if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(base._transform.get_position(), 1f, 0.2f);
			}
		}
	}
}
