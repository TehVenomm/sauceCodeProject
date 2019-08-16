using UnityEngine;

public class UIColliderWideLock : MonoBehaviour
{
	private BoxCollider attach_collider;

	private float sizex;

	public UIColliderWideLock()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		attach_collider = this.GetComponent<BoxCollider>();
		if (attach_collider != null)
		{
			Vector3 size = attach_collider.get_size();
			sizex = size.x;
		}
		else
		{
			Object.Destroy(this);
		}
	}

	private void LateUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		float num = sizex;
		Vector3 size = attach_collider.get_size();
		if (num != size.x)
		{
			Vector3 size2 = attach_collider.get_size();
			size2.x = sizex;
			attach_collider.set_size(size2);
		}
	}
}
