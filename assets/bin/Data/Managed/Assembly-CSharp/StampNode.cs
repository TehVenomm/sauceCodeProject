using System;
using UnityEngine;

public class StampNode : MonoBehaviour
{
	[Serializable]
	public class StampTrigger
	{
		public int eventID;

		public int StampInfoID;
	}

	public Vector3 offset;

	public StampTrigger[] triggers;

	public int autoStampInfoID;

	public float autoBaseY = -1f;

	private bool up;

	public Transform _transform
	{
		get;
		private set;
	}

	public Vector3 scaledeOffset
	{
		get;
		private set;
	}

	private void Awake()
	{
		_transform = base.transform;
		scaledeOffset = offset.Mul(_transform.lossyScale);
		if (autoBaseY != -1f)
		{
			autoBaseY *= _transform.lossyScale.y;
		}
		up = false;
	}

	private void Start()
	{
	}

	public bool UpdateStamp(float base_y)
	{
		float num = _transform.position.y - base_y;
		num = _transform.localToWorldMatrix.MultiplyPoint(offset).y - base_y;
		if (autoBaseY == -1f)
		{
			autoBaseY = num + 0f;
		}
		if (!up)
		{
			if (num > autoBaseY)
			{
				up = true;
			}
		}
		else if (num < autoBaseY)
		{
			up = false;
			return true;
		}
		return false;
	}
}
