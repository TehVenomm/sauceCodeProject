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

		public bool enable => hit == null && id != -1;

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
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int num = touchInfos.Length; i < num; i++)
		{
			TouchInfo touchInfo = touchInfos[i];
			if (touchInfo.id != -1 && touchInfo.activeAxis && touchInfo.enable)
			{
				return touchInfo.axis;
			}
		}
		return Vector2.get_zero();
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
		if (Screen.get_dpi() > 0f)
		{
			pxRate = Screen.get_dpi() / 160f;
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
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		int touchCount = Input.get_touchCount();
		if (touchCount > 0 && disableFlags == (INPUT_DISABLE_FACTOR)0)
		{
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				Touch(touch.get_fingerId(), touch.get_phase(), touch.get_position());
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
					if (OnPinch != null && (touchInfo.moveLengthTotal > scaledDragThresholdLength || touchInfo2.moveLengthTotal > scaledDragThresholdLength) && (touchInfo.move == Vector2.get_zero() || touchInfo2.move == Vector2.get_zero() || Vector2.Angle(touchInfo.move.get_normalized(), touchInfo2.move.get_normalized()) > 90f))
					{
						Vector2 val = touchInfo.position - touchInfo.move - (touchInfo2.position - touchInfo2.move);
						float magnitude = val.get_magnitude();
						Vector2 val2 = touchInfo.position - touchInfo2.position;
						float magnitude2 = val2.get_magnitude();
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
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Expected I4, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		switch ((int)phase)
		{
		case 0:
			if (GetInfo(id) == null)
			{
				TouchInfo info2 = GetInfo(-1);
				if (info2 != null)
				{
					info2.id = id;
					info2.unique = ++unique;
					info2.hit = GetHitTransform(Vector2.op_Implicit(pos));
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
					info2.move = Vector2.get_zero();
					info2.moveLength = 0f;
					info2.moveLengthTotal = 0f;
					info2.beginTime = Time.get_time();
					info2.endTime = 0f;
					info2.activeAxis = false;
					info2.axis = Vector2.get_zero();
					info2.axisNoLimit = Vector2.get_zero();
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
		case 1:
		case 2:
		{
			TouchInfo info3 = GetInfo(id);
			if (info3 != null)
			{
				info3.move = pos - info3.position;
				if (pos != info3.position)
				{
					info3.move = pos - info3.position;
					info3.moveLength = info3.move.get_magnitude();
					info3.moveLengthTotal += info3.moveLength;
				}
				else
				{
					info3.move = Vector2.op_Implicit(Vector3.get_zero());
					info3.moveLength = 0f;
				}
				info3.position = pos;
				info3.prevPositions.Insert(0, pos);
				if (info3.prevPositions.Count > 3)
				{
					info3.prevPositions.RemoveRange(3, info3.prevPositions.Count - 3);
				}
				Vector2 val3 = info3.position - info3.beginPosition;
				float magnitude2 = val3.get_magnitude();
				if (!info3.activeAxis && magnitude2 >= scaledStickThresholdLength)
				{
					info3.activeAxis = true;
				}
				val3.Normalize();
				info3.axis = val3;
				info3.axisNoLimit = val3;
				if (magnitude2 < scaledStickMaxLength)
				{
					TouchInfo touchInfo = info3;
					touchInfo.axis *= magnitude2 / scaledStickMaxLength;
				}
				TouchInfo touchInfo2 = info3;
				touchInfo2.axisNoLimit *= magnitude2 / scaledStickMaxLength;
				if (info3.activeAxis)
				{
					if (info3.enable && OnStick != null)
					{
						OnStick(info3.axis);
					}
				}
				else if (longTouchTime > 0f && !info3.calledLongTouch && Time.get_time() - info3.beginTime >= longTouchTime && info3.enable && OnLongTouch != null)
				{
					OnLongTouch(info3);
					info3.calledLongTouch = true;
				}
			}
			break;
		}
		case 3:
		case 4:
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
				info.endTime = Time.get_time();
				float num = info.endTime - info.beginTime;
				if (num <= flickLimitTime)
				{
					Vector3 val = Vector2.op_Implicit(pos - info.beginPosition);
					float magnitude = val.get_magnitude();
					if (magnitude >= scaledFlickThresholdLength && info.enable && OnFlick != null)
					{
						OnFlick(Vector2.op_Implicit(val.get_normalized()));
						info.calledFlick = true;
					}
				}
				else
				{
					int index = (info.prevPositions.Count > 1) ? 1 : 0;
					Vector3 val2 = Vector2.op_Implicit(info.prevPositions[index] - info.prevPositions[info.prevPositions.Count - 1]);
					float num2 = (!(Time.get_deltaTime() > 0f)) ? 0f : (val2.get_magnitude() / Time.get_deltaTime());
					if (num2 >= scaledFlickThresholdSpeed && info.enable && OnFlick != null)
					{
						OnFlick(Vector2.op_Implicit(val2.get_normalized()));
						info.calledFlick = true;
					}
				}
				if (!info.activeAxis && !info.calledFlick && !info.calledLongTouch && info.enable && OnTap != null)
				{
					OnTap(info);
					info.calledTap = true;
				}
				if (Time.get_time() - info.beginTime <= doubleTapSingleTime && !info.calledFlick)
				{
					if (doubleTapCount == 0)
					{
						doubleTapCount = 1;
						doubleTapBeginTime = info.beginTime;
					}
					else if (doubleTapCount == 1)
					{
						if (Time.get_time() - doubleTapBeginTime <= doubleTapEnableTime)
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
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (touchInfos != null && !_untouch)
		{
			_untouch = true;
			int i = 0;
			for (int num = touchInfos.Length; i < num; i++)
			{
				TouchInfo touchInfo = touchInfos[i];
				if (touchInfo.id != -1)
				{
					Touch(touchInfo.id, 3, touchInfo.position);
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
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		RaycastHit val = default(RaycastHit);
		if (!Physics.Raycast(MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenPointToRay(pos), ref val, 32f))
		{
			return null;
		}
		return val.get_collider().get_transform();
	}
}
