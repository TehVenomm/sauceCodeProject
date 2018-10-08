using System;
using UnityEngine;

public abstract class ChatUITweenGroup
{
	private enum STATE
	{
		OPENED,
		OPENING,
		CLOSING,
		CLOSED
	}

	private UIRect root;

	private UITweener openTween;

	private UITweener closeTween;

	private STATE state = STATE.CLOSED;

	public UIRect rootRect => root;

	public bool isOpened => state == STATE.OPENED;

	public bool isOpening => state == STATE.OPENING;

	public bool isClosing => state == STATE.CLOSING;

	public bool isTransitioning => isOpening || isClosing;

	public ChatUITweenGroup(UIRect root)
	{
		if (Object.op_Implicit(root))
		{
			this.root = root;
		}
	}

	public void Initialize()
	{
		openTween = CreateTween(true);
		closeTween = CreateTween(false);
	}

	protected abstract UITweener CreateTween(bool isOpenTween);

	protected virtual void OnPreClose()
	{
	}

	protected virtual void OnPostOpen()
	{
	}

	public void Open(Action on_finished)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (!Object.op_Implicit(root))
		{
			state = STATE.OPENED;
		}
		else
		{
			if (closeTween.get_enabled())
			{
				closeTween.SetOnFinished((EventDelegate)null);
				closeTween.SetStartToCurrentValue();
				closeTween.ResetToBeginning();
				closeTween.set_enabled(false);
			}
			if (!openTween.get_gameObject().get_activeSelf())
			{
				openTween.get_gameObject().SetActive(true);
			}
			state = STATE.OPENING;
			openTween.set_enabled(true);
			openTween.SetStartToCurrentValue();
			openTween.ResetToBeginning();
			openTween.SetOnFinished(delegate
			{
				state = STATE.OPENED;
				OnPostOpen();
				on_finished();
			});
			openTween.PlayForward();
		}
	}

	public void OpenImmediately()
	{
		if (!Object.op_Implicit(root))
		{
			state = STATE.OPENED;
		}
		else
		{
			state = STATE.OPENED;
			openTween.Sample(1f, true);
		}
	}

	public void Close(Action on_finished)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (!Object.op_Implicit(root))
		{
			state = STATE.CLOSED;
		}
		else
		{
			OnPreClose();
			if (!openTween.get_gameObject().get_activeSelf())
			{
				on_finished();
			}
			else
			{
				if (openTween.get_enabled())
				{
					openTween.SetOnFinished((EventDelegate)null);
					openTween.SetStartToCurrentValue();
					openTween.ResetToBeginning();
					openTween.set_enabled(false);
				}
				state = STATE.CLOSING;
				closeTween.set_enabled(true);
				closeTween.SetStartToCurrentValue();
				closeTween.ResetToBeginning();
				closeTween.SetOnFinished(delegate
				{
					//IL_0017: Unknown result type (might be due to invalid IL or missing references)
					state = STATE.CLOSED;
					root.get_gameObject().SetActive(false);
					on_finished();
				});
				closeTween.PlayForward();
			}
		}
	}

	public void CloseImmediately()
	{
		if (!Object.op_Implicit(root))
		{
			state = STATE.CLOSED;
		}
		else
		{
			state = STATE.CLOSED;
			closeTween.Sample(1f, true);
		}
	}
}
public abstract class ChatUITweenGroup<T> : ChatUITweenGroup where T : UITweener
{
	public ChatUITweenGroup(UIRect root)
		: base(root)
	{
	}

	protected override UITweener CreateTween(bool isOpenTween)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (base.rootRect == null)
		{
			return null;
		}
		T val = base.rootRect.get_gameObject().AddComponent<T>();
		InitTween(val, isOpenTween);
		return val;
	}

	protected abstract void InitTween(T tween, bool isOpenTween);
}
