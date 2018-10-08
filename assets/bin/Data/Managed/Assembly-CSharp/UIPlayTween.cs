using AnimationOrTween;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Play Tween")]
[ExecuteInEditMode]
public class UIPlayTween
{
	public static UIPlayTween current;

	public GameObject tweenTarget;

	public int tweenGroup;

	public Trigger trigger;

	public Direction playDirection = Direction.Forward;

	public bool resetOnPlay;

	public bool resetIfDisabled;

	public EnableCondition ifDisabledOnPlay;

	public DisableCondition disableWhenFinished;

	public bool includeChildren;

	public List<EventDelegate> onFinished = new List<EventDelegate>();

	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	private UITweener[] mTweens;

	private bool mStarted;

	private int mActive;

	private bool mActivated;

	public UIPlayTween()
		: this()
	{
	}

	private void Awake()
	{
		if (eventReceiver != null && EventDelegate.IsValid(onFinished))
		{
			eventReceiver = null;
			callWhenFinished = null;
		}
	}

	private void Start()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		mStarted = true;
		if (tweenTarget == null)
		{
			tweenTarget = this.get_gameObject();
		}
	}

	private void OnEnable()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (mStarted)
		{
			OnHover(UICamera.IsHighlighted(this.get_gameObject()));
		}
		if (UICamera.currentTouch != null)
		{
			if (trigger == Trigger.OnPress || trigger == Trigger.OnPressTrue)
			{
				mActivated = (UICamera.currentTouch.pressed == this.get_gameObject());
			}
			if (trigger == Trigger.OnHover || trigger == Trigger.OnHoverTrue)
			{
				mActivated = (UICamera.currentTouch.current == this.get_gameObject());
			}
		}
		UIToggle component = this.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Add(component.onChange, OnToggle);
		}
	}

	private void OnDisable()
	{
		UIToggle component = this.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Remove(component.onChange, OnToggle);
		}
	}

	private void OnDragOver()
	{
		if (trigger == Trigger.OnHover)
		{
			OnHover(true);
		}
	}

	private void OnHover(bool isOver)
	{
		if (this.get_enabled() && (trigger == Trigger.OnHover || (trigger == Trigger.OnHoverTrue && isOver) || (trigger == Trigger.OnHoverFalse && !isOver)))
		{
			mActivated = (isOver && trigger == Trigger.OnHover);
			Play(isOver);
		}
	}

	private void OnDragOut()
	{
		if (this.get_enabled() && mActivated)
		{
			mActivated = false;
			Play(false);
		}
	}

	private void OnPress(bool isPressed)
	{
		if (this.get_enabled() && (trigger == Trigger.OnPress || (trigger == Trigger.OnPressTrue && isPressed) || (trigger == Trigger.OnPressFalse && !isPressed)))
		{
			mActivated = (isPressed && trigger == Trigger.OnPress);
			Play(isPressed);
		}
	}

	private void OnClick()
	{
		if (this.get_enabled() && trigger == Trigger.OnClick)
		{
			Play(true);
		}
	}

	private void OnDoubleClick()
	{
		if (this.get_enabled() && trigger == Trigger.OnDoubleClick)
		{
			Play(true);
		}
	}

	private void OnSelect(bool isSelected)
	{
		if (this.get_enabled() && (trigger == Trigger.OnSelect || (trigger == Trigger.OnSelectTrue && isSelected) || (trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			mActivated = (isSelected && trigger == Trigger.OnSelect);
			Play(isSelected);
		}
	}

	private void OnToggle()
	{
		if (this.get_enabled() && !(UIToggle.current == null) && (trigger == Trigger.OnActivate || (trigger == Trigger.OnActivateTrue && UIToggle.current.value) || (trigger == Trigger.OnActivateFalse && !UIToggle.current.value)))
		{
			Play(UIToggle.current.value);
		}
	}

	private void Update()
	{
		if (disableWhenFinished != 0 && mTweens != null)
		{
			bool flag = true;
			bool flag2 = true;
			int i = 0;
			for (int num = mTweens.Length; i < num; i++)
			{
				UITweener uITweener = mTweens[i];
				if (uITweener.tweenGroup == tweenGroup)
				{
					if (uITweener.get_enabled())
					{
						flag = false;
						break;
					}
					if (uITweener.direction != (Direction)disableWhenFinished)
					{
						flag2 = false;
					}
				}
			}
			if (flag)
			{
				if (flag2)
				{
					NGUITools.SetActive(tweenTarget, false);
				}
				mTweens = null;
			}
		}
	}

	public void Play(bool forward)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		mActive = 0;
		GameObject val = (!(tweenTarget == null)) ? ((object)tweenTarget) : ((object)this.get_gameObject());
		if (!NGUITools.GetActive(val))
		{
			if (ifDisabledOnPlay != EnableCondition.EnableThenPlay)
			{
				return;
			}
			NGUITools.SetActive(val, true);
		}
		mTweens = ((!includeChildren) ? val.GetComponents<UITweener>() : val.GetComponentsInChildren<UITweener>());
		if (mTweens.Length == 0)
		{
			if (disableWhenFinished != 0)
			{
				NGUITools.SetActive(tweenTarget, false);
			}
		}
		else
		{
			bool flag = false;
			if (playDirection == Direction.Reverse)
			{
				forward = !forward;
			}
			int i = 0;
			for (int num = mTweens.Length; i < num; i++)
			{
				UITweener uITweener = mTweens[i];
				if (uITweener.tweenGroup == tweenGroup)
				{
					if (!flag && !NGUITools.GetActive(val))
					{
						flag = true;
						NGUITools.SetActive(val, true);
					}
					mActive++;
					if (playDirection == Direction.Toggle)
					{
						EventDelegate.Add(uITweener.onFinished, OnFinished, true);
						uITweener.Toggle();
					}
					else
					{
						if (resetOnPlay || (resetIfDisabled && !uITweener.get_enabled()))
						{
							uITweener.Play(forward);
							uITweener.ResetToBeginning();
						}
						EventDelegate.Add(uITweener.onFinished, OnFinished, true);
						uITweener.Play(forward);
					}
				}
			}
		}
	}

	private void OnFinished()
	{
		if (--mActive == 0 && current == null)
		{
			current = this;
			EventDelegate.Execute(onFinished);
			if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
			{
				eventReceiver.SendMessage(callWhenFinished, 1);
			}
			eventReceiver = null;
			current = null;
		}
	}
}
