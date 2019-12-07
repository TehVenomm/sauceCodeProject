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
		num *= Time.deltaTime;
		Vector3 targetPos = GetTargetPos();
		float num2 = Mathf.Abs(Vector3.Angle(base._transform.forward, targetPos));
		if (num2 != 0f)
		{
			float num3 = num / num2;
			if (num3 > 1f)
			{
				num3 = 1f;
			}
			base._transform.rotation = Quaternion.Lerp(base._transform.rotation, Quaternion.LookRotation(targetPos), num3);
			Vector3 vector = Vector3.forward;
			vector = base._transform.rotation * vector;
			vector *= base.speed;
			if (hightLock)
			{
				vector.y = 0f;
			}
			base._rigidbody.velocity = vector;
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

	protected virtual Vector3 GetTargetPos()
	{
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
		return position2 - position;
	}
}
