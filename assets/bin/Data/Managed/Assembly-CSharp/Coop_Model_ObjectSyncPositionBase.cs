using UnityEngine;

public class Coop_Model_ObjectSyncPositionBase : Coop_Model_ObjectBase
{
	public Vector3 pos = Vector3.get_zero();

	public float dir;

	public override Vector3 GetObjectPosition()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return pos;
	}

	public override bool IsHaveObjectPosition()
	{
		return true;
	}

	public void SetSyncPosition(StageObject target)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		pos = target._position;
		Quaternion rotation = target._rotation;
		Vector3 eulerAngles = rotation.get_eulerAngles();
		dir = eulerAngles.y;
	}
}
