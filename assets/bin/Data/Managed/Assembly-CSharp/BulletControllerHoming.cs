using UnityEngine;

public class BulletControllerHoming : BulletControllerBase
{
	private const float targetHightOffset = 1f;

	protected float homingLimit;

	protected float homingChangeStart;

	protected float homingChange;

	protected bool hightLock;

	protected float acceleration;

	public override void Update()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
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
		Vector3 targetPos = GetTargetPos();
		float num2 = Mathf.Abs(Vector3.Angle(base._transform.get_forward(), targetPos));
		if (num2 != 0f)
		{
			float num3 = num / num2;
			if (num3 > 1f)
			{
				num3 = 1f;
			}
			base._transform.set_rotation(Quaternion.Lerp(base._transform.get_rotation(), Quaternion.LookRotation(targetPos), num3));
			Vector3 val = Vector3.get_forward();
			val = base._transform.get_rotation() * val;
			val *= base.speed;
			if (hightLock)
			{
				val.y = 0f;
			}
			base._rigidbody.set_velocity(val);
		}
	}

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		InitParam(bullet.dataHoming);
	}

	protected void InitParam(BulletData.BulletHoming _data)
	{
		homingLimit = _data.limitAngel;
		homingChangeStart = _data.limitChangeStartTime;
		homingChange = _data.limitChangeAngel;
		hightLock = _data.hightLock;
		acceleration = _data.acceleration;
	}

	protected virtual Vector3 GetTargetPos()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
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
		return position2 - position;
	}
}
