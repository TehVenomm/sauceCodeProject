using UnityEngine;

public class UIZOffset
{
	private Material _mat;

	[SerializeField]
	private int zOffset;

	private int sourceQueue;

	public UIZOffset()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		MeshRenderer component = this.GetComponent<MeshRenderer>();
		_mat = new Material(component.get_sharedMaterial());
		sourceQueue = _mat.get_renderQueue();
		_mat.set_renderQueue(sourceQueue + zOffset);
		component.set_material(_mat);
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		if (_mat != null)
		{
			Object.Destroy(_mat);
		}
	}
}
