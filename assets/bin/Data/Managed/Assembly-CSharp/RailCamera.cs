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

	public RailCamera()
		: this()
	{
	}

	private void Awake()
	{
		targetCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
		targetCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
		railAnim = targetCameraTransform.get_gameObject().AddComponent<RailAnimation>();
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
			Object.DestroyImmediate(railAnim);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		Vector2 vector = info.position - info.move;
		Vector2 position = info.position;
		vector.y = 0f;
		position.y = 0f;
		Vector3 position2 = targetCameraTransform.get_position();
		float y = position2.y;
		Vector3 val = targetCamera.ScreenToWorldPoint(vector.ToVector3XY(y));
		Vector3 val2 = targetCamera.ScreenToWorldPoint(position.ToVector3XY(y));
		Vector3 val3 = val2 - val;
		val3.y = 0f;
		float num = val3.get_magnitude();
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
