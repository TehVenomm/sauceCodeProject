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

	protected TargetPoint target;

	protected Vector3 direction = Vector3.forward;

	protected float speed0;

	protected float speed1;

	protected bool isPuppet;

	protected Vector3 puppetTargetPos;

	public void SetTarget(TargetPoint point)
	{
		target = point;
	}

	public TargetPoint GetTarget()
	{
		return target;
	}

	public void SetPuppetTargetPos(Vector3 pos)
	{
		puppetTargetPos = pos;
		isPuppet = true;
	}

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
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
		base._transform.position = pos;
		int num = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulShotDirs.Length;
		int num2 = Random.Range(0, num - 1);
		Vector3 point = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulShotDirs[num2];
		direction = rot * point;
		base._transform.position -= direction * MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulShotDirVec;
		base._transform.rotation = rot;
		base._rigidbody.velocity = direction * speed0;
	}

	public override void Update()
	{
		if (target == null && !isPuppet)
		{
			return;
		}
		base.timeCount += Time.deltaTime;
		bool flag = _CalcSpeed();
		_CalcAngle();
		if (isLookTarget)
		{
			Vector3 vector = (isPuppet ? puppetTargetPos : target.GetTargetPoint()) - base._transform.position;
			float num = Mathf.Abs(Vector3.Angle(base._transform.forward, vector));
			if (num > ignoreAngle)
			{
				float num2 = angularVelocity * Time.deltaTime / num;
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				base._transform.rotation = Quaternion.Lerp(base._transform.rotation, Quaternion.LookRotation(vector), num2);
				direction = base._transform.rotation * Vector3.forward;
				flag = true;
			}
		}
		if (flag)
		{
			base._rigidbody.velocity = direction * speed1;
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
