using UnityEngine;

public class BulletControllerCurve : BulletControllerBase
{
	protected FloatInterpolator curve;

	protected FloatInterpolator curveTime;

	protected Vector3 curveAxis = Vector3.get_zero();

	protected Quaternion baseRotation = Quaternion.get_identity();

	public override void Update()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		if (baseRotation == Quaternion.get_identity())
		{
			baseRotation = base._transform.get_rotation();
		}
		curve.Update(Time.get_deltaTime() * curveTime.Update());
		Quaternion val = Quaternion.AngleAxis(curve.Get(), curveAxis);
		base._transform.set_rotation(baseRotation * val);
		Vector3 forward = Vector3.get_forward();
		forward = base._transform.get_rotation() * forward;
		forward *= base.speed;
		base._rigidbody.set_velocity(forward);
	}

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		curve = new FloatInterpolator();
		curve.loopType = Interpolator.LOOP.REPETE;
		curve.Set(bullet.dataCurve.loopTime, 0f, bullet.dataCurve.curveAngle, bullet.dataCurve.curveAnim, 0f, null);
		curve.Play();
		curveTime = new FloatInterpolator();
		curveTime.Set(bullet.data.appearTime, 1f, 0f, bullet.dataCurve.timeAnim, 0f, null);
		curveTime.Play();
		curveAxis = bullet.dataCurve.curveAxis;
	}
}
