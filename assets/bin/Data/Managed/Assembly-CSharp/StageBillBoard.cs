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
		cameraPos.y = _transform.position.y;
		base.transform.LookAt(cameraPos);
	}
}
