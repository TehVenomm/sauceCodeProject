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

	private Player owner;

	private STATE state;

	private Vector3 startPos = Vector3.zero;

	private float maxDistance;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		maxDistance = bullet.dataSnatch.maxDistance;
		startPos = pos;
		SetState(STATE.FORWARD);
	}

	public override void Update()
	{
		base.timeCount += Time.deltaTime;
		switch (state)
		{
		case STATE.FORWARD:
		{
			float velocity = Mathf.Max(0f, base.initialVelocity - base.initialVelocity * base.timeCount * base.timeCount);
			SetVelocity(velocity);
			if ((base._transform.position - startPos).magnitude > maxDistance)
			{
				SetVelocity(0f);
				SetState(STATE.MISS);
			}
			break;
		}
		case STATE.MISS:
			if ((Object)base._collider != (Object)null)
			{
				base._collider.enabled = false;
			}
			owner.snatchCtrl.OnReach();
			SetState(STATE.DESTROY);
			break;
		case STATE.SNATCH:
			if ((Object)base._collider != (Object)null)
			{
				base._collider.enabled = false;
			}
			SetVelocity(0f);
			SetState(STATE.DESTROY);
			break;
		case STATE.DESTROY:
			bulletObject.OnDestroy();
			SetState(STATE.NONE);
			break;
		}
		Vector3 forward = Vector3.forward;
		forward = base._transform.rotation * forward;
		forward *= base.speed;
		base._rigidbody.velocity = forward;
	}

	public override bool IsHit(Collider collider)
	{
		int layer = collider.gameObject.layer;
		switch (layer)
		{
		case 31:
			return false;
		case 8:
		{
			DangerRader component = collider.gameObject.GetComponent<DangerRader>();
			if ((Object)component != (Object)null)
			{
				return false;
			}
			break;
		}
		}
		if (layer == 11 || layer == 10)
		{
			EnemyColliderSettings component2 = collider.gameObject.GetComponent<EnemyColliderSettings>();
			if ((Object)component2 != (Object)null && component2.targetCollider.GetInstanceID() == collider.GetInstanceID())
			{
				return false;
			}
		}
		return true;
	}

	public override void OnHit(Collider collider)
	{
		int layer = collider.gameObject.layer;
		if (layer != 11 && layer != 10)
		{
			SetState(STATE.MISS);
		}
		else
		{
			AttackRestraintObject component = collider.gameObject.GetComponent<AttackRestraintObject>();
			if ((Object)component != (Object)null)
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

	public override void RegisterFromObject(StageObject obj)
	{
		owner = (obj as Player);
		if ((Object)owner != (Object)null)
		{
			owner.snatchCtrl.SetSnatchBulletTrans(base._transform);
		}
	}

	private void SetState(STATE state)
	{
		this.state = state;
	}
}
