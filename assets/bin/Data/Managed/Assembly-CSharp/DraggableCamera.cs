using System;
using UnityEngine;

public class DraggableCamera
{
	protected Camera __camera;

	protected Transform _cameraTransform;

	protected Vector3 cameraMove = Vector3.get_zero();

	public virtual Camera _camera
	{
		get
		{
			if (__camera == null)
			{
				__camera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
			}
			return __camera;
		}
	}

	protected virtual Transform cameraTransform
	{
		get
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			if (_cameraTransform == null)
			{
				_cameraTransform = _camera.get_transform();
			}
			return _cameraTransform;
		}
	}

	protected virtual Plane hitPlane => new Plane(Vector3.get_up(), 0f);

	public bool isChanging
	{
		get;
		protected set;
	}

	public float distanceManual
	{
		get;
		protected set;
	}

	public Vector3 targetPos
	{
		get;
		set;
	}

	public Vector3 angle
	{
		get;
		protected set;
	}

	public float distance
	{
		get;
		protected set;
	}

	protected virtual float cameraManualDistanceMin => 0f;

	protected virtual float cameraManualDistanceMax => 0f;

	public DraggableCamera()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)


	private void OnEnable()
	{
		InputManager.OnTouchOn = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTouchOn, new InputManager.OnTouchDelegate(OnTouchOn));
		InputManager.OnTouchOff = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTouchOff, new InputManager.OnTouchDelegate(OnTouchOff));
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
		InputManager.OnPinch = (InputManager.OnPinchDelegate)Delegate.Combine(InputManager.OnPinch, new InputManager.OnPinchDelegate(OnPinch));
		InputManager.OnDoubleDrag = (InputManager.OnDoubleTouchDelegate)Delegate.Combine(InputManager.OnDoubleDrag, new InputManager.OnDoubleTouchDelegate(OnDoubleDrag));
	}

	private void OnDisable()
	{
		InputManager.OnTouchOn = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTouchOn, new InputManager.OnTouchDelegate(OnTouchOn));
		InputManager.OnTouchOff = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTouchOff, new InputManager.OnTouchDelegate(OnTouchOff));
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
		InputManager.OnPinch = (InputManager.OnPinchDelegate)Delegate.Remove(InputManager.OnPinch, new InputManager.OnPinchDelegate(OnPinch));
		InputManager.OnDoubleDrag = (InputManager.OnDoubleTouchDelegate)Delegate.Remove(InputManager.OnDoubleDrag, new InputManager.OnDoubleTouchDelegate(OnDoubleDrag));
	}

	protected virtual void OnTouchOn(InputManager.TouchInfo info)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (IsInteractive())
		{
			cameraMove = Vector3.get_zero();
		}
	}

	protected virtual void OnTouchOff(InputManager.TouchInfo info)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (IsInteractive())
		{
			cameraMove = GetCameraMove(info);
		}
	}

	protected virtual void OnDrag(InputManager.TouchInfo info)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (IsInteractive())
		{
			cameraMove = Vector3.get_zero();
			targetPos += GetCameraMove(info);
		}
	}

	protected virtual void OnDoubleDrag(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (IsInteractive())
		{
			cameraMove = Vector3.get_zero();
			targetPos += GetCameraMove(touch_info0, touch_info1);
		}
	}

	protected virtual void OnPinch(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1, float pinch_length)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		if (IsInteractive() && touch_info0 != null && touch_info1 != null)
		{
			Plane hitPlane = this.hitPlane;
			cameraMove = Vector3.get_zero();
			Vector2 val = (touch_info0.position + touch_info1.position) * 0.5f;
			Ray val2 = _camera.ScreenPointToRay(val.ToVector3XY());
			float num = default(float);
			if (hitPlane.Raycast(val2, ref num))
			{
				Vector3 point = val2.GetPoint(num);
				distance -= pinch_length * 0.01f;
				distance = Mathf.Clamp(distance, cameraManualDistanceMin, cameraManualDistanceMax);
				distanceManual = distance;
				UpdateCameraTransform();
				Vector2 val3 = Utility.ToVector2XY(_camera.WorldToScreenPoint(point));
				Vector2 val4 = Utility.ToVector2XY(_camera.WorldToScreenPoint(targetPos));
				Vector2 vector = val4 + (val3 - val);
				val2 = _camera.ScreenPointToRay(vector.ToVector3XY());
				if (hitPlane.Raycast(val2, ref num))
				{
					Vector3 point2 = val2.GetPoint(num);
					point2.y = 0f;
					Vector3 val5 = point2 - targetPos;
					if (!(val5.get_magnitude() < 1.401298E-45f))
					{
						targetPos = point2;
					}
				}
			}
		}
	}

	protected virtual bool IsInteractive()
	{
		return true;
	}

	protected Vector3 GetCameraMove(InputManager.TouchInfo info)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 old_screen_pos = info.position - info.move;
		Vector2 position = info.position;
		return GetCameraMove(old_screen_pos, position);
	}

	protected Vector3 GetCameraMove(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = touch_info0.position - touch_info0.move;
		Vector2 position = touch_info0.position;
		Vector2 val2 = touch_info1.position - touch_info1.move;
		Vector2 position2 = touch_info1.position;
		return GetCameraMove((val + val2) * 0.5f, (position + position2) * 0.5f);
	}

	protected virtual Vector3 GetCameraMove(Vector2 old_screen_pos, Vector2 now_screen_pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		Plane hitPlane = this.hitPlane;
		Ray val = _camera.ScreenPointToRay(old_screen_pos.ToVector3XY());
		Ray val2 = _camera.ScreenPointToRay(now_screen_pos.ToVector3XY());
		float num = default(float);
		if (!hitPlane.Raycast(val, ref num))
		{
			return Vector3.get_zero();
		}
		float num2 = default(float);
		if (!hitPlane.Raycast(val2, ref num2))
		{
			return Vector3.get_zero();
		}
		Vector3 point = val.GetPoint(num);
		Vector3 point2 = val2.GetPoint(num2);
		Vector3 result = point2 - point;
		result.x = 0f - result.x;
		result.y = 0f;
		result.z = 0f - result.z;
		return result;
	}

	protected virtual Vector3 ClampEnableMapArea(Vector3 pos)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		float num = 5f;
		if (pos.x < 0f - num)
		{
			pos.x = 0f - num;
		}
		else if (pos.x > num)
		{
			pos.x = num;
		}
		if (pos.z < 0f - num)
		{
			pos.z = 0f - num;
		}
		else if (pos.z > num)
		{
			pos.z = num;
		}
		return pos;
	}

	protected virtual void UpdateCameraTransform()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		cameraTransform.set_eulerAngles(angle);
		targetPos = ClampEnableMapArea(targetPos);
		cameraTransform.set_position(targetPos - cameraTransform.get_forward() * distance);
	}

	private void FixedUpdate()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (cameraMove != Vector3.get_zero())
		{
			cameraMove *= 0.75f;
			targetPos += cameraMove;
		}
	}

	private void Update()
	{
		UpdateCameraTransform();
	}
}
