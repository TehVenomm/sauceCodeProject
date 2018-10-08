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
		if ((bool)root)
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
		if (!(bool)root)
		{
			state = STATE.OPENED;
		}
		else
		{
			if (closeTween.enabled)
			{
				closeTween.SetOnFinished((EventDelegate)null);
				closeTween.SetStartToCurrentValue();
				closeTween.ResetToBeginning();
				closeTween.enabled = false;
			}
			if (!openTween.gameObject.activeSelf)
			{
				openTween.gameObject.SetActive(true);
			}
			state = STATE.OPENING;
			openTween.enabled = true;
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
		if (!(bool)root)
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
		if (!(bool)root)
		{
			state = STATE.CLOSED;
		}
		else
		{
			OnPreClose();
			if (!openTween.gameObject.activeSelf)
			{
				on_finished();
			}
			else
			{
				if (openTween.enabled)
				{
					openTween.SetOnFinished((EventDelegate)null);
					openTween.SetStartToCurrentValue();
					openTween.ResetToBeginning();
					openTween.enabled = false;
				}
				state = STATE.CLOSING;
				closeTween.enabled = true;
				closeTween.SetStartToCurrentValue();
				closeTween.ResetToBeginning();
				closeTween.SetOnFinished(delegate
				{
					state = STATE.CLOSED;
					root.gameObject.SetActive(false);
					on_finished();
				});
				closeTween.PlayForward();
			}
		}
	}

	public void CloseImmediately()
	{
		if (!(bool)root)
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
		if ((UnityEngine.Object)base.rootRect == (UnityEngine.Object)null)
		{
			return null;
		}
		T val = base.rootRect.gameObject.AddComponent<T>();
		InitTween(val, isOpenTween);
		return val;
	}

	protected abstract void InitTween(T tween, bool isOpenTween);
}
