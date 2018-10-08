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
		base.Update();
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

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
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
