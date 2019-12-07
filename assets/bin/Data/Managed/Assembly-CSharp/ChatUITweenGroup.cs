using System;

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

	public bool isTransitioning
	{
		get
		{
			if (!isOpening)
			{
				return isClosing;
			}
			return true;
		}
	}

	public ChatUITweenGroup(UIRect root)
	{
		if ((bool)root)
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
		if (!root)
		{
			state = STATE.OPENED;
			return;
		}
		if (closeTween.enabled)
		{
			closeTween.SetOnFinished((EventDelegate)null);
			closeTween.SetStartToCurrentValue();
			closeTween.ResetToBeginning();
			closeTween.enabled = false;
		}
		if (!openTween.gameObject.activeSelf)
		{
			openTween.gameObject.SetActive(value: true);
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

	public void OpenImmediately()
	{
		if (!root)
		{
			state = STATE.OPENED;
			return;
		}
		state = STATE.OPENED;
		openTween.Sample(1f, isFinished: true);
	}

	public void Close(Action on_finished)
	{
		if (!root)
		{
			state = STATE.CLOSED;
			return;
		}
		OnPreClose();
		if (!openTween.gameObject.activeSelf)
		{
			on_finished();
			return;
		}
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
			root.gameObject.SetActive(value: false);
			on_finished();
		});
		closeTween.PlayForward();
	}

	public void CloseImmediately()
	{
		if (!root)
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
		T val = base.rootRect.gameObject.AddComponent<T>();
		InitTween(val, isOpenTween);
		return val;
	}

	protected abstract void InitTween(T tween, bool isOpenTween);
}
