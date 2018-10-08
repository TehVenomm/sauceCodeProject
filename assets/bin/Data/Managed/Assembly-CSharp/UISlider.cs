using System;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/NGUI Slider")]
[ExecuteInEditMode]
public class UISlider : UIProgressBar
{
	private enum Direction
	{
		Horizontal,
		Vertical,
		Upgraded
	}

	[HideInInspector]
	[SerializeField]
	private Transform foreground;

	[SerializeField]
	[HideInInspector]
	private float rawValue = 1f;

	[HideInInspector]
	[SerializeField]
	private Direction direction = Direction.Upgraded;

	[SerializeField]
	[HideInInspector]
	protected bool mInverted;

	public bool isColliderEnabled
	{
		get
		{
			Collider component = GetComponent<Collider>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				return component.enabled;
			}
			Collider2D component2 = GetComponent<Collider2D>();
			return (UnityEngine.Object)component2 != (UnityEngine.Object)null && component2.enabled;
		}
	}

	[Obsolete("Use 'value' instead")]
	public float sliderValue
	{
		get
		{
			return base.value;
		}
		set
		{
			base.value = value;
		}
	}

	[Obsolete("Use 'fillDirection' instead")]
	public bool inverted
	{
		get
		{
			return base.isInverted;
		}
		set
		{
		}
	}

	protected override void Upgrade()
	{
		if (direction != Direction.Upgraded)
		{
			mValue = rawValue;
			if ((UnityEngine.Object)foreground != (UnityEngine.Object)null)
			{
				mFG = foreground.GetComponent<UIWidget>();
			}
			if (direction == Direction.Horizontal)
			{
				mFill = (mInverted ? FillDirection.RightToLeft : FillDirection.LeftToRight);
			}
			else
			{
				mFill = ((!mInverted) ? FillDirection.BottomToTop : FillDirection.TopToBottom);
			}
			direction = Direction.Upgraded;
		}
	}

	protected override void OnStart()
	{
		GameObject go = (!((UnityEngine.Object)mBG != (UnityEngine.Object)null) || (!((UnityEngine.Object)mBG.GetComponent<Collider>() != (UnityEngine.Object)null) && !((UnityEngine.Object)mBG.GetComponent<Collider2D>() != (UnityEngine.Object)null))) ? base.gameObject : mBG.gameObject;
		UIEventListener uIEventListener = UIEventListener.Get(go);
		UIEventListener uIEventListener2 = uIEventListener;
		uIEventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uIEventListener2.onPress, new UIEventListener.BoolDelegate(OnPressBackground));
		UIEventListener uIEventListener3 = uIEventListener;
		uIEventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener3.onDrag, new UIEventListener.VectorDelegate(OnDragBackground));
		if ((UnityEngine.Object)thumb != (UnityEngine.Object)null && ((UnityEngine.Object)thumb.GetComponent<Collider>() != (UnityEngine.Object)null || (UnityEngine.Object)thumb.GetComponent<Collider2D>() != (UnityEngine.Object)null) && ((UnityEngine.Object)mFG == (UnityEngine.Object)null || (UnityEngine.Object)thumb != (UnityEngine.Object)mFG.cachedTransform))
		{
			UIEventListener uIEventListener4 = UIEventListener.Get(thumb.gameObject);
			UIEventListener uIEventListener5 = uIEventListener4;
			uIEventListener5.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uIEventListener5.onPress, new UIEventListener.BoolDelegate(OnPressForeground));
			UIEventListener uIEventListener6 = uIEventListener4;
			uIEventListener6.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener6.onDrag, new UIEventListener.VectorDelegate(OnDragForeground));
		}
	}

	protected void OnPressBackground(GameObject go, bool isPressed)
	{
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			mCam = UICamera.currentCamera;
			base.value = ScreenToValue(UICamera.lastEventPosition);
			if (!isPressed && onDragFinished != null)
			{
				onDragFinished();
			}
		}
	}

	protected void OnDragBackground(GameObject go, Vector2 delta)
	{
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			mCam = UICamera.currentCamera;
			base.value = ScreenToValue(UICamera.lastEventPosition);
		}
	}

	protected void OnPressForeground(GameObject go, bool isPressed)
	{
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			mCam = UICamera.currentCamera;
			if (isPressed)
			{
				mOffset = ((!((UnityEngine.Object)mFG == (UnityEngine.Object)null)) ? (base.value - ScreenToValue(UICamera.lastEventPosition)) : 0f);
			}
			else if (onDragFinished != null)
			{
				onDragFinished();
			}
		}
	}

	protected void OnDragForeground(GameObject go, Vector2 delta)
	{
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			mCam = UICamera.currentCamera;
			base.value = mOffset + ScreenToValue(UICamera.lastEventPosition);
		}
	}

	public override void OnPan(Vector2 delta)
	{
		if (base.enabled && isColliderEnabled)
		{
			base.OnPan(delta);
		}
	}
}
