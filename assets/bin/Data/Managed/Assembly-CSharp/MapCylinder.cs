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
		if ((Object)meshRenderer != (Object)null)
		{
			meshRenderer.enabled = false;
		}
	}

	private void Update()
	{
		if (!((Object)meshRenderer == (Object)null) && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!((Object)self == (Object)null))
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
}
