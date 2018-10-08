using System;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Color")]
[ExecuteInEditMode]
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

	public Color hover = new Color(0.882352948f, 0.784313738f, 0.5882353f, 1f);

	public Color pressed = new Color(0.7176471f, 0.6392157f, 0.482352942f, 1f);

	public Color disabledColor = Color.grey;

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
			SetState(value, false);
		}
	}

	public Color defaultColor
	{
		get
		{
			if (!mInitDone)
			{
				OnInit();
			}
			return mDefaultColor;
		}
		set
		{
			if (!mInitDone)
			{
				OnInit();
			}
			mDefaultColor = value;
			State state = mState;
			mState = State.Disabled;
			SetState(state, false);
		}
	}

	public virtual bool isEnabled
	{
		get
		{
			return base.enabled;
		}
		set
		{
			base.enabled = value;
		}
	}

	public void ResetDefaultColor()
	{
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
			SetState(State.Disabled, true);
		}
	}

	protected virtual void OnInit()
	{
		mInitDone = true;
		if ((UnityEngine.Object)tweenTarget == (UnityEngine.Object)null)
		{
			tweenTarget = base.gameObject;
		}
		if ((UnityEngine.Object)tweenTarget != (UnityEngine.Object)null)
		{
			mWidget = tweenTarget.GetComponent<UIWidget>();
		}
		if ((UnityEngine.Object)mWidget != (UnityEngine.Object)null)
		{
			mDefaultColor = mWidget.color;
			mStartingColor = mDefaultColor;
		}
		else if ((UnityEngine.Object)tweenTarget != (UnityEngine.Object)null)
		{
			Renderer component = tweenTarget.GetComponent<Renderer>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				mDefaultColor = ((!Application.isPlaying) ? component.sharedMaterial.color : component.material.color);
				mStartingColor = mDefaultColor;
			}
			else
			{
				Light component2 = tweenTarget.GetComponent<Light>();
				if ((UnityEngine.Object)component2 != (UnityEngine.Object)null)
				{
					mDefaultColor = component2.color;
					mStartingColor = mDefaultColor;
				}
				else
				{
					tweenTarget = null;
					mInitDone = false;
				}
			}
		}
	}

	protected virtual void OnEnable()
	{
		if (mInitDone)
		{
			OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if ((UnityEngine.Object)UICamera.currentTouch.pressed == (UnityEngine.Object)base.gameObject)
			{
				OnPress(true);
			}
			else if ((UnityEngine.Object)UICamera.currentTouch.current == (UnityEngine.Object)base.gameObject)
			{
				OnHover(true);
			}
		}
	}

	protected virtual void OnDisable()
	{
		if (mInitDone && (UnityEngine.Object)tweenTarget != (UnityEngine.Object)null)
		{
			SetState(State.Normal, true);
			TweenColor component = tweenTarget.GetComponent<TweenColor>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				component.value = mDefaultColor;
				component.enabled = false;
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
			if ((UnityEngine.Object)tweenTarget != (UnityEngine.Object)null)
			{
				SetState(isOver ? State.Hover : State.Normal, false);
			}
		}
	}

	protected virtual void OnPress(bool isPressed)
	{
		if (isEnabled && UICamera.currentTouch != null)
		{
			if (!mInitDone)
			{
				OnInit();
			}
			if ((UnityEngine.Object)tweenTarget != (UnityEngine.Object)null)
			{
				if (isPressed)
				{
					SetState(State.Pressed, false);
				}
				else if ((UnityEngine.Object)UICamera.currentTouch.current == (UnityEngine.Object)base.gameObject)
				{
					if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
					{
						SetState(State.Hover, false);
					}
					else if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && (UnityEngine.Object)UICamera.hoveredObject == (UnityEngine.Object)base.gameObject)
					{
						SetState(State.Hover, false);
					}
					else
					{
						SetState(State.Normal, false);
					}
				}
				else
				{
					SetState(State.Normal, false);
				}
			}
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
			if ((UnityEngine.Object)tweenTarget != (UnityEngine.Object)null)
			{
				SetState(State.Pressed, false);
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
			if ((UnityEngine.Object)tweenTarget != (UnityEngine.Object)null)
			{
				SetState(State.Normal, false);
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
		if ((UnityEngine.Object)tweenTarget != (UnityEngine.Object)null)
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
			if (instant && (UnityEngine.Object)tweenColor != (UnityEngine.Object)null)
			{
				tweenColor.value = tweenColor.to;
				tweenColor.enabled = false;
			}
		}
	}
}
