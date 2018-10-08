using AnimationOrTween;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Play Animation")]
[ExecuteInEditMode]
public class UIPlayAnimation
{
	public static UIPlayAnimation current;

	public Animation target;

	public Animator animator;

	public string clipName;

	public Trigger trigger;

	public Direction playDirection = Direction.Forward;

	public bool resetOnPlay;

	public bool clearSelection;

	public EnableCondition ifDisabledOnPlay;

	public DisableCondition disableWhenFinished;

	public List<EventDelegate> onFinished = new List<EventDelegate>();

	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	[SerializeField]
	[HideInInspector]
	private string callWhenFinished;

	private bool mStarted;

	private bool mActivated;

	private bool dragHighlight;

	private bool dualState => trigger == Trigger.OnPress || trigger == Trigger.OnHover;

	public UIPlayAnimation()
		: this()
	{
	}

	private void Awake()
	{
		UIButton component = this.GetComponent<UIButton>();
		if (component != null)
		{
			dragHighlight = component.dragHighlight;
		}
		if (eventReceiver != null && EventDelegate.IsValid(onFinished))
		{
			eventReceiver = null;
			callWhenFinished = null;
		}
	}

	private void Start()
	{
		mStarted = true;
		if (target == null && animator == null)
		{
			animator = this.GetComponentInChildren<Animator>();
		}
		if (animator != null)
		{
			if (animator.get_enabled())
			{
				animator.set_enabled(false);
			}
		}
		else
		{
			if (target == null)
			{
				target = this.GetComponentInChildren<Animation>();
			}
			if (target != null && target.get_enabled())
			{
				target.set_enabled(false);
			}
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

	private void OnHover(bool isOver)
	{
		if (this.get_enabled() && (trigger == Trigger.OnHover || (trigger == Trigger.OnHoverTrue && isOver) || (trigger == Trigger.OnHoverFalse && !isOver)))
		{
			Play(isOver, dualState);
		}
	}

	private void OnPress(bool isPressed)
	{
		if (this.get_enabled() && (UICamera.currentTouchID >= -1 || UICamera.currentScheme == UICamera.ControlScheme.Controller) && (trigger == Trigger.OnPress || (trigger == Trigger.OnPressTrue && isPressed) || (trigger == Trigger.OnPressFalse && !isPressed)))
		{
			Play(isPressed, dualState);
		}
	}

	private void OnClick()
	{
		if ((UICamera.currentTouchID >= -1 || UICamera.currentScheme == UICamera.ControlScheme.Controller) && this.get_enabled() && trigger == Trigger.OnClick)
		{
			Play(true, false);
		}
	}

	private void OnDoubleClick()
	{
		if ((UICamera.currentTouchID >= -1 || UICamera.currentScheme == UICamera.ControlScheme.Controller) && this.get_enabled() && trigger == Trigger.OnDoubleClick)
		{
			Play(true, false);
		}
	}

	private void OnSelect(bool isSelected)
	{
		if (this.get_enabled() && (trigger == Trigger.OnSelect || (trigger == Trigger.OnSelectTrue && isSelected) || (trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			Play(isSelected, dualState);
		}
	}

	private void OnToggle()
	{
		if (this.get_enabled() && !(UIToggle.current == null) && (trigger == Trigger.OnActivate || (trigger == Trigger.OnActivateTrue && UIToggle.current.value) || (trigger == Trigger.OnActivateFalse && !UIToggle.current.value)))
		{
			Play(UIToggle.current.value, dualState);
		}
	}

	private void OnDragOver()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled() && dualState)
		{
			if (UICamera.currentTouch.dragged == this.get_gameObject())
			{
				Play(true, true);
			}
			else if (dragHighlight && trigger == Trigger.OnPress)
			{
				Play(true, true);
			}
		}
	}

	private void OnDragOut()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled() && dualState && UICamera.hoveredObject != this.get_gameObject())
		{
			Play(false, true);
		}
	}

	private void OnDrop(GameObject go)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled() && trigger == Trigger.OnPress && UICamera.currentTouch.dragged != this.get_gameObject())
		{
			Play(false, true);
		}
	}

	public void Play(bool forward)
	{
		Play(forward, true);
	}

	public void Play(bool forward, bool onlyIfDifferent)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit(target) || Object.op_Implicit(animator))
		{
			if (onlyIfDifferent)
			{
				if (mActivated == forward)
				{
					return;
				}
				mActivated = forward;
			}
			if (clearSelection && UICamera.selectedObject == this.get_gameObject())
			{
				UICamera.selectedObject = null;
			}
			int num = 0 - playDirection;
			Direction direction = (Direction)((!forward) ? num : ((int)playDirection));
			ActiveAnimation activeAnimation = (!Object.op_Implicit(target)) ? ActiveAnimation.Play(animator, clipName, direction, ifDisabledOnPlay, disableWhenFinished) : ActiveAnimation.Play(target, clipName, direction, ifDisabledOnPlay, disableWhenFinished);
			if (activeAnimation != null)
			{
				if (resetOnPlay)
				{
					activeAnimation.Reset();
				}
				for (int i = 0; i < onFinished.Count; i++)
				{
					EventDelegate.Add(activeAnimation.onFinished, OnFinished, true);
				}
			}
		}
	}

	public void PlayForward()
	{
		Play(true);
	}

	public void PlayReverse()
	{
		Play(false);
	}

	private void OnFinished()
	{
		if (current == null)
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
