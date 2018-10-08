using UnityEngine;

public class StageBillBoard : MonoBehaviour
{
	private Vector3 cameraPos = Vector3.zero;

	private Transform _transform;

	private void Start()
	{
		_transform = base.transform;
	}

	private void Update()
	{
		cameraPos = Camera.main.transform.position;
		ref Vector3 reference = ref cameraPos;
		Vector3 position = _transform.position;
		reference.y = position.y;
		base.transform.LookAt(cameraPos);
	}
}
