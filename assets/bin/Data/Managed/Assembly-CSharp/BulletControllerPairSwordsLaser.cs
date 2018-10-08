using UnityEngine;

public class BulletControllerPairSwordsLaser : BulletControllerBase
{
	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		if (base.speed <= 0f)
		{
			base._transform.localRotation = rot;
		}
		base._rigidbody.isKinematic = true;
	}
}
