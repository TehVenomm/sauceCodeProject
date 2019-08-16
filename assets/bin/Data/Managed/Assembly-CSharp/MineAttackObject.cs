using UnityEngine;

public class MineAttackObject : AttackColliderObject
{
	public int ignoreLayerMask;

	public bool isHit
	{
		get;
		private set;
	}

	public int hitLayer
	{
		get;
		private set;
	}

	public Player hitPlayer
	{
		get;
		private set;
	}

	public void SetIgnoreLayerMask(int mask)
	{
		ignoreLayerMask = mask;
	}

	public void ResetHit()
	{
		isHit = false;
		ActivateOwnCollider();
	}

	protected override void OnTriggerEnter(Collider collider)
	{
		hitLayer = collider.get_gameObject().get_layer();
		if (((1 << hitLayer) & ignoreLayerMask) > 0)
		{
			return;
		}
		if (hitLayer == 8)
		{
			hitPlayer = collider.get_gameObject().GetComponent<Player>();
			if (collider.get_gameObject().GetComponent<DangerRader>() != null)
			{
				return;
			}
		}
		HealAttackObject component = collider.get_gameObject().GetComponent<HealAttackObject>();
		if (!(component != null))
		{
			isHit = true;
			DeactivateOwnCollider();
			base.OnTriggerEnter(collider);
		}
	}
}
