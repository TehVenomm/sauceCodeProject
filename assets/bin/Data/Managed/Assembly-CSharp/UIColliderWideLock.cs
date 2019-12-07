using UnityEngine;

public class UIColliderWideLock : MonoBehaviour
{
	private BoxCollider attach_collider;

	private float sizex;

	private void Awake()
	{
		attach_collider = GetComponent<BoxCollider>();
		if (attach_collider != null)
		{
			sizex = attach_collider.size.x;
		}
		else
		{
			Object.Destroy(this);
		}
	}

	private void LateUpdate()
	{
		if (sizex != attach_collider.size.x)
		{
			Vector3 size = attach_collider.size;
			size.x = sizex;
			attach_collider.size = size;
		}
	}
}
