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
		openTween = CreateTween(isOpenTween: true);
		closeTween = CreateTween(isOpenTween: false);
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
		if (!Object.op_Implicit(root))
		{
			state = STATE.OPENED;
			return;
		}
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

	public void OpenImmediately()
	{
		if (!Object.op_Implicit(root))
		{
			state = STATE.OPENED;
			return;
		}
		state = STATE.OPENED;
		openTween.Sample(1f, isFinished: true);
	}

	public void Close(Action on_finished)
	{
		if (!Object.op_Implicit(root))
		{
			state = STATE.CLOSED;
			return;
		}
		OnPreClose();
		if (!openTween.get_gameObject().get_activeSelf())
		{
			on_finished();
			return;
		}
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
			state = STATE.CLOSED;
			root.get_gameObject().SetActive(false);
			on_finished();
		});
		closeTween.PlayForward();
	}

	public void CloseImmediately()
	{
		if (!Object.op_Implicit(root))
		{
			state = STATE.CLOSED;
			return;
		}
		state = STATE.CLOSED;
		closeTween.Sample(1f, isFinished: true);
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
