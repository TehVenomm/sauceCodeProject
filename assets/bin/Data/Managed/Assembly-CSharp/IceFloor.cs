using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class IceFloor : MonoBehaviour
{
	private enum STATE
	{
		UPDATE,
		WAIT_FOR_END
	}

	private Rigidbody _rigidbody;

	private Collider _collider;

	private EffectCtrl _effect;

	private float timer;

	private List<Player> hittingPlayerList = new List<Player>(4);

	private STATE state;

	public float duration
	{
		get;
		set;
	}

	public IceFloor()
		: this()
	{
	}

	private void Awake()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		_rigidbody = this.GetComponent<Rigidbody>();
		_rigidbody.set_useGravity(false);
		_rigidbody.set_isKinematic(true);
		_collider = this.GetComponentInChildren<Collider>();
		if (_collider == null)
		{
			CapsuleCollider val = this.get_gameObject().AddComponent<CapsuleCollider>();
			val.set_center(new Vector3(0f, 0f, 0f));
			val.set_direction(2);
			val.set_isTrigger(true);
			_collider = val;
		}
	}

	public void SetCollider(float radius, float height = 2f)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		CapsuleCollider val = _collider as CapsuleCollider;
		if (val == null)
		{
			val = (_collider = this.get_gameObject().AddComponent<CapsuleCollider>());
		}
		val.set_center(new Vector3(0f, 0f, 0f));
		val.set_direction(2);
		val.set_radius(radius);
		val.set_height(height);
		val.set_isTrigger(true);
	}

	public void SetEffect(Transform eff)
	{
		_effect = eff.GetComponent<EffectCtrl>();
	}

	private void Update()
	{
		switch (state)
		{
		case STATE.UPDATE:
			timer += Time.get_deltaTime();
			if (timer > duration)
			{
				if (_effect != null)
				{
					EffectManager.ReleaseEffect(_effect.get_gameObject());
				}
				state = STATE.WAIT_FOR_END;
			}
			break;
		case STATE.WAIT_FOR_END:
			if (_effect == null)
			{
				Object.Destroy(this.get_gameObject());
			}
			break;
		}
	}

	private void OnDestroy()
	{
		for (int i = 0; i < hittingPlayerList.Count; i++)
		{
			hittingPlayerList[i].OnHitExitIceFloor(this.get_gameObject());
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		StageObject componentInParent = collider.get_gameObject().GetComponentInParent<StageObject>();
		if (!(componentInParent == null))
		{
			Player player = componentInParent as Player;
			if (!(player == null))
			{
				hittingPlayerList.Add(player);
				player.OnHitEnterIceFloor(this.get_gameObject());
			}
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		StageObject componentInParent = collider.get_gameObject().GetComponentInParent<StageObject>();
		if (!(componentInParent == null))
		{
			Player player = componentInParent as Player;
			if (!(player == null))
			{
				hittingPlayerList.Remove(player);
				player.OnHitExitIceFloor(this.get_gameObject());
			}
		}
	}
}
