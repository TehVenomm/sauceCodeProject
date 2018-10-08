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
			float num = autoBaseY;
			Vector3 lossyScale = _transform.lossyScale;
			autoBaseY = num * lossyScale.y;
		}
		up = false;
	}

	private void Start()
	{
	}

	public bool UpdateStamp(float base_y)
	{
		Vector3 position = _transform.position;
		float num = position.y - base_y;
		Vector3 vector = _transform.localToWorldMatrix.MultiplyPoint(offset);
		num = vector.y - base_y;
		if (autoBaseY == -1f)
		{
			autoBaseY = num;
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
