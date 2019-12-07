using UnityEngine;

public class LoungePlayerCreateInfo : MonoBehaviour
{
	public Vector3 lookPoint;

	public SphereCollider createArea;

	public Vector3 GetCreatePosition()
	{
		if (createArea == null)
		{
			return Vector3.zero;
		}
		Transform transform = createArea.transform;
		Quaternion rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
		float d = Random.Range(0f, createArea.radius);
		return transform.position + rotation * Vector3.forward * d;
	}
}
