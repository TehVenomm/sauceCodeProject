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
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, skillParam, pos, rot);
		bulletData = bullet;
		this.get_gameObject().set_name(OBJECT_NAME);
		this.get_gameObject().set_layer(31);
		ignoreLayerMask |= 41984;
		ignoreLayerMask |= 20480;
		if (!bullet.data.isObjectHitDelete)
		{
			ignoreLayerMask |= 2490880;
		}
	}

	public override void RegisterFromObject(StageObject obj)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		base.RegisterFromObject(obj);
		owner = (obj as Player);
		base._transform.set_position(obj._position);
		base._transform.set_rotation(obj._rotation);
		isRegistered = true;
	}

	public override bool IsHit(Collider collider)
	{
		int layer = collider.get_gameObject().get_layer();
		if (((1 << layer) & ignoreLayerMask) > 0)
		{
			return false;
		}
		if (layer == 8 && collider.get_gameObject().GetComponent<DangerRader>() != null)
		{
			return false;
		}
		return true;
	}

	public override void Update()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		base.timeCount += Time.get_deltaTime();
		if (owner != null)
		{
			base._transform.set_position(owner._position);
			base._transform.set_rotation(owner._rotation);
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
