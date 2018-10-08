using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Event System (UICamera)")]
public class UICamera
{
	public enum ControlScheme
	{
		Mouse,
		Touch,
		Controller
	}

	public enum ClickNotification
	{
		None,
		Always,
		BasedOnDelta
	}

	public class MouseOrTouch
	{
		public KeyCode key;

		public Vector2 pos;

		public Vector2 lastPos;

		public Vector2 delta;

		public Vector2 totalDelta;

		public Camera pressedCam;

		public GameObject last;

		public GameObject current;

		public GameObject pressed;

		public GameObject dragged;

		public float pressTime;

		public float clickTime;

		public ClickNotification clickNotification = ClickNotification.Always;

		public bool touchBegan = true;

		public bool pressStarted;

		public bool dragStarted;

		public int ignoreDelta;

		public float deltaTime => RealTime.time - pressTime;

		public bool isOverUI => current != null && current != fallThrough && NGUITools.FindInParents<UIRoot>(current) != null;
	}

	public enum EventType
	{
		World_3D,
		UI_3D,
		World_2D,
		UI_2D
	}

	private struct DepthEntry
	{
		public int depth;

		public RaycastHit hit;

		public Vector3 point;

		public GameObject go;
	}

	public class Touch
	{
		public int fingerId;

		public TouchPhase phase;

		public Vector2 position;

		public int tapCount;
	}

	public delegate bool GetKeyStateFunc(KeyCode key);

	public delegate float GetAxisFunc(string name);

	public delegate bool GetAnyKeyFunc();

	public delegate void OnScreenResize();

	public delegate void OnCustomInput();

	public delegate void OnSchemeChange();

	public delegate void MoveDelegate(Vector2 delta);

	public delegate void VoidDelegate(GameObject go);

	public delegate void BoolDelegate(GameObject go, bool state);

	public delegate void FloatDelegate(GameObject go, float delta);

	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	public delegate void ObjectDelegate(GameObject go, GameObject obj);

	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

	public delegate int GetTouchCountCallback();

	public delegate Touch GetTouchCallback(int index);

	public static BetterList<UICamera> list = new BetterList<UICamera>();

	public static GetKeyStateFunc GetKeyDown = Input.GetKeyDown;

	public static GetKeyStateFunc GetKeyUp = Input.GetKeyUp;

	public static GetKeyStateFunc GetKey = Input.GetKey;

	public static GetAxisFunc GetAxis = Input.GetAxis;

	public static GetAnyKeyFunc GetAnyKeyDown;

	public static OnScreenResize onScreenResize;

	public EventType eventType = EventType.UI_3D;

	public bool eventsGoToColliders;

	public LayerMask eventReceiverMask = LayerMask.op_Implicit(-1);

	public bool debug;

	public bool useMouse = true;

	public bool useTouch = true;

	public bool allowMultiTouch = true;

	public bool useKeyboard = true;

	public bool useController = true;

	public bool stickyTooltip = true;

	public float tooltipDelay = 1f;

	public bool longPressTooltip;

	public float mouseDragThreshold = 4f;

	public float mouseClickThreshold = 10f;

	public float touchDragThreshold = 40f;

	public float touchClickThreshold = 40f;

	public float rangeDistance = -1f;

	public string horizontalAxisName = "Horizontal";

	public string verticalAxisName = "Vertical";

	public string horizontalPanAxisName;

	public string verticalPanAxisName;

	public string scrollAxisName = "Mouse ScrollWheel";

	public bool commandClick = true;

	public KeyCode submitKey0 = 13;

	public KeyCode submitKey1 = 330;

	public KeyCode cancelKey0 = 27;

	public KeyCode cancelKey1 = 331;

	public static OnCustomInput onCustomInput;

	public static bool showTooltips = true;

	private static bool mDisableController = false;

	private static Vector2 mLastPos = Vector2.get_zero();

	public static Vector3 lastWorldPosition = Vector3.get_zero();

	public static RaycastHit lastHit;

	public static UICamera current = null;

	public static Camera currentCamera = null;

	public static OnSchemeChange onSchemeChange;

	public static int currentTouchID = -100;

	private static KeyCode mCurrentKey = 48;

	public static MouseOrTouch currentTouch = null;

	private static bool mInputFocus = false;

	private static GameObject mGenericHandler;

	public static GameObject fallThrough;

	public static VoidDelegate onClick;

	public static VoidDelegate onDoubleClick;

	public static BoolDelegate onHover;

	public static BoolDelegate onPress;

	public static BoolDelegate onSelect;

	public static FloatDelegate onScroll;

	public static VectorDelegate onDrag;

	public static VoidDelegate onDragStart;

	public static ObjectDelegate onDragOver;

	public static ObjectDelegate onDragOut;

	public static VoidDelegate onDragEnd;

	public static ObjectDelegate onDrop;

	public static KeyCodeDelegate onKey;

	public static KeyCodeDelegate onNavigate;

	public static VectorDelegate onPan;

	public static BoolDelegate onTooltip;

	public static MoveDelegate onMouseMove;

	private static MouseOrTouch[] mMouse = new MouseOrTouch[3]
	{
		new MouseOrTouch(),
		new MouseOrTouch(),
		new MouseOrTouch()
	};

	public static MouseOrTouch controller = new MouseOrTouch();

	public static List<MouseOrTouch> activeTouches = new List<MouseOrTouch>();

	private static List<int> mTouchIDs = new List<int>();

	private static int mWidth = 0;

	private static int mHeight = 0;

	private static GameObject mTooltip = null;

	private Camera mCam;

	private static float mTooltipTime = 0f;

	private float mNextRaycast;

	public static bool isDragging = false;

	private static GameObject mRayHitObject;

	private static GameObject mHover;

	private static GameObject mSelected;

	private static DepthEntry mHit = default(DepthEntry);

	private static BetterList<DepthEntry> mHits = new BetterList<DepthEntry>();

	private static Plane m2DPlane = new Plane(Vector3.get_back(), 0f);

	private static float mNextEvent = 0f;

	private static int mNotifying = 0;

	private static bool mUsingTouchEvents = true;

	public static GetTouchCountCallback GetInputTouchCount;

	public static GetTouchCallback GetInputTouch;

	[Obsolete("Use new OnDragStart / OnDragOver / OnDragOut / OnDragEnd events instead")]
	public bool stickyPress
	{
		get
		{
			return true;
		}
	}

	public static bool disableController
	{
		get
		{
			return mDisableController && UIPopupList.current == null;
		}
		set
		{
			mDisableController = value;
		}
	}

	[Obsolete("Use lastEventPosition instead. It handles controller input properly.")]
	public static Vector2 lastTouchPosition
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return mLastPos;
		}
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			mLastPos = value;
		}
	}

	public static Vector2 lastEventPosition
	{
		get
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			ControlScheme currentScheme = UICamera.currentScheme;
			if (currentScheme == ControlScheme.Controller)
			{
				GameObject hoveredObject = UICamera.hoveredObject;
				if (hoveredObject != null)
				{
					Bounds val = NGUIMath.CalculateAbsoluteWidgetBounds(hoveredObject.get_transform());
					Camera val2 = NGUITools.FindCameraForLayer(hoveredObject.get_layer());
					return Vector2.op_Implicit(val2.WorldToScreenPoint(val.get_center()));
				}
			}
			return mLastPos;
		}
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			mLastPos = value;
		}
	}

	public static ControlScheme currentScheme
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Invalid comparison between Unknown and I4
			if ((int)mCurrentKey == 0)
			{
				return ControlScheme.Touch;
			}
			if ((int)mCurrentKey >= 330)
			{
				return ControlScheme.Controller;
			}
			return ControlScheme.Mouse;
		}
		set
		{
			switch (value)
			{
			case ControlScheme.Mouse:
				currentKey = 323;
				break;
			case ControlScheme.Controller:
				currentKey = 330;
				break;
			case ControlScheme.Touch:
				currentKey = 0;
				break;
			default:
				currentKey = 48;
				break;
			}
		}
	}

	public static KeyCode currentKey
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return mCurrentKey;
		}
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			if (mCurrentKey != value)
			{
				ControlScheme currentScheme = UICamera.currentScheme;
				mCurrentKey = value;
				ControlScheme currentScheme2 = UICamera.currentScheme;
				if (currentScheme != currentScheme2)
				{
					HideTooltip();
					if (currentScheme2 == ControlScheme.Mouse)
					{
						Cursor.set_lockState(1);
						Cursor.set_visible(true);
					}
					else
					{
						Cursor.set_visible(false);
						Cursor.set_lockState(0);
						mMouse[0].ignoreDelta = 2;
					}
					if (onSchemeChange != null)
					{
						onSchemeChange();
					}
				}
			}
		}
	}

	public static Ray currentRay => (!(currentCamera != null) || currentTouch == null) ? default(Ray) : currentCamera.ScreenPointToRay(Vector2.op_Implicit(currentTouch.pos));

	public static bool inputHasFocus
	{
		get
		{
			if (mInputFocus)
			{
				if (Object.op_Implicit(mSelected) && mSelected.get_activeInHierarchy())
				{
					return true;
				}
				mInputFocus = false;
			}
			return false;
		}
	}

	[Obsolete("Use delegates instead such as UICamera.onClick, UICamera.onHover, etc.")]
	public static GameObject genericEventHandler
	{
		get
		{
			return mGenericHandler;
		}
		set
		{
			mGenericHandler = value;
		}
	}

	private bool handlesEvents => eventHandler == this;

	public Camera cachedCamera
	{
		get
		{
			if (mCam == null)
			{
				mCam = this.GetComponent<Camera>();
			}
			return mCam;
		}
	}

	public static GameObject tooltipObject => mTooltip;

	public static bool isOverUI
	{
		get
		{
			if (currentTouch != null)
			{
				return currentTouch.isOverUI;
			}
			if (mHover == null)
			{
				return false;
			}
			if (mHover == fallThrough)
			{
				return false;
			}
			return NGUITools.FindInParents<UIRoot>(mHover) != null;
		}
	}

	public static GameObject hoveredObject
	{
		get
		{
			if (currentTouch != null && currentTouch.dragStarted)
			{
				return currentTouch.current;
			}
			if (Object.op_Implicit(mHover) && mHover.get_activeInHierarchy())
			{
				return mHover;
			}
			mHover = null;
			return null;
		}
		set
		{
			if (!(mHover == value))
			{
				bool flag = false;
				UICamera uICamera = current;
				if (currentTouch == null)
				{
					flag = true;
					currentTouchID = -100;
					currentTouch = controller;
				}
				ShowTooltip(null);
				if (Object.op_Implicit(mSelected) && currentScheme == ControlScheme.Controller)
				{
					Notify(mSelected, "OnSelect", false);
					if (onSelect != null)
					{
						onSelect(mSelected, false);
					}
					mSelected = null;
				}
				if (Object.op_Implicit(mHover))
				{
					Notify(mHover, "OnHover", false);
					if (onHover != null)
					{
						onHover(mHover, false);
					}
				}
				mHover = value;
				currentTouch.clickNotification = ClickNotification.None;
				if (Object.op_Implicit(mHover))
				{
					if (mHover != controller.current && mHover.GetComponent<UIKeyNavigation>() != null)
					{
						controller.current = mHover;
					}
					if (flag)
					{
						UICamera uICamera2 = (!(mHover != null)) ? list[0] : FindCameraForLayer(mHover.get_layer());
						if (uICamera2 != null)
						{
							current = uICamera2;
							currentCamera = uICamera2.cachedCamera;
						}
					}
					if (onHover != null)
					{
						onHover(mHover, true);
					}
					Notify(mHover, "OnHover", true);
				}
				if (flag)
				{
					current = uICamera;
					currentCamera = ((!(uICamera != null)) ? null : uICamera.cachedCamera);
					currentTouch = null;
					currentTouchID = -100;
				}
			}
		}
	}

	public static GameObject controllerNavigationObject
	{
		get
		{
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Expected O, but got Unknown
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_0116: Expected O, but got Unknown
			if (Object.op_Implicit(controller.current) && controller.current.get_activeInHierarchy())
			{
				return controller.current;
			}
			if (currentScheme == ControlScheme.Controller && current != null && current.useController && UIKeyNavigation.list.size > 0)
			{
				for (int i = 0; i < UIKeyNavigation.list.size; i++)
				{
					UIKeyNavigation uIKeyNavigation = UIKeyNavigation.list[i];
					if (Object.op_Implicit(uIKeyNavigation) && uIKeyNavigation.constraint != UIKeyNavigation.Constraint.Explicit && uIKeyNavigation.startsSelected)
					{
						hoveredObject = uIKeyNavigation.get_gameObject();
						controller.current = mHover;
						return mHover;
					}
				}
				if (mHover == null)
				{
					for (int j = 0; j < UIKeyNavigation.list.size; j++)
					{
						UIKeyNavigation uIKeyNavigation2 = UIKeyNavigation.list[j];
						if (Object.op_Implicit(uIKeyNavigation2) && uIKeyNavigation2.constraint != UIKeyNavigation.Constraint.Explicit)
						{
							hoveredObject = uIKeyNavigation2.get_gameObject();
							controller.current = mHover;
							return mHover;
						}
					}
				}
			}
			controller.current = null;
			return null;
		}
		set
		{
			if (controller.current != value && Object.op_Implicit(controller.current))
			{
				Notify(controller.current, "OnHover", false);
				if (onHover != null)
				{
					onHover(controller.current, false);
				}
				controller.current = null;
			}
			hoveredObject = value;
		}
	}

	public static GameObject selectedObject
	{
		get
		{
			if (Object.op_Implicit(mSelected) && mSelected.get_activeInHierarchy())
			{
				return mSelected;
			}
			mSelected = null;
			return null;
		}
		set
		{
			if (mSelected == value)
			{
				hoveredObject = value;
				controller.current = value;
			}
			else
			{
				ShowTooltip(null);
				bool flag = false;
				UICamera uICamera = current;
				if (currentTouch == null)
				{
					flag = true;
					currentTouchID = -100;
					currentTouch = controller;
				}
				mInputFocus = false;
				if (Object.op_Implicit(mSelected))
				{
					Notify(mSelected, "OnSelect", false);
					if (onSelect != null)
					{
						onSelect(mSelected, false);
					}
				}
				mSelected = value;
				currentTouch.clickNotification = ClickNotification.None;
				if (value != null)
				{
					UIKeyNavigation component = value.GetComponent<UIKeyNavigation>();
					if (component != null)
					{
						controller.current = value;
					}
				}
				if (Object.op_Implicit(mSelected) && flag)
				{
					UICamera uICamera2 = (!(mSelected != null)) ? list[0] : FindCameraForLayer(mSelected.get_layer());
					if (uICamera2 != null)
					{
						current = uICamera2;
						currentCamera = uICamera2.cachedCamera;
					}
				}
				if (Object.op_Implicit(mSelected))
				{
					mInputFocus = (mSelected.get_activeInHierarchy() && mSelected.GetComponent<UIInput>() != null);
					if (onSelect != null)
					{
						onSelect(mSelected, true);
					}
					Notify(mSelected, "OnSelect", true);
				}
				if (flag)
				{
					current = uICamera;
					currentCamera = ((!(uICamera != null)) ? null : uICamera.cachedCamera);
					currentTouch = null;
					currentTouchID = -100;
				}
			}
		}
	}

	[Obsolete("Use either 'CountInputSources()' or 'activeTouches.Count'")]
	public static int touchCount
	{
		get
		{
			return CountInputSources();
		}
	}

	public static int dragCount
	{
		get
		{
			int num = 0;
			int i = 0;
			for (int count = activeTouches.Count; i < count; i++)
			{
				MouseOrTouch mouseOrTouch = activeTouches[i];
				if (mouseOrTouch.dragged != null)
				{
					num++;
				}
			}
			for (int j = 0; j < mMouse.Length; j++)
			{
				if (mMouse[j].dragged != null)
				{
					num++;
				}
			}
			if (controller.dragged != null)
			{
				num++;
			}
			return num;
		}
	}

	public static Camera mainCamera
	{
		get
		{
			UICamera eventHandler = UICamera.eventHandler;
			return (!(eventHandler != null)) ? null : eventHandler.cachedCamera;
		}
	}

	public static UICamera eventHandler
	{
		get
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			for (int i = 0; i < list.size; i++)
			{
				UICamera uICamera = list.buffer[i];
				if (!(uICamera == null) && uICamera.get_enabled() && NGUITools.GetActive(uICamera.get_gameObject()))
				{
					return uICamera;
				}
			}
			return null;
		}
	}

	public UICamera()
		: this()
	{
	}//IL_0009: Unknown result type (might be due to invalid IL or missing references)
	//IL_000e: Unknown result type (might be due to invalid IL or missing references)
	//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
	//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
	//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
	//IL_00c8: Unknown result type (might be due to invalid IL or missing references)


	public static bool IsPressed(GameObject go)
	{
		for (int i = 0; i < 3; i++)
		{
			if (mMouse[i].pressed == go)
			{
				return true;
			}
		}
		int j = 0;
		for (int count = activeTouches.Count; j < count; j++)
		{
			MouseOrTouch mouseOrTouch = activeTouches[j];
			if (mouseOrTouch.pressed == go)
			{
				return true;
			}
		}
		if (controller.pressed == go)
		{
			return true;
		}
		return false;
	}

	public static int CountInputSources()
	{
		int num = 0;
		int i = 0;
		for (int count = activeTouches.Count; i < count; i++)
		{
			MouseOrTouch mouseOrTouch = activeTouches[i];
			if (mouseOrTouch.pressed != null)
			{
				num++;
			}
		}
		for (int j = 0; j < mMouse.Length; j++)
		{
			if (mMouse[j].pressed != null)
			{
				num++;
			}
		}
		if (controller.pressed != null)
		{
			num++;
		}
		return num;
	}

	private static int CompareFunc(UICamera a, UICamera b)
	{
		if (a.cachedCamera.get_depth() < b.cachedCamera.get_depth())
		{
			return 1;
		}
		if (a.cachedCamera.get_depth() > b.cachedCamera.get_depth())
		{
			return -1;
		}
		return 0;
	}

	private static Rigidbody FindRootRigidbody(Transform trans)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		while (trans != null)
		{
			if (trans.GetComponent<UIPanel>() != null)
			{
				return null;
			}
			Rigidbody component = trans.GetComponent<Rigidbody>();
			if (component != null)
			{
				return component;
			}
			trans = trans.get_parent();
		}
		return null;
	}

	private static Rigidbody2D FindRootRigidbody2D(Transform trans)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		while (trans != null)
		{
			if (trans.GetComponent<UIPanel>() != null)
			{
				return null;
			}
			Rigidbody2D component = trans.GetComponent<Rigidbody2D>();
			if (component != null)
			{
				return component;
			}
			trans = trans.get_parent();
		}
		return null;
	}

	public static void Raycast(MouseOrTouch touch)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (!Raycast(Vector2.op_Implicit(touch.pos)))
		{
			mRayHitObject = fallThrough;
		}
		if (mRayHitObject == null)
		{
			mRayHitObject = mGenericHandler;
		}
		touch.last = touch.current;
		touch.current = mRayHitObject;
		mLastPos = touch.pos;
	}

	public static bool Raycast(Vector3 inPos)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Expected O, but got Unknown
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Expected O, but got Unknown
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Expected O, but got Unknown
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Expected O, but got Unknown
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Expected O, but got Unknown
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Expected O, but got Unknown
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Expected O, but got Unknown
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Expected O, but got Unknown
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Expected O, but got Unknown
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Expected O, but got Unknown
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < list.size; i++)
		{
			UICamera uICamera = list.buffer[i];
			if (uICamera.get_enabled() && NGUITools.GetActive(uICamera.get_gameObject()))
			{
				currentCamera = uICamera.cachedCamera;
				Vector3 val = currentCamera.ScreenToViewportPoint(inPos);
				if (!float.IsNaN(val.x) && !float.IsNaN(val.y) && !(val.x < 0f) && !(val.x > 1f) && !(val.y < 0f) && !(val.y > 1f))
				{
					Ray val2 = currentCamera.ScreenPointToRay(inPos);
					int num = currentCamera.get_cullingMask() & LayerMask.op_Implicit(uICamera.eventReceiverMask);
					float num2 = (!(uICamera.rangeDistance > 0f)) ? (currentCamera.get_farClipPlane() - currentCamera.get_nearClipPlane()) : uICamera.rangeDistance;
					if (uICamera.eventType == EventType.World_3D)
					{
						if (Physics.Raycast(val2, ref lastHit, num2, num))
						{
							lastWorldPosition = lastHit.get_point();
							mRayHitObject = lastHit.get_collider().get_gameObject();
							if (!list[0].eventsGoToColliders)
							{
								Rigidbody val3 = FindRootRigidbody(mRayHitObject.get_transform());
								if (val3 != null)
								{
									mRayHitObject = val3.get_gameObject();
								}
							}
							return true;
						}
					}
					else if (uICamera.eventType == EventType.UI_3D)
					{
						RaycastHit[] array = Physics.RaycastAll(val2, num2, num);
						if (array.Length > 1)
						{
							for (int j = 0; j < array.Length; j++)
							{
								GameObject val4 = array[j].get_collider().get_gameObject();
								UIWidget component = val4.GetComponent<UIWidget>();
								if (component != null)
								{
									if (!component.isVisible || (component.hitCheck != null && !component.hitCheck(array[j].get_point())))
									{
										continue;
									}
								}
								else
								{
									UIRect uIRect = NGUITools.FindInParents<UIRect>(val4);
									if (uIRect != null && uIRect.finalAlpha < 0.001f)
									{
										continue;
									}
								}
								mHit.depth = NGUITools.CalculateRaycastDepth(val4);
								if (mHit.depth != 2147483647)
								{
									mHit.hit = array[j];
									mHit.point = array[j].get_point();
									mHit.go = array[j].get_collider().get_gameObject();
									mHits.Add(mHit);
								}
							}
							mHits.Sort((DepthEntry r1, DepthEntry r2) => r2.depth.CompareTo(r1.depth));
							for (int k = 0; k < mHits.size; k++)
							{
								if (IsVisible(ref mHits.buffer[k]))
								{
									DepthEntry depthEntry = mHits[k];
									lastHit = depthEntry.hit;
									DepthEntry depthEntry2 = mHits[k];
									mRayHitObject = depthEntry2.go;
									DepthEntry depthEntry3 = mHits[k];
									lastWorldPosition = depthEntry3.point;
									mHits.Clear();
									return true;
								}
							}
							mHits.Clear();
						}
						else if (array.Length == 1)
						{
							GameObject val5 = array[0].get_collider().get_gameObject();
							UIWidget component2 = val5.GetComponent<UIWidget>();
							if (component2 != null)
							{
								if (!component2.isVisible || (component2.hitCheck != null && !component2.hitCheck(array[0].get_point())))
								{
									continue;
								}
							}
							else
							{
								UIRect uIRect2 = NGUITools.FindInParents<UIRect>(val5);
								if (uIRect2 != null && uIRect2.finalAlpha < 0.001f)
								{
									continue;
								}
							}
							if (IsVisible(array[0].get_point(), array[0].get_collider().get_gameObject()))
							{
								lastHit = array[0];
								lastWorldPosition = array[0].get_point();
								mRayHitObject = lastHit.get_collider().get_gameObject();
								return true;
							}
						}
					}
					else if (uICamera.eventType == EventType.World_2D)
					{
						if (m2DPlane.Raycast(val2, ref num2))
						{
							Vector3 point = val2.GetPoint(num2);
							Collider2D val6 = Physics2D.OverlapPoint(Vector2.op_Implicit(point), num);
							if (Object.op_Implicit(val6))
							{
								lastWorldPosition = point;
								mRayHitObject = val6.get_gameObject();
								if (!uICamera.eventsGoToColliders)
								{
									Rigidbody2D val7 = FindRootRigidbody2D(mRayHitObject.get_transform());
									if (val7 != null)
									{
										mRayHitObject = val7.get_gameObject();
									}
								}
								return true;
							}
						}
					}
					else if (uICamera.eventType == EventType.UI_2D && m2DPlane.Raycast(val2, ref num2))
					{
						lastWorldPosition = val2.GetPoint(num2);
						Collider2D[] array2 = Physics2D.OverlapPointAll(Vector2.op_Implicit(lastWorldPosition), num);
						if (array2.Length > 1)
						{
							for (int l = 0; l < array2.Length; l++)
							{
								GameObject val8 = array2[l].get_gameObject();
								UIWidget component3 = val8.GetComponent<UIWidget>();
								if (component3 != null)
								{
									if (!component3.isVisible || (component3.hitCheck != null && !component3.hitCheck(lastWorldPosition)))
									{
										continue;
									}
								}
								else
								{
									UIRect uIRect3 = NGUITools.FindInParents<UIRect>(val8);
									if (uIRect3 != null && uIRect3.finalAlpha < 0.001f)
									{
										continue;
									}
								}
								mHit.depth = NGUITools.CalculateRaycastDepth(val8);
								if (mHit.depth != 2147483647)
								{
									mHit.go = val8;
									mHit.point = lastWorldPosition;
									mHits.Add(mHit);
								}
							}
							mHits.Sort((DepthEntry r1, DepthEntry r2) => r2.depth.CompareTo(r1.depth));
							for (int m = 0; m < mHits.size; m++)
							{
								if (IsVisible(ref mHits.buffer[m]))
								{
									DepthEntry depthEntry4 = mHits[m];
									mRayHitObject = depthEntry4.go;
									mHits.Clear();
									return true;
								}
							}
							mHits.Clear();
						}
						else if (array2.Length == 1)
						{
							GameObject val9 = array2[0].get_gameObject();
							UIWidget component4 = val9.GetComponent<UIWidget>();
							if (component4 != null)
							{
								if (!component4.isVisible || (component4.hitCheck != null && !component4.hitCheck(lastWorldPosition)))
								{
									continue;
								}
							}
							else
							{
								UIRect uIRect4 = NGUITools.FindInParents<UIRect>(val9);
								if (uIRect4 != null && uIRect4.finalAlpha < 0.001f)
								{
									continue;
								}
							}
							if (IsVisible(lastWorldPosition, val9))
							{
								mRayHitObject = val9;
								return true;
							}
						}
					}
				}
			}
		}
		return false;
	}

	private static bool IsVisible(Vector3 worldPoint, GameObject go)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(go);
		while (uIPanel != null)
		{
			if (!uIPanel.IsVisible(worldPoint))
			{
				return false;
			}
			uIPanel = uIPanel.parentPanel;
		}
		return true;
	}

	private static bool IsVisible(ref DepthEntry de)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(de.go);
		while (uIPanel != null)
		{
			if (!uIPanel.IsVisible(de.point))
			{
				return false;
			}
			uIPanel = uIPanel.parentPanel;
		}
		return true;
	}

	public static bool IsHighlighted(GameObject go)
	{
		return hoveredObject == go;
	}

	public static UICamera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		for (int i = 0; i < list.size; i++)
		{
			UICamera uICamera = list.buffer[i];
			Camera cachedCamera = uICamera.cachedCamera;
			if (cachedCamera != null && (cachedCamera.get_cullingMask() & num) != 0)
			{
				return uICamera;
			}
		}
		return null;
	}

	private static int GetDirection(KeyCode up, KeyCode down)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (GetKeyDown(up))
		{
			currentKey = up;
			return 1;
		}
		if (GetKeyDown(down))
		{
			currentKey = down;
			return -1;
		}
		return 0;
	}

	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (GetKeyDown(up0))
		{
			currentKey = up0;
			return 1;
		}
		if (GetKeyDown(up1))
		{
			currentKey = up1;
			return 1;
		}
		if (GetKeyDown(down0))
		{
			currentKey = down0;
			return -1;
		}
		if (GetKeyDown(down1))
		{
			currentKey = down1;
			return -1;
		}
		return 0;
	}

	private static int GetDirection(string axis)
	{
		float time = RealTime.time;
		if (mNextEvent < time && !string.IsNullOrEmpty(axis))
		{
			float num = GetAxis(axis);
			if (num > 0.75f)
			{
				currentKey = 330;
				mNextEvent = time + 0.25f;
				return 1;
			}
			if (num < -0.75f)
			{
				currentKey = 330;
				mNextEvent = time + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	public static void Notify(GameObject go, string funcName, object obj)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		if (mNotifying <= 10)
		{
			if (currentScheme == ControlScheme.Controller && UIPopupList.isOpen && UIPopupList.current.source == go && UIPopupList.isOpen)
			{
				go = UIPopupList.current.get_gameObject();
			}
			if (Object.op_Implicit(go) && go.get_activeInHierarchy())
			{
				mNotifying++;
				go.SendMessage(funcName, obj, 1);
				if (mGenericHandler != null && mGenericHandler != go)
				{
					mGenericHandler.SendMessage(funcName, obj, 1);
				}
				mNotifying--;
			}
		}
	}

	public static MouseOrTouch GetMouse(int button)
	{
		return mMouse[button];
	}

	public static MouseOrTouch GetTouch(int id, bool createIfMissing = false)
	{
		if (id < 0)
		{
			return GetMouse(-id - 1);
		}
		int i = 0;
		for (int count = mTouchIDs.Count; i < count; i++)
		{
			if (mTouchIDs[i] == id)
			{
				return activeTouches[i];
			}
		}
		if (createIfMissing)
		{
			MouseOrTouch mouseOrTouch = new MouseOrTouch();
			mouseOrTouch.pressTime = RealTime.time;
			mouseOrTouch.touchBegan = true;
			activeTouches.Add(mouseOrTouch);
			mTouchIDs.Add(id);
			return mouseOrTouch;
		}
		return null;
	}

	public static void RemoveTouch(int id)
	{
		int num = 0;
		int count = mTouchIDs.Count;
		while (true)
		{
			if (num >= count)
			{
				return;
			}
			if (mTouchIDs[num] == id)
			{
				break;
			}
			num++;
		}
		mTouchIDs.RemoveAt(num);
		activeTouches.RemoveAt(num);
	}

	private void Awake()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		mWidth = Screen.get_width();
		mHeight = Screen.get_height();
		mMouse[0].pos = Vector2.op_Implicit(Input.get_mousePosition());
		for (int i = 1; i < 3; i++)
		{
			mMouse[i].pos = mMouse[0].pos;
			mMouse[i].lastPos = mMouse[0].pos;
		}
		mLastPos = mMouse[0].pos;
	}

	private void OnEnable()
	{
		list.Add(this);
		list.Sort(CompareFunc);
	}

	private void OnDisable()
	{
		list.Remove(this);
	}

	private void Start()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		if (eventType != 0 && (int)cachedCamera.get_transparencySortMode() != 2)
		{
			cachedCamera.set_transparencySortMode(2);
		}
		if (Application.get_isPlaying())
		{
			if (fallThrough == null)
			{
				UIRoot uIRoot = NGUITools.FindInParents<UIRoot>(this.get_gameObject());
				if (uIRoot != null)
				{
					fallThrough = uIRoot.get_gameObject();
				}
				else
				{
					Transform val = this.get_transform();
					fallThrough = ((!(val.get_parent() != null)) ? this.get_gameObject() : val.get_parent().get_gameObject());
				}
			}
			cachedCamera.set_eventMask(0);
		}
	}

	private void Update()
	{
		if (handlesEvents)
		{
			current = this;
			NGUIDebug.debugRaycast = debug;
			if (useTouch)
			{
				ProcessTouches();
			}
			else if (useMouse)
			{
				ProcessMouse();
			}
			if (onCustomInput != null)
			{
				onCustomInput();
			}
			if ((useKeyboard || useController) && !disableController)
			{
				ProcessOthers();
			}
			if (useMouse && mHover != null)
			{
				float num = string.IsNullOrEmpty(scrollAxisName) ? 0f : GetAxis(scrollAxisName);
				if (num != 0f)
				{
					if (onScroll != null)
					{
						onScroll(mHover, num);
					}
					Notify(mHover, "OnScroll", num);
				}
				if (showTooltips && mTooltipTime != 0f && !UIPopupList.isOpen && (mTooltipTime < RealTime.time || GetKey(304) || GetKey(303)))
				{
					currentTouch = mMouse[0];
					currentTouchID = -1;
					ShowTooltip(mHover);
				}
			}
			if (mTooltip != null && !NGUITools.GetActive(mTooltip))
			{
				ShowTooltip(null);
			}
			current = null;
			currentTouchID = -100;
		}
	}

	private void LateUpdate()
	{
		if (handlesEvents)
		{
			int width = Screen.get_width();
			int height = Screen.get_height();
			if (width != mWidth || height != mHeight)
			{
				mWidth = width;
				mHeight = height;
				UIRoot.Broadcast("UpdateAnchors");
				if (onScreenResize != null)
				{
					onScreenResize();
				}
			}
		}
	}

	public void ProcessMouse()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < 3; i++)
		{
			if (Input.GetMouseButtonDown(i))
			{
				currentKey = 323 + i;
				flag2 = true;
				flag = true;
			}
			else if (Input.GetMouseButton(i))
			{
				currentKey = 323 + i;
				flag = true;
			}
		}
		if (currentScheme != ControlScheme.Touch)
		{
			currentTouch = mMouse[0];
			Vector2 val = Vector2.op_Implicit(Input.get_mousePosition());
			if (currentTouch.ignoreDelta == 0)
			{
				currentTouch.delta = val - currentTouch.pos;
			}
			else
			{
				currentTouch.ignoreDelta--;
				currentTouch.delta.x = 0f;
				currentTouch.delta.y = 0f;
			}
			float sqrMagnitude = currentTouch.delta.get_sqrMagnitude();
			currentTouch.pos = val;
			mLastPos = val;
			bool flag3 = false;
			if (currentScheme != 0)
			{
				if (sqrMagnitude < 0.001f)
				{
					return;
				}
				currentKey = 323;
				flag3 = true;
			}
			else if (sqrMagnitude > 0.001f)
			{
				flag3 = true;
			}
			for (int j = 1; j < 3; j++)
			{
				mMouse[j].pos = currentTouch.pos;
				mMouse[j].delta = currentTouch.delta;
			}
			if (flag || flag3 || mNextRaycast < RealTime.time)
			{
				mNextRaycast = RealTime.time + 0.02f;
				Raycast(currentTouch);
				for (int k = 0; k < 3; k++)
				{
					mMouse[k].current = currentTouch.current;
				}
			}
			bool flag4 = currentTouch.last != currentTouch.current;
			bool flag5 = currentTouch.pressed != null;
			if (!flag5)
			{
				hoveredObject = currentTouch.current;
			}
			currentTouchID = -1;
			if (flag4)
			{
				currentKey = 323;
			}
			if (!flag && flag3 && (!stickyTooltip || flag4))
			{
				if (mTooltipTime != 0f)
				{
					mTooltipTime = Time.get_unscaledTime() + tooltipDelay;
				}
				else if (mTooltip != null)
				{
					ShowTooltip(null);
				}
			}
			if (flag3 && onMouseMove != null)
			{
				onMouseMove(currentTouch.delta);
				currentTouch = null;
			}
			if (flag4 && (flag2 || (flag5 && !flag)))
			{
				hoveredObject = null;
			}
			for (int l = 0; l < 3; l++)
			{
				bool mouseButtonDown = Input.GetMouseButtonDown(l);
				bool mouseButtonUp = Input.GetMouseButtonUp(l);
				if (mouseButtonDown || mouseButtonUp)
				{
					currentKey = 323 + l;
				}
				currentTouch = mMouse[l];
				currentTouchID = -1 - l;
				currentKey = 323 + l;
				if (mouseButtonDown)
				{
					currentTouch.pressedCam = currentCamera;
					currentTouch.pressTime = RealTime.time;
				}
				else if (currentTouch.pressed != null)
				{
					currentCamera = currentTouch.pressedCam;
				}
				ProcessTouch(mouseButtonDown, mouseButtonUp);
			}
			if (!flag && flag4)
			{
				currentTouch = mMouse[0];
				mTooltipTime = RealTime.time + tooltipDelay;
				currentTouchID = -1;
				currentKey = 323;
				hoveredObject = currentTouch.current;
			}
			currentTouch = null;
			mMouse[0].last = mMouse[0].current;
			for (int m = 1; m < 3; m++)
			{
				mMouse[m].last = mMouse[0].last;
			}
		}
	}

	public void ProcessTouches()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Invalid comparison between Unknown and I4
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Invalid comparison between Unknown and I4
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		int num = (GetInputTouchCount != null) ? GetInputTouchCount() : Input.get_touchCount();
		for (int i = 0; i < num; i++)
		{
			TouchPhase phase;
			int fingerId;
			Vector2 position;
			int tapCount;
			if (GetInputTouch == null)
			{
				Touch touch = Input.GetTouch(i);
				phase = touch.get_phase();
				fingerId = touch.get_fingerId();
				position = touch.get_position();
				tapCount = touch.get_tapCount();
			}
			else
			{
				Touch touch2 = GetInputTouch(i);
				phase = touch2.phase;
				fingerId = touch2.fingerId;
				position = touch2.position;
				tapCount = touch2.tapCount;
			}
			currentTouchID = ((!allowMultiTouch) ? 1 : fingerId);
			currentTouch = GetTouch(currentTouchID, true);
			bool flag = (int)phase == 0 || currentTouch.touchBegan;
			bool flag2 = (int)phase == 4 || (int)phase == 3;
			currentTouch.touchBegan = false;
			currentTouch.delta = position - currentTouch.pos;
			currentTouch.pos = position;
			currentKey = 0;
			Raycast(currentTouch);
			if (flag)
			{
				currentTouch.pressedCam = currentCamera;
			}
			else if (currentTouch.pressed != null)
			{
				currentCamera = currentTouch.pressedCam;
			}
			if (tapCount > 1)
			{
				currentTouch.clickTime = RealTime.time;
			}
			ProcessTouch(flag, flag2);
			if (flag2)
			{
				RemoveTouch(currentTouchID);
			}
			currentTouch.last = null;
			currentTouch = null;
			if (!allowMultiTouch)
			{
				break;
			}
		}
		if (num == 0)
		{
			if (mUsingTouchEvents)
			{
				mUsingTouchEvents = false;
			}
			else if (useMouse)
			{
				ProcessMouse();
			}
		}
		else
		{
			mUsingTouchEvents = true;
		}
	}

	private void ProcessFakeTouches()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		bool mouseButton = Input.GetMouseButton(0);
		if (mouseButtonDown || mouseButtonUp || mouseButton)
		{
			currentTouchID = 1;
			currentTouch = mMouse[0];
			currentTouch.touchBegan = mouseButtonDown;
			if (mouseButtonDown)
			{
				currentTouch.pressTime = RealTime.time;
				activeTouches.Add(currentTouch);
			}
			Vector2 val = Vector2.op_Implicit(Input.get_mousePosition());
			currentTouch.delta = val - currentTouch.pos;
			currentTouch.pos = val;
			Raycast(currentTouch);
			if (mouseButtonDown)
			{
				currentTouch.pressedCam = currentCamera;
			}
			else if (currentTouch.pressed != null)
			{
				currentCamera = currentTouch.pressedCam;
			}
			currentKey = 0;
			ProcessTouch(mouseButtonDown, mouseButtonUp);
			if (mouseButtonUp)
			{
				activeTouches.Remove(currentTouch);
			}
			currentTouch.last = null;
			currentTouch = null;
		}
	}

	public void ProcessOthers()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Invalid comparison between Unknown and I4
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Invalid comparison between Unknown and I4
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Invalid comparison between Unknown and I4
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Invalid comparison between Unknown and I4
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Invalid comparison between Unknown and I4
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Invalid comparison between Unknown and I4
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Invalid comparison between Unknown and I4
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Invalid comparison between Unknown and I4
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		currentTouchID = -100;
		currentTouch = controller;
		bool flag = false;
		bool flag2 = false;
		if ((int)submitKey0 != 0 && GetKeyDown(submitKey0))
		{
			currentKey = submitKey0;
			flag = true;
		}
		else if ((int)submitKey1 != 0 && GetKeyDown(submitKey1))
		{
			currentKey = submitKey1;
			flag = true;
		}
		else if (((int)submitKey0 == 13 || (int)submitKey1 == 13) && GetKeyDown(271))
		{
			currentKey = submitKey0;
			flag = true;
		}
		if ((int)submitKey0 != 0 && GetKeyUp(submitKey0))
		{
			currentKey = submitKey0;
			flag2 = true;
		}
		else if ((int)submitKey1 != 0 && GetKeyUp(submitKey1))
		{
			currentKey = submitKey1;
			flag2 = true;
		}
		else if (((int)submitKey0 == 13 || (int)submitKey1 == 13) && GetKeyUp(271))
		{
			currentKey = submitKey0;
			flag2 = true;
		}
		if (flag)
		{
			currentTouch.pressTime = RealTime.time;
		}
		if ((flag || flag2) && currentScheme == ControlScheme.Controller)
		{
			currentTouch.current = controllerNavigationObject;
			ProcessTouch(flag, flag2);
			currentTouch.last = currentTouch.current;
		}
		KeyCode val = 0;
		if (useController)
		{
			if (!disableController && currentScheme == ControlScheme.Controller && (currentTouch.current == null || !currentTouch.current.get_activeInHierarchy()))
			{
				currentTouch.current = controllerNavigationObject;
			}
			if (!string.IsNullOrEmpty(verticalAxisName))
			{
				int direction = GetDirection(verticalAxisName);
				if (direction != 0)
				{
					ShowTooltip(null);
					currentScheme = ControlScheme.Controller;
					currentTouch.current = controllerNavigationObject;
					if (currentTouch.current != null)
					{
						val = ((direction <= 0) ? 274 : 273);
						if (onNavigate != null)
						{
							onNavigate(currentTouch.current, val);
						}
						Notify(currentTouch.current, "OnNavigate", val);
					}
				}
			}
			if (!string.IsNullOrEmpty(horizontalAxisName))
			{
				int direction2 = GetDirection(horizontalAxisName);
				if (direction2 != 0)
				{
					ShowTooltip(null);
					currentScheme = ControlScheme.Controller;
					currentTouch.current = controllerNavigationObject;
					if (currentTouch.current != null)
					{
						val = ((direction2 <= 0) ? 276 : 275);
						if (onNavigate != null)
						{
							onNavigate(currentTouch.current, val);
						}
						Notify(currentTouch.current, "OnNavigate", val);
					}
				}
			}
			float num = string.IsNullOrEmpty(horizontalPanAxisName) ? 0f : GetAxis(horizontalPanAxisName);
			float num2 = string.IsNullOrEmpty(verticalPanAxisName) ? 0f : GetAxis(verticalPanAxisName);
			if (num != 0f || num2 != 0f)
			{
				ShowTooltip(null);
				currentScheme = ControlScheme.Controller;
				currentTouch.current = controllerNavigationObject;
				if (currentTouch.current != null)
				{
					Vector2 val2 = default(Vector2);
					val2._002Ector(num, num2);
					val2 *= Time.get_unscaledDeltaTime();
					if (onPan != null)
					{
						onPan(currentTouch.current, val2);
					}
					Notify(currentTouch.current, "OnPan", val2);
				}
			}
		}
		if (Input.get_anyKeyDown())
		{
			int i = 0;
			for (int num3 = NGUITools.keys.Length; i < num3; i++)
			{
				KeyCode val3 = NGUITools.keys[i];
				if (val != val3 && GetKeyDown(val3) && (useKeyboard || (int)val3 >= 323) && (useController || (int)val3 < 330) && (useMouse || ((int)val3 < 323 && (int)val3 > 329)))
				{
					currentKey = val3;
					if (onKey != null)
					{
						onKey(currentTouch.current, val3);
					}
					Notify(currentTouch.current, "OnKey", val3);
				}
			}
		}
		currentTouch = null;
	}

	private void ProcessPress(bool pressed, float click, float drag)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		if (pressed)
		{
			if (mTooltip != null)
			{
				ShowTooltip(null);
			}
			currentTouch.pressStarted = true;
			if (onPress != null && Object.op_Implicit(currentTouch.pressed))
			{
				onPress(currentTouch.pressed, false);
			}
			Notify(currentTouch.pressed, "OnPress", false);
			currentTouch.pressed = currentTouch.current;
			currentTouch.dragged = currentTouch.current;
			currentTouch.clickNotification = ClickNotification.BasedOnDelta;
			currentTouch.totalDelta = Vector2.get_zero();
			currentTouch.dragStarted = false;
			if (onPress != null && Object.op_Implicit(currentTouch.pressed))
			{
				onPress(currentTouch.pressed, true);
			}
			Notify(currentTouch.pressed, "OnPress", true);
			if (mTooltip != null)
			{
				ShowTooltip(null);
			}
			if (mSelected != currentTouch.pressed)
			{
				mInputFocus = false;
				if (Object.op_Implicit(mSelected))
				{
					Notify(mSelected, "OnSelect", false);
					if (onSelect != null)
					{
						onSelect(mSelected, false);
					}
				}
				mSelected = currentTouch.pressed;
				if (currentTouch.pressed != null)
				{
					UIKeyNavigation component = currentTouch.pressed.GetComponent<UIKeyNavigation>();
					if (component != null)
					{
						controller.current = currentTouch.pressed;
					}
				}
				if (Object.op_Implicit(mSelected))
				{
					mInputFocus = (mSelected.get_activeInHierarchy() && mSelected.GetComponent<UIInput>() != null);
					if (onSelect != null)
					{
						onSelect(mSelected, true);
					}
					Notify(mSelected, "OnSelect", true);
				}
			}
		}
		else if (currentTouch.pressed != null && (currentTouch.delta.get_sqrMagnitude() != 0f || currentTouch.current != currentTouch.last))
		{
			MouseOrTouch mouseOrTouch = currentTouch;
			mouseOrTouch.totalDelta += currentTouch.delta;
			float sqrMagnitude = currentTouch.totalDelta.get_sqrMagnitude();
			bool flag = false;
			if (!currentTouch.dragStarted && currentTouch.last != currentTouch.current)
			{
				currentTouch.dragStarted = true;
				currentTouch.delta = currentTouch.totalDelta;
				isDragging = true;
				if (onDragStart != null)
				{
					onDragStart(currentTouch.dragged);
				}
				Notify(currentTouch.dragged, "OnDragStart", null);
				if (onDragOver != null)
				{
					onDragOver(currentTouch.last, currentTouch.dragged);
				}
				Notify(currentTouch.last, "OnDragOver", currentTouch.dragged);
				isDragging = false;
			}
			else if (!currentTouch.dragStarted && drag < sqrMagnitude)
			{
				flag = true;
				currentTouch.dragStarted = true;
				currentTouch.delta = currentTouch.totalDelta;
			}
			if (currentTouch.dragStarted)
			{
				if (mTooltip != null)
				{
					ShowTooltip(null);
				}
				isDragging = true;
				bool flag2 = currentTouch.clickNotification == ClickNotification.None;
				if (flag)
				{
					if (onDragStart != null)
					{
						onDragStart(currentTouch.dragged);
					}
					Notify(currentTouch.dragged, "OnDragStart", null);
					if (onDragOver != null)
					{
						onDragOver(currentTouch.last, currentTouch.dragged);
					}
					Notify(currentTouch.current, "OnDragOver", currentTouch.dragged);
				}
				else if (currentTouch.last != currentTouch.current)
				{
					if (onDragOut != null)
					{
						onDragOut(currentTouch.last, currentTouch.dragged);
					}
					Notify(currentTouch.last, "OnDragOut", currentTouch.dragged);
					if (onDragOver != null)
					{
						onDragOver(currentTouch.last, currentTouch.dragged);
					}
					Notify(currentTouch.current, "OnDragOver", currentTouch.dragged);
				}
				if (onDrag != null)
				{
					onDrag(currentTouch.dragged, currentTouch.delta);
				}
				Notify(currentTouch.dragged, "OnDrag", currentTouch.delta);
				currentTouch.last = currentTouch.current;
				isDragging = false;
				if (flag2)
				{
					currentTouch.clickNotification = ClickNotification.None;
				}
				else if (currentTouch.clickNotification == ClickNotification.BasedOnDelta && click < sqrMagnitude)
				{
					currentTouch.clickNotification = ClickNotification.None;
				}
			}
		}
	}

	private void ProcessRelease(bool isMouse, float drag)
	{
		if (currentTouch != null)
		{
			currentTouch.pressStarted = false;
			if (currentTouch.pressed != null)
			{
				if (currentTouch.dragStarted)
				{
					if (onDragOut != null)
					{
						onDragOut(currentTouch.last, currentTouch.dragged);
					}
					Notify(currentTouch.last, "OnDragOut", currentTouch.dragged);
					if (onDragEnd != null)
					{
						onDragEnd(currentTouch.dragged);
					}
					Notify(currentTouch.dragged, "OnDragEnd", null);
				}
				if (onPress != null)
				{
					onPress(currentTouch.pressed, false);
				}
				Notify(currentTouch.pressed, "OnPress", false);
				if (isMouse && HasCollider(currentTouch.pressed))
				{
					if (mHover == currentTouch.current)
					{
						if (onHover != null)
						{
							onHover(currentTouch.current, true);
						}
						Notify(currentTouch.current, "OnHover", true);
					}
					else
					{
						hoveredObject = currentTouch.current;
					}
				}
				if (currentTouch.dragged == currentTouch.current || (currentScheme != ControlScheme.Controller && currentTouch.clickNotification != 0 && currentTouch.totalDelta.get_sqrMagnitude() < drag))
				{
					if (currentTouch.clickNotification != 0 && currentTouch.pressed == currentTouch.current)
					{
						ShowTooltip(null);
						float time = RealTime.time;
						if (TutorialMessage.IsActiveButton(currentTouch.pressed))
						{
							if (onClick != null)
							{
								onClick(currentTouch.pressed);
							}
							Notify(currentTouch.pressed, "OnClick", null);
						}
						if (currentTouch.clickTime + 0.35f > time)
						{
							if (onDoubleClick != null)
							{
								onDoubleClick(currentTouch.pressed);
							}
							Notify(currentTouch.pressed, "OnDoubleClick", null);
						}
						currentTouch.clickTime = time;
					}
				}
				else if (currentTouch.dragStarted)
				{
					if (onDrop != null)
					{
						onDrop(currentTouch.current, currentTouch.dragged);
					}
					Notify(currentTouch.current, "OnDrop", currentTouch.dragged);
				}
			}
			currentTouch.dragStarted = false;
			currentTouch.pressed = null;
			currentTouch.dragged = null;
		}
	}

	private bool HasCollider(GameObject go)
	{
		if (go == null)
		{
			return false;
		}
		Collider component = go.GetComponent<Collider>();
		if (component != null)
		{
			return component.get_enabled();
		}
		Collider2D component2 = go.GetComponent<Collider2D>();
		return component2 != null && component2.get_enabled();
	}

	public void ProcessTouch(bool pressed, bool released)
	{
		if (pressed)
		{
			mTooltipTime = Time.get_unscaledTime() + tooltipDelay;
		}
		bool flag = currentScheme == ControlScheme.Mouse;
		float num = (!flag) ? touchDragThreshold : mouseDragThreshold;
		float num2 = (!flag) ? touchClickThreshold : mouseClickThreshold;
		num *= num;
		num2 *= num2;
		if (currentTouch.pressed != null)
		{
			if (released)
			{
				ProcessRelease(flag, num);
			}
			ProcessPress(pressed, num2, num);
			if (currentTouch.pressed == currentTouch.current && mTooltipTime != 0f && currentTouch.clickNotification != 0 && !currentTouch.dragStarted && currentTouch.deltaTime > tooltipDelay)
			{
				mTooltipTime = 0f;
				currentTouch.clickNotification = ClickNotification.None;
				if (longPressTooltip)
				{
					ShowTooltip(currentTouch.pressed);
				}
				Notify(currentTouch.current, "OnLongPress", null);
			}
		}
		else if (flag || pressed || released)
		{
			ProcessPress(pressed, num2, num);
			if (released)
			{
				ProcessRelease(flag, num);
			}
		}
	}

	public static bool ShowTooltip(GameObject go)
	{
		if (mTooltip != go)
		{
			if (mTooltip != null)
			{
				if (onTooltip != null)
				{
					onTooltip(mTooltip, false);
				}
				Notify(mTooltip, "OnTooltip", false);
			}
			mTooltip = go;
			mTooltipTime = 0f;
			if (mTooltip != null)
			{
				if (onTooltip != null)
				{
					onTooltip(mTooltip, true);
				}
				Notify(mTooltip, "OnTooltip", true);
			}
			return true;
		}
		return false;
	}

	public static bool HideTooltip()
	{
		return ShowTooltip(null);
	}
}
