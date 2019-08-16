using UnityEngine;

public class BulletControllerBarrier : BulletControllerBase
{
	private int ignoreLayerMask;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		ignoreLayerMask |= 3072;
		ignoreLayerMask |= 20480;
		ignoreLayerMask |= 393728;
	}

	public override bool IsHit(Collider collider)
	{
		if (((1 << collider.get_gameObject().get_layer()) & ignoreLayerMask) > 0)
		{
			return false;
		}
		AnimEventCollider.AtkColliderHiter component = collider.get_gameObject().GetComponent<AnimEventCollider.AtkColliderHiter>();
		if (component != null)
		{
			return false;
		}
		DangerRader component2 = collider.get_gameObject().GetComponent<DangerRader>();
		if (component2 != null)
		{
			return false;
		}
		return true;
	}
}
