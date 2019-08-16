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
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
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
				centralPoint = Vector3.get_zero();
			}
		}
		else
		{
			centralPoint = targetObject._position;
		}
	}

	public override void Update()
	{
		base.timeCount += Time.get_deltaTime();
		if (!(waitTime >= base.timeCount))
		{
			UpdateRotateAroundCentralPosition();
		}
	}

	private void UpdateRotateAroundCentralPosition()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = base._transform.get_position();
		Vector3 val = position - centralPoint;
		val.Normalize();
		val *= rotateRadius;
		float num = (!isLinearSpeedUp) ? rotateAngle_Deg : Mathf.Lerp(0f, rotateAngle_Deg, Mathf.Clamp01((base.timeCount - waitTime) / speedUpTime));
		val = Quaternion.AngleAxis((float)rotateSign * num * Time.get_deltaTime(), rotateAxis) * val;
		Vector3 val2 = centralPoint + val - position;
		base._transform.set_position(position + val2);
	}
}
