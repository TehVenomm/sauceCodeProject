using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : UIWidgetContainer
{
	public enum State
	{
		Normal,
		Hover,
		Pressed,
		Disabled
	}

	public GameObject tweenTarget;

	public Color hover = new Color(0.882352948f, 40f / 51f, 0.5882353f, 1f);

	public Color pressed = new Color(61f / 85f, 163f / 255f, 41f / 85f, 1f);

	public Color disabledColor = Color.get_grey();

	public float duration = 0.2f;

	[NonSerialized]
	protected Color mStartingColor;

	[NonSerialized]
	protected Color mDefaultColor;

	[NonSerialized]
	protected bool mInitDone;

	[NonSerialized]
	protected UIWidget mWidget;

	[NonSerialized]
	protected State mState;

	public State state
	{
		get
		{
			return mState;
		}
		set
		{
			SetState(value, instant: false);
		}
	}

	public Color defaultColor
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			if (!mInitDone)
			{
				OnInit();
			}
			return mDefaultColor;
		}
		set
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (!mInitDone)
			{
				OnInit();
			}
			mDefaultColor = value;
			State state = mState;
			mState = State.Disabled;
			SetState(state, instant: false);
		}
	}

	public virtual bool isEnabled
	{
		get
		{
			return this.get_enabled();
		}
		set
		{
			this.set_enabled(value);
		}
	}

	public void ResetDefaultColor()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		defaultColor = mStartingColor;
	}

	public void CacheDefaultColor()
	{
		if (!mInitDone)
		{
			OnInit();
		}
	}

	private void Start()
	{
		if (!mInitDone)
		{
			OnInit();
		}
		if (!isEnabled)
		{
			SetState(State.Disabled, instant: true);
		}
	}

	protected virtual void OnInit()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		mInitDone = true;
		if (tweenTarget == null)
		{
			tweenTarget = this.get_gameObject();
		}
		if (tweenTarget != null)
		{
			mWidget = tweenTarget.GetComponent<UIWidget>();
		}
		if (mWidget != null)
		{
			mDefaultColor = mWidget.color;
			mStartingColor = mDefaultColor;
		}
		else
		{
			if (!(tweenTarget != null))
			{
				return;
			}
			Renderer component = tweenTarget.GetComponent<Renderer>();
			if (component != null)
			{
				mDefaultColor = ((!Application.get_isPlaying()) ? component.get_sharedMaterial().get_color() : component.get_material().get_color());
				mStartingColor = mDefaultColor;
				return;
			}
			Light component2 = tweenTarget.GetComponent<Light>();
			if (component2 != null)
			{
				mDefaultColor = component2.get_color();
				mStartingColor = mDefaultColor;
			}
			else
			{
				tweenTarget = null;
				mInitDone = false;
			}
		}
	}

	protected virtual void OnEnable()
	{
		if (mInitDone)
		{
			OnHover(UICamera.IsHighlighted(this.get_gameObject()));
		}
		if (UICamera.currentTouch != null)
		{
			if (UICamera.currentTouch.pressed == this.get_gameObject())
			{
				OnPress(isPressed: true);
			}
			else if (UICamera.currentTouch.current == this.get_gameObject())
			{
				OnHover(isOver: true);
			}
		}
	}

	protected virtual void OnDisable()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (mInitDone && tweenTarget != null)
		{
			SetState(State.Normal, instant: true);
			TweenColor component = tweenTarget.GetComponent<TweenColor>();
			if (component != null)
			{
				component.value = mDefaultColor;
				component.set_enabled(false);
			}
		}
	}

	protected virtual void OnHover(bool isOver)
	{
		if (isEnabled)
		{
			if (!mInitDone)
			{
				OnInit();
			}
			if (tweenTarget != null)
			{
				SetState(isOver ? State.Hover : State.Normal, instant: false);
			}
		}
	}

	protected virtual void OnPress(bool isPressed)
	{
		if (!isEnabled || UICamera.currentTouch == null)
		{
			return;
		}
		if (!mInitDone)
		{
			OnInit();
		}
		if (!(tweenTarget != null))
		{
			return;
		}
		if (isPressed)
		{
			SetState(State.Pressed, instant: false);
		}
		else if (UICamera.currentTouch.current == this.get_gameObject())
		{
			if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
			{
				SetState(State.Hover, instant: false);
			}
			else if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == this.get_gameObject())
			{
				SetState(State.Hover, instant: false);
			}
			else
			{
				SetState(State.Normal, instant: false);
			}
		}
		else
		{
			SetState(State.Normal, instant: false);
		}
	}

	protected virtual void OnDragOver()
	{
		if (isEnabled)
		{
			if (!mInitDone)
			{
				OnInit();
			}
			if (tweenTarget != null)
			{
				SetState(State.Pressed, instant: false);
			}
		}
	}

	protected virtual void OnDragOut()
	{
		if (isEnabled)
		{
			if (!mInitDone)
			{
				OnInit();
			}
			if (tweenTarget != null)
			{
				SetState(State.Normal, instant: false);
			}
		}
	}

	public virtual void SetState(State state, bool instant)
	{
		if (!mInitDone)
		{
			mInitDone = true;
			OnInit();
		}
		if (mState != state)
		{
			mState = state;
			UpdateColor(instant);
		}
	}

	public void UpdateColor(bool instant)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (tweenTarget != null)
		{
			TweenColor tweenColor;
			switch (mState)
			{
			case State.Hover:
				tweenColor = TweenColor.Begin(tweenTarget, duration, hover);
				break;
			case State.Pressed:
				tweenColor = TweenColor.Begin(tweenTarget, duration, pressed);
				break;
			case State.Disabled:
				tweenColor = TweenColor.Begin(tweenTarget, duration, disabledColor);
				break;
			default:
				tweenColor = TweenColor.Begin(tweenTarget, duration, mDefaultColor);
				break;
			}
			if (instant && tweenColor != null)
			{
				tweenColor.value = tweenColor.to;
				tweenColor.set_enabled(false);
			}
		}
	}
}
