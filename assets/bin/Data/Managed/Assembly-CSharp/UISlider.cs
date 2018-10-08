using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI Slider")]
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

	[HideInInspector]
	[SerializeField]
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
			Collider component = this.GetComponent<Collider>();
			if (component != null)
			{
				return component.get_enabled();
			}
			Collider2D component2 = this.GetComponent<Collider2D>();
			return component2 != null && component2.get_enabled();
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
			if (foreground != null)
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
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		GameObject go = (!(mBG != null) || (!(mBG.GetComponent<Collider>() != null) && !(mBG.GetComponent<Collider2D>() != null))) ? this.get_gameObject() : mBG.get_gameObject();
		UIEventListener uIEventListener = UIEventListener.Get(go);
		UIEventListener uIEventListener2 = uIEventListener;
		uIEventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uIEventListener2.onPress, new UIEventListener.BoolDelegate(OnPressBackground));
		UIEventListener uIEventListener3 = uIEventListener;
		uIEventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener3.onDrag, new UIEventListener.VectorDelegate(OnDragBackground));
		if (thumb != null && (thumb.GetComponent<Collider>() != null || thumb.GetComponent<Collider2D>() != null) && (mFG == null || thumb != mFG.cachedTransform))
		{
			UIEventListener uIEventListener4 = UIEventListener.Get(thumb.get_gameObject());
			UIEventListener uIEventListener5 = uIEventListener4;
			uIEventListener5.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uIEventListener5.onPress, new UIEventListener.BoolDelegate(OnPressForeground));
			UIEventListener uIEventListener6 = uIEventListener4;
			uIEventListener6.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uIEventListener6.onDrag, new UIEventListener.VectorDelegate(OnDragForeground));
		}
	}

	protected void OnPressBackground(GameObject go, bool isPressed)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			mCam = UICamera.currentCamera;
			base.value = ScreenToValue(UICamera.lastEventPosition);
		}
	}

	protected void OnPressForeground(GameObject go, bool isPressed)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			mCam = UICamera.currentCamera;
			if (isPressed)
			{
				mOffset = ((!(mFG == null)) ? (base.value - ScreenToValue(UICamera.lastEventPosition)) : 0f);
			}
			else if (onDragFinished != null)
			{
				onDragFinished();
			}
		}
	}

	protected void OnDragForeground(GameObject go, Vector2 delta)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			mCam = UICamera.currentCamera;
			base.value = mOffset + ScreenToValue(UICamera.lastEventPosition);
		}
	}

	public override void OnPan(Vector2 delta)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled() && isColliderEnabled)
		{
			base.OnPan(delta);
		}
	}
}
