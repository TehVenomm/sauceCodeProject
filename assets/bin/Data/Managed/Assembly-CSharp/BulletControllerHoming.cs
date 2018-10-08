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
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
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
}
