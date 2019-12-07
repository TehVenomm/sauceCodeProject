using UnityEngine;

public class TestScreenPosSetter : MonoBehaviour
{
	public PuniController punicon;

	private void Start()
	{
	}

	private void Update()
	{
		if (IsTouchOn())
		{
			punicon.SetStartPosition(GetTouchScreenPos());
		}
		if (IsTouchOff())
		{
			punicon.Reset();
		}
		if (IsTouch())
		{
			punicon.SetEndPosition(GetTouchScreenPos());
		}
	}

	private bool IsTouchOn()
	{
		return false;
	}

	private bool IsTouchOff()
	{
		return false;
	}

	private bool IsTouch()
	{
		return false;
	}

	private Vector3 GetTouchScreenPos()
	{
		return Vector3.zero;
	}
}
