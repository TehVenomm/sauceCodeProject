using UnityEngine;

public class BulletControllerArrowSoul : BulletControllerBase
{
	protected float accel;

	protected float accelStartTime;

	protected float maxSpeed;

	protected float angularVelocity;

	protected float angularStartTime;

	protected float ignoreAngle;

	protected bool isEndAccel;

	protected bool isLookTarget;

	protected Transform target;

	protected Vector3 direction = Vector3.get_forward();

	protected float speed0;

	protected float speed1;

	protected bool isPuppet;

	protected Vector3 puppetTargetPos;

	public void SetTarget(Transform trans)
	{
		target = trans;
	}

	public Transform GetTarget()
	{
		return target;
	}

	public void SetPuppetTargetPos(Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		puppetTargetPos = pos;
		isPuppet = true;
	}

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		base.timeCount = 0f;
		isEndAccel = false;
		isLookTarget = false;
		base.bulletSkillInfoParam = skillParam;
		speed0 = bullet.data.speed;
		speed1 = bullet.data.speed;
		accel = bullet.dataArrowSoul.accel;
		accelStartTime = bullet.dataArrowSoul.accelStartTime;
		maxSpeed = bullet.dataArrowSoul.maxSpeed;
		angularVelocity = bullet.dataArrowSoul.angularVelocity;
		angularStartTime = bullet.dataArrowSoul.angularStartTime;
		ignoreAngle = bullet.dataArrowSoul.ignoreAngle;
		base._transform.set_position(pos);
		int num = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulShotDirs.Length;
		int num2 = Random.Range(0, num - 1);
		Vector3 val = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulShotDirs[num2];
		direction = rot * val;
		object transform = (object)base._transform;
		transform.set_position(transform.get_position() - direction * MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulShotDirVec);
		base._transform.set_rotation(rot);
		base._rigidbody.set_velocity(direction * speed0);
	}

	public override void Update()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		if (!(target == null) || isPuppet)
		{
			base.timeCount += Time.get_deltaTime();
			bool flag = _CalcSpeed();
			_CalcAngle();
			if (isLookTarget)
			{
				Vector3 val = ((!isPuppet) ? target.get_position() : puppetTargetPos) - base._transform.get_position();
				float num = Mathf.Abs(Vector3.Angle(base._transform.get_forward(), val));
				if (num > ignoreAngle)
				{
					float num2 = angularVelocity * Time.get_deltaTime() / num;
					if (num2 > 1f)
					{
						num2 = 1f;
					}
					base._transform.set_rotation(Quaternion.Lerp(base._transform.get_rotation(), Quaternion.LookRotation(val), num2));
					direction = base._transform.get_rotation() * Vector3.get_forward();
					flag = true;
				}
			}
			if (flag)
			{
				base._rigidbody.set_velocity(direction * speed1);
			}
		}
	}

	private bool _CalcSpeed()
	{
		if (isEndAccel || base.timeCount < accelStartTime)
		{
			return false;
		}
		speed1 = speed0 + accel * (base.timeCount - accelStartTime);
		if (speed1 > maxSpeed)
		{
			speed1 = maxSpeed;
			isEndAccel = true;
		}
		return true;
	}

	private void _CalcAngle()
	{
		if (!isLookTarget && !(base.timeCount < angularStartTime))
		{
			isLookTarget = true;
		}
	}
}
