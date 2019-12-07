using UnityEngine;

public class BulletControllerRotateBit : BulletControllerBase
{
	private Vector3 centralPoint;

	private float rotateAngle_Deg;

	private float rotateRadius;

	private Vector3 rotateAxis;

	private int rotateSign;

	private float waitTime;

	private float speedUpTime;

	private bool isLinearSpeedUp;

	private BulletData bulletData;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, skillParam, pos, rot);
		bulletData = bullet;
		rotateAngle_Deg = bullet.dataRotateBit.rotateAngle_Deg;
		rotateRadius = bullet.dataRotateBit.rotateRadius;
		rotateAxis = bullet.dataRotateBit.rotateAxis;
		rotateSign = ((bullet.dataRotateBit.rotateSign <= 0) ? 1 : (-1));
		waitTime = bullet.dataRotateBit.waitTime;
		isLinearSpeedUp = bullet.dataRotateBit.isLinearAngleSpeedUp;
		speedUpTime = bullet.dataRotateBit.speedUpTime;
	}

	public override void RegisterTargetObject(StageObject obj)
	{
		base.RegisterTargetObject(obj);
		if (bulletData.dataRotateBit.isSetCentralPosition)
		{
			centralPoint = bulletData.dataRotateBit.centralPosition;
		}
		else if (targetObject == null)
		{
			if (fromObject != null)
			{
				centralPoint = fromObject._position + fromObject._rotation * new Vector3(0f, 0f, 2f);
			}
			else
			{
				centralPoint = Vector3.zero;
			}
		}
		else
		{
			centralPoint = targetObject._position;
		}
	}

	public override void Update()
	{
		base.timeCount += Time.deltaTime;
		if (!(waitTime >= base.timeCount))
		{
			UpdateRotateAroundCentralPosition();
		}
	}

	private void UpdateRotateAroundCentralPosition()
	{
		Vector3 position = base._transform.position;
		Vector3 point = position - centralPoint;
		point.Normalize();
		point *= rotateRadius;
		float num = isLinearSpeedUp ? Mathf.Lerp(0f, rotateAngle_Deg, Mathf.Clamp01((base.timeCount - waitTime) / speedUpTime)) : rotateAngle_Deg;
		point = Quaternion.AngleAxis((float)rotateSign * num * Time.deltaTime, rotateAxis) * point;
		Vector3 b = centralPoint + point - position;
		base._transform.position = position + b;
	}
}
