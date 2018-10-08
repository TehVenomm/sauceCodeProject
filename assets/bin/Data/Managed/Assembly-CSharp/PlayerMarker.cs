using UnityEngine;

public class PlayerMarker
{
	[SerializeField]
	private float speed;

	[SerializeField]
	private float alphaSpeed;

	private float angle;

	private Transform _transform;

	private float baseAngle = -45f;

	private float alpha;

	private Material _mat;

	private Transform camTransform;

	public PlayerMarker()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		_transform = this.get_transform();
		MeshRenderer component = this.GetComponent<MeshRenderer>();
		if (component != null)
		{
			_mat = component.get_material();
		}
	}

	private void Update()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		alpha += Time.get_deltaTime() * alphaSpeed;
		alpha = Mathf.Clamp01(alpha);
		_mat.SetFloat("_Alpha", alpha);
		angle += Time.get_deltaTime() * speed;
		angle = Mathf.Repeat(angle, 360f);
		if (null != camTransform)
		{
			Vector3 position = camTransform.get_position();
			Vector3 position2 = _transform.get_position();
			position.x = position2.x;
			_transform.LookAt(position);
			_transform.set_localRotation(_transform.get_localRotation() * Quaternion.AngleAxis(angle, Vector3.get_up()));
		}
		else
		{
			_transform.set_localRotation(Quaternion.AngleAxis(baseAngle, Vector3.get_right()) * Quaternion.AngleAxis(angle, Vector3.get_up()));
		}
	}

	public void SetWorldMode(bool enable)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (enable)
		{
			baseAngle = 45f;
			float num = 10f;
			_transform.set_localScale(new Vector3(num, num, num));
		}
		else
		{
			baseAngle = -45f;
			_transform.set_localScale(Vector3.get_one());
		}
	}

	public void SetCamera(Transform c)
	{
		camTransform = c;
	}
}
