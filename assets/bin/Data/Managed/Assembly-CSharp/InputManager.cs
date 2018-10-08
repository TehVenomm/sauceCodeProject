using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviourSingleton<InputManager>
{
	public class TouchInfo
	{
		public int id = -1;

		public int unique;

		public Transform hit;

		public int touchCount;

		public Vector2 position;

		public Vector2 beginPosition;

		public List<Vector2> prevPositions;

		public Vector2 move;

		public float moveLength;

		public float moveLengthTotal;

		public float beginTime;

		public float endTime;

		public bool activeAxis;

		public Vector2 axis;

		public Vector2 axisNoLimit;

		public bool calledTap;

		public bool calledFlick;

		public bool calledLongTouch;

		public bool enable => (Object)hit == (Object)null && id != -1;

		public void Clear()
		{
			id = -1;
			hit = null;
		}
	}

	public delegate void OnTouchDelegate(TouchInfo touch_info);

	public delegate void OnDoubleTouchDelegate(TouchInfo touch_info0, TouchInfo touch_info1);

	public delegate void OnPinchDelegate(TouchInfo touch_info0, TouchInfo touch_info1, float pinch_length);

	public delegate void OnStickDelegate(Vector2 stick_vec);

	public delegate void OnFlickDelegate(Vector2 flick_vec);

	private const int PREV_POSITIONS_NUM = 3;

	public static float pxRate = 1f;

	public static OnTouchDelegate OnTap;

	public static OnTouchDelegate OnDoubleTap;

	public static OnStickDelegate OnStick;

	public static OnTouchDelegate OnLongTouch;

	public static OnFlickDelegate OnFlick;

	public static OnTouchDelegate OnTouchOn;

	public static OnTouchDelegate OnTouchOff;

	public static OnTouchDelegate OnDrag;

	public static OnDoubleTouchDelegate OnDoubleDrag;

	public static OnPinchDelegate OnPinch;

	public static OnTouchDelegate OnTouchOnAlways;

	public static OnTouchDelegate OnTouchOffAlways;

	public static OnTouchDelegate OnDragAlways;

	public int enableTouchCount = 2;

	public int enableStickCount = 1;

	[Tooltip("ドラッグやピンチ判定のタッチ移動距離")]
	public float dragThresholdLength = 1f;

	[Tooltip("バ\u30fcチャルパッドの最大距離")]
	public float stickMaxLength = 80f;

	[Tooltip("バ\u30fcチャルパッド判定のタッチ移動距離")]
	public float stickThresholdLength = 20f;

	[Tooltip("フリック判定のタッチ移動距離、感度高")]
	public float flickThresholdLengthHigh = 8f;

	[Tooltip("フリック判定のタッチ移動距離、感度低")]
	public float flickThresholdLengthLow = 24f;

	[Tooltip("フリック判定の速度、感度高")]
	public float flickThresholdSpeedHigh = 200f;

	[Tooltip("フリック判定の速度、感度低")]
	public float flickThresholdSpeedLow = 600f;

	[Tooltip("フリックを距離と速度のどちらで判定するか分ける時間")]
	public float flickLimitTime = 0.2f;

	[Tooltip("長押し判定の時間")]
	public float longTouchTime = 0.25f;

	[Tooltip("ダブルタップの各タップの許容時間")]
	public float doubleTapSingleTime = 0.15f;

	[Tooltip("ダブルタップの全体の許容時間")]
	public float doubleTapEnableTime = 0.4f;

	private float doubleTapBeginTime;

	private int doubleTapCount;

	private float scaledDragThresholdLength;

	private float scaledStickMaxLength;

	private float scaledStickThresholdLength;

	private float scaledFlickThresholdLength;

	private float scaledFlickThresholdSpeed;

	private TouchInfo[] touchInfos;

	private int unique;

	private bool _untouch;

	public INPUT_DISABLE_FACTOR disableFlags
	{
		get;
		private set;
	}

	protected override void OnDestroySingleton()
	{
		OnTap = null;
		OnDoubleTap = null;
		OnStick = null;
		OnLongTouch = null;
		OnFlick = null;
		OnTouchOn = null;
		OnTouchOff = null;
		OnDrag = null;
		OnDoubleDrag = null;
		OnPinch = null;
		OnTouchOnAlways = null;
		OnTouchOffAlways = null;
		OnDragAlways = null;
	}

	public bool IsDisable()
	{
		return disableFlags != (INPUT_DISABLE_FACTOR)0;
	}

	public void SetDisable(INPUT_DISABLE_FACTOR factor, bool disable)
	{
		if (disable)
		{
			disableFlags |= factor;
		}
		else
		{
			disableFlags &= ~factor;
		}
	}

	public bool IsActiveStick()
	{
		int i = 0;
		for (int num = touchInfos.Length; i < num; i++)
		{
			TouchInfo touchInfo = touchInfos[i];
			if (touchInfo.id != -1 && touchInfo.activeAxis && touchInfo.enable)
			{
				return true;
			}
		}
		return false;
	}

	public Vector2 GetStickVector()
	{
		int i = 0;
		for (int num = touchInfos.Length; i < num; i++)
		{
			TouchInfo touchInfo = touchInfos[i];
			if (touchInfo.id != -1 && touchInfo.activeAxis && touchInfo.enable)
			{
				return touchInfo.axis;
			}
		}
		return Vector2.zero;
	}

	public TouchInfo GetStickInfo()
	{
		int i = 0;
		for (int num = touchInfos.Length; i < num; i++)
		{
			TouchInfo touchInfo = touchInfos[i];
			if (touchInfo.id != -1 && touchInfo.activeAxis && touchInfo.enable)
			{
				return touchInfo;
			}
		}
		return null;
	}

	public bool IsTouch()
	{
		int i = 0;
		for (int num = touchInfos.Length; i < num; i++)
		{
			TouchInfo touchInfo = touchInfos[i];
			if (touchInfo.id != -1 && touchInfo.enable)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsTouchIgnoreHit()
	{
		int i = 0;
		for (int num = touchInfos.Length; i < num; i++)
		{
			TouchInfo touchInfo = touchInfos[i];
			if (touchInfo.id != -1)
			{
				return true;
			}
		}
		return false;
	}

	public int GetActiveInfoCount()
	{
		int num = 0;
		int i = 0;
		for (int num2 = touchInfos.Length; i < num2; i++)
		{
			TouchInfo touchInfo = touchInfos[i];
			if (touchInfo.id != -1)
			{
				num++;
			}
		}
		return num;
	}

	public TouchInfo GetActiveInfo(bool check_enable = false)
	{
		int i = 0;
		for (int num = touchInfos.Length; i < num; i++)
		{
			TouchInfo touchInfo = touchInfos[i];
			if (touchInfo.id != -1 && (!check_enable || touchInfo.enable))
			{
				return touchInfo;
			}
		}
		return null;
	}

	private TouchInfo GetInfo(int id)
	{
		int i = 0;
		for (int num = touchInfos.Length; i < num; i++)
		{
			TouchInfo touchInfo = touchInfos[i];
			if (touchInfo.id == id)
			{
				return touchInfo;
			}
		}
		return null;
	}

	private void Start()
	{
		if (Screen.dpi > 0f)
		{
			pxRate = Screen.dpi / 160f;
		}
		scaledDragThresholdLength = dragThresholdLength * pxRate;
		scaledStickMaxLength = stickMaxLength * pxRate;
		scaledStickThresholdLength = stickThresholdLength * pxRate;
		UpdateConfigInput();
		touchInfos = new TouchInfo[enableTouchCount];
		for (int i = 0; i < enableTouchCount; i++)
		{
			touchInfos[i] = new TouchInfo();
		}
		Untouch();
	}

	public void UpdateConfigInput()
	{
		float num = 0.5f;
		if (GameSaveData.instance != null)
		{
			num = GameSaveData.instance.touchInGameFlick;
		}
		float num2 = flickThresholdLengthLow + (flickThresholdLengthHigh - flickThresholdLengthLow) * num;
		scaledFlickThresholdLength = num2 * pxRate;
		float num3 = flickThresholdSpeedLow + (flickThresholdSpeedHigh - flickThresholdSpeedLow) * num;
		scaledFlickThresholdSpeed = num3 * pxRate;
	}

	private void Update()
	{
		int touchCount = Input.touchCount;
		if (touchCount > 0 && disableFlags == (INPUT_DISABLE_FACTOR)0)
		{
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				Touch(touch.fingerId, touch.phase, touch.position);
			}
		}
		else
		{
			Untouch();
		}
		if (OnDrag != null || OnDoubleDrag != null || OnPinch != null || OnDragAlways != null)
		{
			TouchInfo touchInfo = null;
			TouchInfo touchInfo2 = null;
			int num = 0;
			int j = 0;
			for (int num2 = touchInfos.Length; j < num2; j++)
			{
				TouchInfo touchInfo3 = touchInfos[j];
				if (touchInfo3.id != -1)
				{
					num++;
					if (touchInfo == null)
					{
						touchInfo = touchInfo3;
					}
					else if (touchInfo2 == null)
					{
						touchInfo2 = touchInfo3;
					}
				}
			}
			switch (num)
			{
			case 1:
				if (touchInfo.moveLengthTotal > scaledDragThresholdLength && touchInfo.moveLength > 0f)
				{
					if (touchInfo.enable && OnDrag != null)
					{
						OnDrag(touchInfo);
					}
					if (OnDragAlways != null)
					{
						OnDragAlways(touchInfo);
					}
				}
				break;
			case 2:
				if (touchInfo.moveLength > 0f || touchInfo2.moveLength > 0f)
				{
					bool flag = false;
					if (OnPinch != null && (touchInfo.moveLengthTotal > scaledDragThresholdLength || touchInfo2.moveLengthTotal > scaledDragThresholdLength) && (touchInfo.move == Vector2.zero || touchInfo2.move == Vector2.zero || Vector2.Angle(touchInfo.move.normalized, touchInfo2.move.normalized) > 90f))
					{
						float magnitude = (touchInfo.position - touchInfo.move - (touchInfo2.position - touchInfo2.move)).magnitude;
						float magnitude2 = (touchInfo.position - touchInfo2.position).magnitude;
						float num3 = magnitude2 - magnitude;
						if (Mathf.Abs(num3) > scaledDragThresholdLength)
						{
							SetEnableUIInput(false);
							OnPinch(touchInfo, touchInfo2, num3);
							flag = true;
						}
					}
					if (OnDoubleDrag != null && !flag && touchInfo.moveLengthTotal > scaledDragThresholdLength && touchInfo2.moveLengthTotal > scaledDragThresholdLength)
					{
						SetEnableUIInput(false);
						OnDoubleDrag(touchInfo, touchInfo2);
					}
				}
				break;
			}
		}
	}

	private void Touch(int id, TouchPhase phase, Vector2 pos)
	{
		switch (phase)
		{
		case TouchPhase.Began:
			if (GetInfo(id) == null)
			{
				TouchInfo info2 = GetInfo(-1);
				if (info2 != null)
				{
					info2.id = id;
					info2.unique = ++unique;
					info2.hit = GetHitTransform(pos);
					info2.touchCount = GetActiveInfoCount();
					info2.position = pos;
					info2.beginPosition = pos;
					if (info2.prevPositions == null)
					{
						info2.prevPositions = new List<Vector2>();
					}
					else
					{
						info2.prevPositions.Clear();
					}
					info2.move = Vector2.zero;
					info2.moveLength = 0f;
					info2.moveLengthTotal = 0f;
					info2.beginTime = Time.time;
					info2.endTime = 0f;
					info2.activeAxis = false;
					info2.axis = Vector2.zero;
					info2.axisNoLimit = Vector2.zero;
					info2.calledTap = false;
					info2.calledFlick = false;
					info2.calledLongTouch = false;
					if (info2.enable && OnTouchOn != null)
					{
						OnTouchOn(info2);
					}
					if (OnTouchOnAlways != null)
					{
						OnTouchOnAlways(info2);
					}
					if (info2.touchCount > 1)
					{
						doubleTapCount = 0;
					}
				}
			}
			break;
		case TouchPhase.Moved:
		case TouchPhase.Stationary:
		{
			TouchInfo info3 = GetInfo(id);
			if (info3 != null)
			{
				info3.move = pos - info3.position;
				if (pos != info3.position)
				{
					info3.move = pos - info3.position;
					info3.moveLength = info3.move.magnitude;
					info3.moveLengthTotal += info3.moveLength;
				}
				else
				{
					info3.move = Vector3.zero;
					info3.moveLength = 0f;
				}
				info3.position = pos;
				info3.prevPositions.Insert(0, pos);
				if (info3.prevPositions.Count > 3)
				{
					info3.prevPositions.RemoveRange(3, info3.prevPositions.Count - 3);
				}
				Vector2 vector3 = info3.position - info3.beginPosition;
				float magnitude2 = vector3.magnitude;
				if (!info3.activeAxis && magnitude2 >= scaledStickThresholdLength)
				{
					info3.activeAxis = true;
				}
				vector3.Normalize();
				info3.axis = vector3;
				info3.axisNoLimit = vector3;
				if (magnitude2 < scaledStickMaxLength)
				{
					info3.axis *= magnitude2 / scaledStickMaxLength;
				}
				info3.axisNoLimit *= magnitude2 / scaledStickMaxLength;
				if (info3.activeAxis)
				{
					if (info3.enable && OnStick != null)
					{
						OnStick(info3.axis);
					}
				}
				else if (longTouchTime > 0f && !info3.calledLongTouch && Time.time - info3.beginTime >= longTouchTime && info3.enable && OnLongTouch != null)
				{
					OnLongTouch(info3);
					info3.calledLongTouch = true;
				}
			}
			break;
		}
		case TouchPhase.Ended:
		case TouchPhase.Canceled:
		{
			TouchInfo info = GetInfo(id);
			if (info != null)
			{
				info.position = pos;
				info.prevPositions.Insert(0, pos);
				if (info.prevPositions.Count > 3)
				{
					info.prevPositions.RemoveRange(3, info.prevPositions.Count - 3);
				}
				info.endTime = Time.time;
				float num = info.endTime - info.beginTime;
				if (num <= flickLimitTime)
				{
					Vector3 vector = pos - info.beginPosition;
					float magnitude = vector.magnitude;
					if (magnitude >= scaledFlickThresholdLength && info.enable && OnFlick != null)
					{
						OnFlick(vector.normalized);
						info.calledFlick = true;
					}
				}
				else
				{
					int index = (info.prevPositions.Count > 1) ? 1 : 0;
					Vector3 vector2 = info.prevPositions[index] - info.prevPositions[info.prevPositions.Count - 1];
					float num2 = (!(Time.deltaTime > 0f)) ? 0f : (vector2.magnitude / Time.deltaTime);
					if (num2 >= scaledFlickThresholdSpeed && info.enable && OnFlick != null)
					{
						OnFlick(vector2.normalized);
						info.calledFlick = true;
					}
				}
				if (!info.activeAxis && !info.calledFlick && !info.calledLongTouch && info.enable && OnTap != null)
				{
					OnTap(info);
					info.calledTap = true;
				}
				if (Time.time - info.beginTime <= doubleTapSingleTime && !info.calledFlick)
				{
					if (doubleTapCount == 0)
					{
						doubleTapCount = 1;
						doubleTapBeginTime = info.beginTime;
					}
					else if (doubleTapCount == 1)
					{
						if (Time.time - doubleTapBeginTime <= doubleTapEnableTime)
						{
							if (info.enable && OnDoubleTap != null)
							{
								OnDoubleTap(info);
							}
						}
						else
						{
							doubleTapCount = 1;
							doubleTapBeginTime = info.beginTime;
						}
					}
				}
				else
				{
					doubleTapCount = 0;
				}
				if (info.enable && OnTouchOff != null)
				{
					OnTouchOff(info);
				}
				if (OnTouchOffAlways != null)
				{
					OnTouchOffAlways(info);
				}
				if (info.touchCount == 1 && !info.enable)
				{
					info.id = -1;
					Untouch();
				}
				info.Clear();
				if (!IsEnableUIInput())
				{
					int num3 = 0;
					int i = 0;
					for (int num4 = touchInfos.Length; i < num4; i++)
					{
						if (touchInfos[i].enable)
						{
							num3++;
						}
					}
					if (num3 <= 1)
					{
						SetEnableUIInput(true);
					}
				}
			}
			break;
		}
		}
	}

	public void Untouch()
	{
		if (touchInfos != null && !_untouch)
		{
			_untouch = true;
			int i = 0;
			for (int num = touchInfos.Length; i < num; i++)
			{
				TouchInfo touchInfo = touchInfos[i];
				if (touchInfo.id != -1)
				{
					Touch(touchInfo.id, TouchPhase.Ended, touchInfo.position);
				}
				touchInfo.Clear();
			}
			SetEnableUIInput(true);
			_untouch = false;
		}
	}

	private void SetEnableUIInput(bool is_enable)
	{
		MonoBehaviourSingleton<UIManager>.I.SetEnableUIInput(is_enable);
	}

	private bool IsEnableUIInput()
	{
		return MonoBehaviourSingleton<UIManager>.I.IsEnableUIInput();
	}

	private Transform GetHitTransform(Vector3 pos)
	{
		if (!Physics.Raycast(MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenPointToRay(pos), out RaycastHit hitInfo, 32f))
		{
			return null;
		}
		return hitInfo.collider.transform;
	}
}
