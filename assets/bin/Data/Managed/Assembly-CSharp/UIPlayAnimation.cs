using AnimationOrTween;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Animation")]
public class UIPlayAnimation : MonoBehaviour
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

	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	private bool mStarted;

	private bool mActivated;

	private bool dragHighlight;

	private bool dualState => trigger == Trigger.OnPress || trigger == Trigger.OnHover;

	private void Awake()
	{
		UIButton component = GetComponent<UIButton>();
		if ((Object)component != (Object)null)
		{
			dragHighlight = component.dragHighlight;
		}
		if ((Object)eventReceiver != (Object)null && EventDelegate.IsValid(onFinished))
		{
			eventReceiver = null;
			callWhenFinished = null;
		}
	}

	private void Start()
	{
		mStarted = true;
		if ((Object)target == (Object)null && (Object)animator == (Object)null)
		{
			animator = GetComponentInChildren<Animator>();
		}
		if ((Object)animator != (Object)null)
		{
			if (animator.enabled)
			{
				animator.enabled = false;
			}
		}
		else
		{
			if ((Object)target == (Object)null)
			{
				target = GetComponentInChildren<Animation>();
			}
			if ((Object)target != (Object)null && target.enabled)
			{
				target.enabled = false;
			}
		}
	}

	private void OnEnable()
	{
		if (mStarted)
		{
			OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (trigger == Trigger.OnPress || trigger == Trigger.OnPressTrue)
			{
				mActivated = ((Object)UICamera.currentTouch.pressed == (Object)base.gameObject);
			}
			if (trigger == Trigger.OnHover || trigger == Trigger.OnHoverTrue)
			{
				mActivated = ((Object)UICamera.currentTouch.current == (Object)base.gameObject);
			}
		}
		UIToggle component = GetComponent<UIToggle>();
		if ((Object)component != (Object)null)
		{
			EventDelegate.Add(component.onChange, OnToggle);
		}
	}

	private void OnDisable()
	{
		UIToggle component = GetComponent<UIToggle>();
		if ((Object)component != (Object)null)
		{
			EventDelegate.Remove(component.onChange, OnToggle);
		}
	}

	private void OnHover(bool isOver)
	{
		if (base.enabled && (trigger == Trigger.OnHover || (trigger == Trigger.OnHoverTrue && isOver) || (trigger == Trigger.OnHoverFalse && !isOver)))
		{
			Play(isOver, dualState);
		}
	}

	private void OnPress(bool isPressed)
	{
		if (base.enabled && (UICamera.currentTouchID >= -1 || UICamera.currentScheme == UICamera.ControlScheme.Controller) && (trigger == Trigger.OnPress || (trigger == Trigger.OnPressTrue && isPressed) || (trigger == Trigger.OnPressFalse && !isPressed)))
		{
			Play(isPressed, dualState);
		}
	}

	private void OnClick()
	{
		if ((UICamera.currentTouchID >= -1 || UICamera.currentScheme == UICamera.ControlScheme.Controller) && base.enabled && trigger == Trigger.OnClick)
		{
			Play(true, false);
		}
	}

	private void OnDoubleClick()
	{
		if ((UICamera.currentTouchID >= -1 || UICamera.currentScheme == UICamera.ControlScheme.Controller) && base.enabled && trigger == Trigger.OnDoubleClick)
		{
			Play(true, false);
		}
	}

	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (trigger == Trigger.OnSelect || (trigger == Trigger.OnSelectTrue && isSelected) || (trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			Play(isSelected, dualState);
		}
	}

	private void OnToggle()
	{
		if (base.enabled && !((Object)UIToggle.current == (Object)null) && (trigger == Trigger.OnActivate || (trigger == Trigger.OnActivateTrue && UIToggle.current.value) || (trigger == Trigger.OnActivateFalse && !UIToggle.current.value)))
		{
			Play(UIToggle.current.value, dualState);
		}
	}

	private void OnDragOver()
	{
		if (base.enabled && dualState)
		{
			if ((Object)UICamera.currentTouch.dragged == (Object)base.gameObject)
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
		if (base.enabled && dualState && (Object)UICamera.hoveredObject != (Object)base.gameObject)
		{
			Play(false, true);
		}
	}

	private void OnDrop(GameObject go)
	{
		if (base.enabled && trigger == Trigger.OnPress && (Object)UICamera.currentTouch.dragged != (Object)base.gameObject)
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
		if ((bool)target || (bool)animator)
		{
			if (onlyIfDifferent)
			{
				if (mActivated == forward)
				{
					return;
				}
				mActivated = forward;
			}
			if (clearSelection && (Object)UICamera.selectedObject == (Object)base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			int num = 0 - playDirection;
			Direction direction = (Direction)((!forward) ? num : ((int)playDirection));
			ActiveAnimation activeAnimation = (!(bool)target) ? ActiveAnimation.Play(animator, clipName, direction, ifDisabledOnPlay, disableWhenFinished) : ActiveAnimation.Play(target, clipName, direction, ifDisabledOnPlay, disableWhenFinished);
			if ((Object)activeAnimation != (Object)null)
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
		if ((Object)current == (Object)null)
		{
			current = this;
			EventDelegate.Execute(onFinished);
			if ((Object)eventReceiver != (Object)null && !string.IsNullOrEmpty(callWhenFinished))
			{
				eventReceiver.SendMessage(callWhenFinished, SendMessageOptions.DontRequireReceiver);
			}
			eventReceiver = null;
			current = null;
		}
	}
}
