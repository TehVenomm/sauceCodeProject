using UnityEngine;

[ExecuteInEditMode]
public class FixedViewQuad
{
	public Camera targetCamera;

	public float planeZ = 100f;

	private Transform _transform;

	private MeshRenderer _meshRenderer;

	public FixedViewQuad()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		_transform = this.get_transform();
		_meshRenderer = this.GetComponent<MeshRenderer>();
		if (targetCamera == null)
		{
			targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		}
	}

	private void LateUpdate()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		if (_meshRenderer != null)
		{
			_meshRenderer.set_enabled(targetCamera != null);
		}
		float num = (float)Screen.get_width() * 0.5f;
		float num2 = (float)Screen.get_height() * 0.5f;
		Vector3 val = targetCamera.ScreenToWorldPoint(new Vector3(num, num2, planeZ));
		Vector3 val2 = default(Vector3);
		if (num < num2)
		{
			val2._002Ector(num, -1f, planeZ);
		}
		else
		{
			val2._002Ector(-1f, num2, planeZ);
		}
		Vector3 val3 = targetCamera.ScreenToWorldPoint(val2) - val;
		float num3 = val3.get_magnitude() * 2f;
		_transform.set_localScale(new Vector3(num3, num3, 1f));
		_transform.set_position(val);
		_transform.set_rotation(targetCamera.get_transform().get_rotation());
	}
}
