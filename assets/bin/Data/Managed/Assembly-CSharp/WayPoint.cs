using UnityEngine;

public class WayPoint : MonoBehaviour
{
	public WayPoint[] links;

	public string waitAnimStateName;

	public Vector3 GetPosInCollider()
	{
		Transform transform = base.transform;
		Collider component = GetComponent<Collider>();
		if (component is SphereCollider)
		{
			return transform.position + Quaternion.Euler(0f, Random.Range(0, 360), 0f) * Vector3.forward * Random.Range(0f, (component as SphereCollider).radius);
		}
		if (component is BoxCollider)
		{
			Vector3 size = (component as BoxCollider).size;
			size.x = Utility.SymmetryRandom(size.x * 0.5f);
			size.y = 0f;
			size.z = Utility.SymmetryRandom(size.z * 0.5f);
			return transform.TransformPoint(size);
		}
		return transform.position;
	}
}
