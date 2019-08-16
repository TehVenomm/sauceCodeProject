using UnityEngine;

public class BulletControllerCurve : BulletControllerBase
{
	protected FloatInterpolator curve;

	protected FloatInterpolator curveTime;

	protected Vector3 curveAxis = Vector3.get_zero();

	protected Quaternion baseRotation = Quaternion.get_identity();

	public override void Update()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		baseRotation = base._transform.get_rotation();
		curve = new FloatInterpolator();
		curve.loopType = Interpolator.LOOP.REPETE;
		curve.Set(bullet.dataCurve.loopTime, 0f, bullet.dataCurve.curveAngle, bullet.dataCurve.curveAnim, 0f);
		curve.Play();
		curveTime = new FloatInterpolator();
		curveTime.Set(bullet.data.appearTime, 1f, 0f, bullet.dataCurve.timeAnim, 0f);
		curveTime.Play();
		curveAxis = bullet.dataCurve.curveAxis;
	}
}
