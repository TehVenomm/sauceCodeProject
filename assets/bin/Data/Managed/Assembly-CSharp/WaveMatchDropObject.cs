using UnityEngine;

public class WaveMatchDropObject : MonoBehaviour
{
	private enum eState
	{
		None,
		Drop,
		Idle
	}

	private const string kObjectName = "WM_Drop:";

	private readonly float kColliderSize = 1.2f;

	private readonly float kDropTime = 0.5f;

	private int id;

	private StageObjectManager cachedStageObjMgr;

	private Transform cachedTransform;

	private SphereCollider cachedCollider;

	private int ignoreLayerMask;

	private float lifeSpan;

	private eState state;

	private float dropTime;

	private Vector3 basePos;

	private Vector3 targetPos;

	protected WaveMatchDropTable.WaveMatchDropData tableData;

	public int GetId()
	{
		return id;
	}

	public void Initialize(int _id, Vector3 _pos, Vector3 _offset, float _sec, WaveMatchDropTable.WaveMatchDropData _data)
	{
		base.name = "WM_Drop:" + id.ToString();
		id = _id;
		basePos = _pos;
		targetPos = _pos + _offset;
		lifeSpan = _sec;
		dropTime = 0f;
		tableData = _data;
		cachedStageObjMgr = MonoBehaviourSingleton<StageObjectManager>.I;
		cachedTransform = base.transform;
		cachedTransform.localPosition = basePos;
		cachedCollider = base.gameObject.AddComponent<SphereCollider>();
		cachedCollider.radius = kColliderSize;
		cachedCollider.isTrigger = true;
		cachedCollider.enabled = false;
		base.gameObject.layer = 31;
		ignoreLayerMask |= 41984;
		ignoreLayerMask |= 20480;
		ignoreLayerMask |= 2490880;
		state = eState.Drop;
		MonoBehaviourSingleton<StageObjectManager>.I.AddWaveMatchDropObject(this);
	}

	private void Update()
	{
		switch (state)
		{
		case eState.Drop:
			_UpdateDrop();
			break;
		case eState.Idle:
			_UpdateIdle();
			break;
		}
	}

	private void _UpdateDrop()
	{
		dropTime += Time.deltaTime;
		float num = dropTime / kDropTime;
		if (num > 1f)
		{
			num = 1f;
		}
		cachedTransform.localPosition = Vector3.Lerp(basePos, targetPos, num);
		if (num >= 1f)
		{
			cachedCollider.enabled = true;
			state = eState.Idle;
		}
	}

	private void _UpdateIdle()
	{
		lifeSpan -= Time.deltaTime;
		if (lifeSpan <= 0f)
		{
			cachedStageObjMgr.RemoveWaveMatchDropObject(id);
		}
	}

	public void OnDisappear()
	{
		state = eState.None;
		if ((Object)cachedCollider != (Object)null)
		{
			cachedCollider.enabled = false;
		}
		if ((Object)base.gameObject != (Object)null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	public virtual void OnPicked(Self self)
	{
		if (!tableData.getEffect.IsNullOrWhiteSpace())
		{
			EffectManager.OneShot(tableData.getEffect, self._position, Quaternion.identity, false);
		}
		if (tableData.getSE != 0)
		{
			SoundManager.PlayOneShotSE(tableData.getSE, self._position);
		}
		int i = 0;
		for (int count = tableData.buffTableIds.Count; i < count; i++)
		{
			self.StartBuffByBuffTableId(tableData.buffTableIds[i], null);
		}
	}

	public virtual void OnReceiveEffect()
	{
	}

	private void OnTriggerEnter(Collider collider)
	{
		int layer = collider.gameObject.layer;
		if (((1 << layer) & ignoreLayerMask) <= 0 && (layer != 8 || !((Object)collider.gameObject.GetComponent<DangerRader>() != (Object)null)))
		{
			Self component = collider.gameObject.GetComponent<Self>();
			if (!((Object)component == (Object)null))
			{
				OnPicked(component);
				cachedStageObjMgr.RemoveWaveMatchDropObject(id);
				if ((Object)component.playerSender != (Object)null)
				{
					component.playerSender.OnPickedWaveMatchDropObject(id, tableData.id);
				}
			}
		}
	}
}
