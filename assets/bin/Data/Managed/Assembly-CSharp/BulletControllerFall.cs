using UnityEngine;

public class BulletControllerFall : BulletControllerBase
{
	protected float gravityStartTime;

	protected float gravityRate;

	public override void FixedUpdate()
	{
		base.FixedUpdate();
		if (gravityStartTime >= 0f && base.timeCount >= gravityStartTime)
		{
			base._rigidbody.AddForce(Physics.gravity * gravityRate, ForceMode.Acceleration);
			if (base._rigidbody.velocity != Vector3.zero)
			{
				base._transform.forward = base._rigidbody.velocity;
			}
		}
	}

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		gravityStartTime = bullet.dataFall.gravityStartTime;
		gravityRate = bullet.dataFall.gravityRate;
	}
}
