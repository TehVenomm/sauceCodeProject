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

	public StampNode()
		: this()
	{
	}

	private void Awake()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		_transform = this.get_transform();
		scaledeOffset = offset.Mul(_transform.get_lossyScale());
		if (autoBaseY != -1f)
		{
			float num = autoBaseY;
			Vector3 lossyScale = _transform.get_lossyScale();
			autoBaseY = num * lossyScale.y;
		}
		up = false;
	}

	private void Start()
	{
	}

	public bool UpdateStamp(float base_y)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = _transform.get_position();
		float num = position.y - base_y;
		Matrix4x4 localToWorldMatrix = _transform.get_localToWorldMatrix();
		Vector3 val = localToWorldMatrix.MultiplyPoint(offset);
		num = val.y - base_y;
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
