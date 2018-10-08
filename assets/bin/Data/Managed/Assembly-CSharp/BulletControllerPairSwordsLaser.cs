using UnityEngine;

public class BulletControllerPairSwordsLaser : BulletControllerBase
{
	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		if (base.speed <= 0f)
		{
			base._transform.set_localRotation(rot);
		}
		base._rigidbody.set_isKinematic(true);
	}
}
