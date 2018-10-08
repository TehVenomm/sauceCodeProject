using UnityEngine;

public class BulletControllerFall : BulletControllerBase
{
	protected float gravityStartTime;

	protected float gravityRate;

	public override void FixedUpdate()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		base.FixedUpdate();
		if (gravityStartTime >= 0f && base.timeCount >= gravityStartTime)
		{
			base._rigidbody.AddForce(Physics.get_gravity() * gravityRate, 5);
			if (base._rigidbody.get_velocity() != Vector3.get_zero())
			{
				base._transform.set_forward(base._rigidbody.get_velocity());
			}
		}
	}

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		gravityStartTime = bullet.dataFall.gravityStartTime;
		gravityRate = bullet.dataFall.gravityRate;
	}
}
