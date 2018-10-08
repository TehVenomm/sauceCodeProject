using System;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset
{
	public Transform tweenTarget;

	public Vector3 hover = Vector3.get_zero();

	public Vector3 pressed = new Vector3(2f, -2f);

	public float duration = 0.2f;

	[NonSerialized]
	private Vector3 mPos;

	[NonSerialized]
	private bool mStarted;

	[NonSerialized]
	private bool mPressed;

	public UIButtonOffset()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)
	//IL_0016: Unknown result type (might be due to invalid IL or missing references)
	//IL_001b: Unknown result type (might be due to invalid IL or missing references)


	private void Start()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (!mStarted)
		{
			mStarted = true;
			if (tweenTarget == null)
			{
				tweenTarget = this.get_transform();
			}
			mPos = tweenTarget.get_localPosition();
		}
	}

	private void OnEnable()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		if (mStarted)
		{
			OnHover(UICamera.IsHighlighted(this.get_gameObject()));
		}
	}

	private void OnDisable()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (mStarted && tweenTarget != null)
		{
			TweenPosition component = tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.value = mPos;
				component.set_enabled(false);
			}
		}
	}

	private void OnPress(bool isPressed)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		mPressed = isPressed;
		if (this.get_enabled())
		{
			if (!mStarted)
			{
				Start();
			}
			TweenPosition.Begin(tweenTarget.get_gameObject(), duration, isPressed ? (mPos + pressed) : ((!UICamera.IsHighlighted(this.get_gameObject())) ? mPos : (mPos + hover))).method = UITweener.Method.EaseInOut;
		}
	}

	private void OnHover(bool isOver)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		if (this.get_enabled())
		{
			if (!mStarted)
			{
				Start();
			}
			TweenPosition.Begin(tweenTarget.get_gameObject(), duration, (!isOver) ? mPos : (mPos + hover)).method = UITweener.Method.EaseInOut;
		}
	}

	private void OnDragOver()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		if (mPressed)
		{
			TweenPosition.Begin(tweenTarget.get_gameObject(), duration, mPos + hover).method = UITweener.Method.EaseInOut;
		}
	}

	private void OnDragOut()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (mPressed)
		{
			TweenPosition.Begin(tweenTarget.get_gameObject(), duration, mPos).method = UITweener.Method.EaseInOut;
		}
	}

	private void OnSelect(bool isSelected)
	{
		if (this.get_enabled() && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			OnHover(isSelected);
		}
	}
}
