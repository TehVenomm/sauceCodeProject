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

	private void Start()
	{
		_transform = base.transform;
		meshRenderer = GetComponent<MeshRenderer>();
		if (meshRenderer != null)
		{
			meshRenderer.enabled = false;
		}
	}

	private void Update()
	{
		if (meshRenderer == null || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (!(self == null))
		{
			float magnitude = (self._transform.position - _transform.position).magnitude;
			if (magnitude <= radius - hideLength)
			{
				meshRenderer.enabled = false;
			}
			else if (magnitude >= radius - showLength)
			{
				meshRenderer.enabled = true;
			}
		}
	}
}
