using AnimationOrTween;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Toggle")]
[ExecuteInEditMode]
public class UIToggle : UIWidgetContainer
{
	public delegate bool Validate(bool choice);

	public static BetterList<UIToggle> list = new BetterList<UIToggle>();

	public static UIToggle current;

	public int group;

	public UIWidget activeSprite;

	public Animation activeAnimation;

	public Animator animator;

	public bool startsActive;

	public bool instantTween;

	public bool optionCanBeNone;

	public List<EventDelegate> onChange = new List<EventDelegate>();

	public Validate validator;

	[HideInInspector]
	[SerializeField]
	private UISprite checkSprite;

	[HideInInspector]
	[SerializeField]
	private Animation checkAnimation;

	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	[HideInInspector]
	[SerializeField]
	private string functionName = "OnActivate";

	[SerializeField]
	[HideInInspector]
	private bool startsChecked;

	private bool mIsActive = true;

	private bool mStarted;

	public bool value
	{
		get
		{
			return (!mStarted) ? startsActive : mIsActive;
		}
		set
		{
			if (!mStarted)
			{
				startsActive = value;
			}
			else if (group == 0 || value || optionCanBeNone || !mStarted)
			{
				Set(value);
			}
		}
	}

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
	public bool isChecked
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	public static UIToggle GetActiveToggle(int group)
	{
		for (int i = 0; i < list.size; i++)
		{
			UIToggle uIToggle = list[i];
			if (uIToggle != null && uIToggle.group == group && uIToggle.mIsActive)
			{
				return uIToggle;
			}
		}
		return null;
	}

	private void OnEnable()
	{
		list.Add(this);
	}

	private void OnDisable()
	{
		list.Remove(this);
	}

	private void Start()
	{
		if (startsChecked)
		{
			startsChecked = false;
			startsActive = true;
		}
		if (!Application.get_isPlaying())
		{
			if (checkSprite != null && activeSprite == null)
			{
				activeSprite = checkSprite;
				checkSprite = null;
			}
			if (checkAnimation != null && activeAnimation == null)
			{
				activeAnimation = checkAnimation;
				checkAnimation = null;
			}
			if (Application.get_isPlaying() && activeSprite != null)
			{
				activeSprite.alpha = ((!startsActive) ? 0f : 1f);
			}
			if (EventDelegate.IsValid(onChange))
			{
				eventReceiver = null;
				functionName = null;
			}
		}
		else
		{
			mIsActive = !startsActive;
			mStarted = true;
			bool flag = instantTween;
			instantTween = true;
			Set(startsActive);
			instantTween = flag;
		}
	}

	private void OnClick()
	{
		if (this.get_enabled() && isColliderEnabled && UICamera.currentTouchID != -2)
		{
			value = !value;
		}
	}

	public void Set(bool state)
	{
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		if (validator == null || validator(state))
		{
			if (!mStarted)
			{
				mIsActive = state;
				startsActive = state;
				if (activeSprite != null)
				{
					activeSprite.alpha = ((!state) ? 0f : 1f);
				}
			}
			else if (mIsActive != state)
			{
				if (group != 0 && state)
				{
					int num = 0;
					int size = list.size;
					while (num < size)
					{
						UIToggle uIToggle = list[num];
						if (uIToggle != this && uIToggle.group == group)
						{
							uIToggle.Set(false);
						}
						if (list.size != size)
						{
							size = list.size;
							num = 0;
						}
						else
						{
							num++;
						}
					}
				}
				mIsActive = state;
				if (activeSprite != null)
				{
					if (instantTween || !NGUITools.GetActive(this))
					{
						activeSprite.alpha = ((!mIsActive) ? 0f : 1f);
					}
					else
					{
						TweenAlpha.Begin(activeSprite.get_gameObject(), 0.15f, (!mIsActive) ? 0f : 1f);
					}
				}
				if (current == null)
				{
					UIToggle uIToggle2 = current;
					current = this;
					if (EventDelegate.IsValid(onChange))
					{
						EventDelegate.Execute(onChange);
					}
					else if (eventReceiver != null && !string.IsNullOrEmpty(functionName))
					{
						eventReceiver.SendMessage(functionName, (object)mIsActive, 1);
					}
					current = uIToggle2;
				}
				if (animator != null)
				{
					ActiveAnimation activeAnimation = ActiveAnimation.Play(animator, null, state ? Direction.Forward : Direction.Reverse, EnableCondition.IgnoreDisabledState, DisableCondition.DoNotDisable);
					if (activeAnimation != null && (instantTween || !NGUITools.GetActive(this)))
					{
						activeAnimation.Finish();
					}
				}
				else if (this.activeAnimation != null)
				{
					ActiveAnimation activeAnimation2 = ActiveAnimation.Play(this.activeAnimation, null, state ? Direction.Forward : Direction.Reverse, EnableCondition.IgnoreDisabledState, DisableCondition.DoNotDisable);
					if (activeAnimation2 != null && (instantTween || !NGUITools.GetActive(this)))
					{
						activeAnimation2.Finish();
					}
				}
			}
		}
	}
}
