using UnityEngine;

public class BulletControllerCurve : BulletControllerBase
{
	protected FloatInterpolator curve;

	protected FloatInterpolator curveTime;

	protected Vector3 curveAxis = Vector3.zero;

	protected Quaternion baseRotation = Quaternion.identity;

	public override void Update()
	{
		base.Update();
		if (baseRotation == Quaternion.identity)
		{
			baseRotation = base._transform.rotation;
		}
		curve.Update(Time.deltaTime * curveTime.Update());
		Quaternion rhs = Quaternion.AngleAxis(curve.Get(), curveAxis);
		base._transform.rotation = baseRotation * rhs;
		Vector3 forward = Vector3.forward;
		forward = base._transform.rotation * forward;
		forward *= base.speed;
		base._rigidbody.velocity = forward;
	}

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
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
