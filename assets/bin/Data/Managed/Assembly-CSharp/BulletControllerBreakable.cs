using UnityEngine;

public class BulletControllerBreakable : BulletControllerBase
{
	private const float IS_LAND_HIT_MARGIN = 1f;

	private const float OFFSET_TARGET_HEIGHT = 1f;

	private BulletData.BulletBreakable.MOVE_TYPE moveType;

	private int breakCount;

	private int hitCounter;

	private int ignoreLayerMask;

	private float homingLimit;

	private float homingChangeStart;

	private float homingChange;

	private bool hightLock;

	private float acceleration;

	private int damageToEndurance;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		if (bullet.dataBreakable != null)
		{
			moveType = bullet.dataBreakable.moveType;
			hitCounter = 0;
			breakCount = bullet.dataBreakable.breakCount;
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
			if (bullet.dataBreakable.isIgnoreHitAttackable)
			{
				ignoreLayerMask |= -2147483648;
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
		if (moveType != 0)
		{
			base.timeCount += Time.deltaTime;
			if (!((Object)targetObject == (Object)null))
			{
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
				num *= Time.deltaTime;
				Vector3 position = base._transform.position;
				Vector3 position2 = targetObject._transform.position;
				if (hightLock)
				{
					position2.y = 0f;
					position.y = 0f;
				}
				else
				{
					position2.y += 1f;
				}
				Vector3 vector = position2 - position;
				float num2 = Mathf.Abs(Vector3.Angle(base._transform.forward, vector));
				if (num2 != 0f)
				{
					float num3 = num / num2;
					if (num3 > 1f)
					{
						num3 = 1f;
					}
					base._transform.rotation = Quaternion.Lerp(base._transform.rotation, Quaternion.LookRotation(vector), num3);
					Vector3 vector2 = Vector3.forward;
					vector2 = base._transform.rotation * vector2;
					vector2 *= base.speed;
					if (hightLock)
					{
						vector2.y = 0f;
					}
					base._rigidbody.velocity = vector2;
				}
			}
		}
	}

	public override void OnShot()
	{
		base.gameObject.layer = 31;
	}

	public override bool IsHit(Collider collider)
	{
		if (((1 << collider.gameObject.layer) & ignoreLayerMask) > 0)
		{
			return false;
		}
		DangerRader component = collider.gameObject.GetComponent<DangerRader>();
		if ((Object)component != (Object)null)
		{
			return false;
		}
		return true;
	}

	public override bool IsBreak(Collider collider)
	{
		if (((1 << collider.gameObject.layer) & ignoreLayerMask) > 0)
		{
			return false;
		}
		hitCounter++;
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
		if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle() && MonoBehaviourSingleton<InGameProgress>.IsValid() && !(MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEndurance <= 0f))
		{
			MonoBehaviourSingleton<InGameProgress>.I.DamageToEndurance(damageToEndurance);
			if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(base._transform.position, 1f, 0.2f);
			}
		}
	}
}
