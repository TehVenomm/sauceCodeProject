using UnityEngine;

public class BulletControllerSnatch : BulletControllerBase
{
	private enum STATE
	{
		NONE,
		FORWARD,
		MISS,
		SNATCH,
		DESTROY
	}

	private BulletObject bullet;

	private Player owner;

	private STATE state;

	private Vector3 startPos = Vector3.get_zero();

	private float maxDistance;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		maxDistance = bullet.dataSnatch.maxDistance;
		startPos = pos;
		SetState(STATE.FORWARD);
	}

	public override void Update()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		base.timeCount += Time.get_deltaTime();
		switch (state)
		{
		case STATE.FORWARD:
		{
			float velocity = Mathf.Max(0f, base.initialVelocity - base.initialVelocity * base.timeCount * base.timeCount);
			SetVelocity(velocity);
			Vector3 val = base._transform.get_position() - startPos;
			if (val.get_magnitude() > maxDistance)
			{
				SetVelocity(0f);
				SetState(STATE.MISS);
			}
			break;
		}
		case STATE.MISS:
			if (base._collider != null)
			{
				base._collider.set_enabled(false);
			}
			owner.snatchCtrl.OnReach();
			SetState(STATE.DESTROY);
			break;
		case STATE.SNATCH:
			if (base._collider != null)
			{
				base._collider.set_enabled(false);
			}
			SetVelocity(0f);
			SetState(STATE.DESTROY);
			break;
		case STATE.DESTROY:
			bullet.OnDestroy();
			SetState(STATE.NONE);
			break;
		}
		Vector3 forward = Vector3.get_forward();
		forward = base._transform.get_rotation() * forward;
		forward *= base.speed;
		base._rigidbody.set_velocity(forward);
	}

	public override bool IsHit(Collider collider)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		int layer = collider.get_gameObject().get_layer();
		switch (layer)
		{
		case 31:
			return false;
		case 8:
		{
			DangerRader component = collider.get_gameObject().GetComponent<DangerRader>();
			if (component != null)
			{
				return false;
			}
			break;
		}
		}
		if (layer == 11 || layer == 10)
		{
			EnemyColliderSettings component2 = collider.get_gameObject().GetComponent<EnemyColliderSettings>();
			if (component2 != null && component2.targetCollider.GetInstanceID() == collider.GetInstanceID())
			{
				return false;
			}
		}
		return true;
	}

	public override void OnHit(Collider collider)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		int layer = collider.get_gameObject().get_layer();
		if (layer != 11 && layer != 10)
		{
			SetState(STATE.MISS);
		}
		else
		{
			AttackRestraintObject component = collider.get_gameObject().GetComponent<AttackRestraintObject>();
			if (component != null)
			{
				SetState(STATE.MISS);
			}
			else
			{
				SetState(STATE.SNATCH);
			}
		}
	}

	public override void OnHitStay(Collider collider)
	{
		OnHit(collider);
	}

	public void SetBulletObject(BulletObject bullet)
	{
		this.bullet = bullet;
	}

	public void SetFromObject(StageObject stageObject)
	{
		owner = (stageObject as Player);
		if (owner != null)
		{
			owner.snatchCtrl.SetSnatchBulletTrans(base._transform);
		}
	}

	private void SetState(STATE state)
	{
		this.state = state;
	}
}
