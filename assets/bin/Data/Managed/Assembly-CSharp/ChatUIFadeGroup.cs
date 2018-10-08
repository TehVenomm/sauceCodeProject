public class ChatUIFadeGroup : ChatUITweenGroup<TweenAlpha>
{
	public ChatUIFadeGroup(UIRect root)
		: base(root)
	{
	}

	protected override void InitTween(TweenAlpha tween, bool isOpenTween)
	{
		if (isOpenTween)
		{
			InitTween(tween, 0f, 1f);
		}
		else
		{
			InitTween(tween, 1f, 0f);
		}
	}

	private void InitTween(TweenAlpha tween, float from_alpha, float to_alpha)
	{
		tween.from = from_alpha;
		tween.to = to_alpha;
		tween.method = UITweener.Method.EaseOut;
		tween.duration = 0.25f;
		tween.set_enabled(false);
	}
}
