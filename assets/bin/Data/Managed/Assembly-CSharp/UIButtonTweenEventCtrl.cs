using System;
using UnityEngine;

[AddComponentMenu("ProjectUI/UIButtonTweenEventCtrl")]
[RequireComponent(typeof(UIGameSceneEventSender))]
public class UIButtonTweenEventCtrl : UITweenCtrl
{
	public UITweener[] pushTweens;

	private bool isEnd;

	private void OnValidate()
	{
		if (tweens != null && tweens.Length > 0)
		{
			Array.ForEach(tweens, delegate(UITweener t)
			{
				if (t != null)
				{
					base._TweenReset(t);
					t.set_enabled(false);
				}
			});
		}
		if (pushTweens != null && pushTweens.Length > 0)
		{
			Array.ForEach(pushTweens, delegate(UITweener t)
			{
				if (t != null)
				{
					base._TweenReset(t);
					t.set_enabled(false);
				}
			});
		}
	}

	private void OnEnable()
	{
		OnValidate();
	}

	private void Strat()
	{
		UIGameSceneEventSender uIGameSceneEventSender = this.get_gameObject().GetComponent<UIGameSceneEventSender>();
		if (uIGameSceneEventSender == null)
		{
			uIGameSceneEventSender = this.get_gameObject().AddComponent<UIGameSceneEventSender>();
		}
		if (string.IsNullOrEmpty(uIGameSceneEventSender.eventName))
		{
			uIGameSceneEventSender.eventName = "NONE";
		}
	}

	public void PlayPush(bool isDown)
	{
		if (isDown)
		{
			isEnd = false;
			_Reset(pushTweens);
			isPlaying = false;
			_Play(pushTweens, isDown);
		}
		else
		{
			End(pushTweens);
		}
	}

	protected override void _TweenPlay(UITweener target, bool forward)
	{
		if (target.style != 0)
		{
			if (forward)
			{
				target.Play(forward);
			}
			else
			{
				_TweenReset(target);
			}
		}
		else
		{
			target.Play(forward);
		}
	}

	protected void OnDragOut()
	{
		if (isPlaying)
		{
			End(pushTweens);
		}
	}

	private void End(UITweener[] target_tweens)
	{
		if (target_tweens == null || target_tweens.Length == 0 || isEnd)
		{
			return;
		}
		isEnd = true;
		int i = 0;
		for (int num = target_tweens.Length; i < num; i++)
		{
			if (!(target_tweens[i] == null))
			{
				UITweener.Style style = target_tweens[i].style;
				target_tweens[i].style = UITweener.Style.Once;
				target_tweens[i].tweenFactor = 1f;
				target_tweens[i].Sample(target_tweens[i].tweenFactor, isFinished: true);
				target_tweens[i].PlayForward();
				target_tweens[i].style = style;
			}
		}
	}
}
