using UnityEngine;

public class ChatUISlideGroup : ChatUITweenGroup<TweenPosition>
{
	private Vector3 startPos;

	private Vector3 endPos;

	public ChatUISlideGroup(UIRect root, Vector3 start_pos, Vector3 end_pos)
		: base(root)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		startPos = start_pos;
		endPos = end_pos;
	}

	protected override void InitTween(TweenPosition tween, bool isOpenTween)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		tween.from = from_position;
		tween.to = to_position;
		tween.method = UITweener.Method.EaseOut;
		tween.duration = 0.25f;
		tween.set_enabled(false);
	}

	protected override void OnPreClose()
	{
		UIPanel uIPanel = base.rootRect as UIPanel;
		uIPanel.widgetsAreStatic = true;
	}

	protected override void OnPostOpen()
	{
		UIPanel uIPanel = base.rootRect as UIPanel;
		uIPanel.widgetsAreStatic = false;
	}
}
