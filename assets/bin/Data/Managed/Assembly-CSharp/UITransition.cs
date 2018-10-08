using System;

public class UITransition
{
	public enum TYPE
	{
		NONE,
		OPEN,
		CLOSE,
		NEXT,
		PREV
	}

	public UITweener[] openTweens;

	public UITweener[] closeTweens;

	public UITweener[] nextTweens;

	public UITweener[] prevTweens;

	private int busyCount;

	private Action callback;

	public bool isBusy => busyCount != 0;

	public UITransition()
		: this()
	{
	}

	private void Awake()
	{
		InitTweens();
	}

	private void InitTweens(UITweener[] tweens)
	{
		if (tweens != null)
		{
			int i = 0;
			for (int num = tweens.Length; i < num; i++)
			{
				tweens[i].set_enabled(false);
				tweens[i].AddOnFinished(new EventDelegate(OnFinished));
			}
		}
	}

	public void InitTweens()
	{
		InitTweens(openTweens);
		InitTweens(closeTweens);
		InitTweens(nextTweens);
		InitTweens(prevTweens);
	}

	private void OnFinished()
	{
		busyCount--;
		if (busyCount == 0 && callback != null)
		{
			callback();
			callback = null;
		}
	}

	private void StartAnim(UITweener[] tweens, Action _callback)
	{
		if (busyCount == 0)
		{
			callback = _callback;
			if (tweens == null || tweens.Length == 0)
			{
				busyCount = 1;
				AppMain i = MonoBehaviourSingleton<AppMain>.I;
				i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
				{
					OnFinished();
				});
			}
			else
			{
				busyCount = tweens.Length;
				int j = 0;
				for (int num = tweens.Length; j < num; j++)
				{
					tweens[j].set_enabled(true);
					tweens[j].ResetToBeginning();
				}
				if (GameSceneManager.isAutoEventSkip && isBusy)
				{
					int k = 0;
					for (int num2 = tweens.Length; k < num2; k++)
					{
						if (tweens[k] != null)
						{
							tweens[k].tweenFactor = 1f;
							tweens[k].Sample(1f, false);
						}
					}
				}
			}
		}
	}

	public void Play(TYPE type, Action callback)
	{
		StartAnim(GetTweens(type), callback);
	}

	public void Open(Action callback)
	{
		Play(TYPE.OPEN, callback);
	}

	public void Close(Action callback)
	{
		Play(TYPE.CLOSE, callback);
	}

	public UITweener[] GetTweens(TYPE type)
	{
		switch (type)
		{
		case TYPE.OPEN:
			return openTweens;
		case TYPE.CLOSE:
			return closeTweens;
		case TYPE.NEXT:
			if (nextTweens != null && nextTweens.Length > 0)
			{
				return nextTweens;
			}
			return closeTweens;
		case TYPE.PREV:
			if (prevTweens != null && prevTweens.Length > 0)
			{
				return prevTweens;
			}
			return openTweens;
		default:
			return null;
		}
	}

	public static TYPE GetType(char c)
	{
		switch (c)
		{
		case 'O':
		case 'o':
			return TYPE.OPEN;
		case 'C':
		case 'c':
			return TYPE.CLOSE;
		case 'N':
		case 'n':
			return TYPE.NEXT;
		case 'P':
		case 'p':
			return TYPE.PREV;
		default:
			return TYPE.NONE;
		}
	}
}
