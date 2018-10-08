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
		fixedTime += Time.get_fixedDeltaTime();
	}

	protected override void OnTriggerEnter(Collider collider)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		hitLayer = collider.get_gameObject().get_layer();
		if (((1 << hitLayer) & ignoreLayerMask) == 0)
		{
			if (hitLayer == 11)
			{
				hitEnemy = collider.get_gameObject().GetComponent<Enemy>();
			}
			else if (hitLayer == 31)
			{
				EscapePointObject component = collider.get_gameObject().GetComponent<EscapePointObject>();
				if (component != null)
				{
					return;
				}
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
