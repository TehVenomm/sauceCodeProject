using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Draggable Camera")]
[RequireComponent(typeof(Camera))]
public class UIDraggableCamera
{
	public Transform rootForBounds;

	public Vector2 scale = Vector2.get_one();

	public float scrollWheelFactor;

	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	public bool smoothDragStart = true;

	public float momentumAmount = 35f;

	private Camera mCam;

	private Transform mTrans;

	private bool mPressed;

	private Vector2 mMomentum = Vector2.get_zero();

	private Bounds mBounds;

	private float mScroll;

	private UIRoot mRoot;

	private bool mDragStarted;

	public Vector2 currentMomentum
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mMomentum;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			mMomentum = value;
		}
	}

	public UIDraggableCamera()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)
	//IL_0025: Unknown result type (might be due to invalid IL or missing references)
	//IL_002a: Unknown result type (might be due to invalid IL or missing references)


	private void Start()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		mCam = this.GetComponent<Camera>();
		mTrans = this.get_transform();
		mRoot = NGUITools.FindInParents<UIRoot>(this.get_gameObject());
		if (rootForBounds == null)
		{
			Debug.LogError((object)(NGUITools.GetHierarchy(this.get_gameObject()) + " needs the 'Root For Bounds' parameter to be set"), this);
			this.set_enabled(false);
		}
	}

	private Vector3 CalculateConstrainOffset()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		if (rootForBounds == null || rootForBounds.get_childCount() == 0)
		{
			return Vector3.get_zero();
		}
		Rect rect = mCam.get_rect();
		float num = rect.get_xMin() * (float)Screen.get_width();
		Rect rect2 = mCam.get_rect();
		Vector3 val = default(Vector3);
		val._002Ector(num, rect2.get_yMin() * (float)Screen.get_height(), 0f);
		Rect rect3 = mCam.get_rect();
		float num2 = rect3.get_xMax() * (float)Screen.get_width();
		Rect rect4 = mCam.get_rect();
		Vector3 val2 = default(Vector3);
		val2._002Ector(num2, rect4.get_yMax() * (float)Screen.get_height(), 0f);
		val = mCam.ScreenToWorldPoint(val);
		val2 = mCam.ScreenToWorldPoint(val2);
		Vector3 min = mBounds.get_min();
		float x = min.x;
		Vector3 min2 = mBounds.get_min();
		Vector2 minRect = default(Vector2);
		minRect._002Ector(x, min2.y);
		Vector3 max = mBounds.get_max();
		float x2 = max.x;
		Vector3 max2 = mBounds.get_max();
		Vector2 maxRect = default(Vector2);
		maxRect._002Ector(x2, max2.y);
		return Vector2.op_Implicit(NGUIMath.ConstrainRect(minRect, maxRect, Vector2.op_Implicit(val), Vector2.op_Implicit(val2)));
	}

	public bool ConstrainToBounds(bool immediate)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		if (mTrans != null && rootForBounds != null)
		{
			Vector3 val = CalculateConstrainOffset();
			if (val.get_sqrMagnitude() > 0f)
			{
				if (immediate)
				{
					Transform obj = mTrans;
					obj.set_position(obj.get_position() - val);
				}
				else
				{
					SpringPosition springPosition = SpringPosition.Begin(this.get_gameObject(), mTrans.get_position() - val, 13f);
					springPosition.ignoreTimeScale = true;
					springPosition.worldSpace = true;
				}
				return true;
			}
		}
		return false;
	}

	public void Press(bool isPressed)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		if (isPressed)
		{
			mDragStarted = false;
		}
		if (rootForBounds != null)
		{
			mPressed = isPressed;
			if (isPressed)
			{
				mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(rootForBounds);
				mMomentum = Vector2.get_zero();
				mScroll = 0f;
				SpringPosition component = this.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.set_enabled(false);
				}
			}
			else if (dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
			{
				ConstrainToBounds(false);
			}
		}
	}

	public void Drag(Vector2 delta)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		if (smoothDragStart && !mDragStarted)
		{
			mDragStarted = true;
		}
		else
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			if (mRoot != null)
			{
				delta *= mRoot.pixelSizeAdjustment;
			}
			Vector2 val = Vector2.Scale(delta, -scale);
			Transform obj = mTrans;
			obj.set_localPosition(obj.get_localPosition() + Vector2.op_Implicit(val));
			mMomentum = Vector2.Lerp(mMomentum, mMomentum + val * (0.01f * momentumAmount), 0.67f);
			if (dragEffect != UIDragObject.DragEffect.MomentumAndSpring && ConstrainToBounds(true))
			{
				mMomentum = Vector2.get_zero();
				mScroll = 0f;
			}
		}
	}

	public void Scroll(float delta)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		if (this.get_enabled() && NGUITools.GetActive(this.get_gameObject()))
		{
			if (Mathf.Sign(mScroll) != Mathf.Sign(delta))
			{
				mScroll = 0f;
			}
			mScroll += delta * scrollWheelFactor;
		}
	}

	private void Update()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		float deltaTime = RealTime.deltaTime;
		if (mPressed)
		{
			SpringPosition component = this.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.set_enabled(false);
			}
			mScroll = 0f;
		}
		else
		{
			mMomentum += scale * (mScroll * 20f);
			mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
			if (mMomentum.get_magnitude() > 0.01f)
			{
				Transform obj = mTrans;
				obj.set_localPosition(obj.get_localPosition() + Vector2.op_Implicit(NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime)));
				mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(rootForBounds);
				if (!ConstrainToBounds(dragEffect == UIDragObject.DragEffect.None))
				{
					SpringPosition component2 = this.GetComponent<SpringPosition>();
					if (component2 != null)
					{
						component2.set_enabled(false);
					}
				}
				return;
			}
			mScroll = 0f;
		}
		NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
	}
}
