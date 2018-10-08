using UnityEngine;

public class WayPoint
{
	public WayPoint[] links;

	public string waitAnimStateName;

	public WayPoint()
		: this()
	{
	}

	public Vector3 GetPosInCollider()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		Transform val = this.get_transform();
		Collider component = this.GetComponent<Collider>();
		if (component is SphereCollider)
		{
			return val.get_position() + Quaternion.Euler(0f, (float)Random.Range(0, 360), 0f) * Vector3.get_forward() * Random.Range(0f, (component as SphereCollider).get_radius());
		}
		if (component is BoxCollider)
		{
			Vector3 size = (component as BoxCollider).get_size();
			size.x = Utility.SymmetryRandom(size.x * 0.5f);
			size.y = 0f;
			size.z = Utility.SymmetryRandom(size.z * 0.5f);
			return val.TransformPoint(size);
		}
		return val.get_position();
	}
}
