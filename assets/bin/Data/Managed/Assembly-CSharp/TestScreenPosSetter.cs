using UnityEngine;

public class TestScreenPosSetter : MonoBehaviour
{
	public PuniController punicon;

	public TestScreenPosSetter()
		: this()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Vector3.get_zero();
	}
}
