using UnityEngine;

public class Coop_Model_ObjectBase : Coop_Model_Base
{
	private float _receiveTime;

	public Coop_Model_ObjectBase()
	{
		_receiveTime = 0f;
	}

	public void SetReceiveTime(float time)
	{
		_receiveTime = time;
	}

	public float GetReceiveTime()
	{
		return _receiveTime;
	}

	public virtual Vector3 GetObjectPosition()
	{
		return Vector3.zero;
	}

	public virtual bool IsHaveObjectPosition()
	{
		return false;
	}

	public virtual bool IsForceHandleBefore(StageObject owner)
	{
		return false;
	}

	public virtual bool IsHandleable(StageObject owner)
	{
		return true;
	}
}
