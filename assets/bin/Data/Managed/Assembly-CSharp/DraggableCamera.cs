using System;
using UnityEngine;

public class DraggableCamera : MonoBehaviour
{
	protected Camera __camera;

	protected Transform _cameraTransform;

	protected Vector3 cameraMove = Vector3.zero;

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
			if (_cameraTransform == null)
			{
				_cameraTransform = _camera.transform;
			}
			return _cameraTransform;
		}
	}

	protected virtual Plane hitPlane => new Plane(Vector3.up, 0f);

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
		if (IsInteractive())
		{
			cameraMove = Vector3.zero;
		}
	}

	protected virtual void OnTouchOff(InputManager.TouchInfo info)
	{
		if (IsInteractive())
		{
			cameraMove = GetCameraMove(info);
		}
	}

	protected virtual void OnDrag(InputManager.TouchInfo info)
	{
		if (IsInteractive())
		{
			cameraMove = Vector3.zero;
			targetPos += GetCameraMove(info);
		}
	}

	protected virtual void OnDoubleDrag(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1)
	{
		if (IsInteractive())
		{
			cameraMove = Vector3.zero;
			targetPos += GetCameraMove(touch_info0, touch_info1);
		}
	}

	protected virtual void OnPinch(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1, float pinch_length)
	{
		if (!IsInteractive() || touch_info0 == null || touch_info1 == null)
		{
			return;
		}
		Plane hitPlane = this.hitPlane;
		cameraMove = Vector3.zero;
		Vector2 vector = (touch_info0.position + touch_info1.position) * 0.5f;
		Ray ray = _camera.ScreenPointToRay(vector.ToVector3XY());
		if (!hitPlane.Raycast(ray, out float enter))
		{
			return;
		}
		Vector3 point = ray.GetPoint(enter);
		distance -= pinch_length * 0.01f;
		distance = Mathf.Clamp(distance, cameraManualDistanceMin, cameraManualDistanceMax);
		distanceManual = distance;
		UpdateCameraTransform();
		Vector2 a = _camera.WorldToScreenPoint(point).ToVector2XY();
		Vector2 vector2 = _camera.WorldToScreenPoint(targetPos).ToVector2XY() + (a - vector);
		ray = _camera.ScreenPointToRay(vector2.ToVector3XY());
		if (hitPlane.Raycast(ray, out enter))
		{
			Vector3 point2 = ray.GetPoint(enter);
			point2.y = 0f;
			if (!((point2 - targetPos).magnitude < float.Epsilon))
			{
				targetPos = point2;
			}
		}
	}

	protected virtual bool IsInteractive()
	{
		return true;
	}

	protected Vector3 GetCameraMove(InputManager.TouchInfo info)
	{
		Vector2 old_screen_pos = info.position - info.move;
		Vector2 position = info.position;
		return GetCameraMove(old_screen_pos, position);
	}

	protected Vector3 GetCameraMove(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1)
	{
		Vector2 a = touch_info0.position - touch_info0.move;
		Vector2 position = touch_info0.position;
		Vector2 b = touch_info1.position - touch_info1.move;
		Vector2 position2 = touch_info1.position;
		return GetCameraMove((a + b) * 0.5f, (position + position2) * 0.5f);
	}

	protected virtual Vector3 GetCameraMove(Vector2 old_screen_pos, Vector2 now_screen_pos)
	{
		Plane hitPlane = this.hitPlane;
		Ray ray = _camera.ScreenPointToRay(old_screen_pos.ToVector3XY());
		Ray ray2 = _camera.ScreenPointToRay(now_screen_pos.ToVector3XY());
		if (!hitPlane.Raycast(ray, out float enter))
		{
			return Vector3.zero;
		}
		if (!hitPlane.Raycast(ray2, out float enter2))
		{
			return Vector3.zero;
		}
		Vector3 point = ray.GetPoint(enter);
		Vector3 result = ray2.GetPoint(enter2) - point;
		result.x = 0f - result.x;
		result.y = 0f;
		result.z = 0f - result.z;
		return result;
	}

	protected virtual Vector3 ClampEnableMapArea(Vector3 pos)
	{
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
		cameraTransform.eulerAngles = angle;
		targetPos = ClampEnableMapArea(targetPos);
		cameraTransform.position = targetPos - cameraTransform.forward * distance;
	}

	private void FixedUpdate()
	{
		if (cameraMove != Vector3.zero)
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
