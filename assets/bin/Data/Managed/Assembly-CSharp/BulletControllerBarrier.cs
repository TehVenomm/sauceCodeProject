using UnityEngine;

public class BulletControllerBarrier : BulletControllerBase
{
	private int ignoreLayerMask;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		ignoreLayerMask |= 3072;
		ignoreLayerMask |= 20480;
		ignoreLayerMask |= 393728;
	}

	public override bool IsHit(Collider collider)
	{
		if (((1 << collider.gameObject.layer) & ignoreLayerMask) > 0)
		{
			return false;
		}
		AnimEventCollider.AtkColliderHiter component = collider.gameObject.GetComponent<AnimEventCollider.AtkColliderHiter>();
		if ((Object)component != (Object)null)
		{
			return false;
		}
		DangerRader component2 = collider.gameObject.GetComponent<DangerRader>();
		if ((Object)component2 != (Object)null)
		{
			return false;
		}
		return true;
	}
}
