using UnityEngine;

public class UIColliderWideLock : MonoBehaviour
{
	private BoxCollider attach_collider;

	private float sizex;

	private void Awake()
	{
		attach_collider = GetComponent<BoxCollider>();
		if ((Object)attach_collider != (Object)null)
		{
			Vector3 size = attach_collider.size;
			sizex = size.x;
		}
		else
		{
			Object.Destroy(this);
		}
	}

	private void LateUpdate()
	{
		float num = sizex;
		Vector3 size = attach_collider.size;
		if (num != size.x)
		{
			Vector3 size2 = attach_collider.size;
			size2.x = sizex;
			attach_collider.size = size2;
		}
	}
}
