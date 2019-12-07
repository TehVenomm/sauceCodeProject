using System;
using UnityEngine;

public class RailCamera : MonoBehaviour
{
	public float moveCoef = -0.3f;

	public float easeCoef = 0.9f;

	private float moveRate;

	public bool enableInput
	{
		get;
		set;
	}

	public Camera targetCamera
	{
		get;
		private set;
	}

	public Transform targetCameraTransform
	{
		get;
		private set;
	}

	public RailAnimation railAnim
	{
		get;
		private set;
	}

	private void Awake()
	{
		targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		targetCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		railAnim = targetCameraTransform.gameObject.AddComponent<RailAnimation>();
		enableInput = true;
	}

	private void FixedUpdate()
	{
		if (moveRate != 0f)
		{
			moveRate *= easeCoef;
			railAnim.rate += moveRate;
		}
	}

	private void OnEnable()
	{
		InputManager.OnTouchOn = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTouchOn, new InputManager.OnTouchDelegate(OnTouchOn));
		InputManager.OnTouchOff = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTouchOff, new InputManager.OnTouchDelegate(OnTouchOff));
		InputManager.OnDrag = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDrag, new InputManager.OnTouchDelegate(OnDrag));
		railAnim.enabledRail = true;
	}

	private void OnDisable()
	{
		InputManager.OnTouchOn = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTouchOn, new InputManager.OnTouchDelegate(OnTouchOn));
		InputManager.OnTouchOff = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTouchOff, new InputManager.OnTouchDelegate(OnTouchOff));
		InputManager.OnDrag = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDrag, new InputManager.OnTouchDelegate(OnDrag));
		railAnim.enabledRail = false;
		Stop();
	}

	private void OnDestroy()
	{
		if (!AppMain.isApplicationQuit)
		{
			UnityEngine.Object.DestroyImmediate(railAnim);
		}
	}

	private void OnTouchOn(InputManager.TouchInfo info)
	{
		if (enableInput)
		{
			Stop();
		}
	}

	private void OnTouchOff(InputManager.TouchInfo info)
	{
		if (enableInput)
		{
			moveRate = GetMove(info);
		}
	}

	private void OnDrag(InputManager.TouchInfo info)
	{
		if (enableInput)
		{
			railAnim.rate += GetMove(info);
		}
	}

	protected virtual float GetMove(InputManager.TouchInfo info)
	{
		Vector2 vector = info.position - info.move;
		Vector2 position = info.position;
		vector.y = 0f;
		position.y = 0f;
		float y = targetCameraTransform.position.y;
		Vector3 b = targetCamera.ScreenToWorldPoint(vector.ToVector3XY(y));
		Vector3 vector2 = targetCamera.ScreenToWorldPoint(position.ToVector3XY(y)) - b;
		vector2.y = 0f;
		float num = vector2.magnitude;
		if (info.move.x < 0f)
		{
			num = 0f - num;
		}
		return num * moveCoef;
	}

	public void Stop()
	{
		moveRate = 0f;
	}
}
