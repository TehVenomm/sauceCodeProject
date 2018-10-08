using UnityEngine;

public class AbilityBGWrapContent : UIWrapContent
{
	private UIScrollView scroll;

	protected override void OnMove(UIPanel panel)
	{
		if ((Object)scroll == (Object)null)
		{
			scroll = GetComponentInParent<UIScrollView>();
		}
		base.OnMove(panel);
		scroll.restrictWithinPanel = true;
	}
}
