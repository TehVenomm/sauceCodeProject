using UnityEngine;

public class PlayerMarker : MonoBehaviour
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

	private void Awake()
	{
		_transform = base.transform;
		MeshRenderer component = GetComponent<MeshRenderer>();
		if (component != null)
		{
			_mat = component.material;
		}
	}

	private void Update()
	{
		alpha += Time.deltaTime * alphaSpeed;
		alpha = Mathf.Clamp01(alpha);
		_mat.SetFloat("_Alpha", alpha);
		angle += Time.deltaTime * speed;
		angle = Mathf.Repeat(angle, 360f);
		if (null != camTransform)
		{
			Vector3 position = camTransform.position;
			position.x = _transform.position.x;
			_transform.LookAt(position);
			_transform.localRotation *= Quaternion.AngleAxis(angle, Vector3.up);
		}
		else
		{
			_transform.localRotation = Quaternion.AngleAxis(baseAngle, Vector3.right) * Quaternion.AngleAxis(angle, Vector3.up);
		}
	}

	public void SetWorldMode(bool enable)
	{
		if (enable)
		{
			baseAngle = 45f;
			float num = 10f;
			_transform.localScale = new Vector3(num, num, num);
		}
		else
		{
			baseAngle = -45f;
			_transform.localScale = Vector3.one;
		}
	}

	public void SetCamera(Transform c)
	{
		camTransform = c;
	}
}
