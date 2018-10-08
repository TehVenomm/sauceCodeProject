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
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		if (moveType != 0)
		{
			base.timeCount += Time.get_deltaTime();
			if (!(targetObject == null))
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
		}
	}

	public override void OnShot()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().set_layer(31);
	}

	public override bool IsHit(Collider collider)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (((1 << collider.get_gameObject().get_layer()) & ignoreLayerMask) > 0)
		{
			return false;
		}
		DangerRader component = collider.get_gameObject().GetComponent<DangerRader>();
		if (component != null)
		{
			return false;
		}
		return true;
	}

	public override bool IsBreak(Collider collider)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		if (((1 << collider.get_gameObject().get_layer()) & ignoreLayerMask) > 0)
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
