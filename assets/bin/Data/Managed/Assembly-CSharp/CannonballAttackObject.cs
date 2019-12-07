using UnityEngine;

public class CannonballAttackObject : AttackColliderObject
{
	public int ignoreLayerMask;

	private AttackCannonball owner;

	private float fixedTime;

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

	public Enemy hitEnemy
	{
		get;
		private set;
	}

	public void SetIgnoreLayerMask(int mask)
	{
		ignoreLayerMask = mask;
	}

	public void SetOwner(AttackCannonball owner)
	{
		this.owner = owner;
		fixedTime = 0f;
	}

	public void ResetHit()
	{
		isHit = false;
		ActivateOwnCollider();
	}

	public override float GetTime()
	{
		return fixedTime;
	}

	private void FixedUpdate()
	{
		fixedTime += Time.fixedDeltaTime;
	}

	protected override void OnTriggerEnter(Collider collider)
	{
		hitLayer = collider.gameObject.layer;
		if (((1 << hitLayer) & ignoreLayerMask) == 0)
		{
			if (hitLayer == 11)
			{
				hitEnemy = collider.gameObject.GetComponent<Enemy>();
			}
			else if (hitLayer == 31 && (collider.gameObject.GetComponent<EscapePointObject>() != null || collider.gameObject.GetComponent<BarrierBulletObject>() != null))
			{
				return;
			}
			isHit = true;
			base.OnTriggerEnter(collider);
			DeactivateOwnCollider();
			Destroy();
			if (owner != null)
			{
				owner.OnHit();
			}
		}
	}
}
