using UnityEngine;

public class ChatUISlideGroup : ChatUITweenGroup<TweenPosition>
{
	private Vector3 startPos;

	private Vector3 endPos;

	public ChatUISlideGroup(UIRect root, Vector3 start_pos, Vector3 end_pos)
		: base(root)
	{
		startPos = start_pos;
		endPos = end_pos;
	}

	protected override void InitTween(TweenPosition tween, bool isOpenTween)
	{
		if (isOpenTween)
		{
			InitTween(tween, startPos, endPos);
		}
		else
		{
			InitTween(tween, endPos, startPos);
		}
	}

	private void InitTween(TweenPosition tween, Vector3 from_position, Vector3 to_position)
	{
		tween.from = from_position;
		tween.to = to_position;
		tween.method = UITweener.Method.EaseOut;
		tween.duration = 0.25f;
		tween.enabled = false;
	}

	protected override void OnPreClose()
	{
		(base.rootRect as UIPanel).widgetsAreStatic = true;
	}

	protected override void OnPostOpen()
	{
		(base.rootRect as UIPanel).widgetsAreStatic = false;
	}
}
