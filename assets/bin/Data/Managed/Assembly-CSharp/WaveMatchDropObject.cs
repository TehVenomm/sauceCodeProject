using UnityEngine;

public class WaveMatchDropObject
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

	public WaveMatchDropObject()
		: this()
	{
	}

	public int GetId()
	{
		return id;
	}

	public void Initialize(int _id, Vector3 _pos, Vector3 _offset, float _sec, WaveMatchDropTable.WaveMatchDropData _data)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		this.set_name("WM_Drop:" + id.ToString());
		id = _id;
		basePos = _pos;
		targetPos = _pos + _offset;
		lifeSpan = _sec;
		dropTime = 0f;
		tableData = _data;
		cachedStageObjMgr = MonoBehaviourSingleton<StageObjectManager>.I;
		cachedTransform = this.get_transform();
		cachedTransform.set_localPosition(basePos);
		cachedCollider = this.get_gameObject().AddComponent<SphereCollider>();
		cachedCollider.set_radius(kColliderSize);
		cachedCollider.set_isTrigger(true);
		cachedCollider.set_enabled(false);
		this.get_gameObject().set_layer(31);
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
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		dropTime += Time.get_deltaTime();
		float num = dropTime / kDropTime;
		if (num > 1f)
		{
			num = 1f;
		}
		cachedTransform.set_localPosition(Vector3.Lerp(basePos, targetPos, num));
		if (num >= 1f)
		{
			cachedCollider.set_enabled(true);
			state = eState.Idle;
		}
	}

	private void _UpdateIdle()
	{
		lifeSpan -= Time.get_deltaTime();
		if (lifeSpan <= 0f)
		{
			cachedStageObjMgr.RemoveWaveMatchDropObject(id);
		}
	}

	public void OnDisappear()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		state = eState.None;
		if (cachedCollider != null)
		{
			cachedCollider.set_enabled(false);
		}
		if (this.get_gameObject() != null)
		{
			Object.Destroy(this.get_gameObject());
		}
	}

	public virtual void OnPicked(Self self)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (!tableData.getEffect.IsNullOrWhiteSpace())
		{
			EffectManager.OneShot(tableData.getEffect, self._position, Quaternion.get_identity(), false);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		int layer = collider.get_gameObject().get_layer();
		if (((1 << layer) & ignoreLayerMask) <= 0 && (layer != 8 || !(collider.get_gameObject().GetComponent<DangerRader>() != null)))
		{
			Self component = collider.get_gameObject().GetComponent<Self>();
			if (!(component == null))
			{
				OnPicked(component);
				cachedStageObjMgr.RemoveWaveMatchDropObject(id);
				if (component.playerSender != null)
				{
					component.playerSender.OnPickedWaveMatchDropObject(id, tableData.id);
				}
			}
		}
	}
}
