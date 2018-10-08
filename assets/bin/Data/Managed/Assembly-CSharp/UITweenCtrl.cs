using System;
using UnityEngine;

public class UITweenCtrl
{
	[SerializeField]
	private int _id;

	public UITweener[] tweens;

	protected bool isPlaying;

	private UITable uiTable;

	public int id => _id;

	public UITweenCtrl()
		: this()
	{
	}

	public static void Set(Transform root)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		UITweenCtrl component = root.GetComponent<UITweenCtrl>();
		if (!(component != null))
		{
			component = root.get_gameObject().AddComponent<UITweenCtrl>();
			UITweener[] componentsInChildren = root.GetComponentsInChildren<UITweener>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				componentsInChildren[i].set_enabled(false);
			}
			component.tweens = componentsInChildren;
		}
	}

	private static UITweenCtrl SearchTweenCtrl(Transform root, int tween_ctrl_id)
	{
		UITweenCtrl[] components = root.GetComponents<UITweenCtrl>();
		if (components == null || components.Length == 0)
		{
			return null;
		}
		UITweenCtrl c = null;
		if (components.Length == 1)
		{
			c = components[0];
		}
		else
		{
			Array.ForEach(components, delegate(UITweenCtrl tw)
			{
				if (!(c != null) && tw.id == tween_ctrl_id)
				{
					c = tw;
				}
			});
		}
		return c;
	}

	public static void Play(Transform root, bool forward = true, EventDelegate.Callback callback = null, bool is_input_block = true, int tween_ctrl_id = 0)
	{
		UITweenCtrl uITweenCtrl = SearchTweenCtrl(root, tween_ctrl_id);
		if (!(uITweenCtrl == null))
		{
			if (is_input_block)
			{
				MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.UITWEEN_SMALL, true);
			}
			uITweenCtrl.Play(forward, delegate
			{
				if (is_input_block)
				{
					MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.UITWEEN_SMALL, false);
				}
				if (callback != null)
				{
					callback();
				}
			});
		}
	}

	public static void Skip(Transform root, bool forward = true, int tween_ctrl_id = 0)
	{
		UITweenCtrl uITweenCtrl = SearchTweenCtrl(root, tween_ctrl_id);
		if (!(uITweenCtrl == null))
		{
			uITweenCtrl.Skip(forward);
		}
	}

	public static void Reset(Transform root, int tween_ctrl_id = 0)
	{
		UITweenCtrl uITweenCtrl = SearchTweenCtrl(root, tween_ctrl_id);
		if (!(uITweenCtrl == null))
		{
			uITweenCtrl.Reset();
		}
	}

	public static void SetDurationWithRate(Transform root, float rate, int tween_ctrl_id = 0)
	{
		UITweenCtrl uITweenCtrl = SearchTweenCtrl(root, tween_ctrl_id);
		UITweener[] array = uITweenCtrl.tweens;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].duration *= rate;
		}
	}

	private void Awake()
	{
		if (tweens != null && tweens.Length > 0)
		{
			FillInTheBlanks();
			Reset();
		}
	}

	public void Play(bool forward = true, EventDelegate.Callback onFinished = null)
	{
		_Play(tweens, forward, onFinished);
	}

	protected void _Play(UITweener[] target_tweens, bool forward = true, EventDelegate.Callback onFinished = null)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (target_tweens != null && target_tweens.Length != 0 && !isPlaying)
		{
			if (target_tweens[0] == null)
			{
				Log.Error("tween[0] = null!");
			}
			else
			{
				isPlaying = true;
				uiTable = this.get_gameObject().GetComponentInParent<UITable>();
				if (onFinished != null)
				{
					EventDelegate.Add(target_tweens[0].onFinished, onFinished, true);
				}
				EventDelegate.Add(target_tweens[0].onFinished, OnFinished);
				int i = 0;
				for (int num = target_tweens.Length; i < num; i++)
				{
					if (!(target_tweens[i] == null))
					{
						_TweenPlay(target_tweens[i], forward);
					}
				}
				if (GameSceneManager.isAutoEventSkip)
				{
					AppMain i2 = MonoBehaviourSingleton<AppMain>.I;
					i2.onDelayCall = (Action)Delegate.Combine(i2.onDelayCall, (Action)delegate
					{
						if (isPlaying)
						{
							int j = 0;
							for (int num2 = target_tweens.Length; j < num2; j++)
							{
								if (target_tweens[j] != null)
								{
									target_tweens[j].tweenFactor = 1f;
								}
							}
						}
					});
				}
			}
		}
	}

	protected virtual void _TweenPlay(UITweener target, bool forward)
	{
		target.Play(forward);
	}

	public void Reset()
	{
		_Reset(tweens);
	}

	protected void _Reset(UITweener[] target_tweens)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (target_tweens != null && target_tweens.Length != 0)
		{
			if (target_tweens[0] == null)
			{
				Log.Error("tween[0] = null!");
			}
			else
			{
				isPlaying = false;
				uiTable = this.get_gameObject().GetComponentInParent<UITable>();
				EventDelegate.Set(target_tweens[0].onFinished, OnFinished);
				int i = 0;
				for (int num = target_tweens.Length; i < num; i++)
				{
					if (!(target_tweens[i] == null))
					{
						_TweenReset(target_tweens[i]);
					}
				}
			}
		}
	}

	protected virtual void _TweenReset(UITweener target)
	{
		float duration = target.duration;
		float delay = target.delay;
		UITweener.Style style = target.style;
		target.duration = 0f;
		target.delay = 0f;
		target.style = UITweener.Style.Once;
		target.Play(false);
		target.style = style;
		target.duration = duration;
		target.delay = delay;
	}

	public void Skip(bool forward = true)
	{
		_Skip(tweens, forward);
	}

	protected void _Skip(UITweener[] target_tweens, bool forward = true)
	{
		if (target_tweens != null && target_tweens.Length != 0)
		{
			int i = 0;
			for (int num = target_tweens.Length; i < num; i++)
			{
				if (!(target_tweens[i] == null))
				{
					float tweenFactor = (float)(forward ? 1 : 0);
					target_tweens[i].tweenFactor = tweenFactor;
					target_tweens[i].Sample(target_tweens[i].tweenFactor, false);
				}
			}
		}
	}

	private void OnFinished()
	{
		isPlaying = false;
		if (uiTable != null)
		{
			uiTable.Reposition();
			uiTable = null;
		}
	}

	public void LateUpdate()
	{
		if (uiTable != null)
		{
			uiTable.Reposition();
		}
	}

	public void FillInTheBlanks()
	{
		int num = tweens.Length;
		for (int i = 0; i < num; i++)
		{
			if (tweens[i] == null)
			{
				num--;
				int j = i;
				for (int num2 = tweens.Length; j < num2; j++)
				{
					if (j < num2 - 1)
					{
						tweens[j] = tweens[j + 1];
					}
					else
					{
						tweens[j] = null;
					}
				}
				Array.Resize(ref tweens, num);
				i--;
			}
		}
	}
}
