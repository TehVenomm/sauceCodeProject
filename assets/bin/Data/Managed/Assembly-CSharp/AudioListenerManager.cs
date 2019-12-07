using System;
using UnityEngine;

public class AudioListenerManager : MonoBehaviourSingleton<AudioListenerManager>
{
	[Flags]
	public enum STATUS_FLAGS
	{
		INITIALIZE = 0x1,
		CAMERA_MAIN_ACTIVE = 0x2,
		CAMERA_INGAME_ACTIVE = 0x4,
		TARGET_OBJECT_ACTIVE = 0x20
	}

	[Flags]
	public enum TRACE_FLAGS
	{
		POSITION = 0x1,
		ROTATION = 0x2
	}

	private STATUS_FLAGS Status = STATUS_FLAGS.INITIALIZE;

	private const TRACE_FLAGS TRACE_BOTH = TRACE_FLAGS.POSITION | TRACE_FLAGS.ROTATION;

	private StageObject m_target;

	public bool HasFlag(STATUS_FLAGS flg)
	{
		return (Status & flg) == flg;
	}

	public void SetFlag(STATUS_FLAGS flg, bool isEnable)
	{
		if (isEnable)
		{
			Status |= flg;
		}
		else
		{
			Status &= ~flg;
		}
	}

	public void SetTargetObject(StageObject obj)
	{
		if (!(obj == null))
		{
			m_target = obj;
			SetFlag(STATUS_FLAGS.TARGET_OBJECT_ACTIVE, isEnable: true);
		}
	}

	public void ReSetTargetObject()
	{
		m_target = null;
		SetFlag(STATUS_FLAGS.TARGET_OBJECT_ACTIVE, isEnable: false);
	}

	protected override void Awake()
	{
		base.gameObject.AddComponent<AudioListener>();
		base.Awake();
	}

	private void LateUpdate()
	{
		UpdateListener();
	}

	private void UpdateListener()
	{
		if (Status != 0)
		{
			if (HasFlag(STATUS_FLAGS.CAMERA_INGAME_ACTIVE))
			{
				TraceIngameCamera();
			}
			else if (HasFlag(STATUS_FLAGS.TARGET_OBJECT_ACTIVE))
			{
				TraceObject();
			}
			else if (HasFlag(STATUS_FLAGS.CAMERA_MAIN_ACTIVE))
			{
				TraceMainCamera();
			}
		}
	}

	private void Trace(Transform t, TRACE_FLAGS flag)
	{
		if (t != null)
		{
			if ((flag & TRACE_FLAGS.POSITION) != 0)
			{
				base._transform.position = t.position;
			}
			if ((flag & TRACE_FLAGS.ROTATION) != 0)
			{
				base._transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.up, t.right));
			}
		}
	}

	private void TraceMainCamera(TRACE_FLAGS flags = TRACE_FLAGS.POSITION | TRACE_FLAGS.ROTATION)
	{
		if (MonoBehaviourSingleton<AppMain>.IsValid() && MonoBehaviourSingleton<AppMain>.I.mainCameraTransform != null)
		{
			Trace(MonoBehaviourSingleton<AppMain>.I.mainCameraTransform, TRACE_FLAGS.POSITION | TRACE_FLAGS.ROTATION);
		}
	}

	private void TraceObject()
	{
		TraceMainCamera(TRACE_FLAGS.ROTATION);
		if (m_target == null || m_target._transform == null)
		{
			SetFlag(STATUS_FLAGS.TARGET_OBJECT_ACTIVE, isEnable: false);
		}
		else
		{
			Trace(m_target._transform, TRACE_FLAGS.POSITION);
		}
	}

	private void TraceIngameCamera(TRACE_FLAGS flags = TRACE_FLAGS.POSITION | TRACE_FLAGS.ROTATION)
	{
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform != null)
		{
			Trace(MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform, TRACE_FLAGS.POSITION | TRACE_FLAGS.ROTATION);
		}
		else
		{
			SetFlag(STATUS_FLAGS.CAMERA_INGAME_ACTIVE, isEnable: false);
		}
	}
}
