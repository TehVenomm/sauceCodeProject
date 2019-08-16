using UnityEngine;

public class MapCylinder : MonoBehaviour
{
	[Tooltip("半径")]
	public float radius = 40f;

	[Tooltip("表示状態にする距離")]
	public float showLength = 3f;

	[Tooltip("非表示状態にする距離")]
	public float hideLength = 5f;

	public Transform _transform
	{
		get;
		protected set;
	}

	public MeshRenderer meshRenderer
	{
		get;
		protected set;
	}

	public MapCylinder()
		: this()
	{
	}

	private void Start()
	{
		_transform = this.get_transform();
		meshRenderer = this.GetComponent<MeshRenderer>();
		if (meshRenderer != null)
		{
			meshRenderer.set_enabled(false);
		}
	}

	private void Update()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (meshRenderer == null || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (!(self == null))
		{
			Vector3 val = self._transform.get_position() - _transform.get_position();
			float magnitude = val.get_magnitude();
			if (magnitude <= radius - hideLength)
			{
				meshRenderer.set_enabled(false);
			}
			else if (magnitude >= radius - showLength)
			{
				meshRenderer.set_enabled(true);
			}
		}
	}
}
