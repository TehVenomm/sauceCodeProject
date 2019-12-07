using UnityEngine;

public class BulletControllerSpearBarrier : BulletControllerBase
{
	private static readonly string OBJECT_NAME = "SpearBarrierBullet";

	private BulletData bulletData;

	private SphereCollider cachedCollider;

	private int ignoreLayerMask;

	private Player owner;

	private bool isRegistered;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, skillParam, pos, rot);
		bulletData = bullet;
		base.gameObject.name = OBJECT_NAME;
		base.gameObject.layer = 31;
		ignoreLayerMask |= 41984;
		ignoreLayerMask |= 20480;
		if (!bullet.data.isObjectHitDelete)
		{
			ignoreLayerMask |= 2490880;
		}
	}

	public override void RegisterFromObject(StageObject obj)
	{
		base.RegisterFromObject(obj);
		owner = (obj as Player);
		base._transform.position = obj._position;
		base._transform.rotation = obj._rotation;
		isRegistered = true;
	}

	public override bool IsHit(Collider collider)
	{
		int layer = collider.gameObject.layer;
		if (((1 << layer) & ignoreLayerMask) > 0)
		{
			return false;
		}
		if (layer == 8 && collider.gameObject.GetComponent<DangerRader>() != null)
		{
			return false;
		}
		return true;
	}

	public override void Update()
	{
		base.timeCount += Time.deltaTime;
		if (owner != null)
		{
			base._transform.position = owner._position;
			base._transform.rotation = owner._rotation;
			if (!owner.isActSpecialAction)
			{
				bulletObject.ForceBreak();
			}
			if (owner.spearCtrl.IsBarrierBulletDelete())
			{
				bulletObject.ForceBreak();
				owner.spearCtrl.DisableBarrierBulletDelete();
			}
		}
		if (isRegistered && owner == null)
		{
			bulletObject.ForceBreak();
		}
	}
}
