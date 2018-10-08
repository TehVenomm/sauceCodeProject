using UnityEngine;

public class UIZOffset : MonoBehaviour
{
	private Material _mat;

	[SerializeField]
	private int zOffset;

	private int sourceQueue;

	private void Awake()
	{
		MeshRenderer component = GetComponent<MeshRenderer>();
		_mat = new Material(component.sharedMaterial);
		sourceQueue = _mat.renderQueue;
		_mat.renderQueue = sourceQueue + zOffset;
		component.material = _mat;
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		if ((Object)_mat != (Object)null)
		{
			Object.Destroy(_mat);
		}
	}
}
