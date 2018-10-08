using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Draggable Camera")]
[RequireComponent(typeof(Camera))]
public class UIDraggableCamera : MonoBehaviour
{
	public Transform rootForBounds;

	public Vector2 scale = Vector2.one;

	public float scrollWheelFactor;

	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	public bool smoothDragStart = true;

	public float momentumAmount = 35f;

	private Camera mCam;

	private Transform mTrans;

	private bool mPressed;

	private Vector2 mMomentum = Vector2.zero;

	private Bounds mBounds;

	private float mScroll;

	private UIRoot mRoot;

	private bool mDragStarted;

	public Vector2 currentMomentum
	{
		get
		{
			return mMomentum;
		}
		set
		{
			mMomentum = value;
		}
	}

	private void Start()
	{
		mCam = GetComponent<Camera>();
		mTrans = base.transform;
		mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		if ((Object)rootForBounds == (Object)null)
		{
			Debug.LogError(NGUITools.GetHierarchy(base.gameObject) + " needs the 'Root For Bounds' parameter to be set", this);
			base.enabled = false;
		}
	}

	private Vector3 CalculateConstrainOffset()
	{
		if ((Object)rootForBounds == (Object)null || rootForBounds.childCount == 0)
		{
			return Vector3.zero;
		}
		Vector3 vector = new Vector3(mCam.rect.xMin * (float)Screen.width, mCam.rect.yMin * (float)Screen.height, 0f);
		Vector3 vector2 = new Vector3(mCam.rect.xMax * (float)Screen.width, mCam.rect.yMax * (float)Screen.height, 0f);
		vector = mCam.ScreenToWorldPoint(vector);
		vector2 = mCam.ScreenToWorldPoint(vector2);
		Vector3 min = mBounds.min;
		float x = min.x;
		Vector3 min2 = mBounds.min;
		Vector2 minRect = new Vector2(x, min2.y);
		Vector3 max = mBounds.max;
		float x2 = max.x;
		Vector3 max2 = mBounds.max;
		Vector2 maxRect = new Vector2(x2, max2.y);
		return NGUIMath.ConstrainRect(minRect, maxRect, vector, vector2);
	}

	public bool ConstrainToBounds(bool immediate)
	{
		if ((Object)mTrans != (Object)null && (Object)rootForBounds != (Object)null)
		{
			Vector3 vector = CalculateConstrainOffset();
			if (vector.sqrMagnitude > 0f)
			{
				if (immediate)
				{
					mTrans.position -= vector;
				}
				else
				{
					SpringPosition springPosition = SpringPosition.Begin(base.gameObject, mTrans.position - vector, 13f);
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
		if (isPressed)
		{
			mDragStarted = false;
		}
		if ((Object)rootForBounds != (Object)null)
		{
			mPressed = isPressed;
			if (isPressed)
			{
				mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(rootForBounds);
				mMomentum = Vector2.zero;
				mScroll = 0f;
				SpringPosition component = GetComponent<SpringPosition>();
				if ((Object)component != (Object)null)
				{
					component.enabled = false;
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
		if (smoothDragStart && !mDragStarted)
		{
			mDragStarted = true;
		}
		else
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			if ((Object)mRoot != (Object)null)
			{
				delta *= mRoot.pixelSizeAdjustment;
			}
			Vector2 vector = Vector2.Scale(delta, -scale);
			mTrans.localPosition += (Vector3)vector;
			mMomentum = Vector2.Lerp(mMomentum, mMomentum + vector * (0.01f * momentumAmount), 0.67f);
			if (dragEffect != UIDragObject.DragEffect.MomentumAndSpring && ConstrainToBounds(true))
			{
				mMomentum = Vector2.zero;
				mScroll = 0f;
			}
		}
	}

	public void Scroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
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
		float deltaTime = RealTime.deltaTime;
		if (mPressed)
		{
			SpringPosition component = GetComponent<SpringPosition>();
			if ((Object)component != (Object)null)
			{
				component.enabled = false;
			}
			mScroll = 0f;
		}
		else
		{
			mMomentum += scale * (mScroll * 20f);
			mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
			if (mMomentum.magnitude > 0.01f)
			{
				mTrans.localPosition += (Vector3)NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
				mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(rootForBounds);
				if (!ConstrainToBounds(dragEffect == UIDragObject.DragEffect.None))
				{
					SpringPosition component2 = GetComponent<SpringPosition>();
					if ((Object)component2 != (Object)null)
					{
						component2.enabled = false;
					}
				}
				return;
			}
			mScroll = 0f;
		}
		NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
	}
}
