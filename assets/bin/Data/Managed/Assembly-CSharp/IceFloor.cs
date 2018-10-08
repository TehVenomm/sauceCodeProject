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

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.useGravity = false;
		_rigidbody.isKinematic = true;
		_collider = GetComponentInChildren<Collider>();
		if ((Object)_collider == (Object)null)
		{
			CapsuleCollider capsuleCollider = base.gameObject.AddComponent<CapsuleCollider>();
			capsuleCollider.center = new Vector3(0f, 0f, 0f);
			capsuleCollider.direction = 2;
			capsuleCollider.isTrigger = true;
			_collider = capsuleCollider;
		}
	}

	public void SetCollider(float radius, float height = 2f)
	{
		CapsuleCollider capsuleCollider = _collider as CapsuleCollider;
		if ((Object)capsuleCollider == (Object)null)
		{
			capsuleCollider = (CapsuleCollider)(_collider = base.gameObject.AddComponent<CapsuleCollider>());
		}
		capsuleCollider.center = new Vector3(0f, 0f, 0f);
		capsuleCollider.direction = 2;
		capsuleCollider.radius = radius;
		capsuleCollider.height = height;
		capsuleCollider.isTrigger = true;
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
			timer += Time.deltaTime;
			if (timer > duration)
			{
				if ((Object)_effect != (Object)null)
				{
					EffectManager.ReleaseEffect(_effect.gameObject, true, false);
				}
				state = STATE.WAIT_FOR_END;
			}
			break;
		case STATE.WAIT_FOR_END:
			if ((Object)_effect == (Object)null)
			{
				Object.Destroy(base.gameObject);
			}
			break;
		}
	}

	private void OnDestroy()
	{
		for (int i = 0; i < hittingPlayerList.Count; i++)
		{
			hittingPlayerList[i].OnHitExitIceFloor(base.gameObject);
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		StageObject componentInParent = collider.gameObject.GetComponentInParent<StageObject>();
		if (!((Object)componentInParent == (Object)null))
		{
			Player player = componentInParent as Player;
			if (!((Object)player == (Object)null))
			{
				hittingPlayerList.Add(player);
				player.OnHitEnterIceFloor(base.gameObject);
			}
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		StageObject componentInParent = collider.gameObject.GetComponentInParent<StageObject>();
		if (!((Object)componentInParent == (Object)null))
		{
			Player player = componentInParent as Player;
			if (!((Object)player == (Object)null))
			{
				hittingPlayerList.Remove(player);
				player.OnHitExitIceFloor(base.gameObject);
			}
		}
	}
}
