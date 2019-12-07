using UnityEngine;

public class Coop_Model_ObjectSyncPositionBase : Coop_Model_ObjectBase
{
	public Vector3 pos = Vector3.zero;

	public float dir;

	public override Vector3 GetObjectPosition()
	{
		return pos;
	}

	public override bool IsHaveObjectPosition()
	{
		return true;
	}

	public void SetSyncPosition(StageObject target)
	{
		pos = target._position;
		dir = target._rotation.eulerAngles.y;
	}
}
